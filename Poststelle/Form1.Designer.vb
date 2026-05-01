<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.BearbeitenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EmpfängerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EinstellungenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AutodbBackupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SS = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.min = New System.Windows.Forms.Button()
        Me.x = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.SendungsNummerTB = New System.Windows.Forms.TextBox()
        Me.DruckenButton = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.GedrucktCB = New System.Windows.Forms.CheckBox()
        Me.SeiteCB = New System.Windows.Forms.ComboBox()
        Me.EmpfaengerCB = New System.Windows.Forms.ComboBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DatumFilterCB = New System.Windows.Forms.CheckBox()
        Me.DateTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.SenderCB = New System.Windows.Forms.ComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.MenuStrip1.SuspendLayout()
        Me.SS.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.Transparent
        Me.MenuStrip1.BackgroundImage = Global.Poststelle.My.Resources.Resources.top
        Me.MenuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.MenuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BearbeitenToolStripMenuItem, Me.EinstellungenToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.MenuStrip1.Size = New System.Drawing.Size(758, 24)
        Me.MenuStrip1.TabIndex = 4
        '
        'BearbeitenToolStripMenuItem
        '
        Me.BearbeitenToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EmpfängerToolStripMenuItem})
        Me.BearbeitenToolStripMenuItem.Name = "BearbeitenToolStripMenuItem"
        Me.BearbeitenToolStripMenuItem.Size = New System.Drawing.Size(75, 20)
        Me.BearbeitenToolStripMenuItem.Text = "Bearbeiten"
        '
        'EmpfängerToolStripMenuItem
        '
        Me.EmpfängerToolStripMenuItem.Name = "EmpfängerToolStripMenuItem"
        Me.EmpfängerToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.EmpfängerToolStripMenuItem.Text = "Empfänger"
        '
        'EinstellungenToolStripMenuItem
        '
        Me.EinstellungenToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AutodbBackupToolStripMenuItem})
        Me.EinstellungenToolStripMenuItem.Name = "EinstellungenToolStripMenuItem"
        Me.EinstellungenToolStripMenuItem.Size = New System.Drawing.Size(90, 20)
        Me.EinstellungenToolStripMenuItem.Text = "Einstellungen"
        '
        'AutodbBackupToolStripMenuItem
        '
        Me.AutodbBackupToolStripMenuItem.Name = "AutodbBackupToolStripMenuItem"
        Me.AutodbBackupToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.AutodbBackupToolStripMenuItem.Text = "AutodbBackup"
        '
        'SS
        '
        Me.SS.BackColor = System.Drawing.Color.Transparent
        Me.SS.BackgroundImage = Global.Poststelle.My.Resources.Resources.top
        Me.SS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SS.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1})
        Me.SS.Location = New System.Drawing.Point(0, 432)
        Me.SS.Name = "SS"
        Me.SS.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.SS.Size = New System.Drawing.Size(758, 22)
        Me.SS.TabIndex = 66
        Me.SS.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStripStatusLabel1.Image = Global.Poststelle.My.Resources.Resources.gript
        Me.ToolStripStatusLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolStripStatusLabel1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(23, 17)
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackColor = System.Drawing.Color.Transparent
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.25!, System.Drawing.FontStyle.Bold)
        Me.Button2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Button2.Location = New System.Drawing.Point(718, 2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(16, 15)
        Me.Button2.TabIndex = 9
        Me.Button2.TabStop = False
        Me.Button2.Tag = "█"
        Me.Button2.Text = "█"
        Me.Button2.UseVisualStyleBackColor = False
        Me.Button2.Visible = False
        '
        'min
        '
        Me.min.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.min.BackColor = System.Drawing.Color.Transparent
        Me.min.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.min.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.min.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.25!, System.Drawing.FontStyle.Bold)
        Me.min.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.min.Location = New System.Drawing.Point(718, 2)
        Me.min.Name = "min"
        Me.min.Size = New System.Drawing.Size(16, 15)
        Me.min.TabIndex = 10
        Me.min.TabStop = False
        Me.min.Tag = "▬"
        Me.min.Text = "▬"
        Me.min.UseVisualStyleBackColor = False
        '
        'x
        '
        Me.x.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.x.BackColor = System.Drawing.Color.Transparent
        Me.x.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.x.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.x.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.25!, System.Drawing.FontStyle.Bold)
        Me.x.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.x.Location = New System.Drawing.Point(737, 2)
        Me.x.Name = "x"
        Me.x.Size = New System.Drawing.Size(16, 15)
        Me.x.TabIndex = 8
        Me.x.TabStop = False
        Me.x.Tag = "X"
        Me.x.Text = "X"
        Me.x.UseVisualStyleBackColor = False
        '
        'Timer1
        '
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.PictureBox1)
        Me.Panel3.Location = New System.Drawing.Point(537, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(195, 91)
        Me.Panel3.TabIndex = 58
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.BackgroundImage = Global.Poststelle.My.Resources.Resources.logo
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox1.Location = New System.Drawing.Point(28, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(141, 91)
        Me.PictureBox1.TabIndex = 13
        Me.PictureBox1.TabStop = False
        '
        'Panel4
        '
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.SendungsNummerTB)
        Me.Panel4.Controls.Add(Me.DruckenButton)
        Me.Panel4.Location = New System.Drawing.Point(286, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(251, 91)
        Me.Panel4.TabIndex = 59
        '
        'SendungsNummerTB
        '
        Me.SendungsNummerTB.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SendungsNummerTB.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SendungsNummerTB.Location = New System.Drawing.Point(2, 13)
        Me.SendungsNummerTB.Name = "SendungsNummerTB"
        Me.SendungsNummerTB.Size = New System.Drawing.Size(246, 22)
        Me.SendungsNummerTB.TabIndex = 2
        Me.SendungsNummerTB.Text = "SendungsNummer"
        '
        'DruckenButton
        '
        Me.DruckenButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.DruckenButton.BackgroundImage = Global.Poststelle.My.Resources.Resources.buttons
        Me.DruckenButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.DruckenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DruckenButton.Location = New System.Drawing.Point(3, 41)
        Me.DruckenButton.Name = "DruckenButton"
        Me.DruckenButton.Size = New System.Drawing.Size(245, 48)
        Me.DruckenButton.TabIndex = 4
        Me.DruckenButton.TabStop = False
        Me.DruckenButton.Text = "Drucken"
        Me.DruckenButton.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.GedrucktCB)
        Me.Panel5.Controls.Add(Me.SeiteCB)
        Me.Panel5.Controls.Add(Me.EmpfaengerCB)
        Me.Panel5.Location = New System.Drawing.Point(143, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(143, 91)
        Me.Panel5.TabIndex = 60
        '
        'GedrucktCB
        '
        Me.GedrucktCB.AutoSize = True
        Me.GedrucktCB.Location = New System.Drawing.Point(37, 75)
        Me.GedrucktCB.Name = "GedrucktCB"
        Me.GedrucktCB.Size = New System.Drawing.Size(70, 17)
        Me.GedrucktCB.TabIndex = 67
        Me.GedrucktCB.TabStop = False
        Me.GedrucktCB.Text = "Gedruckt"
        Me.GedrucktCB.UseVisualStyleBackColor = True
        '
        'SeiteCB
        '
        Me.SeiteCB.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SeiteCB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.SeiteCB.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.SeiteCB.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SeiteCB.FormattingEnabled = True
        Me.SeiteCB.Location = New System.Drawing.Point(2, 50)
        Me.SeiteCB.Name = "SeiteCB"
        Me.SeiteCB.Size = New System.Drawing.Size(138, 24)
        Me.SeiteCB.Sorted = True
        Me.SeiteCB.TabIndex = 5
        Me.SeiteCB.TabStop = False
        Me.SeiteCB.Text = "Seite"
        '
        'EmpfaengerCB
        '
        Me.EmpfaengerCB.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EmpfaengerCB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.EmpfaengerCB.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.EmpfaengerCB.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EmpfaengerCB.FormattingEnabled = True
        Me.EmpfaengerCB.Location = New System.Drawing.Point(2, 13)
        Me.EmpfaengerCB.Name = "EmpfaengerCB"
        Me.EmpfaengerCB.Size = New System.Drawing.Size(138, 24)
        Me.EmpfaengerCB.Sorted = True
        Me.EmpfaengerCB.TabIndex = 1
        Me.EmpfaengerCB.Text = "Empfänger"
        '
        'Panel6
        '
        Me.Panel6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel6.Controls.Add(Me.Label3)
        Me.Panel6.Controls.Add(Me.Label1)
        Me.Panel6.Controls.Add(Me.DatumFilterCB)
        Me.Panel6.Controls.Add(Me.DateTimePicker)
        Me.Panel6.Controls.Add(Me.SenderCB)
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(143, 91)
        Me.Panel6.TabIndex = 61
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(0, 37)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(147, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Listen Sortieren (Pflichtfelder):"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(-1, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(142, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Erfassen, Suchen, Sortieren:"
        '
        'DatumFilterCB
        '
        Me.DatumFilterCB.AutoSize = True
        Me.DatumFilterCB.Checked = True
        Me.DatumFilterCB.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DatumFilterCB.Location = New System.Drawing.Point(28, 75)
        Me.DatumFilterCB.Name = "DatumFilterCB"
        Me.DatumFilterCB.Size = New System.Drawing.Size(82, 17)
        Me.DatumFilterCB.TabIndex = 5
        Me.DatumFilterCB.TabStop = False
        Me.DatumFilterCB.Text = "Datum Filter"
        Me.DatumFilterCB.UseVisualStyleBackColor = True
        '
        'DateTimePicker
        '
        Me.DateTimePicker.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker.Location = New System.Drawing.Point(2, 51)
        Me.DateTimePicker.Name = "DateTimePicker"
        Me.DateTimePicker.Size = New System.Drawing.Size(138, 22)
        Me.DateTimePicker.TabIndex = 6
        Me.DateTimePicker.TabStop = False
        '
        'SenderCB
        '
        Me.SenderCB.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SenderCB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.SenderCB.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.SenderCB.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SenderCB.FormattingEnabled = True
        Me.SenderCB.Location = New System.Drawing.Point(2, 13)
        Me.SenderCB.Name = "SenderCB"
        Me.SenderCB.Size = New System.Drawing.Size(138, 24)
        Me.SenderCB.Sorted = True
        Me.SenderCB.TabIndex = 0
        Me.SenderCB.Text = "Sender"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.AutoSize = True
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BackgroundImage = Global.Poststelle.My.Resources.Resources.wallpapermain
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Panel6)
        Me.Panel1.Controls.Add(Me.Panel5)
        Me.Panel1.Location = New System.Drawing.Point(12, 28)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(735, 97)
        Me.Panel1.TabIndex = 56
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(119, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Editieren oder Löschen:"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowDrop = True
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.Location = New System.Drawing.Point(0, 18)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(732, 286)
        Me.DataGridView1.TabIndex = 7
        Me.DataGridView1.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImage = Global.Poststelle.My.Resources.Resources.wallpapermain
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.DataGridView1)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Location = New System.Drawing.Point(12, 125)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(735, 307)
        Me.Panel2.TabIndex = 57
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.BackgroundImage = Global.Poststelle.My.Resources.Resources.wallpapermain
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(758, 454)
        Me.Controls.Add(Me.SS)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.min)
        Me.Controls.Add(Me.x)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.Opacity = 0.9R
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.SS.ResumeLayout(False)
        Me.SS.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents BearbeitenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EinstellungenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SS As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents Button2 As Button
    Friend WithEvents min As Button
    Friend WithEvents x As Button
    Friend WithEvents EmpfängerToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AutodbBackupToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Timer1 As Timer
    Friend WithEvents PrintDialog1 As PrintDialog
    Friend WithEvents PrintDocument1 As Printing.PrintDocument
    Friend WithEvents PrintPreviewDialog1 As PrintPreviewDialog
    Friend WithEvents Panel3 As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents SendungsNummerTB As TextBox
    Friend WithEvents DruckenButton As Button
    Friend WithEvents Panel5 As Panel
    Friend WithEvents SeiteCB As ComboBox
    Friend WithEvents EmpfaengerCB As ComboBox
    Friend WithEvents Panel6 As Panel
    Friend WithEvents DateTimePicker As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents SenderCB As ComboBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Panel2 As Panel
    Friend WithEvents GedrucktCB As CheckBox
    Friend WithEvents DatumFilterCB As CheckBox
End Class
