#Region " Options "
Option Explicit On
Option Strict On
#End Region

Imports System.Drawing

Public Class RandomHelper
    Private Shared _Random As Random = New Random

    Public Shared Function Random(ByVal psngMin As Single, ByVal psngMax As Single) As Single
        Return CSng(psngMin + (_Random.NextDouble * (psngMax - psngMin)))
    End Function

    Public Shared Function DeterministicRandom(ByVal psngMin As Single, ByVal psngMax As Single, ByVal pintSeed As Integer) As Single
        Dim sngSeed As Single

        sngSeed = pintSeed / 100.0F

        If psngMax = psngMin Then
            Return psngMax
        End If

        If sngSeed < psngMax Then
            sngSeed = CInt(CInt((sngSeed + 0.5) + (Math.Abs(Math.Tan(psngMin + 0.76333)))) * (psngMax + 10))
        End If

        If (psngMax - psngMin) = 1 Then
            If sngSeed Mod 2 = 0 Then
                Return psngMax
            Else
                Return psngMin
            End If
        End If

        Dim sngDiff As Single
        Dim sngReturn As Single
        Dim s1 As Single
        Dim s2 As Single
        Dim s3 As Single

        sngDiff = (psngMax - psngMin)
        s1 = sngSeed / sngDiff
        s2 = sngDiff * CInt(s1)
        s3 = sngSeed - s2
        sngReturn = psngMin + s3

        Return sngReturn
    End Function

    Public Shared Function DeterministicRandom(ByVal pintMin As Integer, ByVal pintMax As Integer, ByVal pintSeed As Integer) As Integer

        If pintMax = pintMin Then
            Return pintMax
        End If

        If pintSeed < pintMax Then
            Try
                pintSeed = CInt((pintSeed + 0.5) + (Math.Abs(Math.Tan(pintMin + 0.76333)))) * (pintMax + 10)
            Catch ex As Exception
                pintSeed = CInt(pintMax / 2)
            End Try            
        End If

        If (pintMax - pintMin) = 1 Then
            If pintSeed Mod 2 = 0 Then
                Return pintMax
            Else
                Return pintMin
            End If
        End If

        Dim intReturn As Integer
        intReturn = (pintMin + (pintSeed Mod ((pintMax + 1) - pintMin)))

        Return intReturn
    End Function

    Public Shared Function PointToSeed(ByVal pobjPoint As Point) As Integer
        Dim out As String = String.Empty
        Dim oct1 As String
        Dim oct2 As String
        Dim bin1 As String
        Dim bin2 As String

        If pobjPoint.Y < 0 OrElse pobjPoint.X < 0 Then
            bin1 = "1"
        End If

        bin1 = Convert.ToString(pobjPoint.X, 2)
        oct1 = Convert.ToString(pobjPoint.X, 8)
        bin2 = Convert.ToString(pobjPoint.Y, 2)
        oct2 = Convert.ToString(pobjPoint.Y, 8)

        If pobjPoint.X Mod 2 = 0 Then
            out = bin1 & oct2
        Else
            out = oct1 & bin2
        End If

        If pobjPoint.Y Mod 2 = 0 Then
            out &= bin2 & oct1
        Else
            out &= oct2 & bin1
        End If

        Dim newOut As String = String.Empty
        Dim next3 As String
        Dim counter As Integer
        Dim total As Integer

        Do While out.Length > 6
            counter = 0
            newOut = String.Empty

            Do While counter < out.Length - 1
                next3 = String.Empty
                Do While counter < out.Length - 1 AndAlso next3.Length < 3
                    next3 &= out.Substring(counter, 1)
                    counter += 1
                Loop

                total = Convert.ToInt32(next3, 16)
                newOut &= Convert.ToString(CInt(total / 1.75), 16)
            Loop
            out = newOut
        Loop

        Return Convert.ToInt32(out, 16)
    End Function
End Class
