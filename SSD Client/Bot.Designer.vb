<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Bot
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Bot))
        Me.SSDClientNotifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.NotifyIconContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.NotifyIconContextMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'SSDClientNotifyIcon
        '
        Me.SSDClientNotifyIcon.ContextMenuStrip = Me.NotifyIconContextMenu
        Me.SSDClientNotifyIcon.Icon = CType(resources.GetObject("SSDClientNotifyIcon.Icon"), System.Drawing.Icon)
        Me.SSDClientNotifyIcon.Text = "SSD Client"
        Me.SSDClientNotifyIcon.Visible = True
        '
        'NotifyIconContextMenu
        '
        Me.NotifyIconContextMenu.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.NotifyIconContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.NotifyIconContextMenu.Name = "NotifyIconContextMenu"
        Me.NotifyIconContextMenu.Size = New System.Drawing.Size(153, 70)
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'Bot
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.ControlBox = False
        Me.Enabled = False
        Me.Name = "Bot"
        Me.ShowInTaskbar = False
        Me.Text = "SSD Client"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.NotifyIconContextMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SSDClientNotifyIcon As System.Windows.Forms.NotifyIcon
    Friend WithEvents NotifyIconContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
