Imports System.Data.OleDb
Public Class FrmUser

    Private Function ValidAngka(ByVal e As System.Windows.Forms.KeyPressEventArgs) As Boolean
        Dim strValid As String = "0123456789"
        If Strings.InStr(strValid, e.KeyChar) = 0 And Not (e.KeyChar = Strings.Chr(Keys.Back)) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub BtnKeluar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnKeluar.Click
        Close()
    End Sub

    Sub bersih()
        TextBox2.Clear()
        TextBox3.Clear()
        
    End Sub

    Sub otonumber()
        Try
            Dim otono As String = "select * from tabel_pelanggan where noIDpelanggan in(SELECT MAX(noIDpelanggan)from tabel_pelanggan)order by noIDpelanggan"
            set_koneksi.Open()
            Using cmdsql As New OleDbCommand(otono, set_koneksi)
                Using rs As OleDbDataReader = cmdsql.ExecuteReader
                    rs.Read()
                    Dim urutan As String
                    Dim hitung As String
                    With rs
                        If Not .HasRows Then
                            urutan = "K001"
                            TextBox2.Text = urutan
                        Else
                            hitung = Microsoft.VisualBasic.Right(rs("noIDpelanggan"), 3) + 1
                            urutan = Microsoft.VisualBasic.Right("K00" & hitung, 9)
                        End If
                        TextBox2.Text = urutan
                    End With
                    rs.Close()
                    set_koneksi.Close()
                End Using
            End Using
            otono = Nothing
        Catch ex As Exception
            MsgBox(ex.Message, 16, "Eror")
        End Try
    End Sub

    Sub tampil_data()
        Using Koneksi As New OleDbConnection(My.Settings.Setting_Kon)
            Using Cmd As New OleDbCommand("Select * From tabel_user order by NoIDuser", Koneksi)
                Koneksi.Open()
                Using Data As OleDbDataReader = Cmd.ExecuteReader
                    Dim x As Integer = 0
                    Dim no = 1
                    ListView1.Items.Clear()
                    While Data.Read
                        ListView1.Items.Add(no)
                        ListView1.Items(x).SubItems.Add(Data("NoIDuser"))
                        ListView1.Items(x).SubItems.Add(Data("Username"))
                        ListView1.Items(x).SubItems.Add(Data("Password"))
                        ListView1.Items(x).SubItems.Add(Data("Level"))
                        no = no + 1
                        x = x + 1
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub BtnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBatal.Click
        bersih()
        tampil_data()

        BtnTambah.Enabled = True
        'otonumber()
    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick

        TextBox2.Text = ListView1.SelectedItems(0).SubItems(2).Text.ToString
        TextBox3.Text = ListView1.SelectedItems(0).SubItems(3).Text.ToString
        

        BtnTambah.Enabled = False
    End Sub

    Private Sub ListView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyDown
        If e.KeyCode = 13 Then
            ListView1_DoubleClick(Nothing, Nothing)
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub

    Private Sub BtnTambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTambah.Click
        If TextBox2.Text = "" Then
            MsgBox("Lengkapi Semua Data !", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Try
            Using Koneksi As New OleDbConnection(My.Settings.Setting_Kon)
                Using Cmd As New OleDbCommand("Insert Into tabel_user Values('" & TextBox2.Text & _
                                              "','" & TextBox3.Text & "')", Koneksi)
                    Koneksi.Open()
                    Cmd.CommandType = CommandType.Text
                    Cmd.ExecuteNonQuery()
                End Using
            End Using

            MsgBox("Data Sudah Di Simpan", MsgBoxStyle.Information, "Simpan Data")
            BtnBatal_Click(Nothing, Nothing)
        Catch ex As OleDbException
            MsgBox("Failed to Save : " & ex.Message)
        End Try

    End Sub

    Private Sub BtnHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnHapus.Click
        If TextBox2.Text = "" Then
            MsgBox("Pilih Data Dahulu !", MsgBoxStyle.Information, "Hapus Data")
            Exit Sub
        End If

        Dim Msg As DialogResult
        Msg = MessageBox.Show("Yakin Hapus Data Ini ?", _
        "Konfirmasi Hapus", MessageBoxButtons.YesNo, _
        MessageBoxIcon.Question, _
        MessageBoxDefaultButton.Button2)
        If (Msg = DialogResult.Yes) Then
            Try
                Using conn As New OleDbConnection(My.Settings.Setting_Kon)
                    conn.Open()
                    Using cmd = New OleDbCommand("Delete From tabel_pelanggan Where noIDpelanggan='" & TextBox2.Text & "'", conn)
                        Using results = cmd.ExecuteReader
                        End Using
                    End Using
                End Using

                MsgBox("Data Sudah Di Hapus", MsgBoxStyle.Information, "Hapus Data")
                BtnBatal_Click(Nothing, Nothing)
            Catch ex As OleDbException
                MsgBox("Failed : " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub BtnUbah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub combobox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub combobox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Form_Pelanggan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        bersih()

        'otonumber()
        tampil_data()
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = 13 Then
            Using Koneksi As New OleDbConnection(My.Settings.Setting_Kon)
                Using Cmd As New OleDbCommand("Select * From tabel_pelanggan where noIDpelanggan='" & TextBox2.Text & "'", Koneksi)
                    Koneksi.Open()
                    Using Data As OleDbDataReader = Cmd.ExecuteReader
                        Data.Read()
                        If Data.HasRows = True Then
                            TextBox3.Text = Data("Nama")

                            TextBox3.Text = Data("alamat")

                        Else
                            MsgBox("Data Tidak Ditemukan !", MsgBoxStyle.Exclamation)
                            bersih()
                            TextBox2.Focus()
                        End If
                    End Using
                End Using
            End Using
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class