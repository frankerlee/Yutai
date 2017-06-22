using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal partial class FillFeaturePlaceCtrl : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private IBasicOverposterLayerProperties4 m_OverLayerProperty = null;

        public event OnValueChangeEventHandler OnValueChange;

        public FillFeaturePlaceCtrl()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                this.m_OverLayerProperty.PolygonPlacementMethod = (esriOverposterPolygonPlacementMethod) this.rdoPolygonPlacementMethod.SelectedIndex;
                this.m_OverLayerProperty.PlaceOnlyInsidePolygon = this.chkPlaceOnlyInsidePolygon.Checked;
            }
        }

        public void Cancel()
        {
            this.m_IsPageDirty = false;
        }

        private void chkPlaceOnlyInsidePolygon_CheckedChanged(object sender, EventArgs e)
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

 private void FillFeaturePlaceCtrl_Load(object sender, EventArgs e)
        {
            if (this.m_OverLayerProperty != null)
            {
                this.rdoPolygonPlacementMethod.SelectedIndex = (int) this.m_OverLayerProperty.PolygonPlacementMethod;
                this.chkPlaceOnlyInsidePolygon.Checked = this.m_OverLayerProperty.PlaceOnlyInsidePolygon;
                this.rdoPolygonPlacementMethod_SelectedIndexChanged(this, e);
                this.m_CanDo = true;
            }
        }

        public void Hide()
        {
        }

 private void rdoPolygonPlacementMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.rdoPolygonPlacementMethod.SelectedIndex)
            {
                case 0:
                    this.pictureEdit1.Visible = true;
                    this.pictureEdit2.Visible = false;
                    this.pictureEdit3.Visible = false;
                    break;

                case 1:
                    this.pictureEdit1.Visible = false;
                    this.pictureEdit2.Visible = true;
                    this.pictureEdit3.Visible = false;
                    break;

                case 2:
                    this.pictureEdit1.Visible = false;
                    this.pictureEdit2.Visible = false;
                    this.pictureEdit3.Visible = true;
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

        public void SetObjects(object @object)
        {
            this.m_OverLayerProperty = @object as IBasicOverposterLayerProperties4;
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
                return "";
            }
            set
            {
            }
        }
    }
}

