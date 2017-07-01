using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class frmDataFrameProperty : Form
    {
        private Container container_0 = null;
        private IMapFrame imapFrame_0 = null;
        private MapCoordinateCtrl mapCoordinateCtrl_0 = new MapCoordinateCtrl();
        private MapDataFramePage mapDataFramePage_0 = new MapDataFramePage();
        private MapGeneralInfoCtrl mapGeneralInfoCtrl_0 = new MapGeneralInfoCtrl();

        public frmDataFrameProperty()
        {
            this.InitializeComponent();
            this.mapGeneralInfoCtrl_0.Dock = DockStyle.Fill;
            this.tabPageGeneral.Controls.Add(this.mapGeneralInfoCtrl_0);
            this.mapCoordinateCtrl_0.Dock = DockStyle.Fill;
            this.tabPageCoordinate.Controls.Add(this.mapCoordinateCtrl_0);
            this.mapDataFramePage_0.Dock = DockStyle.Fill;
            this.tabPageMapDataFrame.Controls.Add(this.mapDataFramePage_0);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.mapGeneralInfoCtrl_0.Apply();
            this.mapCoordinateCtrl_0.Apply();
            this.mapDataFramePage_0.Apply();
            base.Close();
        }

        private void frmDataFrameProperty_Load(object sender, EventArgs e)
        {
        }

        public IBasicMap FocusMap
        {
            set
            {
                this.mapGeneralInfoCtrl_0.FocusMap = value;
                this.mapCoordinateCtrl_0.FocusMap = value;
                this.mapDataFramePage_0.Map = value;
            }
        }

        public IMapFrame MapFrame
        {
            set
            {
                this.imapFrame_0 = value;
                this.mapGeneralInfoCtrl_0.FocusMap = this.imapFrame_0.Map as IBasicMap;
                this.mapCoordinateCtrl_0.FocusMap = this.imapFrame_0.Map as IBasicMap;
                this.mapDataFramePage_0.MapFrame = value;
            }
        }
    }
}