using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal partial class LabelConficPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private IBasicOverposterLayerProperties m_pOverposterLayerProperties = null;

        public event OnValueChangeEventHandler OnValueChange;

        public LabelConficPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                this.m_pOverposterLayerProperties.FeatureWeight =
                    (esriBasicOverposterWeight) this.cboFeatureType.SelectedIndex;
                this.m_pOverposterLayerProperties.LabelWeight =
                    (esriBasicOverposterWeight) (this.cboLabelWeight.SelectedIndex - 1);
                double num = 0.0;
                try
                {
                    num = double.Parse(this.txtBufferRatio.Text);
                    if ((num >= 0.0) && (num <= 1.0))
                    {
                        this.m_pOverposterLayerProperties.BufferRatio = num;
                    }
                }
                catch
                {
                }
            }
        }

        public void Cancel()
        {
            this.m_IsPageDirty = false;
        }

        private void cboFeatureType_SelectedIndexChanged(object sender, EventArgs e)
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

        private void cboLabelWeight_SelectedIndexChanged(object sender, EventArgs e)
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

        public void Hide()
        {
        }

        private void LabelConficPropertyPage_Load(object sender, EventArgs e)
        {
            if (this.m_pOverposterLayerProperties != null)
            {
                this.cboFeatureType.SelectedIndex = (int) this.m_pOverposterLayerProperties.FeatureWeight;
                this.cboLabelWeight.SelectedIndex = ((int) this.m_pOverposterLayerProperties.LabelWeight) - 1;
                this.txtBufferRatio.Text = this.m_pOverposterLayerProperties.BufferRatio.ToString();
                this.m_CanDo = true;
            }
        }

        public void SetObjects(object @object)
        {
            this.m_pOverposterLayerProperties = @object as IBasicOverposterLayerProperties;
        }

        private void txtBufferRatio_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
                double num = 0.0;
                try
                {
                    num = double.Parse(this.txtBufferRatio.Text);
                    if ((num >= 0.0) && (num <= 1.0))
                    {
                        this.txtBufferRatio.ForeColor = Color.Black;
                    }
                    else
                    {
                        this.txtBufferRatio.ForeColor = Color.Red;
                    }
                }
                catch
                {
                    this.txtBufferRatio.ForeColor = Color.Red;
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
            get { return "冲突检测"; }
            set { }
        }
    }
}