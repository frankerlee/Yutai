using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class MapGridTypeProperty : UserControl
    {
        private Container container_0 = null;
        protected IMapFrame m_pMapFrame = null;
        private string[] string_0 = new string[] { "经纬网", "方里网", "索引格网" };

        public MapGridTypeProperty()
        {
            this.InitializeComponent();
            this.txtMapGridName.Text = "经纬网";
        }

        public IMapGrid CreateMapGrid()
        {
            int selectedIndex = this.radioMapGridType.SelectedIndex;
            IMapGrid grid = null;
            switch (selectedIndex)
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
            grid.Name = this.txtMapGridName.Text;
            if (grid is IMeasuredGrid)
            {
                try
                {
                    double mapScale = this.m_pMapFrame.Map.MapScale;
                }
                catch
                {
                }
                IEnvelope extent = (this.m_pMapFrame.Map as IActiveView).Extent;
                (grid as IMeasuredGrid).XOrigin = extent.XMin;
                (grid as IMeasuredGrid).YOrigin = extent.YMin;
                (grid as IMeasuredGrid).FixedOrigin = true;
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

