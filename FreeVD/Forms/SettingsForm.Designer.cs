namespace FreeVD
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.PinnedAppsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuUnpin = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.HotkeyList = new System.Windows.Forms.ListView();
            this.colTask = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHotKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAddHotkey = new System.Windows.Forms.Button();
            this.btnDeleteHotkey = new System.Windows.Forms.Button();
            this.tabPinnedApps = new System.Windows.Forms.TabPage();
            this.PinnedAppList = new System.Windows.Forms.ListView();
            this.TypeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabOptions = new System.Windows.Forms.TabPage();
            this.cbAutoStart = new System.Windows.Forms.CheckBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.PinnedAppsMenu.SuspendLayout();
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
            // PinnedAppsMenu
            // 
            this.PinnedAppsMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.PinnedAppsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuUnpin});
            this.PinnedAppsMenu.Name = "mnuPinnedApps";
            this.PinnedAppsMenu.Size = new System.Drawing.Size(107, 26);
            // 
            // MenuUnpin
            // 
            this.MenuUnpin.Name = "MenuUnpin";
            this.MenuUnpin.Size = new System.Drawing.Size(106, 22);
            this.MenuUnpin.Text = "Unpin";
            this.MenuUnpin.Click += new System.EventHandler(this.MenuUnpin_Click);
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
            this.tabPage1.Controls.Add(this.HotkeyList);
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
            // HotkeyList
            // 
            this.HotkeyList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTask,
            this.colHotKey});
            this.HotkeyList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HotkeyList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.HotkeyList.Location = new System.Drawing.Point(2, 2);
            this.HotkeyList.Margin = new System.Windows.Forms.Padding(2);
            this.HotkeyList.Name = "HotkeyList";
            this.HotkeyList.Size = new System.Drawing.Size(522, 384);
            this.HotkeyList.TabIndex = 11;
            this.HotkeyList.UseCompatibleStateImageBehavior = false;
            this.HotkeyList.View = System.Windows.Forms.View.Details;
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
            this.tabPinnedApps.Controls.Add(this.PinnedAppList);
            this.tabPinnedApps.Location = new System.Drawing.Point(4, 22);
            this.tabPinnedApps.Margin = new System.Windows.Forms.Padding(2);
            this.tabPinnedApps.Name = "tabPinnedApps";
            this.tabPinnedApps.Padding = new System.Windows.Forms.Padding(2);
            this.tabPinnedApps.Size = new System.Drawing.Size(526, 388);
            this.tabPinnedApps.TabIndex = 3;
            this.tabPinnedApps.Text = "Pinned Apps";
            this.tabPinnedApps.UseVisualStyleBackColor = true;
            // 
            // PinnedAppList
            // 
            this.PinnedAppList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TypeHeader,
            this.NameHeader});
            this.PinnedAppList.ContextMenuStrip = this.PinnedAppsMenu;
            this.PinnedAppList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PinnedAppList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.PinnedAppList.Location = new System.Drawing.Point(2, 2);
            this.PinnedAppList.MultiSelect = false;
            this.PinnedAppList.Name = "PinnedAppList";
            this.PinnedAppList.Size = new System.Drawing.Size(522, 384);
            this.PinnedAppList.TabIndex = 0;
            this.PinnedAppList.UseCompatibleStateImageBehavior = false;
            this.PinnedAppList.View = System.Windows.Forms.View.Details;
            // 
            // TypeHeader
            // 
            this.TypeHeader.Text = "Type";
            this.TypeHeader.Width = 87;
            // 
            // NameHeader
            // 
            this.NameHeader.Text = "Name";
            this.NameHeader.Width = 428;
            // 
            // tabOptions
            // 
            this.tabOptions.Controls.Add(this.cbAutoStart);
            this.tabOptions.Location = new System.Drawing.Point(4, 22);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.Size = new System.Drawing.Size(526, 388);
            this.tabOptions.TabIndex = 4;
            this.tabOptions.Text = "Options";
            this.tabOptions.UseVisualStyleBackColor = true;
            // 
            // cbAutoStart
            // 
            this.cbAutoStart.AutoSize = true;
            this.cbAutoStart.Location = new System.Drawing.Point(16, 16);
            this.cbAutoStart.Name = "cbAutoStart";
            this.cbAutoStart.Size = new System.Drawing.Size(278, 17);
            this.cbAutoStart.TabIndex = 0;
            this.cbAutoStart.Text = "Start FreeVD automatically when I sign in to Windows";
            this.cbAutoStart.UseVisualStyleBackColor = true;
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
            this.btnOK.Location = new System.Drawing.Point(298, 3);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(74, 27);
            this.btnOK.TabIndex = 22;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(376, 3);
            this.btnApply.Margin = new System.Windows.Forms.Padding(2);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(74, 27);
            this.btnApply.TabIndex = 21;
            this.btnApply.Text = "&Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.ButtonApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(454, 3);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 27);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(544, 461);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(560, 500);
            this.Name = "SettingsForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FreeVD Settings";
            this.PinnedAppsMenu.ResumeLayout(false);
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
        private System.Windows.Forms.ContextMenuStrip PinnedAppsMenu;
        private System.Windows.Forms.ToolStripMenuItem MenuUnpin;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblVersion;
        internal System.Windows.Forms.Button btnOK;
        internal System.Windows.Forms.Button btnApply;
        internal System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabPage1;
        internal System.Windows.Forms.ListView HotkeyList;
        internal System.Windows.Forms.ColumnHeader colTask;
        internal System.Windows.Forms.ColumnHeader colHotKey;
        internal System.Windows.Forms.Button btnAddHotkey;
        internal System.Windows.Forms.Button btnDeleteHotkey;
        private System.Windows.Forms.TabPage tabPinnedApps;
        private System.Windows.Forms.TabPage tabOptions;
        private System.Windows.Forms.CheckBox cbAutoStart;
        private System.Windows.Forms.ListView PinnedAppList;
        private System.Windows.Forms.ColumnHeader TypeHeader;
        private System.Windows.Forms.ColumnHeader NameHeader;
    }
}

