#Region " Options "
Option Explicit On
Option Strict On
#End Region

Public Class EnumHelper

    Public Shared Function EnumToList(ByVal peEnum As Type) As List(Of EnumKeyValueIntString)
        Dim arrValues As Integer()
        Dim arrKeys As String()
        Dim colReturn As List(Of EnumKeyValueIntString) = New List(Of EnumKeyValueIntString)

        arrValues = CType(System.[Enum].GetValues(peEnum), Integer())
        arrKeys = CType(System.[Enum].GetNames(peEnum), String())

        For i As Integer = 0 To arrValues.Length - 1

            colReturn.Add(New EnumKeyValueIntString(arrValues(i), arrKeys(i)))
        Next

        Return colReturn
    End Function

    Public Shared Function TryParse(ByVal pobjEnumType As Type, ByVal pstrEnumText As String) As Boolean
        Dim colEnumList As List(Of EnumKeyValueIntString)
        colEnumList = EnumToList(pobjEnumType)

        For Each objEnum As EnumKeyValueIntString In colEnumList
            If objEnum.Key.ToString.ToLower.Equals(pstrEnumText.ToLower) Then
                Return True
            End If
        Next

        Return False
    End Function

    Public Class EnumKeyValueIntString
        Public Value As Integer
        Public Key As String

        Public Sub New(ByVal pintValue As Integer, ByVal pstrKey As String)
            Me.Value = pintValue
            Me.Key = pstrKey
        End Sub

    End Class

End Class
