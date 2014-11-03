#Region " Options "
Option Explicit On
Option Strict On
#End Region

Imports System.Drawing

Public Class ColourAlphaSetter : Implements QuickImageReader.IQuickColourOperation

    Private Alpha As Integer

    Public Sub New(ByVal pintAlpha As Integer)
        Me.Alpha = pintAlpha
    End Sub

    Public Function CalculateNewColour(ByVal pobjOriginal As System.Drawing.Color, ByVal pintX As Integer, ByVal pintY As Integer) As System.Drawing.Color Implements QuickImageReader.IQuickColourOperation.CalculateNewColour
        Return Color.FromArgb(Me.Alpha, pobjOriginal.R, pobjOriginal.G, pobjOriginal.B)
    End Function

    Public Sub RaiseExceptionIfInvalidForOperation(ByVal pobjImage As System.Drawing.Bitmap) Implements QuickImageReader.IQuickColourOperation.RaiseExceptionIfInvalidForOperation
        If pobjImage.PixelFormat <> Imaging.PixelFormat.Format32bppArgb Then
            Throw New Exception("Only Imaging.PixelFormat.Format32bppArgb is Valid.")
        End If
    End Sub
End Class
