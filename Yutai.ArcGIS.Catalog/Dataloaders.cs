using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog
{
    public class Dataloaders : IConvertEvent, ESRI.ArcGIS.Geodatabase.IFeatureProgress_Event
    {
        private bool bool_0 = false;

        private bool bool_1 = false;

        private IFeatureDataConverter ifeatureDataConverter_0 = new FeatureDataConverter();

        private double double_0 = -1;

        public MainSunProcessAssist assist = null;

        private ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler ifeatureProgress_StepEventHandler_0;

        private SetFeatureClassNameEnventHandler setFeatureClassNameEnventHandler_0;

        private SetFeatureCountEnventHandler setFeatureCountEnventHandler_0;

        private SetMaxValueHandler setMaxValueHandler_0;

        private SetMinValueHandler setMinValueHandler_0;

        private SetPositionHandler setPositionHandler_0;

        private SetMessageHandler setMessageHandler_0;

        private FinishHander finishHander_0;

        public IFeatureDataConverter Converter
        {
            get { return this.ifeatureDataConverter_0; }
        }

        public bool RelpaceObject
        {
            set { this.bool_1 = value; }
        }

        public double Scale
        {
            set { this.double_0 = value; }
        }

        public bool UseExitName
        {
            set { this.bool_0 = value; }
        }

        public Dataloaders()
        {
        }

        private static void AddFields(IRow irow_0, IRow irow_1)
        {
            IFields fields = irow_0.Fields;
            IFields field = irow_1.Fields;
            for (int i = 0; i < field.FieldCount; i++)
            {
                IField field1 = field.Field[i];
                if ((field1.Type == esriFieldType.esriFieldTypeGeometry || field1.Type == esriFieldType.esriFieldTypeOID
                    ? false
                    : field1.Editable))
                {
                    int value = fields.FindField(field1.Name);
                    if (value != -1)
                    {
                        irow_0.Value[value] = irow_1.Value[i];
                    }
                }
            }
        }

        private static void AddFields(IRow irow_0, SortedList<string, int> sortedList_0, IRow irow_1)
        {
            IFields fields = irow_0.Fields;
            IFields field = irow_1.Fields;
            for (int i = 0; i < fields.FieldCount; i++)
            {
                IField field1 = fields.Field[i];
                if ((field1.Type == esriFieldType.esriFieldTypeGeometry || field1.Type == esriFieldType.esriFieldTypeOID
                    ? false
                    : field1.Editable))
                {
                    string name = field1.Name;
                    if (sortedList_0.ContainsKey(name))
                    {
                        int item = sortedList_0[name];
                        if (item != -1)
                        {
                            try
                            {
                                irow_0.set_Value(i, irow_1.Value[item]);
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
        }

        private static void AddFields(IRowBuffer irowBuffer_0, IRow irow_0)
        {
            IFields fields = irowBuffer_0.Fields;
            IFields field = irow_0.Fields;
            for (int i = 0; i < field.FieldCount; i++)
            {
                IField field1 = field.Field[i];
                if ((field1.Type == esriFieldType.esriFieldTypeGeometry || field1.Type == esriFieldType.esriFieldTypeOID
                    ? false
                    : field1.Editable))
                {
                    int value = fields.FindField(field1.Name);
                    if (value != -1)
                    {
                        irowBuffer_0.Value[value] = irow_0.Value[i];
                    }
                }
            }
        }

        private static void AddFields(IRowBuffer irowBuffer_0, SortedList<string, int> sortedList_0, IRow irow_0)
        {
            IFields fields = irowBuffer_0.Fields;
            IFields field = irow_0.Fields;
            for (int i = 0; i < field.FieldCount; i++)
            {
                IField field1 = field.Field[i];
                if ((field1.Type == esriFieldType.esriFieldTypeGeometry || field1.Type == esriFieldType.esriFieldTypeOID
                    ? false
                    : field1.Editable))
                {
                    string lower = field1.Name.ToLower();
                    if (sortedList_0.ContainsKey(lower))
                    {
                        int item = sortedList_0[lower];
                        if (item != -1)
                        {
                            irowBuffer_0.Value[item] = irow_0.Value[i];
                        }
                    }
                }
            }
        }

        public string ConvertData(IFeatureDatasetName ifeatureDatasetName_0, IName iname_0, string string_0,
            IQueryFilter iqueryFilter_0)
        {
            string str = "";
            try
            {
                IWorkspaceName workspaceName = (ifeatureDatasetName_0 as IDatasetName).WorkspaceName;
                IFeatureDataset featureDataset = (ifeatureDatasetName_0 as IName).Open() as IFeatureDataset;
                if ((iname_0.Open() as IFeatureWorkspace as IWorkspace).Type !=
                    esriWorkspaceType.esriFileSystemWorkspace)
                {
                    IFeatureDatasetName ifeatureDatasetName0 = ifeatureDatasetName_0;
                    IFeatureDatasetName featureDatasetNameClass = new FeatureDatasetName() as IFeatureDatasetName;
                    IDatasetName iname0 = (IDatasetName) featureDatasetNameClass;
                    iname0.WorkspaceName = iname_0 as IWorkspaceName;
                    iname0.Name = string_0;
                    this.ifeatureDataConverter_0.ConvertFeatureDataset(ifeatureDatasetName0, featureDatasetNameClass,
                        null, "", 1000, 0);
                }
                else
                {
                    IEnumDatasetName featureClassNames = ifeatureDatasetName_0.FeatureClassNames;
                    for (IDatasetName i = featureClassNames.Next(); i != null; i = featureClassNames.Next())
                    {
                        str = string.Concat(str, "\r\n", this.ConvertData(i, iname_0, i.Name, iqueryFilter_0));
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            return str;
        }

        public string ConvertData(IDatasetName idatasetName_0, IName iname_0, string string_0,
            IQueryFilter iqueryFilter_0)
        {
            string str;
            IFeatureDatasetName iname0;
            IWorkspaceName workspaceName;
            IEnumFieldError enumFieldError;
            IFields field;
            string str1;
            IDatasetName tableNameClass;
            IField field1;
            int num;
            IGeometryDef geometryDef;
            IGeometryDefEdit spatialReference;
            ISpatialReference spatialReference1;
            IEnumInvalidObject enumInvalidObject;
            string str2 = "";
            if (!(idatasetName_0 is IFeatureDatasetName))
            {
                IWorkspaceName workspaceName1 = idatasetName_0.WorkspaceName;
                iname0 = null;
                if (!(iname_0 is IWorkspaceName))
                {
                    if (iname_0 is IFeatureDatasetName)
                    {
                        iname0 = iname_0 as IFeatureDatasetName;
                        workspaceName = (iname_0 as IDatasetName).WorkspaceName;
                    }
                    else
                    {
                        str = "";
                        return str;
                    }
                }
                else
                {
                    workspaceName = iname_0 as IWorkspaceName;
                }

                ITable table = (idatasetName_0 as IName).Open() as ITable;
                string aliasName = (table as IObjectClass).AliasName;
                IFields fields = table.Fields;
                IFieldChecker fieldCheckerClass = new FieldChecker();
                IWorkspace workspace = (workspaceName as IName).Open() as IWorkspace;
                fieldCheckerClass.ValidateWorkspace = workspace;
                fieldCheckerClass.Validate(fields, out enumFieldError, out field);
                fieldCheckerClass.ValidateTableName(string_0, out str1);
                string_0 = this.GetFinalTableName(workspace as IFeatureWorkspace, str1);
                if (enumFieldError != null)
                {
                    string str3 = "Some columns will be given new names as follows:";
                    for (IFieldError i = enumFieldError.Next(); i != null; i = enumFieldError.Next())
                    {
                        IField field2 = fields.Field[i.FieldIndex];
                        IField field3 = field.Field[i.FieldIndex];
                        string[] name = new string[]
                            {str3, field3.Name, "reason: ", field2.Name, "  ", this.method_1(i.FieldError)};
                        str3 = string.Concat(name);
                    }
                    Logger.Current.Info(string.Concat(Application.StartupPath, "\\DataConvert.log"), str3);
                }
                fieldCheckerClass = null;
                if (!(idatasetName_0 is IFeatureClassName))
                {
                    tableNameClass = new TableName() as IDatasetName;

                    tableNameClass.WorkspaceName = workspaceName;
                    tableNameClass.Name = string_0;

                    enumInvalidObject = this.ifeatureDataConverter_0.ConvertTable(idatasetName_0, null, tableNameClass,
                        field, "", 1000, 0);
                    this.method_13(enumInvalidObject);
                }
                else if ((idatasetName_0.Name.IndexOf("Annotation", StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                          (idatasetName_0 as IFeatureClassName).FeatureType == esriFeatureType.esriFTAnnotation
                    ? false
                    : (idatasetName_0 as IFeatureClassName).FeatureType != esriFeatureType.esriFTCoverageAnnotation))
                {
                    tableNameClass = new FeatureClassName() as IDatasetName;

                    tableNameClass.WorkspaceName = workspaceName;
                    tableNameClass.Name = string_0;
                    if (iname0 != null)
                    {
                        (tableNameClass as IFeatureClassName).FeatureDatasetName = iname0 as IDatasetName;
                    }
                    field1 = null;
                    num = 0;
                    while (true)
                    {
                        if (num >= field.FieldCount)
                        {
                            break;
                        }
                        else if (field.Field[num].Type == esriFieldType.esriFieldTypeGeometry)
                        {
                            field1 = field.Field[num];
                            break;
                        }
                        else
                        {
                            num++;
                        }
                    }
                    geometryDef = field1.GeometryDef;
                    spatialReference = geometryDef as IGeometryDefEdit;
                    spatialReference.GridCount_2 = 1;
                    spatialReference.GridSize_2[0] = this.method_9((IFeatureClass) table);
                    spatialReference1 = field1.GeometryDef.SpatialReference;
                    SpatialReferenctOperator.ChangeCoordinateSystem(workspace as IGeodatabaseRelease, spatialReference1,
                        false);
                    spatialReference.SpatialReference_2 = field1.GeometryDef.SpatialReference;
                    (field1 as IFieldEdit).GeometryDef_2 = spatialReference;
                    enumInvalidObject =
                        this.ifeatureDataConverter_0.ConvertFeatureClass(idatasetName_0 as IFeatureClassName,
                            iqueryFilter_0, iname0, tableNameClass as IFeatureClassName, geometryDef, field, "", 1000, 0);
                    this.method_13(enumInvalidObject);
                }
                else if (workspaceName.Type == esriWorkspaceType.esriFileSystemWorkspace)
                {
                    tableNameClass = new FeatureClassName() as IDatasetName;

                    tableNameClass.WorkspaceName = workspaceName;
                    tableNameClass.Name = string_0;
                    if (iname0 != null)
                    {
                        (tableNameClass as IFeatureClassName).FeatureDatasetName = iname0 as IDatasetName;
                    }
                    field1 = null;
                    num = 0;
                    while (true)
                    {
                        if (num >= field.FieldCount)
                        {
                            break;
                        }
                        else if (field.Field[num].Type == esriFieldType.esriFieldTypeGeometry)
                        {
                            field1 = field.Field[num];
                            break;
                        }
                        else
                        {
                            num++;
                        }
                    }
                    geometryDef = field1.GeometryDef;
                    spatialReference = geometryDef as IGeometryDefEdit;
                    spatialReference.GridCount_2 = 1;
                    spatialReference.GridSize_2[0] = this.method_9((IFeatureClass) table);
                    spatialReference1 = field1.GeometryDef.SpatialReference;
                    SpatialReferenctOperator.ChangeCoordinateSystem(workspace as IGeodatabaseRelease, spatialReference1,
                        false);
                    spatialReference.SpatialReference_2 = field1.GeometryDef.SpatialReference;
                    (field1 as IFieldEdit).GeometryDef_2 = spatialReference;
                    enumInvalidObject =
                        this.ifeatureDataConverter_0.ConvertFeatureClass(idatasetName_0 as IFeatureClassName,
                            iqueryFilter_0, iname0, tableNameClass as IFeatureClassName, geometryDef, field, "", 1000, 0);
                    this.method_13(enumInvalidObject);
                }
                else
                {
                    this.method_11(table as IFeatureClass, iname_0, string_0);
                }
                workspace = null;
                str = str2;
            }
            else
            {
                str = this.ConvertData(idatasetName_0 as IFeatureDatasetName, iname_0, string_0, iqueryFilter_0);
            }
            return str;
        }

        public string ConvertData(IPropertySet ipropertySet_0, string string_0, convDataType convDataType_0,
            IPropertySet ipropertySet_1, string string_1, string string_2, convDataType convDataType_1, bool bool_2)
        {
            string result = "";
            IWorkspaceName workspaceName = new WorkspaceName() as IWorkspaceName;
            workspaceName.ConnectionProperties = ipropertySet_0;
            workspaceName.WorkspaceFactoryProgID = this.method_0(convDataType_0);
            IWorkspaceName workspaceName2 = new WorkspaceName() as IWorkspaceName;
            workspaceName2.ConnectionProperties = ipropertySet_1;
            workspaceName2.WorkspaceFactoryProgID = this.method_0(convDataType_1);
            IDatasetName datasetName;
            if (bool_2)
            {
                datasetName = new FeatureClassName() as IDatasetName;
            }
            else
            {
                datasetName = new TableName() as IDatasetName;
            }
            datasetName.Name = string_0;
            datasetName.WorkspaceName = workspaceName;
            IDatasetName datasetName2;
            if (string_2 == "")
            {
                datasetName2 = null;
            }
            else
            {
                datasetName2 = new FeatureDatasetName() as IDatasetName;
                datasetName2.WorkspaceName = workspaceName2;
                datasetName2.Name = string_2;
            }
            IDatasetName datasetName3;
            if (bool_2)
            {
                datasetName3 = new FeatureClassName() as IDatasetName;
                if (datasetName2 != null)
                {
                    IFeatureClassName featureClassName = (IFeatureClassName) datasetName3;
                    featureClassName.FeatureDatasetName = datasetName2;
                }
            }
            else
            {
                datasetName3 = new TableName() as IDatasetName;
            }
            datasetName3.WorkspaceName = workspaceName2;
            datasetName3.Name = string_1;
            IName name = (IName) datasetName;
            ITable table = (ITable) name.Open();
            IFields fields = table.Fields;
            IFieldChecker fieldChecker = new FieldChecker();
            IEnumFieldError enumFieldError;
            IFields fields2;
            fieldChecker.Validate(fields, out enumFieldError, out fields2);
            if (enumFieldError != null)
            {
                string text = "Some columns will be given new names as follows:";
                for (IFieldError fieldError = enumFieldError.Next();
                    fieldError != null;
                    fieldError = enumFieldError.Next())
                {
                    IField field = fields.get_Field(fieldError.FieldIndex);
                    IField field2 = fields2.get_Field(fieldError.FieldIndex);
                    text = string.Concat(new string[]
                    {
                        text,
                        field2.Name,
                        "reason: ",
                        field.Name,
                        "  ",
                        this.method_1(fieldError.FieldError)
                    });
                }
                text += "Continue?";
                System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(text, "错误提示信息",
                    System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
                if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                {
                    result = "Load cancelled";
                }
            }
            if (bool_2)
            {
                IField field3 = null;
                for (int i = 0; i < fields2.FieldCount; i++)
                {
                    if (fields2.get_Field(i).Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        field3 = fields2.get_Field(i);
                    }
                }
                IGeometryDef geometryDef = field3.GeometryDef;
                IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit) geometryDef;
                geometryDefEdit.GridCount_2 = 1;
                geometryDefEdit.set_GridSize(0, this.method_9((IFeatureClass) table));
                ISpatialReference spatialReference = field3.GeometryDef.SpatialReference;
                SpatialReferenctOperator.ChangeCoordinateSystem(
                    (workspaceName2 as IName).Open() as IGeodatabaseRelease, spatialReference, false);
                geometryDefEdit.SpatialReference_2 = field3.GeometryDef.SpatialReference;
                (field3 as IFieldEdit).GeometryDef_2 = geometryDefEdit;
                IEnumInvalidObject enumInvalidObject;
                if (bool_2)
                {
                    enumInvalidObject = this.ifeatureDataConverter_0.ConvertFeatureClass(
                        (IFeatureClassName) datasetName, null, (IFeatureDatasetName) datasetName2,
                        (IFeatureClassName) datasetName3, geometryDef, fields2, "", 1000, 0);
                }
                else
                {
                    enumInvalidObject = this.ifeatureDataConverter_0.ConvertTable(datasetName, null, datasetName3,
                        fields2, "", 1000, 0);
                }
                IInvalidObjectInfo invalidObjectInfo = enumInvalidObject.Next();
                if (invalidObjectInfo == null)
                {
                    result = "Load completed";
                }
            }
            return result;
        }


        public string ConvertDataEx(IDatasetName idatasetName_0, IName iname_0, ref string string_0,
            IQueryFilter iqueryFilter_0)
        {
            string str;
            IFeatureDatasetName iname0;
            IWorkspaceName workspaceName;
            IEnumFieldError enumFieldError;
            IFields field;
            string str1;
            IDatasetName tableNameClass;
            IField field1;
            int num;
            IGeometryDef geometryDef;
            IGeometryDefEdit spatialReference;
            ISpatialReference spatialReference1;
            IEnumInvalidObject enumInvalidObject;
            string str2 = "";
            if (!(idatasetName_0 is IFeatureDatasetName))
            {
                IWorkspaceName workspaceName1 = idatasetName_0.WorkspaceName;
                iname0 = null;
                if (!(iname_0 is IWorkspaceName))
                {
                    if (iname_0 is IFeatureDatasetName)
                    {
                        iname0 = iname_0 as IFeatureDatasetName;
                        workspaceName = (iname_0 as IDatasetName).WorkspaceName;
                    }
                    else
                    {
                        str = "";
                        return str;
                    }
                }
                else
                {
                    workspaceName = iname_0 as IWorkspaceName;
                }

                ITable table = (idatasetName_0 as IName).Open() as ITable;
                string aliasName = (table as IObjectClass).AliasName;
                IFields fields = table.Fields;
                IFieldChecker fieldCheckerClass = new FieldChecker();
                IWorkspace workspace = (workspaceName as IName).Open() as IWorkspace;
                fieldCheckerClass.ValidateWorkspace = workspace;
                fieldCheckerClass.Validate(fields, out enumFieldError, out field);
                fieldCheckerClass.ValidateTableName(string_0, out str1);
                string_0 = this.GetFinalTableName(workspace as IFeatureWorkspace, str1);
                if (enumFieldError != null)
                {
                    string str3 = "Some columns will be given new names as follows:";
                    for (IFieldError i = enumFieldError.Next(); i != null; i = enumFieldError.Next())
                    {
                        IField field2 = fields.Field[i.FieldIndex];
                        IField field3 = field.Field[i.FieldIndex];
                        string[] name = new string[]
                            {str3, field3.Name, "reason: ", field2.Name, "  ", this.method_1(i.FieldError)};
                        str3 = string.Concat(name);
                    }
                    Logger.Current.Info(string.Concat(Application.StartupPath, "\\DataConvert.log"), str3);
                }
                fieldCheckerClass = null;
                if (!(idatasetName_0 is IFeatureClassName))
                {
                    tableNameClass = new TableName() as IDatasetName;

                    tableNameClass.WorkspaceName = workspaceName;
                    tableNameClass.Name = string_0;

                    enumInvalidObject = this.ifeatureDataConverter_0.ConvertTable(idatasetName_0, null, tableNameClass,
                        field, "", 1000, 0);
                    this.method_13(enumInvalidObject);
                }
                else if ((idatasetName_0.Name.IndexOf("Annotation", StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                          (idatasetName_0 as IFeatureClassName).FeatureType == esriFeatureType.esriFTAnnotation
                    ? false
                    : (idatasetName_0 as IFeatureClassName).FeatureType != esriFeatureType.esriFTCoverageAnnotation))
                {
                    tableNameClass = new FeatureClassName() as IDatasetName;

                    tableNameClass.WorkspaceName = workspaceName;
                    tableNameClass.Name = string_0;
                    if (iname0 != null)
                    {
                        (tableNameClass as IFeatureClassName).FeatureDatasetName = iname0 as IDatasetName;
                    }
                    field1 = null;
                    num = 0;
                    while (true)
                    {
                        if (num >= field.FieldCount)
                        {
                            break;
                        }
                        else if (field.Field[num].Type == esriFieldType.esriFieldTypeGeometry)
                        {
                            field1 = field.Field[num];
                            break;
                        }
                        else
                        {
                            num++;
                        }
                    }
                    geometryDef = field1.GeometryDef;
                    spatialReference = geometryDef as IGeometryDefEdit;
                    spatialReference1 = field1.GeometryDef.SpatialReference;
                    SpatialReferenctOperator.ChangeCoordinateSystem(workspace as IGeodatabaseRelease, spatialReference1,
                        false);
                    spatialReference.SpatialReference_2 = field1.GeometryDef.SpatialReference;
                    (field1 as IFieldEdit).GeometryDef_2 = spatialReference;
                    enumInvalidObject =
                        this.ifeatureDataConverter_0.ConvertFeatureClass(idatasetName_0 as IFeatureClassName,
                            iqueryFilter_0, iname0, tableNameClass as IFeatureClassName, geometryDef, field, "", 1000, 0);
                    this.method_13(enumInvalidObject);
                }
                else if (workspaceName.Type == esriWorkspaceType.esriFileSystemWorkspace)
                {
                    tableNameClass = new FeatureClassName() as IDatasetName;

                    tableNameClass.WorkspaceName = workspaceName;
                    tableNameClass.Name = string_0;
                    if (iname0 != null)
                    {
                        (tableNameClass as IFeatureClassName).FeatureDatasetName = iname0 as IDatasetName;
                    }
                    field1 = null;
                    num = 0;
                    while (true)
                    {
                        if (num >= field.FieldCount)
                        {
                            break;
                        }
                        else if (field.Field[num].Type == esriFieldType.esriFieldTypeGeometry)
                        {
                            field1 = field.Field[num];
                            break;
                        }
                        else
                        {
                            num++;
                        }
                    }
                    geometryDef = field1.GeometryDef;
                    spatialReference = geometryDef as IGeometryDefEdit;
                    spatialReference1 = field1.GeometryDef.SpatialReference;
                    SpatialReferenctOperator.ChangeCoordinateSystem(workspace as IGeodatabaseRelease, spatialReference1,
                        false);
                    spatialReference.SpatialReference_2 = field1.GeometryDef.SpatialReference;
                    (field1 as IFieldEdit).GeometryDef_2 = spatialReference;
                    enumInvalidObject =
                        this.ifeatureDataConverter_0.ConvertFeatureClass(idatasetName_0 as IFeatureClassName,
                            iqueryFilter_0, iname0, tableNameClass as IFeatureClassName, geometryDef, field, "", 1000, 0);
                    this.method_13(enumInvalidObject);
                }
                else
                {
                    this.method_11(table as IFeatureClass, iname_0, string_0);
                }
                workspace = null;
                str = str2;
            }
            else
            {
                str = this.ConvertData(idatasetName_0 as IFeatureDatasetName, iname_0, string_0, iqueryFilter_0);
            }
            return str;
        }

        public string GetFinalName(IFeatureWorkspace ifeatureWorkspace_0, string string_0)
        {
            string string0 = string_0;
            int num = 0;
            while (true)
            {
                try
                {
                    if (!(ifeatureWorkspace_0 as IWorkspace2).NameExists[esriDatasetType.esriDTFeatureDataset, string0])
                    {
                        break;
                    }
                    else
                    {
                        num++;
                        string0 = string.Concat(string_0, "_", num.ToString());
                    }
                }
                catch
                {
                    break;
                }
            }
            return string0;
        }

        public string GetFinalTableName(IFeatureWorkspace ifeatureWorkspace_0, string string_0)
        {
            string str;
            int num = 0;
            IFieldChecker fieldCheckerClass = new FieldChecker()
            {
                ValidateWorkspace = ifeatureWorkspace_0 as IWorkspace
            };
            fieldCheckerClass.ValidateTableName(string_0, out str);
            while (true)
            {
                try
                {
                    if ((ifeatureWorkspace_0 as IWorkspace2).NameExists[esriDatasetType.esriDTFeatureClass, str])
                    {
                        num++;
                        str = string.Concat(string_0, "_", num.ToString());
                    }
                    else if (!(ifeatureWorkspace_0 as IWorkspace2).NameExists[esriDatasetType.esriDTTable, str])
                    {
                        break;
                    }
                    else
                    {
                        num++;
                        str = string.Concat(string_0, "_", num.ToString());
                    }
                }
                catch
                {
                    break;
                }
            }
            return str;
        }

        public void LoadData(ITable itable_0, IQueryFilter iqueryFilter_0, ITable itable_1, int int_0)
        {
            if (this.setFeatureCountEnventHandler_0 != null)
            {
                this.setFeatureCountEnventHandler_0(itable_0.RowCount(iqueryFilter_0));
            }
            if (this.setFeatureClassNameEnventHandler_0 != null)
            {
                this.setFeatureClassNameEnventHandler_0((itable_0 as IDataset).Name);
            }
            try
            {
                ICursor cursor = itable_0.Search(iqueryFilter_0, true);
                IRow row = cursor.NextRow();
                ISpatialReference spatialReference = null;
                if (itable_0 is IGeoDataset)
                {
                    ISpatialReference spatialReference1 = (itable_0 as IGeoDataset).SpatialReference;
                }
                if (itable_1 is IGeoDataset)
                {
                    spatialReference = (itable_1 as IGeoDataset).SpatialReference;
                }
                while (row != null)
                {
                    IRow row1 = null;
                    try
                    {
                        if (!(row is IFeature))
                        {
                            row1 = itable_1.CreateRow();
                            Dataloaders.AddFields(row1, row);
                            row1.Store();
                        }
                        else
                        {
                            IGeometry shapeCopy = (row as IFeature).ShapeCopy;
                            if (!shapeCopy.IsEmpty)
                            {
                                if (!(spatialReference is IUnknownCoordinateSystem))
                                {
                                    shapeCopy.Project(spatialReference);
                                }
                                row1 = itable_1.CreateRow();
                                (row1 as IFeature).Shape = shapeCopy;
                                Dataloaders.AddFields(row1, row);
                                row1.Store();
                            }
                        }
                    }
                    catch (COMException cOMException1)
                    {
                        COMException cOMException = cOMException1;
                        if (cOMException.ErrorCode != -2147220936)
                        {
                            Logger.Current.Info(string.Concat(Application.StartupPath, "\\DataLoad.log"),
                                cOMException.ToString());
                        }
                        else
                        {
                            string str = string.Concat(Application.StartupPath, "\\DataLoad.log");
                            int oID = row.OID;
                            Logger.Current.Info(str, string.Concat("对象", oID.ToString(), "坐标范围超界"));
                        }
                    }
                    catch (Exception exception1)
                    {
                        Exception exception = exception1;
                        Logger.Current.Info(string.Concat(Application.StartupPath, "\\DataLoad.log"),
                            exception.ToString());
                    }
                    if (this.ifeatureProgress_StepEventHandler_0 != null)
                    {
                        this.ifeatureProgress_StepEventHandler_0();
                    }
                    row = cursor.NextRow();
                }
                ComReleaser.ReleaseCOMObject(cursor);
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message);
            }
        }

        public void LoadData(ITable itable_0, IQueryFilter iqueryFilter_0, ITable itable_1,
            SortedList<string, string> sortedList_0, int int_0)
        {
            int num = itable_0.RowCount(iqueryFilter_0);
            if (this.assist != null)
            {
                this.assist.ResetSubInfo();
                this.assist.SetSubMaxValue(num);
                this.assist.SetSubPostion(0);
            }
            if (this.setFeatureCountEnventHandler_0 != null)
            {
                this.setFeatureCountEnventHandler_0(itable_0.RowCount(iqueryFilter_0));
            }
            if (this.setFeatureClassNameEnventHandler_0 != null)
            {
                this.setFeatureClassNameEnventHandler_0((itable_0 as IDataset).Name);
            }
            try
            {
                ICursor cursor = itable_0.Search(iqueryFilter_0, true);
                IRow row = cursor.NextRow();
                ISpatialReference spatialReference = null;
                if (itable_0 is IGeoDataset)
                {
                    ISpatialReference spatialReference1 = (itable_0 as IGeoDataset).SpatialReference;
                }
                if (itable_1 is IGeoDataset)
                {
                    spatialReference = (itable_1 as IGeoDataset).SpatialReference;
                }
                SortedList<string, int> strs = new SortedList<string, int>();
                foreach (KeyValuePair<string, string> sortedList0 in sortedList_0)
                {
                    if (sortedList0.Value == "<空>")
                    {
                        continue;
                    }
                    int num1 = itable_0.FindField(sortedList0.Value);
                    if (num1 == -1)
                    {
                        continue;
                    }
                    strs.Add(sortedList0.Key, num1);
                }
                string str = string.Format("共{0}个对象，正在导入第#个对象", num);
                int num2 = 1;
                while (row != null)
                {
                    if (this.assist != null)
                    {
                        this.assist.SubIncrement(1);
                        this.assist.SetSubMessage(str.Replace("#", num2.ToString()));
                        num2++;
                    }
                    IRow row1 = null;
                    try
                    {
                        if (!(row is IFeature))
                        {
                            row1 = itable_1.CreateRow();
                            Dataloaders.AddFields(row1, strs, row);
                            row1.Store();
                        }
                        else
                        {
                            IGeometry shapeCopy = (row as IFeature).ShapeCopy;
                            if (!shapeCopy.IsEmpty)
                            {
                                if (!(spatialReference is IUnknownCoordinateSystem))
                                {
                                    shapeCopy.Project(spatialReference);
                                }
                                row1 = itable_1.CreateRow();
                                CreateFeatureTool.SetGeometry(shapeCopy, row1 as IFeature);
                                Dataloaders.AddFields(row1, strs, row);
                                row1.Store();
                            }
                        }
                    }
                    catch (COMException cOMException1)
                    {
                        COMException cOMException = cOMException1;
                        if (cOMException.ErrorCode != -2147220936)
                        {
                            Logger.Current.Info(string.Concat(Application.StartupPath, "\\DataLoad.log"),
                                cOMException.ToString());
                        }
                        else
                        {
                            string str1 = string.Concat(Application.StartupPath, "\\DataLoad.log");
                            int oID = row.OID;
                            Logger.Current.Info(str1, string.Concat("对象", oID.ToString(), "坐标范围超界"));
                        }
                    }
                    catch (Exception exception1)
                    {
                        Exception exception = exception1;
                        Logger.Current.Info(string.Concat(Application.StartupPath, "\\DataLoad.log"),
                            exception.ToString());
                    }
                    if (this.ifeatureProgress_StepEventHandler_0 != null)
                    {
                        this.ifeatureProgress_StepEventHandler_0();
                    }
                    row = cursor.NextRow();
                }
                ComReleaser.ReleaseCOMObject(cursor);
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message);
            }
        }

        public void LoadData(List<ITable> list_0, string string_0, ITable itable_0,
            SortedList<string, string> sortedList_0, int int_0)
        {
            if (this.assist == null)
            {
                this.assist = new MainSunProcessAssist();
            }
            this.assist.InitProgress();
            this.assist.SetMaxValue(list_0.Count);
            this.assist.SetMessage("开始加载数据.....");
            this.assist.SetPostion(0);
            this.assist.Start();
            int num = 1;
            int count = list_0.Count;
            foreach (ITable list0 in list_0)
            {
                int num1 = num;
                num = num1 + 1;
                this.assist.SetMessage(string.Format("正在加载数据[{0}]....,第{1}/{2}个数据集", (list0 as IDataset).Name, num1,
                    count));
                this.assist.Increment(1);
                IQueryFilter queryFilter = null;
                if (string_0.Length > 0)
                {
                    string str = "";
                    IWorkspace workspace = (list0 as IDataset).Workspace;
                    if (workspace.Type != esriWorkspaceType.esriLocalDatabaseWorkspace)
                    {
                        str = string_0.Replace("?", "_");
                        str = string_0.Replace("*", "%");
                        str = string_0.Replace("[", "");
                        str = string_0.Replace("]", "");
                    }
                    else if (workspace.PathName != null)
                    {
                        if (System.IO.Path.GetExtension(workspace.PathName).ToLower() != ".mdb")
                        {
                            str = string_0.Replace("?", "_");
                            str = string_0.Replace("*", "%");
                            str = string_0.Replace("[", "");
                            str = string_0.Replace("]", "");
                        }
                        else
                        {
                            str = string_0.Replace("_", "?");
                            str = string_0.Replace("%", "*");
                        }
                    }
                    IQueryFilter queryFilterClass = new QueryFilter();

                    queryFilterClass.WhereClause = str;
                    queryFilterClass.SubFields = "*";
                    queryFilter = queryFilterClass;
                }
                this.LoadData(list0, queryFilter, itable_0, sortedList_0, int_0);
            }
            this.assist.End();
        }

        public void LoadDataEx(ITable itable_0, IList<int> ilist_0, ITable itable_1, int int_0)
        {
            bool flag = false;
            IWorkspaceEdit workspace = (itable_1 as IDataset).Workspace as IWorkspaceEdit;
            if (workspace != null)
            {
                if (!workspace.IsBeingEdited())
                {
                    flag = true;
                }
                workspace.StartEditing(true);
                workspace.StartEditOperation();
            }
            try
            {
                ICursor cursor = itable_0.Search(null, true);
                IRow row = cursor.NextRow();
                ISpatialReference spatialReference = null;
                if (itable_0 is IGeoDataset)
                {
                    ISpatialReference spatialReference1 = (itable_0 as IGeoDataset).SpatialReference;
                }
                if (itable_1 is IGeoDataset)
                {
                    spatialReference = (itable_1 as IGeoDataset).SpatialReference;
                }
                while (row != null)
                {
                    IRow row1 = null;
                    if (ilist_0.IndexOf(row.OID) == -1)
                    {
                        try
                        {
                            if (!(row is IFeature))
                            {
                                row1 = itable_1.CreateRow();
                                Dataloaders.AddFields(row1, row);
                                row1.Store();
                            }
                            else
                            {
                                IGeometry shapeCopy = (row as IFeature).ShapeCopy;
                                if (!shapeCopy.IsEmpty)
                                {
                                    if (!(spatialReference is IUnknownCoordinateSystem))
                                    {
                                        shapeCopy.Project(spatialReference);
                                    }
                                    row1 = itable_1.CreateRow();
                                    CreateFeatureTool.SetGeometry(shapeCopy, row1 as IFeature);
                                    Dataloaders.AddFields(row1, row);
                                    row1.Store();
                                }
                            }
                        }
                        catch (COMException cOMException1)
                        {
                            COMException cOMException = cOMException1;
                            if (cOMException.ErrorCode != -2147220936)
                            {
                                Logger.Current.Info(string.Concat(Application.StartupPath, "\\DataLoad.log"),
                                    cOMException.ToString());
                            }
                            else
                            {
                                string str = string.Concat(Application.StartupPath, "\\DataLoad.log");
                                int oID = row.OID;
                                Logger.Current.Info(str, string.Concat("对象", oID.ToString(), "坐标范围超界"));
                            }
                        }
                        catch (Exception exception1)
                        {
                            Exception exception = exception1;
                            Logger.Current.Info(string.Concat(Application.StartupPath, "\\DataLoad.log"),
                                exception.ToString());
                        }
                    }
                    if (this.ifeatureProgress_StepEventHandler_0 != null)
                    {
                        this.ifeatureProgress_StepEventHandler_0();
                    }
                    row = cursor.NextRow();
                }
                ComReleaser.ReleaseCOMObject(cursor);
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message);
            }
            if (workspace != null)
            {
                workspace.StopEditOperation();
                if (flag)
                {
                    workspace.StopEditing(true);
                }
            }
        }

        internal ITextElement MakeTextElement(string string_0, IPoint ipoint_0)
        {
            ITextElement textElementClass = new TextElement() as ITextElement;

            textElementClass.ScaleText = true;
            textElementClass.Text = string_0;

            (textElementClass as IGroupSymbolElement).SymbolID = 0;
            (textElementClass as IElement).Geometry = ipoint_0;
            return textElementClass;
        }

        private string method_0(convDataType convDataType_0)
        {
            string str;
            switch (convDataType_0)
            {
                case convDataType.convDataTypeGDB:
                {
                    str = "esriDataSourcesGDB.SDEWorkspaceFactory.1";
                    break;
                }
                case convDataType.convDataTypePersonalGDB:
                {
                    str = "esriDataSourcesGDB.AccessWorkspaceFactory.1";
                    break;
                }
                case convDataType.convDataTypeShapefile:
                {
                    str = "esriDataSourcesFile.ShapefileWorkspaceFactory.1";
                    break;
                }
                case convDataType.convDataTypeCoverage:
                {
                    str = "esriDataSourcesFile.ArcInfoWorkspaceFactory.1";
                    break;
                }
                case convDataType.convDataTypeInfo:
                {
                    str = "esriDataSourcesFile.ArcInfoWorkspaceFactory.1";
                    break;
                }
                case convDataType.convDataTypeDbase:
                {
                    str = "esriDataSourcesFile.ShapefileWorkspaceFactory.1";
                    break;
                }
                case convDataType.convDataTypeOLEDB:
                {
                    str = "esriDataSourcesOleDB.OLEDBWorkspaceFactory.1";
                    break;
                }
                default:
                {
                    str = "";
                    break;
                }
            }
            return str;
        }

        private string method_1(esriFieldNameErrorType esriFieldNameErrorType_0)
        {
            string str;
            switch (esriFieldNameErrorType_0)
            {
                case esriFieldNameErrorType.esriSQLReservedWord:
                {
                    str = "SQL保留字";
                    break;
                }
                case esriFieldNameErrorType.esriDuplicatedFieldName:
                {
                    str = "字段名重复";
                    break;
                }
                case esriFieldNameErrorType.esriInvalidCharacter:
                {
                    str = "包含无效字符";
                    break;
                }
                case esriFieldNameErrorType.esriInvalidFieldNameLength:
                {
                    str = "字段名太长";
                    break;
                }
                default:
                {
                    str = "";
                    break;
                }
            }
            return str;
        }

        private IFeatureClass method_10(IFeatureClass ifeatureClass_0, IFeatureWorkspaceAnno ifeatureWorkspaceAnno_0,
            IFeatureDataset ifeatureDataset_0, IFeatureClass ifeatureClass_1, string string_0)
        {
            IAnnotateLayerPropertiesCollection annotateLayerPropertiesCollectionClass;
            ISymbolCollection symbolCollectionClass;
            IAnnoClass extension = ifeatureClass_0.Extension as IAnnoClass;
            double referenceScale = 1000;
            if (extension != null)
            {
                referenceScale = extension.ReferenceScale;
            }
            if (this.double_0 > 0)
            {
                referenceScale = this.double_0;
            }
            ISpatialReference spatialReference = null;
            IFields fields = ifeatureClass_0.Fields;
            int num = fields.FindField(ifeatureClass_0.ShapeFieldName);
            if (num != -1)
            {
                spatialReference = fields.Field[num].GeometryDef.SpatialReference;
                SpatialReferenctOperator.ChangeCoordinateSystem(ifeatureWorkspaceAnno_0 as IGeodatabaseRelease,
                    spatialReference, true);
            }
            IObjectClassDescription annotationFeatureClassDescriptionClass = new AnnotationFeatureClassDescription();
            IFeatureClassDescription featureClassDescription =
                annotationFeatureClassDescriptionClass as IFeatureClassDescription;
            IFields requiredFields = annotationFeatureClassDescriptionClass.RequiredFields;
            IFieldEdit field = null;
            int num1 = requiredFields.FindField(featureClassDescription.ShapeFieldName);
            field = requiredFields.Field[num1] as IFieldEdit;
            IGeometryDefEdit geometryDef = field.GeometryDef as IGeometryDefEdit;
            if (spatialReference == null)
            {
                spatialReference = geometryDef.SpatialReference;
                SpatialReferenctOperator.ChangeCoordinateSystem(ifeatureWorkspaceAnno_0 as IGeodatabaseRelease,
                    spatialReference, true);
            }
            geometryDef.SpatialReference_2 = spatialReference;
            field.GeometryDef_2 = geometryDef;
            esriUnits referenceScaleUnits = esriUnits.esriUnknownUnits;
            if (extension == null)
            {
                if (spatialReference is IProjectedCoordinateSystem)
                {
                    referenceScaleUnits = esriUnits.esriMeters;
                }
                else if (spatialReference is IGeographicCoordinateSystem)
                {
                    referenceScaleUnits = esriUnits.esriDecimalDegrees;
                }
                symbolCollectionClass = new SymbolCollection();
                ITextSymbol textSymbolClass = new TextSymbol();
                symbolCollectionClass.Symbol[0] = textSymbolClass as ISymbol;
                annotateLayerPropertiesCollectionClass = new AnnotateLayerPropertiesCollection();
                IAnnotateLayerProperties labelEngineLayerPropertiesClass =
                    new LabelEngineLayerProperties() as IAnnotateLayerProperties;


                labelEngineLayerPropertiesClass.Class = "要素类 1";
                labelEngineLayerPropertiesClass.FeatureLinked = false;
                labelEngineLayerPropertiesClass.AddUnplacedToGraphicsContainer = false;
                labelEngineLayerPropertiesClass.CreateUnplacedElements = true;
                labelEngineLayerPropertiesClass.DisplayAnnotation = true;
                labelEngineLayerPropertiesClass.UseOutput = true;

                ILabelEngineLayerProperties labelEngineLayerProperty =
                    labelEngineLayerPropertiesClass as ILabelEngineLayerProperties;
                labelEngineLayerProperty.Offset = 0;
                labelEngineLayerProperty.SymbolID = 0;
                labelEngineLayerProperty.Symbol = textSymbolClass;
                annotateLayerPropertiesCollectionClass.Add(labelEngineLayerPropertiesClass);
            }
            else
            {
                referenceScaleUnits = extension.ReferenceScaleUnits;
                annotateLayerPropertiesCollectionClass = extension.AnnoProperties;
                symbolCollectionClass = extension.SymbolCollection;
            }
            IGraphicsLayerScale graphicsLayerScaleClass = new GraphicsLayerScale()
            {
                ReferenceScale = referenceScale,
                Units = referenceScaleUnits
            };
            UID instanceCLSID = annotationFeatureClassDescriptionClass.InstanceCLSID;
            UID classExtensionCLSID = annotationFeatureClassDescriptionClass.ClassExtensionCLSID;
            IFeatureClass featureClass = ifeatureWorkspaceAnno_0.CreateAnnotationClass(string_0, requiredFields,
                instanceCLSID, classExtensionCLSID, featureClassDescription.ShapeFieldName, "", ifeatureDataset_0,
                ifeatureClass_1, annotateLayerPropertiesCollectionClass, graphicsLayerScaleClass, symbolCollectionClass,
                true);
            return featureClass;
        }

        private void method_11(IFeatureClass ifeatureClass_0, IName iname_0, string string_0)
        {
            IWorkspace workspace = null;
            IFeatureDataset featureDataset = null;
            if (iname_0 is IWorkspaceName)
            {
                workspace = iname_0.Open() as IWorkspace;
            }
            else if (iname_0 is IFeatureDatasetName)
            {
                featureDataset = iname_0.Open() as IFeatureDataset;
                workspace = featureDataset.Workspace;
            }
            string finalTableName = this.GetFinalTableName(workspace as IFeatureWorkspace, string_0);
            IFeatureClass featureClass = null;
            IRelationshipClass relationshipClass =
                ifeatureClass_0.RelationshipClasses[esriRelRole.esriRelRoleOrigin].Next();
            if (relationshipClass != null)
            {
                try
                {
                    if (relationshipClass.OriginClass != null)
                    {
                        string name = (relationshipClass.OriginClass as IDataset).Name;
                        string[] strArrays = name.Split(new char[] {'.'});
                        featureClass =
                            (workspace as IFeatureWorkspace).OpenFeatureClass(strArrays[(int) strArrays.Length - 1]);
                    }
                }
                catch
                {
                }
            }
            IFeatureClass featureClass1 = this.method_10(ifeatureClass_0, workspace as IFeatureWorkspaceAnno,
                featureDataset, featureClass, finalTableName);
            this.method_12(ifeatureClass_0, featureClass1);
        }

        private void method_12(IFeatureClass ifeatureClass_0, IFeatureClass ifeatureClass_1)
        {
            IFeatureBuffer featureBuffer;
            IAnnotationFeature2 annotationClassID;
            ITransactions workspace = (ifeatureClass_1 as IDataset).Workspace as ITransactions;
            if (!workspace.InTransaction)
            {
                workspace.StartTransaction();
            }
            int num = 0;
            IFeatureCursor featureCursor = ifeatureClass_0.Search(null, false);
            IFeature feature = featureCursor.NextFeature();
            IFeatureCursor featureCursor1 = ifeatureClass_1.Insert(true);
            while (feature != null)
            {
                if (!(feature is IAnnotationFeature2))
                {
                    try
                    {
                        int num1 = feature.Fields.FindField("Text");
                        object value = feature.Value[num1];
                        if (!(value is DBNull))
                        {
                            ITextElement textElement = this.MakeTextElement(value.ToString(), feature.Shape as IPoint);
                            num1 = feature.Fields.FindField("Height");
                            if (num1 != -1)
                            {
                                value = feature.Value[num1];
                                if (!(value is DBNull))
                                {
                                    textElement.Symbol.Size = double.Parse(value.ToString());
                                }
                            }
                            num1 = feature.Fields.FindField("TxtAngle");
                            if (num1 == -1)
                            {
                                num1 = feature.Fields.FindField("Angle");
                            }
                            if (num1 != -1)
                            {
                                value = feature.Value[num1];
                                if (!(value is DBNull))
                                {
                                    textElement.Symbol.Angle = double.Parse(value.ToString());
                                }
                            }
                            num1 = feature.Fields.FindField("Color");
                            if (num1 != -1)
                            {
                                value = feature.Value[num1];
                            }
                            featureBuffer = ifeatureClass_1.CreateFeatureBuffer();
                            annotationClassID = featureBuffer as IAnnotationFeature2;
                            annotationClassID.LinkedFeatureID = -1;
                            annotationClassID.AnnotationClassID = 0;
                            annotationClassID.Annotation = textElement as IElement;
                            featureCursor1.InsertFeature(featureBuffer);
                            num++;
                            if (num >= 800)
                            {
                                featureCursor1.Flush();
                                workspace.CommitTransaction();
                                num = 0;
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        exception.ToString();
                    }
                }
                else if ((feature as IAnnotationFeature2).Annotation != null)
                {
                    try
                    {
                        featureBuffer = ifeatureClass_1.CreateFeatureBuffer();
                        annotationClassID = featureBuffer as IAnnotationFeature2;
                        annotationClassID.LinkedFeatureID = (feature as IAnnotationFeature2).AnnotationClassID;
                        annotationClassID.AnnotationClassID = (feature as IAnnotationFeature2).AnnotationClassID;
                        annotationClassID.Annotation = (feature as IAnnotationFeature2).Annotation;
                        featureCursor1.InsertFeature(featureBuffer);
                        num++;
                        if (num >= 800)
                        {
                            featureCursor1.Flush();
                            workspace.CommitTransaction();
                            num = 0;
                        }
                    }
                    catch
                    {
                    }
                }
                feature = featureCursor.NextFeature();
            }
            if (num > 0)
            {
                featureCursor1.Flush();
                workspace.CommitTransaction();
            }
            Marshal.ReleaseComObject(featureCursor);
            featureCursor = null;
            Marshal.ReleaseComObject(featureCursor1);
            featureCursor1 = null;
        }

        private void method_13(IEnumInvalidObject ienumInvalidObject_0)
        {
            ienumInvalidObject_0.Reset();
            IInvalidObjectInfo invalidObjectInfo = ienumInvalidObject_0.Next();
            IList arrayLists = new ArrayList();
            while (invalidObjectInfo != null)
            {
                int invalidObjectID = invalidObjectInfo.InvalidObjectID;
                string str = string.Concat(invalidObjectID.ToString(), invalidObjectInfo.ErrorDescription);
                arrayLists.Add(str);
                invalidObjectInfo = ienumInvalidObject_0.Next();
            }
            if (arrayLists.Count > 0)
            {
                Logger.Current.Info(string.Concat(Application.StartupPath, "\\DataConvert.log"), arrayLists);
            }
        }

        private void method_14(ITable itable_0, IQueryFilter iqueryFilter_0, ITable itable_1, int int_0)
        {
            try
            {
                try
                {
                    int num = 0;
                    ICursor cursor = itable_1.Insert(true);
                    IRowBuffer rowBuffer = itable_1.CreateRowBuffer();
                    ICursor cursor1 = itable_0.Search(iqueryFilter_0, true);
                    for (IRow i = cursor1.NextRow(); i != null; i = cursor1.NextRow())
                    {
                        try
                        {
                            if (i is IFeature)
                            {
                                CreateFeatureTool.SetGeometry((i as IFeature).Shape, rowBuffer as IFeatureBuffer,
                                    itable_1 as IFeatureClass);
                            }
                            Dataloaders.AddFields(rowBuffer, i);
                            cursor.InsertRow(rowBuffer);
                            int num1 = num + 1;
                            num = num1;
                            if (num1 == int_0)
                            {
                                cursor.Flush();
                                num = 0;
                            }
                        }
                        catch (COMException cOMException1)
                        {
                            COMException cOMException = cOMException1;
                            if (cOMException.ErrorCode != -2147220936)
                            {
                                Logger.Current.Info(string.Concat(Application.StartupPath, "\\DataLoad.log"),
                                    cOMException.ToString());
                            }
                            else
                            {
                                string str = string.Concat(Application.StartupPath, "\\DataLoad.log");
                                int oID = i.OID;
                                Logger.Current.Info(str, string.Concat("对象", oID.ToString(), "坐标范围超界"));
                            }
                        }
                        catch (Exception exception1)
                        {
                            Exception exception = exception1;
                            Logger.Current.Info(string.Concat(Application.StartupPath, "\\DataLoad.log"),
                                exception.ToString());
                        }
                        if (this.ifeatureProgress_StepEventHandler_0 != null)
                        {
                            this.ifeatureProgress_StepEventHandler_0();
                        }
                    }
                    if (num > 0)
                    {
                        cursor.Flush();
                    }
                    ComReleaser.ReleaseCOMObject(cursor);
                }
                catch (Exception exception2)
                {
                    MessageBox.Show(exception2.Message);
                }
            }
            finally
            {
            }
        }

        private long method_2(long long_0, long long_1)
        {
            return (long_0 < long_1 ? long_0 : long_1);
        }

        private long method_3(long long_0, long long_1)
        {
            return (long_0 > long_1 ? long_0 : long_1);
        }

        private double method_4(double double_1, double double_2)
        {
            return (double_1 < double_2 ? double_1 : double_2);
        }

        private double method_5(double double_1, double double_2)
        {
            return (double_1 > double_2 ? double_1 : double_2);
        }

        private int method_6(int int_0, int int_1)
        {
            return (int_0 < int_1 ? int_0 : int_1);
        }

        private int method_7(int int_0, int int_1)
        {
            return (int_0 > int_1 ? int_0 : int_1);
        }

        private double method_8(IFeatureClass ifeatureClass_0)
        {
            double num;
            IEnvelope extent = (ifeatureClass_0 as IGeoDataset).Extent;
            int num1 = ifeatureClass_0.FeatureCount(null);
            if ((num1 == 0 ? false : !extent.IsEmpty))
            {
                double height = extent.Height*extent.Width;
                num = Math.Sqrt(height/(double) num1);
            }
            else
            {
                num = 1000;
            }
            return num;
        }

        private double method_9(IFeatureClass ifeatureClass_0)
        {
            double num;
            double num1 = 0;
            ArrayList arrayLists = new ArrayList();
            double num2 = 0;
            double num3 = 1000000000000;
            double num4 = 1;
            int num5 = 1;
            int num6 = 1;
            int num7 = 0;
            try
            {
                num7 = ifeatureClass_0.FeatureCount(null) - 1;
            }
            catch (Exception exception)
            {
                Logger.Current.Error("设置格网大小", exception, null);
            }
            if (num7 > 0)
            {
                if ((ifeatureClass_0.ShapeType == esriGeometryType.esriGeometryMultipoint
                    ? true
                    : ifeatureClass_0.ShapeType == esriGeometryType.esriGeometryPoint))
                {
                    num1 = this.method_8(ifeatureClass_0);
                }
                int num8 = num7*num5;
                if (num8 > 1000)
                {
                    num8 = 1000;
                }
                int num9 = num7/num8;
                for (int i = 0; i < num7; i = i + num9)
                {
                    arrayLists.Add(i);
                }
                string oIDFieldName = ifeatureClass_0.OIDFieldName;
                for (int j = 0; j < arrayLists.Count; j = j + 250)
                {
                    int num10 = this.method_6(arrayLists.Count - j, 250);
                    string str = string.Concat(oIDFieldName, " IN(");
                    for (int k = 0; k < num10; k++)
                    {
                        str = string.Concat(str, arrayLists[j + k].ToString(), ",");
                    }
                    str = string.Concat(str.Substring(0, str.Length - 1), ")");
                    IQueryFilter queryFilterClass = new QueryFilter()
                    {
                        WhereClause = str
                    };
                    IFeatureCursor featureCursor = ifeatureClass_0.Search(queryFilterClass, true);
                    for (IFeature l = featureCursor.NextFeature(); l != null; l = featureCursor.NextFeature())
                    {
                        IEnvelope extent = l.Extent;
                        num2 = this.method_5(num2, this.method_5(extent.Width, extent.Height));
                        num3 = this.method_4(num3, this.method_4(extent.Width, extent.Height));
                        num4 = (num3 == 0
                            ? num4 + 0.0001
                            : num4 +
                              this.method_4(extent.Width, extent.Height)/this.method_5(extent.Width, extent.Height));
                    }
                    Marshal.ReleaseComObject(featureCursor);
                    featureCursor = null;
                    num1 = (num4/(double) num8 <= 0.5 ? num2/2*(double) num6 : (num3 + (num2 - num3)/2)*(double) num6);
                }
                num = num1;
            }
            else
            {
                num = 1000;
            }
            return num;
        }

        public event FinishHander FinishEvent
        {
            add
            {
                FinishHander finishHander;
                FinishHander finishHander0 = this.finishHander_0;
                do
                {
                    finishHander = finishHander0;
                    FinishHander finishHander1 = (FinishHander) Delegate.Combine(finishHander, value);
                    finishHander0 = Interlocked.CompareExchange<FinishHander>(ref this.finishHander_0, finishHander1,
                        finishHander);
                } while ((object) finishHander0 != (object) finishHander);
            }
            remove
            {
                FinishHander finishHander;
                FinishHander finishHander0 = this.finishHander_0;
                do
                {
                    finishHander = finishHander0;
                    FinishHander finishHander1 = (FinishHander) Delegate.Remove(finishHander, value);
                    finishHander0 = Interlocked.CompareExchange<FinishHander>(ref this.finishHander_0, finishHander1,
                        finishHander);
                } while ((object) finishHander0 != (object) finishHander);
            }
        }

        public event SetFeatureClassNameEnventHandler SetFeatureClassNameEnvent
        {
            add
            {
                SetFeatureClassNameEnventHandler setFeatureClassNameEnventHandler;
                SetFeatureClassNameEnventHandler setFeatureClassNameEnventHandler0 =
                    this.setFeatureClassNameEnventHandler_0;
                do
                {
                    setFeatureClassNameEnventHandler = setFeatureClassNameEnventHandler0;
                    SetFeatureClassNameEnventHandler setFeatureClassNameEnventHandler1 =
                        (SetFeatureClassNameEnventHandler) Delegate.Combine(setFeatureClassNameEnventHandler, value);
                    setFeatureClassNameEnventHandler0 =
                        Interlocked.CompareExchange<SetFeatureClassNameEnventHandler>(
                            ref this.setFeatureClassNameEnventHandler_0, setFeatureClassNameEnventHandler1,
                            setFeatureClassNameEnventHandler);
                } while ((object) setFeatureClassNameEnventHandler0 != (object) setFeatureClassNameEnventHandler);
            }
            remove
            {
                SetFeatureClassNameEnventHandler setFeatureClassNameEnventHandler;
                SetFeatureClassNameEnventHandler setFeatureClassNameEnventHandler0 =
                    this.setFeatureClassNameEnventHandler_0;
                do
                {
                    setFeatureClassNameEnventHandler = setFeatureClassNameEnventHandler0;
                    SetFeatureClassNameEnventHandler setFeatureClassNameEnventHandler1 =
                        (SetFeatureClassNameEnventHandler) Delegate.Remove(setFeatureClassNameEnventHandler, value);
                    setFeatureClassNameEnventHandler0 =
                        Interlocked.CompareExchange<SetFeatureClassNameEnventHandler>(
                            ref this.setFeatureClassNameEnventHandler_0, setFeatureClassNameEnventHandler1,
                            setFeatureClassNameEnventHandler);
                } while ((object) setFeatureClassNameEnventHandler0 != (object) setFeatureClassNameEnventHandler);
            }
        }

        public event SetFeatureCountEnventHandler SetFeatureCountEnvent
        {
            add
            {
                SetFeatureCountEnventHandler setFeatureCountEnventHandler;
                SetFeatureCountEnventHandler setFeatureCountEnventHandler0 = this.setFeatureCountEnventHandler_0;
                do
                {
                    setFeatureCountEnventHandler = setFeatureCountEnventHandler0;
                    SetFeatureCountEnventHandler setFeatureCountEnventHandler1 =
                        (SetFeatureCountEnventHandler) Delegate.Combine(setFeatureCountEnventHandler, value);
                    setFeatureCountEnventHandler0 =
                        Interlocked.CompareExchange<SetFeatureCountEnventHandler>(
                            ref this.setFeatureCountEnventHandler_0, setFeatureCountEnventHandler1,
                            setFeatureCountEnventHandler);
                } while ((object) setFeatureCountEnventHandler0 != (object) setFeatureCountEnventHandler);
            }
            remove
            {
                SetFeatureCountEnventHandler setFeatureCountEnventHandler;
                SetFeatureCountEnventHandler setFeatureCountEnventHandler0 = this.setFeatureCountEnventHandler_0;
                do
                {
                    setFeatureCountEnventHandler = setFeatureCountEnventHandler0;
                    SetFeatureCountEnventHandler setFeatureCountEnventHandler1 =
                        (SetFeatureCountEnventHandler) Delegate.Remove(setFeatureCountEnventHandler, value);
                    setFeatureCountEnventHandler0 =
                        Interlocked.CompareExchange<SetFeatureCountEnventHandler>(
                            ref this.setFeatureCountEnventHandler_0, setFeatureCountEnventHandler1,
                            setFeatureCountEnventHandler);
                } while ((object) setFeatureCountEnventHandler0 != (object) setFeatureCountEnventHandler);
            }
        }

        public event SetMaxValueHandler SetMaxValueEvent
        {
            add
            {
                SetMaxValueHandler setMaxValueHandler;
                SetMaxValueHandler setMaxValueHandler0 = this.setMaxValueHandler_0;
                do
                {
                    setMaxValueHandler = setMaxValueHandler0;
                    SetMaxValueHandler setMaxValueHandler1 =
                        (SetMaxValueHandler) Delegate.Combine(setMaxValueHandler, value);
                    setMaxValueHandler0 = Interlocked.CompareExchange<SetMaxValueHandler>(
                        ref this.setMaxValueHandler_0, setMaxValueHandler1, setMaxValueHandler);
                } while ((object) setMaxValueHandler0 != (object) setMaxValueHandler);
            }
            remove
            {
                SetMaxValueHandler setMaxValueHandler;
                SetMaxValueHandler setMaxValueHandler0 = this.setMaxValueHandler_0;
                do
                {
                    setMaxValueHandler = setMaxValueHandler0;
                    SetMaxValueHandler setMaxValueHandler1 =
                        (SetMaxValueHandler) Delegate.Remove(setMaxValueHandler, value);
                    setMaxValueHandler0 = Interlocked.CompareExchange<SetMaxValueHandler>(
                        ref this.setMaxValueHandler_0, setMaxValueHandler1, setMaxValueHandler);
                } while ((object) setMaxValueHandler0 != (object) setMaxValueHandler);
            }
        }

        public event SetMessageHandler SetMessageEvent
        {
            add
            {
                SetMessageHandler setMessageHandler;
                SetMessageHandler setMessageHandler0 = this.setMessageHandler_0;
                do
                {
                    setMessageHandler = setMessageHandler0;
                    SetMessageHandler setMessageHandler1 =
                        (SetMessageHandler) Delegate.Combine(setMessageHandler, value);
                    setMessageHandler0 = Interlocked.CompareExchange<SetMessageHandler>(ref this.setMessageHandler_0,
                        setMessageHandler1, setMessageHandler);
                } while ((object) setMessageHandler0 != (object) setMessageHandler);
            }
            remove
            {
                SetMessageHandler setMessageHandler;
                SetMessageHandler setMessageHandler0 = this.setMessageHandler_0;
                do
                {
                    setMessageHandler = setMessageHandler0;
                    SetMessageHandler setMessageHandler1 = (SetMessageHandler) Delegate.Remove(setMessageHandler, value);
                    setMessageHandler0 = Interlocked.CompareExchange<SetMessageHandler>(ref this.setMessageHandler_0,
                        setMessageHandler1, setMessageHandler);
                } while ((object) setMessageHandler0 != (object) setMessageHandler);
            }
        }

        public event SetMinValueHandler SetMinValueEvent
        {
            add
            {
                SetMinValueHandler setMinValueHandler;
                SetMinValueHandler setMinValueHandler0 = this.setMinValueHandler_0;
                do
                {
                    setMinValueHandler = setMinValueHandler0;
                    SetMinValueHandler setMinValueHandler1 =
                        (SetMinValueHandler) Delegate.Combine(setMinValueHandler, value);
                    setMinValueHandler0 = Interlocked.CompareExchange<SetMinValueHandler>(
                        ref this.setMinValueHandler_0, setMinValueHandler1, setMinValueHandler);
                } while ((object) setMinValueHandler0 != (object) setMinValueHandler);
            }
            remove
            {
                SetMinValueHandler setMinValueHandler;
                SetMinValueHandler setMinValueHandler0 = this.setMinValueHandler_0;
                do
                {
                    setMinValueHandler = setMinValueHandler0;
                    SetMinValueHandler setMinValueHandler1 =
                        (SetMinValueHandler) Delegate.Remove(setMinValueHandler, value);
                    setMinValueHandler0 = Interlocked.CompareExchange<SetMinValueHandler>(
                        ref this.setMinValueHandler_0, setMinValueHandler1, setMinValueHandler);
                } while ((object) setMinValueHandler0 != (object) setMinValueHandler);
            }
        }

        public event SetPositionHandler SetPositionEvent
        {
            add
            {
                SetPositionHandler setPositionHandler;
                SetPositionHandler setPositionHandler0 = this.setPositionHandler_0;
                do
                {
                    setPositionHandler = setPositionHandler0;
                    SetPositionHandler setPositionHandler1 =
                        (SetPositionHandler) Delegate.Combine(setPositionHandler, value);
                    setPositionHandler0 = Interlocked.CompareExchange<SetPositionHandler>(
                        ref this.setPositionHandler_0, setPositionHandler1, setPositionHandler);
                } while ((object) setPositionHandler0 != (object) setPositionHandler);
            }
            remove
            {
                SetPositionHandler setPositionHandler;
                SetPositionHandler setPositionHandler0 = this.setPositionHandler_0;
                do
                {
                    setPositionHandler = setPositionHandler0;
                    SetPositionHandler setPositionHandler1 =
                        (SetPositionHandler) Delegate.Remove(setPositionHandler, value);
                    setPositionHandler0 = Interlocked.CompareExchange<SetPositionHandler>(
                        ref this.setPositionHandler_0, setPositionHandler1, setPositionHandler);
                } while ((object) setPositionHandler0 != (object) setPositionHandler);
            }
        }

        public event ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler Step
        {
            add
            {
                ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler featureProgressStepEventHandler;
                ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler ifeatureProgressStepEventHandler0 =
                    this.ifeatureProgress_StepEventHandler_0;
                do
                {
                    featureProgressStepEventHandler = ifeatureProgressStepEventHandler0;
                    ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler featureProgressStepEventHandler1 =
                        (ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler)
                        Delegate.Combine(featureProgressStepEventHandler, value);
                    ifeatureProgressStepEventHandler0 =
                        Interlocked.CompareExchange<ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler>(
                            ref this.ifeatureProgress_StepEventHandler_0, featureProgressStepEventHandler1,
                            featureProgressStepEventHandler);
                } while ((object) ifeatureProgressStepEventHandler0 != (object) featureProgressStepEventHandler);
            }
            remove
            {
                ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler featureProgressStepEventHandler;
                ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler ifeatureProgressStepEventHandler0 =
                    this.ifeatureProgress_StepEventHandler_0;
                do
                {
                    featureProgressStepEventHandler = ifeatureProgressStepEventHandler0;
                    ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler featureProgressStepEventHandler1 =
                        (ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler)
                        Delegate.Remove(featureProgressStepEventHandler, value);
                    ifeatureProgressStepEventHandler0 =
                        Interlocked.CompareExchange<ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler>(
                            ref this.ifeatureProgress_StepEventHandler_0, featureProgressStepEventHandler1,
                            featureProgressStepEventHandler);
                } while ((object) ifeatureProgressStepEventHandler0 != (object) featureProgressStepEventHandler);
            }
        }
    }
}