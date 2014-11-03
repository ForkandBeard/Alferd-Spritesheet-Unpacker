#Region " Options "
Option Explicit On
Option Strict On
#End Region

Public Class SuperBase

    Private Chars As String
    Private Colours As List(Of Drawing.Color)

    Public Sub New(ByVal pstrChars As String, ByVal pcolColours As List(Of Drawing.Color))
        Me.Chars = pstrChars
        Me.Colours = pcolColours
    End Sub

    Public ReadOnly Property BaseNumber() As Integer
        Get
            Return Chars.Length * Me.Colours.Count
        End Get
    End Property

    Public Function ConvertToInteger(ByVal pstrSuperBase As String) As Integer

    End Function

    Public Function ConvertToSuperBase(ByVal pintValue As Integer) As List(Of IndexAndColour)
        Dim colReturn As List(Of IndexAndColour) = New List(Of IndexAndColour)
        Dim objColour As Drawing.Color
        Dim strChar As String

        'If pintValue <= Me.BaseNumber Then
        If pintValue < Me.Chars.Length Then
            strChar = Me.Chars.Substring(pintValue, 1)
            objColour = Me.Colours(0)
        Else

            Dim intBase As Integer = Me.BaseNumber
            Do While intBase < pintValue
                intBase *= intBase
            Loop

            intBase = CInt(Math.Floor(intBase / Me.BaseNumber))

            Do

                If pintValue < CInt(Math.Floor(intBase / 2)) Then
                    colReturn.Add(New IndexAndColour(Me.Colours(0), Me.Chars.Substring(0, 1)))
                Else

                    Dim intBases As Integer
                    intBases = CInt(Math.Floor(pintValue / Me.BaseNumber))

                    Dim intV As Integer
                    intV = CInt(Math.Floor(intBases / Me.BaseNumber))

                    objColour = Me.Colours(intV)

                    If intBases = 0 Then
                        strChar = Me.Chars.Substring(pintValue - CInt(Math.Floor(pintValue / Me.Chars.Length) * Me.Chars.Length), 1)
                    Else
                        strChar = Me.Chars.Substring(intBases - (Me.Chars.Length * intV), 1)
                    End If
                    colReturn.Add(New IndexAndColour(objColour, strChar))

                    pintValue -= intBases * Me.BaseNumber
                End If

                intBase = CInt(Math.Floor(intBase / Me.BaseNumber))

            Loop While intBase > 0
        End If
        'End If

        Return colReturn
    End Function

    Public Class IndexAndColour
        Public Colour As Drawing.Color
        Public IndexChar As String

        Public Sub New(ByVal pobjColour As Drawing.Color, ByVal pstrIndexChar As String)
            Me.IndexChar = pstrIndexChar
            Me.Colour = pobjColour
        End Sub
    End Class
End Class
