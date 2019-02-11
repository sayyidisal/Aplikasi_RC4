Imports System.Data.OleDb
Public Class FormDataSiswa
    Private Sub Bersih()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox8.Text = ""
        TextBox9.Text = ""
        TextBox10.Text = ""
        TextBox11.Text = ""
        ComboBox1.Text = ""
        ComboBox2.Text = ""
        TextBox1.Focus()
    End Sub
    Sub TampilData()
        Try
            Using Koneksi As New OleDbConnection(My.Settings.Setting_Kon)
                Using CMD As New OleDbCommand("Select * From tbsiswa", Koneksi)
                    Koneksi.Open()
                    Using Data As OleDbDataReader = CMD.ExecuteReader
                        ListView1.Items.Clear()
                        Dim ListItem As ListViewItem
                        ListItem = New ListViewItem()
                        While Data.Read
                            ListItem = ListView1.Items.Add(Data("namasiswa"))
                            ListItem.SubItems.Add(Data("tempatlahir"))
                            ListItem.SubItems.Add(Data("kelamin"))
                            ListItem.SubItems.Add(Data("agama"))
                            ListItem.SubItems.Add(Data("asalsekolah"))
                            ListItem.SubItems.Add(Data("alamatsiswa"))
                            ListItem.SubItems.Add(Data("telpsiswa"))
                            ListItem.SubItems.Add(Data("namawali"))
                            ListItem.SubItems.Add(Data("alamatwali"))
                            ListItem.SubItems.Add(Data("pekerjaan"))
                            ListItem.SubItems.Add(Data("alamatkerja"))
                            ListItem.SubItems.Add(Data("telpwali"))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message())
        End Try
    End Sub
    Private Sub FormDataSiswa_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub BtnTambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTambah.Click
        If TextBox1.Text = "" Or ComboBox1.Text = "" Then
            MsgBox("Lengkapi Semua Data !", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Try
            Using Koneksii As New OleDbConnection(My.Settings.Setting_Kon)
                Using MyCmd As New OleDbCommand("Insert into tbsiswa values ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "', '" & TextBox5.Text & "', '" & TextBox6.Text & "', '" & TextBox7.Text & "', '" & TextBox8.Text & "', '" & TextBox9.Text & "','" & TextBox10.Text & "', '" & TextBox11.Text & "')", Koneksii)
                    Koneksii.Open()
                    MyCmd.CommandType = CommandType.Text
                    MyCmd.ExecuteNonQuery()

                End Using
            End Using



            MsgBox("Data sudah berhasil disimpan!", MsgBoxStyle.Information, "Simpan Data")
            TampilData()
            Bersih()
        Catch ex As Exception

        End Try

    End Sub
End Class