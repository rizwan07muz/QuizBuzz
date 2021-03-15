Imports MySql.Data.MySqlClient
Public Class FrmNewCandidate
    Dim con As New MySqlConnection("Server=localhost;Database=Quizbuzz;uid=root;pwd=0786")
    Dim cmd As New MySqlCommand()
    Dim ds As New DataSet()
    Dim mysqlda As New MySqlDataAdapter()
    Dim mysqlDR As MySqlDataReader
    Dim DR As DataRow
    Dim mysqlCB As New MySqlCommandBuilder(mysqlda)
    Private Sub FrmNewCandidate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.MaximumSize = New Size(960, 575)
        'Me.MinimumSize = Me.MaximumSize
        Me.MdiParent = frmQuizBuzz
        For i As Integer = 1 To Application.OpenForms.Count - 2
            Application.OpenForms.Item(i).Close()
        Next
        ''''''''''''''''''''''''''''''''id''''Generate'''''''''''''

        Dim Id As String = vbNullString
        con.Open()
        cmd.Connection = con
        cmd.CommandText = "select * from candidate"
        mysqlDR = cmd.ExecuteReader()
        While mysqlDR.Read()
            Id = mysqlDR("CandidateId")
        End While

        If Id <> vbNullString Then
            Dim slno As Integer = Id.Substring(1, 3)
            slno = slno + 1
            If (slno < 10) Then
                Id = "C00" & slno
            ElseIf (slno < 100) Then
                Id = "C0" & slno
            Else
                Id = "C" & slno
            End If

        Else
            Id = "C001"
        End If
        txtCandidateId.Text = Id

        con.Close()
    End Sub
    Public Sub Reset()
        txtName.Clear()
        txtMobileNo.Clear()
        txtEmailId.Clear()
        rtbAddress.Clear()
        cobCity.Text = "Select"
        cobState.Text = "Select"
        cobCountry.Text = "Select"
        txtAadhhar.Clear()
        txtPinCode.Clear()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtCandidateId.Text = String.Empty Then
            MessageBox.Show("Please Enter Candidate Id")
        ElseIf txtName.Text = String.Empty
            MessageBox.Show("Please Enter Candidate Name")
        ElseIf txtMobileNo.Text = String.Empty
            MessageBox.Show("Please Enter Mobile No.")
        ElseIf txtEmailId.Text = String.Empty
            MessageBox.Show("Please Enter Email Id")
        ElseIf txtAadhhar.Text = String.Empty
            MessageBox.Show("Please Enter Aadhhar No")
        ElseIf rtbAddress.Text = String.Empty
            MessageBox.Show("Please Enter Address")
        ElseIf txtPinCode.Text = String.Empty
            MessageBox.Show("Please Enter PinCode")
        Else
            con.Open()
            cmd.Connection = con
            cmd.CommandText = "Select * from Candidate"
            mysqlda.SelectCommand = cmd
            mysqlda.Fill(ds, "Candidate")
            DR = ds.Tables("Candidate").NewRow
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
            DR("AadhharNo") = txtAadhhar.Text
            DR("photo") = PictureBox1.Image
            ds.Tables("Candidate").Rows.Add(DR)
            mysqlda.Update(ds, "Candidate")
            con.Close()
            MessageBox.Show("Candidate Done Sucessfully")
            Reset()
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        txtName.Clear()
        txtMobileNo.Clear()
        txtEmailId.Clear()
        rtbAddress.Clear()
        cobCity.Text = "Select"
        cobState.Text = "Select"
        cobCountry.Text = "Select"
        txtAadhhar.Clear()
        txtPinCode.Clear()
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


End Class