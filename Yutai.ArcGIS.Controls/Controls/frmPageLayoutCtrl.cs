using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using Yutai.ArcGIS.Framework.Docking;

namespace Yutai.ArcGIS.Controls.Controls
{
    public partial class frmPageLayoutCtrl : DockContent
    {

        public frmPageLayoutCtrl()
        {
            this.InitializeComponent();
        }

 private void frmPageLayoutCtrl_Load(object sender, EventArgs e)
        {
            IActiveView activeView = this.axPageLayoutControl1.ActiveView;
            if (activeView.Selection == null)
            {
                activeView.Selection = new SimpleElementSelectionClass();
            }
            IGraphicsContainerProperty selection = activeView.Selection as IGraphicsContainerProperty;
            if (selection != null)
            {
                selection.GraphicsContainer = activeView.GraphicsContainer;
            }
        }

 public AxPageLayoutControl PageLayoutControl
        {
            get
            {
                return this.axPageLayoutControl1;
            }
        }
    }
}

