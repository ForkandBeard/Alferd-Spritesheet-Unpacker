Public Class frmNConvertGuide

    Private f As Boolean = True

    Private Sub frmNConvertGuide_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.txtMain.Text = My.Resources.Advanced_Export_File_Format_Help.Replace("{0}", My.Application.Info.DirectoryPath & "\NConvert")
    End Sub

    Private Sub txtMain_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMain.GotFocus
        If f Then
            Me.Label1.Focus()
            f = False
        End If        
    End Sub
End Class