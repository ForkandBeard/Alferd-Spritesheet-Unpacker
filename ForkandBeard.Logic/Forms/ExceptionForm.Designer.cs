namespace ForkandBeard.Logic.Forms
{
    partial class ExceptionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExceptionForm));
            this.exceptionMessageTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.copyButton = new System.Windows.Forms.Button();
            this.emailButton = new System.Windows.Forms.Button();
            this.updateTextTimer = new System.Windows.Forms.Timer(this.components);
            this.stackFormatted = new ForkandBeard.Logic.Controls.Data.Stack();
            this.SuspendLayout();
            // 
            // exceptionMessageTextBox
            // 
            this.exceptionMessageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.exceptionMessageTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.exceptionMessageTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exceptionMessageTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.exceptionMessageTextBox.Location = new System.Drawing.Point(1, 4);
            this.exceptionMessageTextBox.Multiline = true;
            this.exceptionMessageTextBox.Name = "exceptionMessageTextBox";
            this.exceptionMessageTextBox.ReadOnly = true;
            this.exceptionMessageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.exceptionMessageTextBox.Size = new System.Drawing.Size(386, 76);
            this.exceptionMessageTextBox.TabIndex = 0;
            this.exceptionMessageTextBox.Text = "Error occurred:";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.Location = new System.Drawing.Point(303, 362);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(84, 41);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "close";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // copyButton
            // 
            this.copyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.copyButton.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copyButton.Location = new System.Drawing.Point(1, 362);
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(84, 32);
            this.copyButton.TabIndex = 4;
            this.copyButton.Text = "copy";
            this.copyButton.UseVisualStyleBackColor = true;
            this.copyButton.Click += new System.EventHandler(this.copyButton_Click);
            // 
            // emailButton
            // 
            this.emailButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.emailButton.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emailButton.Location = new System.Drawing.Point(91, 362);
            this.emailButton.Name = "emailButton";
            this.emailButton.Size = new System.Drawing.Size(84, 32);
            this.emailButton.TabIndex = 5;
            this.emailButton.Text = "email...";
            this.emailButton.UseVisualStyleBackColor = true;
            this.emailButton.Visible = false;
            this.emailButton.Click += new System.EventHandler(this.emailButton_Click);
            // 
            // updateTextTimer
            // 
            this.updateTextTimer.Enabled = true;
            this.updateTextTimer.Interval = 5000;
            this.updateTextTimer.Tick += new System.EventHandler(this.updateTextTimer_Tick);
            // 
            // stackFormatted
            // 
            this.stackFormatted.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stackFormatted.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stackFormatted.Location = new System.Drawing.Point(1, 86);
            this.stackFormatted.Name = "stackFormatted";
            this.stackFormatted.Size = new System.Drawing.Size(386, 270);
            this.stackFormatted.TabIndex = 3;
            // 
            // ExceptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(390, 415);
            this.Controls.Add(this.emailButton);
            this.Controls.Add(this.copyButton);
            this.Controls.Add(this.stackFormatted);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.exceptionMessageTextBox);
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(352, 393);
            this.Name = "ExceptionForm";
            this.Text = "Exception";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ExceptionForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox exceptionMessageTextBox;
        private System.Windows.Forms.Button okButton;
        private Controls.Data.Stack stackFormatted;
        private System.Windows.Forms.Button copyButton;
        private System.Windows.Forms.Button emailButton;
        private System.Windows.Forms.Timer updateTextTimer;
    }
}