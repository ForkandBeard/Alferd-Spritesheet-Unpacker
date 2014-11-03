#Region " Options "
Option Strict On
Option Explicit On
#End Region

Imports QuickImagePopper

Public Class frmMain

#Region " Class Data "
    Private Const STR_FORM_TITLE As String = "Alferd Spritesheet Unpacker ver.{0} {1}"
    Private Const INT_REGION_WIDTHS As Integer = 200

    Private LoadingImage As Boolean = False
    Private IsMouseDown As Boolean

    Private FormTitle As String
    Private ZoomImage As Bitmap    

    Private OverlayText As String

    Private MouseLocation As Point
    Private MouseDownLocation As Point
    Private Offset As Point = New Point(0, 0)
    Private _Boxes As List(Of Rectangle) = New List(Of Rectangle)
    Private Property Boxes As List(Of Rectangle)
        Get
            Return Me._Boxes
        End Get
        Set(ByVal value As List(Of Rectangle))
            Me._Boxes = value
        End Set
    End Property
    Private Selected As List(Of Rectangle) = New List(Of Rectangle)
    Private Splits As List(Of Rectangle) = New List(Of Rectangle)
    Private SplitTopLeft As Rectangle
    Private SplitBottomRight As Rectangle
    Private BoxSplitting As Rectangle
    Private Hover As Rectangle = Rectangle.Empty
    'Private BackgroundColour As Color
    Private Options As frmOptions
    Private SuppressThirdPartyWarningMessage As Boolean = False

    Public Shared DistanceBetweenTiles As Integer = 3
    Public Shared SheetWithBoxes As Bitmap
    Private Shared SheetWithBoxesEnlarged As Bitmap
    Public Shared HoverFill As SolidBrush = New SolidBrush(Color.FromArgb(150, 224, 224, 224))
    Public Shared SelectedFill As SolidBrush = New SolidBrush(Color.FromArgb(200, 100, 100, 255))
    Public Shared ZoomPen As Pen = New Pen(Color.FromArgb(100, 100, 100, 255), 4)
    Public Shared Outline As Pen = New Pen(Color.FromArgb(225, 50, 50, 255), 2)

    Public Shared ExportFormat As Drawing.Imaging.ImageFormat = Drawing.Imaging.ImageFormat.Png
    Public Shared ExportNConvertArgs As String = String.Empty
    Private Shared ThirdPartyImageConverterPath As String
    Public Shared PromptForDestinationFolder As Boolean = True
    Public Shared AutoOpenDestinationFolder As Boolean = True
    Public Shared MakeBackgroundTransparent As Boolean = True

    Private multipleUnpackerTimer As Threading.Timer

    Private Random As Random = New Random
#End Region

    Private unpackers As List(Of ImageUnpacker) = New List(Of ImageUnpacker)

    Private Sub CreateUnpacker(ByVal pimgImage As Bitmap, ByVal fileName As String)
        Dim unpacker As ImageUnpacker
        Me.pnlOptions.Enabled = False
        Me.Boxes.Clear()
        Me.Selected.Clear()
        Me.Hover = Rectangle.Empty

        RegionUnpacker.Counter = 0
        SheetWithBoxes = Nothing

        Me.UpdateTitlePc(0)

        Me.pnlZoom.Visible = False

        unpacker = New ImageUnpacker(pimgImage, fileName)
        Me.unpackers.Add(unpacker)
    End Sub

    Private Sub StartUnpackers()
        If Me.unpackers.Count = 1 Then
            Me.HandleOneOrMoreUnpackers(Nothing)
        Else
            If MessageBox.Show(String.Format("You are about to Unpack and automatically export {0} spritesheets to [{1}]. Are you sure you want to continue?", Me.unpackers.Count, Me.txtExportLocation.Text), "Confirm multiple Unpack", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then
                'If Me.unpackers.Count = 1 Then
                'Me.ResetFormPostUnpack(Me.unpackers(0))
                'Else
                'Me.ResetFormPostUnpack(Nothing)
                'End If
                Me.ResetFormPostUnpack(Nothing)
                Exit Sub
            End If
            Me.multipleUnpackerTimer = New Threading.Timer(New Threading.TimerCallback(AddressOf Me.HandleOneOrMoreUnpackers), Nothing, 0, 1000)
        End If

        Me.LoadingImage = True
        Me.ImageClipperAndAnimatorTimer.Start()
        Me.CheckForUnpackFinishTimer.Start()
    End Sub

    Private Sub HandleOneOrMoreUnpackers(ByVal state As Object)
        Try
            If Me.unpackers.Count = 1 Then
                Me.unpackers(0).StartUnpacking()
                Exit Sub
            End If

            Dim countUnpacking As Integer = 0
            Dim countUnpacked As Integer = 0

            For Each unpacker As ImageUnpacker In Me.unpackers
                If unpacker.IsUnpacking() _
                AndAlso Not unpacker.IsUnpacked() Then
                    countUnpacking += 1
                End If

                If unpacker.IsUnpacked() Then
                    countUnpacked += 1
                End If
            Next

            If countUnpacked = Me.unpackers.Count Then
                Dim oldPromptForDestinationFolder As Boolean = PromptForDestinationFolder
                PromptForDestinationFolder = False
                Me.multipleUnpackerTimer.Dispose()

                Me.ExportUnpackers(Me.unpackers)
                Me.unpackers.Clear()
                PromptForDestinationFolder = oldPromptForDestinationFolder

                If AutoOpenDestinationFolder Then
                    Diagnostics.Process.Start(Me.txtExportLocation.Text)
                End If
            Else
                ' Ensure 2 are unpacking at one time.
                If countUnpacking < 2 Then
                    For Each unpacker As ImageUnpacker In Me.unpackers
                        If Not unpacker.IsUnpacking() _
                        AndAlso Not unpacker.IsUnpacked() Then
                            unpacker.StartUnpacking()
                            Me.HandleOneOrMoreUnpackers(state)
                            Exit Sub
                        End If
                    Next
                End If
            End If
        Catch ex As Exception            
            ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk", Me)
        End Try
    End Sub

    Private Sub UpdateTitlePc(ByVal pintPc As Integer)
        Dim title As String
        Dim pcCompleteText As String = "["

        For k As Integer = 0 To 100 Step 10
            If pintPc + 5 >= k Then
                pcCompleteText &= ">"
            Else
                pcCompleteText &= "="
            End If
        Next

        pcCompleteText &= String.Format("] Unpacking {0}%", pintPc)
        title = String.Format(STR_FORM_TITLE, My.Application.Info.Version.Major, pcCompleteText)
        Me.FormTitle = title
    End Sub

    Private Sub SetHoverOverlayText()

        Me.SetOverlayText( _
                            New List(Of String)({ _
                                                "height" _
                                                , "width" _
                                                }) _
                            , New List(Of String)({ _
                                                  Me.Hover.Height.ToString() _
                                                , Me.Hover.Width.ToString() _
                                                }) _
                            )
    End Sub

    Private Sub SetFullImageOverlayText()
        Dim colours As String

        If Me.unpackers(0).ColoursCount > 999 Then
            colours = "999+"
        Else
            colours = Me.unpackers(0).ColoursCount.ToString()
        End If

        Me.SetOverlayText( _
                            New List(Of String)({ _
                                                "height" _
                                                , "width" _
                                                , "colours" _
                                                , "frames" _
                                                , "selected" _
                                                }) _
                            , New List(Of String)({ _
                                                  Me.unpackers(0).GetSize().Height.ToString() _
                                                , Me.unpackers(0).GetSize().Width.ToString() _
                                                , colours _
                                                , Me.Boxes.Count.ToString() _
                                                , Me.Selected.Count.ToString() _
                                                }) _
                            )
    End Sub

    Private Sub SetOverlayText(ByVal labels As List(Of String), ByVal data As List(Of String))
        Me.OverlayText = String.Empty
        For i As Integer = 0 To labels.Count - 1
            Me.OverlayText &= labels(i) & ":" & New String(" "c, 9 - labels(i).Length) & data(i)
            Me.OverlayText &= Environment.NewLine
        Next
    End Sub

    Private Sub SetColoursBasedOnBackground(ByVal colour As Color)
        Dim sngBrightness As Single

        sngBrightness = colour.GetBrightness()

        If sngBrightness >= 0.66 Then
            ' Very light.
            Outline.Color = Color.FromArgb(Outline.Color.A, 0, 0, 0)
            SelectedFill.Color = Color.FromArgb(SelectedFill.Color.A, 0, 0, 75)
            HoverFill.Color = Color.FromArgb(HoverFill.Color.A, 75, 0, 0)
        ElseIf sngBrightness <= 0.33 Then
            ' Very dark.
            Outline.Color = Color.FromArgb(Outline.Color.A, 255, 255, 255)
            SelectedFill.Color = Color.FromArgb(SelectedFill.Color.A, 200, 200, 255)
            HoverFill.Color = Color.FromArgb(HoverFill.Color.A, 255, 200, 200)
        Else

            Dim r As Integer
            Dim g As Integer
            Dim b As Integer

            If colour.R >= colour.G Then
                If colour.B >= colour.R Then
                    ' Blue is dominant.
                    g = 200
                    r = 200
                    b = CInt(colour.R * 0.5F)
                    Outline.Color = Color.FromArgb(Outline.Color.A, r, g, b)
                    HoverFill.Color = Color.FromArgb(HoverFill.Color.A, CInt(r * 0.5F), CInt(g * 0.5F), CInt(b * 0.5F))
                    r = CInt(colour.G * 0.5F)
                Else
                    ' Red is dominant.
                    g = 200
                    b = 200
                    r = CInt(colour.B * 0.5F)
                    Outline.Color = Color.FromArgb(Outline.Color.A, r, g, b)
                    HoverFill.Color = Color.FromArgb(HoverFill.Color.A, CInt(r * 0.5F), CInt(g * 0.5F), CInt(b * 0.5F))
                    b = CInt(colour.G * 0.5F)
                End If
            Else
                If colour.B >= colour.G Then
                    ' Blue is dominant.
                    g = 200
                    b = 200
                    r = CInt(colour.R * 0.5F)
                    Outline.Color = Color.FromArgb(Outline.Color.A, r, g, b)
                    HoverFill.Color = Color.FromArgb(HoverFill.Color.A, CInt(r * 0.5F), CInt(g * 0.5F), CInt(b * 0.5F))
                    b = CInt(colour.G * 0.5F)
                Else
                    ' Green is dominant.
                    b = 200
                    r = 200
                    g = CInt(colour.R * 0.5F)
                    Outline.Color = Color.FromArgb(Outline.Color.A, r, g, b)
                    HoverFill.Color = Color.FromArgb(HoverFill.Color.A, CInt(r * 0.5F), CInt(g * 0.5F), CInt(b * 0.5F))
                    r = CInt(colour.B * 0.5F)
                End If
            End If

            SelectedFill.Color = Color.FromArgb(SelectedFill.Color.A, CInt(r * 0.8F), CInt(g * 0.8F), CInt(b * 0.8F))
        End If
    End Sub

    'Private Sub UnpackImageInNewThread()
    '    Dim objOriginal As Bitmap = Nothing
    '    Dim colColours As Dictionary(Of Color, Integer) = New Dictionary(Of Color, Integer)
    '    Dim objPresentColour As Color
    '    Dim intPc As Integer

    '    Try
    '        SyncLock (Me.OriginalLock)
    '            objOriginal = New Bitmap(CType(Me.Original.Clone, Bitmap))
    '        End SyncLock

    '        For x As Integer = 0 To objOriginal.Width - 1
    '            For y As Integer = 0 To objOriginal.Height - 1
    '                objPresentColour = objOriginal.GetPixel(x, y)

    '                If Not colColours.ContainsKey(objPresentColour) Then
    '                    colColours.Add(objPresentColour, 1)
    '                Else
    '                    colColours(objPresentColour) += 1
    '                End If

    '                If (x + y) Mod 100 = 0 Then
    '                    Dim intTotal As Integer

    '                    intTotal = objOriginal.Width * objOriginal.Height
    '                    intPc = CInt((((x * (objOriginal.Height - 1)) + y) / intTotal) * 10)
    '                    Me.UpdateTitlePc(intPc)
    '                End If
    '            Next
    '        Next

    '        Dim objBackground As Color = Color.Black
    '        Dim objMaxCount As Integer = 0

    '        For Each objColour As Color In colColours.Keys
    '            If colColours(objColour) >= objMaxCount Then
    '                objMaxCount = colColours(objColour)
    '                objBackground = objColour
    '            End If
    '        Next

    '        Me.BackgroundColour = objBackground
    '        Me.pnlMain.BackColor = Me.BackgroundColour
    '        Me.pnlZoom.BackColor = Me.BackgroundColour

    '        Me.SetColoursBasedOnBackground()

    '        Dim colUnpackers As List(Of RegionUnpacker) = New List(Of RegionUnpacker)
    '        Dim objUnpacker As RegionUnpacker
    '        Dim objThread As Threading.Thread
    '        Dim objImage As Bitmap
    '        Dim intXSize As Integer
    '        Dim intYSize As Integer

    '        intYSize = CInt(Math.Ceiling(objOriginal.Height / 6))
    '        intXSize = CInt(Math.Ceiling(objOriginal.Width / 6))

    '        For y As Integer = 0 To intYSize * 7 Step intYSize
    '            For x As Integer = 0 To intXSize * 7 Step intXSize
    '                objImage = CType(objOriginal.Clone, Bitmap)
    '                objUnpacker = New RegionUnpacker(objImage, New Rectangle(x, y, Math.Min(intXSize, (objImage.Width - x) - 1), Math.Min(intYSize, (objImage.Height - y) - 1)), Me.BackgroundColour)
    '                colUnpackers.Add(objUnpacker)
    '                objThread = New Threading.Thread(AddressOf objUnpacker.UnpackRegion)
    '                objThread.Start()
    '                intPc = 10 + CInt((((y * (objOriginal.Width - 1)) + x) / (objOriginal.Height * objOriginal.Width)) * 10)
    '                Me.UpdateTitlePc(intPc)
    '            Next
    '        Next

    '        'For y As Integer = 0 To objOriginal.Height - 1 Step INT_REGION_WIDTHS
    '        '    For x As Integer = 0 To objOriginal.Width - 1 Step INT_REGION_WIDTHS
    '        '        objImage = CType(objOriginal.Clone, Bitmap)
    '        '        objUnpacker = New Unpacker(objImage, New Rectangle(x, y, Math.Min(INT_REGION_WIDTHS, (objImage.Width - x) - 1), Math.Min(INT_REGION_WIDTHS, (objImage.Height - y) - 1)), Me.BackgroundColour)
    '        '        colUnpackers.Add(objUnpacker)
    '        '        objThread = New Threading.Thread(AddressOf objUnpacker.UnpackRegion)
    '        '        objThread.Start()
    '        '        intPc = 10 + CInt((((y * (objOriginal.Width - 1)) + x) / (objOriginal.Height * objOriginal.Width)) * 10)
    '        '        Me.UpdateTitlePc(intPc)
    '        '    Next
    '        'Next
    '        Dim dblTinyIncrement As Double = 0
    '        SyncLock (RegionUnpacker.Wait)
    '            Do While RegionUnpacker.Counter < colUnpackers.Count
    '                Threading.Monitor.Wait(RegionUnpacker.Wait)
    '                Dim intTotal As Integer
    '                intTotal = colUnpackers.Count
    '                intPc = 20 + CInt((RegionUnpacker.Counter / intTotal) * 70)
    '                intPc = CInt(Math.Min(95, intPc + dblTinyIncrement))
    '                Me.UpdateTitlePc(intPc)
    '                dblTinyIncrement += Date.Now.Millisecond / 1800
    '            Loop
    '        End SyncLock

    '        For Each objUnpacked As RegionUnpacker In colUnpackers
    '            Me.Boxes.AddRange(objUnpacked.Boxes)
    '        Next

    '        Me.UpdateTitlePc(Me.Random.Next(96, 100))
    '        RegionUnpacker.CombineBoxes(Me.Boxes, Me.BackgroundColour, objOriginal)
    '        Me.UpdateTitlePc(100)

    '        Me.LoadingImage = False
    '        Me.ImageClipperAndAnimatorTimer.Stop()
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Error in unpacking thread")
    '    Finally
    '        If objOriginal IsNot Nothing Then
    '            objOriginal.Dispose()
    '        End If
    '    End Try
    'End Sub

    Private Sub SplitBoxAtLocation(ByVal pobjBox As Rectangle, ByVal pobjLocation As Point)
        Me.Splits.Clear()

        If pobjBox.Width * pobjBox.Height > 1 Then
            Me.SplitTopLeft = New Rectangle(pobjBox.X, pobjBox.Y, pobjLocation.X - pobjBox.X, pobjLocation.Y - pobjBox.Y)
            Me.Splits.Add(Me.SplitTopLeft)
            Me.Splits.Add(New Rectangle(pobjLocation.X, pobjBox.Y, pobjBox.Right - pobjLocation.X, pobjLocation.Y - pobjBox.Y))
            Me.Splits.Add(New Rectangle(pobjBox.X, pobjLocation.Y, pobjLocation.X - pobjBox.X, pobjBox.Bottom - pobjLocation.Y))
            Me.SplitBottomRight = New Rectangle(pobjLocation.X, pobjLocation.Y, pobjBox.Right - pobjLocation.X, pobjBox.Bottom - pobjLocation.Y)
            Me.Splits.Add(Me.SplitBottomRight)
            Me.BoxSplitting = pobjBox
        Else
            Me.BoxSplitting = Rectangle.Empty
        End If
    End Sub

    Public Sub ReloadOriginal()
        If Me.unpackers.Count <> 1 Then
            Exit Sub
        End If

        Me.pnlOptions.Enabled = False
        Me.Boxes.Clear()
        Me.Selected.Clear()
        Me.Hover = Rectangle.Empty

        RegionUnpacker.Counter = 0
        SheetWithBoxes = Nothing

        Me.LoadingImage = True
        Me.UpdateTitlePc(0)

        Me.pnlZoom.Visible = False

        Me.StartUnpackers()
    End Sub

#Region " Events "

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'TODO: Dispose all unpacker images.

        If SheetWithBoxes IsNot Nothing Then
            SheetWithBoxes.Dispose()
        End If

        If SheetWithBoxesEnlarged IsNot Nothing Then
            SheetWithBoxesEnlarged.Dispose()
        End If
    End Sub

    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Randomize()
        Try
            Me.SuppressThirdPartyWarningMessage = CBool(Configuration.ConfigurationManager.AppSettings("SuppressThirdPartyImageConverterWarningMessage"))
            Me.txtExportLocation.Text = My.Application.Info.DirectoryPath
            Me.KeyPreview = True
            ThirdPartyImageConverterPath = Configuration.ConfigurationManager.AppSettings("ThirdPartyImageConverter")

            If ThirdPartyImageConverterPath.StartsWith("\") Then
                ThirdPartyImageConverterPath = My.Application.Info.DirectoryPath & ThirdPartyImageConverterPath
            End If
            AutoOpenDestinationFolder = CBool(Configuration.ConfigurationManager.AppSettings("AutoOpenDestinationFolder"))
            PromptForDestinationFolder = CBool(Configuration.ConfigurationManager.AppSettings("PromptForDestinationFolder"))
            Outline.Width = CInt(Configuration.ConfigurationManager.AppSettings("TileOutlineWidth"))
            DistanceBetweenTiles = CInt(Configuration.ConfigurationManager.AppSettings("DistanceBetweenFrames"))
            MakeBackgroundTransparent = CBool(Configuration.ConfigurationManager.AppSettings("ExportedOptionsMakeBackgroundTransparent"))

            Dim formats As Dictionary(Of String, Imaging.ImageFormat) = New Dictionary(Of String, Imaging.ImageFormat)
            formats.Add("png", Drawing.Imaging.ImageFormat.Png)
            formats.Add("bmp", Drawing.Imaging.ImageFormat.Bmp)
            formats.Add("gif", Drawing.Imaging.ImageFormat.Gif)
            formats.Add("tiff", Drawing.Imaging.ImageFormat.Tiff)
            formats.Add("jpeg", Drawing.Imaging.ImageFormat.Jpeg)
            formats.Add("jpg", Drawing.Imaging.ImageFormat.Jpeg)
            ExportFormat = formats(Configuration.ConfigurationManager.AppSettings("ExportedOptionsFileFormat").Replace(".", "").ToLower())
        Catch ex As Exception
            ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk")
        End Try
    End Sub

    Private Sub pnlMain_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles pnlMain.DragDrop
        Dim objDropped As Object

        If Me.LoadingImage Then
            Exit Sub
        End If

        Try
            Me.unpackers.Clear()
            For Each objFormat As String In e.Data.GetFormats
                objDropped = e.Data.GetData(objFormat)

                If objDropped.GetType() Is GetType(String()) Then
                    Dim objImage As Bitmap
                    Dim fileNames As List(Of String)
                    Dim hasUserBeenPromptedToConvertFiles As Boolean = False
                    Dim userOkToConvertFiles As Boolean = True

                    fileNames = New List(Of String)(CType(objDropped, String()))

                    For Each strFileName As String In fileNames

                        Try
                            objImage = New Bitmap(strFileName)
                            Me.CreateUnpacker(objImage, IO.Path.GetFileNameWithoutExtension(strFileName))
                        Catch ex As ArgumentException
                            Dim strArgs As String
                            Dim strLocation As String
                            Dim objConvertProcess As Process
                            Dim strTempFileName As String
                            Dim strTempFileNameAndExtension As String

                            If Not hasUserBeenPromptedToConvertFiles Then
                                If Not Me.SuppressThirdPartyWarningMessage Then
                                    userOkToConvertFiles = MessageBox.Show(String.Format("You are trying to load a sprite sheet in a non-standard image file format. " _
                                                                      & "This file will be converted to a common image format first using the third party command line tool '{0}'." _
                                                                      & "{1}{1}Your operating system may request confirmation to execute {0}." _
                                                                      & "{1}{1}If you'd like to use a different conversion utility then please do so by editing the 'app.config' file found here:{1}{2}" _
                                                                      & "{1}{1}Do you want to continue and use '{0}'?" _
                                                                      , Helper.GetThirdPartyConversionToolExecutableName _
                                                                      , Environment.NewLine _
                                                                      , My.Application.Info.DirectoryPath & "\app.config") _
                                                        , "Third Party Converter Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes

                                    hasUserBeenPromptedToConvertFiles = True
                                    If userOkToConvertFiles Then
                                        Me.SuppressThirdPartyWarningMessage = True
                                    End If
                                End If
                            End If

                            If userOkToConvertFiles Then
                                RegionUnpacker.DeleteAllTempFiles()

                                strTempFileName = String.Format("asu_temp_spritesheet_{0}", Guid.NewGuid.ToString.Replace("-", ""))
                                strTempFileNameAndExtension = strTempFileName & IO.Path.GetExtension(strFileName)

                                My.Computer.FileSystem.CopyFile(strFileName, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\" & strTempFileNameAndExtension, True)

                                strLocation = String.Format("""{0}\{1}", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), strTempFileNameAndExtension)
                                strArgs = Configuration.ConfigurationManager.AppSettings("ThirdPartyImageConverterCommandArgsConvertImportToBitmap")
                                strArgs = strArgs.Replace("{temp}", strLocation)

                                objConvertProcess = Diagnostics.Process.Start(ThirdPartyImageConverterPath, strArgs)
                                objConvertProcess.WaitForExit()

                                strLocation = String.Format("{0}\{1}.bmp", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), strTempFileName)
                                objImage = New Bitmap(strLocation)
                                Me.CreateUnpacker(objImage, IO.Path.GetFileNameWithoutExtension(strFileName))
                            End If
                        End Try
                        Me.lblDragAndDrop.Visible = False
                    Next
                    Me.StartUnpackers()
                    Exit Sub
                End If
            Next
        Catch ex As ArgumentException
            MessageBox.Show("File is not an image file format supported by ASU. " & ex.Message, "Unable to import file")
        Catch ex As Exception
            ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk")
        End Try
    End Sub

    Private Sub pnlMain_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles pnlMain.DragEnter
        e.Effect = DragDropEffects.Move
    End Sub

    Private Sub pnlMain_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlMain.MouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                'Me.rdoSelect.Checked = Not Me.rdoSelect.Checked
                Me.chkCut.Checked = Not Me.chkCut.Checked
            End If

            If Me.LoadingImage Then
                Exit Sub
            End If

            If Not Me.chkCut.Checked Then
                If Me.Hover <> Rectangle.Empty Then
                    If Me.Selected.Contains(Me.Hover) Then
                        Me.Selected.Remove(Me.Hover)
                    Else
                        Me.Selected.Add(Me.Hover)
                    End If

                    Exit Sub
                End If
            Else
                If Not Me.BoxSplitting.IsEmpty _
                AndAlso Me.Splits.Count > 0 Then
                    If Me.Selected.Contains(Me.BoxSplitting) Then
                        Me.Selected.Remove(Me.BoxSplitting)
                        Me.Selected.AddRange(Me.Splits)
                    End If
                    Me.Boxes.Remove(Me.BoxSplitting)
                    Me.Boxes.AddRange(Me.Splits)
                    Me.Splits.Clear()
                    SheetWithBoxes = Nothing
                    Me.chkCut.Checked = False
                    Exit Sub
                End If
            End If

            Me.IsMouseDown = True
            Me.MouseDownLocation = New Point(e.Location.X - Me.Offset.X, e.Location.Y - Me.Offset.Y)
        Catch ex As Exception
            ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk")
        End Try
    End Sub

    Private Sub pnlMain_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlMain.MouseMove
        Try
            If Me.LoadingImage Then
                Exit Sub
            End If

            Dim objLocation As Point
            Me.pnlMain.Cursor = Cursors.Default

            objLocation = New Point(e.Location.X - Me.Offset.X, e.Location.Y - Me.Offset.Y)

            If Me.IsMouseDown Then
                Me.Offset = New Point(e.Location.X - Me.MouseDownLocation.X, e.Location.Y - Me.MouseDownLocation.Y)
            Else
                Me.Hover = Rectangle.Empty
                For Each objBox As Rectangle In Me.Boxes

                    If objBox.Contains(objLocation) Then
                        If Me.chkCut.Checked Then
                            Me.pnlMain.Cursor = Cursors.Cross
                            Me.SplitBoxAtLocation(objBox, objLocation)
                        Else
                            Me.Hover = objBox
                        End If
                        Exit For
                    Else
                        Me.Splits.Clear()
                    End If
                Next

                If Me.unpackers.Count > 0 _
                AndAlso Me.AreAllUnpacked() Then
                    If Me.Hover.IsEmpty Then
                        Me.SetFullImageOverlayText()
                    Else
                        Me.SetHoverOverlayText()
                    End If
                End If

                Me.MouseLocation = e.Location
            End If
            Me.Refresh()
            Me.pnlZoom.Refresh()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error during mouse over")
        End Try
    End Sub

    Private Sub pnlMain_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlMain.MouseUp
        Me.IsMouseDown = False
    End Sub

    Private Sub pnlMain_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlMain.Paint
        Dim objGraphics As Graphics = Nothing
        Dim objZoomGraphics As Graphics = Nothing
        Dim objOriginal As Bitmap = Nothing
        Dim objPaint As Bitmap = New Bitmap(Me.pnlMain.ClientRectangle.Width, Me.pnlMain.ClientRectangle.Height)

        Try
            If Me.LoadingImage Then
                Exit Sub
            End If

            If Me.unpackers.Count = 1 Then
                objOriginal = Me.unpackers(0).GetOriginalClone()
            End If

            If objOriginal IsNot Nothing Then
                If SheetWithBoxes Is Nothing Then
                    Dim objBoxGraphics As Graphics = Nothing
                    Try
                        SheetWithBoxes = New Bitmap(objOriginal)
                        objBoxGraphics = Graphics.FromImage(SheetWithBoxes)
                        objBoxGraphics.SmoothingMode = Drawing2D.SmoothingMode.HighSpeed

                        Dim objZoomOutline As Pen
                        objZoomOutline = New Pen(Outline.Color, 1)
                        SheetWithBoxesEnlarged = New Bitmap(objOriginal)
                        objZoomGraphics = Graphics.FromImage(SheetWithBoxesEnlarged)

                        For Each objBox As Rectangle In Me.Boxes
                            objBoxGraphics.DrawRectangle(Outline, objBox)
                            objZoomGraphics.DrawRectangle(objZoomOutline, objBox)
                        Next
                    Finally
                        If objBoxGraphics IsNot Nothing Then
                            objBoxGraphics.Dispose()
                        End If
                    End Try
                End If

                objGraphics = Graphics.FromImage(objPaint)
                objGraphics.DrawImage(SheetWithBoxes, Me.Offset)

                Dim objBoxOffset As Rectangle

                If Me.Splits.Count > 0 Then
                    objBoxOffset = Me.SplitTopLeft
                    objBoxOffset.Offset(Me.Offset)
                    objGraphics.FillRectangle(HoverFill, objBoxOffset)

                    objBoxOffset = Me.SplitBottomRight
                    objBoxOffset.Offset(Me.Offset)
                    objGraphics.FillRectangle(HoverFill, objBoxOffset)
                End If

                If Me.Hover <> Rectangle.Empty Then
                    objBoxOffset = Me.Hover
                    objBoxOffset.Offset(Me.Offset)
                    objGraphics.FillRectangle(HoverFill, objBoxOffset)
                End If

                For Each objSelected As Rectangle In Me.Selected
                    objBoxOffset = objSelected
                    objBoxOffset.Offset(Me.Offset)
                    objGraphics.FillRectangle(SelectedFill, objBoxOffset)
                Next

                If Not Me.IsMouseDown Then
                    Me.ZoomImage = New Bitmap(20, 20)

                    Using g As Graphics = Graphics.FromImage(Me.ZoomImage)
                        g.DrawImage(objPaint, 0, 0, New Rectangle(Me.MouseLocation.X - 10, Me.MouseLocation.Y - 10, 20, 20), GraphicsUnit.Pixel)
                    End Using

                    If Me.pnlZoom.BackgroundImage IsNot Nothing Then
                        Me.pnlZoom.BackgroundImage.Dispose()
                    End If
                    Me.pnlZoom.BackgroundImage = QuickImagePopper.Filters.IncreaseScale(Me.ZoomImage, 4)
                    Me.ZoomImage.Dispose()
                End If

                e.Graphics.DrawImage(objPaint, New Point(0, 0))

                If Outline.Color.GetBrightness() >= 0.5 Then
                    Using overlayBrushShadow As SolidBrush = New SolidBrush(Color.FromArgb(CInt(Outline.Color.G / 2), CInt(Outline.Color.B / 2), CInt(Outline.Color.R / 2)))
                        e.Graphics.DrawString(Me.OverlayText, Me.OverlayFont.Font, overlayBrushShadow, New Point(Me.Width - 109, 6))
                    End Using
                Else
                    Using overlayBrushShadow As SolidBrush = New SolidBrush(Color.FromArgb(Math.Min(255, CInt(Outline.Color.G * 1.5)), Math.Min(255, CInt(Outline.Color.B * 1.5)), Math.Min(255, CInt(Outline.Color.R * 1.5))))
                        e.Graphics.DrawString(Me.OverlayText, Me.OverlayFont.Font, overlayBrushShadow, New Point(Me.Width - 109, 6))
                    End Using
                End If

                Using overlayBrush As SolidBrush = New SolidBrush(Outline.Color)
                    e.Graphics.DrawString(Me.OverlayText, Me.OverlayFont.Font, overlayBrush, New Point(Me.Width - 110, 5))
                End Using
            End If
        Catch ex As Exception
            ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk")
        Finally
            If objOriginal IsNot Nothing Then
                objOriginal.Dispose()
            End If
            If objGraphics IsNot Nothing Then
                objGraphics.Dispose()
            End If
            If objZoomGraphics IsNot Nothing Then
                objZoomGraphics.Dispose()
            End If
            If objPaint IsNot Nothing Then
                objPaint.Dispose()
            End If
        End Try
    End Sub

    Private Function GetNearestWhole(ByVal pintValue As Integer, ByVal pintIncrement As Integer) As Integer
        Return CInt(pintValue / pintIncrement) * pintIncrement
    End Function

    Private Sub frmMain_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.pnlMain.Refresh()
    End Sub

    Private Sub cmdCombine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCombine.Click
        Dim objNewBox As Rectangle = Rectangle.Empty
        Try
            If Me.unpackers.Count <> 1 Then
                MessageBox.Show("No spritesheet loaded. Please drop a spritesheet onto this form and select some frames before trying to combine frames.")
                Exit Sub
            Else
                If Me.Selected.Count = 0 Then
                    MessageBox.Show("No frames selected. Please select some frames before trying to combine.")
                    Exit Sub
                End If
            End If

            For Each objBox As Rectangle In Me.Selected

                If objNewBox = Rectangle.Empty Then
                    objNewBox = objBox
                Else

                    If objBox.Right > objNewBox.Right Then
                        objNewBox.Width = objBox.Right - objNewBox.Left
                    End If

                    If objBox.Left < objNewBox.Left Then
                        objNewBox.Width += objNewBox.Left - objBox.Left
                    End If

                    If objBox.Bottom > objNewBox.Bottom Then
                        objNewBox.Height = objBox.Bottom - objNewBox.Top
                    End If

                    If objBox.Top < objNewBox.Top Then
                        objNewBox.Height += objNewBox.Top - objBox.Top
                    End If

                    objNewBox.X = Math.Min(objNewBox.X, objBox.X)
                    objNewBox.Y = Math.Min(objBox.Y, objNewBox.Y)
                End If

                Me.Boxes.Remove(objBox)
            Next

            Dim colToRemove As List(Of Rectangle) = New List(Of Rectangle)

            For Each objBox In Me.Boxes
                If objBox.IntersectsWith(objNewBox) Then
                    colToRemove.Add(objBox)
                End If
            Next

            For Each objRemove As Rectangle In colToRemove
                Me.Boxes.Remove(objRemove)
            Next

            Me.Selected.Clear()
            Me.Selected.Add(objNewBox)
            Me.Boxes.Add(objNewBox)
            SheetWithBoxes = Nothing
            Me.Refresh()
        Catch ex As Exception
            ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk")
        End Try
    End Sub

    Private Sub ExportUnpackers(ByVal unpackers As List(Of ImageUnpacker))
        Dim strArgs As String
        Dim colTempFiles As List(Of String) = New List(Of String)
        Dim strOutpath As String
        Dim hasUserBeenPromptedToConvertFiles As Boolean = False
        Dim userOkToConvertFiles As Boolean = True
        Dim boxes As List(Of Rectangle)
        Dim laspe As DateTime = Date.MinValue

        Try
            If PromptForDestinationFolder Then
                Me.FolderBrowserDialog1.SelectedPath = Me.txtExportLocation.Text
            End If

            If Me.Selected.Count > 0 _
            OrElse unpackers.Count > 1 Then

                For Each unpacker As ImageUnpacker In unpackers
                    If ( _
                        PromptForDestinationFolder _
                        AndAlso Me.FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK _
                        ) _
                        OrElse Not PromptForDestinationFolder Then

                        If PromptForDestinationFolder Then
                            Me.txtExportLocation.Text = Me.FolderBrowserDialog1.SelectedPath
                            strOutpath = Me.FolderBrowserDialog1.SelectedPath
                        End If

                        strOutpath = Me.txtExportLocation.Text

                        Dim objOriginal As Bitmap

                        objOriginal = unpacker.GetOriginalClone()

                        If Not String.IsNullOrEmpty(ExportNConvertArgs) Then
                            If Not Me.SuppressThirdPartyWarningMessage Then
                                If Not hasUserBeenPromptedToConvertFiles Then
                                    userOkToConvertFiles = MessageBox.Show(String.Format("You are about to export an advanced image file format." _
                                                                      & "This file will be converted to a to common file format first and then converted to the advanced image file format by the third party command line tool '{0}'." _
                                                                      & "{1}{1}Your operating system may request confirmation to execute {0}." _
                                                                      & "{1}{1}If you'd like to use a different conversion utility then please do so by editing the 'app.config' file found here:{1}{2}" _
                                                                      & "{1}{1}Do you want to continue and use'{0}'?" _
                                                                      , Helper.GetThirdPartyConversionToolExecutableName _
                                                                      , Environment.NewLine _
                                                                      , My.Application.Info.DirectoryPath & "\app.config" _
                                                                      ), "Third Party Converter Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes

                                    hasUserBeenPromptedToConvertFiles = True
                                End If

                                If Not userOkToConvertFiles Then
                                    Exit Sub
                                End If
                            Else
                                Me.SuppressThirdPartyWarningMessage = True
                            End If
                        End If

                        If unpackers.Count > 1 Then
                            strOutpath = IO.Path.Combine(Me.txtExportLocation.Text, unpacker.FileName)
                            IO.Directory.CreateDirectory(strOutpath)
                        End If

                        Dim intPreFileCount As Integer

                        intPreFileCount = IO.Directory.GetFiles(strOutpath).Length

                        If unpackers.Count > 1 Then
                            boxes = unpacker.GetBoxes()
                            If Me.Options Is Nothing Then
                                boxes = ImageUnpacker.OrderBoxes(boxes, SelectAllOrder.TopLeft, unpacker.GetSize())
                            Else
                                boxes = ImageUnpacker.OrderBoxes(boxes, CType(Me.Options.cboSelectAllOrder.SelectedIndex, SelectAllOrder), unpacker.GetSize())
                            End If
                        Else
                            boxes = Me.Selected
                        End If

                        If laspe <> Date.MaxValue Then
                            laspe = Date.Now
                        End If

                        For k As Integer = 0 To boxes.Count - 1
                            If Not boxes(k).IsEmpty Then

                                Dim objBitmap As Bitmap = New Bitmap(boxes(k).Width, boxes(k).Height, objOriginal.PixelFormat)

                                Using objGraphics As Graphics = Graphics.FromImage(objBitmap)
                                    objGraphics.DrawImage(objOriginal, New Rectangle(0, 0, objBitmap.Width, objBitmap.Height), boxes(k), GraphicsUnit.Pixel)
                                    objGraphics.Dispose()
                                End Using

                                If Me.Options IsNot Nothing _
                                AndAlso Me.Options.chkPreservePallette.Checked Then
                                    If unpacker.GetPallette() IsNot Nothing Then
                                        Dim quantizer As ImageQuantizers.PaletteQuantizer
                                        Dim quantized As Bitmap
                                        quantizer = New ImageQuantizers.PaletteQuantizer(New ArrayList(unpacker.GetPallette().Entries))
                                        quantized = quantizer.Quantize(objBitmap)
                                        objBitmap.Dispose()
                                        objBitmap = quantized
                                    End If
                                End If

                                If MakeBackgroundTransparent Then
                                    objBitmap.MakeTransparent(unpacker.GetBackgroundColour())
                                End If

                                If String.IsNullOrEmpty(ExportNConvertArgs) Then
                                    objBitmap.Save(String.Format("{0}\{1}.{2}", strOutpath, k.ToString, ExportFormat.ToString.ToLower), ExportFormat)
                                Else
                                    Dim strTempBitmapPath As String
                                    Dim startInfo As Diagnostics.ProcessStartInfo
                                    strTempBitmapPath = String.Format("{0}\{1}.png", strOutpath, k.ToString)
                                    colTempFiles.Add(strTempBitmapPath)
                                    objBitmap.Save(strTempBitmapPath, Imaging.ImageFormat.Png)
                                    strArgs = ExportNConvertArgs.Replace("{file_name}", String.Format("""{0}\{1}.png""", strOutpath, k.ToString))

                                    startInfo = New Diagnostics.ProcessStartInfo(ThirdPartyImageConverterPath, strArgs)
                                    startInfo.CreateNoWindow = True
                                    startInfo.UseShellExecute = True
                                    Using objProcess As Diagnostics.Process = Diagnostics.Process.Start(startInfo)
                                        objProcess.WaitForExit()
                                        If Date.Now.Subtract(laspe).TotalSeconds > 10 Then
                                            If MessageBox.Show("Export of frames is taking a while. Do you want to abort?", "Execessive Export Time", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
                                                If objOriginal IsNot Nothing Then
                                                    objOriginal.Dispose()
                                                End If
                                                Exit Sub
                                            End If
                                            laspe = Date.MaxValue
                                        End If
                                    End Using
                                End If

                                objBitmap.Dispose()
                            End If
                        Next

                        If objOriginal IsNot Nothing Then
                            objOriginal.Dispose()
                        End If

                        If colTempFiles.Count > 0 Then
                            Dim colNotConverted As List(Of String) = New List(Of String)

                            For Each strTempFile As String In colTempFiles
                                If IO.File.Exists(strTempFile) Then
                                    colNotConverted.Add(strTempFile)
                                End If
                                IO.File.Delete(strTempFile)
                            Next

                            If colNotConverted.Count > 0 _
                            AndAlso IO.Directory.GetFiles(strOutpath).Length < (intPreFileCount + (colTempFiles.Count * 2)) Then
                                MessageBox.Show(String.Format("Exported files failed to be converted.{3}{3}Arguments used:{3}{0}{3}{3}Please see '{1}' documentation at {3}[{2}].", ExportNConvertArgs, Helper.GetThirdPartyConversionToolExecutableName, Helper.GetThirdPartyConversionToolDirectory, Environment.NewLine))
                                Exit Sub
                            End If
                        End If

                        If AutoOpenDestinationFolder _
                        AndAlso unpackers.Count = 1 Then
                            Diagnostics.Process.Start(strOutpath)
                        End If
                    End If
                Next
            Else
                If Me.unpackers.Count <> 1 Then
                    MessageBox.Show("No spritesheet loaded. Please drop a spritesheet onto this form and select some frames before exporting.")
                    Exit Sub
                Else
                    MessageBox.Show("No frames selected. Please select some frames before exporting.")
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw New Exception("An error occured whilst exporting frames.", ex)
        End Try
    End Sub

    Private Sub cmdExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExport.Click
        Try
            Me.ExportUnpackers(Me.unpackers)
        Catch ex As Exception
            ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk")
        End Try
    End Sub

    Private Sub pnlZoom_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlZoom.Paint
        Try
            If Not Me.chkCut.Checked Then
                e.Graphics.DrawLine(ZoomPen, 0, (Me.pnlZoom.ClientRectangle.Height / 2.0F) + 2, Me.pnlZoom.ClientRectangle.Width, (Me.pnlZoom.ClientRectangle.Height / 2.0F) + 2)
                e.Graphics.DrawLine(ZoomPen, (Me.pnlZoom.ClientRectangle.Width / 2.0F) + 2, 0, (Me.pnlZoom.ClientRectangle.Width / 2.0F) + 2, Me.pnlZoom.ClientRectangle.Height)
            End If
        Catch sorry As Exception
        End Try
    End Sub

    Private Sub cmdUndo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUndo.Click
        Try
            If Me.unpackers.Count <> 1 Then
                MessageBox.Show("No image loaded. First drag an image onto the form above, or use the paste 'From Clipboard' button.")
                Exit Sub
            End If

            Me.ReloadOriginal()
        Catch ex As Exception
            ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk")
        End Try
    End Sub

    Private Sub SelectAllButton_Click(sender As System.Object, e As System.EventArgs) Handles SelectAllButton.Click
        Try
            Me.Selected.Clear()
            If Me.Options Is Nothing Then
                Me.Selected = ImageUnpacker.OrderBoxes(Me.unpackers(0).GetBoxes(), SelectAllOrder.TopLeft, Me.unpackers(0).GetSize())
            Else
                Me.Selected = ImageUnpacker.OrderBoxes(Me.unpackers(0).GetBoxes(), CType(Me.Options.cboSelectAllOrder.SelectedIndex, SelectAllOrder), Me.unpackers(0).GetSize())
            End If
            Me.SetFullImageOverlayText()
            Me.Refresh()
        Catch ex As Exception
            ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk")
        End Try
    End Sub

    Private Sub DeSelectAllButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeSelectAllButton.Click
        Try
            Me.Selected.Clear()
            Me.SetFullImageOverlayText()
            Me.Refresh()            
        Catch ex As Exception
            ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk")
        End Try
    End Sub

    Private Sub cmdPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPaste.Click
        Try
            If My.Computer.Clipboard.ContainsImage Then
                Me.unpackers.Clear()
                Me.lblDragAndDrop.Visible = False
                Me.CreateUnpacker(New Bitmap(My.Computer.Clipboard.GetImage), "clipboard")
                Me.StartUnpackers()
            Else
                MessageBox.Show("No image found in Clipboard")
            End If
        Catch ex As Exception
            ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk")
        End Try
    End Sub

    Private Sub cmdOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOptions.Click
        Try
            If Me.Options Is Nothing _
            OrElse Me.Options.IsDisposed Then
                Me.Options = New frmOptions
                Me.Options.Main = Me
                Me.Options.chkPromptDestinationFolder.Checked = PromptForDestinationFolder
                Me.Options.chkOpenExportedDestination.Checked = AutoOpenDestinationFolder
            End If

            Me.Options.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error loading options")
        End Try
    End Sub

    Private Function AreAllUnpacked() As Boolean
        Dim unpackedCount As Integer = 0

        For Each unpacker As ImageUnpacker In Me.unpackers
            If unpacker.IsUnpacked() Then
                unpackedCount += 1
            End If
        Next

        Return unpackedCount = Me.unpackers.Count
    End Function

    Private Sub ResetFormPostUnpack(ByVal unpacker As ImageUnpacker)
        If unpacker IsNot Nothing Then
            Me.Boxes = unpacker.GetBoxes()
        Else
            Me.Boxes = New List(Of Rectangle)
        End If

        Me.pnlMain.Refresh()
        Me.CheckForUnpackFinishTimer.Stop()
        Me.ImageClipperAndAnimatorTimer.Stop()
        Me.pnlMain.BackgroundImage = Nothing
        Me.pnlOptions.Enabled = True
        Me.pnlZoom.Visible = Me.unpackers.Count = 1

        Me.Offset = New Point(0, 0)
        Me.Text = String.Format(STR_FORM_TITLE, My.Application.Info.Version.Major, "")
        Me.LoadingImage = False

        If Me.unpackers.Count > 1 _
        OrElse Me.unpackers.Count = 0 Then
            Me.lblDragAndDrop.Visible = True
            Me.pnlMain.BackColor = Color.FromArgb(224, 224, 224)
            Me.SetOverlayText(New List(Of String)(), New List(Of String)())
        Else
            Me.SetFullImageOverlayText()
        End If
    End Sub

    Private Sub CheckForUnpackFinishTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckForUnpackFinishTimer.Tick
        Dim unpacker As ImageUnpacker

        Try
            If Me.unpackers.Count = 1 Then
                unpacker = Me.unpackers(0)
            Else
                Dim counter As Integer = 0
                Do
                    unpacker = Me.unpackers(Me.Random.Next(0, Me.unpackers.Count))
                    counter += 1
                Loop While (unpacker.IsUnpacked() Or Not unpacker.IsUnpacking()) And counter < 100
            End If

            If unpacker.IsBackgroundColourSet() Then
                Me.SetColoursBasedOnBackground(unpacker.GetBackgroundColour())
                Me.pnlMain.BackColor = unpacker.GetBackgroundColour()
            End If

            If Me.AreAllUnpacked() Then
                Me.ResetFormPostUnpack(unpacker)
            End If
        Catch ex As Exception
            CheckForUnpackFinishTimer.Stop()
            MessageBox.Show(ex.Message & ". CheckForUnpackFinishTimer has been stopped, you may need to restart application", "Error during timer")
        End Try
    End Sub

    Private Sub ImageClipperAndAnimatorTimer_BalancedTock() Handles ImageClipperAndAnimatorTimer.BalancedTock
        Dim unpacker As ImageUnpacker
        Dim pcComplete As Integer = 0
        Dim objOriginal As Bitmap = Nothing
        Dim objBackgroundImage As Bitmap = Nothing
        Dim objGraphics As Graphics = Nothing
        Try
            If Me.unpackers.Count = 1 Then
                unpacker = Me.unpackers(0)
                pcComplete = unpacker.GetPcComplete()
            Else
                Dim counter As Integer = 0
                Do
                    unpacker = Me.unpackers(Me.Random.Next(0, Me.unpackers.Count))
                    counter += 1
                Loop While (unpacker.IsUnpacked() Or Not unpacker.IsUnpacking()) And counter < 100

                For Each item As ImageUnpacker In Me.unpackers
                    pcComplete += item.GetPcComplete()
                Next

                pcComplete = CInt(pcComplete / Me.unpackers.Count)
            End If

            If Not unpacker.IsUnpacked() Then
                Dim objRandomRectangle As Rectangle

                objOriginal = unpacker.GetOriginalClone()
                Me.UpdateTitlePc(pcComplete)
                objRandomRectangle = New Rectangle(0, 0, Me.Random.Next(2, CInt(objOriginal.Width * 0.4F)), Me.Random.Next(2, CInt(objOriginal.Height * 0.4F)))
                objBackgroundImage = New Bitmap(objRandomRectangle.Width, objRandomRectangle.Height)
                objRandomRectangle.X = Me.Random.Next(0, CInt(objOriginal.Width - objRandomRectangle.Width))
                objRandomRectangle.Y = Me.Random.Next(0, CInt(objOriginal.Height - objRandomRectangle.Height))

                objGraphics = Graphics.FromImage(objBackgroundImage)
                objGraphics.DrawImage( _
                                    objOriginal _
                                    , 0 _
                                    , 0 _
                                    , objRandomRectangle _
                                    , GraphicsUnit.Pixel _
                                    )

                Me.pnlMain.BackgroundImage = objBackgroundImage
                Me.Text = Me.FormTitle

                SyncLock (RegionUnpacker.Wait)
                    Threading.Monitor.PulseAll(RegionUnpacker.Wait)
                End SyncLock
            End If
        Catch ex As Exception
            ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk", Me)
        Finally
            If objOriginal IsNot Nothing Then
                objOriginal.Dispose()
            End If

            If objGraphics IsNot Nothing Then
                objGraphics.Dispose()
            End If
        End Try
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean

        If keyData = Keys.Escape Then
            If Me.unpackers.Count = 1 _
            AndAlso Me.AreAllUnpacked() Then
                Me.unpackers.Clear()
                Me.ResetFormPostUnpack(Nothing)
            End If
            Return MyBase.ProcessCmdKey(msg, keyData)
        End If

        Dim objPanelRectangle As Rectangle
        Dim objCursor As Point

        objCursor = Cursor.Position
        objPanelRectangle = Me.pnlMain.ClientRectangle

        objPanelRectangle.X = Me.Left
        objPanelRectangle.Y = Me.Top + (Me.Height - Me.pnlMain.Height - Me.pnlOptions.Height)

        If objPanelRectangle.Contains(objCursor) Then
            Select Case keyData
                Case Keys.Left
                    Cursor.Position = New Point(Cursor.Position.X - 1, Cursor.Position.Y)
                    Return True
                Case Keys.Right
                    Cursor.Position = New Point(Cursor.Position.X + 1, Cursor.Position.Y)
                    Return True
                Case Keys.Up
                    Cursor.Position = New Point(Cursor.Position.X, Cursor.Position.Y - 1)
                    Return True
                Case Keys.Down
                    Cursor.Position = New Point(Cursor.Position.X, Cursor.Position.Y + 1)
                    Return True
            End Select
        End If

        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
#End Region

    Private Sub chkCut_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkCut.CheckedChanged
        If Me.chkCut.Checked Then
            Me.chkCut.Image = My.Resources.cut
        Else
            Me.chkCut.Image = My.Resources.cursor
        End If
    End Sub
End Class
