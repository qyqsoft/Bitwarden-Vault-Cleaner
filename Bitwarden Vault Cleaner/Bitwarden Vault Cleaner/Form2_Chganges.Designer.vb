<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2_Chganges
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form2_Chganges))
        Me.Listview_NewContent = New System.Windows.Forms.ListView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button_Accept = New System.Windows.Forms.Button()
        Me.Button_Cancel = New System.Windows.Forms.Button()
        Me.Label_Changes = New System.Windows.Forms.Label()
        Me.Flp_Titel = New System.Windows.Forms.FlowLayoutPanel()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Listview_NewContent
        '
        Me.Listview_NewContent.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Listview_NewContent.FullRowSelect = True
        Me.Listview_NewContent.GridLines = True
        Me.Listview_NewContent.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.Listview_NewContent.HideSelection = False
        Me.Listview_NewContent.Location = New System.Drawing.Point(2, 72)
        Me.Listview_NewContent.MultiSelect = False
        Me.Listview_NewContent.Name = "Listview_NewContent"
        Me.Listview_NewContent.ShowGroups = False
        Me.Listview_NewContent.Size = New System.Drawing.Size(782, 256)
        Me.Listview_NewContent.TabIndex = 3
        Me.Listview_NewContent.UseCompatibleStateImageBehavior = False
        Me.Listview_NewContent.View = System.Windows.Forms.View.Details
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label_Changes)
        Me.Panel1.Controls.Add(Me.Button_Cancel)
        Me.Panel1.Controls.Add(Me.Button_Accept)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 328)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(784, 33)
        Me.Panel1.TabIndex = 4
        '
        'Button_Accept
        '
        Me.Button_Accept.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Accept.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button_Accept.ForeColor = System.Drawing.Color.DarkGreen
        Me.Button_Accept.Location = New System.Drawing.Point(574, 6)
        Me.Button_Accept.Name = "Button_Accept"
        Me.Button_Accept.Size = New System.Drawing.Size(117, 23)
        Me.Button_Accept.TabIndex = 0
        Me.Button_Accept.Text = "Accept Changes"
        Me.Button_Accept.UseVisualStyleBackColor = True
        '
        'Button_Cancel
        '
        Me.Button_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button_Cancel.ForeColor = System.Drawing.Color.DarkRed
        Me.Button_Cancel.Location = New System.Drawing.Point(697, 6)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Button_Cancel.TabIndex = 1
        Me.Button_Cancel.Text = "Cancel"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'Label_Changes
        '
        Me.Label_Changes.AutoSize = True
        Me.Label_Changes.ForeColor = System.Drawing.Color.Navy
        Me.Label_Changes.Location = New System.Drawing.Point(13, 11)
        Me.Label_Changes.Name = "Label_Changes"
        Me.Label_Changes.Size = New System.Drawing.Size(73, 13)
        Me.Label_Changes.TabIndex = 2
        Me.Label_Changes.Text = "                      "
        '
        'Flp_Titel
        '
        Me.Flp_Titel.AutoScroll = True
        Me.Flp_Titel.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Flp_Titel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Flp_Titel.Dock = System.Windows.Forms.DockStyle.Top
        Me.Flp_Titel.Location = New System.Drawing.Point(0, 0)
        Me.Flp_Titel.Name = "Flp_Titel"
        Me.Flp_Titel.Size = New System.Drawing.Size(784, 66)
        Me.Flp_Titel.TabIndex = 5
        '
        'Form2_Chganges
        '
        Me.AcceptButton = Me.Button_Accept
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Button_Cancel
        Me.ClientSize = New System.Drawing.Size(784, 361)
        Me.Controls.Add(Me.Flp_Titel)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Listview_NewContent)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(800, 400)
        Me.Name = "Form2_Chganges"
        Me.ShowInTaskbar = False
        Me.Text = "Vault Changes"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Listview_NewContent As ListView
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Button_Cancel As Button
    Friend WithEvents Button_Accept As Button
    Friend WithEvents Label_Changes As Label
    Friend WithEvents Flp_Titel As FlowLayoutPanel
End Class
