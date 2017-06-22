using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class ObjectClassGeneral : UserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private string string_0 = "";
        private string string_1 = "";

        public event ValueChangedHandler ValueChanged;

        public ObjectClassGeneral()
        {
            this.InitializeComponent();
        }

        private void cboFeatureType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboFeatureType.Enabled)
            {
                if (this.cboFeatureType.SelectedIndex == 0)
                {
                    if (ObjectClassHelper.m_pObjectClassHelper != null)
                    {
                        ObjectClassHelper.m_pObjectClassHelper.m_FeatreType = esriFeatureType.esriFTAnnotation;
                    }
                }
                else if (this.cboFeatureType.SelectedIndex == 1)
                {
                    this.chkRelateFeatureClass.Visible = false;
                    this.cboRelateFC.Visible = false;
                    if (ObjectClassHelper.m_pObjectClassHelper != null)
                    {
                        ObjectClassHelper.m_pObjectClassHelper.m_FeatreType = esriFeatureType.esriFTDimension;
                    }
                }
            }
        }

        private void cboRelateFC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboRelateFC.SelectedIndex != -1)
            {
                ObjectClassHelper.m_pObjectClassHelper.m_RelatedFeatureClass = (this.cboRelateFC.SelectedItem as DatasetWrap).Dataset as IFeatureClass;
            }
        }

        private void chkRelateFeatureClass_CheckedChanged(object sender, EventArgs e)
        {
            this.cboRelateFC.Visible = this.chkRelateFeatureClass.Checked;
            this.cboRelateFC.Enabled = true;
            ObjectClassHelper.m_pObjectClassHelper.IsRelatedFeature = this.chkRelateFeatureClass.Checked;
            if (this.chkRelateFeatureClass.Checked && (this.cboRelateFC.Properties.Items.Count == 0))
            {
                IEnumDataset subsets = (ObjectClassHelper.m_pObjectClassHelper.Workspace as IDataset).Subsets;
                subsets.Reset();
                for (IDataset dataset2 = subsets.Next(); dataset2 != null; dataset2 = subsets.Next())
                {
                    if (dataset2.Type == esriDatasetType.esriDTFeatureClass)
                    {
                        this.cboRelateFC.Properties.Items.Add(new DatasetWrap(dataset2));
                    }
                    else if (dataset2.Type == esriDatasetType.esriDTFeatureDataset)
                    {
                        IEnumDataset dataset3 = dataset2.Subsets;
                        dataset3.Reset();
                        for (IDataset dataset4 = dataset3.Next(); dataset4 != null; dataset4 = dataset3.Next())
                        {
                            this.cboRelateFC.Properties.Items.Add(new DatasetWrap(dataset4));
                        }
                    }
                }
                if (this.cboRelateFC.Properties.Items.Count > 0)
                {
                    this.cboRelateFC.SelectedIndex = 0;
                }
            }
        }

 private void ObjectClassGeneral_Load(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.txtName.Text = this.string_0;
                this.txtAliasName.Text = this.string_1;
                this.txtName.Enabled = false;
                this.radioGroup.Enabled = false;
                try
                {
                    if (!(!ObjectClassShareData.m_IsShapeFile && ObjectClassHelper.m_pObjectClassHelper.m_IsFeatureClass))
                    {
                        this.groupBox1.Visible = false;
                    }
                }
                catch
                {
                }
            }
            else if (!(!ObjectClassHelper.m_pObjectClassHelper.m_IsFeatureClass || ObjectClassShareData.m_IsShapeFile))
            {
                this.radioGroup.Enabled = true;
            }
            else
            {
                this.radioGroup.Enabled = false;
                this.groupBox1.Visible = false;
                this.label2.Visible = false;
                this.txtAliasName.Visible = false;
            }
            if (ObjectClassShareData.m_IsShapeFile)
            {
                this.txtAliasName.Enabled = false;
            }
            this.bool_1 = true;
        }

        private void radioGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.cboFeatureType.Enabled = this.radioGroup.SelectedIndex == 1;
                if (this.radioGroup.SelectedIndex == 0)
                {
                    if (ObjectClassHelper.m_pObjectClassHelper != null)
                    {
                        ObjectClassHelper.m_pObjectClassHelper.m_FeatreType = esriFeatureType.esriFTSimple;
                    }
                    this.cboRelateFC.Visible = false;
                    this.chkRelateFeatureClass.Visible = false;
                    this.cboFeatureType.Enabled = false;
                }
                else
                {
                    this.cboFeatureType.Enabled = true;
                    if (this.cboFeatureType.SelectedIndex == 0)
                    {
                        this.chkRelateFeatureClass.Visible = true;
                        this.cboRelateFC.Visible = this.chkRelateFeatureClass.Checked;
                        if (ObjectClassHelper.m_pObjectClassHelper != null)
                        {
                            ObjectClassHelper.m_pObjectClassHelper.m_FeatreType = esriFeatureType.esriFTAnnotation;
                        }
                    }
                    else if (this.cboFeatureType.SelectedIndex == 1)
                    {
                        this.chkRelateFeatureClass.Visible = false;
                        this.cboRelateFC.Visible = false;
                        if (ObjectClassHelper.m_pObjectClassHelper != null)
                        {
                            ObjectClassHelper.m_pObjectClassHelper.m_FeatreType = esriFeatureType.esriFTDimension;
                        }
                    }
                }
            }
        }

        private void txtAliasName_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.string_1 = this.txtAliasName.Text;
                if (ObjectClassHelper.m_pObjectClassHelper != null)
                {
                    ObjectClassHelper.m_pObjectClassHelper.AliasName = this.string_1;
                }
                if (this.ValueChanged != null)
                {
                    this.ValueChanged(this, e);
                }
            }
        }

        private void txtName_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_1 && (ObjectClassHelper.m_pObjectClassHelper != null))
            {
                ObjectClassHelper.m_pObjectClassHelper.Name = this.txtName.Text;
            }
        }

        public string AliasName
        {
            get
            {
                this.string_1 = this.txtAliasName.Text;
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public string ClassName
        {
            get
            {
                this.string_0 = this.txtName.Text;
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public bool IsEdit
        {
            set
            {
                this.bool_0 = value;
            }
        }

        internal partial class DatasetWrap
        {
            public IDataset Dataset = null;

            public DatasetWrap(IDataset idataset_0)
            {
                this.Dataset = idataset_0;
            }

            public override string ToString()
            {
                return this.Dataset.Name;
            }
        }
    }
}

