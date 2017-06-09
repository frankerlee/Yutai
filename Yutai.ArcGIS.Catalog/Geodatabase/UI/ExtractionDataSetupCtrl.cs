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
    internal class ExtractionDataSetupCtrl : UserControl
    {
        private SimpleButton btnSelectOutGDB;
        private CheckEdit chkResueSchema;
        private Container container_0 = null;
        private Label label1;
        private Label label3;
        private Label lblSource;
        private RadioGroup rdoType;
        private TextEdit txtOutGDB;

        public ExtractionDataSetupCtrl()
        {
            this.InitializeComponent();
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
            string path = this.txtOutGDB.Text.Trim();
            if (path.Length == 0)
            {
                MessageBox.Show("请选择导出位置!");
                return false;
            }
            if (!(Path.GetExtension(path).ToLower() == ".mdb"))
            {
                MessageBox.Show("请选择正确的导出位置!");
                return false;
            }
            if (!File.Exists(path))
            {
                IWorkspaceFactory factory = new AccessWorkspaceFactoryClass();
                try
                {
                    IWorkspaceName name = factory.Create(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path), null, 0);
                    this.txtOutGDB.Tag = name;
                    goto Label_00EC;
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
            IWorkspace workspace = (name2 as IName).Open() as IWorkspace;
            if (!this.method_2(workspace, ExtractionDataHelper.m_pHelper.EnumName))
            {
                return false;
            }
            this.txtOutGDB.Tag = name2;
        Label_00EC:
            ExtractionDataHelper.m_pHelper.ReuseSchema = this.chkResueSchema.Checked;
            ExtractionDataHelper.m_pHelper.CheckOnlySchema = this.rdoType.SelectedIndex == 1;
            ExtractionDataHelper.m_pHelper.CheckoutWorkspaceName = this.txtOutGDB.Tag as IWorkspaceName;
            return true;
        }

        private void ExtractionDataSetupCtrl_Load(object sender, EventArgs e)
        {
            string str = Application.StartupPath + @"\Extract_Output";
            string path = str + ".mdb";
            for (int i = 1; File.Exists(path); i++)
            {
                path = str + "_" + i.ToString() + ".mdb";
            }
            this.txtOutGDB.Text = path;
        }

        private void InitializeComponent()
        {
           System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtractionDataSetupCtrl));
            this.label1 = new Label();
            this.lblSource = new Label();
            this.rdoType = new RadioGroup();
            this.label3 = new Label();
            this.txtOutGDB = new TextEdit();
            this.chkResueSchema = new CheckEdit();
            this.btnSelectOutGDB = new SimpleButton();
            this.rdoType.Properties.BeginInit();
            this.txtOutGDB.Properties.BeginInit();
            this.chkResueSchema.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4f, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "提取数据,从:";
            this.lblSource.AutoSize = true;
            this.lblSource.Location = new Point(0x68, 8);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new Size(0, 0x11);
            this.lblSource.TabIndex = 1;
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
            this.label3.Text = "提取到数据库:";
            this.txtOutGDB.EditValue = "";
            this.txtOutGDB.Location = new Point(0x10, 120);
            this.txtOutGDB.Name = "txtOutGDB";
            this.txtOutGDB.Size = new Size(0xc0, 0x17);
            this.txtOutGDB.TabIndex = 4;
            this.chkResueSchema.Location = new Point(0x10, 0x98);
            this.chkResueSchema.Name = "chkResueSchema";
            this.chkResueSchema.Properties.Caption = "重用方案";
            this.chkResueSchema.Size = new Size(0x88, 0x13);
            this.chkResueSchema.TabIndex = 5;
            this.btnSelectOutGDB.Image = (Image) resources.GetObject("btnSelectOutGDB.Image");
            this.btnSelectOutGDB.Location = new Point(0xd8, 120);
            this.btnSelectOutGDB.Name = "btnSelectOutGDB";
            this.btnSelectOutGDB.Size = new Size(0x18, 0x18);
            this.btnSelectOutGDB.TabIndex = 9;
            base.Controls.Add(this.btnSelectOutGDB);
            base.Controls.Add(this.chkResueSchema);
            base.Controls.Add(this.txtOutGDB);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.rdoType);
            base.Controls.Add(this.lblSource);
            base.Controls.Add(this.label1);
            base.Name = "ExtractionDataSetupCtrl";
            base.Size = new Size(0x130, 0x110);
            base.Load += new EventHandler(this.ExtractionDataSetupCtrl_Load);
            this.rdoType.Properties.EndInit();
            this.txtOutGDB.Properties.EndInit();
            this.chkResueSchema.Properties.EndInit();
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
            if (!(iworkspace_0 is IWorkspaceReplicas))
            {
                goto Label_009D;
            }
            IEnumReplica replicas = (iworkspace_0 as IWorkspaceReplicas).Replicas;
            replicas.Reset();
            replicas.Next();
            ienumName_0.Reset();
            IDatasetName name = ienumName_0.Next() as IDatasetName;
        Label_003C:
            if (name != null)
            {
                bool flag = false;
                try
                {
                    flag = this.method_1(iworkspace_0, name.Type, this.method_0(name).ToLower());
                }
                catch
                {
                }
                while (!flag)
                {
                    name = ienumName_0.Next() as IDatasetName;
                    goto Label_003C;
                }
                if (this.chkResueSchema.Checked)
                {
                    return true;
                }
                MessageBox.Show("选择的导出空间数据库中有同名的要素集，如果要用该导出空间数据库，请在重用选项上打勾。");
                return false;
            }
        Label_009D:
            return true;
        }
    }
}

