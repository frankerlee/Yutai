using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class MapGridCoordinatePropertyPage : UserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private IMapGrid imapGrid_0 = null;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;
        private ITextSymbol itextSymbol_0 = new TextSymbolClass();

        public MapGridCoordinatePropertyPage()
        {
            this.InitializeComponent();
        }

        private void btnMainLineStyle_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    selector.SetSymbol(this.imapGrid_0.TickLineSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.imapGrid_0.TickLineSymbol = selector.GetSymbol() as ILineSymbol;
                    }
                }
            }
            catch
            {
            }
        }

        private void btnSubLineStyle_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    selector.SetSymbol(this.imapGrid_0.SubTickLineSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.imapGrid_0.SubTickLineSymbol = selector.GetSymbol() as ILineSymbol;
                    }
                }
            }
            catch
            {
            }
        }

        private void chkSubTickVisibility_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                bool leftVis = this.chkSubTickVisibility.Checked;
                this.imapGrid_0.SetSubTickVisibility(leftVis, leftVis, leftVis, leftVis);
                this.btnSubLineStyle.Enabled = leftVis;
                this.spinSubTickCount.Enabled = leftVis;
            }
        }

        private void chkTickVisibility_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                bool leftVis = this.chkTickVisibility.Checked;
                this.imapGrid_0.SetTickVisibility(leftVis, leftVis, leftVis, leftVis);
                this.btnMainLineStyle.Enabled = leftVis;
            }
        }

        public void Init()
        {
            this.bool_0 = false;
            if (this.imapGrid_0 != null)
            {
                this.btnMainLineStyle.Style = this.imapGrid_0.TickLineSymbol;
                this.btnSubLineStyle.Style = this.imapGrid_0.SubTickLineSymbol;
                bool leftVis = false;
                bool rightVis = false;
                bool topVis = false;
                bool bottomVis = false;
                this.imapGrid_0.QueryTickVisibility(ref leftVis, ref topVis, ref rightVis, ref bottomVis);
                this.chkTickVisibility.Checked = leftVis;
                this.btnMainLineStyle.Enabled = leftVis;
                this.imapGrid_0.QuerySubTickVisibility(ref leftVis, ref topVis, ref rightVis, ref bottomVis);
                this.chkSubTickVisibility.Checked = leftVis;
                this.btnSubLineStyle.Enabled = leftVis;
                this.spinSubTickCount.Enabled = leftVis;
                this.spinSubTickCount.Value = this.imapGrid_0.SubTickCount;
                IGridLabel labelFormat = this.imapGrid_0.LabelFormat;
                this.itextSymbol_0.Font = labelFormat.Font;
                this.itextSymbol_0.Color = labelFormat.Color;
                this.itextSymbol_0.Text = labelFormat.DisplayName;
                this.styleButton1.Style = this.itextSymbol_0;
                if (labelFormat is IMixedFontGridLabel)
                {
                    (labelFormat as IMixedFontGridLabel).NumGroupedDigits = 2;
                    this.imapGrid_0.LabelFormat = labelFormat;
                }
                if (labelFormat is IFormattedGridLabel)
                {
                    INumericFormat format = new NumericFormatClass
                    {
                        RoundingValue = 0
                    };
                    (labelFormat as IFormattedGridLabel).Format = format as INumberFormat;
                    this.imapGrid_0.LabelFormat = labelFormat;
                }
            }
            this.bool_0 = true;
        }

        private void MapGridCoordinatePropertyPage_Load(object sender, EventArgs e)
        {
        }

        private void spinSubTickCount_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.imapGrid_0.SubTickCount = (short) this.spinSubTickCount.Value;
                }
                catch
                {
                }
            }
        }

        private void styleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                selector.SetStyleGallery(this.istyleGallery_0);
                selector.SetSymbol(this.itextSymbol_0);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    object symbol = selector.GetSymbol();
                    this.itextSymbol_0 = symbol as ITextSymbol;
                    IGridLabel labelFormat = this.imapGrid_0.LabelFormat;
                    labelFormat.Font = this.itextSymbol_0.Font;
                    labelFormat.Color = this.itextSymbol_0.Color;
                    this.imapGrid_0.LabelFormat = labelFormat;
                    this.styleButton1.Style = this.itextSymbol_0;
                }
            }
            catch
            {
            }
        }

        public IMapGrid MapGrid
        {
            set { this.imapGrid_0 = value; }
        }
    }
}