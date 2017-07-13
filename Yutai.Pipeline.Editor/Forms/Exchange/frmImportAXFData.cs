using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Editor.Axf;
using Yutai.Pipeline.Editor.Helper;
using Yutai.Pipeline.Editor.SqlCe;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Editor.Forms.Exchange
{
    public partial class FrmImportAXFData : Form
    {
        private SqlCeDb db = new SqlCeDb(); 
        private List<AxfInfo> _axfInfoList;
        private IMap _map;
        public FrmImportAXFData(IAppContext context)
        {
            _map = context.FocusMap;
            InitializeComponent();
            Text = string.Format("{0}({1})", Text, FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion);
            this.ucTargetFeatureClass.Map = _map;
        }

        private void btnReadData_Click(object sender, EventArgs e)
        {
            Reset();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(ucAxfFolder.SelectedPath))
                    return;
                if (ucTargetFeatureClass.SelectFeatureLayer == null)
                    return;

                _axfInfoList = new List<AxfInfo>();
                List<FileInfo> axfFileInfos = FolderHelper.GetFileInfos(this.ucAxfFolder.SelectedPath,
                    new List<string>() {".axf"});

                foreach (FileInfo axfFileInfo in axfFileInfos)
                {
                    if (axfFileInfo.Name.StartsWith("BackgroundLayers"))
                        continue;

                    if (db.Open(axfFileInfo.FullName))
                    {
                        System.Data.SqlServerCe.SqlCeResultSet pSqlCeResultSet = db.GetTableData("AXF_TABLES", null, SortOrder.None) as System.Data.SqlServerCe.SqlCeResultSet;
                        dataGridView1.DataSource = pSqlCeResultSet;
                        List<AxfTables> pAxfTablesList = AxfTables.GetAxfTablesList(pSqlCeResultSet, dataGridView1);
                        foreach (AxfTables axfTables in pAxfTablesList)
                        {
                            if (axfTables.TableName != this.ucTargetFeatureClass.SelectFeatureLayer.Name)
                                continue;
                            pSqlCeResultSet = db.GetTableData(axfTables.TableName, null, SortOrder.None) as System.Data.SqlServerCe.SqlCeResultSet;
                            dataGridView1.DataSource = pSqlCeResultSet;
                            List<AxfField> pAxfFieldList = AxfField.GetAxfFieldList(pSqlCeResultSet, dataGridView1);
                            AxfInfo pAxfInfo = new AxfInfo
                            {
                                AddCount = pAxfFieldList.Count(c => c.AxfStatus == 1),
                                ModifyCount = pAxfFieldList.Count(c => c.AxfStatus == 2),
                                DeleteCount = pAxfFieldList.Count(c => c.AxfStatus == 128),
                                NoEditCount = pAxfFieldList.Count(c => c.AxfStatus == 0),
                                AxfFieldList = pAxfFieldList,
                                SourceLayerName = axfTables.TableName
                            };
                            IFeatureClass sourceFeatureClass = FeatureClassUtil.GetFeatureClass(axfFileInfo.FullName, axfTables.TableName);
                            if (sourceFeatureClass == null)
                                pAxfInfo.IsSelect = false;
                            else
                                pAxfInfo.IsSelect = true;
                            pAxfInfo.SourceFeatureClass = sourceFeatureClass;
                            pAxfInfo.TargetLayerName = this.ucTargetFeatureClass.SelectFeatureLayer.Name;
                            pAxfInfo.TargetFeatureLayer = this.ucTargetFeatureClass.SelectFeatureLayer;
                            pAxfInfo.AxfFilePath = axfFileInfo.FullName;

                            _axfInfoList.Add(pAxfInfo);
                        }

                        db.Close();
                    }
                }
                dataGridView2.DataSource = _axfInfoList;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            this.ckbAdd.Checked = true;
            this.ckbModify.Checked = true;
            this.ckbDelete.Checked = true;
            this.ckbNoEdit.Checked = true;
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            this.ckbAdd.Checked = false;
            this.ckbModify.Checked = false;
            this.ckbDelete.Checked = false;
            this.ckbNoEdit.Checked = false;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                //IWorkspace pWorkspace = GxDialogHelper.SelectWorkspaceDialog();
                //if (pWorkspace == null)
                //    return;
                //foreach (AxfInfo axfInfo in _axfInfoList)
                //{
                //    if (axfInfo.IsSelect == false)
                //        continue;
                //    if (WorkspaceHelper.Exist(pWorkspace, axfInfo.TableName))
                //    {
                //        throw new Exception(string.Format("要素类[{0}]已存在", axfInfo.TableName));
                //    }
                //    IFeatureClass pFeatureClass = WorkspaceHelper.CreateFeatureClassByAxfFeatureLayer(pWorkspace,
                //        axfInfo.SourceFeatureLayer, true);
                //    List<AxfOperationType> types = GetAxfOperationTypes();
                //    List<AxfField> tempAxfFields = new List<AxfField>();
                //    foreach (AxfField axfField in axfInfo.AxfFieldList)
                //    {
                //        if (types.Contains((AxfOperationType)axfField.AxfStatus) && (tempAxfFields.FirstOrDefault(c => c.Objectid == axfField.Objectid) == null))
                //            tempAxfFields.Add(axfField);
                //    }
                //    foreach (AxfField tempAxfField in tempAxfFields)
                //    {
                //        WorkspaceHelper.FeatureClassToFeatureClass(pFeatureClass, axfInfo.SourceFeatureLayer.FeatureClass, tempAxfField.Objectid, tempAxfField.AxfStatus);
                //        WorkspaceHelper.LoadFeatureClass(_map, pFeatureClass);
                //    }
                //    MessageBox.Show("转出成功！");
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                //foreach (AxfInfo axfInfo in _axfInfoList)
                //{
                //    if (axfInfo.IsSelect == false)
                //        continue;
                //    ImportAxfInfo(axfInfo, GetAxfOperationTypes());
                //}
                //MessageBox.Show("导入成功！");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (AxfInfo axfInfo in _axfInfoList)
                {
                    if (axfInfo.IsSelect == false)
                        continue;
                    if (axfInfo.SourceFeatureClass == null || axfInfo.TargetFeatureLayer == null)
                        continue;
                    if (this.ckbAdd.Checked)
                        ImportAxfInfoForAdd(axfInfo);
                    if (this.ckbModify.Checked)
                        ImportAxfInfoForModify(axfInfo);
                    if (this.ckbDelete.Checked)
                        ImportAxfInfoForDelete(axfInfo);
                }
                MessageBox.Show("签入成功！");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Reset();
            this.Close();
        }

        private void LoadDatabase(string fileName)
        {
            Reset();
            try
            {
                if (!File.Exists(fileName))
                    throw new Exception(string.Format(GlobalText.GetValue("FileNotFound") + ": '{0}'", fileName));

                this.Cursor = Cursors.WaitCursor;

                if (db.Open(fileName))
                {
                    System.Data.SqlServerCe.SqlCeResultSet pSqlCeResultSet = db.GetTableData("AXF_TABLES", null, SortOrder.None) as System.Data.SqlServerCe.SqlCeResultSet;
                    dataGridView1.DataSource = pSqlCeResultSet;
                    List<AxfTables> pAxfTablesList = AxfTables.GetAxfTablesList(pSqlCeResultSet, dataGridView1);
                    pSqlCeResultSet = db.GetTableData("GEOMETRY_COLUMNS", null, SortOrder.None) as System.Data.SqlServerCe.SqlCeResultSet;
                    dataGridView1.DataSource = pSqlCeResultSet;
                    List<AXF_GEOMETRY_COLUMNS> pAxfGeometryColumnss =
                        AXF_GEOMETRY_COLUMNS.GetAxfGeometryColumns(pSqlCeResultSet, dataGridView1);
                    pSqlCeResultSet = db.GetTableData("AXF_LAYERS", null, SortOrder.None) as System.Data.SqlServerCe.SqlCeResultSet;
                    dataGridView1.DataSource = pSqlCeResultSet;
                    List<AXF_LAYERS> pAxfLayerss = AXF_LAYERS.GetAxfLayerss(pSqlCeResultSet, dataGridView1);
                    _axfInfoList = new List<AxfInfo>();
                    foreach (AxfTables axfTables in pAxfTablesList)
                    {
                        pSqlCeResultSet = db.GetTableData(axfTables.TableName, null, SortOrder.None) as System.Data.SqlServerCe.SqlCeResultSet;
                        dataGridView1.DataSource = pSqlCeResultSet;
                        List<AxfField> pAxfFieldList = AxfField.GetAxfFieldList(pSqlCeResultSet, dataGridView1);
                        AxfInfo pAxfInfo = new AxfInfo
                        {
                            AddCount = pAxfFieldList.Count(c => c.AxfStatus == 1),
                            ModifyCount = pAxfFieldList.Count(c => c.AxfStatus == 2),
                            DeleteCount = pAxfFieldList.Count(c => c.AxfStatus == 128),
                            NoEditCount = pAxfFieldList.Count(c => c.AxfStatus == 0),
                            AxfFieldList = pAxfFieldList,
                            SourceLayerName = axfTables.TableName
                        };

                        AXF_GEOMETRY_COLUMNS axfGeometryColumns = pAxfGeometryColumnss.FirstOrDefault(c => c.TableName == pAxfInfo.SourceLayerName);
                        if (axfGeometryColumns == null)
                            return;
                        int pAxfTableId = axfGeometryColumns.TableId;
                        AXF_LAYERS axfLayers = pAxfLayerss.FirstOrDefault(c => c.TableId == pAxfTableId);
                        if (axfLayers == null)
                            return;
                        string layerName = axfLayers.Name;

                        //IFeatureLayer tempFeatureLayer = _featureLayerList.FirstOrDefault(c => c.Name == layerName);
                        //if (tempFeatureLayer != null)
                        //    if (CheckIsAxfLayer(tempFeatureLayer))
                        //    {
                        //        pAxfInfo.SourceLayerName = tempFeatureLayer.Name;
                        //        pAxfInfo.SourceFeatureLayer = tempFeatureLayer;
                        //    }
                        //    else
                        //    {
                        //        pAxfInfo.TargetLayerName = tempFeatureLayer.Name;
                        //        pAxfInfo.TargetFeatureLayer = tempFeatureLayer;
                        //    }

                        _axfInfoList.Add(pAxfInfo);
                    }

                    dataGridView2.DataSource = _axfInfoList;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void Reset()
        {
            dataGridView1.DataSource = null;
            db.Close();
        }

        private List<AxfOperationType> GetAxfOperationTypes()
        {
            List<AxfOperationType> list = new List<AxfOperationType>();
            if (this.ckbAdd.Checked)
                list.Add(AxfOperationType.Add);
            if (this.ckbModify.Checked)
                list.Add(AxfOperationType.Modify);
            if (this.ckbDelete.Checked)
                list.Add(AxfOperationType.Delete);
            if (this.ckbNoEdit.Checked)
                list.Add(AxfOperationType.NoEdit);
            return list;
        }

        public bool CheckIsAxfLayer(IFeatureLayer featureLayer)
        {
            IFeatureClass pFeatureClass = featureLayer.FeatureClass;
            if (pFeatureClass != null)
            {
                IFeatureDataset pFeatureDataset = pFeatureClass.FeatureDataset;
                if (pFeatureDataset != null && pFeatureDataset.Name.ToLower().EndsWith(".axf"))
                {
                    return true;
                }
            }
            return false;
        }

        //private void ImportAxfInfo(AxfInfo axfInfo, List<AxfOperationType> types)
        //{
        //    List<AxfField> tempAxfFields = new List<AxfField>();
        //    foreach (AxfField axfField in axfInfo.AxfFieldList)
        //    {
        //        if (types.Contains((AxfOperationType)axfField.AxfStatus) && (tempAxfFields.FirstOrDefault(c => c.Objectid == axfField.Objectid) == null))
        //            tempAxfFields.Add(axfField);
        //    }
        //    IFeatureCursor pFeatureCursor;
        //    foreach (AxfField tempAxfField in tempAxfFields)
        //    {
        //        IQueryFilter pQueryFilter = new QueryFilterClass();
        //        switch (tempAxfField.Objectid)
        //        {
        //            case -1:
        //                {
        //                    pQueryFilter.WhereClause = "OBJECTID IS NULL";
        //                    pFeatureCursor = axfInfo.SourceFeatureLayer.FeatureClass.Search(pQueryFilter, false);
        //                    IFeature pSourceFeature;
        //                    while ((pSourceFeature = pFeatureCursor.NextFeature()) != null)
        //                    {
        //                        IFeature pNewFeature = axfInfo.TargetFeatureLayer.FeatureClass.CreateFeature();
        //                        IGeometry pGeometry = pSourceFeature.Shape;
        //                        if (pGeometry is IMultipoint)
        //                        {
        //                            pNewFeature.Shape = GeometryConvert.ToPoint(pSourceFeature.ShapeCopy as IMultipoint);
        //                        }
        //                        else
        //                            pNewFeature.Shape = pSourceFeature.ShapeCopy;
        //                        IField pField;
        //                        for (int i = 0; i < pNewFeature.Fields.FieldCount; i++)
        //                        {
        //                            pField = pNewFeature.Fields.Field[i];
        //                            if (pField.Type == esriFieldType.esriFieldTypeGeometry)
        //                                continue;
        //                            int idx = pSourceFeature.Fields.FindField(pField.Name);
        //                            if (pField.Editable && idx != -1)
        //                                if ((pSourceFeature.Value[idx] is DBNull) == false)
        //                                    pNewFeature.Value[i] = pSourceFeature.Value[idx];
        //                        }
        //                        pNewFeature.Store();
        //                    }
        //                }
        //                break;
        //            default:
        //                {
        //                    IFeature pSourceFeature = axfInfo.SourceFeatureLayer.FeatureClass.GetFeature(tempAxfField.Objectid);
        //                    IFeature pFeature = axfInfo.TargetFeatureLayer.FeatureClass.GetFeature(tempAxfField.Objectid);
        //                    if (pFeature == null)
        //                    {
        //                        pFeature = axfInfo.TargetFeatureLayer.FeatureClass.CreateFeature();
        //                    }
        //                    IGeometry pGeometry = pSourceFeature.Shape;
        //                    if (pGeometry is IMultipoint)
        //                    {
        //                        pFeature.Shape = GeometryConvert.ToPoint(pSourceFeature.ShapeCopy as IMultipoint);
        //                    }
        //                    else
        //                        pFeature.Shape = pSourceFeature.ShapeCopy;
        //                    IField pField;
        //                    for (int i = 0; i < pFeature.Fields.FieldCount; i++)
        //                    {
        //                        pField = pFeature.Fields.Field[i];
        //                        if (pField.Type == esriFieldType.esriFieldTypeGeometry)
        //                            continue;
        //                        int idx = pFeature.Fields.FindField(pField.Name);
        //                        if (pField.Editable && idx != -1)
        //                            if ((pSourceFeature.Value[idx] is DBNull) == false)
        //                                pFeature.Value[i] = pSourceFeature.Value[idx];
        //                    }
        //                    pFeature.Store();
        //                }
        //                break;
        //        }
        //    }
        //}

        private void ImportAxfInfoForAdd(AxfInfo axfInfo)
        {
            IQueryFilter pQueryFilter = new QueryFilterClass();
            pQueryFilter.WhereClause = "OBJECTID IS NULL";
            IFeatureCursor pFeatureCursor = axfInfo.SourceFeatureClass.Search(pQueryFilter, false);
            IFeature pSourceFeature;
            while ((pSourceFeature = pFeatureCursor.NextFeature()) != null)
            {
                IFeature pNewFeature = axfInfo.TargetFeatureLayer.FeatureClass.CreateFeature();
                IGeometry pGeometry = pSourceFeature.Shape;
                if (pGeometry is IMultipoint)
                {
                    pNewFeature.Shape = GeometryConvert.ToPoint(pSourceFeature.ShapeCopy as IMultipoint);
                }
                else
                    pNewFeature.Shape = pSourceFeature.ShapeCopy;
                IField pField;
                for (int i = 0; i < pNewFeature.Fields.FieldCount; i++)
                {
                    pField = pNewFeature.Fields.Field[i];
                    if (pField.Type == esriFieldType.esriFieldTypeGeometry)
                        continue;
                    int idx = pSourceFeature.Fields.FindField(pField.Name);
                    if (pField.Editable && idx != -1)
                        if ((pSourceFeature.Value[idx] is DBNull) == false)
                            pNewFeature.Value[i] = pSourceFeature.Value[idx];
                }
                pNewFeature.Store();
            }
            Marshal.ReleaseComObject(pFeatureCursor);
        }

        private void ImportAxfInfoForModify(AxfInfo axfInfo)
        {
            List<AxfField> tempAxfFields = axfInfo.AxfFieldList.Where(c => c.AxfStatus == 2).ToList();
            IFeatureCursor pFeatureCursor = null;
            foreach (AxfField tempAxfField in tempAxfFields)
            {
                IQueryFilter pQueryFilter = new QueryFilterClass();
                pQueryFilter.WhereClause = string.Format("OBJECTID = {0}", tempAxfField.Objectid);
                pFeatureCursor = axfInfo.SourceFeatureClass.Search(pQueryFilter, false);
                IFeature pSourceFeature = pFeatureCursor.NextFeature();
                if (pSourceFeature == null)
                    continue;
                IFeature pTargetFeature = axfInfo.TargetFeatureLayer.FeatureClass.GetFeature(tempAxfField.Objectid);
                pTargetFeature.Shape = pSourceFeature.ShapeCopy;
                IField pField;
                for (int i = 0; i < pTargetFeature.Fields.FieldCount; i++)
                {
                    pField = pTargetFeature.Fields.Field[i];
                    if (pField.Type == esriFieldType.esriFieldTypeGeometry)
                        continue;
                    int idx = pTargetFeature.Fields.FindField(pField.Name);
                    if (pField.Editable && idx != -1)
                        if ((pSourceFeature.Value[idx] is DBNull) == false)
                            pTargetFeature.Value[i] = pSourceFeature.Value[idx];
                }
                pTargetFeature.Store();
            }
            if (pFeatureCursor != null) Marshal.ReleaseComObject(pFeatureCursor);
        }

        private void ImportAxfInfoForDelete(AxfInfo axfInfo)
        {
            List<AxfField> tempAxfFields = axfInfo.AxfFieldList.Where(c => c.AxfStatus == 128).ToList();
            foreach (AxfField tempAxfField in tempAxfFields)
            {
                IFeature pTargetFeature = axfInfo.TargetFeatureLayer.FeatureClass.GetFeature(tempAxfField.Objectid);
                pTargetFeature.Delete();
            }
        }
    }
}
