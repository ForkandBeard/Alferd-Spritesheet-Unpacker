#Region " Options "
Option Explicit On
Option Strict On
#End Region

Imports System.Drawing

Public Class QuickImageReader : Implements IDisposable

    Private Original As Bitmap
    Private NewImage As Bitmap = Nothing
    Private WriteData As Imaging.BitmapData
    Private ReadData As Imaging.BitmapData
    Private WriteScan0Address As IntPtr
    Private ReadScan0Address As IntPtr
    Private WriteByteLength As Integer
    Private ReadByteLength As Integer
    Private PixelByteLength As Integer
    Private ReadRGBValues As Byte()
    Private ReadPadding As Integer
    Private LengthMinusPadding As Integer

    Private _IsIndexRebuildRequired As Boolean = True
    Private _PixelsByColour As Dictionary(Of Color, List(Of Integer))
    Private _PixelsByPoint As Dictionary(Of Point, Color)
    Private _ByteArrayIndexByPoint As Dictionary(Of Point, Integer)

    Public ReadOnly Property Bitmap() As Bitmap
        Get
            If Me.NewImage Is Nothing Then
                Return Me.Original
            Else
                Return Me.NewImage
            End If
        End Get
    End Property

    Public ReadOnly Property OriginalBitmap() As Bitmap
        Get
            Return Me.Original
        End Get
    End Property

    Public Sub New(ByVal pobjImage As Bitmap)
        Select Case pobjImage.PixelFormat
            Case Imaging.PixelFormat.Format24bppRgb
                Me.PixelByteLength = 3
            Case Imaging.PixelFormat.Format32bppArgb
                Me.PixelByteLength = 4
            Case Else
                Throw New Exception("Format not supported.")
        End Select

        Me.Original = pobjImage
    End Sub

    Private Sub LoadInstanceData()
        Me._PixelsByColour = New Dictionary(Of Color, List(Of Integer))
        Me._PixelsByPoint = New Dictionary(Of Point, Color)
        Me._ByteArrayIndexByPoint = New Dictionary(Of Point, Integer)

        Me.ReadData = Me.Original.LockBits(New Rectangle(0, 0, Me.Original.Width, Me.Original.Height), Imaging.ImageLockMode.ReadOnly, Me.Original.PixelFormat)

        'http://msdn.microsoft.com/en-us/library/system.drawing.imaging.bitmapdata.aspx
        Me.NewImage = New Bitmap(Me.Original.Width, Me.Original.Height, Me.Original.PixelFormat) ', objReadData.Stride * pintScale, pobjImage.PixelFormat, New IntPtr(0))

        Me.WriteData = Me.NewImage.LockBits(New Rectangle(0, 0, Me.NewImage.Width, Me.NewImage.Height), Imaging.ImageLockMode.WriteOnly, Me.Original.PixelFormat)

        Me.WriteScan0Address = Me.WriteData.Scan0
        Me.ReadScan0Address = Me.ReadData.Scan0

        Me.WriteByteLength = Math.Abs(Me.WriteData.Stride) * Me.NewImage.Height
        Me.ReadByteLength = Math.Abs(Me.ReadData.Stride) * Me.Original.Height

        ReDim Me.ReadRGBValues(Me.ReadByteLength - 1)

        System.Runtime.InteropServices.Marshal.Copy(Me.ReadScan0Address, Me.ReadRGBValues, 0, Me.ReadByteLength)

        Me.ReadPadding = (Me.ReadData.Stride - (Me.Original.Width * Me.PixelByteLength))  ' 3 = R,G,B. 4 = A,R,G,B.
        Me.LengthMinusPadding = ((Me.ReadRGBValues.Length - 1) - Me.ReadPadding)
    End Sub

    Private Sub RebuildIndexes()
        Me.LoadInstanceData()

        Dim objColour As Color
        Dim x As Integer = 0
        Dim y As Integer = 0

        For i As Integer = 0 To Me.ReadRGBValues.Length - 1 Step Me.PixelByteLength

            Select Case Me.Original.PixelFormat
                Case Imaging.PixelFormat.Format32bppArgb
                    objColour = Color.FromArgb(Me.ReadRGBValues(i + 3), Me.ReadRGBValues(i + 2), Me.ReadRGBValues(i + 1), Me.ReadRGBValues(i))
                Case Imaging.PixelFormat.Format24bppRgb
                    objColour = Color.FromArgb(Me.ReadRGBValues(i + 2), Me.ReadRGBValues(i + 1), Me.ReadRGBValues(i))
            End Select

            Me._PixelsByPoint.Add(New Point(x, y), objColour)
            Me._ByteArrayIndexByPoint.Add(New Point(x, y), i)

            If Not Me._PixelsByColour.ContainsKey(objColour) Then
                Me._PixelsByColour.Add(objColour, New List(Of Integer))
            End If

            Me._PixelsByColour(objColour).Add(i)

            x += 1

            If x >= Me.Original.Width Then
                x = 0
                y += 1
                i += Me.ReadPadding
                If i > Me.LengthMinusPadding Then
                    Exit For
                End If
            End If
        Next

        Me.Original.UnlockBits(Me.ReadData)

        Me._IsIndexRebuildRequired = False
    End Sub

    Public Sub ApplyOperation(ByVal pobjOperation As IQuickColourOperation, ByVal pblnReplaceOriginal As Boolean)
        Me.LoadInstanceData()

        Dim arrWriteRGBValues() As Byte
        Dim colWriteRGBBytes As List(Of Byte) = New List(Of Byte)
        Dim colStrideBytes As List(Of Byte) = New List(Of Byte)
        Dim objOldColour As Color
        Dim objNewColour As Color
        Dim x As Integer = 0
        Dim y As Integer = 0

        For i As Integer = 0 To Me.ReadRGBValues.Length - 1 Step Me.PixelByteLength
            Select Case Me.Original.PixelFormat
                Case Imaging.PixelFormat.Format32bppArgb
                    objOldColour = Color.FromArgb(Me.ReadRGBValues(i + 3), Me.ReadRGBValues(i + 2), Me.ReadRGBValues(i + 1), Me.ReadRGBValues(i))
                Case Imaging.PixelFormat.Format24bppRgb
                    objOldColour = Color.FromArgb(Me.ReadRGBValues(i + 2), Me.ReadRGBValues(i + 1), Me.ReadRGBValues(i))
            End Select

            objNewColour = pobjOperation.CalculateNewColour(objOldColour, x, y)

            Me._PixelsByPoint.Add(New Point(x, y), objNewColour)
            Me._ByteArrayIndexByPoint.Add(New Point(x, y), i)

            If Not Me._PixelsByColour.ContainsKey(objNewColour) Then
                Me._PixelsByColour.Add(objNewColour, New List(Of Integer))
            End If

            Me._PixelsByColour(objNewColour).Add(i)

            colStrideBytes.Add(objNewColour.B)
            colStrideBytes.Add(objNewColour.G)
            colStrideBytes.Add(objNewColour.R)
            If Me.Original.PixelFormat = Imaging.PixelFormat.Format32bppArgb Then
                colStrideBytes.Add(objNewColour.A)
            End If

            ' Strides are rounded up to four bytes...
            If Math.Ceiling(colStrideBytes.Count / 4) = (Me.WriteData.Stride / 4) Then

                If colStrideBytes.Count <> Me.WriteData.Stride Then
                    ' ... so pad short rows.
                    For k As Integer = 0 To (Me.WriteData.Stride - colStrideBytes.Count) - 1
                        colStrideBytes.Add(0)
                    Next
                End If

                colWriteRGBBytes.AddRange(colStrideBytes)
                colStrideBytes = New List(Of Byte)
                ' Read strides are also rounded up to four bytes, so skip the padded bytes.
                i += Me.ReadPadding
                x = 0
                y += 1
            Else
                x += 1
            End If
        Next

        arrWriteRGBValues = colWriteRGBBytes.ToArray

        System.Runtime.InteropServices.Marshal.Copy(arrWriteRGBValues, 0, Me.WriteScan0Address, Me.WriteByteLength)

        ' Unlock the bits.
        Me.NewImage.UnlockBits(Me.WriteData)
        Me.Original.UnlockBits(Me.ReadData)

        If pblnReplaceOriginal Then
            Me.Original = New Bitmap(Me.NewImage)
        End If
    End Sub

    Public Sub SetPixels(ByVal pcolPixels As List(Of Point), ByVal pobjNewColour As Color, ByVal pblnReplaceOriginal As Boolean)
        If Me._IsIndexRebuildRequired Then
            Me.RebuildIndexes()
        End If

        Dim objWriteData As Imaging.BitmapData
        Dim ptrWriteScan0Address As IntPtr
        Dim intWriteByteLength As Integer
        'Dim intPixelByteLength As Integer

        'http://msdn.microsoft.com/en-us/library/system.drawing.imaging.bitmapdata.aspx
        Me.NewImage = New Bitmap(Me.Original.Width, Me.Original.Height, Me.Original.PixelFormat)

        objWriteData = Me.NewImage.LockBits(New Rectangle(0, 0, Me.NewImage.Width, Me.NewImage.Height), Imaging.ImageLockMode.WriteOnly, Me.Original.PixelFormat)
        ptrWriteScan0Address = objWriteData.Scan0
        intWriteByteLength = Math.Abs(objWriteData.Stride) * Me.NewImage.Height

        Dim arrWriteRGBValues() As Byte
        Dim colWriteRGBBytes As List(Of Byte) = New List(Of Byte)
        Dim colRead As List(Of Byte)
        Dim intByteIndex As Integer

        colRead = New List(Of Byte)(Me.ReadRGBValues)
        For Each objPoint As Point In pcolPixels
            If Not Me._ByteArrayIndexByPoint.ContainsKey(objPoint) Then
                Throw New ArgumentException(String.Format("Point {0} is out of bounds.", objPoint.ToString))
            End If
            intByteIndex = Me._ByteArrayIndexByPoint(objPoint)

            Select Case Me.Original.PixelFormat
                Case Imaging.PixelFormat.Format24bppRgb
                    colRead(intByteIndex + 2) = pobjNewColour.R
                    colRead(intByteIndex + 1) = pobjNewColour.G
                    colRead(intByteIndex) = pobjNewColour.B
                Case Imaging.PixelFormat.Format32bppArgb
                    colRead(intByteIndex + 3) = pobjNewColour.A
                    colRead(intByteIndex + 2) = pobjNewColour.R
                    colRead(intByteIndex + 1) = pobjNewColour.G
                    colRead(intByteIndex) = pobjNewColour.B
            End Select
        Next

        arrWriteRGBValues = colRead.ToArray

        System.Runtime.InteropServices.Marshal.Copy(arrWriteRGBValues, 0, ptrWriteScan0Address, intWriteByteLength)

        ' Unlock the bits.
        Me.NewImage.UnlockBits(objWriteData)

        If pblnReplaceOriginal Then
            Me.Original = New Bitmap(Me.NewImage)
        End If

        Me._IsIndexRebuildRequired = True
    End Sub

    Public Sub SetPixel(ByVal pobjPoint As Point, ByVal pobjColour As Color)
        Me.Original.SetPixel(pobjPoint.X, pobjPoint.Y, pobjColour)
        Me.NewImage = New Bitmap(Me.Original)
        Me._IsIndexRebuildRequired = True
    End Sub

    Public Function GetBitmapByteArrayPositionsByColour(ByVal pobjColour As Color) As List(Of Integer)
        If Me._IsIndexRebuildRequired Then
            Me.RebuildIndexes()
        End If

        Return Me._PixelsByColour(pobjColour)
    End Function

    Public Function GetPixel(ByVal pobjPoint As Point) As Color
        If Me._IsIndexRebuildRequired Then
            Me.RebuildIndexes()
        End If

        Return Me._PixelsByPoint(pobjPoint)
    End Function

    Public Interface IQuickColourOperation
        Function CalculateNewColour(ByVal pobjOriginal As Color, ByVal pintX As Integer, ByVal pintY As Integer) As Color
        Sub RaiseExceptionIfInvalidForOperation(ByVal pobjImage As Bitmap)
    End Interface

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free other state (managed objects).
            End If

            ' TODO: free your own state (unmanaged objects).
            If Me.Original IsNot Nothing Then
                Me.Original.Dispose()
            End If

            If Me.NewImage IsNot Nothing Then
                Me.NewImage.Dispose()
            End If
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
