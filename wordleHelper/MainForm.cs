/*
 *  wordleHelper - A little Windows-App to cheat when playing wordle.
    Copyright (C) 2022 Robert Krüger

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System.Diagnostics;

namespace wordleHelper;

/// <summary>
/// Our Main (and only) Form.
/// </summary>
public partial class MainForm : Form
{
    /// <summary>
    /// ctor
    /// </summary>
    public MainForm()
    {
        Evaluator = new Evaluator();
        InitializeComponent();
    }

    /// <summary>
    /// stores the state for later evaluation
    /// </summary>
    private Evaluator Evaluator { get; set; }

    /// <summary>
    /// EventHandler to Handle Click on "FILTER/EVALUATE/GETRESULTS" Button
    /// </summary>
    /// <param name="sender">not used</param>
    /// <param name="e">not used</param>
    private async void BtnEval_Click(object sender, EventArgs e)
    {
        listBox.Items.Clear();
        // ReSharper disable once CoVariantArrayConversion
        listBox.Items.AddRange(items: (await Evaluator.EvaluateAsync(sort: ChkSort.Checked)).ToArray());

        var nextLine = FindNextEmptyLine();
        if (nextLine > 0) Controls.OfType<TextBox>().First(predicate: tb => tb.Name == $"txt{nextLine}_1").Focus();
    }

    /// <summary>
    /// EventHandler to Handle DoubleClick on any of our TextBoxes
    /// so they change Color/Status
    /// </summary>
    /// <param name="sender">used to determine which TextBox the event came from</param>
    /// <param name="e">not used</param>
    private void HandleDoubleClick(object sender, EventArgs e)
    {
        switch (sender)
        {
            case TextBox box:
                var line = byte.Parse(s: box.Name.Substring(startIndex: 3, length: 1));
                var column = byte.Parse(s: box.Name.Substring(startIndex: 5, length: 1));

                var nextColor = GetNextColorInCycle(color: box.BackColor);
                box.BackColor = nextColor;
                Evaluator.SetColor(color: nextColor, line: line, column: column);

                break;
        }
    }

    /// <summary>
    /// Helper to represent the cycle of Colors when user clicks through
    /// </summary>
    /// <param name="color">Old Color (Valid Input: Gray, Yellow, Green, White</param>
    /// <returns>New Color</returns>
    /// <exception cref="ArgumentOutOfRangeException">thrown if invalid input color is given</exception>
    private static Color GetNextColorInCycle(Color color)
    {
        var nextColorDict = new Dictionary<Color, Color>
        {
            {Color.Gray, Color.Yellow},
            {Color.Yellow, Color.Green},
            {Color.Green, Color.White},
            {Color.White, Color.Gray}
        };

        if (!nextColorDict.ContainsKey(key: color))
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(color), actualValue: color, message: null);
        }

        return nextColorDict[key: color];
    }

    /// <summary>
    /// Eventhandler to handle TextChanged-Events in all of the Textboxes.
    /// </summary>
    /// <param name="sender">used to determine which textbox changed</param>
    /// <param name="e">not used</param>
    private void HandleTextChanged(object sender, EventArgs e)
    {
        switch (sender)
        {
            case TextBox box:
                var line = byte.Parse(s: box.Name.Substring(startIndex: 3, length: 1));
                var column = byte.Parse(s: box.Name.Substring(startIndex: 5, length: 1));

                if (!string.IsNullOrWhiteSpace(value: box.Text))
                {
                    Evaluator.SetChar(letter: box.Text.ToCharArray().First(), line: line, column: column);

                    box.BackColor = Color.Gray;
                    Evaluator.SetColor(color: Color.Gray, line: line, column: column);

                    var nextTextBox = GetNextTextBox(line: line, column: column);

                    if (nextTextBox is not null)
                    {
                        nextTextBox.SelectionStart = 0;
                        nextTextBox.SelectionLength = 1;
                        nextTextBox.Focus();
                    }

                    if (column == 5) BtnEval_Click(sender: sender, e: e);
                }
                else
                {
                    Evaluator.SetChar(letter: null, line: line, column: column);
                    box.BackColor = Color.White;
                    Evaluator.SetColor(color: Color.White, line: line, column: column);
                }

                break;
        }
    }

    /// <summary>
    /// Helper - get's the NextTextBox in Order
    /// </summary>
    /// <param name="line">Line of Last used TB (1based-index)</param>
    /// <param name="column">Column of Last used TB (1based-index)</param>
    /// <returns></returns>
    private TextBox? GetNextTextBox(byte line, byte column)
    {
        var nextLine = line;
        var nextColumn = column;

        switch (column)
        {
            case 5 when line == 5:
                return null;
            case < 5:
                nextColumn = (byte)(column + 1);
                break;
            case 5:
                nextColumn = 1;
                nextLine = (byte)(line + 1);
                break;
        }

        var textBoxs = Controls.OfType<TextBox>().ToList();
        return textBoxs.First(predicate: tb => tb.Name == $"txt{nextLine}_{nextColumn}");
    }

    /// <summary>
    /// EventHandler for change in sorting.. reevaluates and then refills the TB by "clicking the button"
    /// </summary>
    /// <param name="sender">not used</param>
    /// <param name="e">not used</param>
    private void ChkSort_CheckedChanged(object sender, EventArgs e)
    {
        BtnEval_Click(sender: sender, e: e);
    }

    /// <summary>
    /// EventHandler to Handle DoubleClick so user can quickly apply the double clicked word
    /// to the next empty line. If not empty line in the grid is left, nothing happens.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ListBox_DoubleClick(object sender, EventArgs e)
    {
        if (listBox.Items.Count == 0) return;

        var word = listBox.Items[index: listBox.SelectedIndex].ToString();

        var nextLine = FindNextEmptyLine();
        if (nextLine > 0) SetWordToLine(nextLine: nextLine, word: word);
    }

    /// <summary>
    /// Fills line at the given lineNumber with given word.
    /// </summary>
    /// <param name="nextLine">LineNumber (1based-index)</param>
    /// <param name="word">The word to put in.</param>
    private void SetWordToLine(byte nextLine, string? word)
    {
        if (word is null || word.Length != 5) return;
        
        var textBoxs = Controls.OfType<TextBox>().ToList();

        var sender = txt1_1;

        foreach (var i in Enumerable.Range(start: 0, count: 5))
        {
            sender = textBoxs.First(predicate: tb => tb.Name == $"txt{nextLine}_{i + 1}");
            sender.Text = word[index: i].ToString();
            Evaluator.SetChar(letter: sender.Text.ToCharArray().First(), line: nextLine, column: (byte)(i + 1));
            sender.BackColor = Color.Gray;
            Evaluator.SetColor(color: Color.Gray, line: nextLine, column: (byte)(i + 1));
        }

        if (nextLine + 1 < 6)
        {
            var textBox = textBoxs.First(predicate: tb => tb.Name == $"txt{nextLine + 1}_1");
            textBox.Focus();
        }

        BtnEval_Click(sender: sender, e: EventArgs.Empty);
    }

    /// <summary>
    /// Returns the next completely empty line (number)
    /// If no empty line is left, 0 is returned.
    /// </summary>
    /// <returns>line number (1based index) - 0 if no empty line found</returns>
    private byte FindNextEmptyLine()
    {
        if (string.IsNullOrWhiteSpace(value: txt1_1.Text) &&
            string.IsNullOrWhiteSpace(value: txt1_2.Text) &&
            string.IsNullOrWhiteSpace(value: txt1_3.Text) &&
            string.IsNullOrWhiteSpace(value: txt1_4.Text) &&
            string.IsNullOrWhiteSpace(value: txt1_5.Text))
            return 1;
        if (string.IsNullOrWhiteSpace(value: txt2_1.Text) &&
            string.IsNullOrWhiteSpace(value: txt2_2.Text) &&
            string.IsNullOrWhiteSpace(value: txt2_3.Text) &&
            string.IsNullOrWhiteSpace(value: txt2_4.Text) &&
            string.IsNullOrWhiteSpace(value: txt2_5.Text))
            return 2;

        if (string.IsNullOrWhiteSpace(value: txt3_1.Text) &&
            string.IsNullOrWhiteSpace(value: txt3_2.Text) &&
            string.IsNullOrWhiteSpace(value: txt3_3.Text) &&
            string.IsNullOrWhiteSpace(value: txt3_4.Text) &&
            string.IsNullOrWhiteSpace(value: txt3_5.Text))
            return 3;

        if (string.IsNullOrWhiteSpace(value: txt4_1.Text) &&
            string.IsNullOrWhiteSpace(value: txt4_2.Text) &&
            string.IsNullOrWhiteSpace(value: txt4_3.Text) &&
            string.IsNullOrWhiteSpace(value: txt4_4.Text) &&
            string.IsNullOrWhiteSpace(value: txt4_5.Text))
            return 4;

        if (string.IsNullOrWhiteSpace(value: txt5_1.Text) &&
            string.IsNullOrWhiteSpace(value: txt5_2.Text) &&
            string.IsNullOrWhiteSpace(value: txt5_3.Text) &&
            string.IsNullOrWhiteSpace(value: txt5_4.Text) &&
            string.IsNullOrWhiteSpace(value: txt5_5.Text))
            return 5;

        return 0;
    }

    /// <summary>
    /// EventHandler for MenuItem that user wants to start over.
    /// Empties all Textboxes, the Listboxes and re-instanciates the Evaluator
    /// to clean up everything.
    /// </summary>
    /// <param name="sender">not used</param>
    /// <param name="e">not used</param>
    private void StartOverToolStripMenuItem_Click(object sender, EventArgs e)
    {
        foreach (var textBox in Controls.OfType<TextBox>().ToList())
        {
            textBox.BackColor = Color.White;
            textBox.Text = string.Empty;
        }

        Evaluator = new Evaluator();
        listBox.Items.Clear();
    }

    /// <summary>
    /// EventHandler for MenuItem when User wants to exit the app.
    /// </summary>
    /// <param name="sender">not used</param>
    /// <param name="e">not used</param>
    private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    /// <summary>
    /// EventHandler for StatusBarItem, User wants to go the Wordle-Page and Play
    /// </summary>
    /// <param name="sender">not used</param>
    /// <param name="e">not used</param>
    private void ToolStripStatusLabel2_Click(object sender, EventArgs e)
    {
        Process.Start(startInfo: new ProcessStartInfo(fileName: "cmd", arguments: "/c start https://www.powerlanguage.co.uk/wordle") { CreateNoWindow = true });
    }

    /// <summary>
    /// EventHandler for StatusBarItem, User wants to go the projects GitHub-Page
    /// </summary>
    /// <param name="sender">not used</param>
    /// <param name="e">not used</param>
    private void GitHubToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Process.Start(startInfo: new ProcessStartInfo(fileName: "cmd", arguments: "/c start https://github.com/sneakpodbob/wordleHelper") { CreateNoWindow = true });
    }
}