#Region " Options "
Option Explicit On
Option Strict On
#End Region
Imports System.Drawing

Public Class UI
    Public Shared Sub DrawCross(ByVal pobjGraphics As Graphics, ByVal pobjPoint As PointF, ByVal pobjColour As Color)

        pobjGraphics.DrawLine(New Pen(pobjColour, 1), pobjPoint.X - 4, pobjPoint.Y, pobjPoint.X + 4, pobjPoint.Y)
        pobjGraphics.DrawLine(New Pen(pobjColour, 1), pobjPoint.X, pobjPoint.Y - 4, pobjPoint.X, pobjPoint.Y + 4)
    End Sub
End Class
