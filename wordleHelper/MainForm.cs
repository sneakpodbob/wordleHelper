using System.Diagnostics;

namespace wordleHelper;

public partial class MainForm : Form
{
    public MainForm(Evaluator evaluator)
    {
        Evaluator = evaluator;
        InitializeComponent();
    }

    private Evaluator Evaluator { get; set; }

    private async void BtnEval_Click(object sender, EventArgs e)
    {
        listBox.Items.Clear();
        // ReSharper disable once CoVariantArrayConversion
        listBox.Items.AddRange((await Evaluator.EvaluateAsync(ChkSort.Checked)).ToArray());

        var nextLine = FindNextEmptyLine();
        if (nextLine > 0) Controls.OfType<TextBox>().First(tb => tb.Name == $"txt{nextLine}_1").Focus();
    }

    private void HandleDoubleClick(object sender, EventArgs e)
    {
        switch (sender)
        {
            case TextBox box:
                var line = byte.Parse(box.Name.Substring(3, 1));
                var column = byte.Parse(box.Name.Substring(5, 1));

                if (box.BackColor == Color.Gray)
                {
                    box.BackColor = Color.Yellow;
                    Evaluator.SetConditionYellow(line, column);
                }
                else if (box.BackColor == Color.Yellow)
                {
                    box.BackColor = Color.Green;
                    Evaluator.SetConditionGreen(line, column);
                }
                else if (box.BackColor == Color.Green)
                {
                    box.BackColor = Color.White;
                    Evaluator.UnSetCondition(line, column);
                }
                else
                {
                    box.BackColor = Color.Gray;
                    Evaluator.SetConditionGray(line, column);
                }

                break;
        }
    }

    private void HandleTextChanged(object sender, EventArgs e)
    {
        switch (sender)
        {
            case TextBox box:
                var line = byte.Parse(box.Name.Substring(3, 1));
                var column = byte.Parse(box.Name.Substring(5, 1));

                if (!string.IsNullOrWhiteSpace(box.Text))
                {
                    Evaluator.SetChar(box.Text.ToCharArray().First(), line, column);

                    box.BackColor = Color.Gray;
                    Evaluator.SetConditionGray(line, column);

                    var nextTextBox = GetNextTextBox(line, column);

                    if (nextTextBox is not null)
                    {
                        nextTextBox.SelectionStart = 0;
                        nextTextBox.SelectionLength = 1;
                        nextTextBox.Focus();
                    }

                    if (column == 5) BtnEval_Click(sender, e);
                }
                else
                {
                    Evaluator.SetChar(null, line, column);
                    box.BackColor = Color.White;
                    Evaluator.UnSetCondition(line, column);
                }

                break;
        }
    }

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
        return textBoxs.First(tb => tb.Name == $"txt{nextLine}_{nextColumn}");
    }

    private void ChkSort_CheckedChanged(object sender, EventArgs e)
    {
        BtnEval_Click(sender, e);
    }

    private void ListBox_DoubleClick(object sender, EventArgs e)
    {
        if (listBox.Items.Count == 0) return;

        var word = listBox.Items[listBox.SelectedIndex].ToString();

        var nextLine = FindNextEmptyLine();
        if (nextLine > 0) SetWordToLine(nextLine, word);
    }

    private void SetWordToLine(byte nextLine, string? word)
    {
        if (word is null) return;
        var textBoxs = Controls.OfType<TextBox>().ToList();

        var sender = txt1_1;

        foreach (var i in Enumerable.Range(0, 5))
        {
            sender = textBoxs.First(tb => tb.Name == $"txt{nextLine}_{i + 1}");
            sender.Text = word[i].ToString();
            Evaluator.SetChar(sender.Text.ToCharArray().First(), nextLine, (byte)(i + 1));
            sender.BackColor = Color.Gray;
            Evaluator.SetConditionGray(nextLine, (byte)(i + 1));
        }

        if (nextLine + 1 < 6)
        {
            var textBox = textBoxs.First(tb => tb.Name == $"txt{nextLine + 1}_1");
            textBox.Focus();
        }

        BtnEval_Click(sender, EventArgs.Empty);
    }

    private byte FindNextEmptyLine()
    {
        if (string.IsNullOrWhiteSpace(txt1_1.Text) &&
            string.IsNullOrWhiteSpace(txt1_2.Text) &&
            string.IsNullOrWhiteSpace(txt1_3.Text) &&
            string.IsNullOrWhiteSpace(txt1_4.Text) &&
            string.IsNullOrWhiteSpace(txt1_5.Text))
            return 1;
        if (string.IsNullOrWhiteSpace(txt2_1.Text) &&
            string.IsNullOrWhiteSpace(txt2_2.Text) &&
            string.IsNullOrWhiteSpace(txt2_3.Text) &&
            string.IsNullOrWhiteSpace(txt2_4.Text) &&
            string.IsNullOrWhiteSpace(txt2_5.Text))
            return 2;

        if (string.IsNullOrWhiteSpace(txt3_1.Text) &&
            string.IsNullOrWhiteSpace(txt3_2.Text) &&
            string.IsNullOrWhiteSpace(txt3_3.Text) &&
            string.IsNullOrWhiteSpace(txt3_4.Text) &&
            string.IsNullOrWhiteSpace(txt3_5.Text))
            return 3;

        if (string.IsNullOrWhiteSpace(txt4_1.Text) &&
            string.IsNullOrWhiteSpace(txt4_2.Text) &&
            string.IsNullOrWhiteSpace(txt4_3.Text) &&
            string.IsNullOrWhiteSpace(txt4_4.Text) &&
            string.IsNullOrWhiteSpace(txt4_5.Text))
            return 4;

        if (string.IsNullOrWhiteSpace(txt5_1.Text) &&
            string.IsNullOrWhiteSpace(txt5_2.Text) &&
            string.IsNullOrWhiteSpace(txt5_3.Text) &&
            string.IsNullOrWhiteSpace(txt5_4.Text) &&
            string.IsNullOrWhiteSpace(txt5_5.Text))
            return 5;

        return 0;
    }

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

    private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void ToolStripStatusLabel2_Click(object sender, EventArgs e)
    {
        Process.Start(new ProcessStartInfo("cmd", $"/c start https://www.powerlanguage.co.uk/wordle") { CreateNoWindow = true });
    }
}