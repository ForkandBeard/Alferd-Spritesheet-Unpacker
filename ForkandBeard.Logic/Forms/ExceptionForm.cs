using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ForkandBeard.Logic.Forms
{
    public partial class ExceptionForm : Form
    {
        private string toEmailAddress;
        private Exception exception;
        private bool changed = false;
        public bool IsClosed {get; set;}

        public ExceptionForm(Exception exception, string to)
        {
            InitializeComponent();
            this.SetException(exception);
            this.SetEmailAddress(to);
            this.UpdateException();
            this.UpdateEmailAddress();
        }

        public void SetException(Exception exception)
        {
            this.exception = exception;
            this.changed = true;
        }

        public void SetEmailAddress(string to)
        {
            this.toEmailAddress = to;
            this.changed = true;
        }

        public void UpdateException()
        {
            this.exceptionMessageTextBox.Text = exception.Message;
            this.stackFormatted.FormatStack(exception.ToString());
            this.okButton.Select();
        }

        public void UpdateEmailAddress()
        {
            this.emailButton.Visible = !String.IsNullOrEmpty(this.toEmailAddress);
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.stackFormatted.stackRichTextBox.Text);
        }

        private void emailButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(
                                            String.Format("mailto:{0}?subject={1}&body={2}"
                                            , this.toEmailAddress                                            
                                            , String.Format("An error occurred in: {0}", Names.GetApplicationNameAndMajorVersion())
                                            , this.stackFormatted.stackRichTextBox.Text.Replace(@"&", " and "))
                                            );
        }

        private void updateTextTimer_Tick(object sender, EventArgs e)
        {
            if (this.changed)
            {
                this.UpdateException();
                this.UpdateEmailAddress();
                this.changed = false;
            }
        }

        private void ExceptionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.IsClosed = true;
        }
    }
}
