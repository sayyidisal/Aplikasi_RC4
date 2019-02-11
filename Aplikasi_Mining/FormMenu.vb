Public Class FormMenu

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        End
    End Sub

    Private Sub UserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserToolStripMenuItem.Click
        FrmUser.Show()
        FrmUser.MdiParent = Me
    End Sub

    Private Sub MinatPelangganToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        FormMinat.Show()
        FormMinat.MdiParent = Me
    End Sub

    Private Sub DataSiswaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataSiswaToolStripMenuItem.Click
        Form_Pelanggan.Show()
        Form_Pelanggan.MdiParent = Me
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        FormEnkripsi.Show()
        FormEnkripsi.MdiParent = Me
    End Sub

    Private Sub ToolStripSplitButton1_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripSplitButton1.ButtonClick

    End Sub

    Private Sub MinatToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        FormMinat.Show()
        FormMinat.MdiParent = Me
    End Sub

    Private Sub FrmToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FrmToolStripMenuItem.Click
        FrmUser.Show()
        FrmUser.MdiParent = Me
    End Sub
End Class
