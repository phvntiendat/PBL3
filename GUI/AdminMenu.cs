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
    public partial class AdminMenu : Form
    {
        public AdminMenu()
        {
            InitializeComponent();
            customizeDesign();
            lb_username.Text = Provider.Instance.currentUser.Fullname;
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

        /*
         đoạn code giúp hide và open cá sub
         */
        private void customizeDesign()
        {
            panelSubData.Visible = false;
            panelsubAccount.Visible = false;
        }
        private void hideSubMenu()
        {
            if (panelSubData.Visible == true)
                panelSubData.Visible = false;
            if (panelsubAccount.Visible == true)
                panelsubAccount.Visible = false;
        }
        private void showSubMenu(Panel subMenu)
        {
            if(subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openChildForm(new Home());
            lbTitle.Text = "Home";
        }

        private void btnDataManaging_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubData);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            openChildForm(new CitizenData());
            lbTitle.Text = "Citizen Data";
        }
        private void button7_Click(object sender, EventArgs e)
        {
            openChildForm(new RegistrationData());
            lbTitle.Text = "Registration Data";

        }
        private void button8_Click(object sender, EventArgs e)
        {
            openChildForm(new VaccineData());
            lbTitle.Text = "Vaccine Data";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openChildForm(new Statistics());
            lbTitle.Text = "Statistics";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AccountSettings_Account v = new AccountSettings_Account();
            v.d = new AccountSettings_Account.MyDelegate(UpdateUsernameLabel);
            openChildForm(v);
            lbTitle.Text = "Account Information";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            showSubMenu(panelsubAccount);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            openChildForm(new AccountSettings_UserMnaging());
            lbTitle.Text = "User Managing";
        }
        private void btn_logOut_Click(object sender, EventArgs e)
        {
            this.Close();
            SignIn signIn = new SignIn();
            signIn.Show();
        }

        //
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
