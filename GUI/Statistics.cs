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
    public partial class Statistics : Form
    {
        public Statistics()
        {
            InitializeComponent();
            InitCBB();
            GUI();
        }
        public void InitCBB()
        {
            cbbType.Items.Add("Age");
            cbbType.Items.Add("Dose");
            cbbType.Items.Add("Gender");
            cbbType.SelectedIndex = 0;
        }
        public void GUI()
        {
            chartDose.Series["Dose"].Points.AddXY("0", Provider.Instance.DoseCounter(0));
            chartDose.Series["Dose"].Points.AddXY("1", Provider.Instance.DoseCounter(1));
            chartDose.Series["Dose"].Points.AddXY("2", Provider.Instance.DoseCounter(2));
            chartDose.Series["Dose"].Points.AddXY("3", Provider.Instance.DoseCounter(3));
            chartDose.Series["Dose"].Points.AddXY("4", Provider.Instance.DoseCounter(4));

            chartAge.Series["Age"].Points.AddXY("5-12", Provider.Instance.AgeCounter(5, 12));
            chartAge.Series["Age"].Points.AddXY("13-40", Provider.Instance.AgeCounter(13, 40));
            chartAge.Series["Age"].Points.AddXY("41-85", Provider.Instance.AgeCounter(41, 85));

            chartGender.Series["Gender"].Points.AddXY("Male", Provider.Instance.GenderCounter(true));
            chartGender.Series["Gender"].Points.AddXY("Female", Provider.Instance.GenderCounter(false)); 
            //format charts
            chartDose.Series["Dose"].IsValueShownAsLabel = true;
            chartAge.Series["Age"].IsValueShownAsLabel = true;
            chartGender.Series["Gender"].IsValueShownAsLabel = true;

            chartDose.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gainsboro;
            chartDose.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gainsboro;

            chartAge.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gainsboro;
            chartAge.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gainsboro;

            chartGender.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gainsboro;
            chartGender.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gainsboro;

        }

        private void cbbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbType.SelectedItem == "Age")
            {
                chartAge.Visible = true;
                chartDose.Visible = false;
                chartGender.Visible = false;
            }
            if (cbbType.SelectedItem == "Dose")
            {
                chartAge.Visible = false;
                chartDose.Visible = true;
                chartGender.Visible = false;
            }
            if(cbbType.SelectedItem == "Gender")
            {
                chartAge.Visible = false;
                chartDose.Visible = false;
                chartGender.Visible = true;
            }
        }
    }
}
