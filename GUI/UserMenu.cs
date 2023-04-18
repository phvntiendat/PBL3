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
using PBL3.GUI;
using System.Runtime.InteropServices;

namespace PBL3
{
    public partial class UserMenu : Form
    {
        public UserMenu()
        {
            InitializeComponent();
            lb_username.Text = Provider.Instance.currentUser.Fullname;
            pnSub.Visible = false;
            openChildForm(new Home());
        }


        private Form acctiveForm = null;
        private void openChildForm(Form ChildForm)
        {
            if (acctiveForm != null)
                acctiveForm.Close();
            acctiveForm = ChildForm;
            ChildForm.TopLevel = false;
            ChildForm.FormBorderStyle = FormBorderStyle.None;
            ChildForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(ChildForm);
            panelChildForm.Tag = ChildForm;
            ChildForm.BringToFront();
            ChildForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openChildForm(new Home());
            lbTitle.Text = "Home";
            pnSub.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pnSub.Visible = true;
            //openChildForm(new VaccinationInfo());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //openChildForm(new AccountSettings_Account());
            AccountSettings_Account v = new AccountSettings_Account();
            v.d = new AccountSettings_Account.MyDelegate(UpdateUsernameLabel);
            openChildForm(v);
            pnSub.Visible = false;
            lbTitle.Text = "Account Settings";
        }

        private void btn_signOut_Click(object sender, EventArgs e)
        {
            this.Dispose();
            SignIn signIn = new SignIn();
            signIn.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            openChildForm(new VaccineRegistration());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            openChildForm(new VaccinationInfo());
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
        private void btnInfo_Click(object sender, EventArgs e)
        {
            VaccinationInfo v = new VaccinationInfo();
            v.d = new VaccinationInfo.MyDelegate(UpdateUsernameLabel);
            openChildForm(v);
            lbTitle.Text = "Vaccination Information";
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            openChildForm(new VaccineRegistration());
            lbTitle.Text = "Register for Vaccination";
        }
        public void UpdateUsernameLabel()
        {
            lb_username.Text = Provider.Instance.currentUser.Fullname;
        }

        private void btn_ExitApp_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to exit?", "NOTICE", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                this.Dispose();
                Application.Exit();
            }
        }
    }
}
