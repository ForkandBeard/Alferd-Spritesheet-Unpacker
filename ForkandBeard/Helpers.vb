#Region " Options "
Option Explicit On
Option Strict On
#End Region

Public Class Helpers

    Public Shared Function VBDateToDBDateTime(ByVal pdteDate As Date) As Object
        If pdteDate = Date.MinValue Then
            Return DBNull.Value
        Else
            Return pdteDate
        End If
    End Function

    Public Shared Function DBTimeToVBTimeSpan(ByVal pobjDBValue As Object, ByVal pobjResultIfNull As TimeSpan) As TimeSpan
        If pobjDBValue Is Nothing _
        OrElse pobjDBValue.GetType Is GetType(DBNull) Then
            Return pobjResultIfNull
        Else
            Return CType(pobjDBValue, TimeSpan)
        End If
    End Function

    Public Shared Function DBDateToVBDate(ByVal pobjDBValue As Object, ByVal pdteResultIfNull As Date) As Date
        If pobjDBValue Is Nothing _
        OrElse pobjDBValue.GetType Is GetType(DBNull) Then
            Return pdteResultIfNull
        Else
            Return CDate(pobjDBValue)
        End If
    End Function

    Public Shared Function DBHexVarcharToVBColour(ByVal pobjDBValue As Object, ByVal pobjResultIfNull As Drawing.Color) As Drawing.Color
        Dim objConverter As Drawing.ColorConverter = New Drawing.ColorConverter()

        If pobjDBValue Is Nothing _
        OrElse pobjDBValue.GetType Is GetType(DBNull) Then
            Return pobjResultIfNull
        Else
            If String.IsNullOrEmpty(pobjDBValue.ToString) Then
                Return Drawing.Color.FromArgb(0, 0, 0, 0)
            Else
                Return CType(objConverter.ConvertFromString(Convert.ToInt32(pobjDBValue.ToString, 16).ToString), Drawing.Color)
            End If
        End If
    End Function

    Public Shared Function DBXMLToVBObject(ByVal pobjDBValue As Object, ByVal pobjResultIfNull As Object) As Object
        If pobjDBValue Is Nothing _
        OrElse pobjDBValue.GetType Is GetType(DBNull) Then
            Return pobjResultIfNull
        Else
            Return XMLReadWriter.DeserialiseObjectFromString(CStr(pobjDBValue), GetType(Object))
        End If
    End Function

    Public Shared Function DBVarcharToVBString(ByVal pobjDBValue As Object, ByVal pstrResultIfNull As String) As String
        If pobjDBValue Is Nothing _
        OrElse pobjDBValue.GetType Is GetType(DBNull) Then
            Return pstrResultIfNull
        Else
            Return CStr(pobjDBValue)
        End If
    End Function

    Public Shared Function DBUniqueIdentifierToVBGUID(ByVal pobjDBValue As Object, ByVal pobjResultIfNull As Guid) As Guid
        If pobjDBValue Is Nothing _
        OrElse pobjDBValue.GetType Is GetType(DBNull) Then
            Return pobjResultIfNull
        Else
            Return New Guid(pobjDBValue.ToString)
        End If
    End Function

    Public Shared Function DBNumericToVBDecimal(ByVal pobjDBValue As Object, ByVal pintResultIfNull As Decimal) As Decimal
        If pobjDBValue Is Nothing _
        OrElse pobjDBValue.GetType Is GetType(DBNull) Then
            Return pintResultIfNull
        Else
            Return CDec(pobjDBValue)
        End If
    End Function

    Public Shared Function DBIntegerToVBInteger(ByVal pobjDBValue As Object, ByVal pintResultIfNull As Integer) As Integer
        If pobjDBValue Is Nothing _
        OrElse pobjDBValue.GetType Is GetType(DBNull) Then
            Return pintResultIfNull
        Else
            Return CInt(pobjDBValue)
        End If
    End Function

    Public Shared Function DBBitToVBBool(ByVal pobjDBValue As Object, ByVal pblnResultIfNull As Boolean) As Boolean
        If pobjDBValue Is Nothing _
        OrElse pobjDBValue.GetType Is GetType(DBNull) Then
            Return pblnResultIfNull
        Else
            Return CBool(pobjDBValue)
        End If
    End Function

    Public Shared Function GetListOfNullOrEmptyColumnNames(ByVal pobjTable As DataTable) As List(Of String)
        Dim colReturn As List(Of String) = New List(Of String)

        For c As Integer = 0 To pobjTable.Columns.Count - 1
            colReturn.Add(pobjTable.Columns(c).ColumnName)
        Next

        For Each objDataRow As Data.DataRow In pobjTable.Rows
            For c As Integer = 0 To pobjTable.Columns.Count - 1
                If colReturn.Contains(pobjTable.Columns(c).ColumnName) Then
                    If Not objDataRow.IsNull(c) _
                    AndAlso Not String.IsNullOrEmpty(objDataRow.Item(c).ToString) Then
                        colReturn.Remove(pobjTable.Columns(c).ColumnName)
                    End If
                End If
            Next
        Next

        Return colReturn
    End Function

    Public Shared Function SQLDataReaderReadToEnd(ByVal pobjSQLDataReader As Data.Common.DbDataReader) As String
        Dim strReturn As String = String.Empty

        If pobjSQLDataReader IsNot Nothing _
        AndAlso pobjSQLDataReader.HasRows() Then
            While pobjSQLDataReader.Read()
                strReturn &= pobjSQLDataReader.GetString(0)
            End While
        End If

        Return strReturn
    End Function
End Class
