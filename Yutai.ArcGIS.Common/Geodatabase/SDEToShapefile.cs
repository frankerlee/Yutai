using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Geodatabase
{
    public class SDEToShapefile : IConvertEvent, IConvertEventEx
    {
        private List<IFeatureClass> list_0 = null;

        private string string_0 = "c:\\";

        private IGeometry igeometry_0 = null;

        private bool bool_0 = false;

        private SetHandleFeatureInfoHandler setHandleFeatureInfoHandler_0;

        private SetFeatureClassMinValueHandler setFeatureClassMinValueHandler_0;

        private SetFeatureClassMaxValueHandler setFeatureClassMaxValueHandler_0;

        private SetFeatureClassPositionHandler setFeatureClassPositionHandler_0;

        private SetFeatureClassNameEnventHandler setFeatureClassNameEnventHandler_0;

        private SetFeatureCountEnventHandler setFeatureCountEnventHandler_0;

        private SetMaxValueHandler setMaxValueHandler_0;

        private SetMinValueHandler setMinValueHandler_0;

        private SetPositionHandler setPositionHandler_0;

        private SetMessageHandler setMessageHandler_0;

        private FinishHander finishHander_0;

        public IGeometry ClipGeometry
        {
            set { this.igeometry_0 = value; }
        }

        public List<IFeatureClass> InputFeatureClasses
        {
            set { this.list_0 = value; }
        }

        public bool IsClip
        {
            set { this.bool_0 = value; }
        }

        public string OutPath
        {
            set { this.string_0 = value; }
        }

        public SDEToShapefile()
        {
            list_0 = new List<IFeatureClass>();
        }

        public void AddFeatureClasses(IList<IFeatureClass> lists)
        {
            list_0.AddRange(lists);
        }

        private static void AddFields(IRow irow_0, IRow irow_1,
            System.Collections.Generic.List<SDEToShapefile.FieldMap> list_1)
        {
            try
            {
                IFields fields = irow_0.Fields;
                IFields fields2 = irow_1.Fields;
                for (int i = 0; i < fields2.FieldCount; i++)
                {
                    IField field = fields2.get_Field(i);
                    if (field.Type != esriFieldType.esriFieldTypeGeometry &&
                        field.Type != esriFieldType.esriFieldTypeOID && field.Editable)
                    {
                        int j = 0;
                        while (j < list_1.Count)
                        {
                            if (!(list_1[j].SourceName == field.Name))
                            {
                                j++;
                            }
                            else
                            {
                                int num = fields.FindField(list_1[j].ToName);
                                if (num == -1)
                                {
                                    break;
                                }
                                object obj = irow_1.get_Value(i);
                                if (obj is System.DBNull && !fields.get_Field(num).IsNullable)
                                {
                                    irow_0.set_Value(num, "");
                                    break;
                                }
                                irow_0.set_Value(num, obj);
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private static void AddFields(IRowBuffer irowBuffer_0, IRow irow_0,
            System.Collections.Generic.List<SDEToShapefile.FieldMap> list_1)
        {
            IFields fields = irowBuffer_0.Fields;
            IFields fields2 = irow_0.Fields;
            for (int i = 0; i < fields2.FieldCount; i++)
            {
                IField field = fields2.get_Field(i);
                if (field.Type != esriFieldType.esriFieldTypeGeometry && field.Type != esriFieldType.esriFieldTypeOID &&
                    field.Editable)
                {
                    int j = 0;
                    while (j < list_1.Count)
                    {
                        if (!(list_1[j].SourceName == field.Name))
                        {
                            j++;
                        }
                        else
                        {
                            int num = fields.FindField(list_1[j].ToName);
                            if (num != -1)
                            {
                                irowBuffer_0.set_Value(num, irow_0.get_Value(i));
                                break;
                            }
                            break;
                        }
                    }
                }
            }
        }

        public void Convert()
        {
            if (this.list_0.Count != 0)
            {
                IWorkspaceFactory shapefileWorkspaceFactoryClass = new ShapefileWorkspaceFactory();
                IPropertySet propertySetClass = new PropertySet();
                propertySetClass.SetProperty("DATABASE", this.string_0);
                IWorkspace workspace = shapefileWorkspaceFactoryClass.Open(propertySetClass, 0);
                frmExportProcessEx _frmExportProcessEx = new frmExportProcessEx()
                {
                    ConvertEvent = this
                };
                _frmExportProcessEx.Show();
                if (this.setMinValueHandler_0 != null)
                {
                    this.setMinValueHandler_0(0);
                }
                if (this.setMaxValueHandler_0 != null)
                {
                    this.setMaxValueHandler_0(this.list_0.Count);
                }
                for (int i = 0; i < this.list_0.Count; i++)
                {
                    IFeatureClass item = this.list_0[i];
                    if (this.setPositionHandler_0 != null)
                    {
                        this.setPositionHandler_0(i);
                    }
                    if (item != null)
                    {
                        ISpatialFilter spatialFilterClass = null;
                        if (this.igeometry_0 != null)
                        {
                            spatialFilterClass = new SpatialFilter()
                            {
                                GeometryField = item.ShapeFieldName,
                                Geometry = this.igeometry_0
                            };
                            if (item.ShapeType != esriGeometryType.esriGeometryPoint)
                            {
                                spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                            }
                            else
                            {
                                spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                            }
                        }
                        int num = item.FeatureCount(spatialFilterClass);
                        if (num != 0)
                        {
                            if (this.setHandleFeatureInfoHandler_0 != null)
                            {
                                this.setHandleFeatureInfoHandler_0(item.AliasName);
                            }
                            string str = string.Format("  正在处理要素类{0}", item.AliasName);
                            if (this.setMessageHandler_0 != null)
                            {
                                this.setMessageHandler_0(str);
                            }
                            if (this.setFeatureClassMinValueHandler_0 != null)
                            {
                                this.setFeatureClassMinValueHandler_0(0);
                            }
                            if (this.setFeatureClassMaxValueHandler_0 != null)
                            {
                                this.setFeatureClassMaxValueHandler_0(num);
                            }
                            this.method_1(item, workspace);
                        }
                        else if (this.setMessageHandler_0 != null)
                        {
                            this.setMessageHandler_0(string.Format("{0}无选中要素，将不导出！", item.AliasName));
                        }
                    }
                }
                if (this.finishHander_0 != null)
                {
                    this.finishHander_0();
                }
                if (_frmExportProcessEx.IsAutoClose)
                {
                    _frmExportProcessEx.Close();
                }
            }
        }

        public void Convert(IWorkspace iworkspace_0)
        {
            if (this.list_0.Count != 0)
            {
                frmExportProcessEx _frmExportProcessEx = new frmExportProcessEx()
                {
                    ConvertEvent = this
                };
                _frmExportProcessEx.Show();
                if (this.setMinValueHandler_0 != null)
                {
                    this.setMinValueHandler_0(0);
                }
                if (this.setMaxValueHandler_0 != null)
                {
                    this.setMaxValueHandler_0(this.list_0.Count);
                }
                for (int i = 0; i < this.list_0.Count; i++)
                {
                    IFeatureClass item = this.list_0[i];
                    if (this.setPositionHandler_0 != null)
                    {
                        this.setPositionHandler_0(i);
                    }
                    ISpatialFilter spatialFilterClass = null;
                    if (this.igeometry_0 != null)
                    {
                        spatialFilterClass = new SpatialFilter()
                        {
                            GeometryField = item.ShapeFieldName,
                            Geometry = this.igeometry_0
                        };
                        if (item.ShapeType != esriGeometryType.esriGeometryPoint)
                        {
                            spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                        }
                        else
                        {
                            spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                        }
                    }
                    int num = item.FeatureCount(spatialFilterClass);
                    if (num == 0)
                    {
                        if (this.setMessageHandler_0 != null)
                        {
                            this.setMessageHandler_0(string.Format("{0}无选中要素，将不导出！", item.AliasName));
                        }
                    }
                    else if (item != null)
                    {
                        if (this.setHandleFeatureInfoHandler_0 != null)
                        {
                            this.setHandleFeatureInfoHandler_0(item.AliasName);
                        }
                        string str = string.Format("  正在处理要素类{0}", item.AliasName);
                        if (this.setMessageHandler_0 != null)
                        {
                            this.setMessageHandler_0(str);
                        }
                        if (this.setFeatureClassMinValueHandler_0 != null)
                        {
                            this.setFeatureClassMinValueHandler_0(0);
                        }
                        if (this.setFeatureClassMaxValueHandler_0 != null)
                        {
                            this.setFeatureClassMaxValueHandler_0(num);
                        }
                        this.method_1(item, iworkspace_0);
                    }
                }
                if (this.finishHander_0 != null)
                {
                    this.finishHander_0();
                }
                if (_frmExportProcessEx.IsAutoClose)
                {
                    _frmExportProcessEx.Close();
                }
            }
        }

        private IGeometry method_0(IGeometry igeometry_1)
        {
            IGeometry igeometry1;
            if (this.igeometry_0 == null)
            {
                igeometry1 = igeometry_1;
            }
            else if (this.bool_0)
            {
                bool flag = false;
                try
                {
                    flag = (this.igeometry_0 as IRelationalOperator).Contains(igeometry_1);
                }
                catch
                {
                }
                if (!flag)
                {
                    IGeometry zAware = null;
                    ITopologicalOperator igeometry0 = (ITopologicalOperator) this.igeometry_0;
                    if (igeometry_1.GeometryType == esriGeometryType.esriGeometryMultipoint)
                    {
                        zAware = igeometry0.Intersect(igeometry_1, esriGeometryDimension.esriGeometry0Dimension);
                        (zAware as IZAware).ZAware = (igeometry_1 as IZAware).ZAware;
                        (zAware as IMAware).MAware = (igeometry_1 as IMAware).MAware;
                    }
                    else if (igeometry_1.GeometryType == esriGeometryType.esriGeometryPolygon)
                    {
                        zAware = igeometry0.Intersect(igeometry_1, esriGeometryDimension.esriGeometry2Dimension);
                        (zAware as IZAware).ZAware = (igeometry_1 as IZAware).ZAware;
                        if ((zAware as IZAware).ZAware)
                        {
                            (zAware as IZ).SetConstantZ((igeometry_1 as IZ).ZMin);
                        }
                        (zAware as IMAware).MAware = (igeometry_1 as IMAware).MAware;
                    }
                    else if (igeometry_1.GeometryType != esriGeometryType.esriGeometryPolyline)
                    {
                        zAware = igeometry_1;
                    }
                    else
                    {
                        try
                        {
                            zAware = igeometry0.Intersect(igeometry_1, esriGeometryDimension.esriGeometry1Dimension);
                            (zAware as IZAware).ZAware = (igeometry_1 as IZAware).ZAware;
                            (zAware as IMAware).MAware = (igeometry_1 as IMAware).MAware;
                        }
                        catch
                        {
                            zAware = igeometry_1;
                        }
                    }
                    igeometry1 = zAware;
                }
                else
                {
                    igeometry1 = igeometry_1;
                }
            }
            else
            {
                igeometry1 = igeometry_1;
            }
            return igeometry1;
        }

        private void method_1(IFeatureClass ifeatureClass_0, IWorkspace iworkspace_0)
        {
            IEnumFieldError enumFieldError;
            IFields field;
            int i;
            IField field1;
            string str;
            string aliasName = ifeatureClass_0.AliasName;
            string[] strArrays = aliasName.Split(new char[] {'.'});
            aliasName = strArrays[(int) strArrays.Length - 1];
            IFields fields = ifeatureClass_0.Fields;
            IFieldChecker fieldCheckerClass = new FieldChecker()
            {
                ValidateWorkspace = iworkspace_0
            };
            fieldCheckerClass.Validate(fields, out enumFieldError, out field);
            List<SDEToShapefile.FieldMap> fieldMaps = new List<SDEToShapefile.FieldMap>();
            for (i = 0; i < fields.FieldCount; i++)
            {
                field1 = fields.Field[i];
                if (field1.Type != esriFieldType.esriFieldTypeBlob)
                {
                    IField field2 = field.Field[i];
                    fieldMaps.Add(new SDEToShapefile.FieldMap(field1.Name, field2.Name));
                }
            }
            for (i = field.FieldCount - 1; i >= 0; i--)
            {
                field1 = field.Field[i];
                if (field1.Type == esriFieldType.esriFieldTypeBlob)
                {
                    (field as IFieldsEdit).DeleteField(field1);
                }
            }
            fieldCheckerClass.ValidateTableName(aliasName, out str);
            aliasName = string.Concat(this.string_0, "\\", str);
            if (File.Exists(string.Concat(aliasName, ".shp")))
            {
                File.Delete(string.Concat(aliasName, ".shp"));
            }
            if (File.Exists(string.Concat(aliasName, ".shx")))
            {
                File.Delete(string.Concat(aliasName, ".shx"));
            }
            if (File.Exists(string.Concat(aliasName, ".dbf")))
            {
                File.Delete(string.Concat(aliasName, ".dbf"));
            }
            int num = field.FindField(ifeatureClass_0.ShapeFieldName);
            IFieldEdit fieldEdit = field.Field[num] as IFieldEdit;
            IGeometryDefEdit geometryDef = fieldEdit.GeometryDef as IGeometryDefEdit;
            if (ifeatureClass_0.FeatureType == esriFeatureType.esriFTAnnotation)
            {
                geometryDef.GeometryType_2 = esriGeometryType.esriGeometryPoint;
            }
            ISpatialReference spatialReference = geometryDef.SpatialReference;
            SpatialReferenctOperator.ChangeCoordinateSystem(iworkspace_0 as IGeodatabaseRelease, spatialReference, true);
            geometryDef.SpatialReference_2 = spatialReference;
            fieldEdit.GeometryDef_2 = geometryDef;
            try
            {
                IFeatureClass featureClass = (iworkspace_0 as IFeatureWorkspace).CreateFeatureClass(str, field, null,
                    null, esriFeatureType.esriFTSimple, ifeatureClass_0.ShapeFieldName, "");
                this.method_2(ifeatureClass_0 as ITable, featureClass as ITable, fieldMaps, 800);
            }
            catch
            {
            }
        }

        private void method_2(ITable itable_0, ITable itable_1, List<SDEToShapefile.FieldMap> list_1, int int_0)
        {
            IGeometry geometry;
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
                ISpatialFilter spatialFilterClass = null;
                if (this.igeometry_0 != null)
                {
                    spatialFilterClass = new SpatialFilter()
                    {
                        GeometryField = (itable_0 as IFeatureClass).ShapeFieldName,
                        Geometry = this.igeometry_0
                    };
                    if ((itable_0 as IFeatureClass).ShapeType != esriGeometryType.esriGeometryPoint)
                    {
                        spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    }
                    else
                    {
                        spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                    }
                }
                ICursor cursor = itable_0.Search(spatialFilterClass, true);
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
                int num = 0;
                while (row != null)
                {
                    if (this.setFeatureClassPositionHandler_0 != null)
                    {
                        this.setFeatureClassPositionHandler_0(num);
                    }
                    if (this.setMessageHandler_0 != null)
                    {
                        this.setMessageHandler_0(string.Format("  正在转换要素:{0}", row.OID));
                    }
                    num++;
                    IRow row1 = null;
                    try
                    {
                        if (!(row is IFeature))
                        {
                            row1 = itable_1.CreateRow();
                            SDEToShapefile.AddFields(row1, row, list_1);
                            row1.Store();
                        }
                        else
                        {
                            if ((itable_0 as IFeatureClass).FeatureType != esriFeatureType.esriFTAnnotation)
                            {
                                geometry = this.method_0((row as IFeature).ShapeCopy);
                            }
                            else
                            {
                                IGeometry geometry1 =
                                    ((row as IAnnotationFeature).Annotation as ITextElement as IElement).Geometry;
                                IPoint lowerLeft = geometry1.Envelope.LowerLeft;
                                if (geometry1 is IPoint)
                                {
                                    lowerLeft = geometry1 as IPoint;
                                }
                                geometry = lowerLeft;
                            }
                            if (!geometry.IsEmpty)
                            {
                                if (!(spatialReference is IUnknownCoordinateSystem))
                                {
                                    geometry.Project(spatialReference);
                                }
                                row1 = itable_1.CreateRow();
                                (row1 as IFeature).Shape = geometry;
                                SDEToShapefile.AddFields(row1, row, list_1);
                                row1.Store();
                            }
                        }
                        if (this.setMessageHandler_0 != null)
                        {
                            this.setMessageHandler_0("  转换成功");
                        }
                    }
                    catch (COMException cOMException)
                    {
                    }
                    catch
                    {
                    }
                    row = cursor.NextRow();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
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

        public event SetFeatureClassMaxValueHandler SetFeatureClassMaxValueEvent
        {
            add
            {
                SetFeatureClassMaxValueHandler setFeatureClassMaxValueHandler;
                SetFeatureClassMaxValueHandler setFeatureClassMaxValueHandler0 = this.setFeatureClassMaxValueHandler_0;
                do
                {
                    setFeatureClassMaxValueHandler = setFeatureClassMaxValueHandler0;
                    SetFeatureClassMaxValueHandler setFeatureClassMaxValueHandler1 =
                        (SetFeatureClassMaxValueHandler) Delegate.Combine(setFeatureClassMaxValueHandler, value);
                    setFeatureClassMaxValueHandler0 =
                        Interlocked.CompareExchange<SetFeatureClassMaxValueHandler>(
                            ref this.setFeatureClassMaxValueHandler_0, setFeatureClassMaxValueHandler1,
                            setFeatureClassMaxValueHandler);
                } while ((object) setFeatureClassMaxValueHandler0 != (object) setFeatureClassMaxValueHandler);
            }
            remove
            {
                SetFeatureClassMaxValueHandler setFeatureClassMaxValueHandler;
                SetFeatureClassMaxValueHandler setFeatureClassMaxValueHandler0 = this.setFeatureClassMaxValueHandler_0;
                do
                {
                    setFeatureClassMaxValueHandler = setFeatureClassMaxValueHandler0;
                    SetFeatureClassMaxValueHandler setFeatureClassMaxValueHandler1 =
                        (SetFeatureClassMaxValueHandler) Delegate.Remove(setFeatureClassMaxValueHandler, value);
                    setFeatureClassMaxValueHandler0 =
                        Interlocked.CompareExchange<SetFeatureClassMaxValueHandler>(
                            ref this.setFeatureClassMaxValueHandler_0, setFeatureClassMaxValueHandler1,
                            setFeatureClassMaxValueHandler);
                } while ((object) setFeatureClassMaxValueHandler0 != (object) setFeatureClassMaxValueHandler);
            }
        }

        public event SetFeatureClassMinValueHandler SetFeatureClassMinValueEvent
        {
            add
            {
                SetFeatureClassMinValueHandler setFeatureClassMinValueHandler;
                SetFeatureClassMinValueHandler setFeatureClassMinValueHandler0 = this.setFeatureClassMinValueHandler_0;
                do
                {
                    setFeatureClassMinValueHandler = setFeatureClassMinValueHandler0;
                    SetFeatureClassMinValueHandler setFeatureClassMinValueHandler1 =
                        (SetFeatureClassMinValueHandler) Delegate.Combine(setFeatureClassMinValueHandler, value);
                    setFeatureClassMinValueHandler0 =
                        Interlocked.CompareExchange<SetFeatureClassMinValueHandler>(
                            ref this.setFeatureClassMinValueHandler_0, setFeatureClassMinValueHandler1,
                            setFeatureClassMinValueHandler);
                } while ((object) setFeatureClassMinValueHandler0 != (object) setFeatureClassMinValueHandler);
            }
            remove
            {
                SetFeatureClassMinValueHandler setFeatureClassMinValueHandler;
                SetFeatureClassMinValueHandler setFeatureClassMinValueHandler0 = this.setFeatureClassMinValueHandler_0;
                do
                {
                    setFeatureClassMinValueHandler = setFeatureClassMinValueHandler0;
                    SetFeatureClassMinValueHandler setFeatureClassMinValueHandler1 =
                        (SetFeatureClassMinValueHandler) Delegate.Remove(setFeatureClassMinValueHandler, value);
                    setFeatureClassMinValueHandler0 =
                        Interlocked.CompareExchange<SetFeatureClassMinValueHandler>(
                            ref this.setFeatureClassMinValueHandler_0, setFeatureClassMinValueHandler1,
                            setFeatureClassMinValueHandler);
                } while ((object) setFeatureClassMinValueHandler0 != (object) setFeatureClassMinValueHandler);
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

        public event SetFeatureClassPositionHandler SetFeatureClassPositionEvent
        {
            add
            {
                SetFeatureClassPositionHandler setFeatureClassPositionHandler;
                SetFeatureClassPositionHandler setFeatureClassPositionHandler0 = this.setFeatureClassPositionHandler_0;
                do
                {
                    setFeatureClassPositionHandler = setFeatureClassPositionHandler0;
                    SetFeatureClassPositionHandler setFeatureClassPositionHandler1 =
                        (SetFeatureClassPositionHandler) Delegate.Combine(setFeatureClassPositionHandler, value);
                    setFeatureClassPositionHandler0 =
                        Interlocked.CompareExchange<SetFeatureClassPositionHandler>(
                            ref this.setFeatureClassPositionHandler_0, setFeatureClassPositionHandler1,
                            setFeatureClassPositionHandler);
                } while ((object) setFeatureClassPositionHandler0 != (object) setFeatureClassPositionHandler);
            }
            remove
            {
                SetFeatureClassPositionHandler setFeatureClassPositionHandler;
                SetFeatureClassPositionHandler setFeatureClassPositionHandler0 = this.setFeatureClassPositionHandler_0;
                do
                {
                    setFeatureClassPositionHandler = setFeatureClassPositionHandler0;
                    SetFeatureClassPositionHandler setFeatureClassPositionHandler1 =
                        (SetFeatureClassPositionHandler) Delegate.Remove(setFeatureClassPositionHandler, value);
                    setFeatureClassPositionHandler0 =
                        Interlocked.CompareExchange<SetFeatureClassPositionHandler>(
                            ref this.setFeatureClassPositionHandler_0, setFeatureClassPositionHandler1,
                            setFeatureClassPositionHandler);
                } while ((object) setFeatureClassPositionHandler0 != (object) setFeatureClassPositionHandler);
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

        public event SetHandleFeatureInfoHandler SetHandleFeatureInfoEvent
        {
            add
            {
                SetHandleFeatureInfoHandler setHandleFeatureInfoHandler;
                SetHandleFeatureInfoHandler setHandleFeatureInfoHandler0 = this.setHandleFeatureInfoHandler_0;
                do
                {
                    setHandleFeatureInfoHandler = setHandleFeatureInfoHandler0;
                    SetHandleFeatureInfoHandler setHandleFeatureInfoHandler1 =
                        (SetHandleFeatureInfoHandler) Delegate.Combine(setHandleFeatureInfoHandler, value);
                    setHandleFeatureInfoHandler0 =
                        Interlocked.CompareExchange<SetHandleFeatureInfoHandler>(
                            ref this.setHandleFeatureInfoHandler_0, setHandleFeatureInfoHandler1,
                            setHandleFeatureInfoHandler);
                } while ((object) setHandleFeatureInfoHandler0 != (object) setHandleFeatureInfoHandler);
            }
            remove
            {
                SetHandleFeatureInfoHandler setHandleFeatureInfoHandler;
                SetHandleFeatureInfoHandler setHandleFeatureInfoHandler0 = this.setHandleFeatureInfoHandler_0;
                do
                {
                    setHandleFeatureInfoHandler = setHandleFeatureInfoHandler0;
                    SetHandleFeatureInfoHandler setHandleFeatureInfoHandler1 =
                        (SetHandleFeatureInfoHandler) Delegate.Remove(setHandleFeatureInfoHandler, value);
                    setHandleFeatureInfoHandler0 =
                        Interlocked.CompareExchange<SetHandleFeatureInfoHandler>(
                            ref this.setHandleFeatureInfoHandler_0, setHandleFeatureInfoHandler1,
                            setHandleFeatureInfoHandler);
                } while ((object) setHandleFeatureInfoHandler0 != (object) setHandleFeatureInfoHandler);
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

        public class FieldMap
        {
            private string string_0;

            private string string_1;

            public string SourceName
            {
                get { return this.string_0; }
                set { this.string_0 = value; }
            }

            public string ToName
            {
                get { return this.string_1; }
                set { this.string_1 = value; }
            }

            public FieldMap(string string_2, string string_3)
            {
                this.string_0 = string_2;
                this.string_1 = string_3;
            }
        }
    }
}