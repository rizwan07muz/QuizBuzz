Imports MySql.Data.MySqlClient
Public Class FrmEditQuizDate
    Dim mysqlcon As New MySqlConnection("Server=localhost;Database=Quizbuzz;uid=root;pwd=0786")
    Dim mysqlcmd As New MySqlCommand()
    Dim ds As New DataSet()
    Dim mysqlda As New MySqlDataAdapter()
    Dim mysqlDR As MySqlDataReader
    Dim DR As DataRow
    Dim mysqlCB As New MySqlCommandBuilder(mysqlda)
    Private Sub FrmEditQuizDate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = frmQuizBuzz
        For i As Integer = 1 To Application.OpenForms.Count - 2
            Application.OpenForms.Item(i).Close()
        Next

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


    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        mysqlcon.Open()
        Dim query As String
        query = "delete from quizbuzz.Quiz where QuizId = '" & txtQuizId.Text & "'"
        mysqlcmd = New MySqlCommand(query, mysqlcon)
        mysqlDR = mysqlcmd.ExecuteReader
        mysqlcon.Close()
        Reset()
        MessageBox.Show("Quiz Date Deleted Successfully...")
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
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
        MessageBox.Show("Update Quiz Date Sucessfully")
    End Sub

    Private Sub txtFilterSearch_TextChanged(sender As Object, e As EventArgs) Handles txtFilterSearch.TextChanged
        Dim adapter As New MySqlDataAdapter(mysqlcmd)
        Dim table As New DataTable()
        mysqlcon.Open()
        mysqlcmd.Connection = mysqlcon
        mysqlcmd.CommandText = "select * from Teamwisecandidate where TeamId like'%" & txtFilterSearch.Text & "%' or TeamName like '%" & txtFilterSearch.Text & "%'"
        mysqlcmd.ExecuteNonQuery()
        adapter.Fill(table)
        DGV1.DataSource = table
        mysqlcon.Close()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If txtQuizId.Text = String.Empty Then
            MessageBox.Show("Please Enter Quiz Id")
        Else
            mysqlcon.Open()
            mysqlcmd.Connection = mysqlcon
            mysqlcmd.CommandText = "Select * from Quiz where QuizId='" & txtQuizId.Text & "'"
            mysqlDR = mysqlcmd.ExecuteReader()
            If mysqlDR.Read() Then
                txtQuizId.Text = mysqlDR("QuizId")
                cobSubject.Text = mysqlDR("subject")
                rtbDate.Text = mysqlDR("Date")
                ' rtpTime.Text = mysqlDR("Time")
            Else
                MessageBox.Show("No Quiz found with this id!")
            End If
            mysqlDR.Close()
            mysqlcon.Close()
        End If
    End Sub
End Class