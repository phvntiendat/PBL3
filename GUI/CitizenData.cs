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
    public partial class CitizenData : Form
    {
        public CitizenData()
        {
            InitializeComponent();
            ShowDGV("", "", "");
            InitCBB();
            cbbAddress.SelectedIndex = 0;
        }
        public void InitCBB()
        {
            cbbAddress.Items.Add("All");
            cbbDoes.Items.Add("All");
            cbbAddress.Items.AddRange(Provider.Instance.GetCBB_Address().ToArray());
            cbbDoes.Items.AddRange(Provider.Instance.GetCBB_Does().ToArray());
            cbbSort.Items.Add("Full Name");
            cbbSort.Items.Add("CMND/CCCD");
            cbbSort.Items.Add("Date of Birth");
            cbbSort.Items.Add("Number of Doses");
            cbbSort.SelectedIndex = 0;
        }
        public void ShowDGV(string txt, string Address = "", string Does = "")
        {
            dgv.DataSource = Provider.Instance.CitizenFilteredViews(txt, Address, Does).ToArray();
            // Modify DGVs Appearance
            dgv.Columns[0].HeaderText = "CMND/CCCD";
            dgv.Columns[1].HeaderText = "Full Name";
            dgv.Columns[2].HeaderText = "Gender";
            dgv.Columns[3].HeaderText = "Date of Birth";
            dgv.Columns[4].HeaderText = "Phone Number";
            dgv.Columns[5].HeaderText = "Address";
            dgv.Columns[6].HeaderText = "Current Vaccination Status";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            CitizenAddEdit f = new CitizenAddEdit("");
            f.d = new CitizenAddEdit.MyDelegate(ShowDGV);
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedCells.Count == 1)
            {
                dgv.Rows[dgv.SelectedCells[0].RowIndex].Selected = true;
                string CMND = dgv.SelectedRows[0].Cells["CMND_CCCD"].Value.ToString();
                CitizenAddEdit f = new CitizenAddEdit(CMND);
                f.d = new CitizenAddEdit.MyDelegate(ShowDGV);
                f.Show();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will permanently remove selected citizen's data, but not remove this citizen's account!", "NOTICE", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (dgv.SelectedCells.Count > 0)
                {
                    dgv.Rows[dgv.SelectedCells[0].RowIndex].Selected = true;
                    string CMND = "";
                    foreach (DataGridViewRow i in dgv.SelectedRows)
                    {
                        CMND = i.Cells["CMND_CCCD"].Value.ToString();
                    }
                    Provider.Instance.DeleteCitizen_BLL(CMND);
                }
                ShowDGV(txtSearch.Text, cbbAddress.SelectedItem.ToString(), cbbDoes.SelectedItem.ToString());
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ShowDGV(txtSearch.Text, cbbAddress.SelectedItem.ToString(), cbbDoes.SelectedItem.ToString());
        }

        private void cbbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbDoes.SelectedItem == null)
            {
                cbbDoes.SelectedIndex = 0;
            }
            ShowDGV(txtSearch.Text, cbbAddress.SelectedItem.ToString(), cbbDoes.SelectedItem.ToString());
        }

        private void cbbDoes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowDGV(txtSearch.Text, cbbAddress.SelectedItem.ToString(), cbbDoes.SelectedItem.ToString());
        }
        public bool SortingDirection = true;
        private void btnSort_Click(object sender, EventArgs e)
        {
            dgv.DataSource = Provider.Instance.Sort_BLL(txtSearch.Text, cbbAddress.SelectedItem.ToString(), cbbDoes.SelectedItem.ToString(), cbbSort.SelectedIndex, SortingDirection);
            SortingDirection = !SortingDirection;
        }

        private void cbbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            SortingDirection = true;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                dgv.SelectAll();
                DataObject dataObj = dgv.GetClipboardContent();
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
