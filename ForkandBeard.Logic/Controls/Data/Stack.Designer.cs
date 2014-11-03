namespace ForkandBeard.Logic.Controls.Data
{
    partial class Stack
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
            this.stackRichTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // stackRichTextBox
            // 
            this.stackRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stackRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.stackRichTextBox.Name = "stackRichTextBox";
            this.stackRichTextBox.Size = new System.Drawing.Size(243, 215);
            this.stackRichTextBox.TabIndex = 0;
            this.stackRichTextBox.Text = "";
            this.stackRichTextBox.WordWrap = false;
            this.stackRichTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.stackRichTextBox_KeyDown);
            // 
            // Stack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.stackRichTextBox);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Stack";
            this.Size = new System.Drawing.Size(243, 215);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox stackRichTextBox;

    }
}
