Imports MySql.Data.MySqlClient
Public Class FrmQuizDateReport
    Dim mysqlcon As New MySqlConnection("Server=localhost;Database=Quizbuzz;uid=root;pwd=0786")
    Dim mysqlcmd As New MySqlCommand()
    Dim ds As New DataSet()
    Dim mysqlda As New MySqlDataAdapter()
    Dim mysqlDR As MySqlDataReader
    Dim DR As DataRow
    Dim mysqlCB As New MySqlCommandBuilder(mysqlda)
    Private Sub FrmQuizDateReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = frmQuizBuzz
        For i As Integer = 1 To Application.OpenForms.Count - 2
            Application.OpenForms.Item(i).Close()
        Next

        mysqlcon.Open()
        mysqlcmd.Connection = mysqlcon
        mysqlcmd.CommandText = "Select * from Quiz"
        mysqlda.SelectCommand = mysqlcmd
        mysqlda.Fill(ds, "Quiz")
        DGV1.DataSource = ds.Tables("Quiz")
        mysqlcon.Close()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Dim adapter As New MySqlDataAdapter(mysqlcmd)
        Dim table As New DataTable()
        mysqlcon.Open()
        mysqlcmd.Connection = mysqlcon
        mysqlcmd.CommandText = "select * from Quiz where QuizId like'%" & txtSearch.Text & "%' or Subject like '%" & txtSearch.Text & "%' or Date like '%" & txtSearch.Text & "%' or Subject like'%" & txtSearch.Text & "%'"
        mysqlcmd.ExecuteNonQuery()
        adapter.Fill(table)
        DGV1.DataSource = table
        mysqlcon.Close()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim mysqlda As New MySqlDataAdapter()
        Dim ds As New DataSet()
        mysqlcon.Open()
        mysqlcmd.Connection = mysqlcon
        If RadioButton1.Checked Then
            mysqlcmd.CommandText = "select * from Quiz where Date='" & DateTimePicker1.Value.Date & "'"
            mysqlcmd.CommandText = "select * from Quiz where Date='" & DateTimePicker1.Value.Date & "' And Date'" & DateTimePicker2.Value.Date & "'"
        End If

        mysqlcon.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub b_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        PrintDialog1.PrinterSettings = PrintDocument1.PrinterSettings
        If PrintDialog1.ShowDialog = DialogResult.OK Then
            PrintDocument1.PrinterSettings = PrintDialog1.PrinterSettings
            PrintDocument1.Print()
        End If
    End Sub
End Class