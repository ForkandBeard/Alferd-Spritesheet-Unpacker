#Region " Options "
Option Explicit On
Option Strict On
#End Region

Imports System.Drawing

Public Class ColourSwapper : Implements QuickImageReader.IQuickColourOperation

    Private OriginalKeyToNewValue As Dictionary(Of Color, Color)

    Public Sub New(ByVal pcolOriginalKeyToNewValue As Dictionary(Of Color, Color))
        Me.OriginalKeyToNewValue = pcolOriginalKeyToNewValue
    End Sub

    Public Function CalculateNewColour(ByVal pobjOriginal As System.Drawing.Color, ByVal pintX As Integer, ByVal pintY As Integer) As System.Drawing.Color Implements QuickImageReader.IQuickColourOperation.CalculateNewColour
        If Me.OriginalKeyToNewValue.ContainsKey(pobjOriginal) Then
            Return Me.OriginalKeyToNewValue(pobjOriginal)
        Else
            Return pobjOriginal
        End If
    End Function

    Public Sub RaiseExceptionIfInvalidForOperation(ByVal pobjImage As System.Drawing.Bitmap) Implements QuickImageReader.IQuickColourOperation.RaiseExceptionIfInvalidForOperation
        ' Do nothing.
    End Sub
End Class
