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
    internal partial class ExtractionDataSetupCtrl : UserControl
    {
        private Container container_0 = null;

        public ExtractionDataSetupCtrl()
        {
            this.InitializeComponent();
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
                IWorkspaceFactory factory = new AccessWorkspaceFactory();
                try
                {
                    IWorkspaceName name = factory.Create(Path.GetDirectoryName(path),
                        Path.GetFileNameWithoutExtension(path), null, 0);
                    this.txtOutGDB.Tag = name;
                    goto Label_00EC;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                    return false;
                }
            }
            IWorkspaceName name2 = new WorkspaceNameClass
            {
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

        private string method_0(IDatasetName idatasetName_0)
        {
            string[] strArray = idatasetName_0.Name.Split(new char[] {'.'});
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
                    string[] strArray = name2.Name.Split(new char[] {'.'});
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