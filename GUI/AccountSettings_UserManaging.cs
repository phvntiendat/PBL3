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

namespace PBL3
{
    public partial class AccountSettings_UserMnaging : Form
    {
        public AccountSettings_UserMnaging()
        {
            InitializeComponent();
            initCBB();
            cbbSort.SelectedIndex = 0;
            //ShowDGV("All","");
        }
        private void initCBB()
        {
            cbbSort.Items.Add("All");
            cbbSort.Items.AddRange(Provider.Instance.GetCBB_Permission().ToArray());
        }
        private void ShowDGV(string _permission, string _username)
        {
            dgv.DataSource = BLL.Provider.Instance.AccountFilteredViews(_permission,_username).ToArray();
            // Modify DGVs Appearance
            dgv.Columns[0].HeaderText = "CMND/CCCD";
            dgv.Columns[1].HeaderText = "Username";
            dgv.Columns[2].HeaderText = "Password";
            dgv.Columns[3].HeaderText = "Fullname";
            dgv.Columns[4].HeaderText = "Permission";
        }
        private void FilterChanged(object sender, EventArgs e)
        {
            //string _permission = cbbSort.SelectedItem.ToString();
            //string _username;
            //if (txtSearch.Text == "" || txtSearch.Text == null)
            //{
            //    _username = "";
            //}else
            //{
            //    _username = txtSearch.Text;
            //}
            //dgv.DataSource = Provider.Instance.AccountFilteredViews(_permission, _username).ToArray();
            ShowDGV(cbbSort.SelectedItem.ToString(), txtSearch.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This action will permanently remove selected account and all data in this account!", "NOTICE", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (dgv.SelectedCells.Count > 0)
                {
                    dgv.Rows[dgv.SelectedCells[0].RowIndex].Selected = true;
                    string CMND = "";
                    foreach (DataGridViewRow i in dgv.SelectedRows)
                    {
                        CMND = i.Cells["CMND_CCCD"].Value.ToString();
                    }
                    Provider.Instance.DeleteAccount_BLL(CMND);
                }
                ShowDGV("All", "");
            }
            else
            {

            }
        }
    }
}
