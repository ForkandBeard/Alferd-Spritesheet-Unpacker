using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ForkandBeard.Logic.Controls.Panels
{
    public partial class FormatStack : UserControl
    {
        private bool toggleIsUnformatted = true;

        public FormatStack()
        {
            InitializeComponent();
        }

        private void toggleFormatClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.toggleIsUnformatted)
                {
                    string stack;

                    if (this.unformattedStackTextBox.Text.Trim().Length == 0)
                    {
                        this.unformattedStackTextBox.Text = Clipboard.GetText();
                    }
                    stack = this.unformattedStackTextBox.Text;
                    this.stackTextBox.Visible = true;
                    this.unformattedStackTextBox.Visible = false;
                    this.stackTextBox.FormatStack(stack);
                    this.toggleFormatClearButton.Text = "clear";
                }
                else
                {
                    this.unformattedStackTextBox.Text = String.Empty;
                    this.unformattedStackTextBox.Visible = true;
                    this.stackTextBox.Visible = false;
                    this.toggleFormatClearButton.Text = "paste + format";
                }

                this.toggleIsUnformatted = !this.toggleIsUnformatted;
            }
            catch (Exception ex)
            {
                try
                {
                    this.unformattedStackTextBox.Text = String.Empty;
                    this.unformattedStackTextBox.Visible = true;
                    this.stackTextBox.Visible = false;
                    this.toggleFormatClearButton.Text = "format";
                    this.toggleIsUnformatted = true;
                }
                catch { }
                ExceptionHandler.HandleException(ex);
            }
        }

        private void unformattedStackTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.unformattedStackTextBox.Text.Trim().Length > 0)
            {
                this.toggleFormatClearButton.Text = "format";
            }
            else
            {
                this.toggleFormatClearButton.Text = "paste + format";
            }
        }
    }
}
