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
            this.HotkeyMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.HotkeyMenu_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.HotkeyMenu_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.HotkeyMenu_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddHotkey = new System.Windows.Forms.Button();
            this.btnDeleteHotkey = new System.Windows.Forms.Button();
            this.tabPinnedApps = new System.Windows.Forms.TabPage();
            this.PinnedAppList = new System.Windows.Forms.ListView();
            this.TypeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabOptions = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.LicenseLabel = new System.Windows.Forms.Label();
            this.AutoStartCheckbox = new System.Windows.Forms.CheckBox();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.ButtonOK = new System.Windows.Forms.Button();
            this.ButtonApply = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.PinnedAppsMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.HotkeyMenu.SuspendLayout();
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
            this.MenuUnpin.Text = "&Unpin";
            this.MenuUnpin.Click += new System.EventHandler(this.MenuUnpin_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
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
            this.splitContainer1.Panel2.Controls.Add(this.VersionLabel);
            this.splitContainer1.Panel2.Controls.Add(this.ButtonOK);
            this.splitContainer1.Panel2.Controls.Add(this.ButtonApply);
            this.splitContainer1.Panel2.Controls.Add(this.ButtonCancel);
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
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(526, 386);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Hotkeys";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // HotkeyList
            // 
            this.HotkeyList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTask,
            this.colHotKey});
            this.HotkeyList.ContextMenuStrip = this.HotkeyMenu;
            this.HotkeyList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HotkeyList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.HotkeyList.Location = new System.Drawing.Point(2, 2);
            this.HotkeyList.Margin = new System.Windows.Forms.Padding(2);
            this.HotkeyList.Name = "HotkeyList";
            this.HotkeyList.Size = new System.Drawing.Size(522, 382);
            this.HotkeyList.TabIndex = 11;
            this.HotkeyList.UseCompatibleStateImageBehavior = false;
            this.HotkeyList.View = System.Windows.Forms.View.Details;
            this.HotkeyList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.HotkeyList_MouseDoubleClick);
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
            // HotkeyMenu
            // 
            this.HotkeyMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HotkeyMenu_Add,
            this.HotkeyMenu_Edit,
            this.HotkeyMenu_Delete});
            this.HotkeyMenu.Name = "HotkeyMenu";
            this.HotkeyMenu.Size = new System.Drawing.Size(108, 70);
            // 
            // HotkeyMenu_Add
            // 
            this.HotkeyMenu_Add.Name = "HotkeyMenu_Add";
            this.HotkeyMenu_Add.Size = new System.Drawing.Size(107, 22);
            this.HotkeyMenu_Add.Text = "&Add";
            this.HotkeyMenu_Add.Click += new System.EventHandler(this.HotkeyMenu_Add_Click);
            // 
            // HotkeyMenu_Edit
            // 
            this.HotkeyMenu_Edit.Name = "HotkeyMenu_Edit";
            this.HotkeyMenu_Edit.Size = new System.Drawing.Size(107, 22);
            this.HotkeyMenu_Edit.Text = "&Edit";
            this.HotkeyMenu_Edit.Click += new System.EventHandler(this.HotkeyMenu_Edit_Click);
            // 
            // HotkeyMenu_Delete
            // 
            this.HotkeyMenu_Delete.Name = "HotkeyMenu_Delete";
            this.HotkeyMenu_Delete.Size = new System.Drawing.Size(107, 22);
            this.HotkeyMenu_Delete.Text = "&Delete";
            this.HotkeyMenu_Delete.Click += new System.EventHandler(this.HotkeyMenu_Delete_Click);
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
            this.tabPinnedApps.Location = new System.Drawing.Point(4, 24);
            this.tabPinnedApps.Margin = new System.Windows.Forms.Padding(2);
            this.tabPinnedApps.Name = "tabPinnedApps";
            this.tabPinnedApps.Padding = new System.Windows.Forms.Padding(2);
            this.tabPinnedApps.Size = new System.Drawing.Size(526, 386);
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
            this.PinnedAppList.Size = new System.Drawing.Size(522, 382);
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
            this.tabOptions.Controls.Add(this.textBox1);
            this.tabOptions.Controls.Add(this.LicenseLabel);
            this.tabOptions.Controls.Add(this.AutoStartCheckbox);
            this.tabOptions.Location = new System.Drawing.Point(4, 24);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.Size = new System.Drawing.Size(526, 386);
            this.tabOptions.TabIndex = 4;
            this.tabOptions.Text = "Options";
            this.tabOptions.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.SystemColors.Window;
            this.textBox1.Location = new System.Drawing.Point(16, 146);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(491, 223);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // LicenseLabel
            // 
            this.LicenseLabel.AutoSize = true;
            this.LicenseLabel.Location = new System.Drawing.Point(13, 128);
            this.LicenseLabel.Name = "LicenseLabel";
            this.LicenseLabel.Size = new System.Drawing.Size(138, 15);
            this.LicenseLabel.TabIndex = 1;
            this.LicenseLabel.Text = "FreeVD {Version} License:";
            // 
            // AutoStartCheckbox
            // 
            this.AutoStartCheckbox.AutoSize = true;
            this.AutoStartCheckbox.Location = new System.Drawing.Point(16, 16);
            this.AutoStartCheckbox.Name = "AutoStartCheckbox";
            this.AutoStartCheckbox.Size = new System.Drawing.Size(307, 19);
            this.AutoStartCheckbox.TabIndex = 0;
            this.AutoStartCheckbox.Text = "Start FreeVD automatically when I sign in to Windows";
            this.AutoStartCheckbox.UseVisualStyleBackColor = true;
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Location = new System.Drawing.Point(5, 10);
            this.VersionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(53, 15);
            this.VersionLabel.TabIndex = 23;
            this.VersionLabel.Text = "{Version}";
            // 
            // ButtonOK
            // 
            this.ButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonOK.Location = new System.Drawing.Point(298, 3);
            this.ButtonOK.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonOK.Name = "ButtonOK";
            this.ButtonOK.Size = new System.Drawing.Size(74, 27);
            this.ButtonOK.TabIndex = 22;
            this.ButtonOK.Text = "&OK";
            this.ButtonOK.UseVisualStyleBackColor = true;
            this.ButtonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // ButtonApply
            // 
            this.ButtonApply.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonApply.Location = new System.Drawing.Point(376, 3);
            this.ButtonApply.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonApply.Name = "ButtonApply";
            this.ButtonApply.Size = new System.Drawing.Size(74, 27);
            this.ButtonApply.TabIndex = 21;
            this.ButtonApply.Text = "&Apply";
            this.ButtonApply.UseVisualStyleBackColor = true;
            this.ButtonApply.Click += new System.EventHandler(this.ButtonApply_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Location = new System.Drawing.Point(454, 3);
            this.ButtonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(74, 27);
            this.ButtonCancel.TabIndex = 20;
            this.ButtonCancel.Text = "&Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.ButtonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.ButtonCancel;
            this.ClientSize = new System.Drawing.Size(544, 461);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.HotkeyMenu.ResumeLayout(false);
            this.tabPinnedApps.ResumeLayout(false);
            this.tabOptions.ResumeLayout(false);
            this.tabOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip PinnedAppsMenu;
        private System.Windows.Forms.ToolStripMenuItem MenuUnpin;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label VersionLabel;
        internal System.Windows.Forms.Button ButtonOK;
        internal System.Windows.Forms.Button ButtonApply;
        internal System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabPage1;
        internal System.Windows.Forms.ListView HotkeyList;
        internal System.Windows.Forms.ColumnHeader colTask;
        internal System.Windows.Forms.ColumnHeader colHotKey;
        internal System.Windows.Forms.Button btnAddHotkey;
        internal System.Windows.Forms.Button btnDeleteHotkey;
        private System.Windows.Forms.TabPage tabPinnedApps;
        private System.Windows.Forms.TabPage tabOptions;
        private System.Windows.Forms.CheckBox AutoStartCheckbox;
        private System.Windows.Forms.ListView PinnedAppList;
        private System.Windows.Forms.ColumnHeader TypeHeader;
        private System.Windows.Forms.ColumnHeader NameHeader;
        private System.Windows.Forms.ContextMenuStrip HotkeyMenu;
        private System.Windows.Forms.ToolStripMenuItem HotkeyMenu_Edit;
        private System.Windows.Forms.ToolStripMenuItem HotkeyMenu_Delete;
        private System.Windows.Forms.ToolStripMenuItem HotkeyMenu_Add;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label LicenseLabel;
    }
}

