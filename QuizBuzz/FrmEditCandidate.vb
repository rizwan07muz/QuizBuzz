Imports MySql.Data.MySqlClient
Public Class FrmEditCandidate
    Dim mysqlcon As New MySqlConnection("Server=localhost;Database=Quizbuzz;uid=root;pwd=0786")
    Dim mysqlcmd As New MySqlCommand()
    Dim ds As New DataSet()
    Dim mysqlda As New MySqlDataAdapter()
    Dim mysqlDR As MySqlDataReader
    Dim DR As DataRow
    Dim mysqlCB As New MySqlCommandBuilder(mysqlda)
    Private Sub FrmEditCandidate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = frmQuizBuzz
        For i As Integer = 1 To Application.OpenForms.Count - 2
            Application.OpenForms.Item(i).Close()
        Next
        Reset()
    End Sub
    Public Sub Reset()
        txtName.Clear()
        txtMobileNo.Clear()
        txtEmailId.Clear()
        rtbAddress.Clear()
        cobCity.Text = "Select"
        cobState.Text = "Select"
        cobCountry.Text = "Select"
        txtAadhharNo.Clear()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        mysqlcon.Open()
        mysqlcmd.Connection = mysqlcon
        mysqlcmd.CommandText = "Select * from candidate where candidateId='" & txtCandidateId.Text & "'"
        mysqlDR = mysqlcmd.ExecuteReader()
        If mysqlDR.Read() Then
            txtCandidateId.Text = mysqlDR("candidateId")
            txtName.Text = mysqlDR("Name")
            dtpDateOfBirth.Text = mysqlDR("DOB")
            rtbAddress.Text = mysqlDR("Address")
            cobCountry.Text = mysqlDR("Country")
            cobState.Text = mysqlDR("State")
            cobCity.Text = mysqlDR("City")
            txtAadhharNo.Text = mysqlDR("AadhharNo")
            txtEmailId.Text = mysqlDR("Emailid")
            txtMobileNo.Text = mysqlDR("MobileNo")
        Else
            MessageBox.Show("No question found with this id!")
        End If
        mysqlDR.Close()
        mysqlcon.Close()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        mysqlcon.Open()
        mysqlcmd.Connection = mysqlcon
        mysqlcmd.CommandText = "Select * from Candidate where candidateId='" & txtCandidateId.Text & "'"
        mysqlda.SelectCommand = mysqlcmd
        mysqlda.Fill(ds, "Candidate")
        DR = ds.Tables("Candidate").Rows(0)
        DR("CandidateId") = txtCandidateId.Text
        DR("Name") = txtName.Text
        If robMale.Checked = True Then
            DR("Gender") = robMale.Text
        Else
            DR("Gender") = robFemale.Text
        End If
        DR("MobileNo") = txtMobileNo.Text
        DR("DOB") = dtpDateOfBirth.Text
        DR("EmailId") = txtEmailId.Text
        DR("Address") = rtbAddress.Text
        DR("City") = cobCity.Text
        DR("Country") = cobCountry.Text
        DR("State") = cobState.Text
        DR("AadhharNo") = txtAadhharNo.Text
        DR("PinCode") = txtPinCode.Text
        mysqlda.Update(ds, "Candidate")
        mysqlcon.Close()
        MessageBox.Show("Candidate Update Sucessfully")
        Reset()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        txtName.ReadOnly = False
        txtMobileNo.ReadOnly = False
        txtEmailId.ReadOnly = False
        txtAadhharNo.ReadOnly = False
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        mysqlcon.Open()
        Dim query As String
        query = "delete from quizbuzz.candidate where CandidateId = '" & txtCandidateId.Text & "'"
        mysqlcmd = New MySqlCommand(query, mysqlcon)
        mysqlDR = mysqlcmd.ExecuteReader
        MessageBox.Show("candidate Deleted Successfully...")
        mysqlcon.Close()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim dialog As New OpenFileDialog
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class