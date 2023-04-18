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
using PBL3.BLL;
using System.Runtime.InteropServices;

namespace PBL3
{
    public partial class CitizenAddEdit : Form
    {
        public delegate void MyDelegate(string txt, string Address, string Does);
        public MyDelegate d;
        string CMND_CCCD = "";
        public CitizenAddEdit(string m)
        {
            InitializeComponent();
            CMND_CCCD = m;
            InitCBB();
            GUI();
        }
        public void InitCBB()
        {
            cbbGender.Items.Add("Male");
            cbbGender.Items.Add("Female");
            cbbDoes.Items.Add("0");
            cbbDoes.Items.Add("1");
            cbbDoes.Items.Add("2");
            cbbDoes.Items.Add("3");
            cbbDoes.Items.Add("4");
        }
        public void GUI()
        {
            if (CMND_CCCD != "")
            {
                Citizen s = Provider.Instance.GetCitizen_By_CMND(CMND_CCCD);
                txtCMND.Text = s.CMND_CCCD;
                txtCMND.Enabled = false;
                txtPhone.Text = s.phone;
                txtFullname.Text = s.fullName;
                txtAddress.Text = s.address;
                if (s.gender)
                {
                    cbbGender.SelectedIndex = 0;
                }
                else
                {
                    cbbGender.SelectedIndex = 1;
                }
                dateTimePicker1.Value = s.birth;
                cbbDoes.SelectedItem = s.vaccination.ToString();
            }
        }
        public Citizen GetData()
        {
            Citizen s = new Citizen();

            s.CMND_CCCD = txtCMND.Text;
            s.fullName = txtFullname.Text;
            s.phone = txtPhone.Text;
            s.address = txtAddress.Text;
            s.birth = dateTimePicker1.Value;
            if (cbbGender.SelectedIndex == 0)
            {
                s.gender = true;
            }
            else
            {
                s.gender = false;
            }
            s.vaccination = Convert.ToInt32(cbbDoes.SelectedItem.ToString());
            return s;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtCMND.Text == "" || txtFullname.Text == "" || txtAddress.Text == "" || txtPhone.Text == "" || cbbGender.Text == "" || cbbDoes.Text == "")
            {
                MessageBox.Show("Please fill in all the information.", "NOTICE");
            }
            else
            {
                Citizen s = GetData();
                if (txtCMND.Enabled == true)
                {
                    if (Provider.Instance.CheckDuplicateCMND(s.CMND_CCCD) == false)
                    {
                        MessageBox.Show("CMND/CCCD already existed!", "NOTICE");
                    }
                    else
                    {
                        Provider.Instance.ExecuteAdd(s, CMND_CCCD);
                        MessageBox.Show("Add successfully, default account for this citizen:\nUsername: " + s.CMND_CCCD + "\nPassword: " + s.CMND_CCCD, "NOTICE");
                        this.Close();
                    }
                }
                else
                {
                    Provider.Instance.ExecuteEdit(s, CMND_CCCD);
                    this.Close();
                }
                d("", "", "");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
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
