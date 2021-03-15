Imports MySql.Data.MySqlClient
Public Class frmEditExpert
    Dim mysqlcon As New MySqlConnection("Server=localhost;Database=Quizbuzz;uid=root;pwd=0786")
    Dim mysqlcmd As New MySqlCommand()
    Dim ds As New DataSet()
    Dim mysqlda As New MySqlDataAdapter()
    Dim mysqlDR As MySqlDataReader
    Dim DR As DataRow
    Dim mysqlCB As New MySqlCommandBuilder(mysqlda)
    Private Sub frmEditExpert_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = frmQuizBuzz
        For i As Integer = 1 To Application.OpenForms.Count - 2
            Application.OpenForms.Item(i).Close()
        Next
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        mysqlcon.Open()
        mysqlcmd.Connection = mysqlcon
        mysqlcmd.CommandText = "Select * from Expert where ExpertId='" & txtExpertId.Text & "'"
        mysqlDR = mysqlcmd.ExecuteReader()
        If mysqlDR.Read() Then
            txtExpertId.Text = mysqlDR("ExpertId")
            txtName.Text = mysqlDR("Name")
            dtpDateOfBirth.Text = mysqlDR("DOB")
            rtbAddress.Text = mysqlDR("Address")
            cobCountry.Text = mysqlDR("Country")
            cobState.Text = mysqlDR("State")
            cobCity.Text = mysqlDR("City")
            txtAadhharNo.Text = mysqlDR("AadhharNo")
            txtEmailId.Text = mysqlDR("Emailid")
            txtMobileNo.Text = mysqlDR("MobileNo")
            txtUserName.Text = mysqlDR("Usename")
            txtPassWord.Text = mysqlDR("Password")
        Else
            MessageBox.Show("No Expert found with this id!")
        End If
        mysqlDR.Close()
        mysqlcon.Close()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If txtExpertId.Text = String.Empty Then
            MessageBox.Show("Please Seearch Expert Id And Update Expert")
        Else
            mysqlcon.Open()
            mysqlcmd.Connection = mysqlcon
            mysqlcmd.CommandText = "Select * from Expert"
            mysqlda.SelectCommand = mysqlcmd
            mysqlda.Fill(ds, "expert")
            DR = ds.Tables("expert").Rows(0)
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
            DR("UserName") = txtUserName.Text
            DR("Password") = txtPassWord.Text
            mysqlda.Update(ds, "expert")
            mysqlcon.Close()
            MessageBox.Show("Expert Done Sucessfully")
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If txtExpertId.Text = String.Empty Then
            MessageBox.Show("Please Enter Expert Id")
        Else
            mysqlcon.Open()
            Dim query As String
            query = "delete from quizbuzz.Expert where Expertid = '" & txtExpertId.Text & "'"
            mysqlcmd = New MySqlCommand(query, mysqlcon)
            mysqlDR = mysqlcmd.ExecuteReader
            mysqlcon.Close()
            MessageBox.Show("Expert Deleted Successfully...")
        End If
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If txtExpertId.Text = String.Empty Then
            MessageBox.Show("Please Search Expert Id And Edit Expert ")
        Else
            Try
                txtName.ReadOnly = False
                txtMobileNo.ReadOnly = False
                txtAadhharNo.ReadOnly = False
                txtEmailId.ReadOnly = False
                txtPinCode.ReadOnly = False
                txtUserName.ReadOnly = False
                txtPassWord.ReadOnly = False
                rtbAddress.ReadOnly = False
            Catch ex As Exception
                MessageBox.Show("....")
            End Try
        End If
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

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class