using System;
using System.Collections;
using System.IO;
using System.Threading;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.DXF;

namespace Yutai.ArcGIS.Common.Geodatabase
{
    public class CADConvert : IConvertEvent, ESRI.ArcGIS.Geodatabase.IFeatureProgress_Event, IDataConvert, ICADConvert
    {
        private ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler ifeatureProgress_StepEventHandler_0;

        private SetFeatureClassNameEnventHandler setFeatureClassNameEnventHandler_0;

        private SetFeatureCountEnventHandler setFeatureCountEnventHandler_0;

        private IList ilist_0 = null;

        private string string_0 = "c:\\test.dxf";

        private string string_1 = "R14";

        private SetMaxValueHandler setMaxValueHandler_0;

        private SetMinValueHandler setMinValueHandler_0;

        private SetPositionHandler setPositionHandler_0;

        private SetMessageHandler setMessageHandler_0;

        private FinishHander finishHander_0;

        public string AutoCADVersion
        {
            get { return this.string_1; }
            set { this.string_1 = value; }
        }

        public IList InputFeatureClasses
        {
            set { this.ilist_0 = value; }
        }

        public string OutPath
        {
            set { this.string_0 = value; }
        }

        public CADConvert()
        {
        }

        public bool Export()
        {
            bool flag;
            if (this.ilist_0 == null)
            {
                flag = false;
            }
            else if (this.ilist_0.Count != 0)
            {
                DXFExport dXFExport = new DXFExport();
                if (this.string_1 == "R2000")
                {
                    DXFExport.autoCADVer = DXF.AutoCADVersion.R2000;
                }
                else if (this.string_1 == "R14")
                {
                    DXFExport.autoCADVer = 0;
                }
                float[] singleArray = new float[] {5f, default(float)};
                dXFExport.AddLType("_SOLID", singleArray);
                singleArray = new float[] {5f, -2f};
                dXFExport.AddLType("_DASH", singleArray);
                singleArray = new float[] {2f, -2f};
                dXFExport.AddLType("_DOT", singleArray);
                dXFExport.AddLType("_DASHDOT", new float[] {5f, -2f, 2f, -2f});
                dXFExport.AddLType("_DASHDOTDOT", new float[] {5f, -2f, 2f, -2f, 2f, -2f});
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
                        this.method_1(featureClass, dXFExport);
                    }
                }
                if (File.Exists(this.string_0))
                {
                    File.Delete(this.string_0);
                }
                dXFExport.SaveToFile(this.string_0);
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        private void method_0(IPointCollection ipointCollection_0, DXFExport dxfexport_0)
        {
            DXFData dXFDatum = new DXFData();
            DXFPoint dXFPoint = new DXFPoint();
            dXFDatum.points.Add(new ArrayList());
            dXFDatum.count = ipointCollection_0.PointCount;
            for (int i = 0; i < ipointCollection_0.PointCount; i++)
            {
                IPoint point = ipointCollection_0.Point[i];
                ((ArrayList) dXFDatum.points[0]).Add(new DXFPoint((float) point.X, (float) point.Y, 0f));
            }
            dxfexport_0.AddPolyLine(dXFDatum, 0);
        }

        private void method_1(IFeatureClass ifeatureClass_0, DXFExport dxfexport_0)
        {
            DXFData dXFDatum;
            int j;
            IPoint point;
            dxfexport_0.CurrentLayer = new DXFLayer(ifeatureClass_0.AliasName);
            IFeatureCursor featureCursor = ifeatureClass_0.Search(null, false);
            for (IFeature i = featureCursor.NextFeature(); i != null; i = featureCursor.NextFeature())
            {
                if (this.ifeatureProgress_StepEventHandler_0 != null)
                {
                    this.ifeatureProgress_StepEventHandler_0();
                }
                if (ifeatureClass_0.FeatureType == esriFeatureType.esriFTAnnotation)
                {
                    ITextElement annotation = (i as IAnnotationFeature2).Annotation as ITextElement;
                    if (annotation != null)
                    {
                        double xMin = ((annotation as IElement).Geometry.Envelope.XMin +
                                       (annotation as IElement).Geometry.Envelope.XMax)/2;
                        double yMin = ((annotation as IElement).Geometry.Envelope.YMin +
                                       (annotation as IElement).Geometry.Envelope.YMax)/2;
                        dXFDatum = new DXFData();
                        dXFDatum.Clear();
                        dXFDatum.color = annotation.Symbol.Color.RGB;
                        dXFDatum.height = (float) (annotation as IElement).Geometry.Envelope.Height;
                        dXFDatum.rotation = (float) annotation.Symbol.Angle;
                        dXFDatum.text = annotation.Text;
                        dXFDatum.point.X = (float) xMin;
                        dXFDatum.point.Y = (float) yMin;
                        dxfexport_0.AddText(dXFDatum);
                    }
                }
                else if (
                    !(ifeatureClass_0.ShapeType == esriGeometryType.esriGeometryPolyline
                        ? false
                        : ifeatureClass_0.ShapeType != esriGeometryType.esriGeometryPolygon))
                {
                    IGeometryCollection shape = i.Shape as IGeometryCollection;
                    if (shape != null)
                    {
                        if (shape.GeometryCount <= 1)
                        {
                            this.method_0(shape as IPointCollection, dxfexport_0);
                        }
                        else
                        {
                            for (j = 0; j < shape.GeometryCount; j++)
                            {
                                this.method_0(shape.Geometry[j] as IPointCollection, dxfexport_0);
                            }
                        }
                    }
                }
                else if (ifeatureClass_0.ShapeType != esriGeometryType.esriGeometryPoint)
                {
                    IPointCollection pointCollection = i.Shape as IPointCollection;
                    if (pointCollection != null)
                    {
                        for (j = 0; j < pointCollection.PointCount; j++)
                        {
                            point = pointCollection.Point[j];
                            dXFDatum = new DXFData();
                            dXFDatum.Clear();
                            dXFDatum.point.X = (float) point.X;
                            dXFDatum.point.Y = (float) point.Y;
                            dxfexport_0.AddPixel(dXFDatum);
                        }
                    }
                }
                else
                {
                    point = i.Shape as IPoint;
                    if (point != null)
                    {
                        dXFDatum = new DXFData();
                        dXFDatum.Clear();
                        dXFDatum.point.X = (float) point.X;
                        dXFDatum.point.Y = (float) point.Y;
                        dxfexport_0.AddPixel(dXFDatum);
                    }
                }
            }
            ComReleaser.ReleaseCOMObject(featureCursor);
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