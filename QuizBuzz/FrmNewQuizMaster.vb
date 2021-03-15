Imports MySql.Data.MySqlClient
Public Class FrmNewQuizMaster
    Dim con As New MySqlConnection("Server=localhost;Database=Quizbuzz;uid=root;pwd=0786")
    Dim cmd As New MySqlCommand()
    Dim ds As New DataSet()
    Dim mysqlda As New MySqlDataAdapter()
    Dim mysqlDR As MySqlDataReader
    Dim DR As DataRow
    Dim mysqlCB As New MySqlCommandBuilder(mysqlda)
    Private Sub FrmNewQuizMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = frmQuizBuzz
        For i As Integer = 1 To Application.OpenForms.Count - 2
            Application.OpenForms.Item(i).Close()
        Next


        Dim Id As String = vbNullString
        con.Open()
        cmd.Connection = con
        cmd.CommandText = "select * from Quizmaster"
        mysqlDR = cmd.ExecuteReader()
        While mysqlDR.Read()
            Id = mysqlDR("Id")
        End While

        If Id <> vbNullString Then
            Dim slno As Integer = Id.Substring(1, 3)
            slno = slno + 1
            If (slno < 10) Then
                Id = "M00" & slno
            ElseIf (slno < 100) Then
                Id = "M0" & slno
            Else
                Id = "M" & slno
            End If

        Else
            Id = "M001"
        End If
        txtId.Text = Id

        con.Close()
    End Sub
    Public Sub Reset()
        txtName.Clear()
        txtMobileNo.Clear()
        txtEmail.Clear()
        txtAadhharNo.Clear()
        txtPinCode.Clear()
        rtbAddress.Clear()
        txtPassword.Clear()
        txtUsername.Clear()
        dtpDateOfBirth.Text = ""
        robMale.Text = "Male"
        robFemale.Text = "Female"
        cobCity.Text = "select"
        cobState.Text = "select"
        cobCountry.Text = "select"
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtName.Text = String.Empty Then
            MessageBox.Show("Please Enter Quiz Master Name")
        ElseIf txtMobileNo.Text = String.Empty
            MessageBox.Show("Please Enter Mobile No.")
        ElseIf txtAadhharNo.Text = String.Empty
            MessageBox.Show("Please Enter Aadhhar No.")
        ElseIf rtbAddress.Text = String.Empty
            MessageBox.Show("Please Enter Address")
        ElseIf txtPinCode.Text = String.Empty
            MessageBox.Show("Please Enter PinCode")
        ElseIf cobCountry.Text = String.Empty
            MessageBox.Show("Please select Country")
        ElseIf cobState.Text = String.Empty
            MessageBox.Show("Plase Select State")
        ElseIf cobCity.Text = String.Empty
            MessageBox.Show("Please Select City")
        ElseIf txtUsername.Text = String.Empty
            MessageBox.Show("Please Enter UserName")
        ElseIf txtPassword.Text = String.Empty
            MessageBox.Show("Please Enter PassWord")
        Else
            con.Open()
            cmd.Connection = con
            cmd.CommandText = "Select * from Quizmaster"
            mysqlda.SelectCommand = cmd
            mysqlda.Fill(ds, "Quizmaster")
            DR = ds.Tables("Quizmaster").NewRow
            DR("Id") = txtId.Text
            DR("Name") = txtName.Text
            DR("Mobile") = txtMobileNo.Text
            DR("Email") = txtEmail.Text
            DR("Address") = rtbAddress.Text
            DR("City") = cobCity.Text
            DR("Country") = cobCountry.Text
            DR("State") = cobState.Text
            DR("AadhharNo") = txtAadhharNo.Text
            DR("Username") = txtUsername.Text
            DR("Password") = txtPassword.Text
            ds.Tables("Quizmaster").Rows.Add(DR)
            mysqlda.Update(ds, "Quizmaster")
            con.Close()
            MessageBox.Show("Done Sucessfully")
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        txtName.Clear()
        txtMobileNo.Clear()
        txtEmail.Clear()
        txtAadhharNo.Clear()
        txtPinCode.Clear()
        rtbAddress.Clear()
        txtPassword.Clear()
        txtUsername.Clear()
        dtpDateOfBirth.Text = ""
        robMale.Text = "Male"
        robFemale.Text = "Female"
        cobCity.Text = "select"
        cobState.Text = "select"
        cobCountry.Text = "select"
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