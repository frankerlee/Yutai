using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class BufferOutputSetCtrl : UserControl
    {
        private Container container_0 = null;
        private SysGrants sysGrants_0 = new SysGrants();

        public BufferOutputSetCtrl()
        {
            this.InitializeComponent();
        }

        private void btnSelectInputFeatures_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile();
            file.AddFilter(new MyGxFilterFeatureClasses(), true);
            if (file.DoModalSave() == DialogResult.OK)
            {
                IArray items = file.Items;
                if (items.Count != 0)
                {
                    System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        IWorkspaceName name = null;
                        IGxObject obj2 = items.get_Element(0) as IGxObject;
                        string str = "";
                        string fullName = "";
                        if (obj2 is IGxDatabase)
                        {
                            BufferHelper.m_BufferHelper.m_pOutFeatureWorksapce = (obj2 as IGxDatabase).Workspace as IFeatureWorkspace;
                            fullName = obj2.FullName;
                        }
                        else if (obj2 is IGxFolder)
                        {
                            name = new WorkspaceNameClass {
                                WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory",
                                PathName = (obj2.InternalObjectName as IFileName).Path
                            };
                            BufferHelper.m_BufferHelper.m_pOutFeatureWorksapce = (name as IName).Open() as IFeatureWorkspace;
                            fullName = (obj2.InternalObjectName as IFileName).Path;
                            str = ".shp";
                        }
                        BufferHelper.m_BufferHelper.m_FeatClassName = file.SaveName;
                        this.txtOutName.Text = fullName + @"\" + file.SaveName + str;
                    }
                    catch (Exception exception)
                    {
                        CErrorLog.writeErrorLog(null, exception, "");
                    }
                    System.Windows.Forms.Cursor.Current = Cursors.Default;
                }
            }
        }

        private void BufferOutputSetCtrl_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void cboEditingPolygonLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboEditingPolygonLayer.SelectedIndex != -1)
            {
                BufferHelper.m_BufferHelper.m_pOutFC = ((this.cboEditingPolygonLayer.SelectedItem as ObjectWrap).Object as IFeatureLayer).FeatureClass;
            }
        }

 public void Init()
        {
            UID uid = new UIDClass {
                Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}"
            };
            IEnumLayer layer = BufferHelper.m_BufferHelper.m_pFocusMap.get_Layers(uid, true);
            this.cboEditingPolygonLayer.Properties.Items.Clear();
            layer.Reset();
            for (ILayer layer2 = layer.Next(); layer2 is IFeatureLayer; layer2 = layer.Next())
            {
                if ((((layer2 as IFeatureLayer).FeatureClass != null) && ((layer2 as IFeatureLayer).FeatureClass.FeatureType == esriFeatureType.esriFTSimple)) && this.method_0(layer2 as IFeatureLayer))
                {
                    this.cboEditingPolygonLayer.Properties.Items.Add(new ObjectWrap(layer2));
                }
            }
            this.cboEditingPolygonLayer.Enabled = false;
            if (this.cboEditingPolygonLayer.Properties.Items.Count > 0)
            {
                this.rdoSaveToEditingLayer.Enabled = true;
                this.cboEditingPolygonLayer.SelectedIndex = 0;
            }
            else
            {
                this.rdoSaveToEditingLayer.Enabled = false;
            }
            if (BufferHelper.m_BufferHelper.m_pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
            {
                this.rdoBufferType.Enabled = true;
            }
            else
            {
                this.rdoBufferType.Enabled = false;
            }
            this.txtOutName.Enabled = this.rdoNewLayer.Checked;
            this.btnSelectInputFeatures.Enabled = this.rdoNewLayer.Checked;
            this.cboEditingPolygonLayer.Enabled = this.rdoSaveToEditingLayer.Checked;
        }

 private bool method_0(IFeatureLayer ifeatureLayer_0)
        {
            return this.method_1(ifeatureLayer_0.FeatureClass as IDataset);
        }

        private bool method_1(IDataset idataset_0)
        {
            IWorkspace workspace = idataset_0.Workspace;
            if (!(workspace as IWorkspaceEdit).IsBeingEdited())
            {
                return false;
            }
            if ((workspace.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace) && (workspace is IVersionedWorkspace))
            {
                IVersionedObject obj2 = idataset_0 as IVersionedObject;
                if (obj2.IsRegisteredAsVersioned)
                {
                    return (((AppConfigInfo.UserID.Length == 0) || (AppConfigInfo.UserID.ToLower() == "admin")) || this.sysGrants_0.GetStaffAndRolesLayerPri(AppConfigInfo.UserID, 2, idataset_0.Name));
                }
            }
            return true;
        }

        private void rdoBufferType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BufferHelper.m_BufferHelper.m_PolygonType = this.rdoOutputType.SelectedIndex;
        }

        private void rdoNewLayer_Click(object sender, EventArgs e)
        {
            this.cboEditingPolygonLayer.Enabled = false;
            BufferHelper.m_BufferHelper.m_OutputType = 2;
            this.txtOutName.Enabled = true;
            this.btnSelectInputFeatures.Enabled = true;
            this.cboEditingPolygonLayer.Enabled = false;
        }

        private void rdoOutputType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BufferHelper.m_BufferHelper.bDissolve = this.rdoOutputType.SelectedIndex == 0;
        }

        private void rdoSaveToEditingLayer_Click(object sender, EventArgs e)
        {
            this.cboEditingPolygonLayer.Enabled = true;
            BufferHelper.m_BufferHelper.m_OutputType = 1;
            this.txtOutName.Enabled = false;
            this.btnSelectInputFeatures.Enabled = false;
            if (this.cboEditingPolygonLayer.SelectedIndex != -1)
            {
                BufferHelper.m_BufferHelper.m_pOutFC = ((this.cboEditingPolygonLayer.SelectedItem as ObjectWrap).Object as IFeatureLayer).FeatureClass;
            }
        }

        private void rdoSaveToGraphicLayer_Click(object sender, EventArgs e)
        {
            this.cboEditingPolygonLayer.Enabled = false;
            BufferHelper.m_BufferHelper.m_OutputType = 0;
            this.txtOutName.Enabled = false;
            this.btnSelectInputFeatures.Enabled = false;
            this.cboEditingPolygonLayer.Enabled = false;
        }
    }
}

