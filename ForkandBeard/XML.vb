#Region " Options "
Option Explicit On
Option Strict On
#End Region

Imports System.Xml.Serialization

Public Class XML

    Public Shared Sub SerialiseObject(ByVal pobjSerialisable As Object, ByVal pstrPath As String)

        Dim objXMLSerialiser As XmlSerializer
        Dim objFileStream As IO.FileStream = Nothing

        Try
            objXMLSerialiser = New XmlSerializer(pobjSerialisable.GetType)
            objFileStream = New IO.FileStream(pstrPath, IO.FileMode.Create)
            objXMLSerialiser.Serialize(objFileStream, pobjSerialisable)
        Catch ex As Exception
            Throw
        Finally

            If objFileStream IsNot Nothing Then
                objFileStream.Close()
                objFileStream.Dispose()
            End If
        End Try

    End Sub

    Public Shared Function DeserialiseObject(ByVal pstrPath As String, ByVal pobjType As System.Type) As Object

        Dim objXMLSerialiser As XmlSerializer
        Dim objFileStream As IO.FileStream = Nothing

        Try
            objXMLSerialiser = New XmlSerializer(pobjType)
            objFileStream = New IO.FileStream(pstrPath, IO.FileMode.Open)

            Return objXMLSerialiser.Deserialize(objFileStream)
        Catch ex As Exception
            Throw
        Finally

            If objFileStream IsNot Nothing Then
                objFileStream.Close()
                objFileStream.Dispose()
            End If
        End Try

    End Function

End Class
