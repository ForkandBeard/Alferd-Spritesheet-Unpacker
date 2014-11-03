' TODO Replace this hack with a fit for purpose control.
Public Class BuffablePanel : Inherits Panel

    Public Sub New()
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        Me.SetStyle(ControlStyles.ResizeRedraw, True)
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
    End Sub

End Class
