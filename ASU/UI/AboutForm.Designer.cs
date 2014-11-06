namespace ASU.UI
{
    partial class AboutForm : System.Windows.Forms.Form
    {

        //Form overrides dispose to clean up the component list.
        [System.Diagnostics.DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components != null)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        //Required by the Windows Form Designer

        private System.ComponentModel.IContainer components;
        //NOTE: The following procedure is required by the Windows Form Designer
        //It can be modified using the Windows Form Designer.  
        //Do not modify it using the code editor.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
	{
            this.components = new System.ComponentModel.Container();
            this.LinkLabel1 = new System.Windows.Forms.LinkLabel();
            this.Label1 = new System.Windows.Forms.Label();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.BuffablePanel1 = new ASU.UI.BuffablePanel(this.components);
            this.SuspendLayout();
            // 
            // LinkLabel1
            // 
            this.LinkLabel1.AutoSize = true;
            this.LinkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.LinkLabel1.ForeColor = System.Drawing.Color.Silver;
            this.LinkLabel1.LinkColor = System.Drawing.Color.Black;
            this.LinkLabel1.Location = new System.Drawing.Point(373, 125);
            this.LinkLabel1.Name = "LinkLabel1";
            this.LinkLabel1.Size = new System.Drawing.Size(165, 18);
            this.LinkLabel1.TabIndex = 0;
            this.LinkLabel1.TabStop = true;
            this.LinkLabel1.Text = "www.forkandbeard.co.uk";
            this.LinkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.ForeColor = System.Drawing.Color.Black;
            this.Label1.Location = new System.Drawing.Point(2, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(189, 36);
            this.Label1.TabIndex = 1;
            this.Label1.Text = "written by \r\nMitchell William Cooper 2011\r\n";
            this.Label1.Click += new System.EventHandler(this.Label1_Click);
            // 
            // Timer1
            // 
            this.Timer1.Enabled = true;
            this.Timer1.Interval = 50;
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.ForeColor = System.Drawing.Color.DimGray;
            this.Label2.Location = new System.Drawing.Point(2, 45);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(125, 36);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "cat@forkandbeard.co.uk";
            this.Label2.Click += new System.EventHandler(this.Label2_Click);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = this.BackColor;
            this.Label3.ForeColor = System.Drawing.Color.White;
            this.Label3.Location = new System.Drawing.Point(-1, 159);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(0, 18);
            this.Label3.TabIndex = 4;
            // 
            // BuffablePanel1
            // 
            this.BuffablePanel1.BackgroundImage = global::ASU.Properties.Resources.AlferdPacker;
            this.BuffablePanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BuffablePanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.BuffablePanel1.Location = new System.Drawing.Point(431, 9);
            this.BuffablePanel1.Name = "BuffablePanel1";
            this.BuffablePanel1.Size = new System.Drawing.Size(108, 113);
            this.BuffablePanel1.TabIndex = 5;
            this.BuffablePanel1.Click += new System.EventHandler(this.BuffablePanel1_Click);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(551, 271);
            this.Controls.Add(this.BuffablePanel1);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.LinkLabel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "What\'s this all aboot";
            this.ResumeLayout(false);
            this.PerformLayout();

	}
        internal System.Windows.Forms.LinkLabel LinkLabel1;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Timer Timer1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label3;
        internal ASU.UI.BuffablePanel BuffablePanel1;
    }
}