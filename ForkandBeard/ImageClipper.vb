#Region " Options "
Option Explicit On
Option Strict On
#End Region

Imports System.Drawing

Public Class ImageClipper
    Public Shared Function ClipImage(ByVal pobjImage As Bitmap, ByVal pobjClipBounds As Rectangle) As List(Of Bitmap)
        Dim colReturn As List(Of Bitmap) = New List(Of Bitmap)
        Dim objNewBitmap As Bitmap

        For x As Integer = 0 To pobjImage.Width Step pobjClipBounds.Width
            For y As Integer = 0 To pobjImage.Height Step pobjClipBounds.Height
                objNewBitmap = New Bitmap(pobjClipBounds.Width, pobjClipBounds.Height)
                Using g As Graphics = Graphics.FromImage(objNewBitmap)
                    g.DrawImage(pobjImage, pobjClipBounds, New Rectangle(x, y, pobjClipBounds.Width, pobjClipBounds.Height), GraphicsUnit.Pixel)
                End Using

                colReturn.Add(objNewBitmap)
            Next
        Next

        Return colReturn
    End Function
End Class
