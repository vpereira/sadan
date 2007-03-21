namespace sadan_server
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.genOneTimePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setSeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.VictimsListView = new System.Windows.Forms.ListView();
            this.HandleColumn = new System.Windows.Forms.ColumnHeader();
            this.NameColumn = new System.Windows.Forms.ColumnHeader();
            this.StatusColumn = new System.Windows.Forms.ColumnHeader();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(554, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem1});
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.exitToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.genOneTimePasswordToolStripMenuItem,
            this.setSeedToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // genOneTimePasswordToolStripMenuItem
            // 
            this.genOneTimePasswordToolStripMenuItem.Name = "genOneTimePasswordToolStripMenuItem";
            this.genOneTimePasswordToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.genOneTimePasswordToolStripMenuItem.Text = "Gen One Time Password";
            this.genOneTimePasswordToolStripMenuItem.Click += new System.EventHandler(this.genOneTimePasswordToolStripMenuItem_Click);
            // 
            // setSeedToolStripMenuItem
            // 
            this.setSeedToolStripMenuItem.Name = "setSeedToolStripMenuItem";
            this.setSeedToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.setSeedToolStripMenuItem.Text = "Set Seed...";
            this.setSeedToolStripMenuItem.Click += new System.EventHandler(this.setSeedToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Number to Dial";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 51);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(228, 20);
            this.textBox1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Pick Victim";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(280, 47);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 27);
            this.button1.TabIndex = 4;
            this.button1.Text = "GO!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // VictimsListView
            // 
            this.VictimsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.HandleColumn,
            this.NameColumn,
            this.StatusColumn});
            this.VictimsListView.FullRowSelect = true;
            this.VictimsListView.HideSelection = false;
            this.VictimsListView.Location = new System.Drawing.Point(12, 106);
            this.VictimsListView.MultiSelect = false;
            this.VictimsListView.Name = "VictimsListView";
            this.VictimsListView.Size = new System.Drawing.Size(228, 190);
            this.VictimsListView.TabIndex = 5;
            this.VictimsListView.UseCompatibleStateImageBehavior = false;
            this.VictimsListView.View = System.Windows.Forms.View.Details;
            this.VictimsListView.Click += new System.EventHandler(this.VictimsListView_Click);
            // 
            // HandleColumn
            // 
            this.HandleColumn.Text = "Handle";
            this.HandleColumn.Width = 59;
            // 
            // NameColumn
            // 
            this.NameColumn.Text = "Name";
            this.NameColumn.Width = 106;
            // 
            // StatusColumn
            // 
            this.StatusColumn.Text = "Status";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "set_seed",
            "set_JobID",
            "gen_keys",
            "ping",
            "post_command"});
            this.comboBox1.Location = new System.Drawing.Point(330, 109);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(267, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Command:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 308);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.VictimsListView);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        internal System.Windows.Forms.ListView VictimsListView;
        internal System.Windows.Forms.ColumnHeader HandleColumn;
        internal System.Windows.Forms.ColumnHeader NameColumn;
        internal System.Windows.Forms.ColumnHeader StatusColumn;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem genOneTimePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setSeedToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
    }
}

