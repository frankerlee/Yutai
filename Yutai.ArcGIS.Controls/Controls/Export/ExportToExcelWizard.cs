using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.Controls.Export
{
    public partial class ExportToExcelWizard : Form
    {
        private ExportToExcelSelectData m_pExcelSelectData = new ExportToExcelSelectData();
        private ExportToExcelSet m_pExportToExcelSet = new ExportToExcelSet();
        private int m_Step = 0;

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

 public IMap Map
        {
            set
            {
                this.m_pExcelSelectData.Map = value;
            }
        }
    }
}

