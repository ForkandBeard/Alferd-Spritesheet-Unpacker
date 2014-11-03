#Region " Options "
Option Strict On
Option Explicit On
#End Region

Public Class frmOptions

    Private Const STR_FORM_TITLE As String = "Alferd Spritesheet Unpacker ver.{0} Options"
    Private Const STR_ADVANCED_EXPORT_FILE_FORMAT As String = "<Advanced>"
    Public Main As frmMain

    Private Sub cmdSelectedColour_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelectedColour.Click
        Me.ColorDialog1.Color = Me.pnlSelectedColour.BackColor
        Me.ColorDialog1.ShowDialog()
        Me.pnlSelectedColour.BackColor = Color.FromArgb(200, Me.ColorDialog1.Color)
    End Sub

    Private Sub cmdTileBorderColour_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTileBorderColour.Click
        Me.ColorDialog1.Color = Me.pnlTileBorderColour.BackColor
        Me.ColorDialog1.ShowDialog()
        Me.pnlTileBorderColour.BackColor = Me.ColorDialog1.Color
    End Sub

    Private Sub cmdHoverColour_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdHoverColour.Click
        Me.ColorDialog1.Color = Me.pnlHoverColour.BackColor
        Me.ColorDialog1.ShowDialog()
        Me.pnlHoverColour.BackColor = Color.FromArgb(150, Me.ColorDialog1.Color)
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Try
            If Me.Main IsNot Nothing Then
                Dim blnReloadNeeded As Boolean = False

                frmMain.SelectedFill = New SolidBrush(Color.FromArgb(frmMain.SelectedFill.Color.A, Me.pnlSelectedColour.BackColor))
                frmMain.ZoomPen = New Pen(Color.FromArgb(frmMain.ZoomPen.Color.A, Me.pnlSelectedColour.BackColor), frmMain.ZoomPen.Width)
                frmMain.HoverFill = New SolidBrush(Color.FromArgb(frmMain.HoverFill.Color.A, Me.pnlHoverColour.BackColor))
                frmMain.Outline = New Pen(Color.FromArgb(frmMain.Outline.Color.A, Me.pnlTileBorderColour.BackColor), Me.ctlOutlineWidth.Value)
                frmMain.PromptForDestinationFolder = Me.chkPromptDestinationFolder.Checked
                frmMain.AutoOpenDestinationFolder = Me.chkOpenExportedDestination.Checked
                frmMain.MakeBackgroundTransparent = Me.chkExportBGTransparent.Checked

                If Me.cboExportFormat.Text <> STR_ADVANCED_EXPORT_FILE_FORMAT Then
                    frmMain.ExportNConvertArgs = String.Empty
                    frmMain.ExportFormat = CType(Me.cboExportFormat.SelectedItem, Drawing.Imaging.ImageFormat)
                Else
                    frmMain.ExportFormat = Nothing
                    frmMain.ExportNConvertArgs = Me.CreateCommandLineArgs
                End If

                If frmMain.DistanceBetweenTiles <> Me.ctlDistanceBetweenTiles.Value Then
                    frmMain.DistanceBetweenTiles = CInt(Me.ctlDistanceBetweenTiles.Value)
                    blnReloadNeeded = True
                End If

                frmMain.SheetWithBoxes = Nothing
                If blnReloadNeeded Then
                    Me.Main.ReloadOriginal()
                Else
                    Me.Main.pnlMain.Refresh()
                End If
            End If
            Me.Hide()
        Catch ex As Exception
            ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk")
        End Try
    End Sub

    Private Sub frmOptions_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Text = String.Format(STR_FORM_TITLE, My.Application.Info.Version.Major)
            Me.lnkCommandLineHelp.Text = String.Format("{0} command line help file", IO.Path.GetFileName(Configuration.ConfigurationManager.AppSettings("ThirdPartyImageConverter")))

            If Me.cboSelectAllOrder.SelectedIndex = -1 Then
                Me.cboSelectAllOrder.SelectedIndex = 0
            End If

            Me.cboExportFormat.Items.Clear()
            Me.cboExportFormat.Items.Add(Drawing.Imaging.ImageFormat.Png)
            Me.cboExportFormat.Items.Add(Drawing.Imaging.ImageFormat.Bmp)
            Me.cboExportFormat.Items.Add(Drawing.Imaging.ImageFormat.Gif)
            Me.cboExportFormat.Items.Add(Drawing.Imaging.ImageFormat.Tiff)
            Me.cboExportFormat.Items.Add(Drawing.Imaging.ImageFormat.Jpeg)
            Me.cboExportFormat.Items.Add(STR_ADVANCED_EXPORT_FILE_FORMAT)
            Me.lblCommandLine.Text = Me.CreateCommandLineArgs

            If Me.Main IsNot Nothing Then
                Me.chkExportBGTransparent.Checked = frmMain.MakeBackgroundTransparent
                Me.pnlSelectedColour.BackColor = frmMain.SelectedFill.Color
                Me.pnlTileBorderColour.BackColor = frmMain.Outline.Color
                Me.pnlHoverColour.BackColor = frmMain.HoverFill.Color
                Me.ctlDistanceBetweenTiles.Value = frmMain.DistanceBetweenTiles
                Me.ctlOutlineWidth.Value = CDec(frmMain.Outline.Width)
                If frmMain.ExportFormat IsNot Nothing Then
                    Me.cboExportFormat.SelectedItem = frmMain.ExportFormat
                Else
                    Me.cboExportFormat.Text = STR_ADVANCED_EXPORT_FILE_FORMAT
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error during option load")
        End Try
    End Sub

    Private Sub cmdAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAbout.Click
        frmAbout.ShowDialog()
    End Sub

    Private Sub cmdNConvertGuide_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frmGuide As frmNConvertGuide = New frmNConvertGuide

        frmGuide.ShowDialog(Me)
    End Sub

    Private Sub cboExportFormat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboExportFormat.SelectedIndexChanged
        Me.txtNConvertArgs.Enabled = Me.cboExportFormat.Text = STR_ADVANCED_EXPORT_FILE_FORMAT        
        Me.chkShowCommandLineArgs.Enabled = Me.txtNConvertArgs.Enabled
        Me.lnkCommandLineHelp.Visible = Me.txtNConvertArgs.Enabled AndAlso Me.chkShowCommandLineArgs.Checked
        Me.lblCommandLine.Visible = Me.txtNConvertArgs.Enabled AndAlso Me.chkShowCommandLineArgs.Checked

        If Me.cboExportFormat.Text = STR_ADVANCED_EXPORT_FILE_FORMAT Then
            Me.chkExportBGTransparent.Checked = False
        Else
            Me.chkShowCommandLineArgs.Checked = False
        End If
    End Sub

    Private Sub txtNConvertArgs_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNConvertArgs.TextChanged
        Me.lblCommandLine.Text = Me.CreateCommandLineArgs
    End Sub

    Private Function CreateCommandLineArgs() As String
        Dim strReturn As String

        strReturn = Configuration.ConfigurationManager.AppSettings("ThirdPartyImageConverterCommandArgsExportFormat").Replace("{text}", Me.txtNConvertArgs.Text)

        Return strReturn
    End Function

    Private Sub chkShowCommandLineArgs_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowCommandLineArgs.CheckedChanged
        Me.lblCommandLine.Visible = Me.chkShowCommandLineArgs.Checked
        Me.lnkCommandLineHelp.Visible = Me.chkShowCommandLineArgs.Checked
        If Me.chkShowCommandLineArgs.Checked Then

            Me.pnlExport.Height += 91
            Me.Height += 89
        Else
            Me.pnlExport.Height -= 91
            Me.Height -= 89
        End If
    End Sub

    Private Sub lnkCommandLineHelp_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkCommandLineHelp.LinkClicked
        Dim strThirdPartyImageConverterHelpFile As String

        strThirdPartyImageConverterHelpFile = Configuration.ConfigurationManager.AppSettings("ThirdPartyImageConverterHelpFile")
        If strThirdPartyImageConverterHelpFile.StartsWith("\") Then
            strThirdPartyImageConverterHelpFile = My.Application.Info.DirectoryPath & strThirdPartyImageConverterHelpFile
        End If
        Diagnostics.Process.Start(strThirdPartyImageConverterHelpFile)
    End Sub

    Private Sub lnkDownload_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkDownload.LinkClicked
        Diagnostics.Process.Start("http://www.alferdspritesheetunpacker.forkandbeard.co.uk?app=Alferd")
    End Sub

    Private Sub lnkHelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHelp.LinkClicked
        Diagnostics.Process.Start("http://www.alferdspritesheetunpacker.forkandbeard.co.uk/ForkandBeard/apps/AlferdSpritesheetUnpacker/FAQ.aspx?app=Alferd")
    End Sub

    Private Sub chkPreservePallette_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkPreservePallette.CheckedChanged
        If Me.chkPreservePallette.Checked Then
            Me.chkExportBGTransparent.Checked = False
            Me.chkExportBGTransparent.Enabled = False
        Else
            Me.chkExportBGTransparent.Enabled = True
        End If
    End Sub
End Class