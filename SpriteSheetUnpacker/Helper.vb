#Region " Options "
Option Strict On
Option Explicit On
#End Region

Public Class Helper

    Public Shared Function GetThirdPartyConversionToolFullPath() As String
        Dim strReturn As String

        strReturn = Configuration.ConfigurationManager.AppSettings("ThirdPartyImageConverter")
        If strReturn.StartsWith("\") Then
            strReturn = My.Application.Info.DirectoryPath & strReturn
        End If

        Return strReturn
    End Function

    Public Shared Function GetThirdPartyConversionToolExecutableName() As String
        Return IO.Path.GetFileName(GetThirdPartyConversionToolFullPath())
    End Function

    Public Shared Function GetThirdPartyConversionToolDirectory() As String
        Return IO.Path.GetDirectoryName(GetThirdPartyConversionToolFullPath())
    End Function
End Class
