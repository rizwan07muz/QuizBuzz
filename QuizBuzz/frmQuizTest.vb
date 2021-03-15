Imports MySql.Data.MySqlClient
Public Class FrmQuizTest
    Dim con As New MySqlConnection("Server=localhost;Database=Quizbuzz;uid=root;pwd=0786")
    Dim cmd As New MySqlCommand()
    Dim ds As New DataSet()
    Dim mysqlda As New MySqlDataAdapter()
    Dim mysqlDR As MySqlDataReader
    Dim DR As DataRow
    Dim mysqlCB As New MySqlCommandBuilder(mysqlda)

    Private Sub FrmQuizTest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = frmQuizBuzz

        con.Open()
        cmd.Connection = con
        cmd.CommandText = "Select * From Quiz "
        mysqlDR = cmd.ExecuteReader()
        If mysqlDR.Read() Then
            txtTeam.Text = mysqlDR("TeamName")
        End If
        con.Close()

        con.Open()
        cmd.Connection = con
        cmd.CommandText = "Select * From Quiz "
        mysqlDR = cmd.ExecuteReader()
        If mysqlDR.Read() Then
            txtQuizId.Text = mysqlDR("QuizId")
        End If
        con.Close()


        con.Open()
        cmd.Connection = con
        cmd.CommandText = "Select * From Question "
        mysqlDR = cmd.ExecuteReader()
        ' mysqlDR.Read()
        If mysqlDR.Read() Then
            txtQuestionNo.Text = mysqlDR("Id")
            txtQuestion.Text = mysqlDR("Question")
            txtOp1.Text = mysqlDR("Op1")
            txtOp2.Text = mysqlDR("Op2")
            txtOp3.Text = mysqlDR("Op3")
            txtOp4.Text = mysqlDR("Op4")
        End If
        '  mysqlDR.Close()
        con.Close()
    End Sub


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Dim result = MessageBox.Show("Are you would like to form Close ?", "Closing Quiz", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If (result = DialogResult.Yes) Then
            Me.Close()
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click

        Dim q As Integer = 0
        con.Open()
        cmd.Connection = con
        cmd.CommandText = "select * from Question"
        mysqlDR = cmd.ExecuteReader()
        While mysqlDR.Read()
            txtQuestionNo.Text = q
            txtQuestion.Text = mysqlDR("Question")

            q = q + 1
        End While
        mysqlDR.Close()
        con.Close()

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class