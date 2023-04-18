using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PBL3.BLL;

namespace PBL3
{
    public partial class AccountSettings_Account : Form
    {
        public delegate void MyDelegate();
        public MyDelegate d;
        public AccountSettings_Account()
        {
            InitializeComponent();
            GUI();
        }
        public void GUI()
        {
            txtUsername.Text = Provider.Instance.currentUser.Username;
            txtUsername.Enabled = false;
            txtFullname.Text = Provider.Instance.currentUser.Fullname;
            txtFullname.Enabled = false;
            if(Provider.Instance.currentUser.Permission == true)
            {
                //txtCMND.Hide();
            }
            txtCMND.Text = Provider.Instance.currentUser.CMND_CCCD;
            txtCMND.Enabled = false;

            txt_password.Text = Provider.Instance.currentUser.Password;
            txt_password.Enabled = false;
        }
        private void btn_showPassword_MouseDown(object sender, MouseEventArgs e)
        {
            txt_password.PasswordChar = '\0';
            txt_password.Text = Provider.Instance.currentUser.Password;
        }

        private void btn_showPassword_MouseUp(object sender, MouseEventArgs e)
        {
            txt_password.PasswordChar = '*';
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            ForgotPassWord_step2 f = new ForgotPassWord_step2(Provider.Instance.currentUser);
            f.Show();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            txtFullname.Enabled = true;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtFullname.Text = Provider.Instance.currentUser.Fullname;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if(txtFullname.Text == "")
            {
                MessageBox.Show("Can not empty", "NOTICE");
            }
            else
            {
                Provider.Instance.ChangingFullname(Provider.Instance.currentUser, txtFullname.Text);
                txtFullname.Enabled = false;
                d();
            }
        }
    }
}
