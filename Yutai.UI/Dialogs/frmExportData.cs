using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Carto;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Plugins.Services;
using Path = System.IO.Path;

namespace Yutai.UI.Dialogs
{
    public partial class frmExportData : Form
    {
        private IFeatureLayer m_pFeatureLayer;
        private IBasicMap m_pMap;

        public frmExportData()
        {
            InitializeComponent();
        }

        private void btnOutName_Click(object sender, EventArgs e)
        {
            frmExplorerData file = new frmExplorerData();
            IGxObjectFilter filter=new MyGxFilterShapefiles() as IGxObjectFilter;
            file.AddFilter(filter, false);
            file.AddFilter(new MyGxFilterFeatureClasses(), true);
            if (file.DoModalSave() == DialogResult.OK)
            {
                IArray items = file.Items;
                if (items.Count > 0)
                {
                    IGxObject obj2 = items.Element[0] as IGxObject;
                    if (obj2 is IGxDataset)
                    {
                        this.rdoSRType.Enabled = false;
                    }
                    else
                    {
                        this.rdoSRType.Enabled = true;
                    }
                    string saveName = file.SaveName;
                    if ((obj2 is IGxFolder) && (Path.GetExtension(file.SaveName) != ".shp"))
                    {
                        saveName = Path.GetFileNameWithoutExtension(file.SaveName) + ".shp";
                    }
                    this.txtOutName.Tag = obj2;
                    this.txtOutName.Text = obj2.FullName + @"\" + saveName;
                }
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            IName internalObjectName;
            IFields fields2;
            IEnumFieldError error;
            string fileNameWithoutExtension;
            string str4;
            string name;
            IField field;
            int num;
            IGeometryDefEdit geometryDef;
            Exception exception;
            IFeatureDataset dataset;
            if (this.txtOutName.Tag == null)
            {
                IGxFolder folder = new GxFolder();
                (folder as IGxFile).Path = Path.GetDirectoryName(this.txtOutName.Text);
                this.txtOutName.Tag = folder;
            }
            if (this.txtOutName.Tag == null)
            {
                return;
            }
            IGxObject tag = this.txtOutName.Tag as IGxObject;
            if (tag is IGxFolder)
            {
                IWorkspaceName name2 = new WorkspaceName() as IWorkspaceName;


                name2.WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory";
                name2.PathName = (tag.InternalObjectName as IFileName).Path;
                
                internalObjectName = name2 as IName;
                string text = this.txtOutName.Text;
                if (Path.GetExtension(this.txtOutName.Text) != ".shp")
                {
                    text = Path.GetFileNameWithoutExtension(this.txtOutName.Text) + ".shp";
                }
                if (File.Exists(text))
                {
                    MessageBox.Show("已经存在该shapefile文件，请重新输入文件名");
                    return;
                }
            }
            else
            {
                internalObjectName = tag.InternalObjectName;
            }
            IFields inputField = this.m_pFeatureLayer.FeatureClass.Fields;
            IFieldChecker checker = new FieldChecker();
            IFeatureClass class2 = null;
            if (internalObjectName is IWorkspaceName)
            {
                IFeatureWorkspace workspace = internalObjectName.Open() as IFeatureWorkspace;
                checker.ValidateWorkspace = workspace as IWorkspace;
                checker.Validate(inputField, out error, out fields2);
                fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.txtOutName.Text);
                checker.ValidateTableName(fileNameWithoutExtension, out str4);
                fileNameWithoutExtension = str4;
                ISpatialReference spatialReference = (this.m_pFeatureLayer.FeatureClass as IGeoDataset).SpatialReference;
                if (this.rdoSRType.SelectedIndex == 0)
                {
                    spatialReference = this.m_pMap.SpatialReference;
                }
                name = "";
                for (num = 0; num < fields2.FieldCount; num++)
                {
                    field = fields2.get_Field(num);
                    if (field.Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        geometryDef = (field as IFieldEdit).GeometryDef as IGeometryDefEdit;
                        geometryDef.SpatialReference_2 = spatialReference;
                        (field as IFieldEdit).GeometryDef_2 = geometryDef;
                        name = field.Name;
                        break;
                    }
                }
                try
                {
                    if ((workspace is IWorkspace2) && (workspace as IWorkspace2).get_NameExists(esriDatasetType.esriDTFeatureClass, fileNameWithoutExtension))
                    {
                        MessageBox.Show("已经存在该要素类，请重新输入要素类名");
                        return;
                    }
                    class2 = workspace.CreateFeatureClass(fileNameWithoutExtension, fields2, null, null, esriFeatureType.esriFTSimple, name, "");
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    MessageBox.Show("无法创建输出要素类!");
                    return;
                }
                goto Label_0400;
            }
            else
            {
                dataset = internalObjectName.Open() as IFeatureDataset;
                IWorkspace workspace2 = dataset.Workspace;
                checker.ValidateWorkspace = workspace2;
                checker.Validate(inputField, out error, out fields2);
                fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.txtOutName.Text);
                checker.ValidateTableName(fileNameWithoutExtension, out str4);
                fileNameWithoutExtension = str4;
                name = "";
                for (num = 0; num < fields2.FieldCount; num++)
                {
                    field = fields2.get_Field(num);
                    if (field.Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        geometryDef = (field as IFieldEdit).GeometryDef as IGeometryDefEdit;
                        geometryDef.SpatialReference_2 = (dataset as IGeoDataset).SpatialReference;
                        (field as IFieldEdit).GeometryDef_2 = geometryDef;
                        name = field.Name;
                        break;
                    }
                }
            }
            try
            {
                class2 = dataset.CreateFeatureClass(fileNameWithoutExtension, fields2, null, null, esriFeatureType.esriFTSimple, name, "");
            }
            catch (Exception exception2)
            {
                exception = exception2;
                MessageBox.Show("无法创建输出要素类!,原因:" + exception.Message);
               
                return;
            }
        Label_0400:
            if (this.cboExportData.SelectedIndex == 0)
            {
                ExportDataHelper.ExportData(this.m_pFeatureLayer, class2, false);
            }
            else if (this.cboExportData.SelectedIndex == 1)
            {
                ISpatialFilter queryFilter = new SpatialFilter()
                {
                    Geometry = (this.m_pMap as IActiveView).Extent
                };
                IFeatureCursor cursor = this.m_pFeatureLayer.Search(queryFilter, false);
                ExportDataHelper.ExportData(cursor, class2);
                ComReleaser.ReleaseCOMObject(cursor);
            }
            else
            {
                ExportDataHelper.ExportData(this.m_pFeatureLayer, class2, true);
            }
            base.DialogResult = DialogResult.OK;

        }

        private void frmExportData_Load(object sender, EventArgs e)
        {
            this.cboExportData.Items.Clear();
            this.cboExportData.Items.Add("所有要素");
            this.cboExportData.Items.Add("视图范围内的要素");
            if ((this.m_pFeatureLayer as IFeatureSelection).SelectionSet.Count > 0)
            {
                this.cboExportData.Items.Add("选中要素");
            }
            this.cboExportData.SelectedIndex = 0;
            string str = Path.GetTempPath() + @"\Export_Data";
            int num = 0;
            string path = str + ".shp";
            while (File.Exists(path))
            {
                num++;
                path = str + "_" + num.ToString() + ".shp";
            }
            this.txtOutName.Text = path;

        }

        public IFeatureLayer FeatureLayer
        {
            set
            {
                this.m_pFeatureLayer = value;
            }
        }

        public IBasicMap FocusMap
        {
            set
            {
                this.m_pMap = value;
            }
        }

    }
}
