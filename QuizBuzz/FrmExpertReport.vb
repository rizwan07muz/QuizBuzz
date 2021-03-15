Imports MySql.Data.MySqlClient
Public Class FrmExpertReport
    Dim mysqlcon As New MySqlConnection("Server=localhost;Database=Quizbuzz;uid=root;pwd=0786")
    Dim mysqlcmd As New MySqlCommand()
    Dim ds As New DataSet()
    Dim mysqlda As New MySqlDataAdapter()
    Dim mysqlDR As MySqlDataReader
    Dim DR As DataRow
    Private Sub FrmExpertReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = frmQuizBuzz
        For i As Integer = 1 To Application.OpenForms.Count - 2
            Application.OpenForms.Item(i).Close()
        Next

        mysqlcon.Open()
        mysqlcmd.Connection = mysqlcon
        mysqlcmd.CommandText = "Select * from Expert"
        mysqlda.SelectCommand = mysqlcmd
        mysqlda.Fill(ds, "Expert")
        DGV1.DataSource = ds.Tables("Expert")
        mysqlcon.Close()
    End Sub

    Private Sub txtFilterSearch_TextChanged(sender As Object, e As EventArgs) Handles txtFilterSearch.TextChanged
        Dim adapter As New MySqlDataAdapter(mysqlcmd)
        Dim table As New DataTable()
        mysqlcon.Open()
        mysqlcmd.Connection = mysqlcon
        mysqlcmd.CommandText = "select * from Expert where ExpertId like'%" & txtFilterSearch.Text & "%' or Name like '%" & txtFilterSearch.Text & "%'"
        mysqlcmd.ExecuteNonQuery()
        adapter.Fill(table)
        DGV1.DataSource = table
        mysqlcon.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub b_Click(sender As Object, e As EventArgs) Handles b.Click
        PrintDialog1.PrinterSettings = PrintDocument1.PrinterSettings
        If PrintDialog1.ShowDialog = DialogResult.OK Then
            PrintDocument1.PrinterSettings = PrintDialog1.PrinterSettings
            PrintDocument1.Print()
        End If
    End Sub
End Class