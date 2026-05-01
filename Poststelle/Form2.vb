Imports System.Data.SQLite

Public Class Form2

    Dim con As New SQLiteConnection("Data Source=Poststelle.db")

    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Dim AutodbBak As Integer

    Const WM_NCLBUTTONDOWN As Integer = &HA1S
    Const HTBOTTOMRIGHT As Integer = 17

    Private Sub Form2_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown, MenuStrip1.MouseDown

        drag = True
        mousex = Windows.Forms.Cursor.Position.X - Me.Left
        mousey = Windows.Forms.Cursor.Position.Y - Me.Top

    End Sub

    Private Sub Form2_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove, MenuStrip1.MouseMove

        If drag Then

            Me.Top = Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = Windows.Forms.Cursor.Position.X - mousex

        End If

    End Sub

    Private Sub Form2_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp, MenuStrip1.MouseUp

        drag = False

    End Sub

    Private Sub SS_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)

        Me.Cursor = Cursors.Arrow

    End Sub


    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim com As New SQLiteCommand
        Dim adapter As New SQLiteDataAdapter
        Dim rd As SQLiteDataReader

        con.Open()

        If con.State = ConnectionState.Open Then

            Try

                com = New SQLiteCommand("SELECT * FROM Einstellungen WHERE ID = '1'", con)

                rd = com.ExecuteReader

                rd.Read()

                If (rd("AutodbBackup")) = "1" Then

                    EinschaltenCB.Checked = True

                End If

                MinutenTB.Text = (rd("AutodbBackupTime"))
                FilePatchTB.Text = (rd("AutodbBackupPfad"))

                rd.Close()
                con.Close()
                com.Dispose()

            Catch ex As Exception

                MsgBox("Fehler:" & ex.Message)

            End Try

        End If

    End Sub


    Private Sub X_Click(sender As Object, e As EventArgs) Handles x.Click

        Me.Close()

    End Sub


    Private Sub FilePatchTB_Click(sender As Object, e As EventArgs) Handles FilePatchTB.Click

        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then

            FilePatchTB.Text = SaveFileDialog1.FileName

        End If

    End Sub


    Private Sub SchließenButton_Click(sender As Object, e As EventArgs) Handles SchließenButton.Click

        Me.Close()

    End Sub


    Private Sub SpeichernButton_Click(sender As Object, e As EventArgs) Handles SpeichernButton.Click

        If MinutenTB.Text = "Minuten" AndAlso MinutenTB.Text = "" OrElse FilePatchTB.Text = "FilePatch" AndAlso FilePatchTB.Text = "" Then

            MsgBox("Bitte alle Felder ausfüllen! ;-)")

        End If

        If EinschaltenCB.Checked = True Then

            AutodbBak = 1
            Sprung()

        ElseIf EinschaltenCB.Checked = False Then

            AutodbBak = 0
            Form1.Timer1.Enabled = False
            Sprung()

        End If

    End Sub


    Private Sub Sprung()

        Dim AutodbBakTim As String = MinutenTB.Text
        Dim AutodbBakPf As String = FilePatchTB.Text

        Dim com As New SQLiteCommand
        Dim adapter As New SQLiteDataAdapter

        con.Open()

        If con.State = ConnectionState.Open Then

            Try

                com = New SQLiteCommand(
                                         "UPDATE Einstellungen SET 
                                         AutodbBackup = '" & AutodbBak & "',
                                         AutodbBackupTime = '" & AutodbBakTim & "',
                                         AutodbBackupPfad = '" & AutodbBakPf & "'
                                         WHERE ID = 1", con)

                com.ExecuteNonQuery()
                con.Close()

                MsgBox("Einstellungen Wurden Gespeichert! ;-)")
                Form1.AutodbBackupEX()

            Catch ex As Exception

                MsgBox("Fehler:" & ex.Message)

            End Try

        End If

    End Sub


    '----

End Class