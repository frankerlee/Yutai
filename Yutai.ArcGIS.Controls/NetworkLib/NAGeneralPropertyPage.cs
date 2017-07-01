using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    internal partial class NAGeneralPropertyPage : UserControl
    {
        private bool m_CanDo = false;
        private bool m_IsDirty = false;

        public NAGeneralPropertyPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.m_IsDirty)
            {
                this.m_IsDirty = false;
                NetworkAnalyst.TraceIndeterminateFlow = this.checkEdit.Checked;
                try
                {
                    double num = double.Parse(this.txtSnapTol.Text);
                    if (num > 0.0)
                    {
                        NetworkAnalyst.SnapTolerance = num;
                    }
                }
                catch
                {
                }
            }
            return true;
        }

        private void checkEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsDirty = true;
            }
        }

        private void Init()
        {
            this.m_CanDo = false;
            this.txtSnapTol.Text = NetworkAnalyst.SnapTolerance.ToString();
            this.checkEdit.Checked = NetworkAnalyst.TraceIndeterminateFlow;
            this.m_CanDo = true;
        }

        private void NAGeneralPropertyPage_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void rdoTrackFeatureSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_IsDirty = true;
        }

        private void txtSnapTol_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                try
                {
                    if (double.Parse(this.txtSnapTol.Text) > 0.0)
                    {
                        this.txtSnapTol.ForeColor = Color.Black;
                        this.m_IsDirty = true;
                    }
                    else
                    {
                        this.txtSnapTol.ForeColor = Color.Red;
                    }
                }
                catch
                {
                    this.txtSnapTol.ForeColor = Color.Red;
                }
            }
        }
    }
}