
namespace PBL3
{
    partial class VaccineRegistration
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VaccineRegistration));
            this.label3 = new System.Windows.Forms.Label();
            this.cbbVaccineType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDose = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRegister = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDosesInjected = new System.Windows.Forms.TextBox();
            this.txtPreDay = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgv = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(58, 299);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 19);
            this.label3.TabIndex = 56;
            this.label3.Text = "Choose Vaccine Type";
            // 
            // cbbVaccineType
            // 
            this.cbbVaccineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbVaccineType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbbVaccineType.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.cbbVaccineType.FormattingEnabled = true;
            this.cbbVaccineType.Location = new System.Drawing.Point(62, 320);
            this.cbbVaccineType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbbVaccineType.Name = "cbbVaccineType";
            this.cbbVaccineType.Size = new System.Drawing.Size(239, 25);
            this.cbbVaccineType.TabIndex = 80;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(58, 167);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 19);
            this.label1.TabIndex = 108;
            this.label1.Text = "New Dose";
            // 
            // txtDose
            // 
            this.txtDose.BackColor = System.Drawing.SystemColors.Window;
            this.txtDose.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDose.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.txtDose.Location = new System.Drawing.Point(62, 188);
            this.txtDose.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtDose.Multiline = true;
            this.txtDose.Name = "txtDose";
            this.txtDose.Size = new System.Drawing.Size(239, 25);
            this.txtDose.TabIndex = 109;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.label2.Location = new System.Drawing.Point(58, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 21);
            this.label2.TabIndex = 112;
            this.label2.Text = "Register for Vaccination";
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(87)))), ((int)(((byte)(201)))));
            this.btnRegister.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRegister.BackgroundImage")));
            this.btnRegister.FlatAppearance.BorderSize = 0;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(62, 374);
            this.btnRegister.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(239, 25);
            this.btnRegister.TabIndex = 113;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(58, 102);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(184, 19);
            this.label5.TabIndex = 114;
            this.label5.Text = "Number of Doses Injected";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(58, 231);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(186, 19);
            this.label6.TabIndex = 116;
            this.label6.Text = "Previous Registration Date";
            // 
            // txtDosesInjected
            // 
            this.txtDosesInjected.BackColor = System.Drawing.SystemColors.Window;
            this.txtDosesInjected.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDosesInjected.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.txtDosesInjected.Location = new System.Drawing.Point(62, 123);
            this.txtDosesInjected.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtDosesInjected.Multiline = true;
            this.txtDosesInjected.Name = "txtDosesInjected";
            this.txtDosesInjected.Size = new System.Drawing.Size(239, 25);
            this.txtDosesInjected.TabIndex = 118;
            // 
            // txtPreDay
            // 
            this.txtPreDay.BackColor = System.Drawing.SystemColors.Window;
            this.txtPreDay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPreDay.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.txtPreDay.Location = new System.Drawing.Point(62, 252);
            this.txtPreDay.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtPreDay.Multiline = true;
            this.txtPreDay.Name = "txtPreDay";
            this.txtPreDay.Size = new System.Drawing.Size(239, 25);
            this.txtPreDay.TabIndex = 109;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.label4.Location = new System.Drawing.Point(361, 46);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 21);
            this.label4.TabIndex = 122;
            this.label4.Text = "Vaccination History";
            // 
            // dgv
            // 
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(365, 123);
            this.dgv.Margin = new System.Windows.Forms.Padding(2);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersWidth = 51;
            this.dgv.RowTemplate.Height = 24;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(280, 276);
            this.dgv.TabIndex = 121;
            // 
            // VaccineRegistration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(54)))), ((int)(((byte)(92)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(701, 438);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.txtDosesInjected);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPreDay);
            this.Controls.Add(this.txtDose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbbVaccineType);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "VaccineRegistration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VaccineRegistration";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbVaccineType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDosesInjected;
        private System.Windows.Forms.TextBox txtPreDay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgv;
    }
}