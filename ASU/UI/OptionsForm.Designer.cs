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
            this.SelectedColourPanel = new System.Windows.Forms.Panel();
            this.TileBorderColourPanel = new System.Windows.Forms.Panel();
            this.HoverColourPanel = new System.Windows.Forms.Panel();
            this.OutlineWidthUpDown = new System.Windows.Forms.NumericUpDown();
            this.CloseButton = new System.Windows.Forms.Button();
            this.OutlineWidthLabel = new System.Windows.Forms.Label();
            this.DistanceBetweenTilesLabel = new System.Windows.Forms.Label();
            this.DistanceBetweenTilesUpDown = new System.Windows.Forms.NumericUpDown();
            this.AboutButton = new System.Windows.Forms.Button();
            this.ExportFormatComboBox = new System.Windows.Forms.ComboBox();
            this.ExportFormatLabel = new System.Windows.Forms.Label();
            this.NConvertArgsTextBox = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.ShowCommandLineArgsCheckBox = new System.Windows.Forms.CheckBox();
            this.CommandLineLabel = new System.Windows.Forms.Label();
            this.CommandLineHelpLink = new System.Windows.Forms.LinkLabel();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.SelectedColourButton = new System.Windows.Forms.Button();
            this.TileBorderColourButton = new System.Windows.Forms.Button();
            this.HoverColourButton = new System.Windows.Forms.Button();
            this.ExportPanel = new System.Windows.Forms.GroupBox();
            this.PreservePalletteCheckBox = new System.Windows.Forms.CheckBox();
            this.ExportBGTransparentCheckBox = new System.Windows.Forms.CheckBox();
            this.DownloadLink = new System.Windows.Forms.LinkLabel();
            this.SelectAllOrderComboBox = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.PromptDestinationFolderCheckBox = new System.Windows.Forms.CheckBox();
            this.OpenExportedDestinationCheckBox = new System.Windows.Forms.CheckBox();
            this.HelpLink = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.OutlineWidthUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DistanceBetweenTilesUpDown)).BeginInit();
            this.GroupBox1.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.ExportPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // SelectedColourPanel
            // 
            this.SelectedColourPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(255)))));
            this.SelectedColourPanel.Location = new System.Drawing.Point(93, 17);
            this.SelectedColourPanel.Name = "SelectedColourPanel";
            this.SelectedColourPanel.Size = new System.Drawing.Size(70, 35);
            this.SelectedColourPanel.TabIndex = 5;
            // 
            // TileBorderColourPanel
            // 
            this.TileBorderColourPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(255)))));
            this.TileBorderColourPanel.Location = new System.Drawing.Point(93, 58);
            this.TileBorderColourPanel.Name = "TileBorderColourPanel";
            this.TileBorderColourPanel.Size = new System.Drawing.Size(70, 35);
            this.TileBorderColourPanel.TabIndex = 7;
            // 
            // HoverColourPanel
            // 
            this.HoverColourPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.HoverColourPanel.Location = new System.Drawing.Point(93, 99);
            this.HoverColourPanel.Name = "HoverColourPanel";
            this.HoverColourPanel.Size = new System.Drawing.Size(70, 35);
            this.HoverColourPanel.TabIndex = 9;
            // 
            // OutlineWidthUpDown
            // 
            this.OutlineWidthUpDown.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutlineWidthUpDown.Location = new System.Drawing.Point(103, 20);
            this.OutlineWidthUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.OutlineWidthUpDown.Name = "OutlineWidthUpDown";
            this.OutlineWidthUpDown.Size = new System.Drawing.Size(48, 27);
            this.OutlineWidthUpDown.TabIndex = 0;
            this.OutlineWidthUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CloseButton.Location = new System.Drawing.Point(286, 328);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(76, 39);
            this.CloseButton.TabIndex = 9;
            this.CloseButton.Text = "Update and Close";
            this.CloseButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CloseButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // OutlineWidthLabel
            // 
            this.OutlineWidthLabel.Location = new System.Drawing.Point(4, 17);
            this.OutlineWidthLabel.Name = "OutlineWidthLabel";
            this.OutlineWidthLabel.Size = new System.Drawing.Size(93, 32);
            this.OutlineWidthLabel.TabIndex = 12;
            this.OutlineWidthLabel.Text = "Tile Outline Width";
            this.OutlineWidthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DistanceBetweenTilesLabel
            // 
            this.DistanceBetweenTilesLabel.Location = new System.Drawing.Point(7, 59);
            this.DistanceBetweenTilesLabel.Name = "DistanceBetweenTilesLabel";
            this.DistanceBetweenTilesLabel.Size = new System.Drawing.Size(90, 32);
            this.DistanceBetweenTilesLabel.TabIndex = 14;
            this.DistanceBetweenTilesLabel.Text = "Distance Between Frames";
            this.DistanceBetweenTilesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DistanceBetweenTilesUpDown
            // 
            this.DistanceBetweenTilesUpDown.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DistanceBetweenTilesUpDown.Location = new System.Drawing.Point(103, 62);
            this.DistanceBetweenTilesUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DistanceBetweenTilesUpDown.Name = "DistanceBetweenTilesUpDown";
            this.DistanceBetweenTilesUpDown.Size = new System.Drawing.Size(48, 27);
            this.DistanceBetweenTilesUpDown.TabIndex = 1;
            this.DistanceBetweenTilesUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // AboutButton
            // 
            this.AboutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AboutButton.Font = new System.Drawing.Font("Calibri", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AboutButton.Location = new System.Drawing.Point(16, 328);
            this.AboutButton.Name = "AboutButton";
            this.AboutButton.Size = new System.Drawing.Size(36, 16);
            this.AboutButton.TabIndex = 6;
            this.AboutButton.Text = "about...";
            this.AboutButton.UseVisualStyleBackColor = true;
            this.AboutButton.Click += new System.EventHandler(this.AboutButton_Click);
            // 
            // ExportFormatComboBox
            // 
            this.ExportFormatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ExportFormatComboBox.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportFormatComboBox.FormattingEnabled = true;
            this.ExportFormatComboBox.Location = new System.Drawing.Point(105, 20);
            this.ExportFormatComboBox.Name = "ExportFormatComboBox";
            this.ExportFormatComboBox.Size = new System.Drawing.Size(229, 27);
            this.ExportFormatComboBox.TabIndex = 0;
            this.ExportFormatComboBox.SelectedIndexChanged += new System.EventHandler(this.ExportFormatComboBox_SelectedIndexChanged);
            // 
            // ExportFormatLabel
            // 
            this.ExportFormatLabel.Location = new System.Drawing.Point(6, 18);
            this.ExportFormatLabel.Name = "ExportFormatLabel";
            this.ExportFormatLabel.Size = new System.Drawing.Size(96, 32);
            this.ExportFormatLabel.TabIndex = 16;
            this.ExportFormatLabel.Text = "File Format";
            this.ExportFormatLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // NConvertArgsTextBox
            // 
            this.NConvertArgsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NConvertArgsTextBox.Enabled = false;
            this.NConvertArgsTextBox.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NConvertArgsTextBox.Location = new System.Drawing.Point(104, 92);
            this.NConvertArgsTextBox.Name = "NConvertArgsTextBox";
            this.NConvertArgsTextBox.Size = new System.Drawing.Size(230, 27);
            this.NConvertArgsTextBox.TabIndex = 3;
            this.NConvertArgsTextBox.Text = "pcx";
            this.NConvertArgsTextBox.TextChanged += new System.EventHandler(this.NConvertArgsTextBox_TextChanged);
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
            // ShowCommandLineArgsCheckBox
            // 
            this.ShowCommandLineArgsCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.ShowCommandLineArgsCheckBox.Enabled = false;
            this.ShowCommandLineArgsCheckBox.Location = new System.Drawing.Point(152, 116);
            this.ShowCommandLineArgsCheckBox.Name = "ShowCommandLineArgsCheckBox";
            this.ShowCommandLineArgsCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowCommandLineArgsCheckBox.Size = new System.Drawing.Size(182, 26);
            this.ShowCommandLineArgsCheckBox.TabIndex = 4;
            this.ShowCommandLineArgsCheckBox.Text = "Show Advanced Command Line";
            this.ShowCommandLineArgsCheckBox.UseVisualStyleBackColor = false;
            this.ShowCommandLineArgsCheckBox.CheckedChanged += new System.EventHandler(this.ShowCommandLineArgsCheckBox_CheckedChanged);
            // 
            // CommandLineLabel
            // 
            this.CommandLineLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CommandLineLabel.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.CommandLineLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CommandLineLabel.Location = new System.Drawing.Point(8, 152);
            this.CommandLineLabel.Name = "CommandLineLabel";
            this.CommandLineLabel.Size = new System.Drawing.Size(326, 59);
            this.CommandLineLabel.TabIndex = 21;
            this.CommandLineLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CommandLineLabel.Visible = false;
            // 
            // CommandLineHelpLink
            // 
            this.CommandLineHelpLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CommandLineHelpLink.BackColor = System.Drawing.Color.Transparent;
            this.CommandLineHelpLink.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CommandLineHelpLink.Location = new System.Drawing.Point(9, 136);
            this.CommandLineHelpLink.Name = "CommandLineHelpLink";
            this.CommandLineHelpLink.Size = new System.Drawing.Size(325, 14);
            this.CommandLineHelpLink.TabIndex = 22;
            this.CommandLineHelpLink.TabStop = true;
            this.CommandLineHelpLink.Text = "command line help file";
            this.CommandLineHelpLink.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CommandLineHelpLink.Visible = false;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.OutlineWidthUpDown);
            this.GroupBox1.Controls.Add(this.OutlineWidthLabel);
            this.GroupBox1.Controls.Add(this.DistanceBetweenTilesUpDown);
            this.GroupBox1.Controls.Add(this.DistanceBetweenTilesLabel);
            this.GroupBox1.Location = new System.Drawing.Point(16, 74);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(157, 103);
            this.GroupBox1.TabIndex = 2;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Frame Settings";
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.SelectedColourButton);
            this.GroupBox2.Controls.Add(this.SelectedColourPanel);
            this.GroupBox2.Controls.Add(this.TileBorderColourButton);
            this.GroupBox2.Controls.Add(this.TileBorderColourPanel);
            this.GroupBox2.Controls.Add(this.HoverColourButton);
            this.GroupBox2.Controls.Add(this.HoverColourPanel);
            this.GroupBox2.Location = new System.Drawing.Point(187, 5);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(175, 143);
            this.GroupBox2.TabIndex = 3;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Colours";
            // 
            // SelectedColourButton
            // 
            this.SelectedColourButton.Image = global::ASU.Properties.Resources.color_wheel;
            this.SelectedColourButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SelectedColourButton.Location = new System.Drawing.Point(9, 17);
            this.SelectedColourButton.Name = "SelectedColourButton";
            this.SelectedColourButton.Size = new System.Drawing.Size(78, 35);
            this.SelectedColourButton.TabIndex = 0;
            this.SelectedColourButton.Text = "Selected Colour";
            this.SelectedColourButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SelectedColourButton.UseVisualStyleBackColor = true;
            this.SelectedColourButton.Click += new System.EventHandler(this.SelectedColourButton_Click);
            // 
            // TileBorderColourButton
            // 
            this.TileBorderColourButton.Image = global::ASU.Properties.Resources.color_wheel;
            this.TileBorderColourButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TileBorderColourButton.Location = new System.Drawing.Point(9, 58);
            this.TileBorderColourButton.Name = "TileBorderColourButton";
            this.TileBorderColourButton.Size = new System.Drawing.Size(78, 35);
            this.TileBorderColourButton.TabIndex = 1;
            this.TileBorderColourButton.Text = "Tile Border";
            this.TileBorderColourButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TileBorderColourButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.TileBorderColourButton.UseVisualStyleBackColor = true;
            this.TileBorderColourButton.Click += new System.EventHandler(this.TileBorderColourButton_Click);
            // 
            // HoverColourButton
            // 
            this.HoverColourButton.Image = global::ASU.Properties.Resources.color_wheel;
            this.HoverColourButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.HoverColourButton.Location = new System.Drawing.Point(9, 99);
            this.HoverColourButton.Name = "HoverColourButton";
            this.HoverColourButton.Size = new System.Drawing.Size(78, 35);
            this.HoverColourButton.TabIndex = 2;
            this.HoverColourButton.Text = "Hover Colour";
            this.HoverColourButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.HoverColourButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.HoverColourButton.UseVisualStyleBackColor = true;
            this.HoverColourButton.Click += new System.EventHandler(this.HoverColourButton_Click);
            // 
            // ExportPanel
            // 
            this.ExportPanel.Controls.Add(this.PreservePalletteCheckBox);
            this.ExportPanel.Controls.Add(this.ExportBGTransparentCheckBox);
            this.ExportPanel.Controls.Add(this.CommandLineHelpLink);
            this.ExportPanel.Controls.Add(this.ExportFormatLabel);
            this.ExportPanel.Controls.Add(this.ExportFormatComboBox);
            this.ExportPanel.Controls.Add(this.NConvertArgsTextBox);
            this.ExportPanel.Controls.Add(this.Label1);
            this.ExportPanel.Controls.Add(this.CommandLineLabel);
            this.ExportPanel.Controls.Add(this.ShowCommandLineArgsCheckBox);
            this.ExportPanel.Location = new System.Drawing.Point(16, 179);
            this.ExportPanel.Name = "ExportPanel";
            this.ExportPanel.Size = new System.Drawing.Size(346, 140);
            this.ExportPanel.TabIndex = 5;
            this.ExportPanel.TabStop = false;
            this.ExportPanel.Text = "Export Options";
            // 
            // PreservePalletteCheckBox
            // 
            this.PreservePalletteCheckBox.AutoSize = true;
            this.PreservePalletteCheckBox.Location = new System.Drawing.Point(106, 72);
            this.PreservePalletteCheckBox.Name = "PreservePalletteCheckBox";
            this.PreservePalletteCheckBox.Size = new System.Drawing.Size(107, 17);
            this.PreservePalletteCheckBox.TabIndex = 2;
            this.PreservePalletteCheckBox.Text = "Preserve Pallette";
            this.PreservePalletteCheckBox.UseVisualStyleBackColor = true;
            this.PreservePalletteCheckBox.CheckedChanged += new System.EventHandler(this.PreservePalletteCheckBox_CheckedChanged);
            // 
            // ExportBGTransparentCheckBox
            // 
            this.ExportBGTransparentCheckBox.AutoSize = true;
            this.ExportBGTransparentCheckBox.Checked = true;
            this.ExportBGTransparentCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ExportBGTransparentCheckBox.Location = new System.Drawing.Point(106, 53);
            this.ExportBGTransparentCheckBox.Name = "ExportBGTransparentCheckBox";
            this.ExportBGTransparentCheckBox.Size = new System.Drawing.Size(168, 17);
            this.ExportBGTransparentCheckBox.TabIndex = 1;
            this.ExportBGTransparentCheckBox.Text = "Make Background Transparent";
            this.ExportBGTransparentCheckBox.UseVisualStyleBackColor = true;
            // 
            // DownloadLink
            // 
            this.DownloadLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DownloadLink.BackColor = System.Drawing.Color.Transparent;
            this.DownloadLink.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DownloadLink.Location = new System.Drawing.Point(6, 351);
            this.DownloadLink.Name = "DownloadLink";
            this.DownloadLink.Size = new System.Drawing.Size(124, 23);
            this.DownloadLink.TabIndex = 7;
            this.DownloadLink.TabStop = true;
            this.DownloadLink.Text = "Download latest version";
            this.DownloadLink.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DownloadLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DownloadLink_LinkClicked);
            // 
            // SelectAllOrderComboBox
            // 
            this.SelectAllOrderComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectAllOrderComboBox.FormattingEnabled = true;
            this.SelectAllOrderComboBox.Items.AddRange(new object[] {
            "Top Left",
            "Bottom Left",
            "Centre"});
            this.SelectAllOrderComboBox.Location = new System.Drawing.Point(280, 156);
            this.SelectAllOrderComboBox.Name = "SelectAllOrderComboBox";
            this.SelectAllOrderComboBox.Size = new System.Drawing.Size(82, 21);
            this.SelectAllOrderComboBox.TabIndex = 4;
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
            // PromptDestinationFolderCheckBox
            // 
            this.PromptDestinationFolderCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.PromptDestinationFolderCheckBox.Checked = true;
            this.PromptDestinationFolderCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PromptDestinationFolderCheckBox.Location = new System.Drawing.Point(6, 5);
            this.PromptDestinationFolderCheckBox.Name = "PromptDestinationFolderCheckBox";
            this.PromptDestinationFolderCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.PromptDestinationFolderCheckBox.Size = new System.Drawing.Size(161, 28);
            this.PromptDestinationFolderCheckBox.TabIndex = 0;
            this.PromptDestinationFolderCheckBox.Text = "Prompt for Export Folder";
            this.PromptDestinationFolderCheckBox.UseVisualStyleBackColor = false;
            // 
            // OpenExportedDestinationCheckBox
            // 
            this.OpenExportedDestinationCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.OpenExportedDestinationCheckBox.Checked = true;
            this.OpenExportedDestinationCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.OpenExportedDestinationCheckBox.Location = new System.Drawing.Point(-19, 33);
            this.OpenExportedDestinationCheckBox.Name = "OpenExportedDestinationCheckBox";
            this.OpenExportedDestinationCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.OpenExportedDestinationCheckBox.Size = new System.Drawing.Size(186, 28);
            this.OpenExportedDestinationCheckBox.TabIndex = 1;
            this.OpenExportedDestinationCheckBox.Text = "Open Folder Exported to";
            this.OpenExportedDestinationCheckBox.UseVisualStyleBackColor = false;
            // 
            // HelpLink
            // 
            this.HelpLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.HelpLink.BackColor = System.Drawing.Color.Transparent;
            this.HelpLink.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpLink.Location = new System.Drawing.Point(136, 351);
            this.HelpLink.Name = "HelpLink";
            this.HelpLink.Size = new System.Drawing.Size(31, 23);
            this.HelpLink.TabIndex = 8;
            this.HelpLink.TabStop = true;
            this.HelpLink.Text = "help";
            this.HelpLink.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.HelpLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HelpLink_LinkClicked);
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 379);
            this.Controls.Add(this.HelpLink);
            this.Controls.Add(this.OpenExportedDestinationCheckBox);
            this.Controls.Add(this.PromptDestinationFolderCheckBox);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.SelectAllOrderComboBox);
            this.Controls.Add(this.DownloadLink);
            this.Controls.Add(this.ExportPanel);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.AboutButton);
            this.Controls.Add(this.CloseButton);
            this.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.OptionsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.OutlineWidthUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DistanceBetweenTilesUpDown)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox2.ResumeLayout(false);
            this.ExportPanel.ResumeLayout(false);
            this.ExportPanel.PerformLayout();
            this.ResumeLayout(false);

        }
        internal System.Windows.Forms.ColorDialog ColorDialog1;
        internal System.Windows.Forms.Button SelectedColourButton;
        internal System.Windows.Forms.Panel SelectedColourPanel;
        internal System.Windows.Forms.Panel TileBorderColourPanel;
        internal System.Windows.Forms.Button TileBorderColourButton;
        internal System.Windows.Forms.Panel HoverColourPanel;
        internal System.Windows.Forms.Button HoverColourButton;
        internal System.Windows.Forms.NumericUpDown OutlineWidthUpDown;
        internal System.Windows.Forms.Button CloseButton;
        internal System.Windows.Forms.Label OutlineWidthLabel;
        internal System.Windows.Forms.Label DistanceBetweenTilesLabel;
        internal System.Windows.Forms.NumericUpDown DistanceBetweenTilesUpDown;
        internal System.Windows.Forms.Button AboutButton;
        internal System.Windows.Forms.ComboBox ExportFormatComboBox;
        internal System.Windows.Forms.Label ExportFormatLabel;
        internal System.Windows.Forms.TextBox NConvertArgsTextBox;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.CheckBox ShowCommandLineArgsCheckBox;
        internal System.Windows.Forms.Label CommandLineLabel;
        internal System.Windows.Forms.LinkLabel CommandLineHelpLink;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.GroupBox ExportPanel;
        internal System.Windows.Forms.LinkLabel DownloadLink;
        internal System.Windows.Forms.CheckBox ExportBGTransparentCheckBox;
        internal System.Windows.Forms.ComboBox SelectAllOrderComboBox;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.CheckBox PromptDestinationFolderCheckBox;
        internal System.Windows.Forms.CheckBox OpenExportedDestinationCheckBox;
        internal System.Windows.Forms.LinkLabel HelpLink;
        internal System.Windows.Forms.CheckBox PreservePalletteCheckBox;
        public OptionsForm()
        {
            InitializeComponent();
        }
    }
}