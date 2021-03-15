Imports MySql.Data.MySqlClient
Public Class FrmEditQuizMaster
    Dim mysqlcon As New MySqlConnection("Server=localhost;Database=Quizbuzz;uid=root;pwd=0786")
    Dim mysqlcmd As New MySqlCommand()
    Dim ds As New DataSet()
    Dim mysqlda As New MySqlDataAdapter()
    Dim mysqlDR As MySqlDataReader
    Dim DR As DataRow
    Dim mysqlCB As New MySqlCommandBuilder(mysqlda)
    Private Sub FrmEditQuizMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = frmQuizBuzz
        For i As Integer = 1 To Application.OpenForms.Count - 2
            Application.OpenForms.Item(i).Close()
        Next
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) 
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtId.Text = String.Empty Then
            MessageBox.Show("Please Seearch QuizMaster Id Then Update Quiz Master")
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
            DR("Mobile") = txtMobileNo.Text
            DR("Email") = txtEmail.Text
            DR("Address") = rtbAddress.Text
            DR("City") = cobCity.Text
            DR("Country") = cobCountry.Text
            DR("State") = cobState.Text
            DR("AadhharNo") = txtAadhharNo.Text
            DR("UserName") = txtUserName.Text
            DR("Password") = txtPassword.Text
            mysqlda.Update(ds, "expert")
            mysqlcon.Close()
            MessageBox.Show("QuizMaster Done Sucessfully")
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim dialog As New OpenFileDialog
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)
        End If
    End Sub
End Class