using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public partial class MeasuredGridPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private string m_Title = "间隔";

        public event OnValueChangeEventHandler OnValueChange;

        public MeasuredGridPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                (GridAxisPropertyPage.m_pMapGrid as IMeasuredGrid).Units = (esriUnits) this.cboMapUnit.SelectedIndex;
                try
                {
                    (GridAxisPropertyPage.m_pMapGrid as IMeasuredGrid).XIntervalSize = double.Parse(this.txtXSpace.Text);
                }
                catch
                {
                }
                try
                {
                    (GridAxisPropertyPage.m_pMapGrid as IMeasuredGrid).YIntervalSize = double.Parse(this.txtYSpace.Text);
                }
                catch
                {
                }
                try
                {
                    (GridAxisPropertyPage.m_pMapGrid as IMeasuredGrid).XOrigin = double.Parse(this.txtOriginX.Text);
                }
                catch
                {
                }
                try
                {
                    (GridAxisPropertyPage.m_pMapGrid as IMeasuredGrid).YOrigin = double.Parse(this.txtOriginY.Text);
                }
                catch
                {
                }
                (GridAxisPropertyPage.m_pMapGrid as IMeasuredGrid).FixedOrigin = this.rdoOriginType.SelectedIndex == 1;
            }
        }

        public void Cancel()
        {
        }

        public void Hide()
        {
        }

        private void MeasuredGridPropertyPage_Load(object sender, EventArgs e)
        {
            if (GridAxisPropertyPage.m_pMapGrid is IMeasuredGrid)
            {
                this.txtXSpace.Text = (GridAxisPropertyPage.m_pMapGrid as IMeasuredGrid).XIntervalSize.ToString();
                this.txtYSpace.Text = (GridAxisPropertyPage.m_pMapGrid as IMeasuredGrid).YIntervalSize.ToString();
                this.cboMapUnit.SelectedIndex = (int) (GridAxisPropertyPage.m_pMapGrid as IMeasuredGrid).Units;
                this.txtOriginX.Text = (GridAxisPropertyPage.m_pMapGrid as IMeasuredGrid).XOrigin.ToString();
                this.txtOriginY.Text = (GridAxisPropertyPage.m_pMapGrid as IMeasuredGrid).YOrigin.ToString();
                this.rdoOriginType.SelectedIndex = (GridAxisPropertyPage.m_pMapGrid as IMeasuredGrid).FixedOrigin
                    ? 1
                    : 0;
                this.m_CanDo = true;
            }
        }

        private void rdoOriginType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtOriginX.Enabled = this.rdoOriginType.SelectedIndex == 1;
            this.txtOriginY.Enabled = this.rdoOriginType.SelectedIndex == 1;
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        public void SetObjects(object @object)
        {
        }

        private void txtOriginX_EditValueChanged(object sender, EventArgs e)
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

        private void txtOriginY_EditValueChanged(object sender, EventArgs e)
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

        private void txtXSpace_EditValueChanged(object sender, EventArgs e)
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

        private void txtYSpace_EditValueChanged(object sender, EventArgs e)
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

        public bool IsPageDirty
        {
            get { return this.m_IsPageDirty; }
        }

        int IPropertyPage.Height
        {
            get { return base.Height; }
        }

        int IPropertyPage.Width
        {
            get { return base.Width; }
        }

        public string Title
        {
            get { return this.m_Title; }
            set { this.m_Title = value; }
        }
    }
}