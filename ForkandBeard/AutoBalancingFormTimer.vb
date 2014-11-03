#Region " Options "
Option Explicit On
Option Strict On
#End Region

Public Class AutoBalancingFormTimer : Inherits Windows.Forms.Timer

    Private _TockLongerThanMaxIntervalOccured As Boolean
    Private _MinInterval As Integer = -1
    Private _MaxInterval As Integer = -1
    Event BalancedTock()
    Event TockLongerThanMaxInterval(ByVal pdblDuration As Double)
    Event AverageTockIntervalResumed()

    Public Property MinInterval() As Integer
        Get
            Return Me._MinInterval
        End Get
        Set(ByVal value As Integer)
            Me._MinInterval = value
        End Set
    End Property

    Public Property MaxInterval() As Integer
        Get
            Return Me._MaxInterval
        End Get
        Set(ByVal value As Integer)
            Me._MaxInterval = value
        End Set
    End Property

    Private Sub AutoBalancingFormTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Tick
        Dim dteStart As Date
        Dim dblDuration As Double
        dteStart = Date.Now

        RaiseEvent BalancedTock()
        dblDuration = Date.Now.Subtract(dteStart).TotalMilliseconds()

        If Me.MinInterval <> -1 AndAlso Me.MaxInterval <> -1 Then

            If dblDuration > Me.MaxInterval Then
                If Not Me._TockLongerThanMaxIntervalOccured Then
                    Me._TockLongerThanMaxIntervalOccured = True
                    RaiseEvent TockLongerThanMaxInterval(dblDuration)
                End If
            End If

            If (dblDuration * 1.1) > Me.Interval Then
                Me.Interval = CInt(Math.Min(dblDuration * 1.2, Me.MaxInterval))
            ElseIf Me.Interval <> Me.MinInterval Then
                If dblDuration * 1.1 <= Me.Interval Then
                    Me.Interval = CInt(Math.Max(Me.Interval * 0.8, Me.MinInterval))

                    If Me._TockLongerThanMaxIntervalOccured _
                    AndAlso Me.Interval <= (Me.MaxInterval - Me.MinInterval) Then
                        Me._TockLongerThanMaxIntervalOccured = False
                        RaiseEvent AverageTockIntervalResumed()
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub New(ByVal pobjContainer As System.ComponentModel.IContainer)
        MyBase.New(pobjContainer)
    End Sub
End Class
