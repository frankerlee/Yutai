using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    public class frmNASheet : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOk;
        private Container components = null;
        private NAGeneralPropertyPage naGeneralPropertyPage1;
        private NAResultSetPropertyPage naResultSetPropertyPage1;
        private NAWeightFilterPropertyPage naWeightFilterPropertyPage1;
        private NAWeightsPropertyPage naWeightsPropertyPage1;
        private Panel panel1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;

        public frmNASheet()
        {
            this.InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if ((this.naGeneralPropertyPage1.Apply() && this.naWeightsPropertyPage1.Apply()) && this.naWeightFilterPropertyPage1.Apply())
            {
                base.Close();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmNASheet_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNASheet));
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.naGeneralPropertyPage1 = new NAGeneralPropertyPage();
            this.tabPage2 = new TabPage();
            this.naWeightsPropertyPage1 = new NAWeightsPropertyPage();
            this.tabPage3 = new TabPage();
            this.naWeightFilterPropertyPage1 = new NAWeightFilterPropertyPage();
            this.tabPage4 = new TabPage();
            this.naResultSetPropertyPage1 = new NAResultSetPropertyPage();
            this.panel1 = new Panel();
            this.btnCancel = new SimpleButton();
            this.btnOk = new SimpleButton();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x142, 0x18f);
            this.tabControl1.TabIndex = 3;
            this.tabPage1.Controls.Add(this.naGeneralPropertyPage1);
            this.tabPage1.Location = new System.Drawing.Point(4, 0x15);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new Size(0x13a, 0x176);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "常规";
            this.naGeneralPropertyPage1.Dock = DockStyle.Fill;
            this.naGeneralPropertyPage1.Location = new System.Drawing.Point(0, 0);
            this.naGeneralPropertyPage1.Name = "naGeneralPropertyPage1";
            this.naGeneralPropertyPage1.Size = new Size(0x13a, 0x176);
            this.naGeneralPropertyPage1.TabIndex = 0;
            this.tabPage2.Controls.Add(this.naWeightsPropertyPage1);
            this.tabPage2.Location = new System.Drawing.Point(4, 0x15);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new Size(0x13a, 0x176);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "权重";
            this.naWeightsPropertyPage1.Dock = DockStyle.Fill;
            this.naWeightsPropertyPage1.Location = new System.Drawing.Point(0, 0);
            this.naWeightsPropertyPage1.Name = "naWeightsPropertyPage1";
            this.naWeightsPropertyPage1.Size = new Size(0x13a, 0x176);
            this.naWeightsPropertyPage1.TabIndex = 0;
            this.tabPage3.Controls.Add(this.naWeightFilterPropertyPage1);
            this.tabPage3.Location = new System.Drawing.Point(4, 0x15);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new Size(0x13a, 0x176);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "权重过滤";
            this.naWeightFilterPropertyPage1.Dock = DockStyle.Fill;
            this.naWeightFilterPropertyPage1.Location = new System.Drawing.Point(0, 0);
            this.naWeightFilterPropertyPage1.Name = "naWeightFilterPropertyPage1";
            this.naWeightFilterPropertyPage1.Size = new Size(0x13a, 0x176);
            this.naWeightFilterPropertyPage1.TabIndex = 0;
            this.tabPage4.Controls.Add(this.naResultSetPropertyPage1);
            this.tabPage4.Location = new System.Drawing.Point(4, 0x15);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new Size(0x13a, 0x176);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "结果";
            this.naResultSetPropertyPage1.Dock = DockStyle.Fill;
            this.naResultSetPropertyPage1.Location = new System.Drawing.Point(0, 0);
            this.naResultSetPropertyPage1.Name = "naResultSetPropertyPage1";
            this.naResultSetPropertyPage1.Size = new Size(0x13a, 0x176);
            this.naResultSetPropertyPage1.TabIndex = 0;
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 0x16f);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x142, 0x20);
            this.panel1.TabIndex = 4;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0xf8, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnOk.Location = new System.Drawing.Point(0xa8, 6);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new Size(0x40, 0x18);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "确定";
            this.btnOk.Click += new EventHandler(this.btnOk_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x142, 0x18f);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.tabControl1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmNASheet";
            this.Text = "分析选项";
            base.Load += new EventHandler(this.frmNASheet_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }
    }
}

