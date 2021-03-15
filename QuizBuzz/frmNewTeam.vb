Imports MySql.Data.MySqlClient
Public Class frmNewTeam
    Dim con As New MySqlConnection("Server=localhost;Database=Quizbuzz;uid=root;pwd=0786")
    Dim cmd As New MySqlCommand()
    Dim ds As New DataSet()
    Dim mysqlda As New MySqlDataAdapter()
    Dim mysqlDR As MySqlDataReader
    Dim DR As DataRow
    Dim mysqlCB As New MySqlCommandBuilder(mysqlda)
    Private Sub FrmTeam_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = frmQuizBuzz
        For i As Integer = 1 To Application.OpenForms.Count - 2
            Application.OpenForms.Item(i).Close()
        Next
        Generate_id()
        DataGridView()

        con.Open()
        cmd.Connection = con
        cmd.CommandText = "Select * from Candidate"
        mysqlda.SelectCommand = cmd
        mysqlda.Fill(ds, "Candidate")
        DGV1.DataSource = ds.Tables("Candidate")
        con.Close()
    End Sub
    Public Sub DataGridView()
        'con.Open()
        'cmd.Connection = con
        'cmd.CommandText = "Select * from teamwisecandidate"
        'mysqlda.SelectCommand = cmd
        'ds.Clear()
        'mysqlda.Fill(ds, "teamwisecandidate")
        'DGV2.DataSource = ds.Tables("teamwisecandidate")
        'con.Close()
    End Sub
    Public Sub Generate_id()
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''Generat_id'''''''''''''''''''''''''''''
        Dim Id As String = vbNullString
        con.Open()
        cmd.Connection = con
        cmd.CommandText = "select * from Teamwisecandidate"
        mysqlDR = cmd.ExecuteReader()
        While mysqlDR.Read()
            Id = mysqlDR("TeamId")
        End While

        If Id <> vbNullString Then
            Dim slno As Integer = Id.Substring(1, 3)
            slno = slno + 1
            If (slno < 10) Then
                Id = "T00" & slno
            ElseIf (slno < 100) Then
                Id = "T0" & slno
            Else
                Id = "T" & slno
            End If

        Else
            Id = "T001"
        End If
        txtTeamId.Text = Id

        con.Close()
    End Sub

    Private Sub btnAddCandidate_Click(sender As Object, e As EventArgs) Handles btnAddCandidate.Click

        Dim j, i As Integer
        For i = 0 To DGV1.Rows.Count - 1
            If DGV1.Rows(i).Cells(0).Value = True Then
                DGV2.Rows.Add(DGV1.Rows)
                DGV2.Rows(j).Cells(0).Value = DGV1.Rows(i).Cells(0).Value
                DGV2.Rows(j).Cells(1).Value = DGV1.Rows(i).Cells(1).Value
                DGV2.Rows(j).Cells(2).Value = DGV1.Rows(i).Cells(2).Value
                DGV2.Rows(j).Cells(3).Value = DGV1.Rows(i).Cells(3).Value
                DGV2.Rows(j).Cells(4).Value = DGV1.Rows(i).Cells(4).Value
                j = j + 1
            End If
        Next
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click

        Dim index As Integer
        index = DGV2.CurrentCell.RowIndex
        DGV2.Rows.RemoveAt(index)
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If txtCandidateId.Text = String.Empty Then
            MessageBox.Show("Please Enter Candidate Id")
        Else
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable()
            con.Open()
            cmd.Connection = con
            cmd.CommandText = “select * from Candidate where CandidateId='" & txtCandidateId.Text & "'"
            cmd.ExecuteNonQuery()
            adapter.Fill(table)
            DGV1.DataSource = table
            con.Close()
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If txtTeamName.Text = String.Empty Then
            MessageBox.Show("Please Enter TeamName.... ")
        Else
            con.Open()
            cmd.Connection = con
            cmd.CommandText = "select * from  teamwisecandidate"
            mysqlda.SelectCommand = cmd
            mysqlda.Fill(ds, "teamwisecandidate")
            DR = ds.Tables("teamwisecandidate").NewRow
            DR("TeamId") = txtTeamId.Text
            DR("TeamName") = txtTeamName.Text
            For i = 0 To DGV2.Rows.Count - 1
                DR("CandidateId") = DGV2.Rows(i).Cells(1).Value
                DR("Name") = DGV2.Rows(i).Cells(2).Value
                DR("MobileNo") = DGV2.Rows(i).Cells(3).Value
                DR("EmailId") = DGV2.Rows(i).Cells(4).Value
                DR("Address") = DGV2.Rows(i).Cells(5).Value
            Next
            ds.Tables("teamwisecandidate").Rows.Add(DR)
            mysqlda.Update(ds, "teamwisecandidate")
            con.Close()
            DataGridView()
            MessageBox.Show("Team Add Successfully.......")
            Generate_id()
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtTeamId_TextChanged(sender As Object, e As EventArgs) Handles txtTeamId.TextChanged

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class