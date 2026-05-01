Imports System.IO
Imports System.Data.SQLite

Public Class Form3

    Dim con As New SQLiteConnection("Data Source=Poststelle.db")
    Dim connectionString As String = "FullUri=file:Poststelle.db?cache=shared"
    Dim myBindingSource As New BindingSource
    Dim myData As New DataTable

    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Dim NME As String
    Dim ABL As String
    Dim MDT As String

    Const WM_NCLBUTTONDOWN As Integer = &HA1S
    Const HTBOTTOMRIGHT As Integer = 17

    Private Sub Form3_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown, MenuStrip1.MouseDown

        drag = True
        mousex = Windows.Forms.Cursor.Position.X - Me.Left
        mousey = Windows.Forms.Cursor.Position.Y - Me.Top

    End Sub

    Private Sub Form3_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove, MenuStrip1.MouseMove

        If drag Then

            Me.Top = Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = Windows.Forms.Cursor.Position.X - mousex

        End If

    End Sub

    Private Sub Form3_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp, MenuStrip1.MouseUp

        drag = False

    End Sub

    Private Sub SS_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)

        Me.Cursor = Cursors.Arrow

    End Sub


    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        NameCBFuellen()
        AbladestelleCBFuellen()
        MandantCBFuellen()
        EmpfaengerFuellen()

    End Sub


    Private Sub X_Click(sender As Object, e As EventArgs) Handles x.Click

        Me.Close()

    End Sub


    Private Sub SchließenButton_Click(sender As Object, e As EventArgs) Handles SchließenButton.Click

        Me.Close()

    End Sub


    Private Sub SpeichernButton_Click(sender As Object, e As EventArgs) Handles SpeichernButton.Click

        Dim Name As String = NameCB.Text
        Dim Abladestelle As String = AbladestelleCB.Text
        Dim Mandant As String = MandantCB.Text

        Dim com As New SQLiteCommand
        Dim adapter As New SQLiteDataAdapter

        If NameCB.Text = "" Or AbladestelleCB.Text = "" Or MandantCB.Text = "" Or NameCB.Text = "Name" Or AbladestelleCB.Text = "Abladestelle" Or MandantCB.Text = "Mandant" Then

            MsgBox("Bitte alle Felder ausfüllen! ;-)")

        ElseIf NameCB.Items.Contains(NameCB.Text) Then

            MsgBox("Den Empfänger gibt es schon! ;-)")

        Else

            con.Open()

            If con.State = ConnectionState.Open Then

                Try

                    com = New SQLiteCommand("INSERT OR REPLACE INTO Empfaenger
                (
                Name,
                Abladestelle,
                Mandant
                )
                    VALUES
                    (
                    @Name,
                    @Abladestelle,
                    @Mandant
                    )", con)
                    com.Parameters.AddWithValue("@Name", Name)
                    com.Parameters.AddWithValue("@Abladestelle", Abladestelle)
                    com.Parameters.AddWithValue("@Mandant", Mandant)

                    com.ExecuteNonQuery()
                    con.Close()

                    MsgBox("Empfänger würde geändert/angelegt! ;-)")

                    NameCB.Items.Clear()
                    AbladestelleCB.Items.Clear()
                    MandantCB.Items.Clear()

                    AbladestelleCBFuellen()
                    MandantCBFuellen()
                    NameCBFuellen()
                    UpdGrid()
                    Form1.EmpfaengerCBFuellen()

                    NameCB.Text = "Name"
                    AbladestelleCB.Text = "Abladestelle"
                    MandantCB.Text = "Mandant"

                    SchließenButton.Focus()

                Catch ex As Exception

                    MsgBox("Fehler:" & ex.Message)

                End Try

            End If

        End If

    End Sub


    Private Sub NameCB_keydown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles NameCB.KeyDown

        If e.KeyCode = Keys.Enter Then

            AbladestelleCB.Focus()
            UpdGrid()

        End If

    End Sub

    Private Sub NameCB_TextChanged(sender As Object, e As EventArgs) Handles NameCB.TextChanged

        If NameCB.Text = "Name" Then

        ElseIf NameCB.Text = "" Then

            UpdGrid()

        Else

            UpdGrid()

        End If

    End Sub

    Private Sub NameCB_Leave(sender As Object, e As EventArgs) Handles NameCB.Leave

        If NameCB.Text = "" Then

            NameCB.Text = "Name"

        End If

    End Sub

    Private Sub LoadDistinctValues(target As ComboBox, query As String, columnName As String)

        Dim com As New SQLiteCommand(query, con)
        Dim rd As SQLiteDataReader

        target.Items.Clear()
        con.Open()

        If con.State = ConnectionState.Open Then

            Try

                rd = com.ExecuteReader

                Do While rd.Read()

                    target.Items.Add(rd(columnName))

                Loop

                rd.Close()
                con.Close()
                com.Dispose()

            Catch ex As Exception

                MsgBox("Fehler:" & ex.Message)

            End Try

        End If

    End Sub

    Private Sub NameCBFuellen()

        LoadDistinctValues(NameCB, "SELECT DISTINCT Name FROM Empfaenger", "Name")

    End Sub


    Private Sub AbladestelleCB_keydown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles AbladestelleCB.KeyDown

        If e.KeyCode = Keys.Enter Then

            MandantCB.Focus()
            UpdGrid()

        End If

    End Sub

    Private Sub AbladestelleCB_TextChanged(sender As Object, e As EventArgs) Handles AbladestelleCB.TextChanged

        If AbladestelleCB.Text = "Abladestelle" Then

        ElseIf AbladestelleCB.Text = "" Then

            UpdGrid()

        Else

            UpdGrid()

        End If

    End Sub

    Private Sub AbladestelleCB_Leave(sender As Object, e As EventArgs) Handles AbladestelleCB.Leave

        If AbladestelleCB.Text = "" Then

            AbladestelleCB.Text = "Abladestelle"

        End If

    End Sub

    Private Sub AbladestelleCBFuellen()

        LoadDistinctValues(AbladestelleCB, "SELECT DISTINCT Abladestelle FROM Empfaenger", "Abladestelle")

    End Sub


    Private Sub MandantCB_KeyDown(sender As Object, e As KeyEventArgs) Handles MandantCB.KeyDown

        If e.KeyCode = Keys.Enter Then

            SpeichernButton.PerformClick()

        End If

    End Sub

    Private Sub MandantCB_TextChanged(sender As Object, e As EventArgs) Handles MandantCB.TextChanged

        If MandantCB.Text = "Mandant" Then

        ElseIf MandantCB.Text = "" Then

            UpdGrid()

        Else

            UpdGrid()

        End If

    End Sub

    Private Sub MandantCB_Leave(sender As Object, e As EventArgs) Handles MandantCB.Leave

        If MandantCB.Text = "" Then

            MandantCB.Text = "Mandant"

        End If

    End Sub

    Private Sub MandantCBFuellen()

        LoadDistinctValues(MandantCB, "SELECT DISTINCT Mandant FROM Empfaenger", "Mandant")

    End Sub


    Private Sub EmpfaengerFuellen()

        If NameCB.Text = "Name" Or NameCB.Text = "" Then

            NME = "%"

        Else

            NME = "%" + NameCB.Text + "%"

        End If

        If AbladestelleCB.Text = "Abladestelle" Or AbladestelleCB.Text = "" Then

            ABL = "%"

        Else

            ABL = "%" + AbladestelleCB.Text + "%"

        End If

        If MandantCB.Text = "Mandant" Or MandantCB.Text = "" Then

            MDT = "%"

        Else

            MDT = "%" + MandantCB.Text + "%"

        End If

        Dim selectConnection As New SQLiteConnection(connectionString)
        Dim selectCommand As New SQLiteCommand("SELECT * FROM Empfaenger WHERE Name LIKE @Name AND Abladestelle LIKE @Abladestelle AND Mandant LIKE @Mandant", selectConnection)
        selectCommand.Parameters.AddWithValue("@Name", NME)
        selectCommand.Parameters.AddWithValue("@Abladestelle", ABL)
        selectCommand.Parameters.AddWithValue("@Mandant", MDT)
        Dim myAdapter As New SQLite.SQLiteDataAdapter(selectCommand)

        Try

            myAdapter.Fill(myData)
            myBindingSource.DataSource = myData

            DataGridView1.DataSource = myBindingSource
            DataGridView1.AutoGenerateColumns = False

            DataGridView1.Columns("ID").Visible = False

            DataGridView1.Columns(1).DataPropertyName = "Name"
            DataGridView1.Sort(DataGridView1.Columns(1), System.ComponentModel.ListSortDirection.Ascending)
            DataGridView1.Columns(2).DataPropertyName = "Abladestelle"
            DataGridView1.Columns(3).DataPropertyName = "Mandant"

        Catch ex As Exception

            MsgBox(Err.Description)

        End Try

    End Sub


    Private Sub CellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged

        GridSaveChange()

    End Sub

    Public Sub GridSaveChange()

        Try

            Dim myAdapter As New SQLite.SQLiteDataAdapter("SELECT * FROM  Empfaenger", connectionString)
            Dim cb As New SQLite.SQLiteCommandBuilder(myAdapter)

            myBindingSource.EndEdit()
            myAdapter.Update(myData)

            Form1.EmpfaengerCBFuellen()

        Catch ex As Exception

            MsgBox(ex.ToString())

        End Try

    End Sub

    Private Sub UpdGrid()

        If DataGridView1.Rows.Count = 0 Then

            EmpfaengerFuellen()

        Else

            myData.Clear()
            EmpfaengerFuellen()

        End If

    End Sub


    '----

End Class
