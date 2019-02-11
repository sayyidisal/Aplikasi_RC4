Imports System.Data.OleDb
Module Module1

    Public username As String
    Public pass As String
    Public autono As String
    Public kesalahan As Integer
    Public periode As String

    Public set_koneksi As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Application.StartupPath & "\Database1.mdb")

    Public Sub ManageConnection()
        If set_koneksi.State = ConnectionState.Closed Then
            set_koneksi.Open()
        End If
    End Sub

    Public Sub dbConnect()
        set_koneksi.Open()
        Dim cari As New OleDbCommand("select Username,Password from Tabel_User where Username='" & username & "'", set_koneksi)
        Dim ketemu As String = cari.ExecuteScalar
        If ketemu = Nothing Then
            MsgBox("Maaf... User Tidak Terdaftar", MsgBoxStyle.Exclamation, "Perhatian...!!!")
            kesalahan = kesalahan + 1
            set_koneksi.Close()
            FormLogin.txtuser.Text = ""
            FormLogin.txtpass.Text = ""
            FormLogin.txtuser.Focus()
            Exit Sub
        Else
            Dim Data As OleDbDataReader = cari.ExecuteReader(CommandBehavior.CloseConnection)
            Data.Read()
            If pass = Data.Item("Password").ToString Then
                set_koneksi.Close()
                FormMenu.Show()
                FormLogin.Hide()
            Else
                MsgBox("Maaf... Password Anda Salah", MsgBoxStyle.Exclamation, "Perhatian...!!!")
                kesalahan = kesalahan + 1
                set_koneksi.Close()
                FormLogin.txtpass.Text = ""
                FormLogin.txtuser.Focus()
                Exit Sub
            End If
            Exit Sub
        End If
        Exit Sub
    End Sub

End Module
