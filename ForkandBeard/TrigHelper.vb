#Region " Options "
Option Explicit On
Option Strict On
#End Region

Imports System.Drawing

Public Class TrigHelper
    ''' <summary>
    ''' Just a fast way of getting distance to compare two distances.
    ''' </summary>
    ''' <param name="pobjPointA"></param>
    ''' <param name="pobjPointB"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetRelativeDistanceBetweenPoints(ByVal pobjPointA As PointF, ByVal pobjPointB As PointF) As Single
        Dim sngReturn As Single
        Dim objOffset As PointF

        objOffset = New PointF(pobjPointA.X - pobjPointB.X, pobjPointA.Y - pobjPointB.Y)
        objOffset.Y = Math.Abs(objOffset.Y)
        objOffset.X = Math.Abs(objOffset.X)

        sngReturn = (objOffset.X * objOffset.X) + (objOffset.Y * objOffset.Y)

        Return sngReturn
    End Function

    Public Shared Function GetRelativeDistanceBetweenPoints(ByVal pobjPointA As PointF, ByVal pobjPointB As PointF, ByVal psngZA As Single, ByVal psngZB As Single) As Single
        Dim sngReturn As Single
        Dim objOffset As PointF
        Dim zOffset As Single

        objOffset = New PointF(pobjPointA.X - pobjPointB.X, pobjPointA.Y - pobjPointB.Y)
        objOffset.Y = Math.Abs(objOffset.Y)
        objOffset.X = Math.Abs(objOffset.X)
        zOffset = Math.Abs(psngZA - psngZB)

        sngReturn = (objOffset.X * objOffset.X) + (objOffset.Y * objOffset.Y) + (zOffset * zOffset)

        Return sngReturn
    End Function

    Public Shared Function GetDistanceBetweenPoints(ByVal pobjPointA As PointF, ByVal pobjPointB As PointF) As Single
        Dim sngReturn As Single
        Dim objOffset As PointF

        objOffset = New PointF(pobjPointA.X - pobjPointB.X, pobjPointA.Y - pobjPointB.Y)
        objOffset.Y = Math.Abs(objOffset.Y)
        objOffset.X = Math.Abs(objOffset.X)

        sngReturn = CSng(Math.Sqrt((objOffset.X * objOffset.X) + (objOffset.Y * objOffset.Y)))

        Return sngReturn
    End Function

    Public Shared Function Rotate(ByVal pobjPoint As PointF, ByVal psngAngle As Single) As PointF
        Dim radian As Double
        Dim radianCos As Double
        Dim radianSin As Double
        Dim newX As Single
        Dim newY As Single

        radian = (psngAngle * (Math.PI / 180))
        radianCos = Math.Cos(radian)
        radianSin = Math.Sin(radian)

        newX = CSng(((pobjPoint.X * radianCos) - (pobjPoint.Y * radianSin)))
        newY = CSng(((pobjPoint.X * radianSin) + (pobjPoint.Y * radianCos)))

        Return New PointF(newX, newY)
    End Function

    Public Shared Function GetOppositeSide(ByVal psngAngle As Single, ByVal psngAdjacentLength As Single) As Double
        Return Math.Tan(DegreeToRadian(psngAngle)) * psngAdjacentLength
    End Function

    Public Shared Function GetAdjacentSide(ByVal psngAngle As Single, ByVal psngHypotenuse As Single) As Double
        Return Math.Cos(DegreeToRadian(psngAngle)) * psngHypotenuse
    End Function

    Public Shared Function GetAngle(ByVal psngOpposite As Single, ByVal psngAdjacent As Single) As Double
        Return Math.Atan(psngAdjacent / psngOpposite)
    End Function

    Public Shared Function GetPointOffsetByAngle(ByVal pobjPoint As PointF, ByVal psngAngle As Single, ByVal psngDistance As Single) As PointF
        Dim objReturn As PointF

        If Single.IsNaN(psngAngle) Then
            Return pobjPoint
        End If

        objReturn = New PointF( _
                         CSng(psngDistance * (Math.Sin(TrigHelper.DegreeToRadian(CDbl(psngAngle))))) _
                        , CSng(psngDistance * (Math.Cos(TrigHelper.DegreeToRadian(CDbl(psngAngle * -1))))) * -1 _
                        )

        Return GetPointOffset(objReturn, pobjPoint)
    End Function

    Private Shared Function GetPointOffset(ByVal pobjPoint As PointF, ByVal pobjOffset As PointF) As PointF

        Return GetPointOffset(pobjPoint, pobjOffset.X, pobjOffset.Y)
    End Function

    Private Shared Function GetPointOffset(ByVal pobjPoint As PointF, ByVal psngX As Single, ByVal psngY As Single) As PointF
        Dim objReturn As PointF

        objReturn = New PointF(pobjPoint.X + psngX, pobjPoint.Y + psngY)

        Return objReturn
    End Function

    Public Shared Function GetAngleBetweenPoints(ByVal pobjSource As PointF, ByVal pobjTarget As PointF) As Single
        Dim sngO As Double = (pobjSource.Y - pobjTarget.Y)
        Dim sngA As Double = (pobjSource.X - pobjTarget.X)
        Dim sngTan As Single = CSng((sngO / sngA))
        Dim sngAngle As Single = CSng(TrigHelper.RadianToDegree(Math.Atan(CDbl(sngTan))))
        If (sngA < 0) Then
            sngAngle = (sngAngle + 90.0F)
        Else
            sngAngle = (sngAngle - 90.0F)
        End If
        Return CSng(TrigHelper.Get0to360AngleValue(CDbl(sngAngle)))
    End Function

    Public Shared Function RadianToDegree(ByVal pdblRadion As Double) As Double
        Return (pdblRadion * 57.295779513082323)
    End Function

    Public Shared Function DegreeToRadian(ByVal pdblDegree As Double) As Double
        Return ((3.1415926535897931 * pdblDegree) / 180)
    End Function

    Public Shared Function Get0to360AngleValue(ByVal pdblAngle As Double) As Double
        Dim dblReturnAngle As Double = pdblAngle
        If (pdblAngle > 360) Then
            dblReturnAngle = (pdblAngle - (CInt(Math.Round(CDbl((pdblAngle / 360)))) * 360))
        End If
        If (pdblAngle < -360) Then
            dblReturnAngle = (pdblAngle + (CInt(Math.Round(CDbl(((pdblAngle * -1) / 360)))) * 360))
        End If
        If (pdblAngle < 0) Then
            dblReturnAngle = (360 + pdblAngle)
        End If
        Return dblReturnAngle
    End Function


    Public Shared Function GetXAsFractionOfY(ByVal pobjPointF As PointF) As Single
        Dim sngXDistanceAsFractionOfY As Single
        sngXDistanceAsFractionOfY = pobjPointF.X / pobjPointF.Y

        If sngXDistanceAsFractionOfY > 1 Then
            sngXDistanceAsFractionOfY = 1 - (1 / sngXDistanceAsFractionOfY)
        End If

        Return sngXDistanceAsFractionOfY
    End Function

    Public Shared Function GetAngleBetweenAngles(ByVal pdblAngle1 As Double, ByVal pdblAngle2 As Double) As Double
        Dim dblDifference As Double

        pdblAngle1 = Get0to360AngleValue(pdblAngle1)
        pdblAngle2 = Get0to360AngleValue(pdblAngle2)

        dblDifference = Math.Abs(pdblAngle1 - pdblAngle2)

        If dblDifference < 180 Then
            Return dblDifference
        Else
            Return 360 - dblDifference
        End If
    End Function

    Public Shared Function IsPointInSquareBoundedPolygon(ByVal pcolPoints As List(Of Point), ByVal psngX As Single, ByVal psngY As Single) As Boolean
        Dim sngMinX As Single = Single.MaxValue
        Dim sngMaxX As Single = Single.MinValue
        Dim sngMinY As Single = Single.MaxValue
        Dim sngMaxY As Single = Single.MinValue

        For Each objPoint As Point In pcolPoints
            sngMaxX = Math.Max(objPoint.X, sngMaxX)
            sngMinX = Math.Min(objPoint.X, sngMinX)

            sngMaxY = Math.Max(objPoint.Y, sngMaxY)
            sngMinY = Math.Min(objPoint.Y, sngMinY)
        Next

        Return New RectangleF(sngMinX, sngMinY, sngMaxX - sngMinX, sngMaxY - sngMinY).Contains(New PointF(psngX, psngY))
    End Function
End Class
