using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.ExtendClass
{
    [Guid("15AABFE5-A7FB-45de-AD7D-F6B1EE1BBE3F")]
    public class FractionTextElement : IBoundsProperties, IClone, IElement, IElementProperties, IElementProperties2,
        IGraphicElement, ITransform2D, IFractionTextElement, IPersistStream, IPersist
    {
        private IDisplay m_pCachedDisplay = null;

        private ISelectionTracker m_pSelectionTracker = null;

        private IFillSymbol m_pFillSymbol1;

        private IFillSymbol m_pFillSymbol2;

        private bool m_AutoTransform = false;

        private IEnvelope m_pGeometry = null;

        private string m_sElementName = "FractionTextElement";

        private string m_sElementType = "FractionTextElement";

        private object m_vCustomProperty = null;

        private bool m_bLocked = false;

        private bool m_resetGeometry = false;

        private bool m_FixedAspectRatio = true;

        private IEnvelope m_pTempEnvelop = new Envelope() as IEnvelope;

        private IPointCollection m_pTempPolyline = new Polyline();

        private ITextSymbol m_NumeratorTextSymbol = new TextSymbol();

        private ITextSymbol m_DenominatorTextSymbol = new TextSymbol();

        private ILineSymbol m_LineSymbol = new SimpleLineSymbol();

        private string m_NumeratorText = "AaBbYyZz";

        private string m_DenominatorText = "AaBbYyZz";

        /// <summary>
        /// </summary>
        public bool AutoTransform
        {
            get { return this.m_AutoTransform; }
            set { this.m_AutoTransform = value; }
        }

        /// <summary>
        /// </summary>
        public object CustomProperty
        {
            get { return this.m_vCustomProperty; }
            set { this.m_vCustomProperty = value; }
        }

        public string DenominatorText
        {
            get { return this.m_DenominatorText; }
            set
            {
                this.m_DenominatorText = value;
                this.m_DenominatorTextSymbol.Text = this.m_DenominatorText;
            }
        }

        public ITextSymbol DenominatorTextSymbol
        {
            get { return this.m_DenominatorTextSymbol; }
            set
            {
                this.m_DenominatorTextSymbol = value;
                this.m_DenominatorTextSymbol.Text = this.m_DenominatorText;
                this.m_DenominatorTextSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
                this.m_DenominatorTextSymbol.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
            }
        }

        /// <summary>
        /// </summary>
        public bool FixedAspectRatio
        {
            get { return this.m_FixedAspectRatio; }
            set { this.m_FixedAspectRatio = value; }
        }

        /// <summary>
        /// 获取石否固定尺寸。
        /// </summary>
        public bool FixedSize
        {
            get { return true; }
        }

        /// <summary>
        /// </summary>
        public IGeometry Geometry
        {
            get { return this.m_pGeometry; }
            set
            {
                if (!(value is IPoint))
                {
                    this.m_pGeometry = value.Envelope;
                }
                else
                {
                    this.m_pGeometry.CenterAt(value as IPoint);
                }
                try
                {
                    this.RefreshTracker();
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// </summary>
        public UID ID
        {
            get
            {
                return new UID()
                {
                    Value = "{15AABFE5-A7FB-45de-AD7D-F6B1EE1BBE3F}"
                };
            }
        }

        public ILineSymbol LineSymbol
        {
            get { return this.m_LineSymbol; }
            set { this.m_LineSymbol = value; }
        }

        /// <summary>
        /// </summary>
        public bool Locked
        {
            get { return this.m_bLocked; }
            set { this.m_bLocked = value; }
        }

        /// <summary>
        /// </summary>
        public string Name
        {
            get { return this.m_sElementName; }
            set { this.m_sElementName = value; }
        }

        public string NumeratorText
        {
            get { return this.m_NumeratorText; }
            set
            {
                this.m_NumeratorText = value;
                this.m_NumeratorTextSymbol.Text = this.m_NumeratorText;
            }
        }

        public ITextSymbol NumeratorTextSymbol
        {
            get { return this.m_NumeratorTextSymbol; }
            set
            {
                this.m_NumeratorTextSymbol = value;
                this.m_NumeratorTextSymbol.Text = this.m_NumeratorText;
                this.m_NumeratorTextSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
                this.m_NumeratorTextSymbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
            }
        }

        /// <summary>
        /// </summary>
        internal IPropertySet PropertySet
        {
            get
            {
                IPropertySet propertySetClass = new PropertySet();
                propertySetClass.SetProperty("DenominatorText", this.m_DenominatorText);
                propertySetClass.SetProperty("DenominatorTextSymbol", this.m_DenominatorTextSymbol);
                propertySetClass.SetProperty("LineSymbol", this.m_LineSymbol);
                propertySetClass.SetProperty("NumeratorText", this.m_NumeratorText);
                propertySetClass.SetProperty("NumeratorTextSymbol", this.m_NumeratorTextSymbol);
                propertySetClass.SetProperty("CustomProperty", this.m_vCustomProperty);
                propertySetClass.SetProperty("Locked", this.m_bLocked);
                propertySetClass.SetProperty("Geometry", this.m_pGeometry);
                propertySetClass.SetProperty("ElementName", this.m_sElementName);
                propertySetClass.SetProperty("ElementType", this.m_sElementType);
                return propertySetClass;
            }
            set
            {
                IPropertySet propertySet = value;
                this.m_DenominatorText = propertySet.GetProperty("DenominatorText").ToString();
                this.m_DenominatorTextSymbol = propertySet.GetProperty("DenominatorTextSymbol") as ITextSymbol;
                this.m_LineSymbol = propertySet.GetProperty("LineSymbol") as ILineSymbol;
                this.m_NumeratorText = propertySet.GetProperty("NumeratorText").ToString();
                this.m_NumeratorTextSymbol = propertySet.GetProperty("NumeratorTextSymbol") as ITextSymbol;
                this.m_pGeometry = propertySet.GetProperty("Geometry") as IEnvelope;
                this.m_vCustomProperty = propertySet.GetProperty("CustomProperty");
                this.m_bLocked = (bool) propertySet.GetProperty("Locked");
                this.m_sElementName = propertySet.GetProperty("ElementName").ToString();
                this.m_sElementType = propertySet.GetProperty("ElementType").ToString();
            }
        }

        /// <summary>
        /// </summary>
        public double ReferenceScale
        {
            get { return 0; }
            set { }
        }

        public bool ResetGeometry
        {
            set { this.m_resetGeometry = true; }
        }

        /// <summary>
        /// </summary>
        public ISelectionTracker SelectionTracker
        {
            get { return this.m_pSelectionTracker; }
        }

        /// <summary>
        /// </summary>
        public ISpatialReference SpatialReference
        {
            get { return this.m_pGeometry.SpatialReference; }
            set { this.m_pGeometry.SpatialReference = value; }
        }

        /// <summary>
        /// </summary>
        protected object State
        {
            get
            {
                object obj;
                IPropertySet propertySetClass = new PropertySet();
                propertySetClass.SetProperty("DenominatorText", this.m_DenominatorText);
                propertySetClass.SetProperty("DenominatorTextSymbol", this.m_DenominatorTextSymbol);
                propertySetClass.SetProperty("LineSymbol", this.m_LineSymbol);
                propertySetClass.SetProperty("NumeratorText", this.m_NumeratorText);
                propertySetClass.SetProperty("NumeratorTextSymbol", this.m_NumeratorTextSymbol);
                propertySetClass.SetProperty("CustomProperty", this.m_vCustomProperty);
                propertySetClass.SetProperty("Locked", this.m_bLocked);
                propertySetClass.SetProperty("Geometry", this.m_pGeometry);
                propertySetClass.SetProperty("ElementName", this.m_sElementName);
                propertySetClass.SetProperty("ElementType", this.m_sElementType);
                IMemoryBlobStream memoryBlobStreamClass = new MemoryBlobStream();
                ((IPersistStream) propertySetClass).Save(memoryBlobStreamClass, 1);
                ((IMemoryBlobStreamVariant) memoryBlobStreamClass).ExportToVariant(out obj);
                return obj;
            }
            set
            {
                IPropertySet propertySetClass = new PropertySet();
                IMemoryBlobStream memoryBlobStreamClass = new MemoryBlobStream();
                ((IMemoryBlobStreamVariant) memoryBlobStreamClass).ImportFromVariant(value);
                (propertySetClass as IPersistStream).Load(memoryBlobStreamClass);
                this.m_DenominatorText = propertySetClass.GetProperty("DenominatorText").ToString();
                this.m_DenominatorTextSymbol = propertySetClass.GetProperty("DenominatorTextSymbol") as ITextSymbol;
                this.m_LineSymbol = propertySetClass.GetProperty("LineSymbol") as ILineSymbol;
                this.m_NumeratorText = propertySetClass.GetProperty("NumeratorText").ToString();
                this.m_NumeratorTextSymbol = propertySetClass.GetProperty("NumeratorTextSymbol") as ITextSymbol;
                this.m_pGeometry = propertySetClass.GetProperty("Geometry") as IEnvelope;
                this.m_vCustomProperty = propertySetClass.GetProperty("CustomProperty");
                this.m_bLocked = (bool) propertySetClass.GetProperty("Locked");
                this.m_sElementName = propertySetClass.GetProperty("ElementName").ToString();
                this.m_sElementType = propertySetClass.GetProperty("ElementType").ToString();
            }
        }

        /// <summary>
        /// </summary>
        public string Type
        {
            get { return this.m_sElementType; }
            set { this.m_sElementType = value; }
        }

        public FractionTextElement()
        {
            this.m_pGeometry = new Envelope() as IEnvelope;
            this.m_pGeometry.PutCoords(4, 4, 8.5, 6.4);
            this.m_pSelectionTracker = new PolygonTracker()
            {
                Locked = true,
                ShowHandles = true
            };
            this.m_pFillSymbol1 = new SimpleFillSymbol();
            (this.m_pFillSymbol1 as ISimpleFillSymbol).Style = esriSimpleFillStyle.esriSFSNull;
            this.m_pFillSymbol2 = new SimpleFillSymbol();
            (this.m_pFillSymbol2 as ISimpleFillSymbol).Style = esriSimpleFillStyle.esriSFSBackwardDiagonal;
            this.m_NumeratorTextSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
            this.m_NumeratorTextSymbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
            this.m_DenominatorTextSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
            this.m_DenominatorTextSymbol.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
        }

        /// <summary>
        /// </summary>
        /// <param name="Display"></param>
        public void Activate(IDisplay Display)
        {
            this.m_pCachedDisplay = Display;
            this.m_pSelectionTracker.Display = this.m_pCachedDisplay as IScreenDisplay;
            this.RefreshTracker();
        }

        /// <summary>
        /// </summary>
        /// <param name="src"></param>
        public void Assign(IClone src)
        {
            src = this;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public bool CanRotate()
        {
            return false;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public IClone Clone()
        {
            return new FractionTextElement()
            {
                State = this.State
            };
        }

        /// <summary>
        /// </summary>
        public void Deactivate()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="Display"></param>
        /// <param name="trackCancel"></param>
        public void Draw(IDisplay Display, ITrackCancel trackCancel)
        {
            double num;
            double num1;
            double num2;
            double num3;
            this.m_pGeometry.QueryCoords(out num, out num1, out num2, out num3);
            IPoint pointClass = new ESRI.ArcGIS.Geometry.Point()
            {
                Y = (num3 + num1)/2,
                X = (num + num2)/2
            };
            Display.SetSymbol(null);
            Display.SetSymbol(this.m_NumeratorTextSymbol as ISymbol);
            Display.DrawText(pointClass, this.m_NumeratorText);
            Display.SetSymbol(null);
            Display.SetSymbol(this.m_DenominatorTextSymbol as ISymbol);
            Display.DrawText(pointClass, this.m_DenominatorText);
            IPointCollection polylineClass = new Polyline();
            object value = Missing.Value;
            pointClass.X = num;
            pointClass.Y = (num3 + num1)/2;
            polylineClass.AddPoint(pointClass, ref value, ref value);
            pointClass.X = num2;
            polylineClass.AddPoint(pointClass, ref value, ref value);
            Display.SetSymbol(null);
            Display.SetSymbol(this.m_LineSymbol as ISymbol);
            Display.DrawPolyline(polylineClass as IPolyline);
            Display.SetSymbol(null);
        }

        /// <summary>
        /// </summary>
        /// <param name="pClassID"></param>
        public void GetClassID(out Guid pClassID)
        {
            pClassID = new Guid("15AABFE5-A7FB-45de-AD7D-F6B1EE1BBE3F");
        }

        /// <summary>
        /// </summary>
        /// <param name="pcbSize"></param>
        public void GetSizeMax(out _ULARGE_INTEGER pcbSize)
        {
            (new PropertySet() as IPersistStream).GetSizeMax(out pcbSize);
        }

        /// <summary>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="Tolerance"></param>
        /// <returns></returns>
        public bool HitTest(double x, double y, double Tolerance)
        {
            IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
            pointClass.PutCoords(x, y);
            IRelationalOperator polygonClass = new Polygon() as IRelationalOperator;
            this.QueryOutline(this.m_pCachedDisplay, polygonClass as IPolygon);
            return !polygonClass.Disjoint(pointClass);
        }

        /// <summary>
        /// </summary>
        public void IsDirty()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool IsEqual(IClone other)
        {
            bool flag;
            if (!(other is FractionTextElement))
            {
                flag = false;
            }
            else
            {
                System.Array state = (System.Array) this.State;
                System.Array arrays = (System.Array) (other as FractionTextElement).State;
                if (arrays.Length == state.Length)
                {
                    int num = 0;
                    while (num < state.Length)
                    {
                        if (state.GetValue(num) == arrays.GetValue(num))
                        {
                            num++;
                        }
                        else
                        {
                            flag = false;
                            return flag;
                        }
                    }
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            return flag;
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool IsIdentical(IClone other)
        {
            return (other != this ? false : true);
        }

        /// <summary>
        /// </summary>
        /// <param name="Stream"></param>
        public void Load(IVariantStream Stream)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="pstm"></param>
        public void Load(IStream pstm)
        {
            IPropertySet propertySetClass = new PropertySet();
            (propertySetClass as IPersistStream).Load(pstm);
            this.PropertySet = propertySetClass;
        }

        /// <summary>
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public void Move(double dx, double dy)
        {
            (this.m_pGeometry as ITransform2D).Move(dx, dy);
            this.RefreshTracker();
        }

        /// <summary>
        /// </summary>
        /// <param name="v"></param>
        public void MoveVector(ILine v)
        {
            (this.m_pGeometry as ITransform2D).MoveVector(v);
            this.RefreshTracker();
        }

        /// <summary>
        /// </summary>
        /// <param name="Display"></param>
        /// <param name="Bounds"></param>
        public void QueryBounds(IDisplay Display, IEnvelope Bounds)
        {
            IGeometry polygonClass = new Polygon() as IGeometry;
            this.QueryOutline(this.m_pCachedDisplay, polygonClass as IPolygon);
            polygonClass.QueryEnvelope(Bounds);
            double width = this.m_pGeometry.Width/this.m_pGeometry.Height;
            Bounds.Width = width*Bounds.Height;
        }

        /// <summary>
        /// </summary>
        /// <param name="Display"></param>
        /// <param name="Outline"></param>
        public void QueryOutline(IDisplay Display, IPolygon Outline)
        {
            if (this.m_resetGeometry)
            {
                int mPCachedDisplay = this.m_pCachedDisplay.hDC;
                Graphics graphic = null;
                IntPtr hdc = (IntPtr) 0;
                if (mPCachedDisplay == 0)
                {
                    graphic = Graphics.FromHwnd(new IntPtr((this.m_pCachedDisplay as IScreenDisplay).hWnd));
                    hdc = graphic.GetHdc();
                    mPCachedDisplay = hdc.ToInt32();
                }
                IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
                pointClass.PutCoords((this.m_pGeometry.XMin + this.m_pGeometry.XMax)/2,
                    (this.m_pGeometry.YMax + this.m_pGeometry.YMin)/2);
                (this.m_NumeratorTextSymbol as ISymbol).QueryBoundary(mPCachedDisplay,
                    this.m_pCachedDisplay.DisplayTransformation, pointClass, Outline);
                double width = Outline.Envelope.Width;
                double height = Outline.Envelope.Height;
                (this.m_DenominatorTextSymbol as ISymbol).QueryBoundary(mPCachedDisplay,
                    this.m_pCachedDisplay.DisplayTransformation, pointClass, Outline);
                double num = Outline.Envelope.Width;
                double height1 = Outline.Envelope.Height;
                if (graphic != null)
                {
                    graphic.ReleaseHdc(hdc);
                }
                double num1 = (width > num ? width : num)*1.1;
                double num2 = (height + height1)*1.1;
                this.m_pGeometry = new Envelope() as IEnvelope;
                this.m_pGeometry.PutCoords(pointClass.X - num1/2, pointClass.X + num1/2, pointClass.Y - num2/2,
                    pointClass.Y + num2/2);
                this.m_pGeometry.CenterAt(pointClass);
                this.m_resetGeometry = false;
            }
            IDisplayTransformation displayTransformation = this.m_pCachedDisplay.DisplayTransformation;
            Outline.SetEmpty();
            object value = Missing.Value;
            IPointCollection outline = Outline as IPointCollection;
            outline.AddPoint(this.m_pGeometry.LowerLeft, ref value, ref value);
            outline.AddPoint(this.m_pGeometry.LowerRight, ref value, ref value);
            outline.AddPoint(this.m_pGeometry.UpperRight, ref value, ref value);
            outline.AddPoint(this.m_pGeometry.UpperLeft, ref value, ref value);
            outline.AddPoint(this.m_pGeometry.LowerLeft, ref value, ref value);
        }

        private void RefreshTracker()
        {
            if (this.m_pCachedDisplay != null)
            {
                IGeometry polygonClass = new Polygon() as IGeometry;
                this.QueryOutline(this.m_pCachedDisplay, polygonClass as IPolygon);
                this.m_pSelectionTracker.Geometry = polygonClass;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="Origin"></param>
        /// <param name="rotationAngle"></param>
        public void Rotate(IPoint Origin, double rotationAngle)
        {
            (this.m_pGeometry as ITransform2D).Rotate(Origin, rotationAngle);
            this.RefreshTracker();
        }

        /// <summary>
        /// </summary>
        /// <param name="Stream"></param>
        public void Save(IVariantStream Stream)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="pstm"></param>
        /// <param name="fClearDirty"></param>
        public void Save(IStream pstm, int fClearDirty)
        {
            (this.PropertySet as IPersistStream).Save(pstm, fClearDirty);
        }

        /// <summary>
        /// </summary>
        /// <param name="Origin"></param>
        /// <param name="sx"></param>
        /// <param name="sy"></param>
        public void Scale(IPoint Origin, double sx, double sy)
        {
            (this.m_pGeometry as ITransform2D).Scale(Origin, sx, sy);
            this.RefreshTracker();
        }

        /// <summary>
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="transformation"></param>
        public void Transform(esriTransformDirection direction, ITransformation transformation)
        {
            (this.m_pGeometry as ITransform2D).Transform(direction, transformation);
            this.RefreshTracker();
        }
    }
}