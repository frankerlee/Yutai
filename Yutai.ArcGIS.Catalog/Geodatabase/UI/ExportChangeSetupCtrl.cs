using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class ExportChangeSetupCtrl : UserControl
    {
        private Container container_0 = null;

        public ExportChangeSetupCtrl()
        {
            this.InitializeComponent();
        }

        private void btnSelectOutGDB_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "增量数据库|*.mdb|增量xml|*.xml"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtDeltaFile.Text = dialog.FileName;
            }
        }

        public bool Do()
        {
            string str = this.txtDeltaFile.Text.Trim();
            if (str.Length == 0)
            {
                MessageBox.Show("请输入增量文件名!");
                return false;
            }
            ExportChangesHelper.m_pHelper.DeltaFileName = str;
            return true;
        }

        private void ExportChangeSetupCtrl_Load(object sender, EventArgs e)
        {
            this.lblCheckOutGDB.Text = this.method_0(ExportChangesHelper.m_pHelper.CheckoutWorkspaceName);
            IWorkspace workspace = (ExportChangesHelper.m_pHelper.CheckoutWorkspaceName as IName).Open() as IWorkspace;
            IWorkspaceReplicas replicas = workspace as IWorkspaceReplicas;
            IEnumReplica replica = replicas.Replicas;
            replica.Reset();
            IReplica replica2 = replica.Next();
            if (replica2 != null)
            {
                this.lblCheckOutName.Text = replica2.Name;
            }
        }

        private string method_0(IWorkspaceName iworkspaceName_0)
        {
            string pathName = iworkspaceName_0.PathName;
            if (iworkspaceName_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
            {
                IPropertySet connectionProperties = iworkspaceName_0.ConnectionProperties;
                pathName = connectionProperties.GetProperty("Version").ToString();
                string str2 = connectionProperties.GetProperty("DB_CONNECTION_PROPERTIES").ToString();
                pathName = pathName + "(" + str2 + ")";
            }
            return pathName;
        }
    }
}