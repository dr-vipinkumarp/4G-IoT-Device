Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled() = True


    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ProgressBar2.Increment(1)
        If ProgressBar2.Value < 50 Then
            Label1.Text = "Loading System Configuration"


        ElseIf ProgressBar2.Value > 90 Then ' ProgressBar2.Value < 60 Then

            'abel1.Text = "Configuring Cloud Connection"

            ' ElseIf ProgressBar2.Value > 60 Then ' ProgressBar2.Value < 90 Then

            'Label1.Text = "Setting Up Configuration Window"

            'ElseIf ProgressBar2.Maximum >= 100 Then
            Timer1.Enabled() = False
            Me.Hide()
            Esmito_4G_Confi.Show()
        End If





    End Sub
End Class
