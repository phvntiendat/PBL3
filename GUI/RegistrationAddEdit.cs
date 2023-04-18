using PBL3.BLL;
using PBL3.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL3
{
    public partial class RegistrationAddEdit : Form
    {
        public delegate void MyDelegate(string txt, string vaccineName, string vaccineState, string dose);
        public MyDelegate d;
        public string regisID;
        public RegistrationAddEdit()
        {
            InitializeComponent();
        }
        public RegistrationAddEdit(string _regisID)
        {
            regisID = _regisID;
            InitializeComponent();
            showData();
            initCBB();
        }
        private void initCBB()
        {
            cbbVaccineName.Items.AddRange(Provider.Instance.GetCBB_AllVaccineName().ToArray());
            cbbVaccineName.Text = Provider.Instance.GetRegistration_By_ID(regisID).vaccineName;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            saveEdit();
            this.Close();
        }
        private void saveEdit()
        {
            Registration r = Provider.Instance.GetRegistration_By_ID(regisID);
            r.vaccineName = cbbVaccineName.Text;
            r.State = txtState.Checked;
            Provider.Instance.ExecuteEdit(r);
            d("", "", "", "");
        }
        private void showData()
        {
            Registration r = Provider.Instance.GetRegistration_By_ID(regisID);
            txtCMND_CCCD.Text = r.CMND_CCCD;
            txtID.Text = r.regisId;
            txtDose.Text = r.Dose.ToString();
            txtRegistDate.Text = r.regisDay.ToString();
            txtCMND_CCCD.Enabled = false;
            txtID.Enabled = false;
            txtDose.Enabled = false;
            txtRegistDate.Enabled = false;
            cbbVaccineName.SelectedItem = r.vaccineName;
            txtState.Checked = r.State;
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
