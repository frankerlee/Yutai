using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class LegendSetupUserControl : UserControl
    {
        private bool bool_0 = false;
        private ILegend ilegend_0 = null;
        private ILegendItem ilegendItem_0 = null;
        private IStyleGallery istyleGallery_0 = null;
        private IStyleGalleryItem 定制 = null;
        private IStyleGalleryItem 定制_1 = null;

        public LegendSetupUserControl()
        {
            this.InitializeComponent();
        }

        private void cboAreaPatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0 && (this.ilegendItem_0 != null))
            {
                this.ilegendItem_0.LegendClassFormat.AreaPatch =
                    this.cboAreaPatches.GetSelectStyleGalleryItem().Item as IAreaPatch;
            }
        }

        private void cboLinePatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0 && (this.ilegendItem_0 != null))
            {
                this.ilegendItem_0.LegendClassFormat.LinePatch =
                    this.cboLinePatches.GetSelectStyleGalleryItem().Item as ILinePatch;
            }
        }

        private void LegendSetupUserControl_Load(object sender, EventArgs e)
        {
            if (this.istyleGallery_0 != null)
            {
                IStyleGalleryItem item = null;
                IEnumStyleGalleryItem item2 = this.istyleGallery_0.get_Items("Line Patches", "", "");
                item2.Reset();
                for (item = item2.Next(); item != null; item = item2.Next())
                {
                    this.cboLinePatches.Add(item);
                }
                if (this.cboLinePatches.Items.Count > 0)
                {
                    this.cboLinePatches.SelectedIndex = 0;
                }
                item2 = this.istyleGallery_0.get_Items("Area Patches", "", "");
                item2.Reset();
                for (item = item2.Next(); item != null; item = item2.Next())
                {
                    this.cboAreaPatches.Add(item);
                }
                if (this.cboAreaPatches.Items.Count > 0)
                {
                    this.cboAreaPatches.SelectedIndex = 0;
                }
            }
            this.method_0();
            this.bool_0 = true;
        }

        private void listLegendLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.bool_0 = false;
            this.ilegendItem_0 =
                (this.listLegendLayers.Items[this.listLegendLayers.SelectedIndex] as LegendItemWrap).LegendItem;
            if (this.ilegendItem_0 != null)
            {
                ILegendClassFormat legendClassFormat = this.ilegendItem_0.LegendClassFormat;
                if (legendClassFormat.AreaPatch == null)
                {
                    ILegendFormat format = this.ilegend_0.Format;
                    legendClassFormat.AreaPatch = format.DefaultAreaPatch;
                    legendClassFormat.LinePatch = format.DefaultLinePatch;
                    legendClassFormat.PatchWidth = format.DefaultPatchWidth;
                    legendClassFormat.PatchHeight = format.DefaultPatchHeight;
                }
                if (this.定制 == null)
                {
                    this.定制 = new ServerStyleGalleryItemClass();
                    this.定制.Name = "定制";
                    this.定制.Item = legendClassFormat.LinePatch;
                }
                if (this.定制_1 == null)
                {
                    this.定制_1 = new ServerStyleGalleryItemClass();
                    this.定制_1.Name = "定制";
                    this.定制_1.Item = legendClassFormat.AreaPatch;
                }
                this.cboLinePatches.SelectStyleGalleryItem(this.定制);
                this.cboAreaPatches.SelectStyleGalleryItem(this.定制_1);
                this.txtWidth.Text = legendClassFormat.PatchWidth.ToString("#.##");
                this.txtHeight.Text = legendClassFormat.PatchHeight.ToString("#.##");
                this.bool_0 = true;
            }
        }

        private void method_0()
        {
            this.listLegendLayers.Items.Clear();
            for (int i = 0; i < this.ilegend_0.ItemCount; i++)
            {
                ILegendItem item = this.ilegend_0.get_Item(i);
                if (!(item.Layer is IFeatureLayer) ||
                    (((item.Layer as IFeatureLayer).FeatureClass.ShapeType != esriGeometryType.esriGeometryPoint) &&
                     ((item.Layer as IFeatureLayer).FeatureClass.ShapeType != esriGeometryType.esriGeometryMultipoint)))
                {
                    this.listLegendLayers.Items.Add(new LegendItemWrap(item));
                }
            }
            if (this.listLegendLayers.Items.Count > 0)
            {
                this.listLegendLayers.SelectedIndex = 0;
            }
        }

        private void txtHeight_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.txtHeight.ForeColor = Color.Red;
                    double num = Convert.ToDouble(this.txtHeight.Text);
                    if (this.ilegendItem_0 != null)
                    {
                        this.ilegendItem_0.LegendClassFormat.PatchHeight = num;
                    }
                }
                catch
                {
                    this.txtHeight.ForeColor = Color.Red;
                }
            }
        }

        private void txtHeight_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtWidth_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.txtWidth.ForeColor = Color.Black;
                    double num = Convert.ToDouble(this.txtWidth.Text);
                    if (this.ilegendItem_0 != null)
                    {
                        this.ilegendItem_0.LegendClassFormat.PatchWidth = num;
                    }
                }
                catch
                {
                    this.txtWidth.ForeColor = Color.Red;
                }
            }
        }

        private void txtWidth_TextChanged(object sender, EventArgs e)
        {
        }

        public ILegend Legend
        {
            set { this.ilegend_0 = value; }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                if (this.istyleGallery_0 != value)
                {
                    this.istyleGallery_0 = value;
                }
            }
        }
    }
}