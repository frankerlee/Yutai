using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.Controls.Export
{
    public class ExportToExcelWizard : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnLast;
        private SimpleButton btnNext;
        private Container components = null;
        private GroupBox groupBox1;
        private ExportToExcelSelectData m_pExcelSelectData = new ExportToExcelSelectData();
        private ExportToExcelSet m_pExportToExcelSet = new ExportToExcelSet();
        private int m_Step = 0;
        private Panel panel1;

        public ExportToExcelWizard()
        {
            this.InitializeComponent();
            ExportToExcelHelper.Init();
            this.m_pExcelSelectData.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(this.m_pExcelSelectData);
            this.m_pExportToExcelSet.Dock = DockStyle.Fill;
            this.m_pExportToExcelSet.Visible = false;
            this.panel1.Controls.Add(this.m_pExportToExcelSet);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            switch (this.m_Step)
            {
                case 0:
                    return;

                case 1:
                    this.m_pExcelSelectData.Visible = true;
                    this.m_pExportToExcelSet.Visible = false;
                    this.btnNext.Text = "下一步";
                    this.btnLast.Enabled = false;
                    break;
            }
            this.m_Step--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            switch (this.m_Step)
            {
                case 0:
                    if (!this.m_pExcelSelectData.Do())
                    {
                        return;
                    }
                    this.m_pExcelSelectData.Visible = false;
                    this.m_pExportToExcelSet.Visible = true;
                    this.btnNext.Text = "完成";
                    this.btnLast.Enabled = true;
                    break;

                case 1:
                    if (this.m_pExportToExcelSet.Do())
                    {
                        base.DialogResult = DialogResult.OK;
                    }
                    return;
            }
            this.m_Step++;
        }

        private void ExportToExcelWizard_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportToExcelWizard));
            this.panel1 = new Panel();
            this.groupBox1 = new GroupBox();
            this.btnLast = new SimpleButton();
            this.btnNext = new SimpleButton();
            this.btnCancel = new SimpleButton();
            base.SuspendLayout();
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x178, 0xd8);
            this.panel1.TabIndex = 0;
            this.groupBox1.Location = new Point(0, 0xd8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x170, 8);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.btnLast.Location = new Point(0x98, 0xe8);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(0x40, 0x18);
            this.btnLast.TabIndex = 2;
            this.btnLast.Text = "上一步";
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.btnNext.Location = new Point(0xe0, 0xe8);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(0x40, 0x18);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "下一步";
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x128, 0xe8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x178, 0x10d);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnNext);
            base.Controls.Add(this.btnLast);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.panel1);
            base.Icon = (Icon) resources.GetObject("$this.Icon");
            base.Name = "ExportToExcelWizard";
            this.Text = "导出数据到Excel向导";
            base.Load += new EventHandler(this.ExportToExcelWizard_Load);
            base.ResumeLayout(false);
        }

        public IMap Map
        {
            set
            {
                this.m_pExcelSelectData.Map = value;
            }
        }
    }
}

