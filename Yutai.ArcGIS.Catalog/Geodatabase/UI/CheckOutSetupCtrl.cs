using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal class CheckOutSetupCtrl : UserControl
    {
        private SimpleButton btnSelectOutGDB;
        private CheckEdit chkResueSchema;
        private Container container_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private RadioGroup rdoType;
        private TextEdit txtCheckOutName;
        private TextEdit txtOutGDB;

        public CheckOutSetupCtrl()
        {
            this.InitializeComponent();
        }

        private void CheckOutSetupCtrl_Load(object sender, EventArgs e)
        {
            string str = Application.StartupPath + @"\CheckOut_Output";
            string path = str + ".mdb";
            for (int i = 1; File.Exists(path); i++)
            {
                path = str + "_" + i.ToString() + ".mdb";
            }
            this.txtOutGDB.Text = path;
            this.txtCheckOutName.Text = Path.GetFileNameWithoutExtension(path);
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
            string str = this.txtCheckOutName.Text.Trim();
            if (str.Length == 0)
            {
                MessageBox.Show("请输入检出名称!");
                return false;
            }
            IVersionedWorkspace workspace = (CheckOutHelper.m_pHelper.MasterWorkspaceName as IName).Open() as IVersionedWorkspace;
            try
            {
                if (workspace.FindVersion(str) != null)
                {
                    MessageBox.Show("主数据库中已存在版本: " + str);
                    return false;
                }
            }
            catch
            {
            }
            string path = this.txtOutGDB.Text.Trim();
            if (path.Length == 0)
            {
                MessageBox.Show("请选择检出位置!");
                return false;
            }
            if (!(Path.GetExtension(path).ToLower() == ".mdb"))
            {
                MessageBox.Show("请选择正确的检出位置!");
                return false;
            }
            if (!File.Exists(path))
            {
                IWorkspaceFactory factory = new AccessWorkspaceFactoryClass();
                try
                {
                    IWorkspaceName name = factory.Create(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path), null, 0);
                    this.txtOutGDB.Tag = name;
                    goto Label_016D;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                    return false;
                }
            }
            IWorkspaceName name2 = new WorkspaceNameClass {
                WorkspaceFactoryProgID = "esriDataSourcesGDB.AccessWorkspaceFactory",
                PathName = path
            };
            IWorkspace workspace2 = (name2 as IName).Open() as IWorkspace;
            if (!this.method_2(workspace2, CheckOutHelper.m_pHelper.EnumName))
            {
                return false;
            }
            this.txtOutGDB.Tag = name2;
        Label_016D:
            CheckOutHelper.m_pHelper.ReuseSchema = this.chkResueSchema.Checked;
            CheckOutHelper.m_pHelper.CheckOutName = str;
            CheckOutHelper.m_pHelper.CheckOnlySchema = this.rdoType.SelectedIndex == 1;
            CheckOutHelper.m_pHelper.CheckoutWorkspaceName = this.txtOutGDB.Tag as IWorkspaceName;
            return true;
        }

        private void InitializeComponent()
        {
           System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckOutSetupCtrl));
            this.label1 = new Label();
            this.label2 = new Label();
            this.rdoType = new RadioGroup();
            this.label3 = new Label();
            this.txtOutGDB = new TextEdit();
            this.chkResueSchema = new CheckEdit();
            this.label4 = new Label();
            this.txtCheckOutName = new TextEdit();
            this.btnSelectOutGDB = new SimpleButton();
            this.rdoType.Properties.BeginInit();
            this.txtOutGDB.Properties.BeginInit();
            this.chkResueSchema.Properties.BeginInit();
            this.txtCheckOutName.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4f, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "检出数据,从:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x68, 8);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0, 0x11);
            this.label2.TabIndex = 1;
            this.rdoType.Location = new Point(0x10, 0x20);
            this.rdoType.Name = "rdoType";
            this.rdoType.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "数据"), new RadioGroupItem(null, "仅方案") });
            this.rdoType.Size = new Size(0x58, 0x30);
            this.rdoType.TabIndex = 2;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x10, 0x60);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x55, 0x11);
            this.label3.TabIndex = 3;
            this.label3.Text = "检出到数据库:";
            this.txtOutGDB.EditValue = "";
            this.txtOutGDB.Location = new Point(0x10, 120);
            this.txtOutGDB.Name = "txtOutGDB";
            this.txtOutGDB.Size = new Size(0xc0, 0x17);
            this.txtOutGDB.TabIndex = 4;
            this.chkResueSchema.Location = new Point(0x10, 0x98);
            this.chkResueSchema.Name = "chkResueSchema";
            this.chkResueSchema.Properties.Caption = "原来空间数据库被检出，则重用方案";
            this.chkResueSchema.Size = new Size(0xd8, 0x13);
            this.chkResueSchema.TabIndex = 5;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x10, 0xb0);
            this.label4.Name = "label4";
            this.label4.Size = new Size(60, 0x11);
            this.label4.TabIndex = 6;
            this.label4.Text = "检出名称:";
            this.txtCheckOutName.EditValue = "";
            this.txtCheckOutName.Location = new Point(0x10, 0xc0);
            this.txtCheckOutName.Name = "txtCheckOutName";
            this.txtCheckOutName.Size = new Size(0xc0, 0x17);
            this.txtCheckOutName.TabIndex = 7;
            this.btnSelectOutGDB.Image = (Image) resources.GetObject("btnSelectOutGDB.Image");
            this.btnSelectOutGDB.Location = new Point(0xd8, 120);
            this.btnSelectOutGDB.Name = "btnSelectOutGDB";
            this.btnSelectOutGDB.Size = new Size(0x18, 0x18);
            this.btnSelectOutGDB.TabIndex = 9;
            base.Controls.Add(this.btnSelectOutGDB);
            base.Controls.Add(this.txtCheckOutName);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.chkResueSchema);
            base.Controls.Add(this.txtOutGDB);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.rdoType);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "CheckOutSetupCtrl";
            base.Size = new Size(0x130, 0x110);
            base.Load += new EventHandler(this.CheckOutSetupCtrl_Load);
            this.rdoType.Properties.EndInit();
            this.txtOutGDB.Properties.EndInit();
            this.chkResueSchema.Properties.EndInit();
            this.txtCheckOutName.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private string method_0(IDatasetName idatasetName_0)
        {
            string[] strArray = idatasetName_0.Name.Split(new char[] { '.' });
            return strArray[strArray.Length - 1];
        }

        private bool method_1(IWorkspace iworkspace_0, esriDatasetType esriDatasetType_0, string string_0)
        {
            if (esriDatasetType_0 == esriDatasetType.esriDTFeatureDataset)
            {
                IEnumDatasetName name = iworkspace_0.get_DatasetNames(esriDatasetType.esriDTFeatureDataset);
                name.Reset();
                for (IDatasetName name2 = name.Next(); name2 != null; name2 = name.Next())
                {
                    string[] strArray = name2.Name.Split(new char[] { '.' });
                    string str = strArray[strArray.Length - 1].ToLower();
                    if (string_0 == str)
                    {
                        return true;
                    }
                }
                return false;
            }
            return (iworkspace_0 as IWorkspace2).get_NameExists(esriDatasetType_0, string_0);
        }

        private bool method_2(IWorkspace iworkspace_0, IEnumName ienumName_0)
        {
            if (iworkspace_0 is IWorkspaceReplicas)
            {
                IEnumReplica replicas = (iworkspace_0 as IWorkspaceReplicas).Replicas;
                replicas.Reset();
                if (replicas.Next() != null)
                {
                    MessageBox.Show("检出数据库中包含一个有效的检出。");
                    return false;
                }
                ienumName_0.Reset();
                for (IDatasetName name = ienumName_0.Next() as IDatasetName; name != null; name = ienumName_0.Next() as IDatasetName)
                {
                    if (this.method_1(iworkspace_0, name.Type, this.method_0(name).ToLower()))
                    {
                        if (this.chkResueSchema.Checked)
                        {
                            return true;
                        }
                        MessageBox.Show("选择的检出空间数据库中有同名的要素集，如果要用该检出空间数据库，请在重用选项上打勾。");
                        return false;
                    }
                }
            }
            return true;
        }
    }
}

