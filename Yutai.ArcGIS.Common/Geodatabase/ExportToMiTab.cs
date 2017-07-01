using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Geodatabase
{
    public class ExportToMiTab : IConvertEvent, ESRI.ArcGIS.Geodatabase.IFeatureProgress_Event, IDataConvert,
        ITABConvert
    {
        private IList ilist_0 = null;

        private string string_0 = "c:\\";

        private IGeometry igeometry_0 = null;

        private bool bool_0 = false;

        private ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler ifeatureProgress_StepEventHandler_0;

        private string string_1 = "tab";

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

        public IList InputFeatureClasses
        {
            set { this.ilist_0 = value; }
        }

        public bool IsClip
        {
            set { this.bool_0 = value; }
        }

        public string OutPath
        {
            set { this.string_0 = value; }
        }

        public string Type
        {
            set { this.string_1 = value; }
        }

        public ExportToMiTab()
        {
        }

        public bool Export()
        {
            return this.method_0();
        }

        private bool method_0()
        {
            bool flag;
            if (this.ilist_0.Count != 0)
            {
                for (int i = 0; i < this.ilist_0.Count; i++)
                {
                    IFeatureClass featureClass = null;
                    object item = this.ilist_0[i];
                    if (item is IFeatureClass)
                    {
                        featureClass = item as IFeatureClass;
                    }
                    else if (item is IFeatureClassName)
                    {
                        featureClass = (item as IName).Open() as IFeatureClass;
                    }
                    if (featureClass != null)
                    {
                        if (this.setFeatureClassNameEnventHandler_0 != null)
                        {
                            this.setFeatureClassNameEnventHandler_0(featureClass.AliasName);
                        }
                        if (this.setFeatureCountEnventHandler_0 != null)
                        {
                            this.setFeatureCountEnventHandler_0(featureClass.FeatureCount(null));
                        }
                        this.method_1(featureClass);
                    }
                }
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        private void method_1(IFeatureClass ifeatureClass_0)
        {
            int i;
            IField field;
            double[] x;
            double[] y;
            IPoint shape;
            string aliasName = ifeatureClass_0.AliasName;
            if (aliasName.IndexOf(".") != -1)
            {
                aliasName = aliasName.Replace(".", "_");
            }
            aliasName = string.Concat(this.string_0, "\\", aliasName);
            if (File.Exists(string.Concat(aliasName, ".tab")))
            {
                File.Delete(string.Concat(aliasName, ".tab"));
            }
            if (File.Exists(string.Concat(aliasName, ".map")))
            {
                File.Delete(string.Concat(aliasName, ".map"));
            }
            if (File.Exists(string.Concat(aliasName, ".dat")))
            {
                File.Delete(string.Concat(aliasName, ".dat"));
            }
            if (File.Exists(string.Concat(aliasName, ".id")))
            {
                File.Delete(string.Concat(aliasName, ".id"));
            }
            string str = string.Concat(aliasName, ".tab");
            IEnvelope extent = (ifeatureClass_0 as IGeoDataset).Extent;
            IntPtr intPtr = TabWrite._mitab_c_create(str, "tab", "", extent.YMax, extent.YMin, extent.XMax, extent.XMin);
            IFields fields = ifeatureClass_0.Fields;
            IList arrayLists = new ArrayList();
            for (i = 0; i < fields.FieldCount; i++)
            {
                field = fields.Field[i];
                string str1 = field.Name.Replace('.', '\u005F');
                if (field.Type == esriFieldType.esriFieldTypeInteger)
                {
                    TabWrite._mitab_c_add_field(intPtr, str1, 2, 4, 0, 0, 0);
                    arrayLists.Add(field.Name);
                }
                else if (field.Type == esriFieldType.esriFieldTypeSmallInteger)
                {
                    TabWrite._mitab_c_add_field(intPtr, str1, 3, 2, 0, 0, 0);
                    arrayLists.Add(field.Name);
                }
                else if (field.Type == esriFieldType.esriFieldTypeSingle)
                {
                    TabWrite._mitab_c_add_field(intPtr, str1, 5, field.Length, field.Precision, 0, 0);
                    arrayLists.Add(field.Name);
                }
                else if (field.Type == esriFieldType.esriFieldTypeDouble)
                {
                    TabWrite._mitab_c_add_field(intPtr, str1, 4, field.Length, field.Precision, 0, 0);
                    arrayLists.Add(field.Name);
                }
                else if (field.Type == esriFieldType.esriFieldTypeString)
                {
                    TabWrite._mitab_c_add_field(intPtr, str1, 1, (field.Length > 254 ? 254 : field.Length), 0, 0, 0);
                    arrayLists.Add(field.Name);
                }
                else if (field.Type == esriFieldType.esriFieldTypeDate)
                {
                    TabWrite._mitab_c_add_field(intPtr, str1, 6, field.Length, field.Precision, 0, 0);
                    arrayLists.Add(field.Name);
                }
            }
            int num = 0;
            if (ifeatureClass_0.FeatureType == esriFeatureType.esriFTAnnotation)
            {
                num = 4;
            }
            else if (ifeatureClass_0.ShapeType == esriGeometryType.esriGeometryPoint)
            {
                num = 1;
            }
            else if (ifeatureClass_0.ShapeType == esriGeometryType.esriGeometryMultipoint)
            {
                num = 10;
            }
            else if (ifeatureClass_0.ShapeType == esriGeometryType.esriGeometryPolyline)
            {
                num = 5;
            }
            else if (ifeatureClass_0.ShapeType == esriGeometryType.esriGeometryPolygon)
            {
                num = 7;
            }
            ISpatialFilter spatialFilterClass = null;
            if (this.igeometry_0 != null)
            {
                spatialFilterClass = new SpatialFilter()
                {
                    GeometryField = ifeatureClass_0.ShapeFieldName,
                    Geometry = this.igeometry_0
                };
                if (ifeatureClass_0.ShapeType != esriGeometryType.esriGeometryPoint)
                {
                    spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                }
                else
                {
                    spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                }
            }
            IFeatureCursor featureCursor = ifeatureClass_0.Search(spatialFilterClass, false);
            for (IFeature j = featureCursor.NextFeature(); j != null; j = featureCursor.NextFeature())
            {
                if (this.ifeatureProgress_StepEventHandler_0 != null)
                {
                    this.ifeatureProgress_StepEventHandler_0();
                }
                if (j.Shape != null)
                {
                    IntPtr intPtr1 = TabWrite._mitab_c_create_feature(intPtr, num);
                    for (i = 0; i < fields.FieldCount; i++)
                    {
                        field = fields.Field[i];
                        int num1 = arrayLists.IndexOf(field.Name);
                        if (num1 != -1)
                        {
                            object value = j.Value[i];
                            if (value is DBNull)
                            {
                                TabWrite._mitab_c_set_field(intPtr1, num1, "");
                            }
                            else if (field.Type != esriFieldType.esriFieldTypeString)
                            {
                                TabWrite._mitab_c_set_field(intPtr1, num1, value.ToString());
                            }
                            else
                            {
                                string str2 = value.ToString();
                                if (str2.Length > 254)
                                {
                                    str2 = str2.Substring(0, 254);
                                }
                                TabWrite._mitab_c_set_field(intPtr1, num1, str2);
                            }
                        }
                    }
                    if (num == 4)
                    {
                        ITextElement annotation = (j as IAnnotationFeature2).Annotation as ITextElement;
                        if (annotation != null)
                        {
                            double xMin = ((annotation as IElement).Geometry.Envelope.XMin +
                                           (annotation as IElement).Geometry.Envelope.XMax)/2;
                            double yMin = ((annotation as IElement).Geometry.Envelope.YMin +
                                           (annotation as IElement).Geometry.Envelope.YMax)/2;
                            double[] numArray = new double[] {xMin};
                            double[] numArray1 = numArray;
                            numArray = new double[] {yMin};
                            TabWrite._mitab_c_set_points(intPtr1, 0, 1, numArray1, numArray);
                            TabWrite._mitab_c_set_font(intPtr1, annotation.Symbol.Font.Name);
                            TabWrite._mitab_c_set_text(intPtr1, annotation.Text);
                            TabWrite._mitab_c_set_text_display(intPtr1, annotation.Symbol.Angle, annotation.Symbol.Size,
                                annotation.Symbol.Size, annotation.Symbol.Color.RGB, 16777215, -1, -1, -1);
                        }
                    }
                    else if (ifeatureClass_0.ShapeType == esriGeometryType.esriGeometryPoint)
                    {
                        x = new double[1];
                        y = new double[1];
                        shape = j.Shape as IPoint;
                        x[0] = shape.X;
                        y[0] = shape.Y;
                        TabWrite._mitab_c_set_points(intPtr1, 0, 1, x, y);
                    }
                    else if (ifeatureClass_0.ShapeType != esriGeometryType.esriGeometryMultipoint)
                    {
                        IGeometryCollection geometryCollection = this.method_2(j.Shape) as IGeometryCollection;
                        geometryCollection = geometryCollection as IPolycurve as IGeometryCollection;
                        for (i = 0; i < geometryCollection.GeometryCount; i++)
                        {
                            List<double> nums = new List<double>();
                            List<double> nums1 = new List<double>();
                            ISegmentCollection geometry = geometryCollection.Geometry[i] as ISegmentCollection;
                            for (int k = 0; k < geometry.SegmentCount; k++)
                            {
                                ISegment segment = geometry.Segment[k];
                                if (segment is ILine)
                                {
                                    if (k == 0)
                                    {
                                        nums.Add(segment.FromPoint.X);
                                        nums1.Add(segment.FromPoint.Y);
                                    }
                                    nums.Add(segment.ToPoint.X);
                                    nums1.Add(segment.ToPoint.Y);
                                }
                                else if (segment != null)
                                {
                                    IPolyline polylineClass = new Polyline() as IPolyline;
                                    object obj = Missing.Value;
                                    try
                                    {
                                        (polylineClass as ISegmentCollection).AddSegment(
                                            (segment as IClone).Clone() as ISegment, ref obj, ref obj);
                                        polylineClass.Densify(0, 0);
                                    }
                                    catch (Exception exception)
                                    {
                                    }
                                    for (int l = (k == 0 ? 0 : 1);
                                        l < (polylineClass as IPointCollection).PointCount;
                                        l++)
                                    {
                                        IPoint point = (polylineClass as IPointCollection).Point[l];
                                        nums.Add(point.X);
                                        nums1.Add(point.Y);
                                    }
                                }
                            }
                            x = new double[nums.Count];
                            y = new double[nums.Count];
                            nums.CopyTo(x);
                            nums1.CopyTo(y);
                            TabWrite._mitab_c_set_points(intPtr1, i, nums.Count, x, y);
                        }
                    }
                    else
                    {
                        IPointCollection pointCollection = j.Shape as IPointCollection;
                        x = new double[pointCollection.PointCount];
                        y = new double[pointCollection.PointCount];
                        for (int m = 0; m < pointCollection.PointCount; m++)
                        {
                            shape = pointCollection.Point[m];
                            x[m] = shape.X;
                            y[m] = shape.Y;
                        }
                        TabWrite._mitab_c_set_points(intPtr1, 0, pointCollection.PointCount, x, y);
                    }
                    TabWrite._mitab_c_write_feature(intPtr, intPtr1);
                }
            }
            ComReleaser.ReleaseCOMObject(featureCursor);
            TabWrite._mitab_c_close(intPtr);
        }

        private IGeometry method_2(IGeometry igeometry_1)
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