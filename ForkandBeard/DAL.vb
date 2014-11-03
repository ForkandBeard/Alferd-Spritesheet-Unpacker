
#Region " Options "
Option Explicit On
Option Strict On
#End Region

Imports System.Configuration

Public Class DAL

    Public Shared Function ExecuteStoredProcedure(ByVal pobjCommand As Data.SqlClient.SqlCommand) As Data.DataTable
        Dim objReturn As Data.DataTable

        Using con As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("db_conn").ConnectionString)
            pobjCommand.Connection = con
            con.Open()
            objReturn = New Data.DataTable
            objReturn.Load(pobjCommand.ExecuteReader(Data.CommandBehavior.CloseConnection))
        End Using

        Return objReturn
    End Function

    Public Shared Function ExecuteStoredProcedureScalar(ByVal pobjCommand As Data.SqlClient.SqlCommand) As Object
        Using con As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("db_conn").ConnectionString)
            pobjCommand.Connection = con
            con.Open()

            Return pobjCommand.ExecuteScalar()
        End Using
    End Function
End Class
