Imports MySql.Data.MySqlClient
Public Class FrmLogIn
    Dim mysqlcon As New MySqlConnection("server=localhost;database=Quizbuzz;uid=root;pwd=0786")
    Dim mysqlcmd As New MySqlCommand()
    Dim ds As New DataSet()
    Dim mysqlda As New MySqlDataAdapter()
    Dim mysqlDR As MySqlDataReader
    Dim DR As DataRow
    Private Sub FrmLogIn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reset()
    End Sub
    Public Sub Reset()
        txtUserName.Clear()
        txtPassWord.Clear()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        mysqlcon.Open()
        mysqlcmd.Connection = mysqlcon
        mysqlcmd.CommandText = "select Username From Expert where UserName ='" & txtUserName.Text & "' and Password= '" & txtPassWord.Text & "'"
        mysqlDR = mysqlcmd.ExecuteReader()
        If (mysqlDR.Read()) Then
            mysqlcon.Close()
            frmQuizBuzz.TeamToolStripMenuItem.Visible = False
            frmQuizBuzz.QuizMasterToolStripMenuItem.Visible = False
            frmQuizBuzz.AdminToolStripMenuItem.Visible = False
            frmQuizBuzz.ReportToolStripMenuItem.Visible = False
            frmQuizBuzz.Show()
            Me.Close()
        Else
            mysqlDR.Close()
            mysqlcmd.CommandText = "select Username From Quizmaster where Username ='" & txtUserName.Text & "' and Password= '" & txtPassWord.Text & "'"
            mysqlDR = mysqlcmd.ExecuteReader()
            If mysqlDR.Read() Then
                mysqlcon.Close()
                frmQuizBuzz.TeamToolStripMenuItem.Visible = False
                frmQuizBuzz.QuestionsBankToolStripMenuItem.Visible = False
                frmQuizBuzz.ExpertToolStripMenuItem.Visible = False
                frmQuizBuzz.AdminToolStripMenuItem.Visible = False
                frmQuizBuzz.ReportToolStripMenuItem.Visible = False
                frmQuizBuzz.Show()
                Me.Close()
            Else
                mysqlDR.Close()
                mysqlcmd.CommandText = "select Username From Admin where Username ='" & txtUserName.Text & "' and Password= '" & txtPassWord.Text & "'"
                mysqlDR = mysqlcmd.ExecuteReader()
                If mysqlDR.Read() Then
                    mysqlcon.Close()
                    frmQuizBuzz.QuizMasterToolStripMenuItem.Visible = False
                    frmQuizBuzz.QuestionsBankToolStripMenuItem.Visible = False
                    frmQuizBuzz.ExpertToolStripMenuItem.Visible = False
                    frmQuizBuzz.Show()
                    Me.Close()
                Else
                    MessageBox.Show("Invalid Username/Password. Try Again........")
                End If
            End If
        End If
        mysqlcon.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If txtPassWord.UseSystemPasswordChar = True Then
            'show password
            txtPassWord.UseSystemPasswordChar = False
        Else
            'hide password
            txtPassWord.UseSystemPasswordChar = True
        End If
    End Sub
End Class