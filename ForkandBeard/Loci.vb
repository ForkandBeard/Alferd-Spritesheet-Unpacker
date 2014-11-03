#Region " Options "
Option Explicit On
Option Strict On
#End Region

Imports System.Drawing

Public Class Loci

    Private Rectangle As RectangleF
    Private Line As Line
    Private Point As PointF
    Private _Type As LociType

    ''' <summary>
    ''' Default constructor creates an empty Loci.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        Me._Type = LociType.Empty
    End Sub

    Public Sub New(ByVal pobjPoint As PointF)
        Me.Point = pobjPoint
        Me._Type = LociType.Point
    End Sub

    Public Sub New(ByVal pobjRectangle As RectangleF)
        Me.Rectangle = pobjRectangle
        Me._Type = LociType.Rectangle
    End Sub

    Public Sub New(ByVal pobjLine As Line)
        Me.Line = pobjLine
        Me._Type = LociType.Line
    End Sub

    Public ReadOnly Property Type() As LociType
        Get
            Return Me._Type
        End Get
    End Property

    Public ReadOnly Property IsEmpty() As Boolean
        Get
            Select Case Me.Type
                Case LociType.Line
                    Return Me.Line Is Nothing
                Case LociType.Point
                    Return Me.Point.IsEmpty
                Case LociType.Rectangle
                    Return Me.Rectangle.IsEmpty
                Case LociType.Empty
                    Return True
            End Select
        End Get
    End Property

    Public Function ToRectangle() As RectangleF
        Select Case Me.Type
            Case LociType.Line
                Return Me.Line.ToRectangle
            Case LociType.Point
                Return New RectangleF(Me.Point.X - 0.5F, Me.Point.Y - 0.5F, 1, 1)
            Case LociType.Rectangle
                Return Me.Rectangle
            Case LociType.Empty
                Return New RectangleF()
        End Select
    End Function

    Public Function GetRectangleLineOrPoint() As Object
        Select Case Me.Type
            Case LociType.Line
                Return Me.Line
            Case LociType.Point
                Return Me.Point
            Case LociType.Rectangle
                Return Me.Rectangle
            Case Else
                Return Nothing
        End Select
    End Function

    Public Function OffsetToNewCentre(ByVal pobjNewCentre As PointF) As Loci
        Select Case Me.Type
            Case LociType.Line
                Dim objCentre As PointF
                objCentre = Me.GetCentrePoint()
                Return New Loci(Me.Line.CreateOffset(New PointF(pobjNewCentre.X - objCentre.X, pobjNewCentre.Y - objCentre.Y)))
            Case LociType.Point
                Return New Loci(pobjNewCentre)
            Case LociType.Rectangle
                Return New Loci(New RectangleF(pobjNewCentre.X - (Me.Rectangle.Width / 2), pobjNewCentre.Y - (Me.Rectangle.Height / 2), Me.Rectangle.Width, Me.Rectangle.Height))
        End Select

        Throw New NotSupportedException
    End Function

    Public Function GetBottomRightPoint() As PointF
        Select Case Me.Type
            Case LociType.Line
                Return Me.Line.GetBottomRight
            Case LociType.Point
                Return Me.Point
            Case LociType.Rectangle
                Return GeometryHelper.GetBottomRightOfRectangle(Me.Rectangle)
            Case LociType.Empty
                Return New PointF()
        End Select
    End Function

    Public Function GetTopLeftPoint() As PointF
        Select Case Me.Type
            Case LociType.Point
                Return Me.Point
            Case LociType.Line
                Return Me.Line.GetTopLeft
            Case LociType.Rectangle
                Return GeometryHelper.GetTopLeftOfRectangle(Me.Rectangle)
            Case LociType.Empty
                Return New PointF()
        End Select
    End Function

    Public Function GetCentrePoint() As PointF
        Select Case Me.Type
            Case LociType.Line
                Return GeometryHelper.GetCentreOfRectangle(Me.Line.ToRectangle)
            Case LociType.Point
                Return Me.Point
            Case LociType.Rectangle
                Return GeometryHelper.GetCentreOfRectangle(Me.Rectangle)
            Case LociType.Empty
                Return New PointF()
        End Select
    End Function

    Public Sub SetCentrePoint(ByVal pobjCentrePoint As PointF)
        Select Case Me.Type
            Case LociType.Line
                Dim objPresentCentre As PointF
                objPresentCentre = Me.GetCentrePoint
                Me.Line = New Line( _
                                    New PointF(Me.Line.PointA.X + (pobjCentrePoint.X - objPresentCentre.X), Me.Line.PointA.Y + (pobjCentrePoint.Y - objPresentCentre.Y)) _
                                , New PointF(Me.Line.PointB.X + (pobjCentrePoint.X - objPresentCentre.X), Me.Line.PointB.Y + (pobjCentrePoint.Y - objPresentCentre.Y)) _
                                )
            Case LociType.Point
                Me.Point = pobjCentrePoint
            Case LociType.Rectangle
                Me.Rectangle = New RectangleF(pobjCentrePoint.X - (Me.Rectangle.Width / 2), pobjCentrePoint.Y - (Me.Rectangle.Height / 2), Me.Rectangle.Width, Me.Rectangle.Height)
        End Select
    End Sub

    Public Function GetRandomPointWithinLoci() As PointF
        Select Case Me.Type
            Case LociType.Line
                Dim objReturn As PointF
                Dim objLineAsRectangle As RectangleF
                Dim sngRectangleVector As Single
                objLineAsRectangle = Me.ToRectangle

                If objLineAsRectangle.Width >= objLineAsRectangle.Height Then
                    objReturn = New PointF(RandomHelper.Random(objLineAsRectangle.Left, objLineAsRectangle.Right), 0)
                    sngRectangleVector = objLineAsRectangle.Height / objLineAsRectangle.Width
                    If Me.Line.PointLeft.Y > Me.Line.PointRight.Y Then
                        objReturn.Y = objLineAsRectangle.Bottom - ((objReturn.X - objLineAsRectangle.Left) * sngRectangleVector)
                    Else
                        objReturn.Y = objLineAsRectangle.Top + ((objReturn.X - objLineAsRectangle.Left) * sngRectangleVector)
                    End If                    
                Else
                    objReturn = New PointF(0, RandomHelper.Random(objLineAsRectangle.Top, objLineAsRectangle.Bottom))
                    sngRectangleVector = objLineAsRectangle.Width / objLineAsRectangle.Height
                    If Me.Line.PointBottom.X < Me.Line.PointTop.X Then
                        objReturn.X = objLineAsRectangle.Right - ((objReturn.Y - objLineAsRectangle.Top) * sngRectangleVector)
                    Else
                        objReturn.X = objLineAsRectangle.Left + ((objReturn.Y - objLineAsRectangle.Top) * sngRectangleVector)
                    End If
                End If

                Return objReturn
            Case LociType.Point
                Return Me.Point
            Case LociType.Rectangle
                Return GeometryHelper.RandomPointInRectangle(Me.Rectangle)
            Case LociType.Empty
                Return New PointF()
        End Select
    End Function

    Public Function GetRandomLineWithinLoci() As Line
        Select Case Me.Type
            Case LociType.Line
                Return Me.Line
            Case LociType.Point
                Return Me.CreateLineFromPoint
            Case LociType.Rectangle
                Dim objPointA As PointF
                Dim objPointB As PointF
                Dim objPointAtCentre As PointF
                objPointAtCentre = Me.GetCentrePoint
                objPointA = New PointF(Me.Rectangle.X, RandomHelper.Random(Me.Rectangle.Top, Me.Rectangle.Bottom))
                ' Second point must go through centre of rectangle.
                objPointB = New PointF(Me.Rectangle.Right, objPointAtCentre.Y + (objPointAtCentre.Y - objPointA.Y))
                Return New Line(objPointA, objPointB)
        End Select

        Throw New NotSupportedException
    End Function

    Public Function DoesLocusIntersect(ByVal pobjTarget As Loci) As Boolean
        Select Case Me.Type
            Case LociType.Line
                Select Case pobjTarget.Type
                    Case LociType.Line
                        Return IntersectionHelpers.DoLinesIntersect(Me.Line, pobjTarget.Line, New Point(0, 0))
                    Case LociType.Point
                        Return IntersectionHelpers.DoLinesIntersect(Me.Line, pobjTarget.CreateLineFromPoint(), New Point(0, 0))
                    Case LociType.Rectangle
                        Return IntersectionHelpers.DoesLineIntersectRectangle(pobjTarget.Rectangle, Me.Line)
                End Select
            Case LociType.Point
                Select Case pobjTarget.Type
                    Case LociType.Line
                        Return IntersectionHelpers.DoLinesIntersect(pobjTarget.Line, Me.CreateLineFromPoint, New Point(0, 0))
                    Case LociType.Point
                        Return Me.Point.Equals(pobjTarget.Point)
                    Case LociType.Rectangle
                        Return pobjTarget.Rectangle.Contains(Me.Point)
                End Select
            Case LociType.Rectangle
                Select Case pobjTarget.Type
                    Case LociType.Line
                        Return IntersectionHelpers.DoesLineIntersectRectangle(Me.Rectangle, pobjTarget.Line)
                    Case LociType.Point
                        Return Me.Rectangle.Contains(pobjTarget.Point)
                    Case LociType.Rectangle
                        Return pobjTarget.Rectangle.IntersectsWith(Me.Rectangle)
                End Select
        End Select

        Throw New NotSupportedException
    End Function

    ''' <returns>Returns the loci which describes the intersection. WARNING: Where points are concerned it is assumed the point does intersect so the point simply gets returned. This is to prevent multiple calls to DoesLocusIntersect.</returns>
    Public Function GetLocusIntersection(ByVal pobjTarget As Loci) As Loci
        Select Case Me.Type
            Case LociType.Line
                Select Case pobjTarget.Type
                    Case LociType.Line
                        Dim objReturn As PointF = PointF.Empty                    
                        IntersectionHelpers.DoLinesIntersect(Me.Line, pobjTarget.Line, objReturn)
                        Return New Loci(objReturn)
                    Case LociType.Point
                        ' Need to change this to return New Loci() if not intersecting.
                        Return New Loci(pobjTarget.Point)
                    Case LociType.Rectangle
                        Dim objReturn As Line = Nothing
                        If IntersectionHelpers.DoesLineIntersectRectangle(pobjTarget.Rectangle, Me.Line, objReturn) Then
                            Return New Loci(objReturn)
                        Else
                            Return New Loci()
                        End If
                End Select
            Case LociType.Point
                Select Case pobjTarget.Type
                    Case LociType.Line
                        Return New Loci(Me.Point)
                    Case LociType.Point
                        Return New Loci(Me.Point)
                    Case LociType.Rectangle
                        Return New Loci(Me.Point)
                End Select
            Case LociType.Rectangle
                Select Case pobjTarget.Type
                    Case LociType.Line
                        Dim objReturn As Line = Nothing
                        If IntersectionHelpers.DoesLineIntersectRectangle(Me.Rectangle, pobjTarget.Line, objReturn) Then
                            Return New Loci(objReturn)
                        Else
                            Return New Loci()
                        End If
                    Case LociType.Point
                        Return New Loci(pobjTarget.Point)
                    Case LociType.Rectangle
                        Dim objRectangle As RectangleF = New RectangleF(pobjTarget.Rectangle.Location, pobjTarget.Rectangle.Size)
                        objRectangle.Intersect(Me.Rectangle)
                        Return New Loci(objRectangle)
                End Select
        End Select

        Throw New NotSupportedException
    End Function

    'TODO: Bit of a HACK which could be replaced because it seems intensive to create line just because we already have a do lines intersect method. See if it creates a bottleneck before optimising.
    Private Function CreateLineFromPoint() As Line
        Return New Line(New PointF(Me.Point.X - 0.1F, Me.Point.Y - 0.1F), New PointF(Me.Point.X + 0.1F, Me.Point.Y + 0.1F))
    End Function

    Public Overrides Function ToString() As String
        Return Me.GetRectangleLineOrPoint.ToString
    End Function

    Public ReadOnly Property CoordPlotterText() As String
        Get
            Select Case Me.Type
                Case LociType.Line
                    Return String.Format("{0}, {1}, {2}, {3}", Me.Line.PointA.X, Me.Line.PointA.Y, Me.Line.PointB.X, Me.Line.PointB.Y)
                Case LociType.Point
                    Return String.Format("{0}, {1}", Me.Point.X, Me.Point.Y)
                Case LociType.Rectangle
                    Return String.Format("{0}, {1}, {2}, {3}", Me.Rectangle.Location.X, Me.Rectangle.Location.Y, GeometryHelper.GetBottomRightOfRectangle(Me.Rectangle).X, GeometryHelper.GetBottomRightOfRectangle(Me.Rectangle).Y)
                Case Else
                    Return String.Empty
            End Select
        End Get
    End Property
End Class
