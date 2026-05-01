Imports System.Drawing
Imports System.Drawing.Printing
Imports System.IO

Public Class Form1

    Private ReadOnly settingsRepository As New SettingsRepository()
    Private ReadOnly recipientRepository As New RecipientRepository()
    Private ReadOnly packageRepository As New PackageRepository()

    Dim myBindingSource As New BindingSource
    Dim myData As New DataTable

    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Dim Pat As String
    Dim DTM As String
    Dim Send As String
    Dim Empf As String
    Dim Abl As String
    Dim SendNr As String
    Dim Mand As String
    Dim Gedruckt As String

    Public ste As String
    Public EmpaengerValue As String

    Const WM_NCLBUTTONDOWN As Integer = &HA1S
    Const HTBOTTOMRIGHT As Integer = 17

    Private Sub Form1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown, MenuStrip1.MouseDown

        drag = True
        mousex = System.Windows.Forms.Cursor.Position.X - Me.Left
        mousey = System.Windows.Forms.Cursor.Position.Y - Me.Top

    End Sub

    Private Sub Form1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove, MenuStrip1.MouseMove

        If drag Then

            Me.Top = System.Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = System.Windows.Forms.Cursor.Position.X - mousex

        End If

    End Sub

    Private Sub Form1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp, MenuStrip1.MouseUp

        drag = False

    End Sub

    Private Sub SS_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles SS.MouseDown

        If Me.WindowState <> FormWindowState.Maximized Then

            If e.Button = System.Windows.Forms.MouseButtons.Left Then

                Me.Capture = False
                Dim theCursor As Cursor = Cursors.Arrow
                Dim Direction As New IntPtr(Bottom)

                If e.X = SS.Width - 1 Or e.X = SS.Width - 2 Or e.X = SS.Width - 3 Or e.X = SS.Width - 4 Or e.X = SS.Width - 5 _
                    Or e.Y = SS.Height - 1 Or e.Y = SS.Height - 2 Or e.Y = SS.Height - 3 Or e.Y = SS.Height - 4 Or e.Y = SS.Height - 5 Then

                    Select Case e.X

                        Case SS.Width - 5 To SS.Width - 1

                            Select Case e.Y

                                Case SS.Height - 5 To SS.Height - 1
                                    Direction = CType(HTBOTTOMRIGHT, IntPtr)
                                    theCursor = Cursors.PanNW

                            End Select

                    End Select

                    Me.Cursor = theCursor
                    Dim msg As Message =
                            Message.Create(Me.Handle, WM_NCLBUTTONDOWN,
                                Direction, IntPtr.Zero)
                    Me.DefWndProc(msg)

                End If

            End If

        End If

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.SS.SizingGrip = False

        If settingsRepository.DatabaseExists() Then

            EmpfaengerCBFuellen()
            SenderCBFuellen()
            GenSeite()
            UpdGrid()
            AutodbBackupEX()

        Else

            Dbersstellen()

        End If

    End Sub

    Private Sub Dbersstellen()

        settingsRepository.CreateDatabase()

        MsgBox("erster Start Datenbank wird erstellt!... ;-)")

        DBStrukturerstellen()

    End Sub

    Private Sub DBStrukturerstellen()

        settingsRepository.CreateSchema()

        MsgBox("DB Tabellen und Struktur erstellt! ;-)")

        StandardEinstellungen()

    End Sub

    Private Sub StandardEinstellungen()

        Try

            settingsRepository.InsertDefaultSettings()

            MsgBox("Standard Einstellungen Wurden Eingerichtet! ;-)")

            EmpfaengerCBFuellen()
            SenderCBFuellen()
        Catch ex As Exception

            MsgBox("Fehler:" & ex.Message)

        End Try

    End Sub

    Public Sub AutodbBackupEX()

        Try

            Dim settings = settingsRepository.GetSettings(1)

            Timer1.Enabled = settings IsNot Nothing AndAlso settings.AutoDbBackupEnabled

            If settings IsNot Nothing AndAlso settings.AutoDbBackupEnabled Then

                Timer1.Interval = settings.AutoDbBackupTime * 60000

            End If
        Catch ex As Exception

            MsgBox("Fehler:" & ex.Message)

        End Try

    End Sub

    Private Sub Timer1_Tick_1(sender As Object, e As EventArgs) Handles Timer1.Tick

        Try

            Dim settings = settingsRepository.GetSettings(1)

            If settings Is Nothing Then

                Exit Sub

            End If

            My.Computer.FileSystem.CopyFile(DatabaseConfig.DatabaseFilePath, settings.AutoDbBackupPath, overwrite:=True)
        Catch ex As Exception

            MsgBox("Fehler:" & ex.Message)

        End Try

    End Sub

    Private Sub X_Click(sender As Object, e As EventArgs) Handles x.Click

        Me.Close()

    End Sub

    Private Sub Min_Click(sender As Object, e As EventArgs) Handles min.Click

        Me.WindowState = FormWindowState.Minimized

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If Me.WindowState = FormWindowState.Maximized Then

            Me.WindowState = FormWindowState.Normal

        ElseIf Me.WindowState = FormWindowState.Normal Then

            Me.WindowState = FormWindowState.Maximized

        End If

    End Sub

    Private Sub EmpfängerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EmpfängerToolStripMenuItem.Click

        Form3.Show()

    End Sub

    Private Sub AutodbbackupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AutodbBackupToolStripMenuItem.Click

        Form2.Show()

    End Sub

    Private Sub SenderCB_keydown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles SenderCB.KeyDown

        If e.KeyCode = Keys.Enter Then

            EmpfaengerCB.Focus()

        End If

    End Sub

    Private Sub SenderCB_TextChanged(sender As Object, e As EventArgs) Handles SenderCB.TextChanged

        If Not SenderCB.Text = "Sender" And Not SeiteCB.Text = "Seite" Then

            UpdGrid()

        End If

    End Sub

    Private Sub SenderCB_Leave(sender As Object, e As EventArgs) Handles SenderCB.Leave

        If SenderCB.Text = "" Then

            SenderCB.Text = "Sender"

        End If

    End Sub

    Private Sub SenderCBFuellen()

        PopulateComboBox(SenderCB, packageRepository.GetDistinctSenders())

    End Sub

    Private Sub EmpfaengerCB_keydown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles EmpfaengerCB.KeyDown

        If e.KeyCode = Keys.Enter Then

            If Not EmpfaengerCB.Items.Contains(EmpfaengerCB.Text) And Not EmpfaengerCB.Text = "Empfänger" And Not EmpfaengerCB.Text = "" Then

                If MsgBox("Empfänger Nicht Gefunden Eintragen? ;-)", vbYesNo) = vbYes Then

                    Form3.Show()
                    Form3.AbladestelleCB.Focus()
                    Form3.NameCB.Text = EmpfaengerCB.Text

                Else

                    SendungsNummerTB.Focus()
                    SendungsNummerTB.SelectAll()

                End If

            Else

                SendungsNummerTB.Focus()
                SendungsNummerTB.SelectAll()

            End If

        End If

    End Sub

    Private Sub EmpfaengerCB_TextChanged(sender As Object, e As EventArgs) Handles EmpfaengerCB.TextChanged

        If Not EmpfaengerCB.Text = "Empfänger" And Not SeiteCB.Text = "Seite" Then

            UpdGrid()

        End If

    End Sub

    Private Sub EmpfaengerCB_Leave(sender As Object, e As EventArgs) Handles EmpfaengerCB.Leave

        If EmpfaengerCB.Text = "" Then

            EmpfaengerCB.Text = "Empfänger"

        End If

    End Sub

    Public Sub EmpfaengerCBFuellen()

        PopulateComboBox(EmpfaengerCB, recipientRepository.GetDistinctNames())

    End Sub

    Private Sub SendungsNummerTB_keydown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles SendungsNummerTB.KeyDown

        If e.KeyCode = Keys.Enter Then

            If Not EmpfaengerCB.Items.Contains(EmpfaengerCB.Text) And Not EmpfaengerCB.Text = "Empfänger" And Not EmpfaengerCB.Text = "" Then

                If MsgBox("Empfänger Nicht Gefunden Eintragen? ;-)", vbYesNo) = vbYes Then

                    Form3.Show()
                    Form3.AbladestelleCB.Focus()
                    Form3.NameCB.Text = EmpfaengerCB.Text

                Else

                    EmpfaengerCB.Focus()

                End If

            Else

                EmpfaengerdatenSammeln()

            End If

        End If

    End Sub

    Private Sub SendungsNummerTB_TextChanged(sender As Object, e As EventArgs) Handles SendungsNummerTB.TextChanged

        If Not SendungsNummerTB.Text = "SendungsNummer" And Not SeiteCB.Text = "Seite" Then

            UpdGrid()

        ElseIf senderCB.Text = "Poststelle" AndAlso EmpfaengerCb.text = "Poststelle" AndAlso SendungsNummerTB.Text = "Poststelle" Then

            Form4.Show()

        End If

    End Sub

    Private Sub SendungsNummerTB_Leave(sender As Object, e As EventArgs) Handles SendungsNummerTB.Leave

        If SendungsNummerTB.Text = "" Then

            SendungsNummerTB.Text = "SendungsNummer"

        End If

    End Sub

    Private Sub DateTimePicker_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker.ValueChanged

        SeiteCB.Text = "Seite"
        GenSeite()
        UpdGrid()

    End Sub

    Private Sub SeiteCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SeiteCB.SelectedIndexChanged

        UpdGrid()

    End Sub

    Private Sub GedrucktCB_CheckedChanged(sender As Object, e As EventArgs) Handles GedrucktCB.CheckedChanged

        UpdGrid()

    End Sub

    Private Sub DatumFilterCB_CheckedChanged(sender As Object, e As EventArgs) Handles DatumFilterCB.CheckedChanged

        If File.Exists(DatabaseConfig.DatabaseFilePath) Then

            GenSeite()

        End If

        If Not SeiteCB.Text = "Seite" Then

            UpdGrid()

        End If

    End Sub

    Private Sub DruckenButton_Click(sender As Object, e As EventArgs) Handles DruckenButton.Click

        If DatumFilterCB.Checked And Not SeiteCB.Text = "Alle" Then

            Drucken()

        ElseIf MsgBox("Achtung der DatumFilter oder Alle Seiten Sind Ausgewählt! Wollen Sie wirklich Drucken?", vbYesNo + vbQuestion, "Achtung") = vbYes Then

            Drucken()

        End If

    End Sub

    Private Sub Drucken()

        DataGridViewPrinter.StartPrint(DataGridView1, False, False, "Poststelle", "Non-BOM Liste")

        Try

            packageRepository.MarkPrinted(CreateCurrentPackageFilter())

            UpdGrid()

            SenderCB.Text = "Sender"
            EmpfaengerCB.Text = "Empfänger"
            SendungsNummerTB.Text = "SendungsNummer"
        Catch ex As Exception

            MsgBox("Fehler:" & ex.Message)

        End Try

    End Sub

    Private Sub EmpfaengerdatenSammeln()

        Empf = EmpfaengerCB.Text
        DTM = DateTimePicker.Text
        Send = SenderCB.Text
        SendNr = SendungsNummerTB.Text

        Try

            Dim recipient = recipientRepository.FindByName(Empf)

            If recipient Is Nothing Then

                MsgBox("Empfänger nicht gefunden! ;-)")
                Exit Sub

            End If

            Abl = recipient.Abladestelle
            Mand = recipient.Mandant
            Packeterfassen()
        Catch ex As Exception

            MsgBox("Fehler:" & ex.Message)

        End Try

    End Sub

    Private Sub Packeterfassen()

        If SenderCB.Text = "Sender" Or EmpfaengerCB.Text = "Empfänger" Or SendungsNummerTB.Text = "SendungsNummer" Or SenderCB.Text = "" Or EmpfaengerCB.Text = "" Or SendungsNummerTB.Text = "" Then

            MsgBox("Bitte alle Felder ausfüllen! ;-)")

        Else

            Try

                packageRepository.Insert(New PackageRecord With {
                    .Mandant = Mand,
                    .Datum = DTM,
                    .Abladestelle = Abl,
                    .Sender = Send,
                    .SendungsNummer = SendNr,
                    .Empfaenger = Empf,
                    .Unterschrift = "                                                           ",
                    .Gedruckt = "0"
                })

                SenderCB.Items.Clear()
                SenderCBFuellen()
                SenderCB.Focus()
                SenderCB.Text = "Sender"
                SenderCB.SelectAll()
                EmpfaengerCB.Text = "Empfänger"
                SendungsNummerTB.Text = "SendungsNummer"
                SeiteWaehlen()
            Catch ex As Exception

                MsgBox("Fehler:" & ex.Message)

            End Try

        End If

    End Sub

    Private Sub SeiteWaehlen()

        DTM = DateTimePicker.Text

        SeiteCB.Items.Clear()
        PopulateComboBox(SeiteCB, packageRepository.GetDistinctMandants(DTM), True)
        SeiteCB.Items.Add("Alle")
        SeiteCB.SelectedItem = Mand

    End Sub

    Private Sub SeitenFuellen()

        If SenderCB.Text = "Sender" Or SenderCB.Text = "" Then

            Send = "%"

        Else

            Send = "%" + SenderCB.Text + "%"

        End If

        If EmpfaengerCB.Text = "Empfänger" Or EmpfaengerCB.Text = "" Then

            Empf = "%"

        Else

            Empf = "%" + EmpfaengerCB.Text + "%"

        End If

        If SendungsNummerTB.Text = "SendungsNummer" Or SendungsNummerTB.Text = "" Then

            SendNr = "%"

        Else

            SendNr = "%" + SendungsNummerTB.Text + "%"

        End If

        If GedrucktCB.Checked Then

            Gedruckt = "%"

        Else

            Gedruckt = "0"

        End If

        If DatumFilterCB.Checked Then

            DTM = DateTimePicker.Text

        Else

            DTM = "%"

        End If

        If SeiteCB.Text = "Alle" Then

            ste = "%"

        Else

            ste = SeiteCB.Text

        End If

        Try

            myData = packageRepository.Search(CreateCurrentPackageFilter())
            myBindingSource.DataSource = myData

            DataGridView1.DataSource = myBindingSource
            DataGridView1.AutoGenerateColumns = False

            DataGridView1.ColumnHeadersDefaultCellStyle.Font = New Font("Tahoma", 8.25, FontStyle.Bold)

            DataGridView1.Columns("ID").Visible = False
            DataGridView1.Columns("Mandant").Visible = False
            DataGridView1.Columns("Gedruckt").Visible = False

            DataGridView1.Columns(1).DataPropertyName = "Mandant"
            DataGridView1.Columns(2).DataPropertyName = "Datum"
            DataGridView1.Columns(3).DataPropertyName = "Abladestelle"
            DataGridView1.Columns(4).DataPropertyName = "Sender"
            DataGridView1.Columns(5).DataPropertyName = "SendungsNummer"
            DataGridView1.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DataGridView1.Columns(6).DataPropertyName = "Empfaenger"
            DataGridView1.Sort(DataGridView1.Columns(6), System.ComponentModel.ListSortDirection.Ascending)
            DataGridView1.Columns(7).DataPropertyName = "Unterschrift"

        Catch ex As Exception

            MsgBox(Err.Description)

        End Try

    End Sub

    Private Sub GenSeite()

        SeiteCB.Items.Clear()

        If DatumFilterCB.Checked Then

            DTM = DateTimePicker.Text

            PopulateComboBox(SeiteCB, packageRepository.GetDistinctMandants(DTM), True)

        Else

            PopulateComboBox(SeiteCB, packageRepository.GetDistinctMandants(), True)

        End If

        SeiteCB.Items.Add("Alle")

    End Sub

    Private Sub CellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged

        GridSaveChange()

    End Sub

    Public Sub GridSaveChange()

        Try

            myBindingSource.EndEdit()
            packageRepository.SaveChanges(myData)

            EmpfaengerCBFuellen()
            SenderCBFuellen()

        Catch ex As Exception

            MsgBox(ex.ToString())

        End Try

    End Sub

    Private Sub UpdGrid()

        SeitenFuellen()

    End Sub

    Dim EasterEggCounter As Integer

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

        EasterEggCounter = EasterEggCounter + 1

        If EasterEggCounter = 5 Then

            SenderCB.Text = "Poststelle"
            EmpfaengerCB.Text = "Poststelle"
            SendungsNummerTB.Text = "Poststelle"

            EasterEggCounter = 0

        End If

    End Sub

    Private Sub PopulateComboBox(target As ComboBox, values As IEnumerable(Of String), Optional preserveExistingItems As Boolean = False)

        If Not preserveExistingItems Then

            target.Items.Clear()

        End If

        For Each value As String In values

            target.Items.Add(value)

        Next

    End Sub

    Private Function CreateCurrentPackageFilter() As PackageFilter

        Return New PackageFilter With {
            .Datum = DTM,
            .Mandant = ste,
            .Sender = Send,
            .SendungsNummer = SendNr,
            .Empfaenger = Empf,
            .Gedruckt = Gedruckt
        }

    End Function

End Class
