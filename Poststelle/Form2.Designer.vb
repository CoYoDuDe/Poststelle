<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form2))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.x = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.SpeichernButton = New System.Windows.Forms.Button()
        Me.SchließenButton = New System.Windows.Forms.Button()
        Me.EinschaltenCB = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MinutenTB = New System.Windows.Forms.TextBox()
        Me.FilePatchTB = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
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
        Me.MenuStrip1.Size = New System.Drawing.Size(410, 24)
        Me.MenuStrip1.TabIndex = 4
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.FileName = "PoststelleBackup.db"
        '
        'x
        '
        Me.x.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.x.BackColor = System.Drawing.Color.Transparent
        Me.x.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.x.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.x.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.25!, System.Drawing.FontStyle.Bold)
        Me.x.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.x.Location = New System.Drawing.Point(389, 3)
        Me.x.Name = "x"
        Me.x.Size = New System.Drawing.Size(16, 15)
        Me.x.TabIndex = 53
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
        Me.Panel1.Controls.Add(Me.SpeichernButton)
        Me.Panel1.Controls.Add(Me.SchließenButton)
        Me.Panel1.Controls.Add(Me.EinschaltenCB)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.MinutenTB)
        Me.Panel1.Controls.Add(Me.FilePatchTB)
        Me.Panel1.Location = New System.Drawing.Point(12, 28)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(386, 57)
        Me.Panel1.TabIndex = 56
        '
        'SpeichernButton
        '
        Me.SpeichernButton.BackgroundImage = Global.Poststelle.My.Resources.Resources.buttons
        Me.SpeichernButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SpeichernButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SpeichernButton.Location = New System.Drawing.Point(235, 29)
        Me.SpeichernButton.Name = "SpeichernButton"
        Me.SpeichernButton.Size = New System.Drawing.Size(139, 23)
        Me.SpeichernButton.TabIndex = 24
        Me.SpeichernButton.Text = "Speichern"
        Me.SpeichernButton.UseVisualStyleBackColor = True
        '
        'SchließenButton
        '
        Me.SchließenButton.BackgroundImage = Global.Poststelle.My.Resources.Resources.buttons
        Me.SchließenButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SchließenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SchließenButton.Location = New System.Drawing.Point(89, 29)
        Me.SchließenButton.Name = "SchließenButton"
        Me.SchließenButton.Size = New System.Drawing.Size(139, 23)
        Me.SchließenButton.TabIndex = 23
        Me.SchließenButton.Text = "Schließen"
        Me.SchließenButton.UseVisualStyleBackColor = True
        '
        'EinschaltenCB
        '
        Me.EinschaltenCB.AutoSize = True
        Me.EinschaltenCB.Location = New System.Drawing.Point(3, 29)
        Me.EinschaltenCB.Name = "EinschaltenCB"
        Me.EinschaltenCB.Size = New System.Drawing.Size(81, 17)
        Me.EinschaltenCB.TabIndex = 22
        Me.EinschaltenCB.Text = "Einschalten"
        Me.EinschaltenCB.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(351, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(24, 13)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Min"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(272, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(24, 13)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Alle"
        '
        'MinutenTB
        '
        Me.MinutenTB.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MinutenTB.Location = New System.Drawing.Point(296, 3)
        Me.MinutenTB.Name = "MinutenTB"
        Me.MinutenTB.Size = New System.Drawing.Size(50, 22)
        Me.MinutenTB.TabIndex = 2
        Me.MinutenTB.Text = "Minuten"
        '
        'FilePatchTB
        '
        Me.FilePatchTB.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FilePatchTB.Location = New System.Drawing.Point(3, 3)
        Me.FilePatchTB.Name = "FilePatchTB"
        Me.FilePatchTB.Size = New System.Drawing.Size(267, 22)
        Me.FilePatchTB.TabIndex = 1
        Me.FilePatchTB.Text = "FilePatch"
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.BackgroundImage = Global.Poststelle.My.Resources.Resources.wallpapermain
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(410, 93)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.x)
        Me.Controls.Add(Me.MenuStrip1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form2"
        Me.Opacity = 0.9R
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents x As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents SpeichernButton As Button
    Friend WithEvents SchließenButton As Button
    Friend WithEvents EinschaltenCB As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents MinutenTB As TextBox
    Friend WithEvents FilePatchTB As TextBox
End Class
