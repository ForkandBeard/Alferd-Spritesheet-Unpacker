#Region " Options "
Option Explicit On
Option Strict On
#End Region

Imports System.Drawing

Public Class Filters

    Public Shared Function BlitOr(ByVal pobjImage1 As Bitmap, ByVal pobjImage2 As Bitmap, ByVal pobjThreshold As Byte) As Bitmap
        Dim objReturn As Bitmap = New Bitmap(Math.Min(pobjImage1.Width, pobjImage2.Width), Math.Min(pobjImage1.Height, pobjImage2.Height))
        Dim idxImage1 As IndexedBitmap
        Dim idxImage2 As IndexedBitmap
        Dim obj1Colour As Color
        Dim obj2Colour As Color
        Dim objNewColour As Color

        Dim intR As Integer
        Dim intG As Integer
        Dim intB As Integer

        idxImage1 = New IndexedBitmap(pobjImage1)
        idxImage2 = New IndexedBitmap(pobjImage2)

        For y As Integer = 0 To Math.Min(pobjImage1.Height - 1, pobjImage2.Height - 1)
            For x As Integer = 0 To Math.Min(pobjImage1.Width - 1, pobjImage2.Width - 1)
                obj1Colour = idxImage1.GetPixel(New Point(x, y)) 'pobjImage1.GetPixel(x, y)
                obj2Colour = idxImage2.GetPixel(New Point(x, y)) 'pobjImage2.GetPixel(x, y)

                If obj1Colour.R > pobjThreshold OrElse obj2Colour.R > pobjThreshold Then
                    intR = 255
                Else
                    intR = 0
                End If

                If obj1Colour.G > pobjThreshold OrElse obj2Colour.G > pobjThreshold Then
                    intG = 255
                Else
                    intG = 0
                End If

                If obj1Colour.B > pobjThreshold OrElse obj2Colour.B > pobjThreshold Then
                    intB = 255
                Else
                    intB = 0
                End If

                objNewColour = Color.FromArgb(intR, intG, intB)
                objReturn.SetPixel(x, y, objNewColour)
            Next
        Next

        Return objReturn
    End Function

    Public Shared Function BlitAnd(ByVal pobjImage1 As Bitmap, ByVal pobjImage2 As Bitmap, ByVal pobjThreshold As Byte) As Bitmap
        Dim objReturn As Bitmap = New Bitmap(Math.Min(pobjImage1.Width, pobjImage2.Width), Math.Min(pobjImage1.Height, pobjImage2.Height))
        Dim idxImage1 As IndexedBitmap
        Dim idxImage2 As IndexedBitmap
        Dim obj1Colour As Color
        Dim obj2Colour As Color
        Dim objNewColour As Color

        Dim intR As Integer
        Dim intG As Integer
        Dim intB As Integer

        idxImage1 = New IndexedBitmap(pobjImage1)
        idxImage2 = New IndexedBitmap(pobjImage2)

        For y As Integer = 0 To Math.Min(pobjImage1.Height - 1, pobjImage2.Height - 1)
            For x As Integer = 0 To Math.Min(pobjImage1.Width - 1, pobjImage2.Width - 1)
                obj1Colour = idxImage1.GetPixel(New Point(x, y)) 'pobjImage1.GetPixel(x, y)
                obj2Colour = idxImage2.GetPixel(New Point(x, y)) 'pobjImage2.GetPixel(x, y)

                If obj1Colour.R > pobjThreshold AndAlso obj2Colour.R > pobjThreshold Then
                    intR = 255
                Else
                    intR = 0
                End If

                If obj1Colour.G > pobjThreshold AndAlso obj2Colour.G > pobjThreshold Then
                    intG = 255
                Else
                    intG = 0
                End If

                If obj1Colour.B > pobjThreshold AndAlso obj2Colour.B > pobjThreshold Then
                    intB = 255
                Else
                    intB = 0
                End If

                objNewColour = Color.FromArgb(intR, intG, intB)
                objReturn.SetPixel(x, y, objNewColour)
            Next
        Next

        Return objReturn
    End Function

    Public Shared Function AddTwoImages(ByVal pobjImage1 As Bitmap, ByVal pobjImage2 As Bitmap) As Bitmap
        Dim objReturn As Bitmap = New Bitmap(Math.Min(pobjImage1.Width, pobjImage2.Width), Math.Min(pobjImage1.Height, pobjImage2.Height))
        Dim idxImage1 As IndexedBitmap
        Dim idxImage2 As IndexedBitmap
        Dim obj1Colour As Color
        Dim obj2Colour As Color
        Dim objNewColour As Color

        Dim intR As Integer
        Dim intG As Integer
        Dim intB As Integer

        idxImage1 = New IndexedBitmap(pobjImage1)
        idxImage2 = New IndexedBitmap(pobjImage2)

        For y As Integer = 0 To Math.Min(pobjImage1.Height - 1, pobjImage2.Height - 1)
            For x As Integer = 0 To Math.Min(pobjImage1.Width - 1, pobjImage2.Width - 1)
                obj1Colour = idxImage1.GetPixel(New Point(x, y)) 'pobjImage1.GetPixel(x, y)
                obj2Colour = idxImage2.GetPixel(New Point(x, y)) 'pobjImage2.GetPixel(x, y)

                intR = Math.Max(Math.Min(CInt(obj1Colour.R) + CInt(obj2Colour.R), 255), 0)
                intG = Math.Max(Math.Min(CInt(obj1Colour.G) + CInt(obj2Colour.G), 255), 0)
                intB = Math.Max(Math.Min(CInt(obj1Colour.B) + CInt(obj2Colour.B), 255), 0)

                objNewColour = Color.FromArgb(intR, intG, intB)
                objReturn.SetPixel(x, y, objNewColour)
            Next
        Next

        Return objReturn
    End Function

    Public Shared Function SubtractImage(ByVal pobjImage1 As Bitmap, ByVal pobjImage2 As Bitmap) As Bitmap
        Dim objReturn As Bitmap = New Bitmap(Math.Min(pobjImage1.Width, pobjImage2.Width), Math.Min(pobjImage1.Height, pobjImage2.Height))
        Dim idxImage1 As IndexedBitmap
        Dim idxImage2 As IndexedBitmap
        Dim obj1Colour As Color
        Dim obj2Colour As Color
        Dim objNewColour As Color

        Dim intR As Integer
        Dim intG As Integer
        Dim intB As Integer

        idxImage1 = New IndexedBitmap(pobjImage1)
        idxImage2 = New IndexedBitmap(pobjImage2)

        For y As Integer = 0 To Math.Min(pobjImage1.Height - 1, pobjImage2.Height - 1)
            For x As Integer = 0 To Math.Min(pobjImage1.Width - 1, pobjImage2.Width - 1)
                obj1Colour = idxImage1.GetPixel(New Point(x, y)) 'pobjImage1.GetPixel(x, y)
                obj2Colour = idxImage2.GetPixel(New Point(x, y)) 'pobjImage2.GetPixel(x, y)

                intR = Math.Max(Math.Min(CInt(obj1Colour.R) - CInt(obj2Colour.R), 255), 0)
                intG = Math.Max(Math.Min(CInt(obj1Colour.G) - CInt(obj2Colour.G), 255), 0)
                intB = Math.Max(Math.Min(CInt(obj1Colour.B) - CInt(obj2Colour.B), 255), 0)

                objNewColour = Color.FromArgb(intR, intG, intB)
                objReturn.SetPixel(x, y, objNewColour)
            Next
        Next

        Return objReturn
    End Function

    Public Shared Function SetAlpha(ByVal pobjImage As Bitmap, ByVal pobjAlpha As Byte) As Bitmap
        Dim objOperator As ColourAlphaSetter
        Dim objReturn As Bitmap

        objOperator = New ColourAlphaSetter(pobjAlpha)
        Using out As QuickImageReader = New QuickImageReader(pobjImage)
            out.ApplyOperation(objOperator, True)
            objReturn = New Bitmap(out.Bitmap)
        End Using

        Return objReturn
    End Function

    Public Shared Function SwapColours(ByVal pobjImage As Bitmap, ByVal pcolOriginalKeyToNewValue As Dictionary(Of Color, Color)) As Bitmap
        Dim objOperator As ColourSwapper
        Dim objReturn As Bitmap

        objOperator = New ColourSwapper(pcolOriginalKeyToNewValue)
        Using out As QuickImageReader = New QuickImageReader(pobjImage)
            out.ApplyOperation(objOperator, True)
            objReturn = New Bitmap(out.Bitmap)
        End Using

        Return objReturn
    End Function

    Public Shared Function FlattenColour(ByVal pobjImage As Bitmap, ByVal penuMode As ColourFlattener.FlattenMode, ByVal pintGrain As Integer) As Bitmap
        Dim objOperator As ColourFlattener
        Dim objReturn As Bitmap
        Dim objReturnedIndexed As IndexedBitmap = Nothing
        Dim objOriginalIndexed As IndexedBitmap = Nothing

        objOperator = New ColourFlattener(penuMode, pintGrain)
        Using out As QuickImageReader = New QuickImageReader(pobjImage)
            out.ApplyOperation(objOperator, True)
            objReturn = New Bitmap(CType(out.Bitmap.Clone(), Bitmap))
        End Using

        objReturnedIndexed = New IndexedBitmap(objReturn)
        objOriginalIndexed = New IndexedBitmap(pobjImage)

        Dim sngClosest As Single
        Dim sngDistance As Single
        Dim objBestColour As Color
        Dim colSwap As Dictionary(Of Color, Color) = New Dictionary(Of Color, Color)
        Dim colOrigColours As Dictionary(Of Color, List(Of Integer)).KeyCollection
        Dim colReturnedColours As Dictionary(Of Color, List(Of Integer)).KeyCollection

        'ReindexImagesInSeperateThreads(New List(Of IndexedBitmap)({objReturnedIndexed, objOriginalIndexed}))

        colOrigColours = objOriginalIndexed.GetPixelsByColour().Keys
        colReturnedColours = objReturnedIndexed.GetPixelsByColour().Keys
        For Each objFlat As Color In colReturnedColours
            sngClosest = Single.MaxValue
            For Each objOrig As Color In colOrigColours
                sngDistance = ForkandBeard.Util.Geometry.TrigHelper.GetRelativeDistanceBetweenPoints(New Point(objFlat.R + 1, objFlat.G + 1), New Point(objOrig.R + 1, objOrig.G + 1), objFlat.B + 1, objOrig.B + 1) ' +1 to prevent  *0.

                If sngDistance < sngClosest Then
                    sngClosest = sngDistance
                    objBestColour = objOrig
                End If
            Next
            colSwap.Add(objFlat, objBestColour)
        Next

        objReturn = SwapColours(objReturn, colSwap)

        Return objReturn
    End Function

    Public Shared Function DecreaseScale(ByVal pobjImage As Bitmap, ByVal pintXPixelsPerPixel As Integer, ByVal pintYPixelsPerPixel As Integer) As Bitmap
        Dim objInxBitmap As IndexedBitmap
        Dim intXCounter As Integer
        Dim intYCounter As Integer
        Dim colColours As Dictionary(Of Color, Integer)
        Dim x As Integer = 0
        Dim y As Integer = 0
        Dim objColour As Color
        Dim objReturn As Bitmap = New Bitmap(CInt(pobjImage.Width / pintXPixelsPerPixel), CInt(pobjImage.Height / pintYPixelsPerPixel))
        Dim intNewX As Integer = 0
        Dim intNewY As Integer = 0

        objInxBitmap = New IndexedBitmap(pobjImage)

        Dim intMostFrequent As Integer
        Dim objMostFrequent As Color
        Dim debug As Integer = 0
        Do
            intXCounter = 0
            intYCounter = 0
            colColours = New Dictionary(Of Color, Integer)
            Do
                objColour = objInxBitmap.GetPixel(New Point(x + intXCounter, y + intYCounter))
                debug += 1
                If Not colColours.ContainsKey(objColour) Then
                    colColours.Add(objColour, 0)
                End If
                colColours(objColour) += 1
                intXCounter += 1
                If intXCounter >= (pintXPixelsPerPixel - 1) Then
                    intXCounter = 0
                    intYCounter += 1
                End If
            Loop While intYCounter < pintYPixelsPerPixel _
                AndAlso ((x + intXCounter) < pobjImage.Width) _
                AndAlso ((y + intYCounter) < pobjImage.Height)

            intMostFrequent = -1

            For Each c As Color In colColours.Keys
                ' TODO: Replace this hack of a 254 which is ignored by decrease scale logic.
                If intMostFrequent = -1 Then
                    objMostFrequent = c
                End If
                If c.A <> 254 Then
                    If colColours(c) > intMostFrequent Then
                        intMostFrequent = colColours(c)
                        objMostFrequent = c
                    End If
                End If
            Next

            objReturn.SetPixel(intNewX, intNewY, objMostFrequent)
            intNewX += 1
            If intNewX >= objReturn.Width Then
                intNewX = 0
                intNewY += 1
            End If

            x += pintXPixelsPerPixel
            If x >= pobjImage.Width Then
                y += pintYPixelsPerPixel
                x = 0
            End If
        Loop While y < pobjImage.Height _
            AndAlso x < pobjImage.Width _
            AndAlso intNewY < objReturn.Height

        Return objReturn
    End Function

    Public Shared Function IncreaseScale(ByVal pobjImage As Bitmap, ByVal pintScale As Integer) As Bitmap
        Dim imgNew As Bitmap
        Dim objWriteData As Imaging.BitmapData
        Dim objReadData As Imaging.BitmapData
        Dim ptrWriteScan0Address As IntPtr
        Dim ptrReadScan0Address As IntPtr
        Dim intWriteByteLength As Integer
        Dim intReadByteLength As Integer
        Dim intPixelByteLength As Integer

        Select Case pobjImage.PixelFormat
            Case Imaging.PixelFormat.Format24bppRgb
                intPixelByteLength = 3
            Case Imaging.PixelFormat.Format32bppArgb
                intPixelByteLength = 4
            Case Else
                Throw New Exception("Format not supported.")
        End Select

        objReadData = pobjImage.LockBits(New Rectangle(0, 0, pobjImage.Width, pobjImage.Height), Imaging.ImageLockMode.ReadOnly, pobjImage.PixelFormat)

        'http://msdn.microsoft.com/en-us/library/system.drawing.imaging.bitmapdata.aspx
        imgNew = New Bitmap(CInt(pobjImage.Width * pintScale), CInt(pobjImage.Height * pintScale), pobjImage.PixelFormat) ', objReadData.Stride * pintScale, pobjImage.PixelFormat, New IntPtr(0))

        objWriteData = imgNew.LockBits(New Rectangle(0, 0, imgNew.Width, imgNew.Height), Imaging.ImageLockMode.WriteOnly, pobjImage.PixelFormat)

        ptrWriteScan0Address = objWriteData.Scan0
        ptrReadScan0Address = objReadData.Scan0

        intWriteByteLength = Math.Abs(objWriteData.Stride) * imgNew.Height
        intReadByteLength = Math.Abs(objReadData.Stride) * pobjImage.Height

        Dim arrWriteRGBValues() As Byte '(intWriteByteLength - 1) As Byte
        Dim colWriteRGBBytes As List(Of Byte) = New List(Of Byte)
        Dim arrReadRGBValues(intReadByteLength - 1) As Byte
        Dim intReadPadding As Integer

        System.Runtime.InteropServices.Marshal.Copy(ptrReadScan0Address, arrReadRGBValues, 0, intReadByteLength)

        intReadPadding = (objReadData.Stride - (pobjImage.Width * intPixelByteLength)) ' 3 = R,G,B. 4 = A,R,G,B.
        Dim colStrideBytes As List(Of Byte) = New List(Of Byte)

        For i As Integer = 0 To arrReadRGBValues.Length - 1 Step intPixelByteLength
            For scale As Integer = 0 To pintScale - 1
                For _rgb As Integer = 0 To intPixelByteLength - 1
                    colStrideBytes.Add(arrReadRGBValues(i + _rgb))
                Next
            Next

            ' Strides are rounded up to four bytes...
            If Math.Ceiling(colStrideBytes.Count / 4) = (objWriteData.Stride / 4) Then

                If colStrideBytes.Count <> objWriteData.Stride Then
                    ' ... so pad short rows.
                    For k As Integer = 0 To (objWriteData.Stride - colStrideBytes.Count) - 1
                        colStrideBytes.Add(0)
                    Next
                End If

                For scale As Integer = 0 To pintScale - 1
                    colWriteRGBBytes.AddRange(colStrideBytes)
                Next
                colStrideBytes = New List(Of Byte)
                ' Read strides are also rounded up to four bytes, so skip the padded bytes.
                i += intReadPadding
            End If
        Next

        arrWriteRGBValues = colWriteRGBBytes.ToArray

        System.Runtime.InteropServices.Marshal.Copy(arrWriteRGBValues, 0, ptrWriteScan0Address, intWriteByteLength)

        ' Unlock the bits.
        imgNew.UnlockBits(objWriteData)
        pobjImage.UnlockBits(objReadData)

        Return imgNew
    End Function
End Class
