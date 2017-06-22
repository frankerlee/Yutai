using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Controls.ConfigSetting
{
    internal partial class frmLayerConfigTable : Form
    {
        private string m_pLayerConfig = "";
        private IWorkspace m_pWorkspace = null;

        public frmLayerConfigTable()
        {
            this.InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode.Tag != null)
            {
                this.m_pLayerConfig = this.treeView1.SelectedNode.Text;
            }
            else
            {
                this.m_pLayerConfig = "";
            }
        }

 private void frmLayerConfigTable_Load(object sender, EventArgs e)
        {
            IEnumDataset dataset = this.m_pWorkspace.get_Datasets(esriDatasetType.esriDTTable);
            dataset.Reset();
            TreeNode node = new TreeNode("空间数据库");
            this.treeView1.Nodes.Add(node);
            for (ITable table = dataset.Next() as ITable; table != null; table = dataset.Next() as ITable)
            {
                IFields fields = table.Fields;
                TreeNode node2 = new TreeNode((table as IDataset).Name) {
                    Tag = table
                };
                node.Nodes.Add(node2);
                if (this.m_pLayerConfig == (table as IDataset).Name)
                {
                    this.treeView1.SelectedNode = node2;
                }
            }
        }

 public string LayerConfig
        {
            get
            {
                return this.m_pLayerConfig;
            }
            set
            {
                this.m_pLayerConfig = value;
            }
        }

        public IWorkspace Workspace
        {
            set
            {
                this.m_pWorkspace = value;
            }
        }
    }
}

