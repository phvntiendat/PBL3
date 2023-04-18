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
using PBL3.DTO;

namespace PBL3.GUI
{
    public partial class VaccineData : Form
    {
        public VaccineData()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(55, 54, 92);
            ShowDGV("", "");
            InitCBB();
            cbbFilter.SelectedIndex = 0;
        }

        public void InitCBB()
        {
            cbbFilter.Items.Add("All");
            cbbFilter.Items.AddRange(Provider.Instance.GetCBB_Filter().ToArray());
            cbbSort.Items.Add("Vaccine Name");
            cbbSort.Items.Add("Quantity");
            cbbSort.SelectedIndex = 0;
        }
        public void ShowDGV(string txt, string search = "")
        {
            dgv.DataSource = BLL.Provider.Instance.VaccineFilteredViews(txt, search).ToArray();
            // Modify DGVs Appearance
            dgv.Columns[0].HeaderText = "Vaccine Name";
            dgv.Columns[1].HeaderText = "Quantity";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            VaccineAddEdit f = new VaccineAddEdit("");
            f.d = new VaccineAddEdit.MyDelegate(ShowDGV);
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 1)
            {
                string name = dgv.SelectedRows[0].Cells["vaccineName"].Value.ToString();
                VaccineAddEdit f = new VaccineAddEdit(name);
                f.d = new VaccineAddEdit.MyDelegate(ShowDGV);
                f.Show();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string vaccine_name = dgv.Rows[dgv.CurrentRow.Index].Cells["vaccineName"].Value.ToString();
            bool check_del = true;

            foreach (Registration r in Provider.Instance.GetAll_Registration())
            {
                if (r.vaccineName == vaccine_name)
                {
                    check_del = false;
                    break;
                }
            }

            if (check_del)
            {
                if (MessageBox.Show("This will permanently remove selected vaccine!", "NOTICE", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (dgv.SelectedRows.Count > 0)
                    {
                        string name = "";
                        foreach (DataGridViewRow i in dgv.SelectedRows)
                        {
                            name = i.Cells["vaccineName"].Value.ToString();
                        }
                        Provider.Instance.Delete_BLL_Vaccine(name);
                    }
                    ShowDGV(cbbFilter.SelectedItem.ToString(), txtSearch.Text);
                }
            }
            else
            {
                MessageBox.Show("There are currently registered users for this vaccine!" + "\n" +
                    "Please do not delete!", "NOTICE");
            }
        }

        public bool SortingDirection = true;
        private void btnSort_Click(object sender, EventArgs e)
        {
            dgv.DataSource = Provider.Instance.Sort_BLL(txtSearch.Text, cbbSort.SelectedIndex, SortingDirection);
            if (SortingDirection == true)
            {
                SortingDirection = false;
            }
            else
            {
                SortingDirection = true;
            }
        }

        private void cbbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowDGV(cbbFilter.SelectedItem.ToString(), txtSearch.Text);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ShowDGV(cbbFilter.SelectedItem.ToString(), txtSearch.Text);
        }
    }
}

