Imports System.Drawing
Imports System.Drawing.Printing
Imports System.IO

Public Class Form1

    Private ReadOnly settingsRepository As New SettingsRepository()
    Private ReadOnly recipientRepository As New RecipientRepository()
    Private ReadOnly packageRepository As New PackageRepository()
    Private ReadOnly packageFormService As New PackageFormService()
    Private ReadOnly shippingLabelParser As New ShippingLabelParser()

    Dim myBindingSource As New BindingSource
    Dim myData As New DataTable

    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Private isInitializing As Boolean = True

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
        ToolStripStatusLabel1.Text = "Bereit"

        Try

            If settingsRepository.HasRequiredSchema() Then

                ReloadUiData()
                AutodbBackupEX()

            Else

                Dbersstellen()

            End If
        Finally

            isInitializing = False

        End Try

    End Sub

    Private Sub Dbersstellen()

        settingsRepository.CreateDatabase()

        DBStrukturerstellen()

    End Sub

    Private Sub DBStrukturerstellen()

        settingsRepository.CreateSchema()

        StandardEinstellungen()

    End Sub

    Private Sub StandardEinstellungen()

        Try

            settingsRepository.InsertDefaultSettings()
            ReloadUiData()
            AutodbBackupEX()
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

        If CanRefreshGrid() Then

            UpdGrid()

        End If

    End Sub

    Private Sub SenderCB_Leave(sender As Object, e As EventArgs) Handles SenderCB.Leave

        If String.IsNullOrWhiteSpace(SenderCB.Text) Then

            SenderCB.Text = UiText.SenderPlaceholder

        End If

    End Sub

    Private Sub SenderCBFuellen()

        If Not settingsRepository.HasRequiredSchema() Then

            Exit Sub

        End If

        PopulateComboBox(SenderCB, packageRepository.GetDistinctSenders())

    End Sub

    Private Sub EmpfaengerCB_keydown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles EmpfaengerCB.KeyDown

        If e.KeyCode = Keys.Enter Then

            If MaybeOpenRecipientMaintenance() Then

                Exit Sub

            End If

            SendungsNummerTB.Focus()
            SendungsNummerTB.SelectAll()

        End If

    End Sub

    Private Sub EmpfaengerCB_TextChanged(sender As Object, e As EventArgs) Handles EmpfaengerCB.TextChanged

        If CanRefreshGrid() Then

            UpdGrid()

        End If

    End Sub

    Private Sub EmpfaengerCB_Leave(sender As Object, e As EventArgs) Handles EmpfaengerCB.Leave

        If String.IsNullOrWhiteSpace(EmpfaengerCB.Text) Then

            EmpfaengerCB.Text = UiText.RecipientPlaceholder

        End If

    End Sub

    Public Sub EmpfaengerCBFuellen()

        If Not settingsRepository.HasRequiredSchema() Then

            Exit Sub

        End If

        PopulateComboBox(EmpfaengerCB, recipientRepository.GetDistinctNames())

    End Sub

    Private Sub SendungsNummerTB_keydown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles SendungsNummerTB.KeyDown

        If e.KeyCode = Keys.Enter Then

            ApplyLabelScanParsing()

            If MaybeOpenRecipientMaintenance() Then

                Exit Sub

            End If

            EmpfaengerdatenSammeln()

        End If

    End Sub

    Private Sub SendungsNummerTB_TextChanged(sender As Object, e As EventArgs) Handles SendungsNummerTB.TextChanged

        If CanRefreshGrid() Then

            UpdGrid()

        ElseIf packageFormService.IsEasterEggTriggered(SenderCB.Text, EmpfaengerCB.Text, SendungsNummerTB.Text) Then

            Form4.Show()

        End If

    End Sub

    Private Sub SendungsNummerTB_Leave(sender As Object, e As EventArgs) Handles SendungsNummerTB.Leave

        If String.IsNullOrWhiteSpace(SendungsNummerTB.Text) Then

            SendungsNummerTB.Text = UiText.TrackingNumberPlaceholder

        Else

            ApplyLabelScanParsing()

        End If

    End Sub

    Private Sub DateTimePicker_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker.ValueChanged

        SeiteCB.Text = UiText.PagePlaceholder
        GenSeite()
        UpdGrid()

    End Sub

    Private Sub SeiteCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SeiteCB.SelectedIndexChanged

        If CanRefreshGrid() Then

            UpdGrid()

        End If

    End Sub

    Private Sub GedrucktCB_CheckedChanged(sender As Object, e As EventArgs) Handles GedrucktCB.CheckedChanged

        If CanRefreshGrid() Then

            UpdGrid()

        End If

    End Sub

    Private Sub DatumFilterCB_CheckedChanged(sender As Object, e As EventArgs) Handles DatumFilterCB.CheckedChanged

        If File.Exists(DatabaseConfig.DatabaseFilePath) Then

            GenSeite()

        End If

        If CanRefreshGrid() Then

            UpdGrid()

        End If

    End Sub

    Private Sub DruckenButton_Click(sender As Object, e As EventArgs) Handles DruckenButton.Click

        If DatumFilterCB.Checked And Not String.Equals(SeiteCB.Text, UiText.AllPagesLabel, StringComparison.Ordinal) Then

            Drucken()

        ElseIf MsgBox("Achtung: Datumfilter ist aus oder alle Seiten sind ausgewaehlt. Wirklich drucken?", vbYesNo + vbQuestion, "Achtung") = vbYes Then

            Drucken()

        End If

    End Sub

    Private Sub Drucken()

        DataGridViewPrinter.StartPrint(DataGridView1, False, False, "Poststelle", "Non-BOM Liste")

        Try

            packageRepository.MarkPrinted(CreateCurrentPackageFilter())
            UpdGrid()
            ResetEntryFields()
        Catch ex As Exception

            MsgBox("Fehler:" & ex.Message)

        End Try

    End Sub

    Private Sub EmpfaengerdatenSammeln()

        If Not packageFormService.IsPackageEntryComplete(SenderCB.Text, EmpfaengerCB.Text, SendungsNummerTB.Text) Then

            MsgBox("Bitte alle Felder ausfuellen! ;-)")
            Exit Sub

        End If

        Try

            Dim recipient = recipientRepository.FindByName(EmpfaengerCB.Text)

            If recipient Is Nothing Then

                MsgBox("Empfaenger nicht gefunden! ;-)")
                Exit Sub

            End If

            Packeterfassen(recipient)
        Catch ex As Exception

            MsgBox("Fehler:" & ex.Message)

        End Try

    End Sub

    Private Sub Packeterfassen(recipient As RecipientRecord)

        Try

            packageRepository.Insert(packageFormService.CreatePackageRecord(recipient,
                                                                          DateTimePicker.Text,
                                                                          SenderCB.Text,
                                                                          SendungsNummerTB.Text))

            SenderCBFuellen()
            SenderCB.Focus()
            ResetEntryFields()
            SenderCB.SelectAll()
            SeiteWaehlen(recipient.Mandant)
        Catch ex As Exception

            MsgBox("Fehler:" & ex.Message)

        End Try

    End Sub

    Private Sub SeiteWaehlen(mandant As String)

        RefreshMandantPages(DateTimePicker.Text)
        SeiteCB.SelectedItem = mandant

    End Sub

    Private Sub SeitenFuellen()

        If Not settingsRepository.HasRequiredSchema() Then

            Exit Sub

        End If

        Try

            Dim filter = CreateCurrentPackageFilter()
            ste = filter.Mandant
            myData = packageRepository.Search(filter)
            myBindingSource.DataSource = myData

            DataGridView1.DataSource = myBindingSource
            DataGridView1.AutoGenerateColumns = False

            DataGridView1.ColumnHeadersDefaultCellStyle.Font = New Font("Tahoma", 8.25, FontStyle.Bold)

            DataGridView1.Columns("ID").Visible = False
            DataGridView1.Columns("Mandant").Visible = False
            DataGridView1.Columns("Gedruckt").Visible = False

            DataGridView1.Columns(1).DataPropertyName = "Mandant"
            DataGridView1.Columns(1).HeaderText = "Mandant"
            DataGridView1.Columns(2).DataPropertyName = "Datum"
            DataGridView1.Columns(2).HeaderText = "Datum"
            DataGridView1.Columns(3).DataPropertyName = "Abladestelle"
            DataGridView1.Columns(3).HeaderText = "Abladestelle"
            DataGridView1.Columns(4).DataPropertyName = "Sender"
            DataGridView1.Columns(4).HeaderText = "Sender"
            DataGridView1.Columns(5).DataPropertyName = "SendungsNummer"
            DataGridView1.Columns(5).HeaderText = "SendungsNummer"
            DataGridView1.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DataGridView1.Columns(6).DataPropertyName = "Empfaenger"
            DataGridView1.Columns(6).HeaderText = "Empfaenger"
            DataGridView1.Sort(DataGridView1.Columns(6), System.ComponentModel.ListSortDirection.Ascending)
            DataGridView1.Columns(7).DataPropertyName = "Unterschrift"
            DataGridView1.Columns(7).HeaderText = "Unterschrift"

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

    End Sub

    Private Sub GenSeite()

        If Not settingsRepository.HasRequiredSchema() Then

            Exit Sub

        End If

        If DatumFilterCB.Checked Then

            RefreshMandantPages(DateTimePicker.Text)

        Else

            RefreshMandantPages()

        End If

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

            SenderCB.Text = UiText.EasterEggTrigger
            EmpfaengerCB.Text = UiText.EasterEggTrigger
            SendungsNummerTB.Text = UiText.EasterEggTrigger

            EasterEggCounter = 0

        End If

    End Sub

    Private Sub ReloadUiData()

        EmpfaengerCBFuellen()
        SenderCBFuellen()
        ResetEntryFields()
        GenSeite()
        UpdGrid()

    End Sub

    Private Sub ResetEntryFields()

        SenderCB.Text = UiText.SenderPlaceholder
        EmpfaengerCB.Text = UiText.RecipientPlaceholder
        SendungsNummerTB.Text = UiText.TrackingNumberPlaceholder
        ToolStripStatusLabel1.Text = "Bereit"

    End Sub

    Private Function ShouldRefreshGrid() As Boolean

        Return Not String.Equals(SeiteCB.Text, UiText.PagePlaceholder, StringComparison.Ordinal)

    End Function

    Private Function CanRefreshGrid() As Boolean

        Return Not isInitializing AndAlso
               settingsRepository.HasRequiredSchema() AndAlso
               ShouldRefreshGrid()

    End Function

    Private Function MaybeOpenRecipientMaintenance() As Boolean

        If Not packageFormService.NeedsRecipientPrompt(EmpfaengerCB.Text, GetComboBoxValues(EmpfaengerCB)) Then

            Return False

        End If

        If MsgBox("Empfaenger nicht gefunden. Neu anlegen?", vbYesNo) = vbYes Then

            Form3.Show()
            Form3.AbladestelleCB.Focus()
            Form3.NameCB.Text = EmpfaengerCB.Text
            Return True

        End If

        Return False

    End Function

    Private Sub RefreshMandantPages(Optional selectedDate As String = Nothing)

        SeiteCB.Items.Clear()

        If String.IsNullOrWhiteSpace(selectedDate) Then

            PopulateComboBox(SeiteCB, packageRepository.GetDistinctMandants(), True)

        Else

            PopulateComboBox(SeiteCB, packageRepository.GetDistinctMandants(selectedDate), True)

        End If

        SeiteCB.Items.Add(UiText.AllPagesLabel)

    End Sub

    Private Sub PopulateComboBox(target As ComboBox, values As IEnumerable(Of String), Optional preserveExistingItems As Boolean = False)

        If Not preserveExistingItems Then

            target.Items.Clear()

        End If

        For Each value As String In values

            target.Items.Add(value)

        Next

    End Sub

    Private Function GetComboBoxValues(target As ComboBox) As IEnumerable(Of String)

        Dim values As New List(Of String)()

        For Each item As Object In target.Items

            values.Add(Convert.ToString(item))

        Next

        Return values

    End Function

    Private Sub ApplyLabelScanParsing()

        Dim parseResult As ShippingLabelParseResult =
            shippingLabelParser.Parse(SendungsNummerTB.Text, GetComboBoxValues(EmpfaengerCB))

        If String.IsNullOrWhiteSpace(parseResult.CleanedTrackingNumber) Then

            Exit Sub

        End If

        If Not String.Equals(SendungsNummerTB.Text, parseResult.CleanedTrackingNumber, StringComparison.Ordinal) Then

            SendungsNummerTB.Text = parseResult.CleanedTrackingNumber
            SendungsNummerTB.SelectionStart = SendungsNummerTB.TextLength

        End If

        If Not String.IsNullOrWhiteSpace(parseResult.DetectedCarrier) AndAlso
           (String.Equals(SenderCB.Text, UiText.SenderPlaceholder, StringComparison.Ordinal) OrElse String.IsNullOrWhiteSpace(SenderCB.Text)) Then

            SenderCB.Text = parseResult.DetectedCarrier

        End If

        If Not String.IsNullOrWhiteSpace(parseResult.MatchedRecipient) AndAlso
           (String.Equals(EmpfaengerCB.Text, UiText.RecipientPlaceholder, StringComparison.Ordinal) OrElse String.IsNullOrWhiteSpace(EmpfaengerCB.Text)) Then

            EmpfaengerCB.Text = parseResult.MatchedRecipient

        End If

        ToolStripStatusLabel1.Text = BuildScanStatus(parseResult)

    End Sub

    Private Function BuildScanStatus(parseResult As ShippingLabelParseResult) As String

        If String.IsNullOrWhiteSpace(parseResult.CleanedTrackingNumber) Then

            Return "Bereit"

        End If

        Dim parts As New List(Of String)()

        If Not String.IsNullOrWhiteSpace(parseResult.DetectedCarrier) Then

            parts.Add("Carrier: " & parseResult.DetectedCarrier)

        End If

        If Not String.IsNullOrWhiteSpace(parseResult.MatchedRecipient) Then

            parts.Add("Empfaenger: " & parseResult.MatchedRecipient)

        End If

        If parseResult.WasNormalized Then

            parts.Add("Scan bereinigt")

        End If

        If parts.Count = 0 Then

            Return "Scan erkannt"

        End If

        Return String.Join(" | ", parts)

    End Function

    Private Function CreateCurrentPackageFilter() As PackageFilter

        Return packageFormService.BuildFilter(DatumFilterCB.Checked,
                                              DateTimePicker.Text,
                                              SeiteCB.Text,
                                              SenderCB.Text,
                                              EmpfaengerCB.Text,
                                              SendungsNummerTB.Text,
                                              GedrucktCB.Checked)

    End Function

End Class
