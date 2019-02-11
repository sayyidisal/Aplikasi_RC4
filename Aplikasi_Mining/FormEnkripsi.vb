Imports System.Data.OleDb
Public Class FormEnkripsi
    Private Function Rc4(ByVal message As String, ByVal password As String) As String
        Dim s = Enumerable.Range(0, 256).ToArray
        Dim i, j As Integer
        For i = 0 To s.Length - 1
            j = (j + Asc(password(i Mod password.Length)) + s(i)) And 255
            Dim temp = s(i)
            s(i) = s(j)
            s(j) = temp
        Next
        i = 0
        j = 0
        Dim sb As New System.Text.StringBuilder(message.Length)
        For Each c As Char In message
            i = (i + 1) And 255
            j = (j + s(i)) And 255
            Dim temp = s(i)
            s(i) = s(j)
            s(j) = temp
            sb.Append(Chr(s((s(i) + s(j)) And 255) Xor Asc(c)))
        Next
        Return sb.ToString
    End Function
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
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        ComboBox2.Text = ""
        ComboBox1.Text = ""
        TextBox4.Clear()
        TextBox5.Clear()
    End Sub

    Sub otonumber()
        Try
            Dim otono As String = "select * from tabel_siswa where noIDsiswa in(SELECT MAX(noIDsiswa)from tabel_pelanggan)order by noIDsiswa"
            set_koneksi.Open()
            Using cmdsql As New OleDbCommand(otono, set_koneksi)
                Using rs As OleDbDataReader = cmdsql.ExecuteReader
                    rs.Read()
                    Dim urutan As String
                    Dim hitung As String
                    With rs
                        If Not .HasRows Then
                            urutan = "K001"
                            TextBox1.Text = urutan
                        Else
                            hitung = Microsoft.VisualBasic.Right(rs("noIDsiswa"), 3) + 1
                            urutan = Microsoft.VisualBasic.Right("K00" & hitung, 9)
                        End If
                        TextBox1.Text = urutan
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
            Using Cmd As New OleDbCommand("Select * From tabel_enkripsi order by noIDsiswa", Koneksi)
                Koneksi.Open()
                Using Data As OleDbDataReader = Cmd.ExecuteReader
                    Dim x As Integer = 0
                    Dim no = 1
                    ListView1.Items.Clear()
                    While Data.Read
                        ListView1.Items.Add(no)
                        ListView1.Items(x).SubItems.Add(Data("noIDsiswa"))
                        ListView1.Items(x).SubItems.Add(Data("Nama"))
                        ListView1.Items(x).SubItems.Add(Data("Jkel"))
                        ListView1.Items(x).SubItems.Add(Data("alamat"))
                        ListView1.Items(x).SubItems.Add(Data("agama"))
                        ListView1.Items(x).SubItems.Add(Data("anak"))
                        ListView1.Items(x).SubItems.Add(Data("asal"))
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


        'otonumber()
    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        TextBox1.Text = ListView1.SelectedItems(0).SubItems(1).Text.ToString
        TextBox2.Text = ListView1.SelectedItems(0).SubItems(2).Text.ToString
        ComboBox1.Text = ListView1.SelectedItems(0).SubItems(3).Text.ToString
        TextBox3.Text = ListView1.SelectedItems(0).SubItems(4).Text.ToString
        ComboBox2.Text = ListView1.SelectedItems(0).SubItems(5).Text.ToString
        TextBox4.Text = ListView1.SelectedItems(0).SubItems(6).Text.ToString
        TextBox5.Text = ListView1.SelectedItems(0).SubItems(7).Text.ToString


    End Sub

    Private Sub ListView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyDown
        If e.KeyCode = 13 Then
            ListView1_DoubleClick(Nothing, Nothing)
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub

    Private Sub BtnTambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If TextBox1.Text = "" Or ComboBox1.Text = "" Then
            MsgBox("Lengkapi Semua Data !", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Try
            Using Koneksi As New OleDbConnection(My.Settings.Setting_Kon)
                Using Cmd As New OleDbCommand("Insert Into tabel_pelanggan Values('" & TextBox1.Text & _
                                              "','" & TextBox2.Text & "','" & ComboBox1.Text & _
                                              "','" & TextBox3.Text & "','" & ComboBox2.Text & "', '" & TextBox4.Text & "','" & TextBox5.Text & "')", Koneksi)
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



    Private Sub BtnUbah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUbah.Click
        Form_Cetak.ShowDialog()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub combobox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox2.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub combobox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.TextChanged

    End Sub

    Private Sub Form_Pelanggan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        bersih()

        'otonumber()
        tampil_data()
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = 13 Then
            Using Koneksi As New OleDbConnection(My.Settings.Setting_Kon)
                Using Cmd As New OleDbCommand("Select * From tabel_pelanggan where noIDsiswa='" & TextBox1.Text & "'", Koneksi)
                    Koneksi.Open()
                    Using Data As OleDbDataReader = Cmd.ExecuteReader
                        Data.Read()
                        If Data.HasRows = True Then
                            TextBox2.Text = Data("Nama")
                            ComboBox1.Text = Data("Jkel")
                            TextBox3.Text = Data("alamat")
                            ComboBox2.Text = Data("agama")
                            TextBox4.Text = Data("anak")
                            TextBox5.Text = Data("asal")
                        Else
                            MsgBox("Data Tidak Ditemukan !", MsgBoxStyle.Exclamation)
                            bersih()
                            TextBox1.Focus()
                        End If
                    End Using
                End Using
            End Using
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If TextBox6.Text = "" Then
                MsgBox("Isi Key Terlebih Dahulu!!!", MsgBoxStyle.Information, "Masukkan Key")
                TextBox6.Focus()
            Else
                TextBox2.Text = Rc4(TextBox2.Text, TextBox6.Text)
                ComboBox1.Text = Rc4(ComboBox1.Text, TextBox6.Text)
                TextBox3.Text = Rc4(TextBox3.Text, TextBox6.Text)
                ComboBox2.Text = Rc4(ComboBox2.Text, TextBox6.Text)
                TextBox4.Text = Rc4(TextBox4.Text, TextBox6.Text)
                TextBox5.Text = Rc4(TextBox5.Text, TextBox6.Text)
                TextBox6.Text = Rc4(TextBox6.Text, TextBox6.Text)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Or ComboBox1.Text = "" Then
            MsgBox("Lengkapi Semua Data !", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Try
            Using Koneksi As New OleDbConnection(My.Settings.Setting_Kon)
                Using Cmd As New OleDbCommand("Insert Into tabel_enkripsi Values('" & TextBox1.Text & _
                                              "','" & TextBox2.Text & "','" & ComboBox1.Text & _
                                              "','" & TextBox3.Text & "','" & ComboBox2.Text & "', '" & TextBox4.Text & "','" & TextBox5.Text & "')", Koneksi)
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
End Class