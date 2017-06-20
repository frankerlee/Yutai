using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Controls.ConfigSetting
{
    internal class frmLayerConfigTable : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOk;
        private Container components = null;
        private string m_pLayerConfig = "";
        private IWorkspace m_pWorkspace = null;
        private TreeView treeView1;

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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLayerConfigTable));
            this.treeView1 = new TreeView();
            this.btnOk = new SimpleButton();
            this.btnCancel = new SimpleButton();
            base.SuspendLayout();
            this.treeView1.Location = new Point(8, 8);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new Size(0xf8, 0xd8);
            this.treeView1.TabIndex = 0;
            this.btnOk.DialogResult = DialogResult.OK;
            this.btnOk.Location = new Point(0x68, 240);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new Size(0x40, 0x18);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "确定";
            this.btnOk.Click += new EventHandler(this.btnOk_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0xb8, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x108, 0x111);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOk);
            base.Controls.Add(this.treeView1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmLayerConfigTable";
            this.Text = "选择图层配置表";
            base.Load += new EventHandler(this.frmLayerConfigTable_Load);
            base.ResumeLayout(false);
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

