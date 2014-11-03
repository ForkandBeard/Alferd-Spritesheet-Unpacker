#Region " Options "
Option Explicit On
Option Strict On
#End Region

Imports System.Drawing

Public Class ColourFlattener : Implements QuickImageReader.IQuickColourOperation

    Public Enum FlattenMode
        Brightness = 0
        Hue = 1
        Saturation = 2
        RGB = 3
    End Enum

    Private Mode As FlattenMode
    Private Grain As Integer

    Public Sub New(ByVal penuMode As FlattenMode, ByVal pintGrain As Integer)
        Me.Mode = penuMode
        Me.Grain = pintGrain
    End Sub

    Public Function CalculateNewColour(ByVal pobjOriginal As System.Drawing.Color, ByVal pintX As Integer, ByVal pintY As Integer) As System.Drawing.Color Implements QuickImageReader.IQuickColourOperation.CalculateNewColour
        Dim objReturn As Color

        Select Case Me.Mode
            Case FlattenMode.RGB
                objReturn = Color.FromArgb(CInt(Me.CalculateGrain(255, pobjOriginal.R)), CInt(Me.CalculateGrain(255, pobjOriginal.G)), CInt(Me.CalculateGrain(255, pobjOriginal.B)))

            Case FlattenMode.Brightness
                objReturn = RGBHSL.SetBrightness(pobjOriginal, Me.CalculateGrain(1, pobjOriginal.GetBrightness()))

            Case FlattenMode.Hue
                objReturn = RGBHSL.SetHue(pobjOriginal, Me.CalculateGrain(1, pobjOriginal.GetHue()))

            Case FlattenMode.Saturation
                objReturn = RGBHSL.SetSaturation(pobjOriginal, Me.CalculateGrain(1, pobjOriginal.GetSaturation()))
        End Select

        Return objReturn
    End Function

    Private Function CalculateGrain(ByVal psngMax As Single, ByVal psngValue As Single) As Single
        Dim sngCalc As Single
        Dim intCalc2 As Integer

        sngCalc = psngMax / Me.Grain
        intCalc2 = CInt(psngValue / sngCalc)
        Return sngCalc * intCalc2
    End Function

    Private Function CalculateGrainWhole(ByVal psngMax As Single, ByVal psngValue As Single) As Integer
        Dim intCalc As Integer
        Dim intCalc2 As Integer

        intCalc = CInt(Math.Floor(psngMax / Me.Grain))
        intCalc2 = CInt(Math.Floor(psngValue / intCalc))
        Return intCalc * intCalc2
    End Function

    Public Sub RaiseExceptionIfInvalidForOperation(ByVal pobjImage As System.Drawing.Bitmap) Implements QuickImageReader.IQuickColourOperation.RaiseExceptionIfInvalidForOperation
        ' Do nothing.
    End Sub
End Class
