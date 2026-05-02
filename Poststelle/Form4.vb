Public Class Form4

    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer

    Const WM_NCLBUTTONDOWN As Integer = &HA1S
    Const HTBOTTOMRIGHT As Integer = 17

    Private Sub Form4_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown, MenuStrip1.MouseDown

        drag = True
        mousex = System.Windows.Forms.Cursor.Position.X - Me.Left
        mousey = System.Windows.Forms.Cursor.Position.Y - Me.Top

    End Sub

    Private Sub Form4_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove, MenuStrip1.MouseMove

        If drag Then

            Me.Top = System.Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = System.Windows.Forms.Cursor.Position.X - mousex

        End If

    End Sub

    Private Sub Form4_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp, MenuStrip1.MouseUp

        drag = False

    End Sub

    Private Sub SS_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)

        Me.Cursor = Cursors.Arrow

    End Sub


    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ApplyApplicationIcon(Me)

    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        PictureBox1.Location = New Point(PictureBox1.Location.X + 5, PictureBox1.Location.Y + 5)
        If PictureBox1.Location.X + PictureBox1.Width > Me.Width Then
            Timer1.Stop()
            Timer2.Start()
        End If

        If PictureBox1.Location.Y + PictureBox1.Height > Me.Height Then
            Timer1.Stop()
            Timer3.Start()
        End If
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        PictureBox1.Location = New Point(PictureBox1.Location.X - 5, PictureBox1.Location.Y + 5)
        If PictureBox1.Location.Y + PictureBox1.Height > Me.Height Then
            Timer2.Stop()
            Timer4.Start()
        End If
        If PictureBox1.Location.X < 0 Then
            Timer2.Stop()
            Timer1.Start()
        End If
    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        PictureBox1.Location = New Point(PictureBox1.Location.X + 5, PictureBox1.Location.Y - 5)
        If PictureBox1.Location.X + PictureBox1.Width > Me.Width Then
            Timer3.Stop()
            Timer4.Start()
        End If
        If (PictureBox1.Location.Y < 0) Then
            Timer3.Stop()
            Timer1.Start()
        End If
    End Sub

    Private Sub Timer4_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer4.Tick
        PictureBox1.Location = New Point(PictureBox1.Location.X - 5, PictureBox1.Location.Y - 5)
        If PictureBox1.Location.X < 0 Then
            Timer4.Stop()
            Timer3.Start()
        End If
        If (PictureBox1.Location.Y < 0) Then
            Timer4.Stop()
            Timer2.Start()
        End If
    End Sub


    Private Sub X_Click(sender As Object, e As EventArgs) Handles x.Click

        Me.Close()

    End Sub


    Private Sub SchließenButton_Click(sender As Object, e As EventArgs) Handles SchließenButton.Click

        Me.Close()

    End Sub


    '----

End Class
