using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class NewRelationClassSetClass : UserControl
    {
        private Container container_0 = null;

        public NewRelationClassSetClass()
        {
            this.InitializeComponent();
        }

        private void method_0(IDatasetName idatasetName_0, TreeNode treeNode_0)
        {
            try
            {
                IEnumDatasetName subsetNames = idatasetName_0.SubsetNames;
                subsetNames.Reset();
                for (IDatasetName name2 = subsetNames.Next(); name2 != null; name2 = subsetNames.Next())
                {
                    TreeNode node = new TreeNode(name2.Name)
                    {
                        Tag = (name2 as IName).Open()
                    };
                    treeNode_0.Nodes.Add(node);
                }
            }
            catch (Exception)
            {
            }
        }

        private void NewRelationClassSetClass_Load(object sender, EventArgs e)
        {
            if (NewRelationClassHelper.m_pWorkspace != null)
            {
                IEnumDatasetName name = NewRelationClassHelper.m_pWorkspace.get_DatasetNames(esriDatasetType.esriDTAny);
                name.Reset();
                for (IDatasetName name2 = name.Next(); name2 != null; name2 = name.Next())
                {
                    if (((name2.Type == esriDatasetType.esriDTFeatureClass) ||
                         (name2.Type == esriDatasetType.esriDTTable)) ||
                        (name2.Type == esriDatasetType.esriDTFeatureDataset))
                    {
                        TreeNode node = new TreeNode(name2.Name)
                        {
                            Tag = (name2 as IName).Open()
                        };
                        this.treeViewSource.Nodes.Add(node);
                        if (name2.Type == esriDatasetType.esriDTFeatureDataset)
                        {
                            this.method_0(name2, node);
                        }
                        node = new TreeNode(name2.Name)
                        {
                            Tag = (name2 as IName).Open()
                        };
                        this.treeViewDest.Nodes.Add(node);
                        if (name2.Type == esriDatasetType.esriDTFeatureDataset)
                        {
                            this.method_0(name2, node);
                        }
                    }
                }
            }
        }

        private void treeViewDest_AfterSelect(object sender, TreeViewEventArgs e)
        {
            NewRelationClassHelper.DestinationClass = e.Node.Tag as IObjectClass;
            if (NewRelationClassHelper.DestinationClass != null)
            {
                NewRelationClassHelper.backwardLabel = (NewRelationClassHelper.DestinationClass as IDataset).Name;
            }
        }

        private void treeViewSource_AfterSelect(object sender, TreeViewEventArgs e)
        {
            NewRelationClassHelper.OriginClass = e.Node.Tag as IObjectClass;
            if (NewRelationClassHelper.OriginClass != null)
            {
                NewRelationClassHelper.forwardLabel = (NewRelationClassHelper.OriginClass as IDataset).Name;
            }
        }

        private void txtRelationClassName_EditValueChanged(object sender, EventArgs e)
        {
            NewRelationClassHelper.relClassName = this.txtRelationClassName.Text;
        }
    }
}