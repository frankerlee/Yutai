using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmGNPropertySheet : Form
    {
        private Container container_0 = null;
        private IGeometricNetwork igeometricNetwork_0 = null;

        public frmGNPropertySheet()
        {
            this.InitializeComponent();
        }

 private void frmGNPropertySheet_Load(object sender, EventArgs e)
        {
            XtraTabPage page = new XtraTabPage {
                Text = "常规"
            };
            GeometryNetGeneralPropertyPage page2 = new GeometryNetGeneralPropertyPage {
                GeometricNetwork = this.igeometricNetwork_0
            };
            page.Controls.Add(page2);
            this.xtraTabControl1.TabPages.Add(page);
            page = new XtraTabPage {
                Text = "连通性"
            };
            GeometryNewConnectivityPropertyPage page3 = new GeometryNewConnectivityPropertyPage {
                GeometricNetwork = this.igeometricNetwork_0
            };
            page.Controls.Add(page3);
            this.xtraTabControl1.TabPages.Add(page);
            page = new XtraTabPage {
                Text = "权重"
            };
            WeightPropertyPage page4 = new WeightPropertyPage {
                GeometricNetwork = this.igeometricNetwork_0
            };
            page.Controls.Add(page4);
            this.xtraTabControl1.TabPages.Add(page);
        }

 public IGeometricNetwork GeometricNetwork
        {
            set
            {
                this.igeometricNetwork_0 = value;
            }
        }
    }
}

