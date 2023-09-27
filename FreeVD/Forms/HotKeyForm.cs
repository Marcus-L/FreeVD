using Humanizer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreeVD
{
    public partial class HotkeyForm : Form
    {
        public class ActionItem
        {
            public VDAction Action { get; set; }
            public string Text { get; set; }
        }

        public ActionItem SelectedAction => (ActionItem)ComboAction.SelectedItem;

        public VDHotkey Hotkey { get; set; }

        public HotkeyForm()
        {
            InitializeComponent();
            Array values = Enum.GetValues(typeof(Keys));

            var actions = Enum.GetValues(typeof(VDAction)).Cast<VDAction>()
                .Select(vda => new ActionItem()
                {
                    Text = vda.Humanize(LetterCasing.Title),
                    Action = vda
                });

            ComboAction.ValueMember = "Action";
            ComboAction.DisplayMember = "Text";
            ComboAction.DataSource = actions.ToList();

            Load += (obj, args) =>
            {
                CbFollow.Checked = Hotkey.Follow;
                NumDesktop.Value = Hotkey.DesktopNumber;
                TbKeys.Text = Hotkey.ToString();
                ComboAction.SelectedValue = Hotkey.Action;
                ConfigureOptions();
                ActiveControl = TbKeys;
            };
        }

        private void ComboAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Hotkey != null && Hotkey.Action != SelectedAction.Action)
            {
                Hotkey.Action = SelectedAction.Action;
                ConfigureOptions();
            }
        }

        private void NumDesktop_ValueChanged(object sender, EventArgs e)
        {
            Hotkey.DesktopNumber = (int)NumDesktop.Value;
        }

        private void CbFollow_CheckedChanged(object sender, EventArgs e)
        {
            Hotkey.Follow = CbFollow.Checked;
        }

        private void ConfigureOptions()
        {
            NumDesktop.Enabled = SelectedAction.Text.ToLower().EndsWith("to desktop");
            if (!NumDesktop.Enabled)
            {
                Hotkey.DesktopNumber = 0;
                NumDesktop.Value = 0;
            }
            CbFollow.Enabled = SelectedAction.Text.ToLower().StartsWith("move window");
            if (!CbFollow.Enabled)
            {
                Hotkey.Follow = false;
                CbFollow.Checked = false;
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (NumDesktop.Enabled && (NumDesktop.Value < 1 || NumDesktop.Value > 20))
            {
                MessageBox.Show("Desktop Number must be between 1 and 20.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Hotkey.Key == 0)
            {
                MessageBox.Show("Please select a valid hotkey.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void TbKeys_KeyDown(object sender, KeyEventArgs e)
        {
            uint key = (uint)e.KeyCode;
            switch (e.KeyCode)
            {
                case Keys.Menu:
                    Hotkey.Alt = true;
                    break;
                case Keys.ControlKey:
                    Hotkey.Ctrl = true;
                    break;
                case Keys.LWin:
                case Keys.RWin:
                    Hotkey.Win = true;
                    break;
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                case Keys.ShiftKey:
                    Hotkey.Shift = true;
                    break;
                case Keys.Back:
                case Keys.Delete:
                    Hotkey.Shift = false;
                    Hotkey.Alt = false;
                    Hotkey.Ctrl = false;
                    Hotkey.Win = false;
                    Hotkey.Key = 0;
                    break;
                default:
                    Hotkey.Key = (uint)e.KeyCode;
                    break;
            }
            TbKeys.Text = Hotkey.ToString();

            // prevent edit and event bubble
            e.SuppressKeyPress = true;
            e.Handled = true;
        }

        private void TbKeys_KeyUp(object sender, KeyEventArgs e)
        {
            // prevent registered hotkeys from picking up the event
            e.Handled = true;
        }
    }
}
