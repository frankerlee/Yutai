using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class MapGridControlFirst : UserControl
    {
        private Container container_0 = null;
        protected IMapFrame m_pMapFrame = null;
        private string[] string_0 = new string[] { "经纬网", "方里网", "索引格网" };

        public MapGridControlFirst()
        {
            this.InitializeComponent();
        }

        public IMapGrid CreateMapGrid()
        {
            return this.CreateMapGrid(this.radioMapGridType.SelectedIndex);
        }

        public IMapGrid CreateMapGrid(int int_0)
        {
            IMapGrid grid = null;
            switch (int_0)
            {
                case 0:
                    grid = new GraticuleClass();
                    break;

                case 1:
                    grid = new MeasuredGridClass();
                    break;

                case 2:
                    grid = new IndexGridClass();
                    break;
            }
            if (grid == null)
            {
                return null;
            }
            grid.SetDefaults(this.m_pMapFrame);
            grid.LineSymbol = null;
            IMarkerSymbol symbol = new SimpleMarkerSymbolClass();
            (symbol as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSCross;
            grid.TickMarkSymbol = symbol;
            if (grid is IMeasuredGrid)
            {
                IFormattedGridLabel label = new FormattedGridLabelClass();
                INumericFormat format = new NumericFormatClass {
                    AlignmentOption = esriNumericAlignmentEnum.esriAlignLeft,
                    AlignmentWidth = 0,
                    RoundingOption = esriRoundingOptionEnum.esriRoundNumberOfDecimals,
                    RoundingValue = 3,
                    ShowPlusSign = true,
                    UseSeparator = true,
                    ZeroPad = true
                };
                label.Format = format as INumberFormat;
                (label as IGridLabel).LabelOffset = 6.0;
                grid.LabelFormat = label as IGridLabel;
                IEnvelope mapBounds = this.m_pMapFrame.MapBounds;
                (grid as IMeasuredGrid).FixedOrigin = true;
                (grid as IMeasuredGrid).XOrigin = mapBounds.XMin + 500.0;
                (grid as IMeasuredGrid).YOrigin = mapBounds.YMin + 500.0;
                grid.SetTickVisibility(false, false, false, false);
                grid.SetSubTickVisibility(false, false, false, false);
            }
            return grid;
        }

 private void radioMapGridType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.radioMapGridType.SelectedIndex != -1)
            {
                this.txtMapGridName.Text = this.string_0[this.radioMapGridType.SelectedIndex];
            }
        }

        public IMapFrame MapFrame
        {
            set
            {
                this.m_pMapFrame = value;
            }
        }
    }
}

