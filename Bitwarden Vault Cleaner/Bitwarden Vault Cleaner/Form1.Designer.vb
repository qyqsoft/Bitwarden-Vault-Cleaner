<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.Tsb_Open = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.Tsb_Clean = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.Tsb_Combine = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.Tsb_Save = New System.Windows.Forms.ToolStripButton()
        Me.Tsl_Info = New System.Windows.Forms.ToolStripLabel()
        Me.Lv_Content = New System.Windows.Forms.ListView()
        Me.Flp_Titel = New System.Windows.Forms.FlowLayoutPanel()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.Tssl_File = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Tsb_Open, Me.ToolStripSeparator1, Me.Tsb_Clean, Me.ToolStripSeparator3, Me.Tsb_Combine, Me.ToolStripSeparator2, Me.Tsb_Save, Me.Tsl_Info})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1000, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'Tsb_Open
        '
        Me.Tsb_Open.Image = CType(resources.GetObject("Tsb_Open.Image"), System.Drawing.Image)
        Me.Tsb_Open.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Tsb_Open.Name = "Tsb_Open"
        Me.Tsb_Open.Size = New System.Drawing.Size(56, 22)
        Me.Tsb_Open.Text = "Open"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'Tsb_Clean
        '
        Me.Tsb_Clean.Image = CType(resources.GetObject("Tsb_Clean.Image"), System.Drawing.Image)
        Me.Tsb_Clean.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Tsb_Clean.Name = "Tsb_Clean"
        Me.Tsb_Clean.Size = New System.Drawing.Size(57, 22)
        Me.Tsb_Clean.Text = "Clean"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'Tsb_Combine
        '
        Me.Tsb_Combine.Image = CType(resources.GetObject("Tsb_Combine.Image"), System.Drawing.Image)
        Me.Tsb_Combine.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Tsb_Combine.Name = "Tsb_Combine"
        Me.Tsb_Combine.Size = New System.Drawing.Size(102, 22)
        Me.Tsb_Combine.Text = "Combine URIs"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'Tsb_Save
        '
        Me.Tsb_Save.Image = CType(resources.GetObject("Tsb_Save.Image"), System.Drawing.Image)
        Me.Tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Tsb_Save.Name = "Tsb_Save"
        Me.Tsb_Save.Size = New System.Drawing.Size(51, 22)
        Me.Tsb_Save.Text = "Save"
        '
        'Tsl_Info
        '
        Me.Tsl_Info.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.Tsl_Info.ForeColor = System.Drawing.Color.Navy
        Me.Tsl_Info.Name = "Tsl_Info"
        Me.Tsl_Info.Size = New System.Drawing.Size(79, 22)
        Me.Tsl_Info.Text = "                        "
        '
        'Lv_Content
        '
        Me.Lv_Content.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Lv_Content.FullRowSelect = True
        Me.Lv_Content.GridLines = True
        Me.Lv_Content.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.Lv_Content.HideSelection = False
        Me.Lv_Content.Location = New System.Drawing.Point(0, 95)
        Me.Lv_Content.MultiSelect = False
        Me.Lv_Content.Name = "Lv_Content"
        Me.Lv_Content.ShowGroups = False
        Me.Lv_Content.Size = New System.Drawing.Size(1000, 330)
        Me.Lv_Content.TabIndex = 2
        Me.Lv_Content.UseCompatibleStateImageBehavior = False
        Me.Lv_Content.View = System.Windows.Forms.View.Details
        '
        'Flp_Titel
        '
        Me.Flp_Titel.AutoScroll = True
        Me.Flp_Titel.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Flp_Titel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Flp_Titel.Dock = System.Windows.Forms.DockStyle.Top
        Me.Flp_Titel.Location = New System.Drawing.Point(0, 25)
        Me.Flp_Titel.Name = "Flp_Titel"
        Me.Flp_Titel.Size = New System.Drawing.Size(1000, 66)
        Me.Flp_Titel.TabIndex = 3
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Tssl_File})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 428)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1000, 22)
        Me.StatusStrip1.TabIndex = 4
        '
        'Tssl_File
        '
        Me.Tssl_File.ForeColor = System.Drawing.Color.Navy
        Me.Tssl_File.Name = "Tssl_File"
        Me.Tssl_File.Size = New System.Drawing.Size(0, 17)
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1000, 450)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.Flp_Titel)
        Me.Controls.Add(Me.Lv_Content)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(800, 400)
        Me.Name = "Form1"
        Me.Text = "Bitwarden Vault Cleaner - tips-und-mehr.de"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents Tsb_Open As ToolStripButton
    Friend WithEvents Tsb_Clean As ToolStripButton
    Friend WithEvents Tsb_Save As ToolStripButton
    Friend WithEvents Lv_Content As ListView
    Friend WithEvents Flp_Titel As FlowLayoutPanel
    Friend WithEvents Tsl_Info As ToolStripLabel
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents Tssl_File As ToolStripStatusLabel
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents Tsb_Combine As ToolStripButton
End Class
