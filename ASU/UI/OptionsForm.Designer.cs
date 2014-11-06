using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace ASU.UI
{
    partial class OptionsForm : System.Windows.Forms.Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.ColorDialog1 = new System.Windows.Forms.ColorDialog();
            this.pnlSelectedColour = new System.Windows.Forms.Panel();
            this.pnlTileBorderColour = new System.Windows.Forms.Panel();
            this.pnlHoverColour = new System.Windows.Forms.Panel();
            this.ctlOutlineWidth = new System.Windows.Forms.NumericUpDown();
            this.cmdClose = new System.Windows.Forms.Button();
            this.lblOutlineWidth = new System.Windows.Forms.Label();
            this.lblDistanceBetweenTiles = new System.Windows.Forms.Label();
            this.ctlDistanceBetweenTiles = new System.Windows.Forms.NumericUpDown();
            this.cmdAbout = new System.Windows.Forms.Button();
            this.cboExportFormat = new System.Windows.Forms.ComboBox();
            this.lblExportFormat = new System.Windows.Forms.Label();
            this.txtNConvertArgs = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.chkShowCommandLineArgs = new System.Windows.Forms.CheckBox();
            this.lblCommandLine = new System.Windows.Forms.Label();
            this.lnkCommandLineHelp = new System.Windows.Forms.LinkLabel();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdSelectedColour = new System.Windows.Forms.Button();
            this.cmdTileBorderColour = new System.Windows.Forms.Button();
            this.cmdHoverColour = new System.Windows.Forms.Button();
            this.pnlExport = new System.Windows.Forms.GroupBox();
            this.chkPreservePallette = new System.Windows.Forms.CheckBox();
            this.chkExportBGTransparent = new System.Windows.Forms.CheckBox();
            this.lnkDownload = new System.Windows.Forms.LinkLabel();
            this.cboSelectAllOrder = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.chkPromptDestinationFolder = new System.Windows.Forms.CheckBox();
            this.chkOpenExportedDestination = new System.Windows.Forms.CheckBox();
            this.lnkHelp = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.ctlOutlineWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctlDistanceBetweenTiles)).BeginInit();
            this.GroupBox1.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.pnlExport.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSelectedColour
            // 
            this.pnlSelectedColour.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(255)))));
            this.pnlSelectedColour.Location = new System.Drawing.Point(93, 17);
            this.pnlSelectedColour.Name = "pnlSelectedColour";
            this.pnlSelectedColour.Size = new System.Drawing.Size(70, 35);
            this.pnlSelectedColour.TabIndex = 5;
            // 
            // pnlTileBorderColour
            // 
            this.pnlTileBorderColour.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(255)))));
            this.pnlTileBorderColour.Location = new System.Drawing.Point(93, 58);
            this.pnlTileBorderColour.Name = "pnlTileBorderColour";
            this.pnlTileBorderColour.Size = new System.Drawing.Size(70, 35);
            this.pnlTileBorderColour.TabIndex = 7;
            // 
            // pnlHoverColour
            // 
            this.pnlHoverColour.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlHoverColour.Location = new System.Drawing.Point(93, 99);
            this.pnlHoverColour.Name = "pnlHoverColour";
            this.pnlHoverColour.Size = new System.Drawing.Size(70, 35);
            this.pnlHoverColour.TabIndex = 9;
            // 
            // ctlOutlineWidth
            // 
            this.ctlOutlineWidth.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlOutlineWidth.Location = new System.Drawing.Point(103, 20);
            this.ctlOutlineWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ctlOutlineWidth.Name = "ctlOutlineWidth";
            this.ctlOutlineWidth.Size = new System.Drawing.Size(48, 27);
            this.ctlOutlineWidth.TabIndex = 0;
            this.ctlOutlineWidth.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdClose.Location = new System.Drawing.Point(286, 328);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(76, 39);
            this.cmdClose.TabIndex = 9;
            this.cmdClose.Text = "Update and Close";
            this.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // lblOutlineWidth
            // 
            this.lblOutlineWidth.Location = new System.Drawing.Point(4, 17);
            this.lblOutlineWidth.Name = "lblOutlineWidth";
            this.lblOutlineWidth.Size = new System.Drawing.Size(93, 32);
            this.lblOutlineWidth.TabIndex = 12;
            this.lblOutlineWidth.Text = "Tile Outline Width";
            this.lblOutlineWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDistanceBetweenTiles
            // 
            this.lblDistanceBetweenTiles.Location = new System.Drawing.Point(7, 59);
            this.lblDistanceBetweenTiles.Name = "lblDistanceBetweenTiles";
            this.lblDistanceBetweenTiles.Size = new System.Drawing.Size(90, 32);
            this.lblDistanceBetweenTiles.TabIndex = 14;
            this.lblDistanceBetweenTiles.Text = "Distance Between Frames";
            this.lblDistanceBetweenTiles.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctlDistanceBetweenTiles
            // 
            this.ctlDistanceBetweenTiles.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlDistanceBetweenTiles.Location = new System.Drawing.Point(103, 62);
            this.ctlDistanceBetweenTiles.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ctlDistanceBetweenTiles.Name = "ctlDistanceBetweenTiles";
            this.ctlDistanceBetweenTiles.Size = new System.Drawing.Size(48, 27);
            this.ctlDistanceBetweenTiles.TabIndex = 1;
            this.ctlDistanceBetweenTiles.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // cmdAbout
            // 
            this.cmdAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdAbout.Font = new System.Drawing.Font("Calibri", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAbout.Location = new System.Drawing.Point(16, 328);
            this.cmdAbout.Name = "cmdAbout";
            this.cmdAbout.Size = new System.Drawing.Size(36, 16);
            this.cmdAbout.TabIndex = 6;
            this.cmdAbout.Text = "about...";
            this.cmdAbout.UseVisualStyleBackColor = true;
            this.cmdAbout.Click += new System.EventHandler(this.cmdAbout_Click);
            // 
            // cboExportFormat
            // 
            this.cboExportFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboExportFormat.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboExportFormat.FormattingEnabled = true;
            this.cboExportFormat.Location = new System.Drawing.Point(105, 20);
            this.cboExportFormat.Name = "cboExportFormat";
            this.cboExportFormat.Size = new System.Drawing.Size(229, 27);
            this.cboExportFormat.TabIndex = 0;
            this.cboExportFormat.SelectedIndexChanged += new System.EventHandler(this.cboExportFormat_SelectedIndexChanged);
            // 
            // lblExportFormat
            // 
            this.lblExportFormat.Location = new System.Drawing.Point(6, 18);
            this.lblExportFormat.Name = "lblExportFormat";
            this.lblExportFormat.Size = new System.Drawing.Size(96, 32);
            this.lblExportFormat.TabIndex = 16;
            this.lblExportFormat.Text = "File Format";
            this.lblExportFormat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNConvertArgs
            // 
            this.txtNConvertArgs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNConvertArgs.Enabled = false;
            this.txtNConvertArgs.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNConvertArgs.Location = new System.Drawing.Point(104, 92);
            this.txtNConvertArgs.Name = "txtNConvertArgs";
            this.txtNConvertArgs.Size = new System.Drawing.Size(230, 27);
            this.txtNConvertArgs.TabIndex = 3;
            this.txtNConvertArgs.Text = "pcx";
            this.txtNConvertArgs.TextChanged += new System.EventHandler(this.txtNConvertArgs_TextChanged);
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(3, 87);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(96, 32);
            this.Label1.TabIndex = 18;
            this.Label1.Text = "Advanced File Format";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkShowCommandLineArgs
            // 
            this.chkShowCommandLineArgs.BackColor = System.Drawing.Color.Transparent;
            this.chkShowCommandLineArgs.Enabled = false;
            this.chkShowCommandLineArgs.Location = new System.Drawing.Point(152, 116);
            this.chkShowCommandLineArgs.Name = "chkShowCommandLineArgs";
            this.chkShowCommandLineArgs.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkShowCommandLineArgs.Size = new System.Drawing.Size(182, 26);
            this.chkShowCommandLineArgs.TabIndex = 4;
            this.chkShowCommandLineArgs.Text = "Show Advanced Command Line";
            this.chkShowCommandLineArgs.UseVisualStyleBackColor = false;
            this.chkShowCommandLineArgs.CheckedChanged += new System.EventHandler(this.chkShowCommandLineArgs_CheckedChanged);
            // 
            // lblCommandLine
            // 
            this.lblCommandLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCommandLine.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblCommandLine.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCommandLine.Location = new System.Drawing.Point(8, 152);
            this.lblCommandLine.Name = "lblCommandLine";
            this.lblCommandLine.Size = new System.Drawing.Size(326, 59);
            this.lblCommandLine.TabIndex = 21;
            this.lblCommandLine.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCommandLine.Visible = false;
            // 
            // lnkCommandLineHelp
            // 
            this.lnkCommandLineHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkCommandLineHelp.BackColor = System.Drawing.Color.Transparent;
            this.lnkCommandLineHelp.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkCommandLineHelp.Location = new System.Drawing.Point(9, 136);
            this.lnkCommandLineHelp.Name = "lnkCommandLineHelp";
            this.lnkCommandLineHelp.Size = new System.Drawing.Size(325, 14);
            this.lnkCommandLineHelp.TabIndex = 22;
            this.lnkCommandLineHelp.TabStop = true;
            this.lnkCommandLineHelp.Text = "command line help file";
            this.lnkCommandLineHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lnkCommandLineHelp.Visible = false;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.ctlOutlineWidth);
            this.GroupBox1.Controls.Add(this.lblOutlineWidth);
            this.GroupBox1.Controls.Add(this.ctlDistanceBetweenTiles);
            this.GroupBox1.Controls.Add(this.lblDistanceBetweenTiles);
            this.GroupBox1.Location = new System.Drawing.Point(16, 74);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(157, 103);
            this.GroupBox1.TabIndex = 2;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Frame Settings";
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.cmdSelectedColour);
            this.GroupBox2.Controls.Add(this.pnlSelectedColour);
            this.GroupBox2.Controls.Add(this.cmdTileBorderColour);
            this.GroupBox2.Controls.Add(this.pnlTileBorderColour);
            this.GroupBox2.Controls.Add(this.cmdHoverColour);
            this.GroupBox2.Controls.Add(this.pnlHoverColour);
            this.GroupBox2.Location = new System.Drawing.Point(187, 5);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(175, 143);
            this.GroupBox2.TabIndex = 3;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Colours";
            // 
            // cmdSelectedColour
            // 
            this.cmdSelectedColour.Image = global::ASU.Properties.Resources.color_wheel;
            this.cmdSelectedColour.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdSelectedColour.Location = new System.Drawing.Point(9, 17);
            this.cmdSelectedColour.Name = "cmdSelectedColour";
            this.cmdSelectedColour.Size = new System.Drawing.Size(78, 35);
            this.cmdSelectedColour.TabIndex = 0;
            this.cmdSelectedColour.Text = "Selected Colour";
            this.cmdSelectedColour.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSelectedColour.UseVisualStyleBackColor = true;
            this.cmdSelectedColour.Click += new System.EventHandler(this.cmdSelectedColour_Click);
            // 
            // cmdTileBorderColour
            // 
            this.cmdTileBorderColour.Image = global::ASU.Properties.Resources.color_wheel;
            this.cmdTileBorderColour.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdTileBorderColour.Location = new System.Drawing.Point(9, 58);
            this.cmdTileBorderColour.Name = "cmdTileBorderColour";
            this.cmdTileBorderColour.Size = new System.Drawing.Size(78, 35);
            this.cmdTileBorderColour.TabIndex = 1;
            this.cmdTileBorderColour.Text = "Tile Border";
            this.cmdTileBorderColour.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdTileBorderColour.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.cmdTileBorderColour.UseVisualStyleBackColor = true;
            this.cmdTileBorderColour.Click += new System.EventHandler(this.cmdTileBorderColour_Click);
            // 
            // cmdHoverColour
            // 
            this.cmdHoverColour.Image = global::ASU.Properties.Resources.color_wheel;
            this.cmdHoverColour.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdHoverColour.Location = new System.Drawing.Point(9, 99);
            this.cmdHoverColour.Name = "cmdHoverColour";
            this.cmdHoverColour.Size = new System.Drawing.Size(78, 35);
            this.cmdHoverColour.TabIndex = 2;
            this.cmdHoverColour.Text = "Hover Colour";
            this.cmdHoverColour.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdHoverColour.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.cmdHoverColour.UseVisualStyleBackColor = true;
            this.cmdHoverColour.Click += new System.EventHandler(this.cmdHoverColour_Click);
            // 
            // pnlExport
            // 
            this.pnlExport.Controls.Add(this.chkPreservePallette);
            this.pnlExport.Controls.Add(this.chkExportBGTransparent);
            this.pnlExport.Controls.Add(this.lnkCommandLineHelp);
            this.pnlExport.Controls.Add(this.lblExportFormat);
            this.pnlExport.Controls.Add(this.cboExportFormat);
            this.pnlExport.Controls.Add(this.txtNConvertArgs);
            this.pnlExport.Controls.Add(this.Label1);
            this.pnlExport.Controls.Add(this.lblCommandLine);
            this.pnlExport.Controls.Add(this.chkShowCommandLineArgs);
            this.pnlExport.Location = new System.Drawing.Point(16, 179);
            this.pnlExport.Name = "pnlExport";
            this.pnlExport.Size = new System.Drawing.Size(346, 140);
            this.pnlExport.TabIndex = 5;
            this.pnlExport.TabStop = false;
            this.pnlExport.Text = "Export Options";
            // 
            // chkPreservePallette
            // 
            this.chkPreservePallette.AutoSize = true;
            this.chkPreservePallette.Location = new System.Drawing.Point(106, 72);
            this.chkPreservePallette.Name = "chkPreservePallette";
            this.chkPreservePallette.Size = new System.Drawing.Size(107, 17);
            this.chkPreservePallette.TabIndex = 2;
            this.chkPreservePallette.Text = "Preserve Pallette";
            this.chkPreservePallette.UseVisualStyleBackColor = true;
            this.chkPreservePallette.CheckedChanged += new System.EventHandler(this.chkPreservePallette_CheckedChanged);
            // 
            // chkExportBGTransparent
            // 
            this.chkExportBGTransparent.AutoSize = true;
            this.chkExportBGTransparent.Checked = true;
            this.chkExportBGTransparent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportBGTransparent.Location = new System.Drawing.Point(106, 53);
            this.chkExportBGTransparent.Name = "chkExportBGTransparent";
            this.chkExportBGTransparent.Size = new System.Drawing.Size(168, 17);
            this.chkExportBGTransparent.TabIndex = 1;
            this.chkExportBGTransparent.Text = "Make Background Transparent";
            this.chkExportBGTransparent.UseVisualStyleBackColor = true;
            // 
            // lnkDownload
            // 
            this.lnkDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkDownload.BackColor = System.Drawing.Color.Transparent;
            this.lnkDownload.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkDownload.Location = new System.Drawing.Point(6, 351);
            this.lnkDownload.Name = "lnkDownload";
            this.lnkDownload.Size = new System.Drawing.Size(124, 23);
            this.lnkDownload.TabIndex = 7;
            this.lnkDownload.TabStop = true;
            this.lnkDownload.Text = "Download latest version";
            this.lnkDownload.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lnkDownload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDownload_LinkClicked);
            // 
            // cboSelectAllOrder
            // 
            this.cboSelectAllOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSelectAllOrder.FormattingEnabled = true;
            this.cboSelectAllOrder.Items.AddRange(new object[] {
            "Top Left",
            "Bottom Left",
            "Centre"});
            this.cboSelectAllOrder.Location = new System.Drawing.Point(280, 156);
            this.cboSelectAllOrder.Name = "cboSelectAllOrder";
            this.cboSelectAllOrder.Size = new System.Drawing.Size(82, 21);
            this.cboSelectAllOrder.TabIndex = 4;
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(210, 151);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(64, 32);
            this.Label2.TabIndex = 15;
            this.Label2.Text = "Select All Order";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkPromptDestinationFolder
            // 
            this.chkPromptDestinationFolder.BackColor = System.Drawing.Color.Transparent;
            this.chkPromptDestinationFolder.Checked = true;
            this.chkPromptDestinationFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPromptDestinationFolder.Location = new System.Drawing.Point(6, 5);
            this.chkPromptDestinationFolder.Name = "chkPromptDestinationFolder";
            this.chkPromptDestinationFolder.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkPromptDestinationFolder.Size = new System.Drawing.Size(161, 28);
            this.chkPromptDestinationFolder.TabIndex = 0;
            this.chkPromptDestinationFolder.Text = "Prompt for Export Folder";
            this.chkPromptDestinationFolder.UseVisualStyleBackColor = false;
            // 
            // chkOpenExportedDestination
            // 
            this.chkOpenExportedDestination.BackColor = System.Drawing.Color.Transparent;
            this.chkOpenExportedDestination.Checked = true;
            this.chkOpenExportedDestination.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOpenExportedDestination.Location = new System.Drawing.Point(-19, 33);
            this.chkOpenExportedDestination.Name = "chkOpenExportedDestination";
            this.chkOpenExportedDestination.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkOpenExportedDestination.Size = new System.Drawing.Size(186, 28);
            this.chkOpenExportedDestination.TabIndex = 1;
            this.chkOpenExportedDestination.Text = "Open Folder Exported to";
            this.chkOpenExportedDestination.UseVisualStyleBackColor = false;
            // 
            // lnkHelp
            // 
            this.lnkHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkHelp.BackColor = System.Drawing.Color.Transparent;
            this.lnkHelp.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkHelp.Location = new System.Drawing.Point(136, 351);
            this.lnkHelp.Name = "lnkHelp";
            this.lnkHelp.Size = new System.Drawing.Size(31, 23);
            this.lnkHelp.TabIndex = 8;
            this.lnkHelp.TabStop = true;
            this.lnkHelp.Text = "help";
            this.lnkHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 379);
            this.Controls.Add(this.lnkHelp);
            this.Controls.Add(this.chkOpenExportedDestination);
            this.Controls.Add(this.chkPromptDestinationFolder);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.cboSelectAllOrder);
            this.Controls.Add(this.lnkDownload);
            this.Controls.Add(this.pnlExport);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.cmdAbout);
            this.Controls.Add(this.cmdClose);
            this.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.frmOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ctlOutlineWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctlDistanceBetweenTiles)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox2.ResumeLayout(false);
            this.pnlExport.ResumeLayout(false);
            this.pnlExport.PerformLayout();
            this.ResumeLayout(false);

        }
        internal System.Windows.Forms.ColorDialog ColorDialog1;
        internal System.Windows.Forms.Button cmdSelectedColour;
        internal System.Windows.Forms.Panel pnlSelectedColour;
        internal System.Windows.Forms.Panel pnlTileBorderColour;
        internal System.Windows.Forms.Button cmdTileBorderColour;
        internal System.Windows.Forms.Panel pnlHoverColour;
        internal System.Windows.Forms.Button cmdHoverColour;
        internal System.Windows.Forms.NumericUpDown ctlOutlineWidth;
        internal System.Windows.Forms.Button cmdClose;
        internal System.Windows.Forms.Label lblOutlineWidth;
        internal System.Windows.Forms.Label lblDistanceBetweenTiles;
        internal System.Windows.Forms.NumericUpDown ctlDistanceBetweenTiles;
        internal System.Windows.Forms.Button cmdAbout;
        internal System.Windows.Forms.ComboBox cboExportFormat;
        internal System.Windows.Forms.Label lblExportFormat;
        internal System.Windows.Forms.TextBox txtNConvertArgs;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.CheckBox chkShowCommandLineArgs;
        internal System.Windows.Forms.Label lblCommandLine;
        internal System.Windows.Forms.LinkLabel lnkCommandLineHelp;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.GroupBox pnlExport;
        internal System.Windows.Forms.LinkLabel lnkDownload;
        internal System.Windows.Forms.CheckBox chkExportBGTransparent;
        internal System.Windows.Forms.ComboBox cboSelectAllOrder;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.CheckBox chkPromptDestinationFolder;
        internal System.Windows.Forms.CheckBox chkOpenExportedDestination;
        internal System.Windows.Forms.LinkLabel lnkHelp;
        internal System.Windows.Forms.CheckBox chkPreservePallette;
        public OptionsForm()
        {
            InitializeComponent();
        }
    }
}