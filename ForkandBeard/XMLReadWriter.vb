#Region " Options "
Option Explicit On
Option Strict On
#End Region

Imports System.Xml.Serialization

Public Class XMLReadWriter

    Private Shared LockCollection As Object = New Object
    Private Shared FileLocks As Dictionary(Of String, Object) = New Dictionary(Of String, Object)

    Public Shared Function SerialiseObject(ByVal pobjSerialisable As Object) As String
        Dim objXMLSerialiser As XmlSerializer
        Dim strReturn As String
        Dim objStringWriter As IO.StringWriter

        objStringWriter = New IO.StringWriter()

        objXMLSerialiser = New XmlSerializer(pobjSerialisable.GetType)
        objXMLSerialiser.Serialize(objStringWriter, pobjSerialisable)

        strReturn = objStringWriter.ToString

        Return strReturn
    End Function

    Public Shared Sub SerialiseObject(ByVal pobjSerialisable As Object, ByVal pstrPath As String, ByVal pblnExceptionIfExists As Boolean)
        SerialiseObject(pobjSerialisable, pstrPath, pblnExceptionIfExists, False)
    End Sub

    Public Shared Sub SerialiseObject(ByVal pobjSerialisable As Object, ByVal pstrPath As String, ByVal pblnExceptionIfExists As Boolean, ByVal pblnSimpleEncrypt As Boolean)

        If pblnExceptionIfExists _
        AndAlso IO.File.Exists(pstrPath) Then
            Throw New IO.IOException(String.Format("File {0} already exists.", pstrPath))
        End If

        Dim objXMLSerialiser As XmlSerializer
        Dim objXMLWriter As System.Xml.XmlWriter = Nothing
        Dim objXMLSettings As System.Xml.XmlWriterSettings = New System.Xml.XmlWriterSettings()
        Dim objLock As Object

        objXMLSettings.NewLineHandling = System.Xml.NewLineHandling.Entitize
        Try

            objXMLSerialiser = New XmlSerializer(pobjSerialisable.GetType)

            SyncLock (LockCollection)
                If Not FileLocks.ContainsKey(pstrPath) Then
                    FileLocks.Add(pstrPath, New Object)
                End If
            End SyncLock

            objLock = FileLocks(pstrPath)

            SyncLock (objLock)
                objXMLWriter = System.Xml.XmlWriter.Create(pstrPath, objXMLSettings)
                objXMLSerialiser.Serialize(objXMLWriter, pobjSerialisable)
            End SyncLock

        Finally
            If objXMLWriter IsNot Nothing Then
                objXMLWriter.Close()
            End If
        End Try

        Try
            If pblnSimpleEncrypt Then
                SyncLock (objLock)
                    Dim arrBytes As Byte()

                    arrBytes = IO.File.ReadAllBytes(pstrPath)

                    IO.File.Delete(pstrPath)

                    Using s As IO.FileStream = New IO.FileStream(pstrPath, IO.FileMode.CreateNew, IO.FileAccess.Write)

                        For k As Integer = 0 To arrBytes.Length() - 1
                            s.WriteByte(arrBytes(k))
                            If k > 5 Then

                                If k Mod 15 = 0 Then
                                    s.WriteByte(60) ' <.

                                ElseIf k Mod 12 = 0 Then
                                    s.WriteByte(62) ' >.

                                ElseIf k Mod 9 = 0 Then
                                    s.WriteByte(47) ' /.

                                ElseIf k Mod 6 = 0 Then
                                    s.WriteByte(57) ' 9.

                                ElseIf k Mod 3 = 0 Then
                                    s.WriteByte(49) ' 1.
                                End If
                            End If
                        Next
                    End Using
                End SyncLock
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
        End Try
    End Sub

    Public Shared Function DeserialiseObjectFromString(ByVal pstrSerialised As String, ByVal pobjType As System.Type) As Object
        Dim objXMLSerialiser As XmlSerializer
        Dim objStringReader As IO.StringReader

        objStringReader = New IO.StringReader(pstrSerialised)

        objXMLSerialiser = New XmlSerializer(pobjType)
        Return objXMLSerialiser.Deserialize(objStringReader)
    End Function

    Public Shared Function DeserialiseObject(ByVal pstrPath As String, ByVal pobjType As System.Type) As Object
        Return DeserialiseObject(pstrPath, pobjType, False)
    End Function

    Public Shared Function DeserialiseObject(ByVal pstrPath As String, ByVal pobjType As System.Type, ByVal pblnDecryptSimpleEncrypt As Boolean) As Object
        Dim objXMLSerialiser As XmlSerializer
        Dim objFileStream As IO.FileStream = Nothing
        Dim objReturn As Object = Nothing
        objXMLSerialiser = New XmlSerializer(pobjType)

        SyncLock (LockCollection)
            If Not FileLocks.ContainsKey(pstrPath) Then
                FileLocks.Add(pstrPath, New Object)
            End If
        End SyncLock

        Dim objLock As Object = FileLocks(pstrPath)

        SyncLock (objLock)
            Try
                If pblnDecryptSimpleEncrypt Then
                    Dim arrBytes As Byte()
                    Dim intCounter As Integer = 1
                    arrBytes = IO.File.ReadAllBytes(pstrPath)

                    IO.File.Delete(pstrPath)

                    Using s As IO.FileStream = New IO.FileStream(pstrPath, IO.FileMode.CreateNew, IO.FileAccess.Write)

                        For k As Integer = 0 To 3
                            s.WriteByte(arrBytes(k))
                        Next

                        For k As Integer = 4 To arrBytes.Length() - 1
                            If intCounter <> 4 Then
                                s.WriteByte(arrBytes(k))
                                intCounter += 1
                            Else
                                intCounter = 1
                            End If
                        Next
                    End Using
                End If

                objFileStream = New IO.FileStream(pstrPath, IO.FileMode.Open, IO.FileAccess.Read)
                objReturn = objXMLSerialiser.Deserialize(objFileStream)
            Finally

                If objFileStream IsNot Nothing Then
                    objFileStream.Close()
                    objFileStream.Dispose()
                End If

                If pblnDecryptSimpleEncrypt Then
                    If objReturn IsNot Nothing Then
                        SerialiseObject(objReturn, pstrPath, False, True)
                    End If
                End If
            End Try

            Return objReturn
        End SyncLock
    End Function

End Class
