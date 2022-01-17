namespace wordleHelper
{
    public partial class MainForm : Form
    {
        public MainForm(EvalCorp evaluator)
        {
            Evaluator = evaluator;
            InitializeComponent();
        }

        public EvalCorp Evaluator { get; set; }

        private void btnEval_Click(object sender, EventArgs e)
        {
            listBox.Items.Clear();
            // ReSharper disable once CoVariantArrayConversion
            listBox.Items.AddRange(items: Evaluator.Evaluate().ToArray());
        }

        private void txt1_1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt1_1.Text))
            {
                Evaluator.SetChar(txt1_1.Text.ToCharArray().First(), 1, 1);
                txt1_2.SelectionStart = 0;
                txt1_2.SelectionLength = 1;
                txt1_2.Focus();
            }
            else
            {
                Evaluator.SetChar(null, 1, 1);
            }
        }

        private void txt1_2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt1_2.Text))
            {
                Evaluator.SetChar(txt1_2.Text.ToCharArray().First(), 1, 2);
                txt1_3.SelectionStart = 0;
                txt1_3.SelectionLength = 1;
                txt1_3.Focus();
            }
            else
            {
                Evaluator.SetChar(null, 1, 2);
            }
        }

        private void txt1_3_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt1_3.Text))
            {
                Evaluator.SetChar(txt1_3.Text.ToCharArray().First(), 1, 3);
                txt1_4.SelectionStart = 0;
                txt1_4.SelectionLength = 1;
                txt1_4.Focus();
            }
            else
            {
                Evaluator.SetChar(null, 1, 2);
            }
        }

        private void txt1_4_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt1_4.Text))
            {
                Evaluator.SetChar(txt1_4.Text.ToCharArray().First(), 1, 4);
                txt1_5.SelectionStart = 0;
                txt1_5.SelectionLength = 1;
                txt1_5.Focus();
            }
            else
            {
                Evaluator.SetChar(null, 1, 4);
            }
        }

        private void txt1_5_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt1_5.Text))
            {
                Evaluator.SetChar(txt1_5.Text.ToCharArray().First(), 1, 5);
            }
            else
            {
                Evaluator.SetChar(null, 1, 5);
            }

            btnEval_Click(sender, e);
        }

        private void txt2_1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt2_1.Text))
            {
                Evaluator.SetChar(txt2_1.Text.ToCharArray().First(), 2, 1);
                txt2_2.SelectionStart = 0;
                txt2_2.SelectionLength = 1;
                txt2_2.Focus();
            }
            else
            {
                Evaluator.SetChar(null, 2, 1);
            }
        }

        private void txt2_2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt2_2.Text))
            {
                Evaluator.SetChar(txt2_2.Text.ToCharArray().First(), 2, 2);
                txt2_3.SelectionStart = 0;
                txt2_3.SelectionLength = 1;
                txt2_3.Focus();
            }
            else
            {
                Evaluator.SetChar(null, 2, 2);
            }

        }

        private void txt2_3_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt2_3.Text))
            {
                Evaluator.SetChar(txt2_3.Text.ToCharArray().First(), 2, 3);
                txt2_4.SelectionStart = 0;
                txt2_4.SelectionLength = 1;
                txt2_4.Focus();
            }
            else
            {
                Evaluator.SetChar(null, 2, 3);
            }

        }

        private void txt2_4_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt2_4.Text))
            {
                Evaluator.SetChar(txt2_4.Text.ToCharArray().First(), 2, 4);
                txt2_5.SelectionStart = 0;
                txt2_5.SelectionLength = 1;
                txt2_5.Focus();
            }
            else
            {
                Evaluator.SetChar(null, 2, 4);
            }

        }

        private void txt2_5_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt2_5.Text))
            {
                Evaluator.SetChar(txt2_5.Text.ToCharArray().First(), 2, 5);
            }
            else
            {
                Evaluator.SetChar(null, 2, 5);
            }

            btnEval_Click(sender, e);
        }

        private void txt3_1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt3_1.Text))
            {
                Evaluator.SetChar(txt3_1.Text.ToCharArray().First(), 3, 1);
                txt3_2.SelectionStart = 0;
                txt3_2.SelectionLength = 1;
                txt3_2.Focus();
            }
            else
            {
                Evaluator.SetChar(null, 3, 1);
            }
        }

        private void txt3_2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt3_2.Text))
            {
                Evaluator.SetChar(txt3_2.Text.ToCharArray().First(), 3, 2);
                txt3_3.SelectionStart = 0;
                txt3_3.SelectionLength = 1;
                txt3_3.Focus();
            }
            else
            {
                Evaluator.SetChar(null, 3, 2);
            }
        }

        private void txt3_3_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt3_3.Text))
            {
                Evaluator.SetChar(txt3_3.Text.ToCharArray().First(), 3, 3);
                txt3_4.SelectionStart = 0;
                txt3_4.SelectionLength = 1;
                txt3_4.Focus();
            }
            else
            {
                Evaluator.SetChar(null, 3, 3);
            }

        }

        private void txt3_4_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt3_4.Text))
            {
                Evaluator.SetChar(txt3_4.Text.ToCharArray().First(), 3, 4);
                txt3_5.SelectionStart = 0;
                txt3_5.SelectionLength = 1;
                txt3_5.Focus();
            }
            else
            {
                Evaluator.SetChar(null, 3, 4);
            }

        }

        private void txt3_5_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt3_5.Text))
            {
                Evaluator.SetChar(txt3_5.Text.ToCharArray().First(), 3, 5);
            }
            else
            {
                Evaluator.SetChar(null, 3, 5);
            }

            btnEval_Click(sender, e);
        }

        private void txt4_1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt4_1.Text))
            {
                Evaluator.SetChar(txt4_1.Text.ToCharArray().First(), 4, 1);
                txt4_2.SelectionStart = 0;
                txt4_2.SelectionLength = 1;
                txt4_2.Focus();
            }
            else
            {
                Evaluator.SetChar(null, 4, 1);
            }
        }

        private void txt4_2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt4_2.Text))
            {
                Evaluator.SetChar(txt4_2.Text.ToCharArray().First(), 4, 2);
                txt4_3.SelectionStart = 0;
                txt4_3.SelectionLength = 1;
                txt4_3.Focus();
            }
            else
            {
                Evaluator.SetChar(null, 4, 2);
            }
        }

        private void txt4_3_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt4_3.Text))
            {
                Evaluator.SetChar(txt4_3.Text.ToCharArray().First(), 4, 3);
                txt4_4.SelectionStart = 0;
                txt4_4.SelectionLength = 1;
                txt4_4.Focus();
            }
            else
            {
                Evaluator.SetChar(null, 4, 3);
            }
        }

        private void txt4_4_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt4_4.Text))
            {
                Evaluator.SetChar(txt4_4.Text.ToCharArray().First(), 4, 4);
                txt4_5.SelectionStart = 0;
                txt4_5.SelectionLength = 1;
                txt4_5.Focus();
            }
            else
            {
                Evaluator.SetChar(null, 4, 4);
            }
        }

        private void txt4_5_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt4_5.Text))
            {
                Evaluator.SetChar(txt4_5.Text.ToCharArray().First(), 4, 5);
            }
            else
            {
                Evaluator.SetChar(null, 4, 5);
            }

            btnEval_Click(sender, e);
        }

        private void txt5_1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt5_1.Text))
            {
                Evaluator.SetChar(txt5_1.Text.ToCharArray().First(), 5, 1);
                txt5_2.SelectionStart = 0;
                txt5_2.SelectionLength = 1;
                txt5_2.Focus();
            }
            else
            {
                Evaluator.SetChar(null, 5, 1);
            }
        }

        private void txt5_2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt5_2.Text))
            {
                Evaluator.SetChar(txt5_2.Text.ToCharArray().First(), 5, 2);
                txt5_3.SelectionStart = 0;
                txt5_3.SelectionLength = 1;
                txt5_3.Focus();
            }
            else
            {
                Evaluator.SetChar(null, 5, 2);
            }
        }

        private void txt5_3_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt5_3.Text))
            {
                Evaluator.SetChar(txt5_3.Text.ToCharArray().First(), 5, 3);
                txt5_4.SelectionStart = 0;
                txt5_4.SelectionLength = 1;
                txt5_4.Focus();
            }
            else
            {
                Evaluator.SetChar(null, 5, 3);
            }
        }

        private void txt5_4_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt5_4.Text))
            {
                Evaluator.SetChar(txt5_4.Text.ToCharArray().First(), 5, 4);
                txt5_5.SelectionStart = 0;
                txt5_5.SelectionLength = 1;
                txt5_5.Focus();
            }
            else
            {
                Evaluator.SetChar(null, 5, 4);
            }
        }

        private void txt5_5_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt5_5.Text))
            {
                Evaluator.SetChar(txt5_5.Text.ToCharArray().First(), 5, 5);
            }
            else
            {
                Evaluator.SetChar(null, 5, 5);
            }

            btnEval_Click(sender, e);
        }

        private void HandleDoubleClick(object sender)
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


        private void txt1_1_DoubleClick(object sender, EventArgs e)
        {
            HandleDoubleClick(sender);
        }

        private void txt1_2_DoubleClick(object sender, EventArgs e)
        {
            HandleDoubleClick(sender);
        }

        private void txt1_3_DoubleClick(object sender, EventArgs e)
        {
            HandleDoubleClick(sender);
        }

        private void txt1_4_DoubleClick(object sender, EventArgs e)
        {
            HandleDoubleClick(sender);
        }

        private void txt1_5_DoubleClick(object sender, EventArgs e)
        {
            HandleDoubleClick(sender);
        }

        private void txt2_1_DoubleClick(object sender, EventArgs e)
        {
            HandleDoubleClick(sender);
        }

        private void txt2_2_DoubleClick(object sender, EventArgs e)
        {
            HandleDoubleClick(sender);
        }

        private void txt2_3_DoubleClick(object sender, EventArgs e)
        {
            HandleDoubleClick(sender);
        }

        private void txt2_4_DoubleClick(object sender, EventArgs e)
        {
            HandleDoubleClick(sender);
        }

        private void txt2_5_DoubleClick(object sender, EventArgs e)
        {
            HandleDoubleClick(sender);
        }

        private void txt3_1_DoubleClick(object sender, EventArgs e)
        {
            HandleDoubleClick(sender);
        }

        private void txt3_2_DoubleClick(object sender, EventArgs e)
        {
            HandleDoubleClick(sender);
        }

        private void txt3_3_DoubleClick(object sender, EventArgs e)
        {
            HandleDoubleClick(sender);
        }

        private void txt3_4_DoubleClick(object sender, EventArgs e)
        {
            HandleDoubleClick(sender);
        }

        private void txt3_5_DoubleClick(object sender, EventArgs e)
        {
            HandleDoubleClick(sender);
        }

        private void txt4_1_DoubleClick(object sender, EventArgs e)
        {
            HandleDoubleClick(sender);
        }

        private void txt4_2_DoubleClick(object sender, EventArgs e)
        {
            HandleDoubleClick(sender);
        }

        private void txt4_3_DoubleClick(object sender, EventArgs e)
        {
            HandleDoubleClick(sender);
        }

        private void txt4_4_DoubleClick(object sender, EventArgs e)
        {
            HandleDoubleClick(sender);
        }

        private void txt4_5_DoubleClick(object sender, EventArgs e)
        {
            HandleDoubleClick(sender);
        }

        private void txt5_1_DoubleClick(object sender, EventArgs e)
        {
            HandleDoubleClick(sender);
        }

        private void txt5_2_DoubleClick(object sender, EventArgs e)
        {
            HandleDoubleClick(sender);
        }

        private void txt5_3_DoubleClick(object sender, EventArgs e)
        {
            HandleDoubleClick(sender);
        }

        private void txt5_4_DoubleClick(object sender, EventArgs e)
        {
            HandleDoubleClick(sender);
        }

        private void txt5_5_DoubleClick(object sender, EventArgs e)
        {
            HandleDoubleClick(sender);
        }
    }
}