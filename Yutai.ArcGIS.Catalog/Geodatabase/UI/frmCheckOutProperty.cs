using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public class frmCheckOutProperty : Form
    {
        private SimpleButton btnOK;
        private ListBox CheckOutDatasetlist;
        private Container container_0 = null;
        private IReplica ireplica_0 = null;
        private IWorkspace iworkspace_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label lblCreateTime;
        private Label lblName;
        private Label lblOwner;
        private Label lblParentVersion;
        private SimpleButton simpleButton1;

        public frmCheckOutProperty(IReplica ireplica_1, IWorkspace iworkspace_1)
        {
            this.InitializeComponent();
            this.ireplica_0 = ireplica_1;
            this.iworkspace_0 = iworkspace_1;
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmCheckOutProperty_Load(object sender, EventArgs e)
        {
            this.lblName.Text = this.ireplica_0.Name;
            this.lblOwner.Text = this.ireplica_0.Owner;
            this.lblCreateTime.Text = new DateTime((long) this.ireplica_0.ReplicaDate).ToLongDateString();
            this.lblParentVersion.Text = this.method_0(this.ireplica_0.Version);
            this.CheckOutDatasetlist.Items.Clear();
            IEnumReplicaDataset replicaDatasets = this.ireplica_0.ReplicaDatasets;
            replicaDatasets.Reset();
            IReplicaDataset dataset2 = replicaDatasets.Next();
            string item = "";
            while (dataset2 != null)
            {
                if (dataset2.Type == esriDatasetType.esriDTTable)
                {
                    item = "表:" + dataset2.Name;
                }
                else if (dataset2.Type == esriDatasetType.esriDTFeatureClass)
                {
                    item = "要素类:" + dataset2.Name;
                }
                else if (dataset2.Type == esriDatasetType.esriDTGeometricNetwork)
                {
                    item = "网络:" + dataset2.Name;
                }
                else if (dataset2.Type == esriDatasetType.esriDTRelationshipClass)
                {
                    item = "关系:" + dataset2.Name;
                }
                else if (dataset2.Type == esriDatasetType.esriDTTopology)
                {
                    item = "拓扑:" + dataset2.Name;
                }
                else
                {
                    item = dataset2.Name;
                }
                this.CheckOutDatasetlist.Items.Add(item);
                dataset2 = replicaDatasets.Next();
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckOutProperty));
            this.CheckOutDatasetlist = new ListBox();
            this.lblParentVersion = new Label();
            this.lblCreateTime = new Label();
            this.lblOwner = new Label();
            this.lblName = new Label();
            this.label5 = new Label();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.simpleButton1 = new SimpleButton();
            this.btnOK = new SimpleButton();
            base.SuspendLayout();
            this.CheckOutDatasetlist.ItemHeight = 12;
            this.CheckOutDatasetlist.Location = new Point(8, 0x98);
            this.CheckOutDatasetlist.Name = "CheckOutDatasetlist";
            this.CheckOutDatasetlist.Size = new Size(0xe8, 0x7c);
            this.CheckOutDatasetlist.TabIndex = 0x13;
            this.lblParentVersion.Location = new Point(0x48, 0x60);
            this.lblParentVersion.Name = "lblParentVersion";
            this.lblParentVersion.Size = new Size(160, 0x18);
            this.lblParentVersion.TabIndex = 0x12;
            this.lblCreateTime.Location = new Point(0x48, 0x40);
            this.lblCreateTime.Name = "lblCreateTime";
            this.lblCreateTime.Size = new Size(160, 0x18);
            this.lblCreateTime.TabIndex = 0x11;
            this.lblOwner.Location = new Point(0x48, 40);
            this.lblOwner.Name = "lblOwner";
            this.lblOwner.Size = new Size(160, 0x18);
            this.lblOwner.TabIndex = 0x10;
            this.lblName.Location = new Point(0x48, 8);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(160, 0x18);
            this.lblName.TabIndex = 15;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(8, 0x80);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x3b, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "检出数据:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 0x60);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x2f, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "父版本:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 0x40);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "创建:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 40);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "属主:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "名字:";
            this.simpleButton1.DialogResult = DialogResult.Cancel;
            this.simpleButton1.Location = new Point(0xb0, 0x120);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(0x38, 0x18);
            this.simpleButton1.TabIndex = 0x15;
            this.simpleButton1.Text = "取消";
            this.btnOK.Location = new Point(0x68, 0x120);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 20;
            this.btnOK.Text = "确定";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x100, 0x145);
            base.Controls.Add(this.simpleButton1);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.CheckOutDatasetlist);
            base.Controls.Add(this.lblParentVersion);
            base.Controls.Add(this.lblCreateTime);
            base.Controls.Add(this.lblOwner);
            base.Controls.Add(this.lblName);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Icon = (Icon) resources.GetObject("$Icon");
            base.Name = "frmCheckOutProperty";
            this.Text = "frmCheckOutProperty";
            base.Load += new EventHandler(this.frmCheckOutProperty_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private string method_0(string string_0)
        {
            if (this.iworkspace_0 is IVersionedWorkspace)
            {
                IVersionedWorkspace workspace = this.iworkspace_0 as IVersionedWorkspace;
                try
                {
                    IVersion version = workspace.FindVersion(string_0);
                    if (version.HasParent())
                    {
                        return version.VersionInfo.Parent.VersionName;
                    }
                }
                catch
                {
                }
            }
            return "";
        }

        public IReplica Replica
        {
            set
            {
                this.ireplica_0 = value;
            }
        }
    }
}

