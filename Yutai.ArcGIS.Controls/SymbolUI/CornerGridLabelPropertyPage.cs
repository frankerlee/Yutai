using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public partial class CornerGridLabelPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private ICornerGridLabel m_pCornerGridLabel = null;
        private string m_Title = "角标";

        public event OnValueChangeEventHandler OnValueChange;

        public CornerGridLabelPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                this.m_pCornerGridLabel.set_CornerLabel(esriGridCornerEnum.esriGridCornerLowerLeft, this.chkLowerLeft.Checked);
                this.m_pCornerGridLabel.set_CornerLabel(esriGridCornerEnum.esriGridCornerLowerRight, this.chkLowerRight.Checked);
                this.m_pCornerGridLabel.set_CornerLabel(esriGridCornerEnum.esriGridCornerUpperLeft, this.chkUpperLeft.Checked);
                this.m_pCornerGridLabel.set_CornerLabel(esriGridCornerEnum.esriGridCornerUpperRight, this.chkUpperRight.Checked);
            }
        }

        public void Cancel()
        {
        }

        private void chkLowerLeft_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkLowerRight_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkUpperLeft_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkUpperRight_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void CornerGridLabelPropertyPage_Load(object sender, EventArgs e)
        {
            this.chkLowerLeft.Checked = this.m_pCornerGridLabel.get_CornerLabel(esriGridCornerEnum.esriGridCornerLowerLeft);
            this.chkLowerRight.Checked = this.m_pCornerGridLabel.get_CornerLabel(esriGridCornerEnum.esriGridCornerLowerRight);
            this.chkUpperLeft.Checked = this.m_pCornerGridLabel.get_CornerLabel(esriGridCornerEnum.esriGridCornerUpperLeft);
            this.chkUpperRight.Checked = this.m_pCornerGridLabel.get_CornerLabel(esriGridCornerEnum.esriGridCornerUpperRight);
            this.m_CanDo = true;
        }

 public void Hide()
        {
        }

 public void SetObjects(object @object)
        {
            this.m_pCornerGridLabel = @object as ICornerGridLabel;
        }

        public bool IsPageDirty
        {
            get
            {
                return this.m_IsPageDirty;
            }
        }

        int IPropertyPage.Height
        {
            get
            {
                return base.Height;
            }
        }

        int IPropertyPage.Width
        {
            get
            {
                return base.Width;
            }
        }

        public string Title
        {
            get
            {
                return this.m_Title;
            }
            set
            {
            }
        }
    }
}

