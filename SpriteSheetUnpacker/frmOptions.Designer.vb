<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOptions
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOptions))
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.cmdSelectedColour = New System.Windows.Forms.Button()
        Me.pnlSelectedColour = New System.Windows.Forms.Panel()
        Me.pnlTileBorderColour = New System.Windows.Forms.Panel()
        Me.cmdTileBorderColour = New System.Windows.Forms.Button()
        Me.pnlHoverColour = New System.Windows.Forms.Panel()
        Me.cmdHoverColour = New System.Windows.Forms.Button()
        Me.ctlOutlineWidth = New System.Windows.Forms.NumericUpDown()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.lblOutlineWidth = New System.Windows.Forms.Label()
        Me.lblDistanceBetweenTiles = New System.Windows.Forms.Label()
        Me.ctlDistanceBetweenTiles = New System.Windows.Forms.NumericUpDown()
        Me.cmdAbout = New System.Windows.Forms.Button()
        Me.cboExportFormat = New System.Windows.Forms.ComboBox()
        Me.lblExportFormat = New System.Windows.Forms.Label()
        Me.txtNConvertArgs = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkShowCommandLineArgs = New System.Windows.Forms.CheckBox()
        Me.lblCommandLine = New System.Windows.Forms.Label()
        Me.lnkCommandLineHelp = New System.Windows.Forms.LinkLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.pnlExport = New System.Windows.Forms.GroupBox()
        Me.chkPreservePallette = New System.Windows.Forms.CheckBox()
        Me.chkExportBGTransparent = New System.Windows.Forms.CheckBox()
        Me.lnkDownload = New System.Windows.Forms.LinkLabel()
        Me.cboSelectAllOrder = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chkPromptDestinationFolder = New System.Windows.Forms.CheckBox()
        Me.chkOpenExportedDestination = New System.Windows.Forms.CheckBox()
        Me.lnkHelp = New System.Windows.Forms.LinkLabel()
        CType(Me.ctlOutlineWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ctlDistanceBetweenTiles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.pnlExport.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdSelectedColour
        '
        Me.cmdSelectedColour.Image = Global.SpriteSheetUnpacker.My.Resources.Resources.color_wheel
        Me.cmdSelectedColour.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSelectedColour.Location = New System.Drawing.Point(9, 17)
        Me.cmdSelectedColour.Name = "cmdSelectedColour"
        Me.cmdSelectedColour.Size = New System.Drawing.Size(78, 35)
        Me.cmdSelectedColour.TabIndex = 0
        Me.cmdSelectedColour.Text = "Selected Colour"
        Me.cmdSelectedColour.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSelectedColour.UseVisualStyleBackColor = True
        '
        'pnlSelectedColour
        '
        Me.pnlSelectedColour.BackColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlSelectedColour.Location = New System.Drawing.Point(93, 17)
        Me.pnlSelectedColour.Name = "pnlSelectedColour"
        Me.pnlSelectedColour.Size = New System.Drawing.Size(70, 35)
        Me.pnlSelectedColour.TabIndex = 5
        '
        'pnlTileBorderColour
        '
        Me.pnlTileBorderColour.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlTileBorderColour.Location = New System.Drawing.Point(93, 58)
        Me.pnlTileBorderColour.Name = "pnlTileBorderColour"
        Me.pnlTileBorderColour.Size = New System.Drawing.Size(70, 35)
        Me.pnlTileBorderColour.TabIndex = 7
        '
        'cmdTileBorderColour
        '
        Me.cmdTileBorderColour.Image = Global.SpriteSheetUnpacker.My.Resources.Resources.color_wheel
        Me.cmdTileBorderColour.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdTileBorderColour.Location = New System.Drawing.Point(9, 58)
        Me.cmdTileBorderColour.Name = "cmdTileBorderColour"
        Me.cmdTileBorderColour.Size = New System.Drawing.Size(78, 35)
        Me.cmdTileBorderColour.TabIndex = 1
        Me.cmdTileBorderColour.Text = "Tile Border"
        Me.cmdTileBorderColour.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdTileBorderColour.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.cmdTileBorderColour.UseVisualStyleBackColor = True
        '
        'pnlHoverColour
        '
        Me.pnlHoverColour.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.pnlHoverColour.Location = New System.Drawing.Point(93, 99)
        Me.pnlHoverColour.Name = "pnlHoverColour"
        Me.pnlHoverColour.Size = New System.Drawing.Size(70, 35)
        Me.pnlHoverColour.TabIndex = 9
        '
        'cmdHoverColour
        '
        Me.cmdHoverColour.Image = Global.SpriteSheetUnpacker.My.Resources.Resources.color_wheel
        Me.cmdHoverColour.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdHoverColour.Location = New System.Drawing.Point(9, 99)
        Me.cmdHoverColour.Name = "cmdHoverColour"
        Me.cmdHoverColour.Size = New System.Drawing.Size(78, 35)
        Me.cmdHoverColour.TabIndex = 2
        Me.cmdHoverColour.Text = "Hover Colour"
        Me.cmdHoverColour.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdHoverColour.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.cmdHoverColour.UseVisualStyleBackColor = True
        '
        'ctlOutlineWidth
        '
        Me.ctlOutlineWidth.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctlOutlineWidth.Location = New System.Drawing.Point(103, 20)
        Me.ctlOutlineWidth.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.ctlOutlineWidth.Name = "ctlOutlineWidth"
        Me.ctlOutlineWidth.Size = New System.Drawing.Size(48, 27)
        Me.ctlOutlineWidth.TabIndex = 0
        Me.ctlOutlineWidth.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdClose.Location = New System.Drawing.Point(286, 328)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(76, 39)
        Me.cmdClose.TabIndex = 9
        Me.cmdClose.Text = "Update and Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'lblOutlineWidth
        '
        Me.lblOutlineWidth.Location = New System.Drawing.Point(4, 17)
        Me.lblOutlineWidth.Name = "lblOutlineWidth"
        Me.lblOutlineWidth.Size = New System.Drawing.Size(93, 32)
        Me.lblOutlineWidth.TabIndex = 12
        Me.lblOutlineWidth.Text = "Tile Outline Width"
        Me.lblOutlineWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDistanceBetweenTiles
        '
        Me.lblDistanceBetweenTiles.Location = New System.Drawing.Point(7, 59)
        Me.lblDistanceBetweenTiles.Name = "lblDistanceBetweenTiles"
        Me.lblDistanceBetweenTiles.Size = New System.Drawing.Size(90, 32)
        Me.lblDistanceBetweenTiles.TabIndex = 14
        Me.lblDistanceBetweenTiles.Text = "Distance Between Frames"
        Me.lblDistanceBetweenTiles.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ctlDistanceBetweenTiles
        '
        Me.ctlDistanceBetweenTiles.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctlDistanceBetweenTiles.Location = New System.Drawing.Point(103, 62)
        Me.ctlDistanceBetweenTiles.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.ctlDistanceBetweenTiles.Name = "ctlDistanceBetweenTiles"
        Me.ctlDistanceBetweenTiles.Size = New System.Drawing.Size(48, 27)
        Me.ctlDistanceBetweenTiles.TabIndex = 1
        Me.ctlDistanceBetweenTiles.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'cmdAbout
        '
        Me.cmdAbout.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdAbout.Font = New System.Drawing.Font("Calibri", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAbout.Location = New System.Drawing.Point(16, 328)
        Me.cmdAbout.Name = "cmdAbout"
        Me.cmdAbout.Size = New System.Drawing.Size(36, 16)
        Me.cmdAbout.TabIndex = 6
        Me.cmdAbout.Text = "about..."
        Me.cmdAbout.UseVisualStyleBackColor = True
        '
        'cboExportFormat
        '
        Me.cboExportFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboExportFormat.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboExportFormat.FormattingEnabled = True
        Me.cboExportFormat.Location = New System.Drawing.Point(105, 20)
        Me.cboExportFormat.Name = "cboExportFormat"
        Me.cboExportFormat.Size = New System.Drawing.Size(229, 27)
        Me.cboExportFormat.TabIndex = 0
        '
        'lblExportFormat
        '
        Me.lblExportFormat.Location = New System.Drawing.Point(6, 18)
        Me.lblExportFormat.Name = "lblExportFormat"
        Me.lblExportFormat.Size = New System.Drawing.Size(96, 32)
        Me.lblExportFormat.TabIndex = 16
        Me.lblExportFormat.Text = "File Format"
        Me.lblExportFormat.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNConvertArgs
        '
        Me.txtNConvertArgs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNConvertArgs.Enabled = False
        Me.txtNConvertArgs.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNConvertArgs.Location = New System.Drawing.Point(104, 92)
        Me.txtNConvertArgs.Name = "txtNConvertArgs"
        Me.txtNConvertArgs.Size = New System.Drawing.Size(230, 27)
        Me.txtNConvertArgs.TabIndex = 3
        Me.txtNConvertArgs.Text = "pcx"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(3, 87)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 32)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Advanced File Format"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkShowCommandLineArgs
        '
        Me.chkShowCommandLineArgs.BackColor = System.Drawing.Color.Transparent
        Me.chkShowCommandLineArgs.Enabled = False
        Me.chkShowCommandLineArgs.Location = New System.Drawing.Point(152, 116)
        Me.chkShowCommandLineArgs.Name = "chkShowCommandLineArgs"
        Me.chkShowCommandLineArgs.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkShowCommandLineArgs.Size = New System.Drawing.Size(182, 26)
        Me.chkShowCommandLineArgs.TabIndex = 4
        Me.chkShowCommandLineArgs.Text = "Show Advanced Command Line"
        Me.chkShowCommandLineArgs.UseVisualStyleBackColor = False
        '
        'lblCommandLine
        '
        Me.lblCommandLine.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCommandLine.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.lblCommandLine.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCommandLine.Location = New System.Drawing.Point(8, 152)
        Me.lblCommandLine.Name = "lblCommandLine"
        Me.lblCommandLine.Size = New System.Drawing.Size(326, 59)
        Me.lblCommandLine.TabIndex = 21
        Me.lblCommandLine.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblCommandLine.Visible = False
        '
        'lnkCommandLineHelp
        '
        Me.lnkCommandLineHelp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnkCommandLineHelp.BackColor = System.Drawing.Color.Transparent
        Me.lnkCommandLineHelp.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkCommandLineHelp.Location = New System.Drawing.Point(9, 136)
        Me.lnkCommandLineHelp.Name = "lnkCommandLineHelp"
        Me.lnkCommandLineHelp.Size = New System.Drawing.Size(325, 14)
        Me.lnkCommandLineHelp.TabIndex = 22
        Me.lnkCommandLineHelp.TabStop = True
        Me.lnkCommandLineHelp.Text = "command line help file"
        Me.lnkCommandLineHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lnkCommandLineHelp.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ctlOutlineWidth)
        Me.GroupBox1.Controls.Add(Me.lblOutlineWidth)
        Me.GroupBox1.Controls.Add(Me.ctlDistanceBetweenTiles)
        Me.GroupBox1.Controls.Add(Me.lblDistanceBetweenTiles)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 74)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(157, 103)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Frame Settings"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmdSelectedColour)
        Me.GroupBox2.Controls.Add(Me.pnlSelectedColour)
        Me.GroupBox2.Controls.Add(Me.cmdTileBorderColour)
        Me.GroupBox2.Controls.Add(Me.pnlTileBorderColour)
        Me.GroupBox2.Controls.Add(Me.cmdHoverColour)
        Me.GroupBox2.Controls.Add(Me.pnlHoverColour)
        Me.GroupBox2.Location = New System.Drawing.Point(187, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(175, 143)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Colours"
        '
        'pnlExport
        '
        Me.pnlExport.Controls.Add(Me.chkPreservePallette)
        Me.pnlExport.Controls.Add(Me.chkExportBGTransparent)
        Me.pnlExport.Controls.Add(Me.lnkCommandLineHelp)
        Me.pnlExport.Controls.Add(Me.lblExportFormat)
        Me.pnlExport.Controls.Add(Me.cboExportFormat)
        Me.pnlExport.Controls.Add(Me.txtNConvertArgs)
        Me.pnlExport.Controls.Add(Me.Label1)
        Me.pnlExport.Controls.Add(Me.lblCommandLine)
        Me.pnlExport.Controls.Add(Me.chkShowCommandLineArgs)
        Me.pnlExport.Location = New System.Drawing.Point(16, 179)
        Me.pnlExport.Name = "pnlExport"
        Me.pnlExport.Size = New System.Drawing.Size(346, 140)
        Me.pnlExport.TabIndex = 5
        Me.pnlExport.TabStop = False
        Me.pnlExport.Text = "Export Options"
        '
        'chkPreservePallette
        '
        Me.chkPreservePallette.AutoSize = True
        Me.chkPreservePallette.Location = New System.Drawing.Point(106, 72)
        Me.chkPreservePallette.Name = "chkPreservePallette"
        Me.chkPreservePallette.Size = New System.Drawing.Size(107, 17)
        Me.chkPreservePallette.TabIndex = 2
        Me.chkPreservePallette.Text = "Preserve Pallette"
        Me.chkPreservePallette.UseVisualStyleBackColor = True
        '
        'chkExportBGTransparent
        '
        Me.chkExportBGTransparent.AutoSize = True
        Me.chkExportBGTransparent.Checked = True
        Me.chkExportBGTransparent.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkExportBGTransparent.Location = New System.Drawing.Point(106, 53)
        Me.chkExportBGTransparent.Name = "chkExportBGTransparent"
        Me.chkExportBGTransparent.Size = New System.Drawing.Size(168, 17)
        Me.chkExportBGTransparent.TabIndex = 1
        Me.chkExportBGTransparent.Text = "Make Background Transparent"
        Me.chkExportBGTransparent.UseVisualStyleBackColor = True
        '
        'lnkDownload
        '
        Me.lnkDownload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lnkDownload.BackColor = System.Drawing.Color.Transparent
        Me.lnkDownload.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkDownload.Location = New System.Drawing.Point(6, 351)
        Me.lnkDownload.Name = "lnkDownload"
        Me.lnkDownload.Size = New System.Drawing.Size(124, 23)
        Me.lnkDownload.TabIndex = 7
        Me.lnkDownload.TabStop = True
        Me.lnkDownload.Text = "Download latest version"
        Me.lnkDownload.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboSelectAllOrder
        '
        Me.cboSelectAllOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSelectAllOrder.FormattingEnabled = True
        Me.cboSelectAllOrder.Items.AddRange(New Object() {"Top Left", "Bottom Left", "Centre"})
        Me.cboSelectAllOrder.Location = New System.Drawing.Point(280, 156)
        Me.cboSelectAllOrder.Name = "cboSelectAllOrder"
        Me.cboSelectAllOrder.Size = New System.Drawing.Size(82, 21)
        Me.cboSelectAllOrder.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(210, 151)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 32)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Select All Order"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkPromptDestinationFolder
        '
        Me.chkPromptDestinationFolder.BackColor = System.Drawing.Color.Transparent
        Me.chkPromptDestinationFolder.Checked = True
        Me.chkPromptDestinationFolder.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPromptDestinationFolder.Location = New System.Drawing.Point(6, 5)
        Me.chkPromptDestinationFolder.Name = "chkPromptDestinationFolder"
        Me.chkPromptDestinationFolder.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkPromptDestinationFolder.Size = New System.Drawing.Size(161, 28)
        Me.chkPromptDestinationFolder.TabIndex = 0
        Me.chkPromptDestinationFolder.Text = "Prompt for Export Folder"
        Me.chkPromptDestinationFolder.UseVisualStyleBackColor = False
        '
        'chkOpenExportedDestination
        '
        Me.chkOpenExportedDestination.BackColor = System.Drawing.Color.Transparent
        Me.chkOpenExportedDestination.Checked = True
        Me.chkOpenExportedDestination.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOpenExportedDestination.Location = New System.Drawing.Point(-19, 33)
        Me.chkOpenExportedDestination.Name = "chkOpenExportedDestination"
        Me.chkOpenExportedDestination.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkOpenExportedDestination.Size = New System.Drawing.Size(186, 28)
        Me.chkOpenExportedDestination.TabIndex = 1
        Me.chkOpenExportedDestination.Text = "Open Folder Exported to"
        Me.chkOpenExportedDestination.UseVisualStyleBackColor = False
        '
        'lnkHelp
        '
        Me.lnkHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lnkHelp.BackColor = System.Drawing.Color.Transparent
        Me.lnkHelp.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkHelp.Location = New System.Drawing.Point(136, 351)
        Me.lnkHelp.Name = "lnkHelp"
        Me.lnkHelp.Size = New System.Drawing.Size(31, 23)
        Me.lnkHelp.TabIndex = 8
        Me.lnkHelp.TabStop = True
        Me.lnkHelp.Text = "help"
        Me.lnkHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(374, 379)
        Me.Controls.Add(Me.lnkHelp)
        Me.Controls.Add(Me.chkOpenExportedDestination)
        Me.Controls.Add(Me.chkPromptDestinationFolder)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboSelectAllOrder)
        Me.Controls.Add(Me.lnkDownload)
        Me.Controls.Add(Me.pnlExport)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdAbout)
        Me.Controls.Add(Me.cmdClose)
        Me.Font = New System.Drawing.Font("Calibri", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOptions"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Options"
        CType(Me.ctlOutlineWidth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ctlDistanceBetweenTiles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.pnlExport.ResumeLayout(False)
        Me.pnlExport.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents cmdSelectedColour As System.Windows.Forms.Button
    Friend WithEvents pnlSelectedColour As System.Windows.Forms.Panel
    Friend WithEvents pnlTileBorderColour As System.Windows.Forms.Panel
    Friend WithEvents cmdTileBorderColour As System.Windows.Forms.Button
    Friend WithEvents pnlHoverColour As System.Windows.Forms.Panel
    Friend WithEvents cmdHoverColour As System.Windows.Forms.Button
    Friend WithEvents ctlOutlineWidth As System.Windows.Forms.NumericUpDown
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents lblOutlineWidth As System.Windows.Forms.Label
    Friend WithEvents lblDistanceBetweenTiles As System.Windows.Forms.Label
    Friend WithEvents ctlDistanceBetweenTiles As System.Windows.Forms.NumericUpDown
    Friend WithEvents cmdAbout As System.Windows.Forms.Button
    Friend WithEvents cboExportFormat As System.Windows.Forms.ComboBox
    Friend WithEvents lblExportFormat As System.Windows.Forms.Label
    Friend WithEvents txtNConvertArgs As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkShowCommandLineArgs As System.Windows.Forms.CheckBox
    Friend WithEvents lblCommandLine As System.Windows.Forms.Label
    Friend WithEvents lnkCommandLineHelp As System.Windows.Forms.LinkLabel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents pnlExport As System.Windows.Forms.GroupBox
    Friend WithEvents lnkDownload As System.Windows.Forms.LinkLabel
    Friend WithEvents chkExportBGTransparent As System.Windows.Forms.CheckBox
    Friend WithEvents cboSelectAllOrder As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkPromptDestinationFolder As System.Windows.Forms.CheckBox
    Friend WithEvents chkOpenExportedDestination As System.Windows.Forms.CheckBox
    Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
    Friend WithEvents chkPreservePallette As System.Windows.Forms.CheckBox
End Class
