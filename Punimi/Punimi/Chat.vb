Public Class Chat
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Client.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Server.Show()
    End Sub
End Class