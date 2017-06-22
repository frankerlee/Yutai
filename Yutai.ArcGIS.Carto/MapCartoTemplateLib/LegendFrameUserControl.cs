using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    internal partial class LegendFrameUserControl : UserControl
    {
        private bool bool_0 = false;
        private IBackground ibackground_0 = null;
        private IBorder iborder_0 = null;
        private IShadow ishadow_0 = null;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;

        public LegendFrameUserControl()
        {
            this.InitializeComponent();
        }

 private void LegendFrameUserControl_Load(object sender, EventArgs e)
        {
            this.cboBorder.Add(null);
            this.cboBackground.Add(null);
            this.cboShadow.Add(null);
            if (this.istyleGallery_0 != null)
            {
                IStyleGalleryItem item = null;
                IEnumStyleGalleryItem item2 = this.istyleGallery_0.get_Items("Borders", "", "");
                item2.Reset();
                for (item = item2.Next(); item != null; item = item2.Next())
                {
                    this.cboBorder.Add(item);
                }
                if (this.cboBorder.Items.Count > 0)
                {
                    this.cboBorder.SelectedIndex = 0;
                }
                item2 = this.istyleGallery_0.get_Items("Backgrounds", "", "");
                item2.Reset();
                for (item = item2.Next(); item != null; item = item2.Next())
                {
                    this.cboBackground.Add(item);
                }
                if (this.cboBackground.Items.Count > 0)
                {
                    this.cboBackground.SelectedIndex = 0;
                }
                item2 = this.istyleGallery_0.get_Items("Shadows", "", "");
                item2.Reset();
                for (item = item2.Next(); item != null; item = item2.Next())
                {
                    this.cboShadow.Add(item);
                }
                if (this.cboShadow.Items.Count > 0)
                {
                    this.cboShadow.SelectedIndex = 0;
                }
            }
            this.method_0();
            this.bool_0 = true;
        }

        private void method_0()
        {
            IStyleGalleryItem oO = null;
            if (this.iborder_0 == null)
            {
                oO = null;
            }
            else
            {
                oO = new MyStyleGalleryItem {
                    Name = "<定制>",
                    Item = this.iborder_0
                };
            }
            this.cboBorder.SelectStyleGalleryItem(oO);
            if (this.ibackground_0 == null)
            {
                oO = null;
            }
            else
            {
                oO = new MyStyleGalleryItem {
                    Name = "<定制>",
                    Item = this.ibackground_0
                };
            }
            this.cboBackground.SelectStyleGalleryItem(oO);
            if (this.ishadow_0 == null)
            {
                oO = null;
            }
            else
            {
                oO = new MyStyleGalleryItem {
                    Name = "<定制>",
                    Item = this.ishadow_0
                };
            }
            this.cboShadow.SelectStyleGalleryItem(oO);
        }

        private void txtCornerRounding_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.txtCornerRounding.ForeColor = Color.Black;
                short num = short.Parse(this.txtCornerRounding.Text);
                if ((num < 0) || (num > 100))
                {
                    this.txtCornerRounding.ForeColor = Color.Red;
                }
                else
                {
                    if (this.iborder_0 != null)
                    {
                        (this.iborder_0 as IFrameDecoration).CornerRounding = num;
                    }
                    if (this.ibackground_0 != null)
                    {
                        (this.ibackground_0 as IFrameDecoration).CornerRounding = num;
                    }
                    if (this.ishadow_0 != null)
                    {
                        (this.ishadow_0 as IFrameDecoration).CornerRounding = num;
                    }
                }
            }
            catch
            {
                this.txtCornerRounding.ForeColor = Color.Red;
            }
        }

        private void txtGap_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.txtGap.ForeColor = Color.Black;
                double num = double.Parse(this.txtGap.Text);
                if (this.iborder_0 != null)
                {
                    (this.iborder_0 as IFrameDecoration).HorizontalSpacing = num;
                    (this.iborder_0 as IFrameDecoration).VerticalSpacing = num;
                }
                if (this.ibackground_0 != null)
                {
                    (this.ibackground_0 as IFrameDecoration).HorizontalSpacing = num;
                    (this.ibackground_0 as IFrameDecoration).VerticalSpacing = num;
                }
                if (this.ishadow_0 != null)
                {
                    (this.ishadow_0 as IFrameDecoration).HorizontalSpacing = num;
                    (this.ishadow_0 as IFrameDecoration).VerticalSpacing = num;
                }
            }
            catch
            {
                this.txtGap.ForeColor = Color.Red;
            }
        }

        public MapCartoTemplateLib.MapTemplateElement MapTemplateElement
        {
            set
            {
                if (value is MapTemplateLegendElement)
                {
                    this.mapTemplateElement_0 = value;
                    this.iborder_0 = (this.mapTemplateElement_0 as MapTemplateLegendElement).Border;
                    this.ibackground_0 = (this.mapTemplateElement_0 as MapTemplateLegendElement).Background;
                    this.ishadow_0 = (this.mapTemplateElement_0 as MapTemplateLegendElement).Shadow;
                    this.method_0();
                }
            }
        }
    }
}

