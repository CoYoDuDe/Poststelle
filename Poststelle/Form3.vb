Option Strict On

Public Class Form3

    Private ReadOnly recipientRepository As New RecipientRepository()
    Private ReadOnly recipientFormService As New RecipientFormService()

    Dim myBindingSource As New BindingSource
    Dim myData As New DataTable

    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Private isInitializing As Boolean = True

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

        ApplyApplicationIcon(Me)

        Try

            ReloadUiData()
        Finally

            isInitializing = False

        End Try

    End Sub

    Private Sub X_Click(sender As Object, e As EventArgs) Handles x.Click

        Me.Close()

    End Sub

    Private Sub SchliessenButton_Click(sender As Object, e As EventArgs) Handles SchließenButton.Click

        Me.Close()

    End Sub

    Private Sub SpeichernButton_Click(sender As Object, e As EventArgs) Handles SpeichernButton.Click

        If Not recipientFormService.IsRecipientEntryComplete(NameCB.Text, AbladestelleCB.Text, MandantCB.Text) Then

            MsgBox("Bitte alle Felder ausfuellen! ;-)")

        ElseIf NameCB.Items.Contains(NameCB.Text) Then

            MsgBox("Den Empfaenger gibt es schon! ;-)")

        Else

            Try

                recipientRepository.Upsert(New RecipientRecord With {
                    .Name = NameCB.Text,
                    .Abladestelle = AbladestelleCB.Text,
                    .Mandant = MandantCB.Text
                })

                MsgBox("Empfaenger wurde geaendert/angelegt! ;-)")

                ReloadUiData()
                Form1.EmpfaengerCBFuellen()
                ResetEntryFields()
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

        If Not isInitializing Then

            UpdGrid()

        End If

    End Sub

    Private Sub NameCB_Leave(sender As Object, e As EventArgs) Handles NameCB.Leave

        If String.IsNullOrWhiteSpace(NameCB.Text) Then

            NameCB.Text = UiText.NamePlaceholder

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

        If Not isInitializing Then

            UpdGrid()

        End If

    End Sub

    Private Sub AbladestelleCB_Leave(sender As Object, e As EventArgs) Handles AbladestelleCB.Leave

        If String.IsNullOrWhiteSpace(AbladestelleCB.Text) Then

            AbladestelleCB.Text = UiText.DropOffPlaceholder

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

        If Not isInitializing Then

            UpdGrid()

        End If

    End Sub

    Private Sub MandantCB_Leave(sender As Object, e As EventArgs) Handles MandantCB.Leave

        If String.IsNullOrWhiteSpace(MandantCB.Text) Then

            MandantCB.Text = UiText.ClientPlaceholder

        End If

    End Sub

    Private Sub MandantCBFuellen()

        PopulateComboBox(MandantCB, recipientRepository.GetDistinctMandants())

    End Sub

    Private Sub EmpfaengerFuellen()

        Try

            myData = recipientRepository.Search(recipientFormService.BuildFilter(NameCB.Text,
                                                                                 AbladestelleCB.Text,
                                                                                 MandantCB.Text))
            myBindingSource.DataSource = myData

            DataGridView1.AutoGenerateColumns = True
            DataGridView1.DataSource = myBindingSource

            If DataGridView1.Columns.Count > 0 Then

                If DataGridView1.Columns.Contains("ID") Then

                    DataGridView1.Columns("ID").Visible = False

                End If

                If DataGridView1.Columns.Count > 1 Then

                    DataGridView1.Sort(DataGridView1.Columns(1), System.ComponentModel.ListSortDirection.Ascending)

                End If

            End If

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

    End Sub

    Private Sub CellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged

        If Not isInitializing Then

            GridSaveChange()

        End If

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

    Private Sub ReloadUiData()

        NameCBFuellen()
        AbladestelleCBFuellen()
        MandantCBFuellen()
        ResetEntryFields()
        EmpfaengerFuellen()

    End Sub

    Private Sub ResetEntryFields()

        NameCB.Text = UiText.NamePlaceholder
        AbladestelleCB.Text = UiText.DropOffPlaceholder
        MandantCB.Text = UiText.ClientPlaceholder

    End Sub

    Private Sub PopulateComboBox(target As ComboBox, values As IEnumerable(Of String))

        target.Items.Clear()

        For Each value As String In values

            target.Items.Add(value)

        Next

    End Sub

End Class
