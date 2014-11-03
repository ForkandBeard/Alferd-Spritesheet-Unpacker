#Region " Options "
Option Explicit On
Option Strict On
#End Region

Public Class ColourComparer : Implements IEqualityComparer(Of Drawing.Color)

    Public Function Equals1(ByVal x As System.Drawing.Color, ByVal y As System.Drawing.Color) As Boolean Implements System.Collections.Generic.IEqualityComparer(Of System.Drawing.Color).Equals
        Return AreColoursEqual(x, y)
    End Function

    Public Function GetHashCode1(ByVal obj As System.Drawing.Color) As Integer Implements System.Collections.Generic.IEqualityComparer(Of System.Drawing.Color).GetHashCode
        Return obj.G + (255 * obj.B) + ((255 * 255) * obj.R)
    End Function

    Public Shared Function AreColoursEqual(ByVal x As System.Drawing.Color, ByVal y As System.Drawing.Color) As Boolean
        Return x.R = y.R AndAlso x.B = y.B AndAlso x.G = y.G
    End Function
End Class
