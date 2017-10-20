using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreeVD
{
    public partial class HotKeyForm : Form
    {
        public HotKeyForm()
        {
            InitializeComponent();
            Array values = Enum.GetValues(typeof(Keys));

            foreach (Keys k in values)
            {
                cmbKey.Items.Add(k.ToString());
            }
        }

        private void txtHotkey_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtHotkey_KeyDown(object sender, KeyEventArgs e)
        {            

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Hotkey hk = new Hotkey(cmbDesktopNumber.Text);
            //HotkeyItem hki = new HotkeyItem(cmbHotkeyType.Text, hk);
            //KeysConverter kc = new KeysConverter();
            //if (hk.Register((Keys)kc.ConvertFromString(cmbKey.Text), chkALT.Checked, chkCTRL.Checked, chkSHIFT.Checked, chkWIN.Checked) == true)
            //{
            //    Program.hotkeys.Add(hki);

            //    switch (cmbHotkeyType.Text)
            //    {
            //        case "Navigate to Desktop":
            //            hk.Callback = VirtualDesktopFunctions.DesktopGo;
            //            break;
            //        case "Move Window to Desktop":
            //            switch (cmbDesktopNumber.Text)
            //            {
            //                case "1":
            //                case "2":
            //                case "3":
            //                case "4":
            //                case "5":
            //                case "6":
            //                case "7":
            //                case "8":
            //                case "9":
            //                    hk.Callback = VirtualDesktopFunctions.DesktopMove;
            //                    break;
            //                case "Next":
            //                    hk.Callback = VirtualDesktopFunctions.DesktopMoveNext;
            //                    break;
            //                case "Previous":
            //                    hk.Callback = VirtualDesktopFunctions.DesktopMovePrevious;
            //                    break;
            //                default:
            //                    break;
            //            }
            //            break;
            //        case "Move Window to Desktop & Follow":
            //            switch (cmbDesktopNumber.Text)
            //            {
            //                case "1":
            //                case "2":
            //                case "3":
            //                case "4":
            //                case "5":
            //                case "6":
            //                case "7":
            //                case "8":
            //                case "9":
            //                    hk.Callback = VirtualDesktopFunctions.DesktopMoveFollow;
            //                    break;
            //                case "Next":
            //                    hk.Callback = VirtualDesktopFunctions.DesktopMoveNextFollow;
            //                    break;
            //                case "Previous":
            //                    hk.Callback = VirtualDesktopFunctions.DesktopMovePreviousFollow;
            //                    break;
            //                default:
            //                    break;
            //            }
            //            break;
            //        case "Pin/Unpin Window":
            //            hk.Callback = VirtualDesktopFunctions.PinWindow;
            //            break;
            //        case "Pin/Unpin Application":
            //            hk.Callback = VirtualDesktopFunctions.PinApp;
            //            break;
            //        default:
            //            break;
            //    }

            //    Program.MainForm.UpdateHotkeyTab();
            //    Program.MainForm.SaveSettings();
            //    this.Close();
            //}
        }
    }
}
