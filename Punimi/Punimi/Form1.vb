Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load


    End Sub
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Dim iExit As DialogResult
        iExit = MessageBox.Show("Confirm if you want to exit", "Student Ranking", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If iExit = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.DataGridView1.Rows.Clear()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim a As Integer
        If DataGridView1.SelectedRows.Count > 0 Then
            For q = DataGridView1.SelectedRows.Count - 1 To 0 Step -1
                DataGridView1.Rows.RemoveAt(DataGridView1.SelectedRows(q).Index)

            Next
        Else
            MessageBox.Show("No row selected")
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        DataGridView1.Rows.Add(txtStudent_ID.Text, txtEmri.Text, txtMbiemri.Text, txtStatistike.Text, txtAnalize1.Text, txtOOP_GUI.Text, txtDatabaze.Text, txtAnalize2.Text, txtI_S.Text, txtB_I.Text, txtI_A.Text, txtMesatarja.Text)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        ResetTextBox()

    End Sub

    Public Sub ResetTextBox(Optional ByVal txtcol As Control.ControlCollection = Nothing)
        If txtcol Is Nothing Then txtcol = Me.Controls
        For Each txt As Control In txtcol
            If TypeOf (txt) Is TextBox Then
                DirectCast(txt, TextBox).Clear()
            Else
                If Not txt.Controls Is Nothing OrElse txt.Controls.Count <> 0 Then
                    ResetTextBox(txt.Controls)

                End If
            End If
        Next
    End Sub

    Private Sub Numbers_Only(sender As Object, e As KeyPressEventArgs) Handles txtStatistike.KeyPress, txtTotal.KeyPress, txtOOP_GUI.KeyPress, txtMesatarja.KeyPress, txtI_S.KeyPress, txtI_A.KeyPress, txtDatabaze.KeyPress, txtB_I.KeyPress, txtAnalize2.KeyPress, txtAnalize1.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub btnRanking_Click(sender As Object, e As EventArgs) Handles btnRanking.Click
        Dim R(15) As Double
        R(0) = Val(txtStatistike.Text)
        R(1) = Val(txtAnalize1.Text)
        R(2) = Val(txtOOP_GUI.Text)
        R(3) = Val(txtDatabaze.Text)
        R(4) = Val(txtAnalize2.Text)
        R(5) = Val(txtI_S.Text)
        R(6) = Val(txtB_I.Text)
        R(7) = Val(txtI_A.Text)

        R(8) = (R(0) + R(1) + R(2) + R(3) + R(4) + R(5) + R(6) + R(7)) / 8
        R(9) = R(0) + R(1) + R(2) + R(3) + R(4) + R(5) + R(6) + R(7)

        txtTotal.Text = R(9)
        txtMesatarja.Text = R(8)


    End Sub

    Private Sub btnTranscript_Click(sender As Object, e As EventArgs) Handles btnTranscript.Click
        rtTranscript.AppendText(" Student_ID: " + txtStudent_ID.Text + vbTab + "Emri: " + txtEmri.Text + vbTab + "Mbiemri: " + txtMbiemri.Text + vbNewLine)
        rtTranscript.AppendText(" Statistike:" + vbTab + vbTab + vbTab + (txtStatistike.Text) + vbNewLine)
        rtTranscript.AppendText(" Analize1:" + vbTab + vbTab + vbTab + vbTab + (txtAnalize1.Text) + vbNewLine)
        rtTranscript.AppendText(" OOP GUI:" + vbTab + vbTab + vbTab + (txtOOP_GUI.Text) + vbNewLine)
        rtTranscript.AppendText(" Databaze:" + vbTab + vbTab + vbTab + (txtDatabaze.Text) + vbNewLine)
        rtTranscript.AppendText(" Analize2:" + vbTab + vbTab + vbTab + vbTab + (txtAnalize2.Text) + vbNewLine)
        rtTranscript.AppendText(" I.S:" + vbTab + vbTab + vbTab + vbTab + (txtI_S.Text) + vbNewLine)
        rtTranscript.AppendText(" B.I:" + vbTab + vbTab + vbTab + vbTab + (txtB_I.Text) + vbNewLine)
        rtTranscript.AppendText(" I_A:" + vbTab + vbTab + vbTab + vbTab + (txtI_A.Text) + vbNewLine)
        rtTranscript.AppendText(" Mesatarja:" + vbTab + vbTab + vbTab + (txtMesatarja.Text) + vbNewLine)
        rtTranscript.AppendText(vbNewLine)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Chat.Show()
    End Sub
End Class
