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

namespace PBL3.GUI
{
    public partial class RegistrationData : Form
    {
        private bool sortingDirection = false;
        public RegistrationData()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(55, 54, 92);
            initCBB();
            showDGV("All", "All", "All", "All");
            dgvRegistration.Columns[6].Visible = false;
            Provider.Instance.SyncRegistration();
        }

        private void showDGV(string cmnd_cccd, string vaccineName = "", string state = "", string dose = "")
        {
            dgvRegistration.DataSource = Provider.Instance.RegistrationFilterViews(cmnd_cccd, vaccineName, state, dose).ToArray();
            // Modify DGVs Appearance
            dgvRegistration.Columns[0].HeaderText = "ID";
            dgvRegistration.Columns[1].HeaderText = "CMND/CCCD";
            dgvRegistration.Columns[2].HeaderText = "Dose";
            dgvRegistration.Columns[3].HeaderText = "Vaccine Name";
            dgvRegistration.Columns[4].HeaderText = "Registration Date";
            dgvRegistration.Columns[5].HeaderText = "Vaccination State";
        }
        private void initCBB()
        {
            cbbVaccinationState.Items.Add("All");
            cbbVaccineName.Items.Add("All");

            cbbVaccinationState.Items.AddRange(Provider.Instance.GetCBB_VaccinationState().ToArray());
            cbbVaccineName.Items.AddRange(Provider.Instance.GetCBB_VaccineName().ToArray());

            cbbVaccinationState.SelectedIndex = 0;
            cbbVaccineName.SelectedIndex = 0;

            cbbSort.Items.AddRange(new string[] { "Registration ID", "CMND/CCCD", "Dose", "Registration Date", "State" });
            cbbSort.SelectedIndex = 0;

            cbbDose.Items.Add("All");
            cbbDose.Items.Add("0");
            cbbDose.Items.Add("1");
            cbbDose.Items.Add("2");
            cbbDose.Items.Add("3");
            cbbDose.Items.Add("4");
            cbbDose.SelectedIndex = 0;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
        private void ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbbVaccineName.SelectedItem == null || cbbDose.SelectedItem == null)
                {
                    showDGV(txtSearch.Text, "All", cbbVaccinationState.SelectedItem.ToString(), "All");

                }
                else
                {
                    showDGV(txtSearch.Text, cbbVaccineName.SelectedItem.ToString(), cbbVaccinationState.SelectedItem.ToString(), cbbDose.SelectedItem.ToString());
                }
            }
            catch (Exception e1)
            {

            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvRegistration.SelectedCells.Count > 0)
            {
                dgvRegistration.Rows[dgvRegistration.SelectedCells[0].RowIndex].Selected = true;
                string regisID = "";
                foreach (DataGridViewRow i in dgvRegistration.SelectedRows)
                {
                    regisID = i.Cells["regisId"].Value.ToString();
                }
                RegistrationAddEdit s = new RegistrationAddEdit(regisID);
                Provider.Instance.SyncRegistration();
                s.d = new RegistrationAddEdit.MyDelegate(showDGV);
                s.Show();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will permanently remove selected registration data!", "NOTICE", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (dgvRegistration.SelectedCells.Count == 1)
                {
                    dgvRegistration.Rows[dgvRegistration.SelectedCells[0].RowIndex].Selected = true;
                    string regisID = "";
                    foreach (DataGridViewRow i in dgvRegistration.SelectedRows)
                    {
                        regisID = i.Cells["regisId"].Value.ToString();
                    }
                    Provider.Instance.DeleteRegistration_BLL(regisID);
                }
                showDGV(txtSearch.Text, cbbVaccineName.SelectedItem.ToString(), cbbVaccinationState.SelectedItem.ToString(), cbbDose.SelectedItem.ToString());
            }
        }
        private void btnSort_Click(object sender, EventArgs e)
        {
            if (cbbSort.SelectedItem != null || cbbSort.SelectedItem.ToString() != "")
            {
                dgvRegistration.DataSource = Provider.Instance.Sort_BLL(cbbSort.SelectedItem.ToString(), sortingDirection, txtSearch.Text, cbbVaccineName.SelectedItem.ToString(), cbbVaccinationState.SelectedItem.ToString(), cbbDose.SelectedItem.ToString());
                sortingDirection = !sortingDirection;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                dgvRegistration.SelectAll();
                DataObject dataObj = dgvRegistration.GetClipboardContent();
                if (dataObj != null)
                {
                    Clipboard.SetDataObject(dataObj);
                }
                Provider.Instance.load_Excel_App();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString(), "Error!");
                Console.WriteLine(e1.ToString());
            }
        }
    }
}

