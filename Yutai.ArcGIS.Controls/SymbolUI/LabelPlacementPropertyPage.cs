using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal partial class LabelPlacementPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private IBasicOverposterLayerProperties4 m_pOverposterLayerProperties = null;

        public event OnValueChangeEventHandler OnValueChange;

        public LabelPlacementPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                this.pointFeatureLabelCtrl1.Apply();
                this.lineFeaturePlaceSetCtrl1.Apply();
                this.fillFeaturePlaceCtrl1.Apply();
                this.m_pOverposterLayerProperties.FeatureType =
                    (esriBasicOverposterFeatureType) this.cboFeatureType.SelectedIndex;
                this.m_pOverposterLayerProperties.NumLabelsOption =
                    (esriBasicNumLabelsOption) (this.radioGroup1.SelectedIndex + 1);
            }
        }

        public void Cancel()
        {
            this.m_IsPageDirty = false;
        }

        private void cboFeatureType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.cboFeatureType.SelectedIndex)
            {
                case 0:
                    this.groupBox4.Enabled = false;
                    this.groupBoxPoint.Visible = true;
                    this.groupBoxLine.Visible = false;
                    this.groupBoxFill.Visible = false;
                    break;

                case 1:
                    this.groupBox4.Enabled = true;
                    this.groupBoxPoint.Visible = false;
                    this.groupBoxLine.Visible = true;
                    this.groupBoxFill.Visible = false;
                    break;

                case 2:
                    this.groupBox4.Enabled = true;
                    this.groupBoxPoint.Visible = false;
                    this.groupBoxLine.Visible = false;
                    this.groupBoxFill.Visible = true;
                    break;
            }
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

        private void LabelPlacementPropertyPage_Load(object sender, EventArgs e)
        {
            this.pointFeatureLabelCtrl1.OnValueChange +=
                new OnValueChangeEventHandler(this.pointFeatureLabelCtrl1_OnValueChange);
            this.lineFeaturePlaceSetCtrl1.OnValueChange +=
                new OnValueChangeEventHandler(this.pointFeatureLabelCtrl1_OnValueChange);
            this.fillFeaturePlaceCtrl1.OnValueChange +=
                new OnValueChangeEventHandler(this.pointFeatureLabelCtrl1_OnValueChange);
            this.cboFeatureType.SelectedIndex = (int) this.m_pOverposterLayerProperties.FeatureType;
            this.radioGroup1.SelectedIndex = ((int) this.m_pOverposterLayerProperties.NumLabelsOption) - 1;
            this.cboFeatureType_SelectedIndexChanged(this, e);
            this.m_CanDo = true;
        }

        private void pointFeatureLabelCtrl1_OnValueChange()
        {
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
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

        public void SetObjects(object @object)
        {
            this.m_pOverposterLayerProperties = @object as IBasicOverposterLayerProperties4;
            this.pointFeatureLabelCtrl1.SetObjects(@object);
            this.lineFeaturePlaceSetCtrl1.SetObjects(@object);
            this.fillFeaturePlaceCtrl1.SetObjects(@object);
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
            get { return "放置"; }
            set { }
        }
    }
}