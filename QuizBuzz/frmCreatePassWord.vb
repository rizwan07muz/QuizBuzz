Imports MySql.Data.MySqlClient
Public Class frmCreatePassWord
    Dim mysqlcon As New MySqlConnection("Server=localhost;Database=Quizbuzz;uid=root;pwd=0786")
    Dim mysqlcmd As New MySqlCommand()
    Dim ds As New DataSet()
    Dim mysqlda As New MySqlDataAdapter()
    Dim mysqlDR As MySqlDataReader
    Dim DR As DataRow
    Private Sub FrmwlcmQuizBuzz_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        mysqlcon.Open()
        mysqlcmd.Connection = mysqlcon
        mysqlcmd.CommandText = "select *from  user where password ='" & txtPassword.Text & "'"
        mysqlda.SelectCommand = mysqlcmd
        mysqlda.Fill(ds, "user")
        DR = ds.Tables("user").NewRow
        DR("UserName") = txtUsername.Text
        DR("password") = txtPassword.Text
        ds.Tables("user").Rows.Add(DR)
        mysqlda.Update(ds, "user")
        mysqlcon.Close()
        MessageBox.Show("Done  Create passWord...")
        Me.Close()
        FrmLogIn.Show()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


End Class