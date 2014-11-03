#Region " Options "
Option Explicit On
Option Strict On
#End Region

Public Class Dates

    Public Shared Function GetDateDayNth(ByVal pdteDate As Date, ByVal pblnTrimDayName As Boolean) As String
        Dim strFormat As String
        Dim strDay As String

        If pblnTrimDayName Then
            strFormat = "ddd dd"
        Else
            strFormat = "dddd dd"
        End If

        strDay = pdteDate.Day.ToString

        If strDay.Equals("11") _
        OrElse strDay.Equals("12") _
        OrElse strDay.Equals("13") Then
            Return pdteDate.ToString(strFormat) & "th"
        End If

        Select Case strDay.Substring(strDay.Length - 1, 1)
            Case "1"
                Return pdteDate.ToString(strFormat) & "st"
            Case "2"
                Return pdteDate.ToString(strFormat) & "nd"
            Case "3"
                Return pdteDate.ToString(strFormat) & "rd"
            Case Else
                Return pdteDate.ToString(strFormat) & "th"
        End Select
    End Function

    Public Shared Function GetTimeSpanDescription(ByVal pintSeconds As Integer) As String
        Dim strReturn As String = String.Empty

        If pintSeconds < 60 Then
            strReturn = pintSeconds.ToString & " secs"
        ElseIf pintSeconds < 3600 Then
            strReturn = Math.Round((pintSeconds / 60), 2).ToString & " minutes"
        ElseIf pintSeconds < 86400 Then
            strReturn = Math.Round(((pintSeconds / 60) / 60), 2).ToString & " hours"
        Else
            strReturn = Math.Round(((pintSeconds / 60) / 60) / 24, 2).ToString & " days"
        End If

        Return strReturn
    End Function
End Class
