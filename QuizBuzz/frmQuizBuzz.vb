Public Class frmQuizBuzz
    Private Sub frmQuizBuzz_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.MaximumSize = New Size(1000, 550)
        'Me.MinimumSize = Me.MaximumSize
        'Me.MaximizeBox = True
        'Me.MinimizeBox = True
    End Sub

    Private Sub NewExpertToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewExpertToolStripMenuItem.Click
        frmNewExpert.Show()
    End Sub

    Private Sub EditExpertToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditExpertToolStripMenuItem.Click
        frmEditExpert.Show()
    End Sub

    Private Sub NewTeamToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewTeamToolStripMenuItem.Click
        frmNewTeam.Show()
    End Sub

    Private Sub NewCandidateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewCandidateToolStripMenuItem.Click
        FrmNewCandidate.Show()
    End Sub

    Private Sub EditCandidateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditCandidateToolStripMenuItem.Click
        FrmEditCandidate.Show()
    End Sub

    Private Sub NewQuestionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewQuestionToolStripMenuItem.Click
        FrmNewQuestion.Show()
    End Sub

    Private Sub EditQuestionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditQuestionToolStripMenuItem.Click
        frmEditQuestion.Show()
    End Sub

    Private Sub NewAdminToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewAdminToolStripMenuItem.Click
        frmAdmin.Show()
    End Sub

    Private Sub EditTeamToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditTeamToolStripMenuItem.Click
        FrmEditTeam.Show()
    End Sub

    Private Sub NewQuizMasterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewQuizMasterToolStripMenuItem.Click
        FrmNewQuizMaster.Show()
    End Sub

    Private Sub EditQuizMasterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditQuizMasterToolStripMenuItem.Click
        FrmEditQuizMaster.Show()
    End Sub
    Private Sub LongOutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LongOutToolStripMenuItem.Click

        Dim result = MessageBox.Show("Are you would like to Log Out?", "Closing Quiz", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If (result = DialogResult.Yes) Then
        End If
        Me.Hide()
        FrmLogIn.Show()
    End Sub

    Private Sub QuizToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QuizToolStripMenuItem.Click
        frmNewQuizDate.Show()
    End Sub

    Private Sub QuizTestToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QuizTestToolStripMenuItem.Click
        FrmEditQuizDate.Show()
    End Sub

    Private Sub AdminToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdminToolStripMenuItem.Click

    End Sub

    Private Sub AddSubjectsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddSubjectsToolStripMenuItem.Click
        FrmAddSubject.Show()
    End Sub

    Private Sub EditSubjectsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditSubjectsToolStripMenuItem.Click
        FrmEditSubject.Show()
    End Sub

    Private Sub QuestionReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QuestionReportToolStripMenuItem.Click
        frmQuestionReport.Show()
    End Sub

    Private Sub QuizDateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QuizDateToolStripMenuItem.Click
        FrmQuizDateReport.Show()
    End Sub

    Private Sub QuizMasterReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QuizMasterReportToolStripMenuItem.Click
        FrmQuizMasterReport.Show()
    End Sub

    Private Sub ExpertReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExpertReportToolStripMenuItem.Click
        FrmExpertReport.Show()
    End Sub

    Private Sub CandidateReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CandidateReportToolStripMenuItem.Click
        FrmCandidateReport.Show()
    End Sub

    Private Sub TeamReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TeamReportToolStripMenuItem.Click
        FrmTeamReport.Show()
    End Sub

    Private Sub QuizTestToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles QuizTestToolStripMenuItem1.Click
        FrmQuizTest.Show()
    End Sub
End Class
