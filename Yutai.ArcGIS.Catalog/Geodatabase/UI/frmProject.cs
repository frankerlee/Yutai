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

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmProject : Form, IProjectForm
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private IGxDataset igxDataset_0 = null;
        private IName iname_0 = null;
        private int int_2 = 0;
        private int int_3 = 0;
        private ISpatialReference ispatialReference_0 = new UnknownCoordinateSystemClass();

        public frmProject()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if ((this.string_0 != null) && (this.string_0.Length != 0))
            {
                if (this.bool_0)
                {
                    MessageBox.Show("输出要素类已存在，请重新指定输出要素类名！");
                }
                else if (this.ispatialReference_0 == null)
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
                    double num = 1000.0;
                    this.string_0 = System.IO.Path.GetFileName(this.txtOutFeat.Text);
                    ISpatialReference spatialReference = ((IGeoDataset) this.idataset_0).SpatialReference;
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
                                extent = ((IGeoDataset) this.idataset_0).Extent;
                                extent.PutCoords(num2, num4, num3, num5);
                                extent.Project(this.ispatialReference_0);
                                if (!extent.IsEmpty)
                                {
                                    this.ispatialReference_0.SetDomain(extent.XMin, extent.XMax, extent.YMin,
                                        extent.YMax);
                                    num = extent.Width/2.0;
                                }
                            }
                            else if (spatialReference is IGeographicCoordinateSystem)
                            {
                                extent = ((IGeoDataset) this.idataset_0).Extent;
                                extent.Project(this.ispatialReference_0);
                                if (!extent.IsEmpty)
                                {
                                    this.ispatialReference_0.SetDomain(extent.XMin, extent.XMax, extent.YMin,
                                        extent.YMax);
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
                    this.progressBar1.Visible = true;
                    this.int_2 = this.int_3;
                    this.progressBar1.Value = this.int_3;
                    SRLibCommonFunc.m_pfrm = this;
                    if (this.idataset_0 is IFeatureClass)
                    {
                        SRLibCommonFunc.Project((IFeatureClass) this.idataset_0, this.ispatialReference_0,
                            this.iworkspace_0, this.string_0, num);
                    }
                    else if (this.idataset_0 is IFeatureDataset)
                    {
                        SRLibCommonFunc.Project((IFeatureDataset) this.idataset_0, this.ispatialReference_0,
                            this.iworkspace_0, this.string_0);
                    }
                    this.progressBar1.Visible = false;
                    this.string_0 = "";
                    this.idataset_0 = null;
                    this.iworkspace_0 = null;
                    this.txtInputFeat.Text = "";
                    this.txtOutFeat.Text = "";
                }
            }
        }

        private void btnSelectIn_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile
            {
                AllowMultiSelect = false,
                Text = "选择要素"
            };
            file.RemoveAllFilters();
            file.AddFilter(new MyGxFilterDatasets(), true);
            if (file.DoModalOpen() == DialogResult.OK)
            {
                IArray items = file.Items;
                if (items.Count != 0)
                {
                    this.igxDataset_0 = items.get_Element(0) as IGxDataset;
                    this.idataset_0 = this.igxDataset_0.Dataset;
                    this.txtInputFeat.Text = (this.igxDataset_0 as IGxObject).FullName;
                    this.iworkspace_0 = this.idataset_0.Workspace;
                    string tableName = this.idataset_0.Name + "_Project1";
                    IFieldChecker checker = new FieldCheckerClass
                    {
                        InputWorkspace = this.iworkspace_0
                    };
                    checker.ValidateTableName(tableName, out this.string_0);
                    this.txtOutFeat.Text = this.iworkspace_0.PathName + @"\" + this.string_0;
                    if ((this.iworkspace_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace) ||
                        (this.iworkspace_0.Type == esriWorkspaceType.esriLocalDatabaseWorkspace))
                    {
                        if (((IWorkspace2) this.iworkspace_0).get_NameExists(esriDatasetType.esriDTFeatureClass,
                            this.string_0))
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
            file.AddFilter(new MyGxFilterFeatureClasses(), true);
            if (file.DoModalSave() == DialogResult.OK)
            {
                IArray items = file.Items;
                if (items.Count != 0)
                {
                    IGxObject obj2 = items.get_Element(0) as IGxObject;
                    if (obj2 is IGxDatabase)
                    {
                        this.iname_0 = obj2.InternalObjectName;
                    }
                    else if (obj2 is IGxFolder)
                    {
                        IWorkspaceName name = new WorkspaceNameClass
                        {
                            WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory",
                            PathName = (obj2.InternalObjectName as IFileName).Path
                        };
                        this.iname_0 = name as IName;
                    }
                    else if (obj2 is IGxDataset)
                    {
                        return;
                    }
                    this.iworkspace_0 = this.iname_0.Open() as IWorkspace;
                    string saveName = file.SaveName;
                    IFieldChecker checker = new FieldCheckerClass
                    {
                        InputWorkspace = this.iworkspace_0
                    };
                    checker.ValidateTableName(saveName, out this.string_0);
                    this.txtOutFeat.Text = this.iworkspace_0.PathName + @"\" + this.string_0;
                    if ((this.iworkspace_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace) ||
                        (this.iworkspace_0.Type == esriWorkspaceType.esriLocalDatabaseWorkspace))
                    {
                        if (((IWorkspace2) this.iworkspace_0).get_NameExists(esriDatasetType.esriDTFeatureClass,
                            this.string_0))
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

        private bool method_0()
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

        private bool method_1()
        {
            return this.bool_1;
        }

        private void method_2(string string_1)
        {
        }

        private void method_3(int int_4)
        {
            this.int_1 = int_4;
            if (this.int_1 > 0)
            {
                this.progressBar1.Maximum = this.int_1;
            }
        }

        private void method_4(int int_4)
        {
            this.int_3 = int_4;
            this.progressBar1.Minimum = this.int_3;
        }

        private void method_5(int int_4)
        {
        }

        private void method_6(int int_4)
        {
            this.int_0 = int_4;
        }

        private void method_7()
        {
            this.int_2 += this.int_0;
            if (this.int_2 <= this.int_1)
            {
                this.progressBar1.Increment(this.int_0);
            }
        }

        private void txtOutFeat_TextChanged(object sender, EventArgs e)
        {
            if (this.method_0())
            {
                this.pictureBox1.Visible = true;
            }
            else
            {
                this.pictureBox1.Visible = false;
            }
        }

        public IFeatureDataConverter FeatureProgress
        {
            set
            {
                this.ifeatureProgress_Event_0 = (IFeatureProgress_Event) value;
                this.ifeatureProgress_Event_0.Step += (new IFeatureProgress_StepEventHandler(this.method_7));
            }
        }
    }
}