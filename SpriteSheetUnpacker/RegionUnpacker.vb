#Region " Options "
Option Strict On
Option Explicit On
#End Region

Public Class RegionUnpacker
    Implements IDisposable
    Public Shared Wait As Object = New Object
    Public Shared Counter As Integer
    Private BackgroundColour As Color
    Private Image As Bitmap
    Private Region As Rectangle
    Public Boxes As List(Of Rectangle) = New List(Of Rectangle)

    Public Sub New(ByVal pimgImage As Bitmap, ByVal pobjRegion As Rectangle, ByVal pobjBackgroundColour As Color)
        Me.Image = pimgImage
        Me.Region = pobjRegion
        Me.BackgroundColour = pobjBackgroundColour
    End Sub

    Public Sub UnpackRegion()
        Me.Boxes = CreateBoxes(Me.Image, Me.Region, Me.BackgroundColour)
        CombineBoxes(Me.Boxes, Me.BackgroundColour, Me.Image)
        SyncLock (Wait)
            Counter += 1
            Threading.Monitor.PulseAll(Wait)
        End SyncLock
    End Sub

    Public Function GetImage() As Bitmap
        Return Me.Image
    End Function

    Private Shared Function CreateBoxes(ByVal pobjImage As Bitmap, ByVal pobjRegion As Rectangle, ByVal pobjBackground As Color) As List(Of Rectangle)
        Dim colReturn As List(Of Rectangle) = New List(Of Rectangle)
        Dim objPresentPixel As Point
        Dim objNewBox As Rectangle
        Dim x2 As Integer
        Dim y2 As Integer
        Dim objPresentColour As Color

        For y As Integer = pobjRegion.Top To pobjRegion.Bottom
            For x As Integer = pobjRegion.Left To pobjRegion.Right

                If x > 0 _
                AndAlso x < pobjImage.Width _
                AndAlso y > 0 _
                AndAlso y < pobjImage.Height Then
                    objPresentPixel = New Point(x, y)

                    objPresentColour = pobjImage.GetPixel(x, y)

                    If objPresentColour <> pobjBackground Then
                        objNewBox = New Rectangle(objPresentPixel, New Size(0, 0))
                        x2 = x

                        Do While x2 < (pobjImage.Width - 1) _
                            AndAlso pobjImage.GetPixel(x2, y) <> pobjBackground
                            x2 += 1
                            objNewBox = New Rectangle(objNewBox.X, objNewBox.Y, objNewBox.Width + 1, objNewBox.Height)
                        Loop

                        y2 = y
                        Do While y2 < (pobjImage.Height - 1) _
                            AndAlso pobjImage.GetPixel(x2, y2) <> pobjBackground
                            y2 += 1
                            objNewBox = New Rectangle(objNewBox.X, objNewBox.Y, objNewBox.Width, objNewBox.Height + 1)
                        Loop

                        y2 = y + objNewBox.Height
                        Do While y2 < (pobjImage.Height - 1) _
                            AndAlso pobjImage.GetPixel(x, y2) <> pobjBackground
                            y2 += 1
                            objNewBox = New Rectangle(objNewBox.X, objNewBox.Y, objNewBox.Width, objNewBox.Height + 1)
                        Loop

                        colReturn.Add(objNewBox)

                        x += (objNewBox.Width + 1)
                    End If
                End If
            Next
        Next

        Return colReturn
    End Function

    Public Shared Sub CombineBoxes(ByRef pcolBoxes As List(Of Rectangle), ByVal pobjBackground As Color, ByVal pobjImage As Bitmap)
        Dim intIndex As Integer = 0
        Do
            intIndex = CombineFirstOverlappingBox(pcolBoxes, pobjBackground, pobjImage, intIndex)
        Loop While intIndex <> -1
    End Sub

    Private Shared Function CombineFirstOverlappingBox(ByRef pcolBoxes As List(Of Rectangle), ByVal pobjBackground As Color, ByVal pobjImage As Bitmap, ByVal pintStartIndex As Integer) As Integer
        Dim objNewBox As Rectangle = Rectangle.Empty
        Dim colOldBoxes As List(Of Rectangle) = New List(Of Rectangle)
        Dim intReturn As Integer = -1
        Dim objBox As Rectangle

        For i As Integer = pintStartIndex To pcolBoxes.Count - 1 ' Each objBox As Rectangle In pcolBoxes
            objBox = pcolBoxes(i)
            For Each objCollider As Rectangle In pcolBoxes

                If objBox <> objCollider Then

                    If DoBoxesContainAdjacentOrOverlappingPixels(objBox, objCollider, pobjBackground, pobjImage) Then

                        objNewBox = objBox

                        intReturn = i
                        If objCollider.Right > objNewBox.Right Then
                            objNewBox.Width = objCollider.Right - objNewBox.Left
                        End If

                        If objCollider.Left < objNewBox.Left Then
                            objNewBox.Width += objNewBox.Left - objCollider.Left
                        End If

                        If objCollider.Bottom > objNewBox.Bottom Then
                            objNewBox.Height = objCollider.Bottom - objNewBox.Top
                        End If

                        If objCollider.Top < objNewBox.Top Then
                            objNewBox.Height += objNewBox.Top - objCollider.Top
                        End If

                        objNewBox.X = Math.Min(objNewBox.X, objCollider.X)
                        objNewBox.Y = Math.Min(objCollider.Y, objNewBox.Y)

                        colOldBoxes.Add(objBox)
                        colOldBoxes.Add(objCollider)
                        Exit For
                    End If
                End If
            Next

            If objNewBox <> Rectangle.Empty Then
                Exit For
            End If
        Next

        If objNewBox <> Rectangle.Empty Then
            For Each objBox2 As Rectangle In colOldBoxes
                pcolBoxes.Remove(objBox2)
            Next
            pcolBoxes.Add(objNewBox)
        End If

        Return intReturn
    End Function

    Public Shared Sub DeleteAllTempFiles()
        Try
            Console.WriteLine("Deleting from " & Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData))
            For Each strFile As String In IO.Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "asu_temp_spritesheet*", IO.SearchOption.TopDirectoryOnly)
                Console.WriteLine("Deleting " & strFile)
                IO.File.Delete(strFile)
            Next
        Catch ignore As Exception
            Console.WriteLine(ignore.ToString)
        End Try
    End Sub

    Private Shared Function DoBoxesContainAdjacentOrOverlappingPixels(ByVal pobjBox1 As Rectangle, ByVal pobjBox2 As Rectangle, ByVal pobjBackground As Color, ByVal pobjImage As Bitmap) As Boolean
        Dim objIntersection As Rectangle

        If pobjBox1.IntersectsWith(pobjBox2) Then
            objIntersection = Rectangle.Intersect(pobjBox1, pobjBox2)
            For x As Integer = objIntersection.X To objIntersection.Right
                For y As Integer = objIntersection.Y To objIntersection.Bottom
                    If pobjImage.GetPixel(x, y) <> pobjBackground Then
                        Return True
                    End If
                Next
            Next

        End If

        If ForkandBeard.Util.Geometry.GeometryHelper.GetXGapBetweenRectangles(pobjBox1, pobjBox2) <= frmMain.DistanceBetweenTiles Then

            For y As Integer = pobjBox1.Y - frmMain.DistanceBetweenTiles To pobjBox1.Bottom + frmMain.DistanceBetweenTiles
                If y >= pobjBox2.Top _
                AndAlso y <= pobjBox2.Bottom Then

                    If pobjBox2.Left > pobjBox1.Right Then
                        If pobjImage.GetPixel(pobjBox1.Right, y) <> pobjBackground Then
                            If pobjImage.GetPixel(pobjBox2.Left, y) <> pobjBackground Then
                                Return True
                            End If
                        End If
                    Else
                        If pobjImage.GetPixel(pobjBox1.Left, y) <> pobjBackground Then
                            If pobjImage.GetPixel(pobjBox2.Right, y) <> pobjBackground Then
                                Return True
                            End If
                        End If
                    End If

                End If
            Next
        End If

        If ForkandBeard.Util.Geometry.GeometryHelper.GetYGapBetweenRectangles(pobjBox1, pobjBox2) <= frmMain.DistanceBetweenTiles Then

            For x As Integer = pobjBox1.Left - frmMain.DistanceBetweenTiles To pobjBox1.Right + frmMain.DistanceBetweenTiles
                If x >= pobjBox2.Left _
                AndAlso x <= pobjBox2.Right Then

                    If pobjBox2.Top > pobjBox1.Bottom Then
                        If pobjImage.GetPixel(x, pobjBox1.Bottom) <> pobjBackground Then
                            If pobjImage.GetPixel(x, pobjBox2.Top) <> pobjBackground Then
                                Return True
                            End If
                        End If
                    Else
                        If pobjImage.GetPixel(x, pobjBox1.Top) <> pobjBackground Then
                            If pobjImage.GetPixel(x, pobjBox2.Bottom) <> pobjBackground Then
                                Return True
                            End If
                        End If
                    End If
                End If
            Next
        End If

        Return False
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
                If Me.Image IsNot Nothing Then
                    'Me.Image.Dispose()
                End If
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
