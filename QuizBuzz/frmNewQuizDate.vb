Imports MySql.Data.MySqlClient
Public Class frmNewQuizDate
    Dim mysqlcon As New MySqlConnection("Server=localhost;Database=Quizbuzz;uid=root;pwd=0786")
    Dim mysqlcmd As New MySqlCommand()
    Dim ds As New DataSet()
    Dim mysqlda As New MySqlDataAdapter()
    Dim mysqlDR As MySqlDataReader
    Dim DR As DataRow
    Dim mysqlCB As New MySqlCommandBuilder(mysqlda)

    Private Sub FrmQuiz_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = frmQuizBuzz
        For i As Integer = 1 To Application.OpenForms.Count - 2
            Application.OpenForms.Item(i).Close()
        Next
        Generate_id()

        mysqlcon.Open()
        mysqlcmd.Connection = mysqlcon
        mysqlcmd.CommandText = "Select * from Teamwisecandidate"
        mysqlda.SelectCommand = mysqlcmd
        mysqlda.Fill(ds, "Teamwisecandidate")
        DGV1.DataSource = ds.Tables("Teamwisecandidate")
        mysqlcon.Close()


        mysqlcon.Open()
        mysqlcmd.Connection = mysqlcon
        mysqlcmd.CommandText = "Select * From subject"
        mysqlDR = mysqlcmd.ExecuteReader()
        While mysqlDR.Read()
            cobSubject.Items.Add(mysqlDR("subject"))
        End While
        mysqlcon.Close()
    End Sub
    Public Sub Generate_id()
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''Generat_id'''''''''''''''''''''''''''''
        Dim Id As String = vbNullString
        mysqlcon.Open()
        mysqlcmd.Connection = mysqlcon
        mysqlcmd.CommandText = "select * from Quiz"
        mysqlDR = mysqlcmd.ExecuteReader()
        While mysqlDR.Read()
            Id = mysqlDR("QuizId")
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
        txtQuizId.Text = Id

        mysqlcon.Close()
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        mysqlcon.Open()
        mysqlcmd.Connection = mysqlcon
        mysqlcmd.CommandText = "select * from  Quiz"
        mysqlda.SelectCommand = mysqlcmd
        mysqlda.Fill(ds, "Quiz")
        DR = ds.Tables("Quiz").NewRow
        DR("QuizId") = txtQuizId.Text
        DR("Subject") = cobSubject.Text
        DR("Date") = rtbDate.Text
        DR("Time") = rtpTime.Value.TimeOfDay
        For i = 0 To DGV2.Rows.Count - 1
            DR("TeamId") = DGV2.Rows(i).Cells(1).Value
            DR("TeamName") = DGV2.Rows(i).Cells(2).Value
            DR("CandidateId") = DGV2.Rows(i).Cells(3).Value
        Next
        ds.Tables("Quiz").Rows.Add(DR)
        mysqlda.Update(ds, "Quiz")
        mysqlcon.Close()
        MessageBox.Show("Add Quiz Date Sucessfully")
        Generate_id()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub btnTeamAdd_Click(sender As Object, e As EventArgs) Handles btnTeamAdd.Click
        Dim j, i As Integer
        For i = 0 To DGV1.Rows.Count - 2
            If DGV1.Rows(i).Cells(0).Value = True Then
                DGV2.Rows.Add()
                DGV2.Rows(j).Cells(0).Value = DGV1.Rows(i).Cells(0).Value
                DGV2.Rows(j).Cells(1).Value = DGV1.Rows(i).Cells(1).Value
                DGV2.Rows(j).Cells(2).Value = DGV1.Rows(i).Cells(2).Value
                DGV2.Rows(j).Cells(3).Value = DGV1.Rows(i).Cells(3).Value
                j = j + 1
            End If
        Next
    End Sub



    Private Sub btnclose_Click_1(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnsearch_Click(sender As Object, e As EventArgs) Handles btnsearch.Click
        Dim adapter As New MySqlDataAdapter(mysqlcmd)
        Dim table As New DataTable()
        mysqlcon.Open()
        mysqlcmd.Connection = mysqlcon
        mysqlcmd.CommandText = "select * from Teamwisecandidate where TeamId='" & txtTeamId.Text & "'"
        mysqlcmd.ExecuteNonQuery()
        adapter.Fill(table)
        DGV1.DataSource = table
        mysqlcon.Close()
    End Sub

    Private Sub txtTeamId_TextChanged(sender As Object, e As EventArgs) Handles txtTeamId.TextChanged
        Dim adapter As New MySqlDataAdapter(mysqlcmd)
        Dim table As New DataTable()
        mysqlcon.Open()
        mysqlcmd.Connection = mysqlcon
        mysqlcmd.CommandText = "select * from Teamwisecandidate where TeamId like'%" & txtTeamId.Text & "%' or TeamName like'%" & txtTeamId.Text & "%'"
        mysqlcmd.ExecuteNonQuery()
        adapter.Fill(table)
        DGV1.DataSource = table
        mysqlcon.Close()
    End Sub


End Class