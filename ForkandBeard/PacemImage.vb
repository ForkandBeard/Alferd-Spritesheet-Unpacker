Imports Microsoft.VisualBasic

Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Collections.Generic
Imports System.IO

'http://ryoushin.com/cmerighi/en-US/40.aspx
Public Class PacemImage

    'Public Sub New(ByVal path As String)
    '    _Image = Image.FromFile(path)
    '    _Bitmap = Bitmap.FromFile(path)
    '    _Graphics = Graphics.FromImage(_Image)
    'End Sub

    Private _Image As Image
    Private _Bitmap As Bitmap
    Private _Graphics As Graphics

    Private Sub part1(ByVal topleft As Point, ByVal topright As Point, ByVal bottomleft As Point, ByVal bottomright As Point)
        Dim pointarray() As Point = New Point() {topleft, topright, bottomright, bottomleft}
        Dim minx As Integer = Int32.MaxValue
        Dim maxx As Integer = Int32.MinValue
        Dim miny As Integer = Int32.MaxValue
        Dim maxy As Integer = Int32.MinValue
        For Each p As Point In pointarray
            minx = Math.Min(minx, p.X)
            maxx = Math.Max(maxx, p.X)
            miny = Math.Min(miny, p.Y)
            maxy = Math.Max(maxy, p.Y)
        Next
        Dim rect As Rectangle = New Rectangle(0, 0, maxx, maxy)
        ' resizing...
        Dim bmp As Bitmap = New Bitmap(rect.Width, rect.Height)
        Dim bmpresize As Bitmap = New Bitmap(rect.Width, rect.Height)
        _Graphics = Graphics.FromImage(CType(bmpresize, System.Drawing.Image))
        _Graphics.DrawImage(_Bitmap, rect)
        _Bitmap = bmpresize
        Dim A As PointF = CType(topleft, PointF)
        Dim B As PointF = CType(topright, PointF)
        Dim C As PointF = CType(bottomright, PointF)
        Dim D As PointF = CType(bottomleft, PointF)
        ' lati
        Dim AB() As PointF = New PointF() {A, B}
        Dim AD() As PointF = New PointF() {A, D}
        Dim CD() As PointF = New PointF() {D, C}
        Dim BC() As PointF = New PointF() {C, B}
        ' diagonali
        Dim AC() As PointF = New PointF() {A, C}
        Dim BD() As PointF = New PointF() {D, B}
        ' 

        Dim mAC As Single = GetAngularCoefficient(AC)
        Dim mBD As Single = GetAngularCoefficient(BD)
        Me.Part2(AB, CD, AD, BC, A, B, C, mAC, mBD, rect, D, bmp)
    End Sub

    Private Sub Part2(ByVal AB() As PointF, ByVal CD() As PointF, ByVal AD() As PointF, ByVal BC() As PointF, ByVal A As PointF, ByVal B As PointF, ByVal C As PointF, ByVal mAC As Single, ByVal mBD As Single, ByVal rect As Rectangle, ByVal D As PointF, ByVal bmp As Bitmap)
        Dim H As PointF = PointF.Empty

        Dim args1 As New KeyValuePair(Of PointF, Single)(A, mAC)
        Dim args2 As New KeyValuePair(Of PointF, Single)(B, mBD)

        H = CType(GetIntersection(New KeyValuePair(Of PointF, Single)() {args1, args2}), PointF)
        Dim mAB As Single = GetAngularCoefficient(AB)
        Dim mCD As Single = GetAngularCoefficient(CD)
        Dim mAD As Single = GetAngularCoefficient(AD)
        Dim mBC As Single = GetAngularCoefficient(BC)


        Dim O As Nullable(Of PointF) = GetIntersection(New KeyValuePair(Of PointF, Single)() { _
            New KeyValuePair(Of PointF, Single)(B, mAB), _
            New KeyValuePair(Of PointF, Single)(C, mCD)})

        Dim N As Nullable(Of PointF) = GetIntersection(New KeyValuePair(Of PointF, Single)() { _
            New KeyValuePair(Of PointF, Single)(A, mAD), _
            New KeyValuePair(Of PointF, Single)(B, mBC)})

        Me.Part3(rect, mAB, mBC, A, mAD, O, N, B, C, D, mCD, bmp)
    End Sub

    Private Sub Part3(ByVal rect As Rectangle, ByVal mAB As Single, ByVal mBC As Single, ByVal A As PointF, ByVal mAD As Single, ByVal O As Nullable(Of PointF), ByVal N As Nullable(Of PointF), ByVal B As PointF, ByVal C As PointF, ByVal D As PointF, ByVal mCD As Single, ByVal bmp As Bitmap)
        Dim y As Integer = 0
        Do While (y < rect.Height)
            Dim x As Integer = 0
            Do While (x < rect.Width)
                Me.Part3B(mAB, mAD, mCD, x, y, O, mBC, N, A, B, rect, D, bmp, C)
                x = (x + 1)
            Loop
            y = (y + 1)
        Loop

        Me.Part4(rect, A, B, C, D, bmp)
    End Sub

    Private Sub Part3B(ByVal mAB As Single, ByVal mAD As Single, ByVal mCD As Single, ByVal x As Integer, ByVal y As Integer, ByVal O As Nullable(Of PointF), ByVal mBC As Single, ByVal N As Nullable(Of PointF), ByVal A As PointF, ByVal B As PointF, ByVal rect As Rectangle, ByVal D As PointF, ByVal bmp As Bitmap, ByVal C As PointF)
        Dim P As PointF = New PointF(CType(x, Single), CType(y, Single))
        Dim mPN As Single = Single.Epsilon
        Dim mPO As Single = Single.Epsilon
        If Not O.HasValue Then
            mPO = mAB
        Else
            mPO = GetAngularCoefficient(New PointF() {O.Value, P})
        End If
        If Not N.HasValue Then
            mPN = mBC
        Else
            mPN = GetAngularCoefficient(New PointF() {N.Value, P})
        End If
        Dim L As PointF = PointF.Empty

        Dim Ltemp As Nullable(Of PointF) = GetIntersection(New KeyValuePair(Of PointF, Single)() { _
                    New KeyValuePair(Of PointF, Single)(P, mPO), _
                    New KeyValuePair(Of PointF, Single)(A, mAD)})
        If Ltemp.HasValue Then
            L = Ltemp.Value
        Else
            L = A
        End If
        Dim M As PointF = PointF.Empty
        Dim Mtemp As Nullable(Of PointF) = GetIntersection(New KeyValuePair(Of PointF, Single)() { _
                    New KeyValuePair(Of PointF, Single)(P, mPO), _
                    New KeyValuePair(Of PointF, Single)(B, mBC)})
        If Not Mtemp.HasValue Then
            M = C
        Else
            M = Mtemp.Value
        End If
        Dim J As PointF = PointF.Empty
        Dim Jtemp As Nullable(Of PointF) = GetIntersection(New KeyValuePair(Of PointF, Single)() { _
                    New KeyValuePair(Of PointF, Single)(P, mPN), _
                    New KeyValuePair(Of PointF, Single)(A, mAB)})
        If Jtemp.HasValue Then
            J = Jtemp.Value
        Else
            J = B
        End If
        Dim K As PointF = PointF.Empty
        Dim Ktemp As Nullable(Of PointF) = GetIntersection(New KeyValuePair(Of PointF, Single)() { _
                    New KeyValuePair(Of PointF, Single)(P, mPN), _
                    New KeyValuePair(Of PointF, Single)(D, mCD)})
        If Ktemp.HasValue Then
            K = Ktemp.Value
        Else
            K = D
        End If
        Dim dLP As Double = GetDistance(L, P)
        Dim dMP As Double = GetDistance(M, P)
        Dim dJP As Double = GetDistance(J, P)
        Dim dKP As Double = GetDistance(K, P)
        Dim yP0 As Integer = CType(Math.Round((CType(rect.Height, Double) _
                        * (dJP _
                        / (dJP + dKP)))), Integer)
        Dim xP0 As Long

        If (dLP + dMP) = 0 Then
            xP0 = 0
        Else
            xP0 = CType(Math.Round((CType(rect.Width, Long) _
                            * (dLP _
                            / (dLP + dMP)))), Long)
        End If

        If ((yP0 >= 0) _
                    AndAlso ((xP0 >= 0) _
                    AndAlso ((yP0 < rect.Height) _
                    AndAlso (xP0 < rect.Width)))) Then
            Me.Part3C(bmp, xP0, yP0, x, y)
        End If
    End Sub

    Private Sub Part3C(ByVal bmp As Bitmap, ByVal xP0 As Single, ByVal yP0 As Single, ByVal x As Integer, ByVal y As Integer)
        Dim clr As Color = _Bitmap.GetPixel(xP0, yP0)
        bmp.SetPixel(x, y, clr)
    End Sub

    Private Sub Part4(ByVal rect As Rectangle, ByVal A As PointF, ByVal B As PointF, ByVal C As PointF, ByVal D As PointF, ByVal bmp As Bitmap)
        Dim _bmp As Bitmap = New Bitmap(rect.Width, rect.Height)
        Dim _g As Graphics = Graphics.FromImage(CType(_bmp, System.Drawing.Image))
        Dim path As GraphicsPath = New GraphicsPath
        path.AddLines(New PointF() {A, B, C, D})
        path.CloseFigure()
        _g.Clip = New Region(path)
        _g.DrawImage(bmp, 0, 0)
        ' setting values of the scoping class objects
        _Image = CType(_bmp, System.Drawing.Image)
        bmp.Dispose()
        _Bitmap = _bmp
        _Graphics = _g
    End Sub

    Public Function Distort(ByVal pobjBitmap As Bitmap, ByVal topleft As Point, ByVal topright As Point, ByVal bottomleft As Point, ByVal bottomright As Point) As Bitmap

        Me._Bitmap = New Bitmap(CType(pobjBitmap.Clone(), Bitmap))
        Me.part1(topleft, topright, bottomleft, bottomright)
 
        Return Me._Bitmap
    End Function

    Private Function GetDistance(ByVal A As PointF, ByVal B As PointF) As Double
        Return Math.Sqrt((Math.Pow((CType(A.X, Double) - CType(B.X, Double)), 2D) + Math.Pow((CType(A.Y, Double) - CType(B.Y, Double)), 2D)))
    End Function


    Private Function GetIntersection(ByVal pointAngularCoeff As KeyValuePair(Of PointF, Single)()) As Nullable(Of PointF)

        If pointAngularCoeff.Length <> 2 Then Throw New ArgumentException("Parameter must be of 2 items", "pointAngularCoeff")
        Dim U As PointF = pointAngularCoeff(0).Key
        Dim V As PointF = pointAngularCoeff(1).Key
        Dim m1 As Single = pointAngularCoeff(0).Value
        Dim m2 As Single = pointAngularCoeff(1).Value

        If U = V Then Return U

        If m1 = m2 Then Return Nothing

        Dim newx As Single = Single.Epsilon
        Dim newy As Single = Single.Epsilon
        If Single.IsInfinity(m1) Then
            newx = U.X : newy = V.Y + m2 * (-V.X + U.X)
        End If
        If Single.IsInfinity(m2) Then
            newx = V.X : newy = U.Y + m1 * (-U.X + V.X)
        End If
        If newx = Single.Epsilon Then
            Dim q1 As Single = U.Y - m1 * U.X
            Dim q2 As Single = V.Y - m2 * V.X
            newx = (q1 - q2) / (m2 - m1)
            newy = m1 * newx + q1
        End If
        Return New PointF(newx, newy)
    End Function

    Private Function GetAngularCoefficient(ByVal segment As PointF()) As Single
        If segment.Length <> 2 Then Throw New ArgumentException("Parameter must be of 2 items", "segment")
        Dim U As PointF = segment(0)
        Dim V As PointF = segment(1)
        Dim angle As Double = GetAngularCoefficientRads(U, V)

        If angle Mod Math.PI = Math.PI / 2D Then Return 0 'Single.PositiveInfinity
        If angle Mod Math.PI = -Math.PI / 2D Then Return 0 'Single.NegativeInfinity
        Return CSng(Math.Tan(angle))
    End Function


    Private Function GetAngleRadians(ByVal from As PointF, ByVal vertex As PointF, ByVal [to] As PointF) As Double
        Dim first As Double = GetAngularCoefficientRads(vertex, [to])
        Dim second As Double = GetAngularCoefficientRads(vertex, [from])
        Return (first - second)
    End Function

    Private Function GetAngularCoefficientRads(ByVal from As PointF, ByVal [to] As PointF) As Double
        If ([to].Y = from.Y) Then

            If (from.X > [to].X) Then
                Return Math.PI
            Else
                Return 0D
            End If

        ElseIf ([to].X = from.X) Then

            If ([to].Y < from.Y) Then
                Return -Math.PI / 2D
            End If
            Return Math.PI / 2D

        Else
            Dim m As Double = Math.Atan(CType((([to].Y - from.Y) _
                            / ([to].X - from.X)), Double))
            If ([to].X < 0) Then
                If (m > 0) Then
                    m += Math.PI / 2D
                End If
                If (m < 0) Then
                    m -= Math.PI
                End If
            End If
            Return m
        End If
    End Function

    Public Sub Dispose()
        _Image.Dispose()
        _Bitmap.Dispose()
        _Graphics.Dispose()
    End Sub

    Public ReadOnly Property InnerBmp() As Bitmap
        Get
            Return _Bitmap
        End Get
    End Property


End Class
