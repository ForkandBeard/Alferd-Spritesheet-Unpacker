#Region " Options "
Option Strict On
Option Explicit On
#End Region

Imports System.Drawing

Public Class ImageColourSwapper

    Public Shared Function SwapColours(ByVal pobjImage As Bitmap, ByVal pobjOldColour As Color, ByVal pobjNewColour As Color) As Bitmap
        Dim colKeyOriginal As Dictionary(Of Color, Color) = New Dictionary(Of Color, Color)(New ForkandBeard.ColourComparer())
        colKeyOriginal.Add(pobjOldColour, pobjNewColour)
        Return SwapColours(pobjImage, colKeyOriginal)
    End Function

    Public Shared Function SwapColours(ByVal pobjImage As Bitmap, ByVal pcolKeyOriginal As Dictionary(Of Color, Color)) As Bitmap
        Dim objReturn As Bitmap

        objReturn = CType(pobjImage.Clone, Bitmap)

        Dim objPresentColour As Color

        For x As Integer = 0 To objReturn.Width - 1
            For y As Integer = 0 To objReturn.Height - 1
                objPresentColour = objReturn.GetPixel(x, y)

                If pcolKeyOriginal.ContainsKey(objPresentColour) Then
                    objReturn.SetPixel(x, y, pcolKeyOriginal(objPresentColour))
                End If
            Next
        Next

        Return objReturn
    End Function
End Class
