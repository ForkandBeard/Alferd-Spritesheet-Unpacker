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
            this.pnlOptions = new System.Windows.Forms.Panel();
            this.SelectAllButton = new System.Windows.Forms.Button();
            this.DeSelectAllButton = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.chkCut = new System.Windows.Forms.CheckBox();
            this.cmdPaste = new System.Windows.Forms.Button();
            this.cmdUndo = new System.Windows.Forms.Button();
            this.cmdExport = new System.Windows.Forms.Button();
            this.txtExportLocation = new System.Windows.Forms.TextBox();
            this.cmdOptions = new System.Windows.Forms.Button();
            this.cmdCombine = new System.Windows.Forms.Button();
            this.FolderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.CheckForUnpackFinishTimer = new System.Windows.Forms.Timer(this.components);
            this.ImageClipperAndAnimatorTimer = new ForkandBeard.Util.UI.AutoBalancingFormTimer(this.components);
            this.pnlMain = new ASU.UI.BuffablePanel(this.components);
            this.OverlayFont = new System.Windows.Forms.Label();
            this.lblDragAndDrop = new System.Windows.Forms.Label();
            this.pnlZoom = new ASU.UI.BuffablePanel(this.components);
            this.pnlOptions.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOptions
            // 
            this.pnlOptions.Controls.Add(this.SelectAllButton);
            this.pnlOptions.Controls.Add(this.DeSelectAllButton);
            this.pnlOptions.Controls.Add(this.Label1);
            this.pnlOptions.Controls.Add(this.chkCut);
            this.pnlOptions.Controls.Add(this.cmdPaste);
            this.pnlOptions.Controls.Add(this.cmdUndo);
            this.pnlOptions.Controls.Add(this.cmdExport);
            this.pnlOptions.Controls.Add(this.txtExportLocation);
            this.pnlOptions.Controls.Add(this.cmdOptions);
            this.pnlOptions.Controls.Add(this.cmdCombine);
            this.pnlOptions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlOptions.Location = new System.Drawing.Point(0, 238);
            this.pnlOptions.Name = "pnlOptions";
            this.pnlOptions.Size = new System.Drawing.Size(604, 89);
            this.pnlOptions.TabIndex = 0;
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
            // Label1
            // 
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 61);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(81, 13);
            this.Label1.TabIndex = 8;
            this.Label1.Text = "export location:";
            // 
            // chkCut
            // 
            this.chkCut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkCut.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkCut.Image = global::ASU.Properties.Resources.cursor;
            this.chkCut.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCut.Location = new System.Drawing.Point(266, 6);
            this.chkCut.Name = "chkCut";
            this.chkCut.Size = new System.Drawing.Size(78, 35);
            this.chkCut.TabIndex = 4;
            this.chkCut.Text = "Click Mode";
            this.chkCut.UseVisualStyleBackColor = true;
            this.chkCut.Click += new System.EventHandler(this.chkCut_CheckedChanged);
            // 
            // cmdPaste
            // 
            this.cmdPaste.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdPaste.Image = global::ASU.Properties.Resources.paste_plain;
            this.cmdPaste.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdPaste.Location = new System.Drawing.Point(182, 6);
            this.cmdPaste.Name = "cmdPaste";
            this.cmdPaste.Size = new System.Drawing.Size(78, 35);
            this.cmdPaste.TabIndex = 2;
            this.cmdPaste.Text = "From Clipboard";
            this.cmdPaste.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdPaste.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.cmdPaste.UseVisualStyleBackColor = true;
            this.cmdPaste.Click += new System.EventHandler(this.cmdPaste_Click);
            // 
            // cmdUndo
            // 
            this.cmdUndo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdUndo.Image = global::ASU.Properties.Resources.arrow_refresh;
            this.cmdUndo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdUndo.Location = new System.Drawing.Point(98, 6);
            this.cmdUndo.Name = "cmdUndo";
            this.cmdUndo.Size = new System.Drawing.Size(78, 35);
            this.cmdUndo.TabIndex = 1;
            this.cmdUndo.Text = "Reload";
            this.cmdUndo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdUndo.UseVisualStyleBackColor = true;
            this.cmdUndo.Click += new System.EventHandler(this.cmdUndo_Click);
            // 
            // cmdExport
            // 
            this.cmdExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExport.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExport.Image = global::ASU.Properties.Resources.images;
            this.cmdExport.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdExport.Location = new System.Drawing.Point(444, 47);
            this.cmdExport.Name = "cmdExport";
            this.cmdExport.Size = new System.Drawing.Size(157, 39);
            this.cmdExport.TabIndex = 7;
            this.cmdExport.Text = "Export Selected...";
            this.cmdExport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdExport.UseVisualStyleBackColor = true;
            this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
            // 
            // txtExportLocation
            // 
            this.txtExportLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExportLocation.Location = new System.Drawing.Point(97, 58);
            this.txtExportLocation.Name = "txtExportLocation";
            this.txtExportLocation.Size = new System.Drawing.Size(341, 21);
            this.txtExportLocation.TabIndex = 6;
            // 
            // cmdOptions
            // 
            this.cmdOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdOptions.Image = global::ASU.Properties.Resources.wrench;
            this.cmdOptions.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdOptions.Location = new System.Drawing.Point(10, 6);
            this.cmdOptions.Name = "cmdOptions";
            this.cmdOptions.Size = new System.Drawing.Size(78, 35);
            this.cmdOptions.TabIndex = 0;
            this.cmdOptions.Text = "Options...";
            this.cmdOptions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdOptions.UseVisualStyleBackColor = true;
            this.cmdOptions.Click += new System.EventHandler(this.cmdOptions_Click);
            // 
            // cmdCombine
            // 
            this.cmdCombine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdCombine.Image = global::ASU.Properties.Resources.compress;
            this.cmdCombine.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCombine.Location = new System.Drawing.Point(349, 6);
            this.cmdCombine.Name = "cmdCombine";
            this.cmdCombine.Size = new System.Drawing.Size(70, 35);
            this.cmdCombine.TabIndex = 5;
            this.cmdCombine.Text = "Combine Selected";
            this.cmdCombine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdCombine.UseVisualStyleBackColor = true;
            this.cmdCombine.Click += new System.EventHandler(this.cmdCombine_Click);
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
            // pnlMain
            // 
            this.pnlMain.AllowDrop = true;
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlMain.Controls.Add(this.OverlayFont);
            this.pnlMain.Controls.Add(this.lblDragAndDrop);
            this.pnlMain.Controls.Add(this.pnlZoom);
            this.pnlMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(604, 238);
            this.pnlMain.TabIndex = 4;
            this.pnlMain.DragDrop += new System.Windows.Forms.DragEventHandler(this.pnlMain_DragDrop);
            this.pnlMain.DragEnter += new System.Windows.Forms.DragEventHandler(this.pnlMain_DragEnter);
            this.pnlMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMain_Paint);
            this.pnlMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseDown);
            this.pnlMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseMove);
            this.pnlMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseUp);
            // 
            // OverlayFont
            // 
            this.OverlayFont.AutoSize = true;
            this.OverlayFont.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OverlayFont.ForeColor = System.Drawing.Color.Blue;
            this.OverlayFont.Location = new System.Drawing.Point(502, 12);
            this.OverlayFont.Name = "OverlayFont";
            this.OverlayFont.Size = new System.Drawing.Size(79, 13);
            this.OverlayFont.TabIndex = 2;
            this.OverlayFont.Text = "overlay font";
            this.OverlayFont.Visible = false;
            // 
            // lblDragAndDrop
            // 
            this.lblDragAndDrop.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDragAndDrop.AutoSize = true;
            this.lblDragAndDrop.Location = new System.Drawing.Point(216, 103);
            this.lblDragAndDrop.Name = "lblDragAndDrop";
            this.lblDragAndDrop.Size = new System.Drawing.Size(385, 40);
            this.lblDragAndDrop.TabIndex = 1;
            this.lblDragAndDrop.Text = "Drag and drop image(s) here";
            // 
            // pnlZoom
            // 
            this.pnlZoom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlZoom.ForeColor = System.Drawing.Color.Transparent;
            this.pnlZoom.Location = new System.Drawing.Point(8, 6);
            this.pnlZoom.Name = "pnlZoom";
            this.pnlZoom.Size = new System.Drawing.Size(80, 80);
            this.pnlZoom.TabIndex = 0;
            this.pnlZoom.Visible = false;
            this.pnlZoom.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlZoom_Paint);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 327);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlOptions);
            this.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(620, 365);
            this.Name = "MainForm";
            this.Text = "Alferd Spritesheet Unpacker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.pnlOptions.ResumeLayout(false);
            this.pnlOptions.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }
        internal System.Windows.Forms.Panel pnlOptions;
        internal System.Windows.Forms.Button cmdExport;
        internal System.Windows.Forms.Button cmdCombine;
        internal ASU.UI.BuffablePanel pnlMain;
        internal System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog1;
        internal ASU.UI.BuffablePanel pnlZoom;
        internal System.Windows.Forms.TextBox txtExportLocation;
        internal System.Windows.Forms.Button cmdOptions;
        internal System.Windows.Forms.Button cmdUndo;
        internal System.Windows.Forms.Button DeSelectAllButton;
        internal System.Windows.Forms.Button cmdPaste;
        internal System.Windows.Forms.Timer CheckForUnpackFinishTimer;
        internal System.Windows.Forms.Label lblDragAndDrop;
        internal ForkandBeard.Util.UI.AutoBalancingFormTimer ImageClipperAndAnimatorTimer;
        internal System.Windows.Forms.CheckBox chkCut;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button SelectAllButton;

        internal System.Windows.Forms.Label OverlayFont;
        //public MainForm()
        //{
        //    InitializeComponent();
        //}
    }
}