using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal class frmAddClass : Form
    {
        private SimpleButton btnOK;
        private Container container_0 = null;
        private IList ilist_0 = new ArrayList();
        private ITopology itopology_0 = null;
        private ListBoxControl listBoxControl1;
        private SimpleButton simpleButton2;

        public frmAddClass()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.listBoxControl1.SelectedIndices.Count > 0)
            {
                for (int i = 0; i < this.listBoxControl1.SelectedIndices.Count; i++)
                {
                    this.ilist_0.Add((this.listBoxControl1.Items[this.listBoxControl1.SelectedIndices[i]] as FeatureClassWrap).FeatureClass);
                }
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

        private void frmAddClass_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddClass));
            this.listBoxControl1 = new ListBoxControl();
            this.btnOK = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            ((ISupportInitialize) this.listBoxControl1).BeginInit();
            base.SuspendLayout();
            this.listBoxControl1.ItemHeight = 0x11;
            this.listBoxControl1.Location = new Point(8, 8);
            this.listBoxControl1.Name = "listBoxControl1";
            this.listBoxControl1.Size = new Size(160, 0x98);
            this.listBoxControl1.TabIndex = 0;
            this.listBoxControl1.SelectedIndexChanged += new EventHandler(this.listBoxControl1_SelectedIndexChanged);
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new Point(0x10, 0xb0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.simpleButton2.DialogResult = DialogResult.Cancel;
            this.simpleButton2.Location = new Point(0x58, 0xb0);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(0x38, 0x18);
            this.simpleButton2.TabIndex = 2;
            this.simpleButton2.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0xba, 0xdf);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.listBoxControl1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) resources.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmAddClass";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "增加类";
            base.Load += new EventHandler(this.frmAddClass_Load);
            ((ISupportInitialize) this.listBoxControl1).EndInit();
            base.ResumeLayout(false);
        }

        private void listBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxControl1.SelectedIndices.Count > 0)
            {
                this.btnOK.Enabled = true;
            }
            else
            {
                this.btnOK.Enabled = false;
            }
        }

        private void method_0()
        {
            IEnumDataset subsets = this.itopology_0.FeatureDataset.Subsets;
            subsets.Reset();
            for (IDataset dataset3 = subsets.Next(); dataset3 != null; dataset3 = subsets.Next())
            {
                IVersionedObject obj2;
                int num;
                if ((dataset3.Type != esriDatasetType.esriDTFeatureClass) || ((dataset3 as IFeatureClass).FeatureType != esriFeatureType.esriFTSimple))
                {
                    continue;
                }
                if (!(dataset3 is ITopologyClass))
                {
                    goto Label_00EF;
                }
                bool flag = true;
                if (dataset3.Workspace is IVersionedWorkspace)
                {
                    obj2 = dataset3 as IVersionedObject;
                    if (obj2.IsRegisteredAsVersioned)
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    num = 0;
                    while (num < TopologyEditHelper.m_pList.Count)
                    {
                        if (TopologyEditHelper.m_pList.get_Element(num) == dataset3)
                        {
                            goto Label_00C3;
                        }
                        num++;
                    }
                }
                goto Label_00C5;
            Label_00C3:
                flag = false;
            Label_00C5:
                if (flag)
                {
                    this.listBoxControl1.Items.Add(new FeatureClassWrap(dataset3 as IFeatureClass));
                }
                continue;
            Label_00EF:
                flag = true;
                if (dataset3.Workspace is IVersionedWorkspace)
                {
                    obj2 = dataset3 as IVersionedObject;
                    if (obj2.IsRegisteredAsVersioned)
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    for (num = 0; num < TopologyEditHelper.m_pList.Count; num++)
                    {
                        if (TopologyEditHelper.m_pList.get_Element(num) == dataset3)
                        {
                            goto Label_0155;
                        }
                    }
                }
                goto Label_0157;
            Label_0155:
                flag = false;
            Label_0157:
                if (flag)
                {
                    this.listBoxControl1.Items.Add(new FeatureClassWrap(dataset3 as IFeatureClass));
                }
            }
        }

        public IList List
        {
            get
            {
                return this.ilist_0;
            }
        }

        public ITopology Topology
        {
            set
            {
                this.itopology_0 = value;
            }
        }

        protected class FeatureClassWrap
        {
            private IFeatureClass ifeatureClass_0 = null;

            public FeatureClassWrap(IFeatureClass ifeatureClass_1)
            {
                this.ifeatureClass_0 = ifeatureClass_1;
            }

            public override string ToString()
            {
                return this.ifeatureClass_0.AliasName;
            }

            public IFeatureClass FeatureClass
            {
                get
                {
                    return this.ifeatureClass_0;
                }
            }
        }
    }
}

