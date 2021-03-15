Imports MySql.Data.MySqlClient
Public Class FrmEditSubject
    Dim con As New MySqlConnection("Server=localhost;Database=Quizbuzz;uid=root;pwd=0786")
    Dim cmd As New MySqlCommand()
    Dim ds As New DataSet()
    Dim mysqlda As New MySqlDataAdapter()
    Dim mysqlDR As MySqlDataReader
    Dim DR As DataRow
    Dim mysqlCB As New MySqlCommandBuilder(mysqlda)
    Private Sub FrmEditSubject_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = frmQuizBuzz
        For i As Integer = 1 To Application.OpenForms.Count - 2
            Application.OpenForms.Item(i).Close()
        Next
        DataGridView()
        Reset()
    End Sub
    Public Sub Reset()
        txtSubjectId.Clear()
        txtSubject.Clear()
    End Sub
    Public Sub DataGridView()
        con.Open()
        cmd.Connection = con
        cmd.CommandText = "Select * from Subject"
        mysqlda.SelectCommand = cmd
        ds.Clear()
        mysqlda.Fill(ds, "Subject")
        DGV.DataSource = ds.Tables("Subject")
        con.Close()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        con.Open()
        cmd.Connection = con
        cmd.CommandText = "Select * from Subject"
        mysqlda.SelectCommand = cmd
        mysqlda.Fill(ds, "Subject")
        DR = ds.Tables("Subject").Rows(0)
        DR("SubjectId") = txtSubjectId.Text
        DR("Subject") = txtSubject.Text
        mysqlda.Update(ds, "Subject")
        con.Close()
        DataGridView()
        MessageBox.Show("Add Subject Sucessfully")
        Reset()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        con.Open()
        Dim query As String
        query = "delete from quizbuzz.Subject where SubjectId = '" & txtSubjectId.Text & "'"
        cmd = New MySqlCommand(query, con)
        mysqlDR = cmd.ExecuteReader
        MessageBox.Show("Subject Deleted Successfully...")
        con.Close()
        DataGridView()
        Reset()
    End Sub

    Private Sub DGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV.CellContentClick
        Dim index As Integer
        index = e.RowIndex
        Dim selectedRow As DataGridViewRow
        selectedRow = DGV.Rows(index)
        txtSubjectId.Text = selectedRow.Cells(0).Value.ToString()
        txtSubject.Text = selectedRow.Cells(1).Value.ToString()

    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        txtSubject.ReadOnly = False
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class