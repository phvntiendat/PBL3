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
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void btn_showPassword_MouseDown(object sender, MouseEventArgs e)
        {
            Button B = (Button)sender;
            switch (B.Name)
            {
                case "btn_showPassword":
                    txt_password.PasswordChar = '\0';
                    break;
                case "btn_reShowPassword":
                    txt_rePassword.PasswordChar = '\0';
                    break;
                default:
                    break;
            }

        }

        private void btn_showPassword_MouseUp(object sender, MouseEventArgs e)
        {
            Button B = (Button)sender;
            switch (B.Name)
            {
                case "btn_showPassword":
                    txt_password.PasswordChar = '*';
                    break;
                case "btn_reShowPassword":
                    txt_rePassword.PasswordChar = '*';
                    break;
                default:
                    break;
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Dispose();
        }

        private void btn_createAccount_Click(object sender, EventArgs e)
        {
            if (txt_username.Text != "" && txt_cmnd.Text != "")
            {
                if (txt_password.Text == txt_rePassword.Text && txt_password.Text != "")
                {
                    string cmnd = txt_cmnd.Text;
                    string username = txt_username.Text;
                    string password = txt_password.Text;
                    bool flag = true;
                    foreach (Account i in Provider.Instance.database.Accounts)
                    {
                        if (i.Username == username && i.CMND_CCCD == cmnd)
                        {
                            flag = false;
                            MessageBox.Show("Username already exist.\nPlease try again.", "NOTICE");
                            break;
                        }
                    }
                    foreach (Citizen i in Provider.Instance.database.Citizens)
                    {
                        if (i.CMND_CCCD == cmnd)
                        {
                            flag = false;
                            MessageBox.Show("CMND/CCCD already exist.\nPlease try again.", "NOTICE");
                            break;
                        }
                    }
                    if (flag)
                    {
                        Account newAccount = new Account(cmnd, "Người dùng", username, password, false);
                        Citizen newCitizen = new Citizen(cmnd, "", "", true, "", DateTime.Now, 0);
                        Provider.Instance.database.Citizens.Add(newCitizen);
                        Provider.Instance.database.Accounts.Add(newAccount);
                        Provider.Instance.database.SaveChangesAsync();
                        MessageBox.Show("Create account successfully.", "NOTICE");
                        this.Close();
                    }

                }
                else
                {
                    MessageBox.Show("Password does not match!", "NOTICE");
                }
            }
            else
            {
                MessageBox.Show("CMND/CCCD or Username can not blank!", "NOTICE");
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
        private void btnMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
