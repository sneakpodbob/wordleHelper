namespace wordleHelper;

public partial class MainForm : Form
{
    public MainForm(EvalCorp evaluator)
    {
        Evaluator = evaluator;
        InitializeComponent();
    }

    private EvalCorp Evaluator { get; set; }

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
                var row = byte.Parse(box.Name.Substring(5, 1));

                if (box.BackColor == Color.Gray)
                {
                    box.BackColor = Color.Yellow;
                    Evaluator.SetConditionYellow(line, row);
                }
                else if (box.BackColor == Color.Yellow)
                {
                    box.BackColor = Color.Green;
                    Evaluator.SetConditionGreen(line, row);
                }
                else if (box.BackColor == Color.Green)
                {
                    box.BackColor = Color.White;
                    Evaluator.UnSetCondition(line, row);
                }
                else
                {
                    box.BackColor = Color.Gray;
                    Evaluator.SetConditionGray(line, row);
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
                var row = byte.Parse(box.Name.Substring(5, 1));

                if (!string.IsNullOrWhiteSpace(box.Text))
                {
                    Evaluator.SetChar(box.Text.ToCharArray().First(), line, row);

                    box.BackColor = Color.Gray;
                    Evaluator.SetConditionGray(line, row);

                    var nextTextBox = GetNextTextBox(line, row);

                    if (nextTextBox is not null)
                    {
                        nextTextBox.SelectionStart = 0;
                        nextTextBox.SelectionLength = 1;
                        nextTextBox.Focus();
                    }

                    if (row == 5) BtnEval_Click(sender, e);
                }
                else
                {
                    Evaluator.SetChar(null, line, row);
                    box.BackColor = Color.White;
                    Evaluator.UnSetCondition(line, row);
                }

                break;
        }
    }

    private TextBox? GetNextTextBox(byte line, byte row)
    {
        var nextLine = line;
        var nextRow = row;

        switch (row)
        {
            case 5 when line == 5:
                return null;
            case < 5:
                nextRow = (byte)(row + 1);
                break;
            case 5:
                nextRow = 1;
                nextLine = (byte)(line + 1);
                break;
        }

        var textBoxs = Controls.OfType<TextBox>().ToList();
        return textBoxs.First(tb => tb.Name == $"txt{nextLine}_{nextRow}");
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

        Evaluator = new EvalCorp();
        listBox.Items.Clear();
    }

    private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }
}