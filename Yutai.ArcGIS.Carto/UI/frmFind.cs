using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class frmFind : Form, IDockContent
    {
        private FindControl findControl_0 = new FindControl();
        private IApplication iapplication_0 = null;
        private IContainer icontainer_0 = null;

        public frmFind()
        {
            this.InitializeComponent();
            this.findControl_0.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(this.findControl_0);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            IArray array = this.findControl_0.Find();
            this.btnFind.Enabled = false;
            this.btnStop.Enabled = true;
            frmFindResult result = new frmFindResult
            {
                Text =
                    "在" + this.findControl_0.m_strFindLayers + "的" + this.findControl_0.m_strFindField + "中查找" +
                    this.findControl_0.m_strSearch + "的结果",
                FindResults = array,
                ActiveView = this.iapplication_0.ActiveView
            };
            this.iapplication_0.DockWindows(result, null);
            this.btnFind.Enabled = true;
            this.btnStop.Enabled = false;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.btnFind.Enabled = true;
            this.btnStop.Enabled = false;
        }

        public IApplication Application
        {
            set
            {
                this.iapplication_0 = value;
                this.findControl_0.FocusMap = this.iapplication_0.FocusMap;
            }
        }

        public DockingStyle DefaultDockingStyle
        {
            get { return DockingStyle.Bottom; }
        }

        public IMap FocusMap
        {
            set { this.findControl_0.FocusMap = value; }
        }

        string IDockContent.Name
        {
            get { return base.Name; }
        }

        int IDockContent.Width
        {
            get { return base.Width; }
        }
    }
}