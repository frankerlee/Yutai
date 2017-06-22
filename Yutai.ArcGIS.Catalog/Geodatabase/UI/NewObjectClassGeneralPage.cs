using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class NewObjectClassGeneralPage : UserControl
    {
        private bool bool_0 = false;
        private IContainer icontainer_0 = null;
        private string string_0 = "";

        public event ValueChangedHandler ValueChanged;

        public NewObjectClassGeneralPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.txtName.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入要素类名称!");
                return false;
            }
            NewObjectClassHelper.m_pObjectClassHelper.Name = this.txtName.Text.Trim();
            NewObjectClassHelper.m_pObjectClassHelper.AliasName = this.txtAliasName.Text.Trim();
            if (NewObjectClassHelper.m_pObjectClassHelper.AliasName.Length == 0)
            {
                NewObjectClassHelper.m_pObjectClassHelper.AliasName = NewObjectClassHelper.m_pObjectClassHelper.Name;
            }
            switch (this.cboFeatureType.SelectedIndex)
            {
                case 0:
                    NewObjectClassHelper.m_pObjectClassHelper.FeatureType = esriFeatureType.esriFTSimple;
                    NewObjectClassHelper.m_pObjectClassHelper.ShapeType = esriGeometryType.esriGeometryPoint;
                    break;

                case 1:
                    NewObjectClassHelper.m_pObjectClassHelper.FeatureType = esriFeatureType.esriFTSimple;
                    NewObjectClassHelper.m_pObjectClassHelper.ShapeType = esriGeometryType.esriGeometryMultipoint;
                    break;

                case 2:
                    NewObjectClassHelper.m_pObjectClassHelper.FeatureType = esriFeatureType.esriFTSimple;
                    NewObjectClassHelper.m_pObjectClassHelper.ShapeType = esriGeometryType.esriGeometryPolyline;
                    break;

                case 3:
                    NewObjectClassHelper.m_pObjectClassHelper.FeatureType = esriFeatureType.esriFTSimple;
                    NewObjectClassHelper.m_pObjectClassHelper.ShapeType = esriGeometryType.esriGeometryPolygon;
                    break;

                case 4:
                    NewObjectClassHelper.m_pObjectClassHelper.FeatureType = esriFeatureType.esriFTSimple;
                    NewObjectClassHelper.m_pObjectClassHelper.ShapeType = esriGeometryType.esriGeometryMultiPatch;
                    break;

                case 5:
                    NewObjectClassHelper.m_pObjectClassHelper.FeatureType = esriFeatureType.esriFTAnnotation;
                    NewObjectClassHelper.m_pObjectClassHelper.ShapeType = esriGeometryType.esriGeometryPolygon;
                    NewObjectClassHelper.m_pObjectClassHelper.IsRelatedFeature = this.chkRelateFeatureClass.Checked;
                    if (!this.chkRelateFeatureClass.Checked || (this.cboRelateFC.SelectedIndex == -1))
                    {
                        NewObjectClassHelper.m_pObjectClassHelper.RelatedFeatureClass = null;
                        break;
                    }
                    NewObjectClassHelper.m_pObjectClassHelper.RelatedFeatureClass = (this.cboRelateFC.SelectedItem as ObjectWrap).Object as IFeatureClass;
                    break;

                case 6:
                    NewObjectClassHelper.m_pObjectClassHelper.FeatureType = esriFeatureType.esriFTDimension;
                    NewObjectClassHelper.m_pObjectClassHelper.ShapeType = esriGeometryType.esriGeometryPolygon;
                    break;
            }
            NewObjectClassHelper.m_pObjectClassHelper.HasM = this.chkHasM.Checked;
            NewObjectClassHelper.m_pObjectClassHelper.HasZ = this.chkHasZ.Checked;
            return true;
        }

        private void cboFeatureType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool flag = this.cboFeatureType.SelectedIndex == 5;
            this.cboRelateFC.Visible = flag;
            this.chkRelateFeatureClass.Visible = flag;
            if ((this.cboFeatureType.SelectedIndex == 5) || (this.cboFeatureType.SelectedIndex == 6))
            {
                this.chkHasM.Enabled = false;
                this.chkHasZ.Enabled = false;
                this.chkHasM.Checked = false;
                this.chkHasZ.Checked = false;
            }
            else if (this.cboFeatureType.SelectedIndex == 4)
            {
                this.chkHasM.Enabled = true;
                this.chkHasZ.Enabled = false;
                this.chkHasM.Checked = false;
                this.chkHasZ.Checked = true;
            }
            else
            {
                this.chkHasM.Enabled = true;
                this.chkHasZ.Enabled = true;
                this.chkHasM.Checked = false;
                this.chkHasZ.Checked = false;
            }
        }

        private void chkRelateFeatureClass_CheckedChanged(object sender, EventArgs e)
        {
            this.cboRelateFC.Enabled = (this.cboRelateFC.Properties.Items.Count > 0) && this.chkRelateFeatureClass.Checked;
        }

 private void method_0(IList ilist_0, IWorkspace iworkspace_0)
        {
            IEnumDataset dataset = iworkspace_0.get_Datasets(esriDatasetType.esriDTAny);
            dataset.Reset();
            for (IDataset dataset2 = dataset.Next(); dataset2 != null; dataset2 = dataset.Next())
            {
                if (dataset2.Type == esriDatasetType.esriDTFeatureClass)
                {
                    if ((dataset2 as IFeatureClass).FeatureType == esriFeatureType.esriFTSimple)
                    {
                        ilist_0.Add(new ObjectWrap(dataset2));
                    }
                }
                else if (dataset2.Type == esriDatasetType.esriDTFeatureDataset)
                {
                    IEnumDataset subsets = dataset2.Subsets;
                    subsets.Reset();
                    for (IDataset dataset4 = subsets.Next(); dataset4 != null; dataset4 = subsets.Next())
                    {
                        if ((dataset4.Type == esriDatasetType.esriDTFeatureClass) && ((dataset4 as IFeatureClass).FeatureType == esriFeatureType.esriFTSimple))
                        {
                            ilist_0.Add(new ObjectWrap(dataset4));
                        }
                    }
                }
            }
        }

        private void method_1(IList ilist_0, IWorkspace iworkspace_0)
        {
            IEnumDatasetName name = iworkspace_0.get_DatasetNames(esriDatasetType.esriDTAny);
            name.Reset();
            for (IDatasetName name2 = name.Next(); name2 != null; name2 = name.Next())
            {
                if (name2.Type == esriDatasetType.esriDTFeatureClass)
                {
                    if ((name2 as IFeatureClassName).FeatureType == esriFeatureType.esriFTSimple)
                    {
                        ilist_0.Add(new ObjectWrap(name2));
                    }
                }
                else if (name2.Type == esriDatasetType.esriDTFeatureDataset)
                {
                    IEnumDatasetName subsetNames = name2.SubsetNames;
                    subsetNames.Reset();
                    for (IDatasetName name4 = subsetNames.Next(); name4 != null; name4 = subsetNames.Next())
                    {
                        if ((name4.Type == esriDatasetType.esriDTFeatureClass) && ((name4 as IFeatureClassName).FeatureType == esriFeatureType.esriFTSimple))
                        {
                            ilist_0.Add(new ObjectWrap(name4));
                        }
                    }
                }
            }
        }

        private void NewObjectClassGeneralPage_Load(object sender, EventArgs e)
        {
            if (!NewObjectClassHelper.m_pObjectClassHelper.IsEdit)
            {
                this.cboFeatureType.SelectedIndex = 3;
                this.method_0(this.cboRelateFC.Properties.Items, NewObjectClassHelper.m_pObjectClassHelper.Workspace);
                if (this.cboRelateFC.Properties.Items.Count == 0)
                {
                    this.cboRelateFC.SelectedIndex = 0;
                }
                this.cboRelateFC.Enabled = (this.cboRelateFC.Properties.Items.Count > 0) && this.chkRelateFeatureClass.Checked;
                this.chkRelateFeatureClass.Enabled = this.cboRelateFC.Properties.Items.Count > 0;
            }
            else
            {
                this.txtName.Text = NewObjectClassHelper.m_pObjectClassHelper.Name;
                this.txtName.Properties.ReadOnly = true;
                this.txtAliasName.Text = NewObjectClassHelper.m_pObjectClassHelper.AliasName;
                this.cboFeatureType.Enabled = false;
                if (!NewObjectClassHelper.m_pObjectClassHelper.IsFeatureClass)
                {
                    this.groupBox1.Visible = false;
                    this.groupBox1.Visible = false;
                }
                else
                {
                    if (NewObjectClassHelper.m_pObjectClassHelper.FeatureType == esriFeatureType.esriFTSimple)
                    {
                        switch (NewObjectClassHelper.m_pObjectClassHelper.ShapeType)
                        {
                            case esriGeometryType.esriGeometryPoint:
                                this.cboFeatureType.SelectedIndex = 0;
                                break;

                            case esriGeometryType.esriGeometryMultipoint:
                                this.cboFeatureType.SelectedIndex = 1;
                                break;

                            case esriGeometryType.esriGeometryPolyline:
                                this.cboFeatureType.SelectedIndex = 2;
                                break;

                            case esriGeometryType.esriGeometryPolygon:
                                this.cboFeatureType.SelectedIndex = 3;
                                break;

                            case esriGeometryType.esriGeometryAny:
                                this.cboFeatureType.SelectedIndex = -1;
                                break;

                            case esriGeometryType.esriGeometryMultiPatch:
                                this.cboFeatureType.SelectedIndex = 4;
                                break;
                        }
                    }
                    else if (NewObjectClassHelper.m_pObjectClassHelper.FeatureType == esriFeatureType.esriFTAnnotation)
                    {
                        this.cboFeatureType.SelectedIndex = 5;
                    }
                    else
                    {
                        this.cboFeatureType.SelectedIndex = 6;
                    }
                    this.chkHasZ.Checked = NewObjectClassHelper.m_pObjectClassHelper.HasZ;
                    this.chkHasZ.Enabled = false;
                    this.chkHasM.Checked = NewObjectClassHelper.m_pObjectClassHelper.HasM;
                    this.chkHasM.Enabled = false;
                }
            }
            this.bool_0 = true;
        }

        private void txtAliasName_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0 && (this.ValueChanged != null))
            {
                this.ValueChanged(this, new EventArgs());
            }
        }

        public string AliasName
        {
            get
            {
                this.string_0 = this.txtAliasName.Text;
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}

