namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using ESRI.ArcGIS.GeoDatabaseDistributed;
    using JLK.Catalog;
    using JLK.Catalog.UI;
    using JLK.Editors;
    using JLK.Editors.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Resources;
    using System.Windows.Forms;

    internal class CheckInSetupCtrl : UserControl
    {
        private bool bool_0 = false;
        private SimpleButton btnSelectDelta;
        private SimpleButton btnSelectGDB;
        private CheckEdit chkReconcile;
        private CheckEdit chkReconcile2;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private Label label1;
        private Label label3;
        private Label label4;
        private Label label6;
        private Label label8;
        private Label lblCheckInGDB;
        private Label lblCheckInGDB2;
        private Label lblCheckOutName;
        private Label lblCheckOutName1;
        private Label lblMasterGDB2;
        private Panel panel_by_MasterGDB;
        private Panel panel1;
        private Panel panel2;
        private Panel panelbycheckoutdb;
        private RadioGroup radioGroup1;
        private TextEdit txtDelta;
        private TextEdit txtGDB;

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

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
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

        private void InitializeComponent()
        {
            ResourceManager manager = new ResourceManager(typeof(CheckInSetupCtrl));
            this.panel_by_MasterGDB = new Panel();
            this.groupBox1 = new GroupBox();
            this.panel2 = new Panel();
            this.btnSelectDelta = new SimpleButton();
            this.txtDelta = new TextEdit();
            this.panel1 = new Panel();
            this.btnSelectGDB = new SimpleButton();
            this.txtGDB = new TextEdit();
            this.radioGroup1 = new RadioGroup();
            this.chkReconcile = new CheckEdit();
            this.lblCheckInGDB = new Label();
            this.label4 = new Label();
            this.lblCheckOutName = new Label();
            this.label1 = new Label();
            this.panelbycheckoutdb = new Panel();
            this.lblCheckInGDB2 = new Label();
            this.label8 = new Label();
            this.chkReconcile2 = new CheckEdit();
            this.lblMasterGDB2 = new Label();
            this.label3 = new Label();
            this.lblCheckOutName1 = new Label();
            this.label6 = new Label();
            this.panel_by_MasterGDB.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.txtDelta.Properties.BeginInit();
            this.panel1.SuspendLayout();
            this.txtGDB.Properties.BeginInit();
            this.radioGroup1.Properties.BeginInit();
            this.chkReconcile.Properties.BeginInit();
            this.panelbycheckoutdb.SuspendLayout();
            this.chkReconcile2.Properties.BeginInit();
            base.SuspendLayout();
            this.panel_by_MasterGDB.Controls.Add(this.groupBox1);
            this.panel_by_MasterGDB.Controls.Add(this.chkReconcile);
            this.panel_by_MasterGDB.Controls.Add(this.lblCheckInGDB);
            this.panel_by_MasterGDB.Controls.Add(this.label4);
            this.panel_by_MasterGDB.Controls.Add(this.lblCheckOutName);
            this.panel_by_MasterGDB.Controls.Add(this.label1);
            this.panel_by_MasterGDB.Location = new Point(8, 8);
            this.panel_by_MasterGDB.Name = "panel_by_MasterGDB";
            this.panel_by_MasterGDB.Size = new Size(0x178, 0x108);
            this.panel_by_MasterGDB.TabIndex = 6;
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.radioGroup1);
            this.groupBox1.Location = new Point(0, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(360, 0x88);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择检入数据";
            this.panel2.Controls.Add(this.btnSelectDelta);
            this.panel2.Controls.Add(this.txtDelta);
            this.panel2.Enabled = false;
            this.panel2.Location = new Point(0x1c, 0x58);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x130, 0x20);
            this.panel2.TabIndex = 3;
            this.btnSelectDelta.Image = (Image) manager.GetObject("btnSelectDelta.Image");
            this.btnSelectDelta.Location = new Point(0x110, 6);
            this.btnSelectDelta.Name = "btnSelectDelta";
            this.btnSelectDelta.Size = new Size(0x18, 0x18);
            this.btnSelectDelta.TabIndex = 10;
            this.btnSelectDelta.Click += new EventHandler(this.btnSelectDelta_Click);
            this.txtDelta.EditValue = "";
            this.txtDelta.Location = new Point(0x10, 6);
            this.txtDelta.Name = "txtDelta";
            this.txtDelta.Properties.ReadOnly = true;
            this.txtDelta.Size = new Size(0xe0, 0x17);
            this.txtDelta.TabIndex = 0;
            this.panel1.Controls.Add(this.btnSelectGDB);
            this.panel1.Controls.Add(this.txtGDB);
            this.panel1.Enabled = false;
            this.panel1.Location = new Point(0x18, 0x20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x130, 0x20);
            this.panel1.TabIndex = 2;
            this.btnSelectGDB.Image = (Image) manager.GetObject("btnSelectGDB.Image");
            this.btnSelectGDB.Location = new Point(0x110, 6);
            this.btnSelectGDB.Name = "btnSelectGDB";
            this.btnSelectGDB.Size = new Size(0x18, 0x18);
            this.btnSelectGDB.TabIndex = 10;
            this.btnSelectGDB.Click += new EventHandler(this.btnSelectGDB_Click);
            this.txtGDB.EditValue = "";
            this.txtGDB.Location = new Point(0x10, 6);
            this.txtGDB.Name = "txtGDB";
            this.txtGDB.Properties.ReadOnly = true;
            this.txtGDB.Size = new Size(0xe0, 0x17);
            this.txtGDB.TabIndex = 0;
            this.radioGroup1.Location = new Point(8, -8);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = Color.Transparent;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "空间数据库"), new RadioGroupItem(null, "增量数据库或增量xml文件") });
            this.radioGroup1.Size = new Size(0x108, 0x70);
            this.radioGroup1.TabIndex = 0;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            this.chkReconcile.Location = new Point(0, 0xe0);
            this.chkReconcile.Name = "chkReconcile";
            this.chkReconcile.Properties.Caption = "与父版本协调和提交";
            this.chkReconcile.Size = new Size(0x90, 0x13);
            this.chkReconcile.TabIndex = 10;
            this.lblCheckInGDB.AutoSize = true;
            this.lblCheckInGDB.Location = new Point(0x48, 0xc0);
            this.lblCheckInGDB.Name = "lblCheckInGDB";
            this.lblCheckInGDB.Size = new Size(0, 0x11);
            this.lblCheckInGDB.TabIndex = 9;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0, 0xc0);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x48, 0x11);
            this.label4.TabIndex = 8;
            this.label4.Text = "目的数据库:";
            this.lblCheckOutName.AutoSize = true;
            this.lblCheckOutName.Location = new Point(0x48, 160);
            this.lblCheckOutName.Name = "lblCheckOutName";
            this.lblCheckOutName.Size = new Size(0, 0x11);
            this.lblCheckOutName.TabIndex = 7;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0, 160);
            this.label1.Name = "label1";
            this.label1.Size = new Size(60, 0x11);
            this.label1.TabIndex = 6;
            this.label1.Text = "检出名字:";
            this.panelbycheckoutdb.Controls.Add(this.lblCheckInGDB2);
            this.panelbycheckoutdb.Controls.Add(this.label8);
            this.panelbycheckoutdb.Controls.Add(this.chkReconcile2);
            this.panelbycheckoutdb.Controls.Add(this.lblMasterGDB2);
            this.panelbycheckoutdb.Controls.Add(this.label3);
            this.panelbycheckoutdb.Controls.Add(this.lblCheckOutName1);
            this.panelbycheckoutdb.Controls.Add(this.label6);
            this.panelbycheckoutdb.Location = new Point(8, 8);
            this.panelbycheckoutdb.Name = "panelbycheckoutdb";
            this.panelbycheckoutdb.Size = new Size(0x178, 0xb0);
            this.panelbycheckoutdb.TabIndex = 7;
            this.panelbycheckoutdb.Visible = false;
            this.lblCheckInGDB2.AutoSize = true;
            this.lblCheckInGDB2.Location = new Point(120, 0x18);
            this.lblCheckInGDB2.Name = "lblCheckInGDB2";
            this.lblCheckInGDB2.Size = new Size(0, 0x11);
            this.lblCheckInGDB2.TabIndex = 0x11;
            this.lblCheckInGDB2.Tag = "";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x10, 0x18);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x61, 0x11);
            this.label8.TabIndex = 0x10;
            this.label8.Text = "要检入的数据库:";
            this.chkReconcile2.Location = new Point(0x10, 0x88);
            this.chkReconcile2.Name = "chkReconcile2";
            this.chkReconcile2.Properties.Caption = "与父版本协调和提交";
            this.chkReconcile2.Size = new Size(0x90, 0x13);
            this.chkReconcile2.TabIndex = 15;
            this.lblMasterGDB2.AutoSize = true;
            this.lblMasterGDB2.Location = new Point(120, 0x68);
            this.lblMasterGDB2.Name = "lblMasterGDB2";
            this.lblMasterGDB2.Size = new Size(0, 0x11);
            this.lblMasterGDB2.TabIndex = 14;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x10, 0x68);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x48, 0x11);
            this.label3.TabIndex = 13;
            this.label3.Text = "目的数据库:";
            this.lblCheckOutName1.AutoSize = true;
            this.lblCheckOutName1.Location = new Point(120, 0x40);
            this.lblCheckOutName1.Name = "lblCheckOutName1";
            this.lblCheckOutName1.Size = new Size(0, 0x11);
            this.lblCheckOutName1.TabIndex = 12;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x10, 0x40);
            this.label6.Name = "label6";
            this.label6.Size = new Size(60, 0x11);
            this.label6.TabIndex = 11;
            this.label6.Text = "检出名字:";
            base.Controls.Add(this.panelbycheckoutdb);
            base.Controls.Add(this.panel_by_MasterGDB);
            base.Name = "CheckInSetupCtrl";
            base.Size = new Size(400, 0x120);
            base.Load += new EventHandler(this.CheckInSetupCtrl_Load);
            this.panel_by_MasterGDB.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.txtDelta.Properties.EndInit();
            this.panel1.ResumeLayout(false);
            this.txtGDB.Properties.EndInit();
            this.radioGroup1.Properties.EndInit();
            this.chkReconcile.Properties.EndInit();
            this.panelbycheckoutdb.ResumeLayout(false);
            this.chkReconcile2.Properties.EndInit();
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

