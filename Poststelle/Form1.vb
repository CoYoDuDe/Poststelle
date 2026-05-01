Imports System.Data.SQLite
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Printing

Public Class Form1

    Dim con As New SQLiteConnection("Data Source=Poststelle.db")
    Dim connectionString As String = "FullUri=file:Poststelle.db?cache=shared"
    Dim myBindingSource As New BindingSource
    Dim myData As New DataTable
    Dim myAdapter As New SQLiteDataAdapter

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
        mousex = Windows.Forms.Cursor.Position.X - Me.Left
        mousey = Windows.Forms.Cursor.Position.Y - Me.Top

    End Sub

    Private Sub Form1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove, MenuStrip1.MouseMove

        If drag Then

            Me.Top = Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = Windows.Forms.Cursor.Position.X - mousex

        End If

    End Sub

    Private Sub Form1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp, MenuStrip1.MouseUp

        drag = False

    End Sub

    Private Sub SS_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles SS.MouseDown

        If Me.WindowState <> FormWindowState.Maximized Then

            If e.Button = Windows.Forms.MouseButtons.Left Then

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

    'Private Sub SS_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles SS.MouseMove

    'If Me.WindowState <> FormWindowState.Maximized Then

    'If e.X = SS.Width - 1 Or e.X = SS.Width - 2 Or e.X = SS.Width - 3 Or e.X = SS.Width - 4 Or e.X = SS.Width - 5 Then

    'Dim theCursor As Cursor = Cursors.Arrow

    'Select Case e.X

    'Case SS.Width - 5 To SS.Width - 1

    'Select Case e.Y

    'Case SS.Height - 5 To SS.Height - 1
    'theCursor = Cursors.PanNW

    'End Select

    'End Select

    'Me.Cursor = theCursor

    'End If

    'End If

    'End Sub

    'Private Sub SS_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles SS.MouseUp

    'Me.Cursor = Cursors.Arrow

    'End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.SS.SizingGrip = False

        If System.IO.File.Exists("Poststelle.db") Then

            EmpfaengerCBFuellen()
            SenderCBFuellen()
            SeitenFuellen()
            GenSeite()
            AutodbBackupEX()

        Else

            Dbersstellen()

        End If

    End Sub

    Private Sub Dbersstellen()

        Dim connect As New SQLite.SQLiteConnection() With {.ConnectionString = "Data Source=Poststelle.db;"}

        connect.Open()
        connect.Close()

        MsgBox("erster Start Datenbank wird erstellt!... ;-)")

        DBStrukturerstellen()

    End Sub

    Private Sub DBStrukturerstellen()

        Dim con As New SQLite.SQLiteConnection()
        Dim com As SQLiteCommand
        Dim com0 As SQLiteCommand
        Dim com1 As SQLiteCommand

        con.ConnectionString = "Data Source=Poststelle.db;"
        con.Open()
        con.Close()
        con.Open()
        com = con.CreateCommand
        com.CommandText = "CREATE TABLE Packete (
	                       ID	INTEGER Not NULL,
	                       Mandant	TEXT,
	                       Datum	TEXT,
	                       Abladestelle	TEXT,
	                       Sender	TEXT,
	                       SendungsNummer	TEXT,
	                       Empfaenger	TEXT,
                           Unterschrift	TEXT,
                           Gedruckt	TEXT,
	                       PRIMARY KEY(ID));"

        com0 = con.CreateCommand
        com0.CommandText = "CREATE TABLE Empfaenger (
	                        ID	INTEGER Not NULL,
	                        Name	TEXT,
	                        Abladestelle	TEXT,
	                        Mandant	TEXT,
	                        PRIMARY KEY(ID));"

        com1 = con.CreateCommand
        com1.CommandText = "CREATE TABLE Einstellungen (
	                        ID	INTEGER Not NULL,
	                        AutodbBackup	TEXT,
	                        AutodbBackupTime	TEXT,
	                        AutodbBackupPfad	TEXT,
	                        PRIMARY KEY(ID));"

        com.ExecuteNonQuery()
        com.Dispose()
        com0.ExecuteNonQuery()
        com0.Dispose()
        com1.ExecuteNonQuery()
        com1.Dispose()
        con.Close()

        MsgBox("DB Tabellen und Struktur erstellt! ;-)")

        StandardEinstellungen()

    End Sub

    Private Sub StandardEinstellungen()

        Dim com As New SQLiteCommand
        Dim adapter As New SQLiteDataAdapter

        con.Open()

        If con.State = ConnectionState.Open Then

            Try

                Dim Patch As String = "PoststelleBackup.db"

                com = New SQLiteCommand("INSERT INTO Einstellungen
                (
                AutodbBackup,
                AutodbBackupTime,
                AutodbBackupPfad               
                )
                    VALUES
                    (
                    0,
                    30,                    
                    '" & Patch & "'                    
                    )", con)

                com.ExecuteNonQuery()
                con.Close()

                MsgBox("Standard Einstellungen Wurden Eingerichtet! ;-)")

                EmpfaengerCBFuellen()
                SenderCBFuellen()
            Catch ex As Exception

                MsgBox("Fehler:" & ex.Message)

            End Try

        End If

    End Sub

    Public Sub AutodbBackupEX()

        Dim com As New SQLiteCommand
        Dim adapter As New SQLiteDataAdapter
        Dim rd As SQLiteDataReader

        con.Open()

        If con.State = ConnectionState.Open Then

            Try

                com = New SQLiteCommand("SELECT * FROM Einstellungen WHERE ID = @Id", con)
                com.Parameters.AddWithValue("@Id", 1)

                rd = com.ExecuteReader

                Do While rd.Read()

                    If (rd("AutodbBackup")) = "1" Then

                        Timer1.Enabled = True
                        Dim tim As Integer = (rd("AutodbBackupTime"))
                        Timer1.Interval = tim * 60000

                    End If

                Loop

                rd.Close()
                con.Close()
                com.Dispose()

            Catch ex As Exception

                MsgBox("Fehler:" & ex.Message)

            End Try

        End If

    End Sub

    Private Sub Timer1_Tick_1(sender As Object, e As EventArgs) Handles Timer1.Tick

        Dim com As New SQLiteCommand
        Dim adapter As New SQLiteDataAdapter
        Dim rd As SQLiteDataReader

        con.Open()

        If con.State = ConnectionState.Open Then

            Try

                com = New SQLiteCommand("SELECT * FROM Einstellungen WHERE ID = @Id", con)
                com.Parameters.AddWithValue("@Id", 1)

                rd = com.ExecuteReader

                rd.Read()
                Dim Pat As String = (rd("AutodbBackupPfad"))

                My.Computer.FileSystem.CopyFile("Poststelle.db", Pat, overwrite:=True)

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

        SenderCB.Items.Clear()

        Dim com As New SQLiteCommand
        Dim adapter As New SQLiteDataAdapter
        Dim rd As SQLiteDataReader

        con.Open()

        If con.State = ConnectionState.Open Then

            Try

                com = New SQLiteCommand("SELECT DISTINCT Sender FROM Packete", con)

                rd = com.ExecuteReader

                Do While rd.Read()

                    SenderCB.Items.Add(rd("Sender"))

                Loop

                rd.Close()
                con.Close()
                com.Dispose()

            Catch ex As Exception

                MsgBox("Fehler:" & ex.Message)

            End Try

        End If

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

        EmpfaengerCB.Items.Clear()

        Dim com As New SQLiteCommand
        Dim adapter As New SQLiteDataAdapter
        Dim rd As SQLiteDataReader

        con.Open()

        If con.State = ConnectionState.Open Then

            Try

                com = New SQLiteCommand("SELECT DISTINCT Name FROM Empfaenger", con)

                rd = com.ExecuteReader

                Do While rd.Read()

                    EmpfaengerCB.Items.Add(rd("Name"))

                Loop

                rd.Close()
                con.Close()
                com.Dispose()

            Catch ex As Exception

                MsgBox("Fehler:" & ex.Message)

            End Try

        End If

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

        If System.IO.File.Exists("Poststelle.db") Then

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

        Dim com As New SQLiteCommand
        Dim adapter As New SQLiteDataAdapter

        con.Open()

        If con.State = ConnectionState.Open Then

            Try

                com = New SQLiteCommand(
                                         "UPDATE Packete SET 
                                         Gedruckt = @GedrucktNeu
                                          WHERE Datum LIKE @Datum AND Mandant LIKE @Mandant AND Sender LIKE @Sender AND SendungsNummer Like @SendungsNummer AND Empfaenger LIKE @Empfaenger AND Gedruckt LIKE @Gedruckt", con)
                com.Parameters.AddWithValue("@GedrucktNeu", "1")
                com.Parameters.AddWithValue("@Datum", DTM)
                com.Parameters.AddWithValue("@Mandant", ste)
                com.Parameters.AddWithValue("@Sender", Send)
                com.Parameters.AddWithValue("@SendungsNummer", SendNr)
                com.Parameters.AddWithValue("@Empfaenger", Empf)
                com.Parameters.AddWithValue("@Gedruckt", Gedruckt)

                com.ExecuteNonQuery()
                con.Close()

                UpdGrid()

                SenderCB.Text = "Sender"
                EmpfaengerCB.Text = "Empfänger"
                SendungsNummerTB.Text = "SendungsNummer"

            Catch ex As Exception

                MsgBox("Fehler:" & ex.Message)

            End Try

        End If

    End Sub

    Private Sub EmpfaengerdatenSammeln()

        Dim com As New SQLiteCommand
        Dim adapter As New SQLiteDataAdapter
        Dim rd As SQLiteDataReader
        Empf = EmpfaengerCB.Text
        DTM = DateTimePicker.Text
        Send = SenderCB.Text
        SendNr = SendungsNummerTB.Text

        con.Open()

        If con.State = ConnectionState.Open Then

            Try

                com = New SQLiteCommand("SELECT Abladestelle, Mandant FROM Empfaenger WHERE Name = @Name", con)
                com.Parameters.AddWithValue("@Name", Empf)

                rd = com.ExecuteReader

                Do While rd.Read()

                    Abl = (rd("Abladestelle"))
                    Mand = (rd("Mandant"))

                Loop

                rd.Close()
                con.Close()
                com.Dispose()
                Packeterfassen()

            Catch ex As Exception

                MsgBox("Fehler:" & ex.Message)

            End Try

        End If

    End Sub

    Private Sub Packeterfassen()

        Dim com As New SQLiteCommand
        Dim adapter As New SQLiteDataAdapter

        If SenderCB.Text = "Sender" Or EmpfaengerCB.Text = "Empfänger" Or SendungsNummerTB.Text = "SendungsNummer" Or SenderCB.Text = "" Or EmpfaengerCB.Text = "" Or SendungsNummerTB.Text = "" Then

            MsgBox("Bitte alle Felder ausfüllen! ;-)")

        Else

            con.Open()

            If con.State = ConnectionState.Open Then

                Try

                    com = New SQLiteCommand("INSERT INTO Packete
                (
                Mandant,
                Datum,
                Abladestelle,
                Sender,
                SendungsNummer,
                Empfaenger,
                Unterschrift,
                Gedruckt
                )
                    VALUES
                    (
                    @Mandant,
                    @Datum,
                    @Abladestelle,
                    @Sender,
                    @SendungsNummer,
                    @Empfaenger,
                    @Unterschrift,
                    @Gedruckt

                    )", con)
                    com.Parameters.AddWithValue("@Mandant", Mand)
                    com.Parameters.AddWithValue("@Datum", DTM)
                    com.Parameters.AddWithValue("@Abladestelle", Abl)
                    com.Parameters.AddWithValue("@Sender", Send)
                    com.Parameters.AddWithValue("@SendungsNummer", SendNr)
                    com.Parameters.AddWithValue("@Empfaenger", Empf)
                    com.Parameters.AddWithValue("@Unterschrift", "                                                           ")
                    com.Parameters.AddWithValue("@Gedruckt", "0")

                    com.ExecuteNonQuery()
                    con.Close()

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

        End If

    End Sub

    Private Sub SeiteWaehlen()

        DTM = DateTimePicker.Text

        Dim com As New SQLiteCommand
        Dim adapter As New SQLiteDataAdapter
        Dim rd As SQLiteDataReader

        SeiteCB.Items.Clear()

        con.Open()

        If con.State = ConnectionState.Open Then

            Try

                com = New SQLiteCommand("SELECT DISTINCT Mandant FROM Packete WHERE Datum = @Datum", con)
                com.Parameters.AddWithValue("@Datum", DTM)

                rd = com.ExecuteReader

                Do While rd.Read()

                    SeiteCB.Items.Add(rd("Mandant"))

                Loop

                SeiteCB.Items.Add("Alle")
                SeiteCB.SelectedItem = Mand

                rd.Close()
                con.Close()
                com.Dispose()

            Catch ex As Exception

                MsgBox("Fehler:" & ex.Message)

            End Try

        End If

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

        Dim selectConnection As New SQLiteConnection(connectionString)
        Dim selectCommand As New SQLiteCommand("SELECT * FROM Packete WHERE Datum LIKE @Datum AND Mandant LIKE @Mandant AND Sender LIKE @Sender AND SendungsNummer Like @SendungsNummer AND Empfaenger LIKE @Empfaenger AND Gedruckt LIKE @Gedruckt", selectConnection)
        selectCommand.Parameters.AddWithValue("@Datum", DTM)
        selectCommand.Parameters.AddWithValue("@Mandant", ste)
        selectCommand.Parameters.AddWithValue("@Sender", Send)
        selectCommand.Parameters.AddWithValue("@SendungsNummer", SendNr)
        selectCommand.Parameters.AddWithValue("@Empfaenger", Empf)
        selectCommand.Parameters.AddWithValue("@Gedruckt", Gedruckt)
        Dim myAdapter As New SQLite.SQLiteDataAdapter(selectCommand)

        Try

            myAdapter.Fill(myData)
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

        Dim com As New SQLiteCommand
        Dim adapter As New SQLiteDataAdapter
        Dim rd As SQLiteDataReader

        SeiteCB.Items.Clear()

        con.Open()

        If DatumFilterCB.Checked Then

            DTM = DateTimePicker.Text

            If con.State = ConnectionState.Open Then

                Try

                    com = New SQLiteCommand("SELECT DISTINCT Mandant FROM Packete WHERE Datum = @Datum", con)
                    com.Parameters.AddWithValue("@Datum", DTM)

                    rd = com.ExecuteReader

                    Do While rd.Read()

                        SeiteCB.Items.Add(rd("Mandant"))

                    Loop

                    SeiteCB.Items.Add("Alle")

                    rd.Close()
                    con.Close()
                    com.Dispose()

                Catch ex As Exception

                    MsgBox("Fehler:" & ex.Message)

                End Try

            End If

        Else

            If con.State = ConnectionState.Open Then

                Try

                    com = New SQLiteCommand("SELECT DISTINCT Mandant FROM Packete", con)

                    rd = com.ExecuteReader

                    Do While rd.Read()

                        SeiteCB.Items.Add(rd("Mandant"))

                    Loop

                    SeiteCB.Items.Add("Alle")

                    rd.Close()
                    con.Close()
                    com.Dispose()

                Catch ex As Exception

                    MsgBox("Fehler:" & ex.Message)

                End Try

            End If

        End If

    End Sub

    Private Sub CellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged

        GridSaveChange()

    End Sub

    Public Sub GridSaveChange()

        Try

            Dim myAdapter As New SQLite.SQLiteDataAdapter("SELECT * FROM  Packete", connectionString)
            Dim cb As New SQLite.SQLiteCommandBuilder(myAdapter)

            myBindingSource.EndEdit()
            myAdapter.Update(myData)

            EmpfaengerCBFuellen()
            SenderCBFuellen()

        Catch ex As Exception

            MsgBox(ex.ToString())

        End Try

    End Sub

    Private Sub UpdGrid()

        If DataGridView1.Rows.Count = 0 Then

            SeitenFuellen()

        Else

            myData.Clear()
            SeitenFuellen()

        End If

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


    '----

End Class
