using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class frmAddClass : Form
    {
        private Container container_0 = null;
        private IList ilist_0 = new ArrayList();
        private ITopology itopology_0 = null;

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

 private void frmAddClass_Load(object sender, EventArgs e)
        {
            this.method_0();
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

        protected partial class FeatureClassWrap
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

