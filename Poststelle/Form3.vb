Public Class Form3

    Private ReadOnly recipientRepository As New RecipientRepository()

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
        mousex = System.Windows.Forms.Cursor.Position.X - Me.Left
        mousey = System.Windows.Forms.Cursor.Position.Y - Me.Top

    End Sub

    Private Sub Form3_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove, MenuStrip1.MouseMove

        If drag Then

            Me.Top = System.Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = System.Windows.Forms.Cursor.Position.X - mousex

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

        If NameCB.Text = "" Or AbladestelleCB.Text = "" Or MandantCB.Text = "" Or NameCB.Text = "Name" Or AbladestelleCB.Text = "Abladestelle" Or MandantCB.Text = "Mandant" Then

            MsgBox("Bitte alle Felder ausfüllen! ;-)")

        ElseIf NameCB.Items.Contains(NameCB.Text) Then

            MsgBox("Den Empfänger gibt es schon! ;-)")

        Else

            Try

                recipientRepository.Upsert(New RecipientRecord With {
                    .Name = Name,
                    .Abladestelle = Abladestelle,
                    .Mandant = Mandant
                })

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

    Private Sub NameCBFuellen()

        PopulateComboBox(NameCB, recipientRepository.GetDistinctNames())

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

        PopulateComboBox(AbladestelleCB, recipientRepository.GetDistinctAbladestellen())

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

        PopulateComboBox(MandantCB, recipientRepository.GetDistinctMandants())

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

        Try

            myData = recipientRepository.Search(New RecipientFilter With {
                .Name = NME,
                .Abladestelle = ABL,
                .Mandant = MDT
            })
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

            myBindingSource.EndEdit()
            recipientRepository.SaveChanges(myData)

            Form1.EmpfaengerCBFuellen()

        Catch ex As Exception

            MsgBox(ex.ToString())

        End Try

    End Sub

    Private Sub UpdGrid()

        EmpfaengerFuellen()

    End Sub

    Private Sub PopulateComboBox(target As ComboBox, values As IEnumerable(Of String))

        target.Items.Clear()

        For Each value As String In values

            target.Items.Add(value)

        Next

    End Sub

End Class
