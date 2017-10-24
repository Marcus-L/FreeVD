namespace FreeVD
{
    partial class HotkeyForm
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
            this.lblAction = new System.Windows.Forms.Label();
            this.ComboAction = new System.Windows.Forms.ComboBox();
            this.lblDesktopNumber = new System.Windows.Forms.Label();
            this.BtnOK = new System.Windows.Forms.Button();
            this.TbKeys = new System.Windows.Forms.TextBox();
            this.CbFollow = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.NumDesktop = new System.Windows.Forms.NumericUpDown();
            this.BtnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NumDesktop)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAction
            // 
            this.lblAction.Location = new System.Drawing.Point(22, 7);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(85, 27);
            this.lblAction.TabIndex = 0;
            this.lblAction.Text = "&Action:";
            this.lblAction.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ComboAction
            // 
            this.ComboAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboAction.FormattingEnabled = true;
            this.ComboAction.Location = new System.Drawing.Point(114, 9);
            this.ComboAction.Name = "ComboAction";
            this.ComboAction.Size = new System.Drawing.Size(221, 23);
            this.ComboAction.TabIndex = 1;
            this.ComboAction.SelectedIndexChanged += new System.EventHandler(this.ComboAction_SelectedIndexChanged);
            // 
            // lblDesktopNumber
            // 
            this.lblDesktopNumber.Location = new System.Drawing.Point(7, 45);
            this.lblDesktopNumber.Name = "lblDesktopNumber";
            this.lblDesktopNumber.Size = new System.Drawing.Size(100, 27);
            this.lblDesktopNumber.TabIndex = 2;
            this.lblDesktopNumber.Text = "Desktop &Number:";
            this.lblDesktopNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BtnOK
            // 
            this.BtnOK.Location = new System.Drawing.Point(155, 126);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(87, 27);
            this.BtnOK.TabIndex = 7;
            this.BtnOK.Text = "&OK";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // TbKeys
            // 
            this.TbKeys.BackColor = System.Drawing.SystemColors.Window;
            this.TbKeys.Location = new System.Drawing.Point(114, 85);
            this.TbKeys.Name = "TbKeys";
            this.TbKeys.Size = new System.Drawing.Size(221, 23);
            this.TbKeys.TabIndex = 6;
            this.TbKeys.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbKeys_KeyDown);
            this.TbKeys.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TbKeys_KeyUp);
            // 
            // CbFollow
            // 
            this.CbFollow.AutoSize = true;
            this.CbFollow.Location = new System.Drawing.Point(182, 50);
            this.CbFollow.Name = "CbFollow";
            this.CbFollow.Size = new System.Drawing.Size(86, 19);
            this.CbFollow.TabIndex = 4;
            this.CbFollow.Text = "And &Follow";
            this.CbFollow.UseVisualStyleBackColor = true;
            this.CbFollow.CheckedChanged += new System.EventHandler(this.CbFollow_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "&Keys:";
            // 
            // NumDesktop
            // 
            this.NumDesktop.Location = new System.Drawing.Point(114, 48);
            this.NumDesktop.Name = "NumDesktop";
            this.NumDesktop.Size = new System.Drawing.Size(55, 23);
            this.NumDesktop.TabIndex = 3;
            this.NumDesktop.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NumDesktop.ValueChanged += new System.EventHandler(this.NumDesktop_ValueChanged);
            // 
            // BtnCancel
            // 
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(248, 126);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(87, 27);
            this.BtnCancel.TabIndex = 8;
            this.BtnCancel.Text = "&Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // HotkeyForm
            // 
            this.AcceptButton = this.BtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(358, 171);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.NumDesktop);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TbKeys);
            this.Controls.Add(this.CbFollow);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.lblDesktopNumber);
            this.Controls.Add(this.ComboAction);
            this.Controls.Add(this.lblAction);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HotkeyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Hotkey";
            ((System.ComponentModel.ISupportInitialize)(this.NumDesktop)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.ComboBox ComboAction;
        private System.Windows.Forms.Label lblDesktopNumber;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.TextBox TbKeys;
        private System.Windows.Forms.CheckBox CbFollow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown NumDesktop;
        private System.Windows.Forms.Button BtnCancel;
    }
}