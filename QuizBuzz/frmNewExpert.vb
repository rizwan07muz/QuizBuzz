Imports MySql.Data.MySqlClient
Public Class frmNewExpert
    Dim con As New MySqlConnection("Server=localhost;Database=Quizbuzz;uid=root;pwd=0786")
    Dim cmd As New MySqlCommand()
    Dim ds As New DataSet()
    Dim mysqlda As New MySqlDataAdapter()
    Dim mysqlDR As MySqlDataReader
    Dim DR As DataRow
    Dim mysqlCB As New MySqlCommandBuilder(mysqlda)
    Private Sub frmNewExpert_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = frmQuizBuzz
        For i As Integer = 1 To Application.OpenForms.Count - 2
            Application.OpenForms.Item(i).Close()
        Next

        Dim Id As String = vbNullString
        con.Open()
        cmd.Connection = con
        cmd.CommandText = "select * from expert"
        mysqlDR = cmd.ExecuteReader()
        While mysqlDR.Read()
            Id = mysqlDR("ExpertId")
        End While

        If Id <> vbNullString Then
            Dim slno As Integer = Id.Substring(1, 3)
            slno = slno + 1
            If (slno < 10) Then
                Id = "E00" & slno
            ElseIf (slno < 100) Then
                Id = "E0" & slno
            Else
                Id = "E" & slno
            End If
        Else
            Id = "E001"
        End If
        txtExpertId.Text = Id
        con.Close()
    End Sub
    Public Sub Reset()
        txtName.Clear()
        txtMobileNo.Clear()
        txtEmailId.Clear()
        rtbAddress.Clear()
        txtPinCode.Clear()
        cobCity.Text = "Select"
        cobState.Text = "Select"
        cobCountry.Text = "Select"
        txtAadhharNo.Clear()
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtExpertId.Text = String.Empty Then
            MessageBox.Show("Please Enter Expert Id")
        ElseIf txtName.Text = String.Empty Then
            MessageBox.Show("Please Enter Expert Name")
        ElseIf txtMobileNo.Text = String.Empty Then
            MessageBox.Show("Please Enter Mobile NO.")
        ElseIf txtExpertId.Text = String.Empty Then
            MessageBox.Show("Please Enter Email Id")
        ElseIf rtbAddress.Text = String.Empty Then
            MessageBox.Show("Please Enter Address")
        ElseIf txtAadhharNo.Text = String.Empty Then
            MessageBox.Show("Please Enter Aadhhar No.")
        ElseIf txtPinCode.Text = String.Empty Then
            MessageBox.Show("Please Enter PinCode")
        ElseIf txtUserName.Text = String.Empty Then
            MessageBox.Show("Please Enter UserName")
        ElseIf txtPassword.Text = String.Empty Then
            MessageBox.Show("Please Enter PassWord")
        Else
            con.Open()
            cmd.Connection = con
            cmd.CommandText = "Select * from expert"
            mysqlda.SelectCommand = cmd
            mysqlda.Fill(ds, "expert")
            DR = ds.Tables("expert").NewRow
            DR("ExpertId") = txtExpertId.Text
            DR("Name") = txtName.Text
            If robMale.Checked = True Then
                DR("Gender") = robMale.Text
            Else
                DR("Gender") = robFemale.Text
            End If
            DR("MobileNo") = txtMobileNo.Text
            DR("EmailId") = txtEmailId.Text
            DR("Address") = rtbAddress.Text
            DR("City") = cobCity.Text
            DR("Country") = cobCountry.Text
            DR("State") = cobState.Text
            DR("AadhharNo") = txtAadhharNo.Text
            DR("Pincode") = txtPinCode.Text
            DR("UserName") = txtUserName.Text
            DR("Password") = txtPassword.Text
            ds.Tables("expert").Rows.Add(DR)
            mysqlda.Update(ds, "expert")
            con.Close()
            MessageBox.Show("Expert Done Sucessfully")
            Reset()
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        txtName.Clear()
        txtMobileNo.Clear()
        txtEmailId.Clear()
        rtbAddress.Clear()
        txtPinCode.Clear()
        cobCity.Text = "Select"
        cobState.Text = "Select"
        cobCountry.Text = "Select"
        txtAadhharNo.Clear()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


End Class