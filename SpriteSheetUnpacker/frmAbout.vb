#Region " Options "
Option Strict On
Option Explicit On
#End Region

Public Class frmAbout


    Private Movement As PointF = New PointF(0, 0)

    Private CoolBackGroundImage As Bitmap = Nothing
    Private Flood As Integer
    Private Counter As Integer = 0
    Private NewDrops As Integer
    Private Random As Random = New Random
    Private Drops As List(Of PointF) = New List(Of PointF)

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            Me.Refresh()

            If Me.CoolBackGroundImage Is Nothing Then
                Exit Sub
            End If

            Me.RotateBackground()

            Dim objGraphics As Graphics

            objGraphics = Graphics.FromImage(Me.CoolBackGroundImage)
            objGraphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

            If Me.Random.Next(0, 20) = 1 Then
                Dim objHead As Bitmap
                objHead = My.Resources.AlferdPackerHead

                If Me.Random.Next(0, 2) = 1 Then
                    objHead.RotateFlip(RotateFlipType.RotateNoneFlipY)
                End If

                If Me.Random.Next(0, 2) = 1 Then
                    objHead.RotateFlip(RotateFlipType.RotateNoneFlipX)
                End If

                objHead.MakeTransparent(Color.White)
                objHead = Rotatamathon.Rotate.RotateImage(objHead, Me.Random.Next(0, 360))

                Dim objRandomRectangle As RectangleF

                objRandomRectangle = New RectangleF(ForkandBeard.GeometryHelper.RandomPointInRectangle(New Rectangle(0, 0, Me.CoolBackGroundImage.Width, Me.CoolBackGroundImage.Height)), objHead.Size)

                objGraphics.DrawImage(objHead, ForkandBeard.GeometryHelper.RandomPointInRectangle(New Rectangle(0, 0, Me.CoolBackGroundImage.Width, Me.CoolBackGroundImage.Height)))
                objGraphics.DrawLine( _
                    Pens.Black _
                    , ForkandBeard.GeometryHelper.RandomPointInRectangle(objRandomRectangle) _
                    , ForkandBeard.GeometryHelper.RandomPointInRectangle(New Rectangle(0, 0, Me.CoolBackGroundImage.Width, Me.CoolBackGroundImage.Height)) _
                    )
                'objGraphics.FillRectangle(New SolidBrush(Color.FromArgb(100, 255, 255, 255)), objRandomRectangle)
                'Me.RotateBackground()
                objHead.Dispose()
                objGraphics.Dispose()
                objGraphics = Graphics.FromImage(Me.CoolBackGroundImage)
                objGraphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            End If

            If Me.Random.Next(0, 20) = 1 Then
                Dim intRand As Integer
                intRand = Me.Random.Next(0, 5)
                Select Case intRand
                    Case 0
                        Me.Movement = New PointF(0, 0.1)
                    Case 1
                        Me.Movement = New PointF(0, -0.1)
                    Case 2
                        Me.Movement = New PointF(0.1, 0)
                    Case 3
                        Me.Movement = New PointF(-0.1, 0)
                    Case 4
                        Me.Movement = New PointF(0, 0)
                End Select
            End If

            If Me.Random.Next(0, 10) = 1 Then
                Dim intSize As Integer
                intSize = Me.Random.Next(1, 25)
                objGraphics.FillEllipse(New SolidBrush(Color.FromArgb(100, 200, 0, 0)), New RectangleF(ForkandBeard.GeometryHelper.RandomPointInRectangle(New Rectangle(0, 0, Me.CoolBackGroundImage.Width, Me.CoolBackGroundImage.Height)), New Size(intSize, intSize)))
            End If

            If Me.Random.Next(0, 2) = 1 Then
                objGraphics.DrawLine( _
                    New Pen(Color.FromArgb(100, 50, 50, 50), 3) _
                    , ForkandBeard.GeometryHelper.RandomPointInRectangle(New Rectangle(0, 0, Me.CoolBackGroundImage.Width, Me.CoolBackGroundImage.Height)) _
                    , ForkandBeard.GeometryHelper.RandomPointInRectangle(New Rectangle(0, 0, Me.CoolBackGroundImage.Width, Me.CoolBackGroundImage.Height)) _
                    )
            Else
                objGraphics.DrawLine( _
                    Pens.White _
                    , ForkandBeard.GeometryHelper.RandomPointInRectangle(New Rectangle(0, 0, Me.CoolBackGroundImage.Width, Me.CoolBackGroundImage.Height)) _
                    , ForkandBeard.GeometryHelper.RandomPointInRectangle(New Rectangle(0, 0, Me.CoolBackGroundImage.Width, Me.CoolBackGroundImage.Height)) _
                    )
            End If

            objGraphics.Dispose()
        Catch sorry As Exception
        End Try
    End Sub

    Private Sub RotateBackground()
        Dim objGraphics As Graphics
        Dim objOldImage As Bitmap

        objOldImage = CType(Me.CoolBackGroundImage.Clone, Bitmap)
        Me.CoolBackGroundImage.Dispose()

        Me.CoolBackGroundImage = New Bitmap(Me.Width * 2, Me.Height * 2)

        objGraphics = Graphics.FromImage(Me.CoolBackGroundImage)
        objGraphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        objGraphics.RotateTransform(0.05)

        objGraphics.DrawImage(objOldImage, Me.Movement)
        objGraphics.Dispose()
        objOldImage.Dispose()
    End Sub

    Private Sub frmAbout_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        MessageBox.Show("Any bugs, suggestions, feedback to cat@forkandbeard.co.uk")
        Me.CoolBackGroundImage = New Bitmap(Me.Width * 2, Me.Height * 2)
        Graphics.FromImage(Me.CoolBackGroundImage).FillRectangle(Brushes.Silver, New Rectangle(0, 0, Me.Width * 2, Me.Height * 2))
    End Sub

    Private Sub frmAbout_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        e.Graphics.DrawImage(Me.CoolBackGroundImage, (Me.CoolBackGroundImage.Width * 0.3F) * -1, (Me.CoolBackGroundImage.Height * 0.3F) * -1)
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Diagnostics.Process.Start("http://www.forkandbeard.co.uk?app=Alferd")
        Me.LinkLabel1.Visible = False
    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click
        Me.Label3.Visible = False
    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click
        Me.Label2.Visible = False
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
        Me.Label1.Visible = False
    End Sub

    Private Sub BuffablePanel1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BuffablePanel1.Click
        Me.BuffablePanel1.Visible = False
    End Sub
End Class