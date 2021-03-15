Imports MySql.Data.MySqlClient
Public Class FrmQuizMasterReport
    Dim mysqlcon As New MySqlConnection("Server=localhost;Database=Quizbuzz;uid=root;pwd=0786")
    Dim mysqlcmd As New MySqlCommand()
    Dim ds As New DataSet()
    Dim mysqlda As New MySqlDataAdapter()
    Dim mysqlDR As MySqlDataReader
    Dim DR As DataRow
    Private Sub FrmQuizMasterReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = frmQuizBuzz
        For i As Integer = 1 To Application.OpenForms.Count - 2
            Application.OpenForms.Item(i).Close()
        Next

        mysqlcon.Open()
        mysqlcmd.Connection = mysqlcon
        mysqlcmd.CommandText = "Select * from Quizmaster"
        mysqlda.SelectCommand = mysqlcmd
        mysqlda.Fill(ds, "Quizmaster")
        DGV1.DataSource = ds.Tables("Quizmaster")
        mysqlcon.Close()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Dim adapter As New MySqlDataAdapter(mysqlcmd)
        Dim table As New DataTable()
        mysqlcon.Open()
        mysqlcmd.Connection = mysqlcon
        mysqlcmd.CommandText = "select * from Quizmaster where Id like'%" & txtSearch.Text & "%' or Name like '%" & txtSearch.Text & "%'"
        mysqlcmd.ExecuteNonQuery()
        adapter.Fill(table)
        DGV1.DataSource = table
        mysqlcon.Close()
    End Sub
End Class