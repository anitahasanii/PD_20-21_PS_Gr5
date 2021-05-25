Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Public Class Client

    Dim Client As TcpClient
    Dim RX As StreamReader
    Dim TX As StreamWriter
    Private Sub Client_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Try
            Client = New TcpClient("127.0.0.1", 4305)
            If Client.GetStream.CanRead = True Then
                RX = New StreamReader(Client.GetStream)
                TX = New StreamWriter(Client.GetStream)
                Threading.ThreadPool.QueueUserWorkItem(AddressOf Connected)


            End If

        Catch ex As Exception
            RichTextBox1.Text = "Failed to connect , E : " + ex.Message + vbNewLine

        End Try
    End Sub

    Function Connected()
        If RX.BaseStream.CanRead = True Then
            Try
                While RX.BaseStream.CanRead = True
                    Dim RawData As String = RX.ReadLine
                    If RawData.ToUpper = "/MSG" Then
                        Threading.ThreadPool.QueueUserWorkItem(AddressOf MSG1, "Hello World")

                    Else
                        RichTextBox1.Text += "Server>>" + RawData + vbNewLine
                    End If
                End While
            Catch ex As Exception
                Client.Close()
            End Try
        End If
        Return True

    End Function
    Function MSG1(ByVal Data As String)
        MsgBox(Data)
        Return True
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            If TextBox1.Text.Length > 0 Then
                SendToServer(TextBox1.Text)
                TextBox1.Clear()

            End If
        End If
    End Sub

    Function SendToServer(ByVal Data As String)
        Try
            TX.WriteLine(Data)
            TX.Flush()

        Catch ex As Exception

        End Try
        Return True
    End Function


End Class