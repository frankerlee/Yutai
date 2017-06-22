using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.GeoDatabaseDistributed;
using Yutai.ArcGIS.Catalog.UI;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class CheckInSetupCtrl : UserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;

        public CheckInSetupCtrl()
        {
            this.InitializeComponent();
        }

        private void btnSelectDelta_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "*.mdb|*.mdb|*.xml|*.xml",
                Multiselect = false
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.bool_0 = false;
                this.lblCheckOutName.Text = "";
                this.txtDelta.Text = "";
                string fileName = dialog.FileName;
                esriExportDataChangesOption esriExportToAccess = esriExportDataChangesOption.esriExportToAccess;
                if (dialog.FilterIndex == 2)
                {
                    esriExportToAccess = esriExportDataChangesOption.esriExportToXML;
                }
                IDeltaDataChangesInit init = new DeltaDataChangesClass();
                try
                {
                    init.Init(fileName, esriExportToAccess);
                    IDeltaDataChanges changes = init as IDeltaDataChanges;
                    this.txtDelta.Text = fileName;
                    this.txtDelta.Tag = changes.Container;
                    IDataChanges changes2 = init as IDataChanges;
                    if (changes2.ParentWorkspaceName != null)
                    {
                        IWorkspace workspace = (changes2.ParentWorkspaceName as IName).Open() as IWorkspace;
                        IReplica replica = (workspace as IWorkspaceReplicas).get_ReplicaByID(changes2.ParentReplicaID);
                        if (replica != null)
                        {
                            this.lblCheckOutName.Text = replica.Name;
                            this.bool_0 = true;
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("检入空间中没有检出数据或检出数据无效!");
                }
            }
        }

        private void btnSelectGDB_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile();
            file.AddFilter(new MyGxFilterWorkspaces(), true);
            if (file.ShowDialog() == DialogResult.OK)
            {
                IGxDatabase database = file.Items.get_Element(0) as IGxDatabase;
                if (database != null)
                {
                    this.txtGDB.Text = (database as IGxObject).FullName;
                    if (!database.IsConnected)
                    {
                        database.Connect();
                    }
                    IWorkspaceReplicas workspace = database.Workspace as IWorkspaceReplicas;
                    if (workspace != null)
                    {
                        IEnumReplica replicas = workspace.Replicas;
                        replicas.Reset();
                        IReplica replica2 = replicas.Next();
                        if (replica2 != null)
                        {
                            this.lblCheckOutName.Text = replica2.Name;
                            this.txtGDB.Tag = database.WorkspaceName;
                        }
                        else
                        {
                            this.txtGDB.Tag = null;
                        }
                    }
                }
            }
        }

        private void CheckInSetupCtrl_Load(object sender, EventArgs e)
        {
            this.Init();
        }

 public bool Do()
        {
            if (CheckInHelper.m_pHelper.MasterWorkspaceName != null)
            {
                CheckInHelper.m_pHelper.ReconcileCheckout = this.chkReconcile.Checked;
                if (this.radioGroup1.SelectedIndex == 0)
                {
                    if (this.txtGDB.Tag == null)
                    {
                        MessageBox.Show("请选择检出空间数据库!");
                        return false;
                    }
                    CheckInHelper.m_pHelper.CheckInfromGDB = true;
                    CheckInHelper.m_pHelper.CheckoutWorkspaceName = this.txtGDB.Tag as IWorkspaceName;
                    CheckInHelper.m_pHelper.CheckInName = this.lblCheckOutName.Text;
                }
                else
                {
                    string path = this.txtDelta.Text.Trim();
                    if (path.Length == 0)
                    {
                        MessageBox.Show("请输入增量文件!");
                        return false;
                    }
                    string str2 = Path.GetExtension(path).ToLower();
                    if ((str2 != ".xml") || (str2 != ".mdb"))
                    {
                        MessageBox.Show("请输入正确的增量文件!");
                    }
                    if (!File.Exists(path))
                    {
                        MessageBox.Show("指定的增量文件不存在!");
                        return false;
                    }
                    CheckInHelper.m_pHelper.CheckInfromGDB = false;
                    CheckInHelper.m_pHelper.DeltaFileName = this.txtDelta.Text;
                }
                CheckInHelper.m_pHelper.CheckInName = this.lblCheckOutName.Text;
            }
            else
            {
                CheckInHelper.m_pHelper.ReconcileCheckout = this.chkReconcile2.Checked;
                CheckInHelper.m_pHelper.CheckInfromGDB = true;
                CheckInHelper.m_pHelper.CheckInName = this.lblCheckOutName1.Text;
                CheckInHelper.m_pHelper.MasterWorkspaceName = this.lblMasterGDB2.Tag as IWorkspaceName;
            }
            return true;
        }

        public void Init()
        {
            if (CheckInHelper.m_pHelper.MasterWorkspaceName != null)
            {
                this.panel_by_MasterGDB.Visible = true;
                this.panelbycheckoutdb.Visible = false;
                this.lblCheckInGDB.Text = this.method_0(CheckInHelper.m_pHelper.MasterWorkspaceName);
                this.radioGroup1_SelectedIndexChanged(this, new EventArgs());
            }
            else
            {
                this.panel_by_MasterGDB.Visible = false;
                this.panelbycheckoutdb.Visible = true;
                IWorkspace workspace = (CheckInHelper.m_pHelper.CheckoutWorkspaceName as IName).Open() as IWorkspace;
                IWorkspaceReplicas replicas = workspace as IWorkspaceReplicas;
                IEnumReplica replica = replicas.Replicas;
                replica.Reset();
                IReplica replica2 = replica.Next();
                if (replica2 != null)
                {
                    this.lblCheckOutName1.Text = replica2.Name;
                    this.lblMasterGDB2.Text = this.method_0(replica2.ConnectionInfo);
                    this.lblMasterGDB2.Tag = replica2.ConnectionInfo;
                    this.lblCheckInGDB2.Text = this.method_0(CheckInHelper.m_pHelper.CheckoutWorkspaceName);
                }
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
                pathName = pathName + "(" + str2;
                str2 = "sde";
                pathName = pathName + "-" + str2 + ")";
            }
            return pathName;
        }

        private string method_1(IWorkspace iworkspace_0)
        {
            string pathName = iworkspace_0.PathName;
            if (iworkspace_0.Type != esriWorkspaceType.esriRemoteDatabaseWorkspace)
            {
                return pathName;
            }
            IPropertySet connectionProperties = iworkspace_0.ConnectionProperties;
            pathName = connectionProperties.GetProperty("Version").ToString();
            string str2 = connectionProperties.GetProperty("DB_CONNECTION_PROPERTIES").ToString();
            pathName = pathName + "(" + str2;
            try
            {
                str2 = connectionProperties.GetProperty("User").ToString();
                pathName = pathName + "-" + str2;
            }
            catch
            {
            }
            return (pathName + ")");
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.radioGroup1.SelectedIndex == 0)
            {
                this.panel1.Enabled = true;
                this.panel2.Enabled = false;
            }
            else
            {
                this.panel1.Enabled = false;
                this.panel2.Enabled = true;
            }
            CheckInHelper.m_pHelper.CheckInfromGDB = this.radioGroup1.SelectedIndex == 0;
        }
    }
}

