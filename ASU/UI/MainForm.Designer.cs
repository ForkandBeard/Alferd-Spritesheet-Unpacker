using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace ASU.UI
{
    partial class MainForm 
    {

        //Form overrides dispose to clean up the component list.
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        //Required by the Windows Form Designer

        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.OptionsPanel = new System.Windows.Forms.Panel();
            this.SelectAllButton = new System.Windows.Forms.Button();
            this.DeSelectAllButton = new System.Windows.Forms.Button();
            this.ExportLocationLabel = new System.Windows.Forms.Label();
            this.ClickModeCheckBoxButton = new System.Windows.Forms.CheckBox();
            this.PasteButton = new System.Windows.Forms.Button();
            this.ReloadButton = new System.Windows.Forms.Button();
            this.ExportSelectedButton = new System.Windows.Forms.Button();
            this.ExportLocationTextBox = new System.Windows.Forms.TextBox();
            this.OptionsButton = new System.Windows.Forms.Button();
            this.CombineButton = new System.Windows.Forms.Button();
            this.FolderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.CheckForUnpackFinishTimer = new System.Windows.Forms.Timer(this.components);
            this.ImageClipperAndAnimatorTimer = new ForkandBeard.Util.UI.AutoBalancingFormTimer(this.components);
            this.MainPanel = new ASU.UI.BuffablePanel(this.components);
            this.OverlayFontLabel = new System.Windows.Forms.Label();
            this.DragAndDropLabel = new System.Windows.Forms.Label();
            this.ZoomPanel = new ASU.UI.BuffablePanel(this.components);
            this.OptionsPanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // OptionsPanel
            // 
            this.OptionsPanel.Controls.Add(this.SelectAllButton);
            this.OptionsPanel.Controls.Add(this.DeSelectAllButton);
            this.OptionsPanel.Controls.Add(this.ExportLocationLabel);
            this.OptionsPanel.Controls.Add(this.ClickModeCheckBoxButton);
            this.OptionsPanel.Controls.Add(this.PasteButton);
            this.OptionsPanel.Controls.Add(this.ReloadButton);
            this.OptionsPanel.Controls.Add(this.ExportSelectedButton);
            this.OptionsPanel.Controls.Add(this.ExportLocationTextBox);
            this.OptionsPanel.Controls.Add(this.OptionsButton);
            this.OptionsPanel.Controls.Add(this.CombineButton);
            this.OptionsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.OptionsPanel.Location = new System.Drawing.Point(0, 238);
            this.OptionsPanel.Name = "OptionsPanel";
            this.OptionsPanel.Size = new System.Drawing.Size(604, 89);
            this.OptionsPanel.TabIndex = 0;
            // 
            // SelectAllButton
            // 
            this.SelectAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectAllButton.Image = global::ASU.Properties.Resources.select_all;
            this.SelectAllButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SelectAllButton.Location = new System.Drawing.Point(444, 6);
            this.SelectAllButton.Name = "SelectAllButton";
            this.SelectAllButton.Size = new System.Drawing.Size(78, 35);
            this.SelectAllButton.TabIndex = 9;
            this.SelectAllButton.Text = "Select All";
            this.SelectAllButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SelectAllButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.SelectAllButton.UseVisualStyleBackColor = true;
            this.SelectAllButton.Click += new System.EventHandler(this.SelectAllButton_Click);
            // 
            // DeSelectAllButton
            // 
            this.DeSelectAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DeSelectAllButton.Image = global::ASU.Properties.Resources.deselect_all;
            this.DeSelectAllButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.DeSelectAllButton.Location = new System.Drawing.Point(523, 6);
            this.DeSelectAllButton.Name = "DeSelectAllButton";
            this.DeSelectAllButton.Size = new System.Drawing.Size(78, 35);
            this.DeSelectAllButton.TabIndex = 3;
            this.DeSelectAllButton.Text = "De-select All";
            this.DeSelectAllButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DeSelectAllButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.DeSelectAllButton.UseVisualStyleBackColor = true;
            this.DeSelectAllButton.Click += new System.EventHandler(this.DeSelectAllButton_Click);
            // 
            // ExportLocationLabel
            // 
            this.ExportLocationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ExportLocationLabel.AutoSize = true;
            this.ExportLocationLabel.Location = new System.Drawing.Point(12, 61);
            this.ExportLocationLabel.Name = "ExportLocationLabel";
            this.ExportLocationLabel.Size = new System.Drawing.Size(81, 13);
            this.ExportLocationLabel.TabIndex = 8;
            this.ExportLocationLabel.Text = "export location:";
            // 
            // ClickModeCheckBoxButton
            // 
            this.ClickModeCheckBoxButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ClickModeCheckBoxButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.ClickModeCheckBoxButton.Image = global::ASU.Properties.Resources.cursor;
            this.ClickModeCheckBoxButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ClickModeCheckBoxButton.Location = new System.Drawing.Point(266, 6);
            this.ClickModeCheckBoxButton.Name = "ClickModeCheckBoxButton";
            this.ClickModeCheckBoxButton.Size = new System.Drawing.Size(78, 35);
            this.ClickModeCheckBoxButton.TabIndex = 4;
            this.ClickModeCheckBoxButton.Text = "Click Mode";
            this.ClickModeCheckBoxButton.UseVisualStyleBackColor = true;
            this.ClickModeCheckBoxButton.Click += new System.EventHandler(this.ClickModeCheckBoxButton_CheckedChanged);
            // 
            // PasteButton
            // 
            this.PasteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PasteButton.Image = global::ASU.Properties.Resources.paste_plain;
            this.PasteButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.PasteButton.Location = new System.Drawing.Point(182, 6);
            this.PasteButton.Name = "PasteButton";
            this.PasteButton.Size = new System.Drawing.Size(78, 35);
            this.PasteButton.TabIndex = 2;
            this.PasteButton.Text = "From Clipboard";
            this.PasteButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PasteButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.PasteButton.UseVisualStyleBackColor = true;
            this.PasteButton.Click += new System.EventHandler(this.PasteButton_Click);
            // 
            // ReloadButton
            // 
            this.ReloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ReloadButton.Image = global::ASU.Properties.Resources.arrow_refresh;
            this.ReloadButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ReloadButton.Location = new System.Drawing.Point(98, 6);
            this.ReloadButton.Name = "ReloadButton";
            this.ReloadButton.Size = new System.Drawing.Size(78, 35);
            this.ReloadButton.TabIndex = 1;
            this.ReloadButton.Text = "Reload";
            this.ReloadButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ReloadButton.UseVisualStyleBackColor = true;
            this.ReloadButton.Click += new System.EventHandler(this.UndoButton_Click);
            // 
            // ExportSelectedButton
            // 
            this.ExportSelectedButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportSelectedButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportSelectedButton.Image = global::ASU.Properties.Resources.images;
            this.ExportSelectedButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ExportSelectedButton.Location = new System.Drawing.Point(444, 47);
            this.ExportSelectedButton.Name = "ExportSelectedButton";
            this.ExportSelectedButton.Size = new System.Drawing.Size(157, 39);
            this.ExportSelectedButton.TabIndex = 7;
            this.ExportSelectedButton.Text = "Export Selected...";
            this.ExportSelectedButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ExportSelectedButton.UseVisualStyleBackColor = true;
            this.ExportSelectedButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // txtExportLocation
            // 
            this.ExportLocationTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportLocationTextBox.Location = new System.Drawing.Point(97, 58);
            this.ExportLocationTextBox.Name = "txtExportLocation";
            this.ExportLocationTextBox.Size = new System.Drawing.Size(341, 21);
            this.ExportLocationTextBox.TabIndex = 6;
            // 
            // OptionsButton
            // 
            this.OptionsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OptionsButton.Image = global::ASU.Properties.Resources.wrench;
            this.OptionsButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.OptionsButton.Location = new System.Drawing.Point(10, 6);
            this.OptionsButton.Name = "OptionsButton";
            this.OptionsButton.Size = new System.Drawing.Size(78, 35);
            this.OptionsButton.TabIndex = 0;
            this.OptionsButton.Text = "Options...";
            this.OptionsButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OptionsButton.UseVisualStyleBackColor = true;
            this.OptionsButton.Click += new System.EventHandler(this.OptionsButton_Click);
            // 
            // CombineButton
            // 
            this.CombineButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CombineButton.Image = global::ASU.Properties.Resources.compress;
            this.CombineButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CombineButton.Location = new System.Drawing.Point(349, 6);
            this.CombineButton.Name = "CombineButton";
            this.CombineButton.Size = new System.Drawing.Size(70, 35);
            this.CombineButton.TabIndex = 5;
            this.CombineButton.Text = "Combine Selected";
            this.CombineButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CombineButton.UseVisualStyleBackColor = true;
            this.CombineButton.Click += new System.EventHandler(this.CombineButton_Click);
            // 
            // CheckForUnpackFinishTimer
            // 
            this.CheckForUnpackFinishTimer.Interval = 200;
            this.CheckForUnpackFinishTimer.Tick += new System.EventHandler(this.CheckForUnpackFinishTimer_Tick);
            // 
            // ImageClipperAndAnimatorTimer
            // 
            this.ImageClipperAndAnimatorTimer.Interval = 75;
            this.ImageClipperAndAnimatorTimer.MaxInterval = 1000;
            this.ImageClipperAndAnimatorTimer.MinInterval = 40;
            this.ImageClipperAndAnimatorTimer.BalancedTock += new ForkandBeard.Util.UI.AutoBalancingFormTimer.BalancedTockEventHandler(this.ImageClipperAndAnimatorTimer_BalancedTock);
            // 
            // MainPanel
            // 
            this.MainPanel.AllowDrop = true;
            this.MainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MainPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.MainPanel.Controls.Add(this.OverlayFontLabel);
            this.MainPanel.Controls.Add(this.DragAndDropLabel);
            this.MainPanel.Controls.Add(this.ZoomPanel);
            this.MainPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(604, 238);
            this.MainPanel.TabIndex = 4;
            this.MainPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainPanel_DragDrop);
            this.MainPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainPanel_DragEnter);
            this.MainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.MainPanel_Paint);
            this.MainPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainPanel_MouseDown);
            this.MainPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainPanel_MouseMove);
            this.MainPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainPanel_MouseUp);
            // 
            // OverlayFont
            // 
            this.OverlayFontLabel.AutoSize = true;
            this.OverlayFontLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OverlayFontLabel.ForeColor = System.Drawing.Color.Blue;
            this.OverlayFontLabel.Location = new System.Drawing.Point(502, 12);
            this.OverlayFontLabel.Name = "OverlayFont";
            this.OverlayFontLabel.Size = new System.Drawing.Size(79, 13);
            this.OverlayFontLabel.TabIndex = 2;
            this.OverlayFontLabel.Text = "overlay font";
            this.OverlayFontLabel.Visible = false;
            // 
            // DragAndDropLabel
            // 
            this.DragAndDropLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.DragAndDropLabel.AutoSize = true;
            this.DragAndDropLabel.Location = new System.Drawing.Point(216, 103);
            this.DragAndDropLabel.Name = "DragAndDropLabel";
            this.DragAndDropLabel.Size = new System.Drawing.Size(385, 40);
            this.DragAndDropLabel.TabIndex = 1;
            this.DragAndDropLabel.Text = "Drag and drop image(s) here";
            // 
            // ZoomPanel
            // 
            this.ZoomPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ZoomPanel.ForeColor = System.Drawing.Color.Transparent;
            this.ZoomPanel.Location = new System.Drawing.Point(8, 6);
            this.ZoomPanel.Name = "ZoomPanel";
            this.ZoomPanel.Size = new System.Drawing.Size(80, 80);
            this.ZoomPanel.TabIndex = 0;
            this.ZoomPanel.Visible = false;
            this.ZoomPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.ZoomPanel_Paint);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 327);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.OptionsPanel);
            this.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(620, 365);
            this.Name = "MainForm";
            this.Text = "Alferd Spritesheet Unpacker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.OptionsPanel.ResumeLayout(false);
            this.OptionsPanel.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.ResumeLayout(false);

        }
        internal System.Windows.Forms.Panel OptionsPanel;
        internal System.Windows.Forms.Button ExportSelectedButton;
        internal System.Windows.Forms.Button CombineButton;
        internal ASU.UI.BuffablePanel MainPanel;
        internal System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog1;
        internal ASU.UI.BuffablePanel ZoomPanel;
        internal System.Windows.Forms.TextBox ExportLocationTextBox;
        internal System.Windows.Forms.Button OptionsButton;
        internal System.Windows.Forms.Button ReloadButton;
        internal System.Windows.Forms.Button DeSelectAllButton;
        internal System.Windows.Forms.Button PasteButton;
        internal System.Windows.Forms.Timer CheckForUnpackFinishTimer;
        internal System.Windows.Forms.Label DragAndDropLabel;
        internal ForkandBeard.Util.UI.AutoBalancingFormTimer ImageClipperAndAnimatorTimer;
        internal System.Windows.Forms.CheckBox ClickModeCheckBoxButton;
        internal System.Windows.Forms.Label ExportLocationLabel;
        internal System.Windows.Forms.Button SelectAllButton;

        internal System.Windows.Forms.Label OverlayFontLabel;
    }
}