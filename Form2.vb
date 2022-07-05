'Setup the Device by clicking the button
'Data Frame for Configuration

'______________________________________________________________________________________________________________________________________________
'|  Device Validation Token  |   BIN    |   Media Code  | BMS Category Code | Application Code |  Operator    |      API      | Bearer Code  |
'----------------------------------------------------------------------------------------------------------------------------------------------
'|          5 Byte           | 20 Byte  |    4 Byte     |      4 Byte       |      4 Byte      |    4 Byte    |    200 Byte   |  100 Byte    | 
'----------------------------------------------------------------------------------------------------------------------------------------------

' Total= 341 Byte String 



'Device Validation Token = DVT
'               1) Device ID = ECC01

'Media Code = MC
'               1) 4G (LTE)  = 4GLT
'               2) Bluetooth = BLLT
'               3) Both      = 4GBT

'BMS category Code = BCC
'               1) Esmito    = ESMT
'               2) Daly     = DALY

'Application Code  = AC
'               1) Fixed (Default)   = FIXD
'               2) Swap with immobilization  =SIMM
'               3) Swap with LS (CH) = SLSC
'               4) Swap with LS (CH+DCH) =SLCD
'               5) Swap with LS (Full) = SLFL

'Operator Code =OC
'               1) Airtel (Default)   = AIRT
'               2) VI  =VOID
'               3) BSNL = BSNL
'               4) JIO =JIOM



'----------------------------------------------------------------------------------------



Imports System
Imports System.Threading
Imports System.IO.Ports
Imports System.ComponentModel
Public Class Esmito_4G_Confi
    Dim DVT As String = "ECC01"
    Dim MC As String = ""
    Dim BCC As String = ""
    Dim AC As String = ""
    Dim OC As String = ""
    Dim BearerCode As String = ""
    Dim API As String = ""
    Dim Configuration_Frame As String = ""
    '------------------------------------------------
    Dim myPort As Array
    Delegate Sub SetTextCallback(ByVal [text] As String) 'Added to prevent threading errors during receiveing of data
    '------------------------------------------------
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        myPort = IO.Ports.SerialPort.GetPortNames()
        ComboBox1.Items.AddRange(myPort)



    End Sub
    '------------------------------------------------




    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.Text = "Daly" Then
            'TextBox2.Visible = True

            BCC = "DALY"
        Else
            ' TextBox2.Visible = False


            BCC = "ESMT"
        End If
    End Sub






    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Device_Config_Button.Click
        MsgBox(DVT & " | " & MC & " | " & BCC & " | " & AC & " | " & OC & " | " & BearerCode & " | " & API)
        Configuration_Frame = DVT & " | " & MC & " | " & BCC & " | " & AC & " | " & OC & " | " & BearerCode & " | " & API

        SerialPort1.Write(Configuration_Frame & vbCr) 'concatenate with \n

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.Text = "4G (LTE)" Then
            MC = "4GLT"
        ElseIf ComboBox2.Text = "Bluetooth" Then
            MC = "BLLT"
        ElseIf ComboBox2.Text = "4G (LTE) + Bluetooth" Then
            MC = "4GBT"
        Else
            MC = "0000"


        End If
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged

        If ComboBox4.Text = "Fixed (Default)" Then

            AC = "FIXD"

        ElseIf ComboBox4.Text = "Swap with immobilization" Then
            AC = "SIMM"

        ElseIf ComboBox4.Text = "Swap with LS (CH)" Then
            AC = "SLSC"

        ElseIf ComboBox4.Text = "Swap with LS (CH+DCH)" Then
            AC = "SLCD"

        ElseIf ComboBox4.Text = "Swap with LS (Full)" Then
            AC = "SLFL"

        Else AC = "0000"

        End If




    End Sub

    Private Sub Device_Connect_Button_Click(sender As Object, e As EventArgs) Handles Device_Connect_Button.Click
        SerialPort1.PortName = ComboBox1.Text
        SerialPort1.BaudRate = 115200
        SerialPort1.Open()
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        ComboBox2.Enabled = True
        ComboBox3.Enabled = True
        ComboBox4.Enabled = True
        ComboBox5.Enabled = True
        Device_Config_Button.Enabled = True
        RichTextBox2.Enabled = True
        Button2.Enabled = True
        PictureBox1.Visible = True

    End Sub

    Private Sub SerialPort1_DataReceived(sender As System.Object, e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        ReceivedText(SerialPort1.ReadExisting())
    End Sub

    Private Sub ReceivedText(ByVal [text] As String) 'input from ReadExisting
        If Me.RichTextBox1.InvokeRequired Then
            Dim x As New SetTextCallback(AddressOf ReceivedText)
            Me.Invoke(x, New Object() {(text)})
        Else
            Me.RichTextBox1.Text &= [text] 'append text
        End If
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        SerialPort1.Write(RichTextBox2.Text & vbCr) 'concatenate with \n
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        RichTextBox1.Text = ""
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedIndexChanged
        If ComboBox5.Text = "JIO" Then

            OC = "JIOM"
        ElseIf ComboBox5.Text = "BSNL" Then

            OC = "BSNL"

        ElseIf ComboBox5.Text = "VI" Then

            OC = "VOID"

        Else
            OC = "Airtel"

        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        myPort = IO.Ports.SerialPort.GetPortNames()
        ComboBox1.Items.AddRange(myPort)

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        BearerCode = TextBox2.Text
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        API = TextBox3.Text
    End Sub
End Class


