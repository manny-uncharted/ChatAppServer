Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading


Public Class TCPControl

    Public Event MessageReceived(sender As TCPControl, Data As String)


    ' SERVER CONFIGURATIONS
    Public ServerIP As IPAddress = IPAddress.Parse("172.16.9.48") ' IP of the server
    'Public ServerIP As IPAddress = IPAddress.Parse("172.16.0.1") ' IP of the server
    Public ServerPort As Integer = 64555
    Public Server As TcpListener

    Private CommThread As Thread ' Thread for the communication
    Public IsListening As Boolean = True ' Flag for the communication thread


    ' CLIENT CONFIGURATIONS
    Public Client As TcpClient
    Public ClientData As StreamReader

    Public Sub New()
        Server = New TcpListener(ServerIP, ServerPort)
        Server.Start()

        CommThread = New Thread(New ThreadStart(AddressOf Listening))
        CommThread.Start()
    End Sub

    Private Sub Listening()
        ' CREATE LISTENER LOOP
        Do Until IsListening = False
            ' ACCEPT INCOMING CONNECTIONS
            If Server.Pending = True Then
                Client = Server.AcceptTcpClient
                ClientData = New StreamReader(Client.GetStream)

            End If


            ' RAISE EVENT FOR INCOMING MESSAGES
            Try
                RaiseEvent MessageReceived(Me, ClientData.ReadLine)
            Catch ex As Exception

            End Try


            ' REDUCE CPU USAGE BY PUTTING THREAD TO SLEEP BRIEFLY
            Thread.Sleep(100)

        Loop
    End Sub


End Class
