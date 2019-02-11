Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Public Class FormLaporan

    Private Sub FormLaporan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim crRDoc As ReportDocument
        Dim crDB As Database
        Dim crTables As Tables
        Dim crTable As Table
        Dim crTableLogOnInfo As TableLogOnInfo
        Dim crConnInfo As ConnectionInfo

        crConnInfo = New ConnectionInfo()
        With crConnInfo
            .UserID = "admin"
            .Password = ""
            .ServerName = Application.StartupPath & "\Database1.mdb"
            .DatabaseName = Application.StartupPath & "\Database1.mdb"
            .IntegratedSecurity = True
        End With

        crRDoc = New ReportDocument
        crRDoc.Load(Application.StartupPath & "\Report1.rpt")
        crDB = crRDoc.Database
        crTables = crRDoc.Database.Tables

        For Each crTable In crTables
            crTableLogOnInfo = crTable.LogOnInfo
            crTableLogOnInfo.ConnectionInfo = crConnInfo
            crTable.ApplyLogOnInfo(crTableLogOnInfo)
        Next

        CrystalReportViewer1.ReportSource = crRDoc
    End Sub
End Class