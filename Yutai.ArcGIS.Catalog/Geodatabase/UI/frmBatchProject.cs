using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmBatchProject : Form, IProjectForm
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IArray iarray_0 = new ArrayClass();
        private IArray iarray_1 = new ArrayClass();
        private IGxDataset igxDataset_0 = null;
        private IGxObject igxObject_0 = null;
        private IName iname_0 = null;
        private IName iname_1 = null;
        private int int_2 = 0;
        private int int_3 = 0;
        private ISpatialReference ispatialReference_0 = new UnknownCoordinateSystemClass();

        public frmBatchProject()
        {
            this.InitializeComponent();
        }

        public void Add(IGxObjectFilter igxObjectFilter_0)
        {
            if (igxObjectFilter_0 != null)
            {
                this.iarray_1.Add(igxObjectFilter_0);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = this.listView1.Items.Count - 1; i >= 0; i--)
            {
                if (this.listView1.Items[i].Selected)
                {
                    this.iarray_0.Remove(i);
                    this.listView1.Items.RemoveAt(i);
                }
            }
        }

        private void btnInputFeatClassProperty_Click(object sender, EventArgs e)
        {
            IDatasetName name = this.iarray_0.get_Element(this.listView1.SelectedIndices[0]) as IDatasetName;
            if (name != null)
            {
                IGeoDataset dataset = (name as IName).Open() as IGeoDataset;
                if (dataset != null)
                {
                    new frmSpatialReference {SpatialRefrence = dataset.SpatialReference, IsEdit = false}.ShowDialog();
                }
                dataset = null;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.iname_0 != null)
            {
                if (this.ispatialReference_0 == null)
                {
                    MessageBox.Show("请输入投影坐标系！");
                }
                else if (this.ispatialReference_0 is IGeographicCoordinateSystem)
                {
                    MessageBox.Show("请输入投影坐标系！");
                }
                else if (this.ispatialReference_0 is IUnknownCoordinateSystem)
                {
                    MessageBox.Show("请输入投影坐标系！");
                }
                else
                {
                    int num;
                    string name;
                    this.progressBar1.Visible = true;
                    this.btnOK.Enabled = false;
                    this.btnCancel.Enabled = false;
                    for (num = 0; num < this.iarray_0.Count; num++)
                    {
                        name = (this.iarray_0.get_Element(num) as IDatasetName).Name;
                        if (!this.method_0(this.iarray_0.get_Element(num) as IName, name))
                        {
                            this.progressBar1.Visible = false;
                            this.btnOK.Enabled = true;
                            this.btnCancel.Enabled = true;
                            return;
                        }
                    }
                    for (num = 0; num < this.iarray_0.Count; num++)
                    {
                        name = (this.iarray_0.get_Element(num) as IDatasetName).Name;
                        this.method_1(this.iarray_0.get_Element(num) as IName, name);
                    }
                    this.progressBar1.Visible = false;
                    this.listView1.Items.Clear();
                    this.iarray_0.RemoveAll();
                    base.Close();
                }
            }
        }

        private void btnSelectInputFeatures_Click(object sender, EventArgs e)
        {
            int num;
            frmOpenFile file = new frmOpenFile
            {
                Text = "添加数据"
            };
            file.RemoveAllFilters();
            file.AllowMultiSelect = true;
            if (this.iarray_1.Count != 0)
            {
                for (num = 0; num < this.iarray_1.Count; num++)
                {
                    if (num == 0)
                    {
                        file.AddFilter(this.iarray_1.get_Element(num) as IGxObjectFilter, true);
                    }
                    else
                    {
                        file.AddFilter(this.iarray_1.get_Element(num) as IGxObjectFilter, false);
                    }
                }
            }
            else
            {
                file.AddFilter(new MyGxFilterDatasets(), true);
            }
            if (file.DoModalOpen() == DialogResult.OK)
            {
                IArray items = file.Items;
                if (items.Count != 0)
                {
                    for (num = 0; num < items.Count; num++)
                    {
                        IGxObject obj2 = items.get_Element(num) as IGxObject;
                        this.iarray_0.Add(obj2.InternalObjectName);
                        this.listView1.Items.Add(obj2.FullName);
                    }
                }
            }
        }

        private void btnSelectOut_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile
            {
                Text = "保存位置"
            };
            file.RemoveAllFilters();
            file.AddFilter(new MyGxFilterWorkspaces(), true);
            file.AddFilter(new MyGxFilterFeatureDatasets(), false);
            if (file.DoModalSaveLocation() == DialogResult.OK)
            {
                IArray items = file.Items;
                if (items.Count != 0)
                {
                    this.igxObject_0 = items.get_Element(0) as IGxObject;
                    this.iname_0 = this.igxObject_0.InternalObjectName;
                    if (this.igxObject_0 is IGxDatabase)
                    {
                        this.iname_0 = this.igxObject_0.InternalObjectName;
                        this.iworkspace_0 = this.iname_0.Open() as IWorkspace;
                        this.btnSR.Enabled = true;
                    }
                    else if (this.igxObject_0 is IGxFolder)
                    {
                        IWorkspaceName name = new WorkspaceNameClass
                        {
                            WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory",
                            PathName = (this.igxObject_0.InternalObjectName as IFileName).Path
                        };
                        this.iname_0 = name as IName;
                        this.iworkspace_0 = this.iname_0.Open() as IWorkspace;
                        this.btnSR.Enabled = true;
                    }
                    else if (this.igxObject_0 is IGxDataset)
                    {
                        IDatasetName internalObjectName = this.igxObject_0.InternalObjectName as IDatasetName;
                        if (internalObjectName.Type != esriDatasetType.esriDTFeatureDataset)
                        {
                            return;
                        }
                        this.iname_0 = internalObjectName as IName;
                        this.btnSR.Enabled = false;
                        IGeoDataset dataset = (internalObjectName as IName).Open() as IGeoDataset;
                        this.iworkspace_0 = (dataset as IDataset).Workspace;
                        this.ispatialReference_0 = dataset.SpatialReference;
                        this.txtOutSR.Text = this.ispatialReference_0.Name;
                    }
                    this.txtOutFeat.Text = this.igxObject_0.FullName;
                }
            }
        }

        private void btnSR_Click(object sender, EventArgs e)
        {
            frmSpatialReference reference = new frmSpatialReference
            {
                SpatialRefrence = this.ispatialReference_0
            };
            if (reference.ShowDialog() == DialogResult.OK)
            {
                this.ispatialReference_0 = reference.SpatialRefrence;
                this.txtOutSR.Text = this.ispatialReference_0.Name;
            }
        }

        public void Clear()
        {
            this.iarray_1.RemoveAll();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                this.btnDelete.Enabled = true;
                this.btnInputFeatClassProperty.Enabled = true;
            }
            else
            {
                this.btnDelete.Enabled = false;
                this.btnInputFeatClassProperty.Enabled = false;
            }
        }

        private bool method_0(IName iname_2, string string_1)
        {
            try
            {
                IDataset dataset = iname_2.Open() as IDataset;
                ISpatialReference spatialReference = ((IGeoDataset) dataset).SpatialReference;
                if (spatialReference.HasXYPrecision())
                {
                    double num;
                    double num2;
                    double num3;
                    double num4;
                    spatialReference.GetDomain(out num, out num2, out num3, out num4);
                    new PointClass();
                    if (!(spatialReference is IUnknownCoordinateSystem))
                    {
                        IEnvelope extent;
                        if (spatialReference is IProjectedCoordinateSystem)
                        {
                            extent = ((IGeoDataset) dataset).Extent;
                            extent.PutCoords(num, num3, num2, num4);
                            extent.Project(this.ispatialReference_0);
                            if (extent.IsEmpty)
                            {
                                return
                                (MessageBox.Show(dataset.Name + "不能向该投影转换，是否继续后续要素类投影变换?", "投影",
                                     MessageBoxButtons.YesNo) == DialogResult.Yes);
                            }
                        }
                        else if (spatialReference is IGeographicCoordinateSystem)
                        {
                            extent = ((IGeoDataset) dataset).Extent;
                            extent.Project(this.ispatialReference_0);
                            if (extent.IsEmpty)
                            {
                                return
                                (MessageBox.Show(dataset.Name + "不能向该投影转换，是否继续后续要素类投影变换?", "投影",
                                     MessageBoxButtons.YesNo) == DialogResult.Yes);
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show("无法对" + (iname_2 as IDatasetName).Name + "作变换,请查看错误日志文件!");
                Logger.Current.Error("", exception, "");
            }
            return false;
        }

        private void method_1(IName iname_2, string string_1)
        {
            try
            {
                double num = 1000.0;
                IDataset dataset = iname_2.Open() as IDataset;
                ISpatialReference spatialReference = ((IGeoDataset) dataset).SpatialReference;
                (this.ispatialReference_0 as IControlPrecision2).IsHighPrecision =
                    (spatialReference as IControlPrecision2).IsHighPrecision;
                if (spatialReference.HasXYPrecision())
                {
                    double num2;
                    double num3;
                    double num4;
                    double num5;
                    spatialReference.GetDomain(out num2, out num3, out num4, out num5);
                    new PointClass();
                    if (!(spatialReference is IUnknownCoordinateSystem))
                    {
                        IEnvelope extent;
                        if (spatialReference is IProjectedCoordinateSystem)
                        {
                            extent = ((IGeoDataset) dataset).Extent;
                            extent.PutCoords(num2, num4, num3, num5);
                            extent.Project(this.ispatialReference_0);
                            if (!extent.IsEmpty)
                            {
                                this.ispatialReference_0.SetDomain(extent.XMin, extent.XMax, extent.YMin, extent.YMax);
                                num = extent.Width/2.0;
                            }
                        }
                        else if (spatialReference is IGeographicCoordinateSystem)
                        {
                            extent = ((IGeoDataset) dataset).Extent;
                            extent.Project(this.ispatialReference_0);
                            if (!extent.IsEmpty)
                            {
                                this.ispatialReference_0.SetDomain(extent.XMin, extent.XMax, extent.YMin, extent.YMax);
                                num = extent.Width/2.0;
                            }
                        }
                    }
                }
                if (spatialReference.HasZPrecision())
                {
                    double num6;
                    double num7;
                    spatialReference.GetZDomain(out num6, out num7);
                    this.ispatialReference_0.SetZDomain(num6, num7);
                }
                if (spatialReference.HasMPrecision())
                {
                    double num8;
                    double num9;
                    spatialReference.GetZDomain(out num8, out num9);
                    this.ispatialReference_0.SetZDomain(num8, num9);
                }
                SpatialReferenctOperator.ChangeCoordinateSystem(this.iworkspace_0 as IGeodatabaseRelease,
                    this.ispatialReference_0, false);
                this.int_2 = this.int_3;
                this.progressBar1.Value = this.int_3;
                SRLibCommonFunc.m_pfrm = this;
                if (dataset is IFeatureClass)
                {
                    if (this.iname_0 is IFeatureDatasetName)
                    {
                        SRLibCommonFunc.Project((IFeatureClass) dataset, this.ispatialReference_0,
                            this.iname_0 as IFeatureDatasetName, string_1, num);
                    }
                    else
                    {
                        SRLibCommonFunc.Project((IFeatureClass) dataset, this.ispatialReference_0, this.iworkspace_0,
                            string_1, num);
                    }
                }
                else if (dataset is IFeatureDataset)
                {
                    SRLibCommonFunc.Project((IFeatureDataset) dataset, this.ispatialReference_0, this.iworkspace_0,
                        string_1);
                }
                dataset = null;
            }
            catch (Exception exception)
            {
                MessageBox.Show("无法对" + (iname_2 as IDatasetName).Name + "作变换,请查看错误日志文件!");
                Logger.Current.Error("", exception, "");
            }
        }

        private bool method_2()
        {
            if (this.iworkspace_0 == null)
            {
                this.bool_0 = false;
            }
            else if ((this.iworkspace_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace) ||
                     (this.iworkspace_0.Type == esriWorkspaceType.esriLocalDatabaseWorkspace))
            {
                if (((IWorkspace2) this.iworkspace_0).get_NameExists(esriDatasetType.esriDTFeatureClass, this.string_0))
                {
                    this.bool_0 = true;
                }
                else
                {
                    this.bool_0 = false;
                }
            }
            else if (this.iworkspace_0.Type == esriWorkspaceType.esriFileSystemWorkspace)
            {
                if (File.Exists(this.txtOutFeat.Text + ".shp"))
                {
                    this.bool_0 = true;
                }
                else
                {
                    this.bool_0 = false;
                }
            }
            return this.bool_0;
        }

        private bool method_3()
        {
            return this.bool_1;
        }

        private void method_4(string string_1)
        {
        }

        private void method_5(int int_4)
        {
            this.int_1 = int_4;
            if (this.int_1 > 0)
            {
                this.progressBar1.Maximum = this.int_1;
            }
        }

        private void method_6(int int_4)
        {
            this.int_3 = int_4;
            this.progressBar1.Minimum = this.int_3;
        }

        private void method_7(int int_4)
        {
        }

        private void method_8(int int_4)
        {
            this.int_0 = int_4;
        }

        private void method_9()
        {
            this.int_2 += this.int_0;
            if (this.int_2 <= this.int_1)
            {
                this.progressBar1.Increment(this.int_0);
            }
        }

        private void txtOutFeat_TextChanged(object sender, EventArgs e)
        {
        }

        public IFeatureDataConverter FeatureProgress
        {
            set
            {
                this.ifeatureProgress_Event_0 = (IFeatureProgress_Event) value;
                this.ifeatureProgress_Event_0.Step += (new IFeatureProgress_StepEventHandler(this.method_9));
            }
        }
    }
}