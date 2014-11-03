#Region " Options "
Option Explicit On
Option Strict On
#End Region

Imports System.Drawing
Public Class Line

    Public X1 As Single
    Public X2 As Single
    Public Y1 As Single
    Public Y2 As Single
    Public PointA As PointF
    Public PointB As PointF
    Private _PointLeft As PointF
    Private _PointRight As PointF
    Private _PointTop As PointF
    Private _PointBottom As PointF
    Private _Angle As Single = -1
    Private _Length As Single = -1

    Public ReadOnly Property PointLeft() As PointF
        Get
            If Me._PointLeft.IsEmpty Then
                If Me.PointA.X < Me.PointB.X Then
                    Me._PointLeft = Me.PointA
                Else
                    Me._PointLeft = Me.PointB
                End If
            End If

            Return Me._PointLeft
        End Get
    End Property

    Public ReadOnly Property PointRight() As PointF
        Get
            If Me._PointRight.IsEmpty Then
                If Me.PointA.X > Me.PointB.X Then
                    Me._PointRight = Me.PointA
                Else
                    Me._PointRight = Me.PointB
                End If
            End If

            Return Me._PointRight
        End Get
    End Property

    Public ReadOnly Property PointTop() As PointF
        Get
            If Me._PointTop.IsEmpty Then
                If Me.PointA.Y < Me.PointB.Y Then
                    Me._PointTop = Me.PointA
                Else
                    Me._PointTop = Me.PointB
                End If
            End If

            Return Me._PointTop
        End Get
    End Property

    Public ReadOnly Property PointBottom() As PointF
        Get
            If Me._PointBottom.IsEmpty Then
                If Me.PointA.Y > Me.PointB.Y Then
                    Me._PointBottom = Me.PointA
                Else
                    Me._PointBottom = Me.PointB
                End If
            End If

            Return Me._PointBottom
        End Get
    End Property

    Public Property IntPointA() As Point
        Get
            Return Point.Truncate(Me.PointA)
        End Get
        Set(ByVal value As Point)
            Me.PointA = value
        End Set
    End Property

    Public Property IntPointB() As Point
        Get
            Return Point.Truncate(Me.PointB)
        End Get
        Set(ByVal value As Point)
            Me.PointB = value
        End Set
    End Property

    Public ReadOnly Property Angle() As Single
        Get
            If Me._Angle = -1 Then
                Me._Angle = ForkandBeard.TrigHelper.GetAngleBetweenPoints(Me.PointA, Me.PointB)
            End If
            Return Me._Angle
        End Get
    End Property

    Public ReadOnly Property Length() As Single
        Get
            If Me._Length = -1 Then
                Me._Length = ForkandBeard.TrigHelper.GetDistanceBetweenPoints(Me.PointA, Me.PointB)
            End If
            Return Me._Length
        End Get
    End Property

    Public Sub New(ByVal pobjPointA As PointF, ByVal pobjPointB As PointF)
        Me.X1 = pobjPointA.X
        Me.X2 = pobjPointB.X
        Me.Y1 = pobjPointA.Y
        Me.Y2 = pobjPointB.Y
        Me.PointA = pobjPointA
        Me.PointB = pobjPointB
    End Sub

    Public Sub New()
    End Sub

    Public Function CreateOffset(ByVal pobjOffset As PointF) As Line
        Return New Line(New PointF(Me.PointA.X + pobjOffset.X, Me.PointA.Y + pobjOffset.Y), New PointF(Me.PointB.X + pobjOffset.X, Me.PointB.Y + pobjOffset.Y))
    End Function

    Public Function GetTop() As Single
        Return Math.Min(Me.Y1, Me.Y2)
    End Function

    Public Function GetLeft() As Single
        Return Math.Min(Me.X1, Me.X2)
    End Function

    Public Function GetRight() As Single
        Return Math.Max(Me.X2, Me.X1)
    End Function

    Public Function GetBottom() As Single
        Return Math.Max(Me.Y2, Me.Y1)
    End Function

    Public Function ToRectangle() As RectangleF
        Return New RectangleF(Me.GetLeft, Me.GetTop, Me.GetRight - Me.GetLeft, Me.GetBottom - Me.GetTop)
    End Function

    Public Function GetTopLeft() As PointF
        Return New PointF(Me.GetLeft, Me.GetTop)
    End Function

    Public Function GetBottomRight() As PointF
        Return New PointF(Me.GetRight, Me.GetBottom)
    End Function

    Public Overrides Function ToString() As String
        Return String.Format("Line PointA[{0}] PointB[{1}]", Me.PointA.ToString, Me.PointB.ToString)
    End Function
End Class
