Imports MySql.Data.MySqlClient
Public Class frmEditQuestion
    Dim mysqlcon As New MySqlConnection("Server=localhost;Database=Quizbuzz;uid=root;pwd=0786")
    Dim mysqlcmd As New MySqlCommand()
    Dim ds As New DataSet()
    Dim mysqlda As New MySqlDataAdapter()
    Dim mysqlDR As MySqlDataReader
    Dim DR As DataRow
    Dim mysqlCB As New MySqlCommandBuilder(mysqlda)
    Private Sub frmEditQuestion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = frmQuizBuzz
        For i As Integer = 1 To Application.OpenForms.Count - 2
            Application.OpenForms.Item(i).Close()
        Next
        If ActiveMdiChild IsNot Nothing Then
            ActiveMdiChild.Close()
        End If
        Datagridview()
        Reset()
    End Sub
    Public Sub Datagridview()
        mysqlcon.Open()
        mysqlcmd.Connection = mysqlcon
        mysqlcmd.CommandText = "Select * from Question"
        mysqlda.SelectCommand = mysqlcmd
        ds.Clear()
        mysqlda.Fill(ds, "Question")
        DGV1.DataSource = ds.Tables("Question")
        mysqlcon.Close()
    End Sub
    Public Sub Reset()
        txtId.Clear()
        CobSubject.Text = "Select"
        txtQuestuon.Clear()
        txtOp1.Clear()
        txtOp2.Clear()
        txtOp3.Clear()
        txtOp4.Clear()
        txtCorrectAnswer.Clear()
    End Sub


    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If txtId.Text = String.Empty Then
            MessageBox.Show("Please Enter Question Id")
        Else
            mysqlcon.Open()
            mysqlcmd.Connection = mysqlcon
            mysqlcmd.CommandText = "Select * from Question where Id='" & txtId.Text & "'"
            mysqlDR = mysqlcmd.ExecuteReader()
            If mysqlDR.Read() Then
                txtId.Text = mysqlDR("Id")
                txtQuestuon.Text = mysqlDR("Question")
                CobSubject.Text = mysqlDR("subject")
                txtOp1.Text = mysqlDR("op1")
                txtOp2.Text = mysqlDR("op2")
                txtOp3.Text = mysqlDR("op3")
                txtOp4.Text = mysqlDR("op4")
                txtCorrectAnswer.Text = mysqlDR("correctanswer")
            Else
                MessageBox.Show("No question found with this id!")
            End If
            mysqlDR.Close()
            mysqlcon.Close()
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        mysqlcon.Open()

        mysqlcmd.Connection = mysqlcon
        mysqlcmd.CommandText = "Select * from question where id = '" & txtId.Text & "'"
        mysqlda.SelectCommand = mysqlcmd
        mysqlda.Fill(ds, "question")
        DR = ds.Tables("question").Rows(0)
        DR("Id") = txtId.Text
        DR("Subject") = CobSubject.Text
        DR("Question") = txtQuestuon.Text
        DR("Op1") = txtOp1.Text
        DR("Op2") = txtOp2.Text
        DR("Op3") = txtOp3.Text
        DR("Op4") = txtOp4.Text
        DR("CorrectAnswer") = txtCorrectAnswer.Text
        mysqlda.Update(ds, "Question")

        mysqlcon.Close()
        MessageBox.Show("Question Done Sucessfully")
        Datagridview()
        Reset()

    End Sub
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        txtId.ReadOnly = False
        txtQuestuon.ReadOnly = False
        txtOp1.ReadOnly = False
        txtOp2.ReadOnly = False
        txtOp3.ReadOnly = False
        txtOp4.ReadOnly = False
        txtCorrectAnswer.ReadOnly = False
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If txtId.Text = String.Empty Then
            MessageBox.Show("Please Enter Question Id")
        Else
            mysqlcon.Open()
            mysqlcmd.Connection = mysqlcon
            mysqlcmd.CommandText = "delete from Question where Id = '" & txtId.Text & "'"
            mysqlDR = mysqlcmd.ExecuteReader()
            MessageBox.Show("Question Delated.....")
            mysqlcon.Close()
            Datagridview()
        End If
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub DGV1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
        Dim index As Integer
        index = e.RowIndex
        Dim selectedRow As DataGridViewRow
        selectedRow = DGV1.Rows(index)
        txtId.Text = selectedRow.Cells(0).Value.ToString()
        CobSubject.Text = selectedRow.Cells(1).Value.ToString()
        txtQuestuon.Text = selectedRow.Cells(2).Value.ToString()
        txtOp1.Text = selectedRow.Cells(3).Value.ToString()
        txtOp2.Text = selectedRow.Cells(4).Value.ToString()
        txtOp3.Text = selectedRow.Cells(5).Value.ToString()
        txtOp4.Text = selectedRow.Cells(6).Value.ToString()
        txtCorrectAnswer.Text = selectedRow.Cells(7).Value.ToString()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class