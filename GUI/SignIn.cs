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
    public partial class SignIn : Form
    {
        public SignIn()
        {
            InitializeComponent();
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
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ForgotPassword_step1 fg_1 = new ForgotPassword_step1();
            fg_1.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignUp signup = new SignUp();
            signup.Show();
        }

        private void btn_showPassword_MouseDown(object sender, MouseEventArgs e)
        {
            txt_password.PasswordChar = '\0';
        }

        private void btn_showPassword_MouseUp(object sender, MouseEventArgs e)
        {
            txt_password.PasswordChar = '*';
        }

        private void btn_signIn_Click(object sender, EventArgs e)
        {
            if (txt_password.Text != "" && txt_userName.Text != "")
            {
                string username = txt_userName.Text;
                string password = txt_password.Text;
                foreach (Account i in Provider.Instance.database.Accounts)
                {
                    if (username == i.Username && password == i.Password)
                    {
                        Provider.Instance.currentUser = i;
                        break;
                    }
                    else
                    {
                        Provider.Instance.currentUser = null;
                    }
                }
                if (Provider.Instance.currentUser != null)
                {
                    //Nếu có quyền admin, mở menu Admin
                    if (Provider.Instance.currentUser.Permission)
                    {
                        this.Hide();
                        AdminMenu adminMenu = new AdminMenu();
                        adminMenu.Show();
                    }
                    else
                    {
                        this.Hide();
                        UserMenu userMenu = new UserMenu();
                        userMenu.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Username or password incorrect.", "NOTICE");
                }
            }
            else
            {
                MessageBox.Show("Username or password incorrect.", "NOTICE");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to exit?", "NOTICE", MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                this.Dispose();
                Application.Exit();
            }
        }
    }
}
