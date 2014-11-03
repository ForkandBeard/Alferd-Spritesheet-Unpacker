#Region " Options "
Option Explicit On
Option Strict On
#End Region

Imports System.Drawing

Public Class IndexedBitmap : Implements IDisposable
    Private _Bitmap As Bitmap    
    Private _PixelsByColour As Dictionary(Of Color, List(Of Integer))
    Private _PixelsByPoint As Dictionary(Of Point, Color)
    Private _ByteArrayIndexByPoint As Dictionary(Of Point, Integer)
    Private _ImageAsBytes As Byte()
    Private _IsIndexRebuildRequired As Boolean = True    

    Public Sub New(ByVal pobjBitmap As Bitmap)
        If pobjBitmap.PixelFormat <> Imaging.PixelFormat.Format32bppArgb _
        AndAlso pobjBitmap.PixelFormat <> Imaging.PixelFormat.Format24bppRgb Then
            Throw New ArgumentException("Image format not supported.")
        End If
        Me._Bitmap = pobjBitmap
    End Sub

    Public Shared Function IsIndexable(ByVal pobjBitmap As Bitmap) As Boolean
        Return pobjBitmap.PixelFormat = Imaging.PixelFormat.Format32bppArgb _
        OrElse pobjBitmap.PixelFormat = Imaging.PixelFormat.Format24bppRgb
    End Function

    Public Property Bitmap() As Bitmap
        Get
            Return Me._Bitmap
        End Get
        Set(ByVal value As Bitmap)
            Me._Bitmap = value
            Me._IsIndexRebuildRequired = True
        End Set
    End Property

    Public Sub SetPixel(ByVal pobjPoint As Point, ByVal pobjColour As Color)
        Me._Bitmap.SetPixel(pobjPoint.X, pobjPoint.Y, pobjColour)
        Me._IsIndexRebuildRequired = True
    End Sub

    Public Sub RebuildIndexes()
        Dim objReadData As Imaging.BitmapData
        Dim intReadByteLength As Integer
        Dim ptrReadScan0Address As IntPtr
        Dim intColourBytesLength As Integer

        Select Case Me._Bitmap.PixelFormat
            Case Imaging.PixelFormat.Format24bppRgb
                intColourBytesLength = 3
            Case Imaging.PixelFormat.Format32bppArgb
                intColourBytesLength = 4
        End Select

        Me._PixelsByColour = New Dictionary(Of Color, List(Of Integer))
        Me._PixelsByPoint = New Dictionary(Of Point, Color)
        Me._ByteArrayIndexByPoint = New Dictionary(Of Point, Integer)

        objReadData = Me._Bitmap.LockBits(New Rectangle(0, 0, Me._Bitmap.Width, Me._Bitmap.Height), Imaging.ImageLockMode.ReadOnly, Me._Bitmap.PixelFormat)

        intReadByteLength = Math.Abs(objReadData.Stride) * Me._Bitmap.Height
        ptrReadScan0Address = objReadData.Scan0

        Dim arrReadRGBValues(intReadByteLength - 1) As Byte

        System.Runtime.InteropServices.Marshal.Copy(ptrReadScan0Address, arrReadRGBValues, 0, intReadByteLength)

        Me._ImageAsBytes = arrReadRGBValues

        Dim intReadPadding As Integer
        Dim x As Integer = 0
        Dim y As Integer = 0
        Dim intLengthMinusPadding As Integer

        intReadPadding = (objReadData.Stride - (Me._Bitmap.Width * intColourBytesLength)) ' 3 = R,G,B. 4 = A,R,G,B.

        intLengthMinusPadding = ((arrReadRGBValues.Length - 1) - intReadPadding)

        For i As Integer = 0 To arrReadRGBValues.Length - 1 Step intColourBytesLength

            Me.HandleWord(arrReadRGBValues, i, x, y)
            x += 1

            If x >= Me._Bitmap.Width Then
                x = 0
                y += 1
                i += intReadPadding
                If i > intLengthMinusPadding Then
                    Exit For
                End If
            End If
        Next

        Me._Bitmap.UnlockBits(objReadData)
        Me._IsIndexRebuildRequired = False
    End Sub

    Private Sub HandleWord(ByVal parrReadRGBValues() As Byte, ByVal pintCounter As Integer, ByVal x As Integer, ByVal y As Integer)
        Dim objColour As Color

        Select Case Me._Bitmap.PixelFormat
            Case Imaging.PixelFormat.Format32bppArgb
                objColour = Color.FromArgb(parrReadRGBValues(pintCounter + 3), parrReadRGBValues(pintCounter + 2), parrReadRGBValues(pintCounter + 1), parrReadRGBValues(pintCounter))
            Case Imaging.PixelFormat.Format24bppRgb
                objColour = Color.FromArgb(parrReadRGBValues(pintCounter + 2), parrReadRGBValues(pintCounter + 1), parrReadRGBValues(pintCounter))
        End Select

        Me._PixelsByPoint.Add(New Point(x, y), objColour)
        Me._ByteArrayIndexByPoint.Add(New Point(x, y), pintCounter)

        Me.AddPixelByColour(objColour, pintCounter)
    End Sub

    Private Sub AddPixelByColour(ByVal pobjColour As Color, ByVal pintByteIndex As Integer)
        If Not Me._PixelsByColour.ContainsKey(pobjColour) Then
            Me._PixelsByColour.Add(pobjColour, New List(Of Integer))
        End If

        Me._PixelsByColour(pobjColour).Add(pintByteIndex)
    End Sub

    Public Function GetBitmapByteArrayPositionsByColour(ByVal pobjColour As Color) As List(Of Integer)
        If Me._IsIndexRebuildRequired Then
            Me.RebuildIndexes()
        End If

        Return Me._PixelsByColour(pobjColour)
    End Function

    Public Function GetPixelsByColour() As Dictionary(Of Color, List(Of Integer))
        If Me._IsIndexRebuildRequired Then
            Me.RebuildIndexes()
        End If

        Return New Dictionary(Of Color, List(Of Integer))(Me._PixelsByColour)
    End Function

    Public Function GetPixel(ByVal pobjPoint As Point) As Color
        If Me._IsIndexRebuildRequired Then
            Me.RebuildIndexes()
        End If

        Return Me._PixelsByPoint(pobjPoint)
    End Function

    Public Sub SetPixels(ByVal pcolPixels As List(Of Point), ByVal pobjNewColour As Color)
        If Me._IsIndexRebuildRequired Then
            Me.RebuildIndexes()
        End If

        Dim imgNew As Bitmap
        Dim objWriteData As Imaging.BitmapData
        Dim ptrWriteScan0Address As IntPtr
        Dim intWriteByteLength As Integer
        'Dim intPixelByteLength As Integer

        'http://msdn.microsoft.com/en-us/library/system.drawing.imaging.bitmapdata.aspx
        imgNew = New Bitmap(Me._Bitmap.Width, Me._Bitmap.Height, Me._Bitmap.PixelFormat) ', objReadData.Stride * pintScale, pobjImage.PixelFormat, New IntPtr(0))

        objWriteData = imgNew.LockBits(New Rectangle(0, 0, imgNew.Width, imgNew.Height), Imaging.ImageLockMode.WriteOnly, Me._Bitmap.PixelFormat)
        ptrWriteScan0Address = objWriteData.Scan0
        intWriteByteLength = Math.Abs(objWriteData.Stride) * imgNew.Height

        Dim arrWriteRGBValues() As Byte
        Dim colWriteRGBBytes As List(Of Byte) = New List(Of Byte)
        Dim colRead As List(Of Byte)
        Dim intByteIndex As Integer

        colRead = New List(Of Byte)(Me._ImageAsBytes)
        For Each objPoint As Point In pcolPixels
            If Not Me._ByteArrayIndexByPoint.ContainsKey(objPoint) Then
                Throw New ArgumentException(String.Format("Point {0} is out of bounds.", objPoint.ToString))
            End If
            intByteIndex = Me._ByteArrayIndexByPoint(objPoint)

            Select Case Me._Bitmap.PixelFormat
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
        imgNew.UnlockBits(objWriteData)

        Me._Bitmap.Dispose()
        Me._Bitmap = imgNew

        Me._IsIndexRebuildRequired = True
    End Sub

    Public Sub EnsureIndexIsRebuilt()
        Me._IsIndexRebuildRequired = True
    End Sub

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        Dim objOther As IndexedBitmap

        Try
            objOther = CType(obj, IndexedBitmap)
        Catch sorry_no_time As Exception
            Return False
        End Try

        If objOther._ImageAsBytes.Length <> Me._ImageAsBytes.Length Then
            Return False
        Else
            For k As Integer = 0 To Me._ImageAsBytes.Length - 1
                If objOther._ImageAsBytes(k) <> Me._ImageAsBytes(k) Then
                    Return False
                End If
            Next
        End If

        Return True
    End Function

    Private disposedValue As Boolean = False        ' To detect redundant calls
    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free other state (managed objects).
            End If

            ' TODO: free your own state (unmanaged objects).
            If Me._Bitmap IsNot Nothing Then
                Me._Bitmap.Dispose()
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
