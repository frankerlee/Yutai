using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Output;

namespace Yutai.ArcGIS.Controls.Controls.Export
{
    internal partial class ExportMapGeneralPropertyPage : UserControl
    {
        private bool m_CanDo = false;
        private IExport m_pExport = null;

        public ExportMapGeneralPropertyPage()
        {
            this.InitializeComponent();
        }

        private void ExportMapGeneralPropertyPage_Load(object sender, EventArgs e)
        {
            this.txtResolution.Value = (decimal) this.m_pExport.Resolution;
            if (this.m_pExport is IOutputRasterSettings)
            {
                this.panel1.Visible = true;
                this.txtResampleRatio.Value = (this.m_pExport as IOutputRasterSettings).ResampleRatio;
            }
            else
            {
                this.panel1.Visible = false;
            }
            this.m_CanDo = true;
        }

        private void txtResampleRatio_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                try
                {
                    double num = (double) this.txtResampleRatio.Value;
                    if ((num > 0.0) && (num < 6.0))
                    {
                        (this.m_pExport as IOutputRasterSettings).ResampleRatio = (int) num;
                    }
                }
                catch
                {
                }
            }
        }

        private void txtResolution_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                try
                {
                    double num = (double) this.txtResolution.Value;
                    if (num > 0.0)
                    {
                        this.m_pExport.Resolution = num;
                    }
                }
                catch
                {
                }
            }
        }

        public IExport Export
        {
            set { this.m_pExport = value; }
        }
    }
}