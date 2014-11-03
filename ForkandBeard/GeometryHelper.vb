#Region " Options "
Option Explicit On
Option Strict On
#End Region

Imports System.Drawing

Public Class GeometryHelper
    Public Shared Function RandomPointInRectangle(ByVal pobjRectangle As RectangleF) As PointF
        Return New PointF( _
                           RandomHelper.Random(pobjRectangle.X, pobjRectangle.X + pobjRectangle.Width) _
                            , RandomHelper.Random(pobjRectangle.Y, pobjRectangle.Y + pobjRectangle.Height) _
                            )
    End Function

    Public Shared Function GetBottomLeftOfRectangle(ByVal pobjRectangle As RectangleF) As PointF
        Return New PointF(pobjRectangle.Left, pobjRectangle.Bottom)
    End Function

    Public Shared Function GetTopRightOfRectangle(ByVal pobjRectangle As RectangleF) As PointF
        Return New PointF(pobjRectangle.Right, pobjRectangle.Top)
    End Function

    Public Shared Function GetTopLeftOfRectangle(ByVal pobjRectangle As RectangleF) As PointF
        Return pobjRectangle.Location
    End Function

    Public Shared Function GetBottomRightOfRectangle(ByVal pobjRectangle As RectangleF) As PointF
        Return New PointF(pobjRectangle.Right, pobjRectangle.Bottom)
    End Function

    Public Shared Function GetXGapBetweenRectangles(ByVal pobjRectangleA As RectangleF, ByVal pobjRectangleB As RectangleF) As Single
        Dim sngReturn As Single

        If pobjRectangleA.Right >= pobjRectangleB.Left Then
            sngReturn = pobjRectangleA.Left - pobjRectangleB.Right
        Else
            sngReturn = pobjRectangleB.Left - pobjRectangleA.Right
        End If

        sngReturn = Math.Max(0, sngReturn)

        Return sngReturn
    End Function

    Public Shared Function GetYGapBetweenRectangles(ByVal pobjRectangleA As RectangleF, ByVal pobjRectangleB As RectangleF) As Single
        Dim sngReturn As Single

        If pobjRectangleA.Bottom >= pobjRectangleB.Top Then
            sngReturn = pobjRectangleA.Top - pobjRectangleB.Bottom
        Else
            sngReturn = pobjRectangleB.Top - pobjRectangleA.Bottom
        End If

        sngReturn = Math.Max(0, sngReturn)

        Return sngReturn
    End Function

    Public Shared Function RectangleToRandomSmallerRectangles(ByVal pobjRectangle As Rectangle, ByVal pobjSmallest As Size, ByVal pobjLargest As Size, ByVal pobjRand As Random) As List(Of Rectangle)
        Dim colReturn As List(Of Rectangle) = New List(Of Rectangle)
        Dim objPossibleRect As Rectangle
        Dim blnOverlapping As Boolean = False
        Dim intNonEmptySpaces As Integer = 0
        Dim intPossibleAttempts As Integer
        Dim intIncrement As Integer
        Dim intMissCount As Integer = 0
        Dim objSmartFill As Rectangle = Rectangle.Empty
        intNonEmptySpaces = 0
        intIncrement = Math.Max(pobjSmallest.Height, pobjSmallest.Width)
        Do While intNonEmptySpaces < (pobjRectangle.Height * pobjRectangle.Width)

            intPossibleAttempts = 0

            Do
                Do
                    If intMissCount < Math.Max(25, (pobjRectangle.Height * pobjRectangle.Width) / 10000) _
                    AndAlso intNonEmptySpaces < ((pobjRectangle.Height * pobjRectangle.Width) * 0.8) Then
                        objPossibleRect = New Rectangle(pobjRand.Next(pobjRectangle.X, pobjRectangle.X + pobjRectangle.Width), pobjRand.Next(pobjRectangle.Y, pobjRectangle.Y + pobjRectangle.Height), pobjRand.Next(pobjSmallest.Width, pobjLargest.Width), pobjRand.Next(pobjSmallest.Height, pobjLargest.Height))
                    Else
                        If objSmartFill = Rectangle.Empty Then
                            objSmartFill = New Rectangle(pobjRectangle.X, pobjRectangle.Y, intIncrement, intIncrement)
                        Else
                            If objSmartFill.X < pobjRectangle.Right Then
                                objSmartFill.X += intIncrement
                            Else
                                objSmartFill.X = pobjRectangle.X
                                objSmartFill.Y += intIncrement
                            End If
                        End If
                        objPossibleRect = objSmartFill
                    End If

                    If objPossibleRect.X < (pobjRectangle.X + pobjSmallest.Width) Then
                        objPossibleRect.X = pobjRectangle.X
                    End If

                    If objPossibleRect.Y < (pobjRectangle.Y + pobjSmallest.Height) Then
                        objPossibleRect.Y = pobjRectangle.Y
                    End If

                    If objPossibleRect.X > (pobjRectangle.Right - pobjSmallest.Width) Then
                        objPossibleRect.X = (pobjRectangle.Right - pobjSmallest.Width)
                    End If

                    If objPossibleRect.Y > (pobjRectangle.Bottom - pobjSmallest.Height) Then
                        objPossibleRect.Y = (pobjRectangle.Bottom - pobjSmallest.Height)
                    End If

                    objPossibleRect.Width = CInt(objPossibleRect.Width / intIncrement) * intIncrement
                    objPossibleRect.Height = CInt(objPossibleRect.Height / intIncrement) * intIncrement

                    objPossibleRect.X = CInt(objPossibleRect.X / intIncrement) * intIncrement
                    objPossibleRect.Y = CInt(objPossibleRect.Y / intIncrement) * intIncrement
                Loop While Not pobjRectangle.Contains(objPossibleRect) AndAlso objSmartFill = Rectangle.Empty

                blnOverlapping = False
                For Each objRect As Rectangle In colReturn
                    If objRect.IntersectsWith(objPossibleRect) Then
                        blnOverlapping = True
                        Exit For
                    End If
                Next

                intPossibleAttempts += 1
            Loop While blnOverlapping And (intPossibleAttempts < 10000 OrElse objSmartFill <> Rectangle.Empty)

            If Not blnOverlapping Then
                colReturn.Add(objPossibleRect)
                intNonEmptySpaces += (objPossibleRect.Width * objPossibleRect.Height)
            Else
                intMissCount += 1
                For k As Integer = 0 To colReturn.Count - 1
                    Dim objToEdit As Rectangle
                    Dim objOriginal As Rectangle
                    Dim objNudgedX As Rectangle
                    Dim objNudgedY As Rectangle
                    ' Dim intEditIndex As Integer

                    'intEditIndex = pobjRand.Next(0, colReturn.Count - 1)
                    objToEdit = colReturn(k)
                    objOriginal = objToEdit
                    objNudgedX = objToEdit
                    If objToEdit.X > 0 Then
                        blnOverlapping = False
                        Do
                            objToEdit.X -= intIncrement
                            For Each objRect As Rectangle In colReturn
                                If colReturn.IndexOf(objRect) <> k Then
                                    If objRect.IntersectsWith(objPossibleRect) _
                                    OrElse objRect.Contains(objPossibleRect) _
                                    OrElse objPossibleRect.Contains(objRect) Then
                                        blnOverlapping = True
                                        Exit For
                                    End If
                                End If
                            Next
                        Loop While Not blnOverlapping AndAlso objToEdit.X > pobjSmallest.Width
                        objToEdit.X += intIncrement
                        If objToEdit.X <> objNudgedX.X Then
                            Dim objNew As Rectangle
                            objNew = New Rectangle(objToEdit.Right, objNudgedX.Y, objNudgedX.Width - objToEdit.Width, objNudgedX.Height)
                            colReturn.Add(objNew)
                            intNonEmptySpaces += (objNew.Width * objNew.Height)
                        End If
                    End If

                    objNudgedY = objToEdit
                    If objToEdit.Y > 0 Then
                        blnOverlapping = False
                        Do
                            objToEdit.Y -= intIncrement
                            For Each objRect As Rectangle In colReturn
                                If colReturn.IndexOf(objRect) <> k Then
                                    If objRect.IntersectsWith(objToEdit) Then
                                        blnOverlapping = True
                                        Exit For
                                    End If
                                End If
                            Next
                        Loop While Not blnOverlapping AndAlso objToEdit.Y > pobjSmallest.Height
                        objToEdit.Y += intIncrement
                        If objToEdit.Y <> objNudgedY.Y Then
                            Dim objNew As Rectangle
                            objNew = New Rectangle(objNudgedY.X, objToEdit.Bottom, objNudgedY.Width, objNudgedY.Height - objToEdit.Height)
                            colReturn.Add(objNew)
                            intNonEmptySpaces += (objNew.Width * objNew.Height)
                        End If
                    End If

                    If Not objNudgedX.Equals(objOriginal) _
                        AndAlso Not objNudgedY.Equals(objOriginal) Then
                        Dim objNew As Rectangle
                        objNew = New Rectangle(objNudgedX.Right, objNudgedY.Bottom, objOriginal.Width - (objOriginal.X - objNudgedX.X), objOriginal.Height - (objOriginal.Y - objNudgedY.Y))
                        colReturn.Add(objNew)
                        intNonEmptySpaces += (objNew.Width * objNew.Height)
                    End If
                Next
                If intMissCount Mod 2 = 0 Then
                    For k As Integer = 1 To CInt(colReturn.Count / 10)
                        Dim objToRemove As Rectangle

                        objToRemove = colReturn(pobjRand.Next(0, colReturn.Count - 1))

                        intNonEmptySpaces -= (objToRemove.Width * objToRemove.Height)
                        colReturn.Remove(objToRemove)
                    Next
                End If
            End If

            'Dim grid As Bitmap = New Bitmap(pobjRectangle.Width, pobjRectangle.Height)

            'Using g As Graphics = Graphics.FromImage(grid)
            '    For Each rect As Rectangle In colReturn
            '        g.FillRectangle(New SolidBrush(Color.LightGray), New Rectangle(rect.X - pobjRectangle.X, rect.Y - pobjRectangle.Y, rect.Width, rect.Height))
            '        g.DrawRectangle(New Pen(Color.Red), New Rectangle(rect.X - pobjRectangle.X, rect.Y - pobjRectangle.Y, rect.Width, rect.Height))
            '    Next
            'End Using

            'grid.Save("C:\MWC\Workspace\a\grid.png")
        Loop

        Return colReturn
    End Function

    Public Shared Function RectangleTo4Lines(ByVal pobjRectangle As RectangleF) As List(Of Line)
        Dim colReturn As List(Of Line) = New List(Of Line)

        colReturn.Add(New Line(GetTopLeftOfRectangle(pobjRectangle), New PointF(pobjRectangle.Right, pobjRectangle.Top)))
        colReturn.Add(New Line(GetTopLeftOfRectangle(pobjRectangle), New PointF(pobjRectangle.Left, pobjRectangle.Bottom)))
        colReturn.Add(New Line(New PointF(pobjRectangle.Left, pobjRectangle.Bottom), New PointF(pobjRectangle.Right, pobjRectangle.Bottom)))
        colReturn.Add(New Line(New PointF(pobjRectangle.Right, pobjRectangle.Bottom), New PointF(pobjRectangle.Right, pobjRectangle.Top)))

        Return colReturn
    End Function

    Public Shared Function GetCornerOfRectangleNearestPoint(ByVal pobjRectangle As RectangleF, ByVal pobjPoint As PointF) As PointF
        If Math.Abs(pobjRectangle.X - pobjPoint.X) < Math.Abs(pobjRectangle.Right - pobjPoint.X) Then
            ' Left side is nearer.
            If Math.Abs(pobjRectangle.Y - pobjPoint.Y) < Math.Abs(pobjRectangle.Bottom - pobjPoint.Y) Then
                ' TopLeft side is nearer.
                Return GetTopLeftOfRectangle(pobjRectangle)
            Else
                ' BottomLeft side is nearer.
                Return GetBottomLeftOfRectangle(pobjRectangle)
            End If
        Else
            ' Right side is nearer.
            If Math.Abs(pobjRectangle.Y - pobjPoint.Y) < Math.Abs(pobjRectangle.Bottom - pobjPoint.Y) Then
                ' TopRight side is nearer.
                Return GetTopRightOfRectangle(pobjRectangle)
            Else
                ' BottomRight side is nearer.
                Return GetBottomRightOfRectangle(pobjRectangle)
            End If
        End If
    End Function

    Public Shared Function GetCentreOfRectangle(ByVal pobjRectangle As RectangleF) As PointF
        Return New PointF(pobjRectangle.X + (pobjRectangle.Width / 2), pobjRectangle.Y + (pobjRectangle.Height / 2))
    End Function

    Public Shared Function CreateSubRectanglesWithinRectangle(ByVal pobjOuterRectangle As RectangleF, ByVal pintRowCount As Integer, ByVal pintColumnCount As Integer) As List(Of RectangleF)
        Dim colReturn As List(Of RectangleF) = New List(Of RectangleF)
        Dim sngWidth As Single
        Dim sngHeight As Single

        sngHeight = pobjOuterRectangle.Height / pintRowCount
        sngWidth = pobjOuterRectangle.Width / pintColumnCount

        For r As Integer = 0 To pintRowCount - 1
            For c As Integer = 0 To pintColumnCount - 1
                colReturn.Add(New RectangleF(pobjOuterRectangle.X + (c * sngWidth), pobjOuterRectangle.Y + (r * sngHeight), sngWidth, sngHeight))
            Next
        Next

        Return colReturn
    End Function

    Public Shared Function CreateShrunkRectangle(ByVal pobjRectangle As Rectangle, ByVal psngFactor As Single) As Rectangle
        Dim objReturn As Rectangle

        objReturn = New Rectangle(0, 0, CInt(pobjRectangle.Width * psngFactor), CInt(pobjRectangle.Height * psngFactor))

        objReturn.X = CInt(pobjRectangle.X - ((pobjRectangle.Width - objReturn.Width) / 2))
        objReturn.Y = CInt(pobjRectangle.Y - ((pobjRectangle.Height - objReturn.Height) / 2))

        Return objReturn
    End Function
End Class
