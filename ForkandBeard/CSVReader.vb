#Region " Options "
Option Explicit On
Option Strict On
#End Region

Public Class CSVReader : Implements IDisposable

    ''' <summary>
    ''' Non zero-indexed index.
    ''' </summary>
    ''' <remarks></remarks>
    Public PresentRowIndex As Integer
    Private RowData As Dictionary(Of Integer, List(Of String))
    Private TextReader As IO.TextReader
    Private FileStream As IO.FileStream

    Public Sub New(ByVal pstrFilePathAndName As String)
        If Not My.Computer.FileSystem.FileExists(pstrFilePathAndName) Then
            Throw New ArgumentException(String.Format("File {0} does exist.", pstrFilePathAndName))
        End If

        Dim objFile As IO.FileInfo

        objFile = My.Computer.FileSystem.GetFileInfo(pstrFilePathAndName)

        If Not objFile.Extension = ".csv" Then
            Throw New ArgumentException(String.Format("File {0} is not a csv file.", pstrFilePathAndName))
        End If

        Me.FileStream = objFile.OpenRead
        Me.TextReader = New IO.StreamReader(Me.FileStream)
        Me.PresentRowIndex = 0
        Me.RowData = New Dictionary(Of Integer, List(Of String))
    End Sub

    Public ReadOnly Property IsEndOfFile() As Boolean
        Get
            Return Me.TextReader.Peek() = -1
        End Get
    End Property

    Public Sub MoveToNextRow()
        Me.PresentRowIndex += 1
        Me.RowData.Add(Me.PresentRowIndex, New List(Of String)(Me.TextReader.ReadLine().Split(","c)))
    End Sub

    Public Function GetPresentRow() As List(Of String)
        If Me.PresentRowIndex = 0 Then
            If Me.IsEndOfFile Then
                Throw New Exception("No rows in file.")
            End If
            Me.MoveToNextRow()
        End If
        Return Me.RowData(Me.PresentRowIndex)
    End Function

    Public Function GetPresentRowsLastNonEmptyColumnIndex() As Integer
        Dim colPresentRow As List(Of String)
        colPresentRow = Me.GetPresentRow

        For i As Integer = colPresentRow.Count - 1 To 0 Step -1
            If Not String.IsNullOrEmpty(colPresentRow(i)) Then
                Return i
            End If
        Next
    End Function

    Public Function GetPresentRowsFirstNonEmptyColumnIndex() As Integer
        Dim colPresentRow As List(Of String)
        colPresentRow = Me.GetPresentRow

        For i As Integer = 0 To colPresentRow.Count
            If Not String.IsNullOrEmpty(colPresentRow(i)) Then
                Return i
            End If
        Next
    End Function

    Public Function GetRow(ByVal pintIndex As Integer) As List(Of String)
        If Me.PresentRowIndex < pintIndex Then
            While Not Me.IsEndOfFile AndAlso Me.PresentRowIndex < pintIndex
                Me.MoveToNextRow()
            End While
        End If

        If Me.PresentRowIndex < pintIndex Then
            Throw New Exception("Row does not exist.")
        End If

        Return Me.RowData(pintIndex)
    End Function

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free other state (managed objects).
                If Me.TextReader IsNot Nothing Then
                    Me.TextReader.Close()
                    Me.TextReader.Dispose()
                End If
                If Me.FileStream IsNot Nothing Then
                    Me.FileStream.Close()
                    Me.FileStream.Dispose()
                End If
            End If

            ' TODO: free your own state (unmanaged objects).
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

