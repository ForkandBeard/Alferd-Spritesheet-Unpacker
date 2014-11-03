namespace ForkandBeard.Logic.Controls.Panels
{
    partial class FormatStack
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toggleFormatClearButton = new System.Windows.Forms.Button();
            this.unformattedStackTextBox = new System.Windows.Forms.TextBox();
            this.stackTextBox = new ForkandBeard.Logic.Controls.Data.Stack();
            this.SuspendLayout();
            // 
            // toggleFormatClearButton
            // 
            this.toggleFormatClearButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toggleFormatClearButton.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleFormatClearButton.Location = new System.Drawing.Point(0, 228);
            this.toggleFormatClearButton.Name = "toggleFormatClearButton";
            this.toggleFormatClearButton.Size = new System.Drawing.Size(353, 39);
            this.toggleFormatClearButton.TabIndex = 1;
            this.toggleFormatClearButton.Text = "paste + format";
            this.toggleFormatClearButton.UseVisualStyleBackColor = true;
            this.toggleFormatClearButton.Click += new System.EventHandler(this.toggleFormatClearButton_Click);
            // 
            // unformattedStackTextBox
            // 
            this.unformattedStackTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.unformattedStackTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.unformattedStackTextBox.Location = new System.Drawing.Point(0, 0);
            this.unformattedStackTextBox.Multiline = true;
            this.unformattedStackTextBox.Name = "unformattedStackTextBox";
            this.unformattedStackTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.unformattedStackTextBox.Size = new System.Drawing.Size(353, 228);
            this.unformattedStackTextBox.TabIndex = 2;
            this.unformattedStackTextBox.WordWrap = false;
            this.unformattedStackTextBox.TextChanged += new System.EventHandler(this.unformattedStackTextBox_TextChanged);
            // 
            // stackTextBox
            // 
            this.stackTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stackTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stackTextBox.Location = new System.Drawing.Point(0, 0);
            this.stackTextBox.Name = "stackTextBox";
            this.stackTextBox.Size = new System.Drawing.Size(353, 228);
            this.stackTextBox.TabIndex = 0;
            this.stackTextBox.Visible = false;
            // 
            // FormatStack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.stackTextBox);
            this.Controls.Add(this.unformattedStackTextBox);
            this.Controls.Add(this.toggleFormatClearButton);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormatStack";
            this.Size = new System.Drawing.Size(353, 267);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Data.Stack stackTextBox;
        private System.Windows.Forms.Button toggleFormatClearButton;
        private System.Windows.Forms.TextBox unformattedStackTextBox;
    }
}
