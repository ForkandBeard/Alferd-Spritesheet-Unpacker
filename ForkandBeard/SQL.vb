#Region " Options "
Option Explicit On
Option Strict On
#End Region

''' <remarks>
''' [30/04/2012] MitchellCooper   Added code regions for different return types and added DataReader return type.
''' </remarks>
Public Class SQL

    Private Shared _ConnectionString As String

    Public Shared Sub SetConnectionString(ByVal pstrConnectionString As String)
        _ConnectionString = pstrConnectionString
        If Not _ConnectionString.Contains("Application Name") Then
            _ConnectionString &= ";Application Name=" & My.Application.Info.AssemblyName
        End If
    End Sub

#Region " Dynamic SQL "
    ''' <summary>
    ''' Calling this overload ensures the Connection is not closed.
    ''' </summary>
    Public Shared Function ExecuteSQL(ByVal pstrSQL As String, ByVal pintTimeOut As Integer, ByVal pobjConnection As SqlClient.SqlConnection) As DataSet
        Dim adp As SqlClient.SqlDataAdapter = Nothing
        Dim ds As DataSet = New DataSet()

        Try
            Using cmd As SqlClient.SqlCommand = New SqlClient.SqlCommand(pstrSQL)
                If pobjConnection.State <> ConnectionState.Open Then
                    pobjConnection.Open()
                End If

                cmd.Connection = pobjConnection
                If pintTimeOut <> -1 Then
                    cmd.CommandTimeout = pintTimeOut
                End If
                adp = New SqlClient.SqlDataAdapter()
                adp.SelectCommand = cmd
                adp.Fill(ds)
                Return ds
            End Using
        Finally
            If adp IsNot Nothing Then
                adp.Dispose()
            End If
        End Try
    End Function

    Public Shared Function ExecuteSQL(ByVal pstrSQL As String, ByVal pintTimeOut As Integer) As DataSet
        Dim conn As SqlClient.SqlConnection = Nothing

        Try
            conn = CreateOpenConnection(_ConnectionString)
            Return ExecuteSQL(pstrSQL, pintTimeOut, conn)
        Finally
            If conn IsNot Nothing Then
                If conn.State <> ConnectionState.Closed Then
                    conn.Close()
                End If
                conn.Dispose()
            End If
        End Try
    End Function

    Public Shared Function ExecuteSQL(ByVal pstrSQL As String) As DataSet
        Return ExecuteSQL(pstrSQL, -1)
    End Function

    ''' <summary>
    ''' Calling this overload ensures the Connection is not closed.
    ''' </summary>
    Public Shared Function ExecuteSQL(ByVal pstrSQL As String, ByVal pobjConnection As SqlClient.SqlConnection) As DataSet
        Return ExecuteSQL(pstrSQL, -1, pobjConnection)
    End Function

#End Region

#Region " Scalar "

    ''' [01/05/2012] MitchellCooper   Overload now calls correct ExecuteStoredProcedureScalar function.
    Public Shared Function ExecuteStoredProcedureScalar(ByVal pobjStoredProcedure As SqlClient.SqlCommand) As Object
        Dim conn As SqlClient.SqlConnection = Nothing

        Try
            conn = CreateOpenConnection(_ConnectionString)
            Return ExecuteStoredProcedureScalar(pobjStoredProcedure, conn)
        Finally
            If conn IsNot Nothing Then
                If conn.State <> ConnectionState.Closed Then
                    conn.Close()
                End If

                conn.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Calling this overload ensures the Connection is not closed.
    ''' </summary>
    Public Shared Function ExecuteStoredProcedureScalar(ByVal pobjStoredProcedure As SqlClient.SqlCommand, ByVal pobjConnection As SqlClient.SqlConnection) As Object
        Dim rdr As SqlClient.SqlDataReader = Nothing

        Try
            Using pobjStoredProcedure
                pobjStoredProcedure.Connection = pobjConnection
                rdr = pobjStoredProcedure.ExecuteReader

                If rdr.Read Then
                    Return rdr(0)
                Else
                    Return Nothing
                End If
            End Using
        Finally

            If rdr IsNot Nothing Then
                rdr.Dispose()
            End If
        End Try
    End Function
#End Region

#Region " NonQuery "
    Public Shared Sub ExecuteStoredProcedureNonQuery(ByVal pobjStoredProcedure As SqlClient.SqlCommand, ByVal pintTimeOut As Integer)
        Dim conn As SqlClient.SqlConnection = Nothing

        Try
            conn = CreateOpenConnection(_ConnectionString)

            Using pobjStoredProcedure
                pobjStoredProcedure.Connection = conn
                If pintTimeOut <> -1 Then
                    pobjStoredProcedure.CommandTimeout = pintTimeOut
                End If
                pobjStoredProcedure.ExecuteNonQuery()
            End Using
        Finally
            If conn IsNot Nothing Then
                If conn.State <> ConnectionState.Closed Then
                    conn.Close()
                End If

                conn.Dispose()
            End If
        End Try
    End Sub

    Public Shared Sub ExecuteStoredProcedureNonQuery(ByVal pobjStoredProcedure As SqlClient.SqlCommand)
        ExecuteStoredProcedureNonQuery(pobjStoredProcedure, -1)
    End Sub
#End Region

#Region " Reader "

    ''' <summary>
    ''' Calling this overload ensures the Connection is not closed.
    ''' </summary>
    ''' <remarks>
    ''' [30/04/2012] MitchellCooper   Created.
    ''' </remarks>
    Public Shared Function ExecuteStoredProcedureReader(ByVal pobjStoredProcedure As SqlClient.SqlCommand, ByVal pobjConnection As SqlClient.SqlConnection) As SqlClient.SqlDataReader
        Dim objReturn As SqlClient.SqlDataReader = Nothing

        Using pobjStoredProcedure
            pobjStoredProcedure.Connection = pobjConnection
            objReturn = pobjStoredProcedure.ExecuteReader

            Return objReturn
        End Using
    End Function

    ''' <remarks>
    ''' [30/04/2012] MitchellCooper   Created.
    ''' [31/05/2012] MitchellCooper   Removed conn.Close() as this also closes the SqlDataReader.
    ''' </remarks>
    Public Shared Function ExecuteStoredProcedureReader(ByVal pobjStoredProcedure As SqlClient.SqlCommand) As SqlClient.SqlDataReader
        Dim conn As SqlClient.SqlConnection = Nothing

        conn = CreateOpenConnection(_ConnectionString)
        Return ExecuteStoredProcedureReader(pobjStoredProcedure, conn)

        ' Don't close connection as this will close reader.
    End Function
#End Region

#Region " DataSet "

    ''' <summary>
    ''' Calling this overload ensures the Connection is not closed.
    ''' </summary>
    Public Shared Function ExecuteStoredProcedure(ByVal pobjStoredProcedure As SqlClient.SqlCommand, ByVal pobjConnection As SqlClient.SqlConnection) As DataSet
        Dim adp As SqlClient.SqlDataAdapter = Nothing
        Dim ds As DataSet = New DataSet()

        Try
            adp = New SqlClient.SqlDataAdapter()
            Using pobjStoredProcedure
                pobjStoredProcedure.Connection = pobjConnection
                adp.SelectCommand = pobjStoredProcedure
                adp.Fill(ds)

                Return ds
            End Using
        Finally
            If adp IsNot Nothing Then
                adp.Dispose()
            End If
        End Try
    End Function

    Public Shared Function ExecuteStoredProcedure(ByVal pobjStoredProcedure As SqlClient.SqlCommand) As DataSet
        Dim conn As SqlClient.SqlConnection = Nothing

        Try
            conn = CreateOpenConnection(_ConnectionString)
            Return ExecuteStoredProcedure(pobjStoredProcedure, conn)
        Finally
            If conn IsNot Nothing Then
                If conn.State <> ConnectionState.Closed Then
                    conn.Close()
                End If

                conn.Dispose()
            End If
        End Try
    End Function
#End Region

    Public Shared Function CreateStoredProcedureSQLCommand(ByVal pstrStoredProcedureName As String) As SqlClient.SqlCommand
        Dim objReturn As SqlClient.SqlCommand

        objReturn = New SqlClient.SqlCommand(pstrStoredProcedureName)
        objReturn.CommandType = Data.CommandType.StoredProcedure

        Return objReturn
    End Function

    Public Shared Function CreateOpenConnection(ByVal pstrConnectionString As String) As SqlClient.SqlConnection
        SetConnectionString(pstrConnectionString)

        Return CreateOpenConnection()
    End Function

    Public Shared Function CreateOpenConnection() As SqlClient.SqlConnection
        Dim objReturn As SqlClient.SqlConnection = Nothing

        If String.IsNullOrEmpty(_ConnectionString) Then
            Throw New Exception("Connection string not set. Please call ForkandBeard.DAL.SQL.SetConnectionString prior to using DAL methods.")
        End If

        objReturn = New SqlClient.SqlConnection(_ConnectionString)
        objReturn.Open()

        Return objReturn
    End Function
End Class

