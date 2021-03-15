Imports MySql.Data.MySqlClient
Public Class FrmEditTeam
    Dim mysqlcon As New MySqlConnection("Server=localhost;Database=Quizbuzz;uid=root;pwd=0786")
    Dim mysqlcmd As New MySqlCommand()
    Dim ds As New DataSet()
    Dim mysqlda As New MySqlDataAdapter()
    Dim mysqlDR As MySqlDataReader
    Dim DR As DataRow
    Dim mysqlCB As New MySqlCommandBuilder(mysqlda)
    Private Sub FrmEditTeam_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = frmQuizBuzz
        For i As Integer = 1 To Application.OpenForms.Count - 2
            Application.OpenForms.Item(i).Close()
        Next
        DataGridView()
        Reset()

        mysqlcon.Open()
        mysqlcmd.Connection = mysqlcon
        mysqlcmd.CommandText = "Select * from Candidate"
        mysqlda.SelectCommand = mysqlcmd
        mysqlda.Fill(ds, "Candidate")
        DGV2.DataSource = ds.Tables("Candidate")
        mysqlcon.Close()
    End Sub
    Public Sub Reset()
        txtTeamId.Clear()
        txtTeamName.Clear()
    End Sub
    Public Sub DataGridView()
        mysqlcon.Open()
        mysqlcmd.Connection = mysqlcon
        mysqlcmd.CommandText = "Select * from teamwisecandidate"
        mysqlda.SelectCommand = mysqlcmd
        ds.Clear()
        mysqlda.Fill(ds, "teamwisecandidate")
        DGV1.DataSource = ds.Tables("teamwisecandidate")
        mysqlcon.Close()
    End Sub


    Private Sub btnSearchCandidate_Click(sender As Object, e As EventArgs)
        Dim adapter As New MySqlDataAdapter(mysqlcmd)
        Dim table As New DataTable()
        mysqlcon.Open()
        mysqlcmd.Connection = mysqlcon
        mysqlcmd.CommandText = "select * from Candidate where CandidateId='" & txtCandidateId.Text & "'"
        mysqlcmd.ExecuteNonQuery()
        adapter.Fill(table)
        DGV2.DataSource = table
        mysqlcon.Close()
    End Sub

    Private Sub txtCandidateId_TextChanged(sender As Object, e As EventArgs) Handles txtCandidateId.TextChanged
        Dim adapter As New MySqlDataAdapter(mysqlcmd)
        Dim table As New DataTable()
        mysqlcon.Open()
        mysqlcmd.Connection = mysqlcon
        mysqlcmd.CommandText = "select * from Candidate where Name like'%" & txtCandidateId.Text & "%' or CandidateId like '%" & txtCandidateId.Text & "%'"
        mysqlcmd.ExecuteNonQuery()
        adapter.Fill(table)
        DGV2.DataSource = table
        mysqlcon.Close()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        mysqlcon.Open()
        mysqlcmd.Connection = mysqlcon
        mysqlcmd.CommandText = "select * from teamwisecandidate where Teamid='" & txtTeamId.Text & "'"
        mysqlda.SelectCommand = mysqlcmd
        mysqlda.Fill(ds, "teamwisecandidate")
        DR = ds.Tables("teamwisecandidate").Rows(0)
        DR("TeamId") = txtTeamId.Text
        DR("TeamName") = txtTeamName.Text
        mysqlda.Update(ds, "teamwisecandidate")
        mysqlcon.Close()
        DataGridView()
        MessageBox.Show("Team Update Successfully.......")
        Reset()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If txtTeamId.Text = String.Empty Then
            MessageBox.Show("Please Enter Team Id")
        Else
            mysqlcon.Open()
            mysqlcmd.Connection = mysqlcon
            mysqlcmd.CommandText = "delete from Teamwisecandidate where TeamId = '" & txtTeamId.Text & "'"
            mysqlDR = mysqlcmd.ExecuteReader()
            MessageBox.Show("Team Delated.....")
            mysqlcon.Close()
            DataGridView()
            Reset()
        End If
    End Sub

    Private Sub btnSearchTeamId_Click(sender As Object, e As EventArgs) Handles btnSearchTeamId.Click
        mysqlcon.Open()
        mysqlcmd.Connection = mysqlcon
        mysqlcmd.CommandText = "Select * from Teamwisecandidate where TeamId='" & txtTeamId.Text & "'"
        mysqlDR = mysqlcmd.ExecuteReader()
        If mysqlDR.Read() Then
            txtTeamId.Text = mysqlDR("TeamId")
            txtCandidateId.Text = mysqlDR("CandidateId")
            txtTeamName.Text = mysqlDR("TeamName")
        Else
            MessageBox.Show("No Team...")
        End If
        mysqlDR.Close()
        mysqlcon.Close()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        txtTeamName.ReadOnly = False
        txtCandidateId.ReadOnly = False
    End Sub
End Class