Imports MySql.Data.MySqlClient
Public Class frmAdmin
    Dim con As New MySqlConnection("Server=localhost;Database=Quizbuzz;uid=root;pwd=0786")
    Dim cmd As New MySqlCommand()
    Dim ds As New DataSet()
    Dim mysqlda As New MySqlDataAdapter()
    Dim mysqlDR As MySqlDataReader
    Dim DR As DataRow
    Dim mysqlCB As New MySqlCommandBuilder(mysqlda)
    Private Sub FrmNewAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = frmQuizBuzz
        For i As Integer = 1 To Application.OpenForms.Count - 2
            Application.OpenForms.Item(i).Close()
        Next
        con.Open()
        cmd.Connection = con
        cmd.CommandText = "Select * From country"
        mysqlDR = cmd.ExecuteReader()
        While mysqlDR.Read()
            cobCountry.Items.Add(mysqlDR("country"))
        End While
        con.Close()

        con.Open()
        cmd.Connection = con
        cmd.CommandText = "Select * From state"
        mysqlDR = cmd.ExecuteReader()
        While mysqlDR.Read()
            cobState.Items.Add(mysqlDR("state"))
        End While
        con.Close()

        con.Open()
        cmd.Connection = con
        cmd.CommandText = "Select * From city"
        mysqlDR = cmd.ExecuteReader()
        While mysqlDR.Read()
            cobCity.Items.Add(mysqlDR("City"))
        End While
        con.Close()
    End Sub
    Public Sub Reset()
        txtId.Clear()
        txtName.Clear()
        txtMobileNo.Clear()
        txtEmail.Clear()
        rtbAddress.Clear()
        txtAadhhar.Clear()
        cobCity.Text = "Select"
        cobState.Text = "Select"
        cobCountry.Text = "Select"
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtId.Text = String.Empty Then
            MessageBox.Show("Please Enter Admin Id")
        ElseIf txtName.Text = String.Empty
            MessageBox.Show("Please Enter Admin Name")
        ElseIf txtMobileNo.Text = String.Empty
            MessageBox.Show("Please Enter Mobile No.")
        ElseIf txtEmail.Text = String.Empty
            MessageBox.Show("Please Enter Email Id")
        ElseIf txtAadhhar.Text = String.Empty
            MessageBox.Show("Please Enter Aadhhar No")
        ElseIf txtUserName.Text = String.Empty
            MessageBox.Show("Please Enter UserName")
        ElseIf txtPassword.Text = String.Empty
            MessageBox.Show("Please Enter Password")
        Else
            con.Open()
            cmd.Connection = con
            cmd.CommandText = "Select * from admin"
            mysqlda.SelectCommand = cmd
            mysqlda.Fill(ds, "admin")
            DR = ds.Tables("admin").NewRow
            DR("Id") = txtId.Text
            DR("Name") = txtName.Text
            DR("MobileNo") = txtMobileNo.Text
            If robMale.Checked = True Then
                DR("Gender") = robMale.Text
            Else
                DR("Gender") = robFemale.Text
            End If
            DR("DOB") = dtpDateOfBirth.Text
            DR("Email") = txtEmail.Text
            DR("Address") = rtbAddress.Text
            DR("City") = cobCity.Text
            DR("Country") = cobCountry.Text
            DR("State") = cobState.Text
            DR("AadhharNo") = txtAadhhar.Text
            DR("Pincode") = txtPincode.Text
            DR("Username") = txtUserName.Text
            DR("Password") = txtPassword.Text
            ds.Tables("admin").Rows.Add(DR)
            mysqlda.Update(ds, "admin")
            con.Close()
            MessageBox.Show("Admin Done Sucessfully")
            Reset()
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        txtId.Clear()
        txtName.Clear()
        txtMobileNo.Clear()
        txtEmail.Clear()
        rtbAddress.Clear()
        txtAadhhar.Clear()
        cobCity.Text = "Select"
        cobState.Text = "Select"
        cobCountry.Text = "Select"
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

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub
End Class