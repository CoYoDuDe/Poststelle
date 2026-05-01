<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form3
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form3))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.x = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.MandantCB = New System.Windows.Forms.ComboBox()
        Me.AbladestelleCB = New System.Windows.Forms.ComboBox()
        Me.NameCB = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.SpeichernButton = New System.Windows.Forms.Button()
        Me.SchließenButton = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.Transparent
        Me.MenuStrip1.BackgroundImage = Global.Poststelle.My.Resources.Resources.top
        Me.MenuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.MenuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.MenuStrip1.Size = New System.Drawing.Size(444, 24)
        Me.MenuStrip1.TabIndex = 4
        '
        'x
        '
        Me.x.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.x.BackColor = System.Drawing.Color.Transparent
        Me.x.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.x.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.x.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.25!, System.Drawing.FontStyle.Bold)
        Me.x.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.x.Location = New System.Drawing.Point(423, 3)
        Me.x.Name = "x"
        Me.x.Size = New System.Drawing.Size(16, 15)
        Me.x.TabIndex = 5
        Me.x.TabStop = False
        Me.x.Tag = "X"
        Me.x.Text = "X"
        Me.x.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.AutoSize = True
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BackgroundImage = Global.Poststelle.My.Resources.Resources.wallpapermain
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.MandantCB)
        Me.Panel1.Controls.Add(Me.AbladestelleCB)
        Me.Panel1.Controls.Add(Me.NameCB)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(12, 28)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(420, 49)
        Me.Panel1.TabIndex = 56
        '
        'MandantCB
        '
        Me.MandantCB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.MandantCB.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.MandantCB.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MandantCB.FormattingEnabled = True
        Me.MandantCB.Location = New System.Drawing.Point(278, 19)
        Me.MandantCB.Name = "MandantCB"
        Me.MandantCB.Size = New System.Drawing.Size(121, 24)
        Me.MandantCB.TabIndex = 2
        Me.MandantCB.Text = "Mandant"
        '
        'AbladestelleCB
        '
        Me.AbladestelleCB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.AbladestelleCB.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.AbladestelleCB.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AbladestelleCB.FormattingEnabled = True
        Me.AbladestelleCB.Location = New System.Drawing.Point(151, 19)
        Me.AbladestelleCB.Name = "AbladestelleCB"
        Me.AbladestelleCB.Size = New System.Drawing.Size(121, 24)
        Me.AbladestelleCB.TabIndex = 1
        Me.AbladestelleCB.Text = "Abladestelle"
        '
        'NameCB
        '
        Me.NameCB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.NameCB.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.NameCB.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NameCB.FormattingEnabled = True
        Me.NameCB.Location = New System.Drawing.Point(24, 19)
        Me.NameCB.Name = "NameCB"
        Me.NameCB.Size = New System.Drawing.Size(121, 24)
        Me.NameCB.TabIndex = 0
        Me.NameCB.Text = "Name"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(1, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(146, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Eintragen, Suchen, Sortieren:"
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.AutoSize = True
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImage = Global.Poststelle.My.Resources.Resources.wallpapermain
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.DataGridView1)
        Me.Panel2.Controls.Add(Me.SpeichernButton)
        Me.Panel2.Controls.Add(Me.SchließenButton)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Location = New System.Drawing.Point(12, 82)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(420, 216)
        Me.Panel2.TabIndex = 57
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(6, 16)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(407, 164)
        Me.DataGridView1.TabIndex = 5
        '
        'SpeichernButton
        '
        Me.SpeichernButton.BackgroundImage = Global.Poststelle.My.Resources.Resources.buttons
        Me.SpeichernButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SpeichernButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SpeichernButton.Location = New System.Drawing.Point(214, 186)
        Me.SpeichernButton.Name = "SpeichernButton"
        Me.SpeichernButton.Size = New System.Drawing.Size(194, 23)
        Me.SpeichernButton.TabIndex = 3
        Me.SpeichernButton.Text = "Speichern"
        Me.SpeichernButton.UseVisualStyleBackColor = True
        '
        'SchließenButton
        '
        Me.SchließenButton.BackgroundImage = Global.Poststelle.My.Resources.Resources.buttons
        Me.SchließenButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SchließenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SchließenButton.Location = New System.Drawing.Point(8, 186)
        Me.SchließenButton.Name = "SchließenButton"
        Me.SchließenButton.Size = New System.Drawing.Size(200, 23)
        Me.SchließenButton.TabIndex = 4
        Me.SchließenButton.Text = "Schließen"
        Me.SchließenButton.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(2, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(119, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Editieren oder Löschen:"
        '
        'Form3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.BackgroundImage = Global.Poststelle.My.Resources.Resources.wallpapermain
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(444, 309)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.x)
        Me.Controls.Add(Me.MenuStrip1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form3"
        Me.Opacity = 0.9R
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents x As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents SpeichernButton As Button
    Friend WithEvents SchließenButton As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents MandantCB As ComboBox
    Friend WithEvents AbladestelleCB As ComboBox
    Friend WithEvents NameCB As ComboBox
    Friend WithEvents DataGridView1 As DataGridView
End Class
