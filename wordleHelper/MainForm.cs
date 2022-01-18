namespace wordleHelper
{
    public partial class MainForm : Form
    {
        public MainForm(EvalCorp evaluator)
        {
            Evaluator = evaluator;
            InitializeComponent();
        }

        private EvalCorp Evaluator { get; set; }

        private void btnEval_Click(object sender, EventArgs e)
        {
            listBox.Items.Clear();
            // ReSharper disable once CoVariantArrayConversion
            listBox.Items.AddRange(items: Evaluator.Evaluate(ChkSort.Checked).ToArray());
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

                        if (row == 5) btnEval_Click(sender, e);
                    }
                    else
                    {
                        Evaluator.SetChar(null, line, row);
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
            btnEval_Click(sender, e);
        }
    }
}