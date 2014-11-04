<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.pnlOptions = New System.Windows.Forms.Panel()
        Me.SelectAllButton = New System.Windows.Forms.Button()
        Me.DeSelectAllButton = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkCut = New System.Windows.Forms.CheckBox()
        Me.cmdPaste = New System.Windows.Forms.Button()
        Me.cmdUndo = New System.Windows.Forms.Button()
        Me.cmdExport = New System.Windows.Forms.Button()
        Me.txtExportLocation = New System.Windows.Forms.TextBox()
        Me.cmdOptions = New System.Windows.Forms.Button()
        Me.cmdCombine = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.CheckForUnpackFinishTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ImageClipperAndAnimatorTimer = New ForkandBeard.Util.UI.AutoBalancingFormTimer(Me.components)
        Me.pnlMain = New SpriteSheetUnpacker.BuffablePanel()
        Me.OverlayFont = New System.Windows.Forms.Label()
        Me.lblDragAndDrop = New System.Windows.Forms.Label()
        Me.pnlZoom = New SpriteSheetUnpacker.BuffablePanel()
        Me.pnlOptions.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlOptions
        '
        Me.pnlOptions.Controls.Add(Me.SelectAllButton)
        Me.pnlOptions.Controls.Add(Me.DeSelectAllButton)
        Me.pnlOptions.Controls.Add(Me.Label1)
        Me.pnlOptions.Controls.Add(Me.chkCut)
        Me.pnlOptions.Controls.Add(Me.cmdPaste)
        Me.pnlOptions.Controls.Add(Me.cmdUndo)
        Me.pnlOptions.Controls.Add(Me.cmdExport)
        Me.pnlOptions.Controls.Add(Me.txtExportLocation)
        Me.pnlOptions.Controls.Add(Me.cmdOptions)
        Me.pnlOptions.Controls.Add(Me.cmdCombine)
        Me.pnlOptions.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlOptions.Location = New System.Drawing.Point(0, 238)
        Me.pnlOptions.Name = "pnlOptions"
        Me.pnlOptions.Size = New System.Drawing.Size(604, 89)
        Me.pnlOptions.TabIndex = 0
        '
        'SelectAllButton
        '
        Me.SelectAllButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SelectAllButton.Image = Global.SpriteSheetUnpacker.My.Resources.Resources.select_all
        Me.SelectAllButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.SelectAllButton.Location = New System.Drawing.Point(444, 6)
        Me.SelectAllButton.Name = "SelectAllButton"
        Me.SelectAllButton.Size = New System.Drawing.Size(78, 35)
        Me.SelectAllButton.TabIndex = 9
        Me.SelectAllButton.Text = "Select All"
        Me.SelectAllButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SelectAllButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.SelectAllButton.UseVisualStyleBackColor = True
        '
        'DeSelectAllButton
        '
        Me.DeSelectAllButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DeSelectAllButton.Image = Global.SpriteSheetUnpacker.My.Resources.Resources.deselect_all
        Me.DeSelectAllButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.DeSelectAllButton.Location = New System.Drawing.Point(523, 6)
        Me.DeSelectAllButton.Name = "DeSelectAllButton"
        Me.DeSelectAllButton.Size = New System.Drawing.Size(78, 35)
        Me.DeSelectAllButton.TabIndex = 3
        Me.DeSelectAllButton.Text = "De-select All"
        Me.DeSelectAllButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DeSelectAllButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.DeSelectAllButton.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 61)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "export location:"
        '
        'chkCut
        '
        Me.chkCut.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkCut.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkCut.Image = Global.SpriteSheetUnpacker.My.Resources.Resources.cursor
        Me.chkCut.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkCut.Location = New System.Drawing.Point(266, 6)
        Me.chkCut.Name = "chkCut"
        Me.chkCut.Size = New System.Drawing.Size(78, 35)
        Me.chkCut.TabIndex = 4
        Me.chkCut.Text = "Click Mode"
        Me.chkCut.UseVisualStyleBackColor = True
        '
        'cmdPaste
        '
        Me.cmdPaste.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdPaste.Image = Global.SpriteSheetUnpacker.My.Resources.Resources.paste_plain
        Me.cmdPaste.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdPaste.Location = New System.Drawing.Point(182, 6)
        Me.cmdPaste.Name = "cmdPaste"
        Me.cmdPaste.Size = New System.Drawing.Size(78, 35)
        Me.cmdPaste.TabIndex = 2
        Me.cmdPaste.Text = "From Clipboard"
        Me.cmdPaste.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdPaste.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.cmdPaste.UseVisualStyleBackColor = True
        '
        'cmdUndo
        '
        Me.cmdUndo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdUndo.Image = Global.SpriteSheetUnpacker.My.Resources.Resources.arrow_refresh
        Me.cmdUndo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdUndo.Location = New System.Drawing.Point(98, 6)
        Me.cmdUndo.Name = "cmdUndo"
        Me.cmdUndo.Size = New System.Drawing.Size(78, 35)
        Me.cmdUndo.TabIndex = 1
        Me.cmdUndo.Text = "Reload"
        Me.cmdUndo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdUndo.UseVisualStyleBackColor = True
        '
        'cmdExport
        '
        Me.cmdExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdExport.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExport.Image = Global.SpriteSheetUnpacker.My.Resources.Resources.images
        Me.cmdExport.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdExport.Location = New System.Drawing.Point(444, 47)
        Me.cmdExport.Name = "cmdExport"
        Me.cmdExport.Size = New System.Drawing.Size(157, 39)
        Me.cmdExport.TabIndex = 7
        Me.cmdExport.Text = "Export Selected..."
        Me.cmdExport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdExport.UseVisualStyleBackColor = True
        '
        'txtExportLocation
        '
        Me.txtExportLocation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtExportLocation.Location = New System.Drawing.Point(97, 58)
        Me.txtExportLocation.Name = "txtExportLocation"
        Me.txtExportLocation.Size = New System.Drawing.Size(341, 21)
        Me.txtExportLocation.TabIndex = 6
        '
        'cmdOptions
        '
        Me.cmdOptions.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdOptions.Image = Global.SpriteSheetUnpacker.My.Resources.Resources.wrench
        Me.cmdOptions.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdOptions.Location = New System.Drawing.Point(10, 6)
        Me.cmdOptions.Name = "cmdOptions"
        Me.cmdOptions.Size = New System.Drawing.Size(78, 35)
        Me.cmdOptions.TabIndex = 0
        Me.cmdOptions.Text = "Options..."
        Me.cmdOptions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdOptions.UseVisualStyleBackColor = True
        '
        'cmdCombine
        '
        Me.cmdCombine.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdCombine.Image = Global.SpriteSheetUnpacker.My.Resources.Resources.compress
        Me.cmdCombine.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdCombine.Location = New System.Drawing.Point(349, 6)
        Me.cmdCombine.Name = "cmdCombine"
        Me.cmdCombine.Size = New System.Drawing.Size(70, 35)
        Me.cmdCombine.TabIndex = 5
        Me.cmdCombine.Text = "Combine Selected"
        Me.cmdCombine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdCombine.UseVisualStyleBackColor = True
        '
        'CheckForUnpackFinishTimer
        '
        Me.CheckForUnpackFinishTimer.Interval = 200
        '
        'ImageClipperAndAnimatorTimer
        '
        Me.ImageClipperAndAnimatorTimer.Interval = 75
        Me.ImageClipperAndAnimatorTimer.MaxInterval = 1000
        Me.ImageClipperAndAnimatorTimer.MinInterval = 40
        '
        'pnlMain
        '
        Me.pnlMain.AllowDrop = True
        Me.pnlMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.pnlMain.Controls.Add(Me.OverlayFont)
        Me.pnlMain.Controls.Add(Me.lblDragAndDrop)
        Me.pnlMain.Controls.Add(Me.pnlZoom)
        Me.pnlMain.Cursor = System.Windows.Forms.Cursors.Default
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Font = New System.Drawing.Font("Segoe UI", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(604, 238)
        Me.pnlMain.TabIndex = 4
        '
        'OverlayFont
        '
        Me.OverlayFont.AutoSize = True
        Me.OverlayFont.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OverlayFont.ForeColor = System.Drawing.Color.Blue
        Me.OverlayFont.Location = New System.Drawing.Point(502, 12)
        Me.OverlayFont.Name = "OverlayFont"
        Me.OverlayFont.Size = New System.Drawing.Size(79, 13)
        Me.OverlayFont.TabIndex = 2
        Me.OverlayFont.Text = "overlay font"
        Me.OverlayFont.Visible = False
        '
        'lblDragAndDrop
        '
        Me.lblDragAndDrop.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblDragAndDrop.AutoSize = True
        Me.lblDragAndDrop.Location = New System.Drawing.Point(216, 103)
        Me.lblDragAndDrop.Name = "lblDragAndDrop"
        Me.lblDragAndDrop.Size = New System.Drawing.Size(385, 40)
        Me.lblDragAndDrop.TabIndex = 1
        Me.lblDragAndDrop.Text = "Drag and drop image(s) here"
        '
        'pnlZoom
        '
        Me.pnlZoom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlZoom.ForeColor = System.Drawing.Color.Transparent
        Me.pnlZoom.Location = New System.Drawing.Point(8, 6)
        Me.pnlZoom.Name = "pnlZoom"
        Me.pnlZoom.Size = New System.Drawing.Size(80, 80)
        Me.pnlZoom.TabIndex = 0
        Me.pnlZoom.Visible = False
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(604, 327)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlOptions)
        Me.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(620, 365)
        Me.Name = "frmMain"
        Me.Text = "Alferd Spritesheet Unpacker"
        Me.pnlOptions.ResumeLayout(False)
        Me.pnlOptions.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlOptions As System.Windows.Forms.Panel
    Friend WithEvents cmdExport As System.Windows.Forms.Button
    Friend WithEvents cmdCombine As System.Windows.Forms.Button
    Friend WithEvents pnlMain As SpriteSheetUnpacker.BuffablePanel
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents pnlZoom As SpriteSheetUnpacker.BuffablePanel
    Friend WithEvents txtExportLocation As System.Windows.Forms.TextBox
    Friend WithEvents cmdOptions As System.Windows.Forms.Button
    Friend WithEvents cmdUndo As System.Windows.Forms.Button
    Friend WithEvents DeSelectAllButton As System.Windows.Forms.Button
    Friend WithEvents cmdPaste As System.Windows.Forms.Button
    Friend WithEvents CheckForUnpackFinishTimer As System.Windows.Forms.Timer
    Friend WithEvents lblDragAndDrop As System.Windows.Forms.Label
    Friend WithEvents ImageClipperAndAnimatorTimer As ForkandBeard.Util.UI.AutoBalancingFormTimer
    Friend WithEvents chkCut As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SelectAllButton As System.Windows.Forms.Button
    Friend WithEvents OverlayFont As System.Windows.Forms.Label

End Class
