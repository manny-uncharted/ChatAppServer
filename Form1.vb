

Public Class Form1

    Private Server As TCPControl



    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Server = New TCPControl

        txtChat.Text = ":: SERVER STARTED ::" & vbCrLf


        ' ADD A HANDLER
        AddHandler Server.MessageReceived, AddressOf OnlineReceived

    End Sub

    Private Delegate Sub UpdateTextDelegate(TB As TextBox, txt As String)

    ' UPDATE TEXTBOX
    Private Sub UpdateText(TB As TextBox, txt As String)
        If TB.InvokeRequired Then
            TB.Invoke(New UpdateTextDelegate(AddressOf UpdateText), New Object() {TB, txt})
        Else
            If txt IsNot Nothing Then
                TB.AppendText(txt & vbCrLf)
            End If
        End If
    End Sub

    Private Sub OnlineReceived(sender As TCPControl, Data As String)
        UpdateText(txtChat, Data)
    End Sub

    ' Account for closing the loop created in the server

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Server.IsListening = False
    End Sub
End Class
