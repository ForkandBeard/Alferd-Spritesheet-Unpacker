#Region " Options "
Option Explicit On
Option Strict On
#End Region

Imports System.Text.RegularExpressions

Public Class Text
    Public Shared Function IsURL(ByVal pstrURL As String) As Boolean
        Dim blnHTTP As Boolean
        Dim blnFTP As Boolean
        Dim blnWWW As Boolean

        If String.IsNullOrEmpty(pstrURL) Then
            Return False
        End If

        If pstrURL.Length < 8 Then
            Return False
        End If

        pstrURL = pstrURL.ToLower

        blnHTTP = pstrURL.Substring(0, 7) = "http://" OrElse pstrURL.Substring(0, 8) = "https://"
        blnFTP = pstrURL.Substring(0, 4) = "ftp:"
        blnWWW = pstrURL.Substring(0, 4) = "www."

        Return blnHTTP OrElse blnFTP OrElse blnWWW
    End Function

    Public Shared Function DoesStringContainDigits(ByVal pstrString As String) As Boolean
        Return Regex.IsMatch(pstrString, "[0-9]+")
    End Function

    Public Shared Function DoesStringContainUpperAndLower(ByVal pstrString As String) As Boolean
        Return pstrString.ToLower <> pstrString _
                AndAlso pstrString.ToUpper <> pstrString
    End Function

    Public Shared Function ConvertToAlphaNumericString(ByVal pstrString As String) As String
        Return Regex.Replace(pstrString, "[\W]", "")
    End Function

    ''' <remarks>
    ''' [25/05/2012] MitchellCooper   Created.
    ''' </remarks>
    Public Shared Function NumberWithCommas(ByVal pdecNumber As Decimal, ByVal pblnIncludeDecimal As Boolean) As String
        If pblnIncludeDecimal Then
            Return String.Format("{0:n}", pdecNumber)
        Else
            Return String.Format("{0:n0}", pdecNumber)
        End If
    End Function
End Class
