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
    internal class ExportChangeSetupCtrl : UserControl
    {
        private SimpleButton btnSelectOutGDB;
        private Container container_0 = null;
        private Label label1;
        private Label label3;
        private Label lblCheckOutGDB;
        private Label lblCheckOutName;
        private Label lblInfo;
        private TextEdit txtDeltaFile;

        public ExportChangeSetupCtrl()
        {
            this.InitializeComponent();
        }

        private void btnSelectOutGDB_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog {
                Filter = "增量数据库|*.mdb|增量xml|*.xml"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtDeltaFile.Text = dialog.FileName;
            }
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
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

        private void InitializeComponent()
        {
           System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportChangeSetupCtrl));
            this.lblInfo = new Label();
            this.txtDeltaFile = new TextEdit();
            this.btnSelectOutGDB = new SimpleButton();
            this.label1 = new Label();
            this.lblCheckOutName = new Label();
            this.lblCheckOutGDB = new Label();
            this.label3 = new Label();
            this.txtDeltaFile.Properties.BeginInit();
            base.SuspendLayout();
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new Point(0x10, 0x10);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new Size(0xc4, 0x11);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "要创建的增量数据库或增量xml文件";
            this.txtDeltaFile.EditValue = "";
            this.txtDeltaFile.Location = new Point(0x10, 40);
            this.txtDeltaFile.Name = "txtDeltaFile";
            this.txtDeltaFile.Size = new Size(0xe0, 0x17);
            this.txtDeltaFile.TabIndex = 1;
            this.btnSelectOutGDB.Image = (Image) resources.GetObject("btnSelectOutGDB.Image");
            this.btnSelectOutGDB.Location = new Point(0xf8, 40);
            this.btnSelectOutGDB.Name = "btnSelectOutGDB";
            this.btnSelectOutGDB.Size = new Size(0x18, 0x18);
            this.btnSelectOutGDB.TabIndex = 10;
            this.btnSelectOutGDB.Click += new EventHandler(this.btnSelectOutGDB_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x58);
            this.label1.Name = "label1";
            this.label1.Size = new Size(60, 0x11);
            this.label1.TabIndex = 11;
            this.label1.Text = "检出名称:";
            this.lblCheckOutName.AutoSize = true;
            this.lblCheckOutName.Location = new Point(120, 0x58);
            this.lblCheckOutName.Name = "lblCheckOutName";
            this.lblCheckOutName.Size = new Size(0, 0x11);
            this.lblCheckOutName.TabIndex = 12;
            this.lblCheckOutGDB.AutoSize = true;
            this.lblCheckOutGDB.Location = new Point(120, 0x88);
            this.lblCheckOutGDB.Name = "lblCheckOutGDB";
            this.lblCheckOutGDB.Size = new Size(0, 0x11);
            this.lblCheckOutGDB.TabIndex = 14;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x10, 0x88);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x48, 0x11);
            this.label3.TabIndex = 13;
            this.label3.Text = "导出数据库:";
            base.Controls.Add(this.lblCheckOutGDB);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.lblCheckOutName);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.btnSelectOutGDB);
            base.Controls.Add(this.txtDeltaFile);
            base.Controls.Add(this.lblInfo);
            base.Name = "ExportChangeSetupCtrl";
            base.Size = new Size(0x130, 0x108);
            base.Load += new EventHandler(this.ExportChangeSetupCtrl_Load);
            this.txtDeltaFile.Properties.EndInit();
            base.ResumeLayout(false);
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

