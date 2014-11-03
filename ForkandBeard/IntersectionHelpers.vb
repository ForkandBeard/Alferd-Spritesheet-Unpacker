#Region " Options "
Option Explicit On
Option Strict On
#End Region

Imports System
Imports System.Windows
Imports System.Drawing
'Imports System.Windows.Shapes

Public Class IntersectionHelpers

    ''' <summary>
    ''' This is based off an explanation and expanded math presented by Paul Bourke:
    ''' 
    ''' It takes two lines as inputs and returns true if they intersect, false if they 
    ''' don't.
    ''' If they do, ptIntersection returns the point where the two lines intersect.  
    ''' </summary>
    ''' <param name="L1">The first line</param>
    ''' <param name="L2">The second line</param>
    ''' <param name="ptIntersection">The point where both lines intersect (if they do).</param>
    ''' <returns></returns>
    ''' <remarks>See http:'local.wasp.uwa.edu.au/~pbourke/geometry/lineline2d/</remarks>
    Public Shared Function DoLinesIntersect(ByVal L1 As Line, ByVal L2 As Line, ByRef ptIntersection As PointF) As Boolean
        ' Denominator for ua and ub are the same, so store this calculation
        Dim d As Single = _
           (L2.Y2 - L2.Y1) * (L1.X2 - L1.X1) _
           - _
           (L2.X2 - L2.X1) * (L1.Y2 - L1.Y1)

        'n_a and n_b are calculated as seperate values for readability
        Dim n_a As Single = _
           (L2.X2 - L2.X1) * (L1.Y1 - L2.Y1) _
           - _
           (L2.Y2 - L2.Y1) * (L1.X1 - L2.X1)

        Dim n_b As Single = _
           (L1.X2 - L1.X1) * (L1.Y1 - L2.Y1) _
           - _
           (L1.Y2 - L1.Y1) * (L1.X1 - L2.X1)

        ' Make sure there is not a division by zero - this also indicates that
        ' the lines are parallel.  
        ' If n_a and n_b were both equal to zero the lines would be on top of each 
        ' other (coincidental).  This check is not done because it is not 
        ' necessary for this implementation (the parallel check accounts for this).
        If d = 0 Then Return False

        ' Calculate the intermediate fractional point that the lines potentially intersect.
        Dim ua As Single = n_a / d
        Dim ub As Single = n_b / d

        ' The fractional point will be between 0 and 1 inclusive if the lines
        ' intersect.  If the fractional calculation is larger than 1 or smaller
        ' than 0 the lines would need to be longer to intersect.
        If ua >= 0D AndAlso ua <= 1D AndAlso ub >= 0D AndAlso ub <= 1D Then
            ptIntersection.X = CSng(L1.X1 + (ua * (L1.X2 - L1.X1)))
            ptIntersection.Y = CSng(L1.Y1 + (ua * (L1.Y2 - L1.Y1)))
            Return True
        End If

        Return False
    End Function

    Public Shared Function DoesLineIntersectRectangle(ByVal pobjRectangle As RectangleF, ByVal pobjLine As Line, ByRef pobjIntersectingPortionOfLine As Line) As Boolean
        Dim objLineAsRectangle As RectangleF
        objLineAsRectangle = pobjLine.ToRectangle

        If Not pobjRectangle.IntersectsWith(objLineAsRectangle) Then
            Return False
        Else
            Dim blnIsPointAIn As Boolean
            Dim blnIsPointBIn As Boolean

            blnIsPointAIn = pobjRectangle.Contains(pobjLine.PointA)
            blnIsPointBIn = pobjRectangle.Contains(pobjLine.PointB)

            If blnIsPointAIn AndAlso blnIsPointBIn Then
                pobjIntersectingPortionOfLine = pobjLine
            Else
                Dim colIntersections(1) As PointF ' Either one or two intersections.
                Dim colLines As List(Of Line)
                colLines = GeometryHelper.RectangleTo4Lines(pobjRectangle)

                For Each objLine As Line In colLines
                    If colIntersections(0).IsEmpty Then
                        DoLinesIntersect(objLine, pobjLine, colIntersections(0))
                    ElseIf colIntersections(1).IsEmpty Then
                        DoLinesIntersect(objLine, pobjLine, colIntersections(1))
                    End If
                Next

                If Not blnIsPointAIn AndAlso Not blnIsPointBIn Then
                    pobjIntersectingPortionOfLine = New Line(colIntersections(0), colIntersections(1))
                Else
                    If blnIsPointAIn Then
                        pobjIntersectingPortionOfLine = New Line(colIntersections(0), pobjLine.PointA)
                    Else
                        pobjIntersectingPortionOfLine = New Line(colIntersections(0), pobjLine.PointB)
                    End If
                End If
            End If

            Return True
        End If
    End Function

    Public Shared Function DoesLineIntersectRectangle(ByVal pobjRectangle As RectangleF, ByVal pobjLine As Line) As Boolean
        Dim objLineAsRectangle As RectangleF
        objLineAsRectangle = pobjLine.ToRectangle

        If Not pobjRectangle.IntersectsWith(objLineAsRectangle) Then
            Return False
        Else

            If pobjRectangle.Contains(pobjLine.PointA) _
            AndAlso pobjRectangle.Contains(pobjLine.PointB) Then
                Return True
            End If

            Dim colLines As List(Of Line)
            colLines = GeometryHelper.RectangleTo4Lines(pobjRectangle)
            For Each objLine As Line In colLines
                If DoLinesIntersect(pobjLine, objLine, New PointF) Then
                    Return True
                End If
            Next

            ' False when line just misses a corner of the rectangle.
            Return False
        End If
    End Function

End Class