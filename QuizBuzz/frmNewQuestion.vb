Imports MySql.Data.MySqlClient
Public Class FrmNewQuestion
    Dim con As New MySqlConnection("Server=localhost;Database=Quizbuzz;uid=root;pwd=0786")
    Dim cmd As New MySqlCommand()
    Dim ds As New DataSet()
    Dim mysqlda As New MySqlDataAdapter()
    Dim mysqlDR As MySqlDataReader
    Dim DR As DataRow
    Dim mysqlCB As New MySqlCommandBuilder(mysqlda)
    Private Sub FrmNewQuestion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = frmQuizBuzz
        For i As Integer = 1 To Application.OpenForms.Count - 2
            Application.OpenForms.Item(i).Close()
        Next
        datagridview()
        Generate_id()
        '''''''''''''''''''''''''''''''''''''''''''''Show-Subject''''COB''''''''''''''''''''''''''''''''''''''''''''''''
        con.Open()
        cmd.Connection = con
        cmd.CommandText = "Select * From subject"
        mysqlDR = cmd.ExecuteReader()
        While mysqlDR.Read()
            CobSubject.Items.Add(mysqlDR("subject"))
        End While
        con.Close()

    End Sub
    Public Sub Generate_id()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''Generate-ID''''''''''''''''''''''''''''''''''''''
        Dim Id As String = vbNullString
        con.Open()
        cmd.Connection = con
        cmd.CommandText = "select * from question"
        mysqlDR = cmd.ExecuteReader()
        While mysqlDR.Read()
            Id = mysqlDR("Id")
        End While

        If Id <> vbNullString Then
            Dim slno As Integer = Id.Substring(1, 3)
            slno = slno + 1
            If (slno < 10) Then
                Id = "Q00" & slno
            ElseIf (slno < 100) Then
                Id = "Q0" & slno
            Else
                Id = "Q" & slno
            End If

        Else
            Id = "Q001"
        End If
        txtQuestionId.Text = Id
        con.Close()
    End Sub
    Public Sub datagridview()
        con.Open()
        cmd.Connection = con
        cmd.CommandText = "Select * from Question"
        mysqlda.SelectCommand = cmd
        ds.Clear()
        mysqlda.Fill(ds, "Question")
        DGV1.DataSource = ds.Tables("Question")
        con.Close()
    End Sub
    Public Sub Reset()
        CobSubject.Text = "Select"
        txtQuestuon.Clear()
        txtOp1.Clear()
        txtOp2.Clear()
        txtOp3.Clear()
        txtOp4.Clear()
        txtCorrectAnswer.Clear()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If txtQuestionId.Text = String.Empty Then
            MessageBox.Show("Please Enter Question Id")
        ElseIf txtQuestuon.Text = String.Empty
            MessageBox.Show("Please Enter Question")
        ElseIf txtOp1.Text = String.Empty
            MessageBox.Show("Please Enter OP-1")
        ElseIf txtOp2.Text = String.Empty
            MessageBox.Show("Please Enter OP-2")
        ElseIf txtOp3.Text = String.Empty
            MessageBox.Show("Please Enter OP-3")
        ElseIf txtOp4.Text = String.Empty
            MessageBox.Show("Please Enter OP-4")
        ElseIf txtCorrectAnswer.Text = String.Empty
            MessageBox.Show("Please Enter Correct Answer")
        Else
            con.Open()
            cmd.Connection = con
            cmd.CommandText = "Select * from question"
            mysqlda.SelectCommand = cmd
            mysqlda.Fill(ds, "question")
            DR = ds.Tables("question").NewRow
            DR("Id") = txtQuestionId.Text
            DR("Subject") = CobSubject.Text
            DR("Question") = txtQuestuon.Text
            DR("Op1") = txtOp1.Text
            DR("Op2") = txtOp2.Text
            DR("Op3") = txtOp3.Text
            DR("Op4") = txtOp4.Text
            DR("CorrectAnswer") = txtCorrectAnswer.Text
            ds.Tables("Question").Rows.Add(DR)
            mysqlda.Update(ds, "Question")
            con.Close()
            datagridview()
            MessageBox.Show("Question Done Sucessfully")
            Generate_id()
            Reset()
        End If
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        CobSubject.Text = "Select"
        txtQuestuon.Clear()
        txtOp1.Clear()
        txtOp2.Clear()
        txtOp3.Clear()
        txtOp4.Clear()
        txtCorrectAnswer.Clear()
    End Sub
End Class