namespace FreeVD
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.SystemTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.SystemTrayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuSwitchDesktop = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGatherWindows = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPinnedApps = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuUnpin = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lstHotkeys = new System.Windows.Forms.ListView();
            this.colTask = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHotKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAddHotkey = new System.Windows.Forms.Button();
            this.btnDeleteHotkey = new System.Windows.Forms.Button();
            this.tabPinnedApps = new System.Windows.Forms.TabPage();
            this.lstPinnedApps = new System.Windows.Forms.ListBox();
            this.tabOptions = new System.Windows.Forms.TabPage();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SystemTrayMenu.SuspendLayout();
            this.mnuPinnedApps.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPinnedApps.SuspendLayout();
            this.tabOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // SystemTray
            // 
            this.SystemTray.ContextMenuStrip = this.SystemTrayMenu;
            this.SystemTray.Icon = ((System.Drawing.Icon)(resources.GetObject("SystemTray.Icon")));
            this.SystemTray.Text = "FreeVD";
            this.SystemTray.Visible = true;
            this.SystemTray.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SystemTray_MouseClick);
            this.SystemTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SystemTray_MouseDoubleClick);
            // 
            // SystemTrayMenu
            // 
            this.SystemTrayMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.SystemTrayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSwitchDesktop,
            this.mnuGatherWindows,
            this.toolStripSeparator2,
            this.mnuSettings,
            this.mnuExit});
            this.SystemTrayMenu.Name = "SystemTrayMenu";
            this.SystemTrayMenu.ShowImageMargin = false;
            this.SystemTrayMenu.Size = new System.Drawing.Size(175, 98);
            this.SystemTrayMenu.Opening += new System.ComponentModel.CancelEventHandler(this.SystemTrayMenu_Opening);
            // 
            // mnuSwitchDesktop
            // 
            this.mnuSwitchDesktop.Name = "mnuSwitchDesktop";
            this.mnuSwitchDesktop.Size = new System.Drawing.Size(174, 22);
            this.mnuSwitchDesktop.Text = "Switch Desktop";
            // 
            // mnuGatherWindows
            // 
            this.mnuGatherWindows.Name = "mnuGatherWindows";
            this.mnuGatherWindows.Size = new System.Drawing.Size(174, 22);
            this.mnuGatherWindows.Text = "Bring All Windows Here";
            this.mnuGatherWindows.Click += new System.EventHandler(this.mnuGatherWindows_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(171, 6);
            // 
            // mnuSettings
            // 
            this.mnuSettings.Name = "mnuSettings";
            this.mnuSettings.Size = new System.Drawing.Size(174, 22);
            this.mnuSettings.Text = "Settings";
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(174, 22);
            this.mnuExit.Text = "Exit";
            // 
            // mnuPinnedApps
            // 
            this.mnuPinnedApps.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mnuPinnedApps.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuUnpin});
            this.mnuPinnedApps.Name = "mnuPinnedApps";
            this.mnuPinnedApps.Size = new System.Drawing.Size(107, 26);
            this.mnuPinnedApps.Opening += new System.ComponentModel.CancelEventHandler(this.mnuPinnedApps_Opening);
            // 
            // mnuUnpin
            // 
            this.mnuUnpin.Name = "mnuUnpin";
            this.mnuUnpin.Size = new System.Drawing.Size(106, 22);
            this.mnuUnpin.Text = "Unpin";
            this.mnuUnpin.Click += new System.EventHandler(this.mnuUnpin_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(5, 5);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabs);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblVersion);
            this.splitContainer1.Panel2.Controls.Add(this.btnOK);
            this.splitContainer1.Panel2.Controls.Add(this.btnApply);
            this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
            this.splitContainer1.Size = new System.Drawing.Size(534, 451);
            this.splitContainer1.SplitterDistance = 414;
            this.splitContainer1.TabIndex = 20;
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabPage1);
            this.tabs.Controls.Add(this.tabPinnedApps);
            this.tabs.Controls.Add(this.tabOptions);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Margin = new System.Windows.Forms.Padding(2);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(534, 414);
            this.tabs.TabIndex = 18;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lstHotkeys);
            this.tabPage1.Controls.Add(this.btnAddHotkey);
            this.tabPage1.Controls.Add(this.btnDeleteHotkey);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(526, 388);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Hotkeys";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lstHotkeys
            // 
            this.lstHotkeys.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTask,
            this.colHotKey});
            this.lstHotkeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstHotkeys.Location = new System.Drawing.Point(2, 2);
            this.lstHotkeys.Margin = new System.Windows.Forms.Padding(2);
            this.lstHotkeys.Name = "lstHotkeys";
            this.lstHotkeys.Size = new System.Drawing.Size(522, 384);
            this.lstHotkeys.TabIndex = 11;
            this.lstHotkeys.UseCompatibleStateImageBehavior = false;
            this.lstHotkeys.View = System.Windows.Forms.View.Details;
            // 
            // colTask
            // 
            this.colTask.Text = "Task";
            this.colTask.Width = 251;
            // 
            // colHotKey
            // 
            this.colHotKey.Text = "Hotkey";
            this.colHotKey.Width = 247;
            // 
            // btnAddHotkey
            // 
            this.btnAddHotkey.Location = new System.Drawing.Point(9, 291);
            this.btnAddHotkey.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddHotkey.Name = "btnAddHotkey";
            this.btnAddHotkey.Size = new System.Drawing.Size(50, 21);
            this.btnAddHotkey.TabIndex = 13;
            this.btnAddHotkey.Text = "Add";
            this.btnAddHotkey.UseVisualStyleBackColor = true;
            // 
            // btnDeleteHotkey
            // 
            this.btnDeleteHotkey.Location = new System.Drawing.Point(63, 291);
            this.btnDeleteHotkey.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteHotkey.Name = "btnDeleteHotkey";
            this.btnDeleteHotkey.Size = new System.Drawing.Size(50, 21);
            this.btnDeleteHotkey.TabIndex = 12;
            this.btnDeleteHotkey.Text = "Delete";
            this.btnDeleteHotkey.UseVisualStyleBackColor = true;
            // 
            // tabPinnedApps
            // 
            this.tabPinnedApps.Controls.Add(this.lstPinnedApps);
            this.tabPinnedApps.Location = new System.Drawing.Point(4, 22);
            this.tabPinnedApps.Margin = new System.Windows.Forms.Padding(2);
            this.tabPinnedApps.Name = "tabPinnedApps";
            this.tabPinnedApps.Padding = new System.Windows.Forms.Padding(2);
            this.tabPinnedApps.Size = new System.Drawing.Size(526, 388);
            this.tabPinnedApps.TabIndex = 3;
            this.tabPinnedApps.Text = "Pinned Applications";
            this.tabPinnedApps.UseVisualStyleBackColor = true;
            // 
            // lstPinnedApps
            // 
            this.lstPinnedApps.ContextMenuStrip = this.mnuPinnedApps;
            this.lstPinnedApps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstPinnedApps.FormattingEnabled = true;
            this.lstPinnedApps.HorizontalScrollbar = true;
            this.lstPinnedApps.Location = new System.Drawing.Point(2, 2);
            this.lstPinnedApps.Margin = new System.Windows.Forms.Padding(2);
            this.lstPinnedApps.Name = "lstPinnedApps";
            this.lstPinnedApps.Size = new System.Drawing.Size(522, 384);
            this.lstPinnedApps.TabIndex = 0;
            // 
            // tabOptions
            // 
            this.tabOptions.Controls.Add(this.checkBox1);
            this.tabOptions.Location = new System.Drawing.Point(4, 22);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.Size = new System.Drawing.Size(526, 388);
            this.tabOptions.TabIndex = 4;
            this.tabOptions.Text = "Options";
            this.tabOptions.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(16, 16);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(278, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Start FreeVD automatically when I sign in to Windows";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(5, 10);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(101, 13);
            this.lblVersion.TabIndex = 23;
            this.lblVersion.Text = "Version Placeholder";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(473, 2);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(53, 27);
            this.btnOK.TabIndex = 22;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(414, 2);
            this.btnApply.Margin = new System.Windows.Forms.Padding(2);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(55, 27);
            this.btnApply.TabIndex = 21;
            this.btnApply.Text = "&Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(354, 2);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(56, 27);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 461);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(560, 500);
            this.Name = "frmMain";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FreeVD Settings";
            this.SystemTrayMenu.ResumeLayout(false);
            this.mnuPinnedApps.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPinnedApps.ResumeLayout(false);
            this.tabOptions.ResumeLayout(false);
            this.tabOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.NotifyIcon SystemTray;
        internal System.Windows.Forms.ContextMenuStrip SystemTrayMenu;
        internal System.Windows.Forms.ToolStripMenuItem mnuSettings;
        internal System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuSwitchDesktop;
        private System.Windows.Forms.ContextMenuStrip mnuPinnedApps;
        private System.Windows.Forms.ToolStripMenuItem mnuUnpin;
        private System.Windows.Forms.ToolStripMenuItem mnuGatherWindows;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblVersion;
        internal System.Windows.Forms.Button btnOK;
        internal System.Windows.Forms.Button btnApply;
        internal System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabPage1;
        internal System.Windows.Forms.ListView lstHotkeys;
        internal System.Windows.Forms.ColumnHeader colTask;
        internal System.Windows.Forms.ColumnHeader colHotKey;
        internal System.Windows.Forms.Button btnAddHotkey;
        internal System.Windows.Forms.Button btnDeleteHotkey;
        private System.Windows.Forms.TabPage tabPinnedApps;
        private System.Windows.Forms.ListBox lstPinnedApps;
        private System.Windows.Forms.TabPage tabOptions;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

