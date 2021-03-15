Imports MySql.Data.MySqlClient
Public Class FrmAddSubject
    Dim con As New MySqlConnection("Server=localhost;Database=Quizbuzz;uid=root;pwd=0786")
    Dim cmd As New MySqlCommand()
    Dim ds As New DataSet()
    Dim mysqlda As New MySqlDataAdapter()
    Dim mysqlDR As MySqlDataReader
    Dim DR As DataRow
    Dim mysqlCB As New MySqlCommandBuilder(mysqlda)
    Private Sub FrmSubject_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = frmQuizBuzz
        For i As Integer = 1 To Application.OpenForms.Count - 2
            Application.OpenForms.Item(i).Close()
        Next
        Generate_id()
        Reset()
        DataGridView()
    End Sub
    Public Sub DataGridView()
        con.Open()
        cmd.Connection = con
        cmd.CommandText = "Select * from Subject"
        mysqlda.SelectCommand = cmd
        ds.Clear()
        mysqlda.Fill(ds, "Subject")
        DGV.DataSource = ds.Tables("Subject")
        con.Close()
    End Sub
    Public Sub Generate_id()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''Generate-Id'''''''''''''''''''''''''''''''''''''
        Dim Id As String = vbNullString
        con.Open()
        cmd.Connection = con
        cmd.CommandText = "select * from Subject"
        mysqlDR = cmd.ExecuteReader()
        While mysqlDR.Read()
            Id = mysqlDR("SubjectId")
        End While

        If Id <> vbNullString Then
            Dim slno As Integer = Id.Substring(1, 3)
            slno = slno + 1
            If (slno < 10) Then
                Id = "S00" & slno
            ElseIf (slno < 100) Then
                Id = "S0" & slno
            Else
                Id = "S" & slno
            End If

        Else
            Id = "S001"
        End If
        txtSubjectId.Text = Id
        con.Close()
    End Sub
    Public Sub Reset()
        txtSubject.Clear()
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        txtSubject.Clear()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If txtSubjectId.Text = String.Empty Then
            MessageBox.Show("Please Enter Subject Id")
        ElseIf txtSubject.Text = String.Empty
            MessageBox.Show("Please Enter Subject")
        Else
            con.Open()
            cmd.Connection = con
            cmd.CommandText = "Select * from Subject"
            mysqlda.SelectCommand = cmd
            mysqlda.Fill(ds, "Subject")
            DR = ds.Tables("Subject").NewRow
            DR("SubjectId") = txtSubjectId.Text
            DR("Subject") = txtSubject.Text
            ds.Tables("Subject").Rows.Add(DR)
            mysqlda.Update(ds, "Subject")
            con.Close()
            DataGridView()
            MessageBox.Show("Add Subject Sucessfully")
            Generate_id()
            Reset()
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class