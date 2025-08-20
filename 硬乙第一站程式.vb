Public Class Form1
    Dim ledColor, greenQuery(16), redQuery(9), greenIndex, redIndex As Integer

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ledColor = 1
        redIndex = 0
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ledColor = 2
        greenIndex = 0
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        SerialPort1.Close()
        End
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If SerialPort1.IsOpen Then
            LedOff()
            Button4.Text = "Disconnect Bluetooth"
            SerialPort1.Close()
        Else
            SerialPort1.Open()
            LedOff()
            Button4.Text = "Connnect Bluetooth"
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim s = ShapeContainer1.Shapes
        greenQuery = {&H1, &H3, &H6, &HC, &H18, &H30, &H60, &HC0, &H60, &H30, &H18, &HC, &H6, &H3, &H1, 0}
        redQuery = {&HC0, &H60, &H30, &H18, &HC, &H6, &H3, &H1, 0}
        Label1.Text = "Current Time: " + TimeString
        If SerialPort1.IsOpen Then
            For i = 0 To 7
                s(i).fillcolor = Color.DarkRed
                s(i + 8).fillcolor = Color.DarkGreen
            Next
            LedOff()
            For i = 0 To 10 ^ 8
            Next
            If ledColor = 1 And redQuery(redIndex) > 0 Then
                SerialPort1.Write("R" & redQuery(redIndex))
                Display(ledColor, redQuery(redIndex))
                redIndex += 1
            End If
            If ledColor = 2 And greenQuery(greenIndex) > 0 Then
                SerialPort1.Write("G" & greenQuery(greenIndex))
                Display(ledColor, greenQuery(greenIndex))
                greenIndex += 1
            End If
        Else
            For i = 0 To 15
                s(i).fillcolor = Color.Transparent
            Next
        End If 
    End Sub

    Private Sub LedOff()
        SerialPort1.Write("R0")
        SerialPort1.Write("G0")
    End Sub

    Private Sub Display(ByVal ledColor, ByVal no)
        Dim s = ShapeContainer1.Shapes
        For i = 0 To 7
            If no Mod 2 = 1 And ledColor = 1 Then s(i).fillcolor = Color.Red
            If no Mod 2 = 1 And ledColor = 2 Then s(i + 8).fillcolor = Color.GreenYellow
            no \= 2
        Next
    End Sub
    
End Class
