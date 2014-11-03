#Region " Options "
Option Strict On
Option Explicit On
#End Region

Public Class ImageUnpacker
    Private original As Bitmap
    Private originalLock As Object = New Object()
    Private pcComplete As Integer = 0
    Private backgroundColour As Color? = Nothing
    Private boxesLock As Object = New Object()
    Private boxes As List(Of Rectangle)
    Private areaUnpacked As Integer
    Private areaUnpackedLock As Object = New Object()
    Private areaToUnpack As Integer
    Private threadCounter As Integer = 0
    Private threadCompleteCounterLock As Object = New Object()
    Private threadCompleteCounter As Integer = 0
    Private areAllThreadsCreated As Boolean
    Private isUnpackingComplete As Boolean = False
    Private originalSize As Size
    Private pallette As Imaging.ColorPalette = Nothing
    Private _isBackgroundColourSet As Boolean = False
    Private _isUnpacking As Boolean = False
    Private Const INT_MAX_REGION_WIDTH As Integer = 400    
    Public FileName As String
    Public ColoursCount As Integer = 0

    Public Event UnpackingComplete()
    Public Event PcCompleteChanged(ByVal pcComplete As Integer)

    Public Sub New(ByVal image As Bitmap, ByVal fileName As String)
        If image.Palette.Entries.Length > 0 Then
            Me.pallette = image.Palette
        End If
        Me.original = New Bitmap(CType(image.Clone, Bitmap))
        Me.originalSize = image.Size
        Me.boxes = New List(Of Rectangle)
        Me.FileName = fileName
    End Sub

    Public Function GetPallette() As Imaging.ColorPalette
        Return Me.pallette
    End Function

    Public Function GetSize() As Size
        Return Me.originalSize
    End Function

    Public Function IsUnpacking() As Boolean
        Return Me._isUnpacking
    End Function

    Public Function IsBackgroundColourSet() As Boolean
        Return Me._isBackgroundColourSet
    End Function

    Public Function GetBackgroundColour() As Color
        If Me.backgroundColour.HasValue Then
            Return Me.backgroundColour.Value
        Else
            Return Color.Black
        End If
    End Function

    Public Function GetBoxes() As List(Of Rectangle)
        Return New List(Of Rectangle)(Me.boxes)
    End Function

    Public Function IsUnpacked() As Boolean
        Return Me.isUnpackingComplete
    End Function

    Public Function GetPcComplete() As Integer
        Return Me.pcComplete
    End Function

    Public Function GetOriginalClone() As Bitmap
        Dim clone As Bitmap

        SyncLock (Me.originalLock)
            clone = New Bitmap(CType(Me.original.Clone(), Bitmap))
        End SyncLock

        Return clone
    End Function

    Private Sub SetPcComplete(ByVal pcComplete As Integer)
        If pcComplete > Me.pcComplete Then
            Me.pcComplete = pcComplete
            RaiseEvent PcCompleteChanged(Me.pcComplete)
        End If
    End Sub

    Public Sub StartUnpacking()
        Dim newThread As Threading.Thread = New Threading.Thread(AddressOf Me.Unpack)
        Me.isUnpackingComplete = False
        Me.pcComplete = 0
        Me.boxes.Clear()
        Me._isUnpacking = True
        Me.threadCounter = 0
        Me.threadCompleteCounter = 0
        newThread.Start()
    End Sub

    Public Shared Function OrderBoxes(ByVal boxes As List(Of Rectangle), ByVal enuSelectAllOrder As SelectAllOrder, ByVal spriteSheetSize As Size) As List(Of Rectangle)
        Dim colOrderedFrames As SortedDictionary(Of Integer, List(Of Rectangle)) = New SortedDictionary(Of Integer, List(Of Rectangle))
        Dim intLocation As Integer
        Dim returnedOrder As List(Of Rectangle) = New List(Of Rectangle)

        For Each objFrame As Rectangle In boxes

            Select Case enuSelectAllOrder
                Case SelectAllOrder.TopLeft
                    intLocation = objFrame.X + (objFrame.Y * spriteSheetSize.Width)
                Case SelectAllOrder.BottomLeft
                    intLocation = objFrame.X + ((objFrame.Y + objFrame.Height) * spriteSheetSize.Width)
                Case SelectAllOrder.Centre
                    intLocation = CInt((objFrame.X + (objFrame.Width / 2)) + ((objFrame.Y + (objFrame.Height / 2)) * spriteSheetSize.Width))
            End Select

            If Not colOrderedFrames.ContainsKey(intLocation) Then
                colOrderedFrames.Add(intLocation, New List(Of Rectangle))
            End If
            colOrderedFrames(intLocation).Add(objFrame)
        Next

        For Each intLocationKey As Integer In colOrderedFrames.Keys
            For Each objFrame As Rectangle In colOrderedFrames(intLocationKey)
                returnedOrder.Add(objFrame)
            Next
        Next

        Return returnedOrder
    End Function

    Private Sub Unpack(ByVal state As Object)
        Try
            Dim intXSize As Integer
            Dim intYSize As Integer
            Dim region As Rectangle
            Dim regionThread As Threading.Thread

            Me.areAllThreadsCreated = False
            If Not Me.backgroundColour.HasValue Then
                Me.SetBackgroundColour(Me.GetOriginalClone())
            End If
            Me.SetPcComplete(10)

            intYSize = CInt(Math.Ceiling(Me.originalSize.Height / 3))
            intXSize = CInt(Math.Ceiling(Me.originalSize.Width / 3))

            Me.areaToUnpack = Me.originalSize.Width * Me.originalSize.Height

            For y As Integer = 0 To intYSize * 4 Step intYSize
                For x As Integer = 0 To intXSize * 4 Step intXSize

                    region = New Rectangle(x, y, Math.Min(intXSize, (Me.originalSize.Width - x) - 1), Math.Min(intYSize, (Me.originalSize.Height - y) - 1))
                    regionThread = New Threading.Thread(AddressOf Me.HandleDividedAreaThread)
                    regionThread.Name = "Region thread " & (y * (intXSize * 4)) + x
                    Me.threadCounter += 1
                    regionThread.Start(region)

                    Me.SetPcComplete(10 + CInt((((Math.Min(y, Me.originalSize.Height) * (Me.originalSize.Width - 1)) + Math.Min(x, Me.originalSize.Width)) / (Me.originalSize.Height * Me.originalSize.Width)) * 10))
                Next
            Next

            Me.SetPcComplete(20)
            Me.areAllThreadsCreated = True
            SyncLock (Me.threadCompleteCounterLock)
                If Me.threadCompleteCounter = Me.threadCounter Then
                    Me.HandleUnpackComplete()
                End If
            End SyncLock
        Catch ex As Exception
            ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk")
        End Try
    End Sub

    Private Sub HandleDividedAreaThread(ByVal regionObject As Object)
        Try
            Me.HandleDividedArea(CType(regionObject, Rectangle), True, Me.GetOriginalClone())
        Catch ex As Exception
            ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk")
        End Try
    End Sub

    Private Sub HandleDividedArea(ByVal region As Rectangle, ByVal updateCounter As Boolean, ByVal image As Bitmap)

        If region.Width > INT_MAX_REGION_WIDTH _
        OrElse region.Height > INT_MAX_REGION_WIDTH Then
            Dim quarterRegions As List(Of Rectangle) = New List(Of Rectangle)

            ' Top left.
            quarterRegions.Add(New Rectangle(region.X, region.Y, CInt(region.Width / 2), CInt(region.Height / 2)))
            ' Top right.
            quarterRegions.Add(New Rectangle(region.X + CInt(region.Width / 2), region.Y, CInt(region.Width / 2) + 1, CInt(region.Height / 2)))
            ' Bottom left.
            quarterRegions.Add(New Rectangle(region.X, region.Y + CInt(region.Height / 2), CInt(region.Width / 2), CInt(region.Height / 2) + 1))
            ' Bottom right.
            quarterRegions.Add(New Rectangle(region.X + CInt(region.Width / 2), region.Y + CInt(region.Height / 2), CInt(region.Width / 2) + 1, CInt(region.Height / 2) + 1))
            For Each quarter As Rectangle In quarterRegions
                Me.HandleDividedArea(quarter, False, image)
            Next
        Else
            Using unpacker As RegionUnpacker = New RegionUnpacker(image, region, Me.backgroundColour.Value)
                unpacker.UnpackRegion()
                SyncLock (Me.areaUnpackedLock)
                    Me.areaUnpacked += CInt((region.Width * region.Height) * 0.8)
                End SyncLock

                Me.SetPcComplete(20 + CInt((Me.areaUnpacked / Me.areaToUnpack) * 75))

                SyncLock (Me.boxesLock)
                    Me.boxes.AddRange(unpacker.Boxes)
                    'RegionUnpacker.CombineBoxes(Me.boxes, Me.backgroundColour.Value, unpacker.GetImage())
                End SyncLock
            End Using

            SyncLock (Me.areaUnpackedLock)
                Me.areaUnpacked += CInt((region.Width * region.Height) * 0.2)
            End SyncLock

            Me.SetPcComplete(20 + CInt((Me.areaUnpacked / Me.areaToUnpack) * 80))
        End If

        If updateCounter Then
            SyncLock (Me.threadCompleteCounterLock)
                Me.threadCompleteCounter += 1
                If Me.areAllThreadsCreated _
                AndAlso Me.threadCompleteCounter = Me.threadCounter Then
                    Me.HandleUnpackComplete()
                End If
            End SyncLock
        End If
    End Sub

    Private Sub SetBackgroundColour(ByVal image As Bitmap)
        Dim colours As Dictionary(Of Color, Integer) = New Dictionary(Of Color, Integer)
        Dim presentColour As Color
        Dim maxCount As Integer = 0

        For x As Integer = 0 To Me.originalSize.Width - 1
            For y As Integer = 0 To Me.originalSize.Height - 1
                presentColour = image.GetPixel(x, y)

                If Not colours.ContainsKey(presentColour) Then
                    colours.Add(presentColour, 1)
                Else
                    colours(presentColour) += 1
                End If

                If (x + y) Mod 100 = 0 Then
                    Dim intTotal As Integer

                    intTotal = Me.originalSize.Width * Me.originalSize.Height
                    Me.SetPcComplete(CInt((((x * (Me.originalSize.Height - 1)) + y) / intTotal) * 10))
                End If
            Next
        Next

        For Each colour As Color In colours.Keys
            If colours(colour) >= maxCount Then
                maxCount = colours(colour)
                Me.backgroundColour = colour
            End If
        Next
        image.Dispose()
        Me._isBackgroundColourSet = True
        Me.ColoursCount = colours.Count - 1
    End Sub

    Private Sub HandleUnpackComplete()
        Using img As Bitmap = Me.GetOriginalClone()
            SyncLock (Me.boxesLock)
                RegionUnpacker.CombineBoxes(Me.boxes, Me.backgroundColour.Value, img)
            End SyncLock
        End Using

        Me.isUnpackingComplete = True
        Me._isUnpacking = False
        Me.SetPcComplete(100)
        RaiseEvent UnpackingComplete()
    End Sub
End Class
