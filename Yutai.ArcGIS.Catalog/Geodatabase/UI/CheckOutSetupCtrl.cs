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
    internal partial class CheckOutSetupCtrl : UserControl
    {
        private Container container_0 = null;

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

        public bool Do()
        {
            string str = this.txtCheckOutName.Text.Trim();
            if (str.Length == 0)
            {
                MessageBox.Show("请输入检出名称!");
                return false;
            }
            IVersionedWorkspace workspace =
                (CheckOutHelper.m_pHelper.MasterWorkspaceName as IName).Open() as IVersionedWorkspace;
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
                IWorkspaceFactory factory = new AccessWorkspaceFactory();
                try
                {
                    IWorkspaceName name = factory.Create(Path.GetDirectoryName(path),
                        Path.GetFileNameWithoutExtension(path), null, 0);
                    this.txtOutGDB.Tag = name;
                    goto Label_016D;
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
                for (IDatasetName name = ienumName_0.Next() as IDatasetName;
                    name != null;
                    name = ienumName_0.Next() as IDatasetName)
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