Public Class Form1
    Dim colorL, greenQ(16), redQ(9), gi, ri As Integer
 

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        colorL = 1
        ri = 0
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        colorL = 2
        gi = 0
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        SP1.Close()
        End
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If SP1.IsOpen Then
            LedOff()
            Button4.Text = "Connnect Bluetooth"
            SP1.Close()
        Else
            SP1.Open()
            LedOff()
            Button4.Text = "Disconnect Bluetooth"
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Label1.Text = "Current Time: " + TimeString
        Dim s = ShapeContainer1.Shapes
        greenQ = {&H1, &H3, &H6, &HC, &H18, &H30, &H60, &HC0, &H60, &H30, &H18, &HC, &H6, &H3, &H1, 0}
        redQ = {&HC0, &H60, &H30, &H18, &HC, &H6, &H3, &H1, 0}
        If SP1.IsOpen Then
            LedOff()
            For i = 0 To 7
                s(i).fillcolor = Color.DarkRed
                s(i + 8).fillcolor = Color.DarkGreen
            Next
            For i = 0 To 10 ^ 7
            Next
            If colorL = 1 And redQ(ri) > 0 Then
                SP1.Write("R" & redQ(ri))
                Display(redQ(ri))
                ri += 1
            End If
            If colorL = 2 And greenQ(gi) > 0 Then
                SP1.Write("G" & greenQ(gi))
                Display(greenQ(gi))
                gi += 1
            End If
        Else
            For i = 0 To 15
                s(i).fillcolor = Color.Transparent
            Next
        End If 
    End Sub
    Private Sub LedOff()
        SP1.Write("R0")
        SP1.Write("G0")
    End Sub
    Private Sub Display(ByVal no)
        Dim s = ShapeContainer1.Shapes
        For i = 0 To 7
            If no Mod 2 = 1 And colorL = 1 Then s(i).fillcolor = Color.Red
            If no Mod 2 = 1 And colorL = 2 Then s(i + 8).fillcolor = Color.GreenYellow
            no \= 2
        Next
    End Sub
End Class
