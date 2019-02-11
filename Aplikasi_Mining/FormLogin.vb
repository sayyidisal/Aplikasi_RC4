Imports System.Data.OleDb

Public Class FormLogin

    Private Sub BtnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLogin.Click
        If kesalahan = 3 Then
            MsgBox("Anda telah melakukan kesalahan 3 kali. Program Akan Ditutup !", MsgBoxStyle.Exclamation)
            End
            Exit Sub
        End If

        Module1.username = Trim(txtuser.Text)
        Module1.pass = Trim(txtpass.Text)
        dbConnect()
    End Sub

    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        End
    End Sub

    Private Sub FormLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        kesalahan = 0
    End Sub
End Class
