using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class LayerDisplaySetCtrl : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;

        public LayerDisplaySetCtrl()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                this.ilayer_0.ShowTips = this.chkShowMapTip.Checked;
                if (this.ilayer_0 is ILayerEffects)
                {
                    try
                    {
                        short num = Convert.ToInt16(this.txtPercent.Text);
                        if ((num >= 0) && (num <= 100))
                        {
                            (this.ilayer_0 as ILayerEffects).Transparency = num;
                        }
                    }
                    catch
                    {
                    }
                }
                if (this.ilayer_0 is IFeatureLayer2)
                {
                    (this.ilayer_0 as IFeatureLayer2).ScaleSymbols = this.chkScaleSymbols.Checked;
                }
                if (this.ilayer_0 is IHotlinkContainer)
                {
                    if (this.cboFields.SelectedIndex > 0)
                    {
                        (this.ilayer_0 as IHotlinkContainer).HotlinkField =
                            (this.cboFields.SelectedItem as FieldWrapEx).Name;
                        (this.ilayer_0 as IHotlinkContainer).HotlinkType =
                            (esriHyperlinkType) this.rdoHyperLinkeType.SelectedIndex;
                    }
                    else
                    {
                        (this.ilayer_0 as IHotlinkContainer).HotlinkField = "";
                    }
                }
            }
            return true;
        }

        private void cboFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.rdoHyperLinkeType.Enabled = this.cboFields.SelectedIndex > 0;
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
        }

        private void chkScaleSymbols_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
        }

        private void chkShowMapTip_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
        }

        private void LayerDisplaySetCtrl_Load(object sender, EventArgs e)
        {
            this.chkShowMapTip.Checked = this.ilayer_0.ShowTips;
            if (this.ilayer_0 is ILayerEffects)
            {
                this.txtPercent.Text = (this.ilayer_0 as ILayerEffects).Transparency.ToString();
            }
            else
            {
                this.txtPercent.Enabled = false;
            }
            if (this.ilayer_0 is IFeatureLayer)
            {
                this.chkScaleSymbols.Checked = (this.ilayer_0 as IFeatureLayer2).ScaleSymbols;
                this.chkScaleSymbols.Enabled = true;
                this.chkShowMapTip.Enabled = true;
            }
            else
            {
                this.chkScaleSymbols.Enabled = false;
                this.chkShowMapTip.Enabled = true;
            }
            ILayerFields fields = this.ilayer_0 as ILayerFields;
            this.cboFields.Properties.Items.Clear();
            this.cboFields.Properties.Items.Add("<无>");
            if (this.ilayer_0 is IHotlinkContainer)
            {
                int num2 = 0;
                string hotlinkField = (this.ilayer_0 as IHotlinkContainer).HotlinkField;
                this.HyperLinkGroup.Visible = true;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.get_Field(i);
                    IFieldInfo info = fields.get_FieldInfo(i);
                    if ((field.Type == esriFieldType.esriFieldTypeString) && info.Visible)
                    {
                        this.cboFields.Properties.Items.Add(new FieldWrapEx(field, info));
                        if (field.Name == hotlinkField)
                        {
                            num2 = this.cboFields.Properties.Items.Count - 1;
                        }
                    }
                }
                this.cboFields.SelectedIndex = num2;
                this.rdoHyperLinkeType.SelectedIndex = (int) (this.ilayer_0 as IHotlinkContainer).HotlinkType;
            }
            else
            {
                this.HyperLinkGroup.Visible = false;
            }
            this.bool_0 = true;
        }

        private ITable method_0()
        {
            if (this.ilayer_0 == null)
            {
                return null;
            }
            if (this.ilayer_0 is IDisplayTable)
            {
                return (this.ilayer_0 as IDisplayTable).DisplayTable;
            }
            if (this.ilayer_0 is IAttributeTable)
            {
                return (this.ilayer_0 as IAttributeTable).AttributeTable;
            }
            if (this.ilayer_0 is IFeatureLayer)
            {
                return ((this.ilayer_0 as IFeatureLayer).FeatureClass as ITable);
            }
            return (this.ilayer_0 as ITable);
        }

        private void method_1(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
        }

        private void rdoHyperLinkeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
        }

        private void txtPercent_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
        }

        public IBasicMap FocusMap
        {
            set { }
        }

        public bool IsPageDirty
        {
            get { return this.bool_1; }
        }

        public ILayer Layer
        {
            set { this.ilayer_0 = value; }
        }

        public object SelectItem
        {
            set { this.ilayer_0 = value as ILayer; }
        }
    }
}