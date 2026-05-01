Public Class Form2

    Private ReadOnly settingsRepository As New SettingsRepository()
    Private ReadOnly settingsFormService As New SettingsFormService()

    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Dim AutodbBak As Integer

    Const WM_NCLBUTTONDOWN As Integer = &HA1S
    Const HTBOTTOMRIGHT As Integer = 17

    Private Sub Form2_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown, MenuStrip1.MouseDown

        drag = True
        mousex = System.Windows.Forms.Cursor.Position.X - Me.Left
        mousey = System.Windows.Forms.Cursor.Position.Y - Me.Top

    End Sub

    Private Sub Form2_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove, MenuStrip1.MouseMove

        If drag Then

            Me.Top = System.Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = System.Windows.Forms.Cursor.Position.X - mousex

        End If

    End Sub

    Private Sub Form2_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp, MenuStrip1.MouseUp

        drag = False

    End Sub

    Private Sub SS_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)

        Me.Cursor = Cursors.Arrow

    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            Dim settings = settingsRepository.GetSettings(1)

            If settings Is Nothing Then

                Exit Sub

            End If

            EinschaltenCB.Checked = settings.AutoDbBackupEnabled
            MinutenTB.Text = settings.AutoDbBackupTime.ToString()
            FilePatchTB.Text = settings.AutoDbBackupPath
        Catch ex As Exception

            MsgBox("Fehler:" & ex.Message)

        End Try

    End Sub

    Private Sub X_Click(sender As Object, e As EventArgs) Handles x.Click

        Me.Close()

    End Sub

    Private Sub FilePatchTB_Click(sender As Object, e As EventArgs) Handles FilePatchTB.Click

        If SaveFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

            FilePatchTB.Text = SaveFileDialog1.FileName

        End If

    End Sub

    Private Sub SchließenButton_Click(sender As Object, e As EventArgs) Handles SchließenButton.Click

        Me.Close()

    End Sub

    Private Sub SpeichernButton_Click(sender As Object, e As EventArgs) Handles SpeichernButton.Click

        Dim backupMinutes As Integer
        Dim validationMessage As String = String.Empty

        If Not settingsFormService.ValidateBackupSettings(MinutenTB.Text, FilePatchTB.Text, backupMinutes, validationMessage) Then

            MsgBox(validationMessage)
            Exit Sub

        End If

        If EinschaltenCB.Checked = True Then

            AutodbBak = 1
            Sprung(backupMinutes)

        ElseIf EinschaltenCB.Checked = False Then

            AutodbBak = 0
            Form1.Timer1.Enabled = False
            Sprung(backupMinutes)

        End If

    End Sub

    Private Sub Sprung(backupMinutes As Integer)

        Try

            settingsRepository.UpdateSettings(New SettingsRecord With {
                .Id = 1,
                .AutoDbBackupEnabled = (AutodbBak = 1),
                .AutoDbBackupTime = backupMinutes,
                .AutoDbBackupPath = FilePatchTB.Text
            })

            MsgBox("Einstellungen wurden gespeichert! ;-)")
            Form1.AutodbBackupEX()
        Catch ex As Exception

            MsgBox("Fehler:" & ex.Message)

        End Try

    End Sub

End Class
