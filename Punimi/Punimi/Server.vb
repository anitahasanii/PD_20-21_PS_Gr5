Imports System.Net.Sockets
Imports System.Net
Imports System.IO
Public Class Server
    Dim ServerStatus As Boolean = False
    Dim ServerTrying As Boolean = False
    Dim Server As TcpListener
    Dim Clients As New List(Of TcpClient)

    Private Sub Server_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        StartServer()
    End Sub

    Function StartServer()
        If ServerStatus = False Then
            ServerTrying = True
            Try
                Server = New TcpListener(IPAddress.Any, 4305)
                Server.Start()
                ServerStatus = True
                Threading.ThreadPool.QueueUserWorkItem(AddressOf Handler_Client)
            Catch ex As Exception
                ServerStatus = False
            End Try
            ServerTrying = False
        End If

        Return True
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        StopServer()
    End Sub

    Function StopServer()
        If ServerStatus = True Then
            ServerTrying = True
            Try
                For Each Client As TcpClient In Clients
                    Client.Close()
                Next
                Server.Stop()
                ServerStatus = False
            Catch ex As Exception
                StopServer()
            End Try
        End If
        Return True
    End Function

    Function Handler_Client(ByVal state As Object)
        Dim TempClient As TcpClient
        Try
            Using Client As TcpClient = Server.AcceptTcpClient

                If ServerTrying = False Then
                    Threading.ThreadPool.QueueUserWorkItem(AddressOf Handler_Client)
                End If
                Clients.Add(Client)
                TempClient = Client
                Dim TX As New StreamWriter(Client.GetStream)
                Dim RX As New StreamReader(Client.GetStream)
                If RX.BaseStream.CanRead = True Then
                    While RX.BaseStream.CanRead = True
                        Dim RawData As String = RX.ReadLine
                        Richtextbox1.Text += Client.Client.RemoteEndPoint.ToString + ">>" + RawData + vbNewLine
                    End While
                End If
                If RX.BaseStream.CanRead = False Then
                    Client.Close()
                    Clients.Remove(Client)
                End If

            End Using
        Catch ex As Exception
            If TempClient.GetStream.CanRead = False Then
                TempClient.Close()
                Clients.Remove(TempClient)
            End If
        Catch ex As Exception
            If TempClient.GetStream.CanRead = False Then
                TempClient.Close()
                Clients.Remove(TempClient)
            End If
        End Try


        Return True
    End Function

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Threading.ThreadPool.QueueUserWorkItem(AddressOf SendToClients, TextBox1.Text)
    End Sub
    Function SendToClients(ByVal Data As String)
        If ServerStatus = True Then
            If Clients.Count > 0 Then

                Try
                    For Each Client As TcpClient In Clients
                        Dim TX1 As New StreamWriter(Client.GetStream)
                        '' Dim RX1 As New StreamReader(Client.GetStream)
                        TX1.WriteLine(Data)
                        TX1.Flush()
                    Next

                Catch ex As Exception
                    SendToClients(Data)
                End Try
            End If
        End If
    End Function


End Class