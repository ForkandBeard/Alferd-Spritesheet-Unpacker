#Region " Options "
Option Strict On
Option Explicit On
#End Region
Imports System.Drawing

Public Class PointSpiralMover

    Private Centre As Point
    Private Present As Point
    Private x As Integer = 1
    Private y As Integer = 0
    Private Counter As Integer = 0
    Private Length As Integer = 1

    Private Radius As Integer = -1
    Private MaxX As Integer
    Private MaxY As Integer
    Private MinX As Integer
    Private MinY As Integer

    Public Sub New(ByVal pobjCentre As Point, ByVal pintRadius As Integer)
        Me.Centre = pobjCentre
        Me.Present = pobjCentre
        Me.Radius = pintRadius
    End Sub

    Public Sub New(ByVal pobjCentre As Point, ByVal pintMaxY As Integer, ByVal pintMinY As Integer, ByVal pintMaxX As Integer, ByVal pintMinX As Integer)
        Me.Centre = pobjCentre
        Me.Present = pobjCentre
        Me.MaxX = pintMaxX
        Me.MinX = pintMinX
        Me.MaxY = pintMaxY
        Me.MinY = pintMinY
    End Sub

    Public Function GetPresentPoint() As Point
        Return Me.Present
    End Function

    Public Function MoveNext() As Point
        Dim objNew As Point

        If Not Me.IsTileOutOfBounds(Me.Present) Then

            ' Spiral tile around centre.
            If Me.Counter = Me.Length Then
                If Me.x <> 0 Then
                    Me.y = x
                    Me.x = 0
                Else
                    Me.x = Me.y * -1
                    Me.y = 0
                    Me.Length += 1
                End If

                Me.Counter = 0
            End If

            objNew = New Point(Me.Present.X + Me.x, Me.Present.Y + Me.y)
            If Not Me.IsTileOutOfBounds(objNew) Then
                Me.Present = objNew
                Me.Counter += 1
            Else
                Return Point.Empty
            End If

            Return Me.Present
        Else
            Return Point.Empty
        End If
    End Function

    Private Function IsTileOutOfBounds(ByVal pobjTile As Point) As Boolean

        If Me.Radius <> -1 Then
            Return (pobjTile.Y >= (Me.Centre.Y + Me.Radius) _
                OrElse pobjTile.Y <= Me.Centre.Y - Me.Radius _
                OrElse pobjTile.X >= Me.Centre.X + Me.Radius _
                OrElse pobjTile.X <= Me.Centre.X - Me.Radius)
        Else
            Return pobjTile.Y > Me.MaxY _
                OrElse pobjTile.Y < Me.MinY _
                OrElse pobjTile.X > Me.MaxX _
                OrElse pobjTile.X < Me.MinX
        End If
    End Function
End Class
