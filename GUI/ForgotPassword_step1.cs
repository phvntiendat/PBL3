using PBL3.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PBL3.DTO;
using System.Runtime.InteropServices;

namespace PBL3
{
    public partial class ForgotPassword_step1 : Form
    {
        public ForgotPassword_step1()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(55, 54, 92);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void btn_checkUsernameValid_Click(object sender, EventArgs e)
        {
            Account userForgotten = null;
            string username = txt_username.Text;
            string cmnd = txtCMND_CCCD.Text;
            foreach (Account i in Provider.Instance.database.Accounts)
            {
                if (username == i.Username && cmnd == i.CMND_CCCD)
                {
                    userForgotten = i;
                    break;
                }
                else
                { }
            }
            if (userForgotten != null)
            {
                ForgotPassWord_step2 fg_2 = new ForgotPassWord_step2(userForgotten);
                this.Close();
                fg_2.Show(); 
            }
            else
            {
                MessageBox.Show("Does not exist this user.", "NOTICE");
            }
        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
