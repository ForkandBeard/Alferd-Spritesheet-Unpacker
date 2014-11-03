#Region " Options "
Option Explicit On
Option Strict On
#End Region

Imports System.Security.Cryptography

Public Class Hashcodes

    ''' <summary>
    ''' This Hashcode will clash quite a lot but adequete for certain situations.
    ''' </summary>
    Public Shared Function ToHashcode(ByVal pstrText As String) As Integer
        Dim intReturn As Integer
        Try
            For i As Integer = 0 To pstrText.Length - 1
                intReturn += (i * 255) + Asc(pstrText.Substring(i, 1))
            Next
        Catch ex As OverflowException
            intReturn = Integer.MaxValue - pstrText.Length
        End Try
        Return intReturn
    End Function

    Public Shared Function ToMD5Hashcode(ByVal pstrText As String) As String
        Dim strReturn As String
        Dim objMD5Crypto As MD5CryptoServiceProvider
        Dim arrValues() As Byte
        Dim arrHash() As Byte

        objMD5Crypto = New MD5CryptoServiceProvider
        arrValues = System.Text.Encoding.UTF8.GetBytes(pstrText)
        arrHash = objMD5Crypto.ComputeHash(arrValues)

        objMD5Crypto.Clear()
        strReturn = Convert.ToBase64String(arrHash)

        Return strReturn
    End Function

    Public Shared Function FileToMD5HashCode(ByVal pstrFileName As String) As String
        Dim arrHash() As Byte
        Using objReader As New System.IO.FileStream(pstrFileName, IO.FileMode.Open, IO.FileAccess.Read)
            Using objMD5Crypto As New System.Security.Cryptography.MD5CryptoServiceProvider

                arrHash = objMD5Crypto.ComputeHash(objReader)

                Return ByteArrayToString(arrHash)
            End Using
        End Using
    End Function

    Private Shared Function ByteArrayToString(ByVal parrInput() As Byte) As String

        Dim sb As New System.Text.StringBuilder(parrInput.Length * 2)

        For i As Integer = 0 To parrInput.Length - 1
            sb.Append(parrInput(i).ToString("X2"))
        Next

        Return sb.ToString().ToLower
    End Function
End Class
