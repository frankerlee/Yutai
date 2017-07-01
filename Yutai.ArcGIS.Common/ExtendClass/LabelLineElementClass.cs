using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.ExtendClass
{
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("A62DC65E-5631-41a5-8A21-8C6166F6D557")]
    [ProgId("JLK.ExtendClass.LabelLineElementClass")]
    public sealed class LabelLineElementClass : ILineElement, IElement, IElementProperties, IElementProperties2,
        IElementProperties3, IBoundsProperties, ITransform2D, IGraphicElement, IPersistVariant, IClone,
        IDocumentVersionSupportGEN
    {
        private const double c_Cosine30 = 0.866025403784439;

        private const double c_Deg2Rad = 0.0174532925199433;

        private const double c_Rad2Deg = 57.2957795130823;

        private const int c_Version = 2;

        public const string CLASSGUID = "A62DC65E-5631-41a5-8A21-8C6166F6D557";

        public const int LOGPIXELSX = 88;

        public const int LOGPIXELSY = 90;

        private IPolyline m_triangle = null;

        private IPoint m_pointGeometry = null;

        private ILineSymbol m_fillSymbol = null;

        private double m_rotation = 0;

        private double m_size = 20;

        private ISelectionTracker m_selectionTracker = null;

        private IDisplay m_cachedDisplay = null;

        private ISpatialReference m_nativeSR = null;

        private string m_elementName = string.Empty;

        private string m_elementType = "LabelLineElement";

        private object m_customProperty = null;

        private bool m_autoTrans = true;

        private double m_scaleRef = 0;

        private esriAnchorPointEnum m_anchorPointType = esriAnchorPointEnum.esriCenterPoint;

        private double m_dDeviceRatio = 0;

        public esriAnchorPointEnum AnchorPoint
        {
            get { return this.m_anchorPointType; }
            set { this.m_anchorPointType = value; }
        }

        /// <summary>
        /// Indicates if transform is applied to symbols and other parts of element.
        /// False = only apply transform to geometry.
        /// Update font size in ITransform2D routines
        /// </summary>
        public bool AutoTransform
        {
            get { return this.m_autoTrans; }
            set { this.m_autoTrans = value; }
        }

        public object CustomProperty
        {
            get { return this.m_customProperty; }
            set { this.m_customProperty = value; }
        }

        public bool FixedAspectRatio
        {
            get { return true; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public bool FixedSize
        {
            get { return true; }
        }

        public IGeometry Geometry
        {
            get { return this.Clone(this.m_triangle) as IGeometry; }
            set
            {
                try
                {
                    this.m_triangle = this.Clone(value) as IPolyline;
                    this.UpdateElementSpatialRef();
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.Message);
                }
            }
        }

        public UID ID
        {
            get
            {
                return new UID()
                {
                    Value = "{A62DC65E-5631-41a5-8A21-8C6166F6D557}"
                };
            }
        }

        public bool Locked
        {
            get { return false; }
            set { }
        }

        public string Name
        {
            get { return this.m_elementName; }
            set { this.m_elementName = value; }
        }

        public double ReferenceScale
        {
            get { return this.m_scaleRef; }
            set { this.m_scaleRef = value; }
        }

        public ISelectionTracker SelectionTracker
        {
            get { return this.m_selectionTracker; }
        }

        public ISpatialReference SpatialReference
        {
            get { return this.m_nativeSR; }
            set
            {
                this.m_nativeSR = value;
                this.UpdateElementSpatialRef();
            }
        }

        public ILineSymbol Symbol
        {
            get { return this.m_fillSymbol; }
            set { this.m_fillSymbol = value; }
        }

        public string Type
        {
            get { return this.m_elementType; }
            set { this.m_elementType = value; }
        }

        public LabelLineElementClass()
        {
            this.m_triangle = new Polyline() as IPolyline;
            this.m_triangle.SetEmpty();
            this.InitMembers();
        }

        public void Activate(IDisplay Display)
        {
            this.m_cachedDisplay = Display;
            this.SetupDeviceRatio(Display.hDC, Display);
            this.RefreshTracker();
        }

        public void Assign(IClone src)
        {
            if (null == src)
            {
                throw new COMException("Invalid objact.");
            }
            if (!(src is LabelLineElementClass))
            {
                throw new COMException("Bad object type.");
            }
            LabelLineElementClass labelLineElementClass = (LabelLineElementClass) src;
            this.m_elementName = labelLineElementClass.Name;
            this.m_elementType = labelLineElementClass.Type;
            this.m_autoTrans = labelLineElementClass.AutoTransform;
            this.m_scaleRef = labelLineElementClass.ReferenceScale;
            this.m_anchorPointType = labelLineElementClass.AnchorPoint;
            IObjectCopy objectCopyClass = new ObjectCopy();
            if (null != labelLineElementClass.CustomProperty)
            {
                if (labelLineElementClass.CustomProperty is IClone)
                {
                    this.m_customProperty = ((IClone) labelLineElementClass.CustomProperty).Clone();
                }
                else if (labelLineElementClass.CustomProperty is IPersistStream)
                {
                    this.m_customProperty = objectCopyClass.Copy(labelLineElementClass.CustomProperty);
                }
                else if (labelLineElementClass.CustomProperty.GetType().IsSerializable)
                {
                    MemoryStream memoryStream = new MemoryStream();
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(memoryStream, labelLineElementClass.CustomProperty);
                    memoryStream = new MemoryStream(memoryStream.ToArray());
                    this.m_customProperty = binaryFormatter.Deserialize(memoryStream);
                }
            }
            if (null == labelLineElementClass.SpatialReference)
            {
                this.m_nativeSR = null;
            }
            else
            {
                this.m_nativeSR = objectCopyClass.Copy(labelLineElementClass.SpatialReference) as ISpatialReference;
            }
            if (null == labelLineElementClass.Symbol)
            {
                this.m_fillSymbol = null;
            }
            else
            {
                this.m_fillSymbol = (labelLineElementClass.Symbol as IClone).Clone() as ILineSymbol;
            }
            if (null == labelLineElementClass.Geometry)
            {
                this.m_triangle = null;
                this.m_pointGeometry = null;
            }
            else
            {
                this.m_triangle = objectCopyClass.Copy(labelLineElementClass.Geometry) as IPolyline;
            }
        }

        public bool CanRotate()
        {
            return true;
        }

        public IClone Clone()
        {
            LabelLineElementClass labelLineElementClass = new LabelLineElementClass();
            try
            {
                labelLineElementClass.Assign(this);
            }
            catch (Exception exception)
            {
            }
            return labelLineElementClass;
        }

        private IClone Clone(object obj)
        {
            IClone clone;
            if ((obj == null ? false : obj is IClone))
            {
                clone = ((IClone) obj).Clone();
            }
            else
            {
                clone = null;
            }
            return clone;
        }

        public object ConvertToSupportedObject(esriArcGISVersion docVersion)
        {
            ICharacterMarkerSymbol characterMarkerSymbol = new CharacterMarkerSymbol();
            characterMarkerSymbol.Color = this.m_fillSymbol.Color;
            characterMarkerSymbol.Angle = this.m_rotation;
            characterMarkerSymbol.Size = this.m_size;
            // characterMarkerSymbol.Font = Converter.ToStdFont(new System.Drawing.Font("ESRI Default Marker", (float)this.m_size, System.Drawing.FontStyle.Regular));
            characterMarkerSymbol.CharacterIndex = 184;
            IMarkerElement markerElement = new MarkerElement() as IMarkerElement;
            markerElement.Symbol = characterMarkerSymbol;
            IPoint geometry = ((IClone) this.m_pointGeometry).Clone() as IPoint;
            IElement element = (IElement) markerElement;
            element.Geometry = geometry;
            return element;
        }

        public void Deactivate()
        {
            this.m_cachedDisplay = null;
        }

        public void Draw(IDisplay Display, ITrackCancel TrackCancel)
        {
            if ((this.m_triangle == null ? false : null != this.m_fillSymbol))
            {
                Display.SetSymbol((ISymbol) this.m_fillSymbol);
                Display.DrawPolyline(this.m_triangle);
            }
        }

        [DllImport("gdi32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern int GetDeviceCaps(int hDC, int nIndex);

        public bool HitTest(double x, double y, double Tolerance)
        {
            bool flag;
            if (null != this.m_cachedDisplay)
            {
                IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
                pointClass.PutCoords(x, y);
                flag = ((IRelationalOperator) this.m_triangle).Contains(pointClass);
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        private void InitMembers()
        {
            this.m_selectionTracker = new PolygonTracker()
            {
                Locked = false,
                ShowHandles = true
            };
        }

        public bool IsEqual(IClone other)
        {
            if (null == other)
            {
                throw new COMException("Invalid objact.");
            }
            if (!(other is LabelLineElementClass))
            {
                throw new COMException("Bad object type.");
            }
            LabelLineElementClass labelLineElementClass = (LabelLineElementClass) other;
            return ((!(labelLineElementClass.Name == this.m_elementName) ||
                     !(labelLineElementClass.Type == this.m_elementType) ||
                     labelLineElementClass.AutoTransform != this.m_autoTrans ||
                     labelLineElementClass.ReferenceScale != this.m_scaleRef ||
                     labelLineElementClass.AnchorPoint != this.m_anchorPointType ||
                     !((IClone) labelLineElementClass.Geometry).IsEqual((IClone) this.m_triangle) ||
                     !((IClone) labelLineElementClass.Symbol).IsEqual((IClone) this.m_fillSymbol)
                ? true
                : !((IClone) labelLineElementClass.SpatialReference).IsEqual((IClone) this.m_nativeSR))
                ? false
                : true);
        }

        public bool IsIdentical(IClone other)
        {
            if (null == other)
            {
                throw new COMException("Invalid objact.");
            }
            if (!(other is LabelLineElementClass))
            {
                throw new COMException("Bad object type.");
            }
            return ((LabelLineElementClass) other != this ? false : true);
        }

        public bool IsSupportedAtVersion(esriArcGISVersion docVersion)
        {
            return (esriArcGISVersion.esriArcGISVersion83 != docVersion ? true : false);
        }

        public void Load(IVariantStream Stream)
        {
            int num = (int) Stream.Read();
            if ((num > 2 ? true : num <= 0))
            {
                throw new Exception("Wrong version!");
            }
            this.InitMembers();
            this.m_size = (double) Stream.Read();
            this.m_scaleRef = (double) Stream.Read();
            this.m_anchorPointType = (esriAnchorPointEnum) Stream.Read();
            this.m_autoTrans = (bool) Stream.Read();
            this.m_elementType = (string) Stream.Read();
            this.m_elementName = (string) Stream.Read();
            this.m_nativeSR = Stream.Read() as ISpatialReference;
            this.m_fillSymbol = Stream.Read() as ILineSymbol;
            this.m_triangle = Stream.Read() as IPolyline;
            if (num == 2)
            {
                this.m_rotation = (double) Stream.Read();
            }
        }

        public void Move(double dx, double dy)
        {
            if (null != this.m_triangle)
            {
                ((ITransform2D) this.m_triangle).Move(dx, dy);
                this.RefreshTracker();
            }
        }

        public void MoveVector(ILine v)
        {
            if (null != this.m_triangle)
            {
                ((ITransform2D) this.m_triangle).MoveVector(v);
                this.RefreshTracker();
            }
        }

        private double PointsToMap(IDisplayTransformation displayTransform, double dPointSize)
        {
            double num = 0;
            num = (displayTransform != null ? displayTransform.FromPoints(dPointSize) : dPointSize*this.m_dDeviceRatio);
            return num;
        }

        public void QueryBounds(IDisplay Display, IEnvelope Bounds)
        {
            IPolygon polygonClass = new Polygon() as IPolygon;
            polygonClass.SetEmpty();
            ((ISymbol) this.m_fillSymbol).QueryBoundary(Display.hDC, Display.DisplayTransformation, this.m_triangle,
                polygonClass);
            Bounds.XMin = polygonClass.Envelope.XMin;
            Bounds.XMax = polygonClass.Envelope.XMax;
            Bounds.YMin = polygonClass.Envelope.YMin;
            Bounds.YMax = polygonClass.Envelope.YMax;
            Bounds.SpatialReference = polygonClass.Envelope.SpatialReference;
        }

        public void QueryOutline(IDisplay Display, IPolygon Outline)
        {
            IPolygon polygonClass = new Polygon() as IPolygon;
            polygonClass.SetEmpty();
            ((ISymbol) this.m_fillSymbol).QueryBoundary(Display.hDC, Display.DisplayTransformation, this.m_triangle,
                polygonClass);
            ((IPointCollection) Outline).AddPointCollection((IPointCollection) polygonClass);
        }

        /// <summary>
        /// assign the triangle's geometry to the selection tracker
        /// </summary>
        private void RefreshTracker()
        {
            if (null != this.m_cachedDisplay)
            {
                this.m_selectionTracker.Display = (IScreenDisplay) this.m_cachedDisplay;
                IPolygon polygonClass = new Polygon() as IPolygon;
                this.QueryOutline(this.m_cachedDisplay, polygonClass);
                this.m_selectionTracker.Geometry = polygonClass;
            }
        }

        public void Rotate(IPoint Origin, double rotationAngle)
        {
            if (null != this.m_triangle)
            {
                ((ITransform2D) this.m_triangle).Rotate(Origin, rotationAngle);
                this.m_rotation = rotationAngle*57.2957795130823;
                this.RefreshTracker();
            }
        }

        public void Save(IVariantStream Stream)
        {
            Stream.Write(2);
            Stream.Write(this.m_size);
            Stream.Write(this.m_scaleRef);
            Stream.Write(this.m_anchorPointType);
            Stream.Write(this.m_autoTrans);
            Stream.Write(this.m_elementType);
            Stream.Write(this.m_elementName);
            Stream.Write(this.m_nativeSR);
            Stream.Write(this.m_fillSymbol);
            Stream.Write(this.m_triangle);
            Stream.Write(this.m_rotation);
        }

        public void Scale(IPoint Origin, double sx, double sy)
        {
            if (null != this.m_triangle)
            {
                ((ITransform2D) this.m_triangle).Scale(Origin, sx, sy);
                if (this.m_autoTrans)
                {
                    LabelLineElementClass mSize = this;
                    mSize.m_size = mSize.m_size*Math.Max(sx, sy);
                }
                this.RefreshTracker();
            }
        }

        private void SetupDeviceRatio(int hDC, IDisplay display)
        {
            if (display.DisplayTransformation != null)
            {
                if (display.DisplayTransformation.Resolution != 0)
                {
                    this.m_dDeviceRatio = display.DisplayTransformation.Resolution/72;
                    if (display.DisplayTransformation.ReferenceScale != 0)
                    {
                        this.m_dDeviceRatio = this.m_dDeviceRatio*display.DisplayTransformation.ReferenceScale/
                                              display.DisplayTransformation.ScaleRatio;
                    }
                }
            }
            else if (display.hDC == 0)
            {
                this.m_dDeviceRatio = (double) (1/(this.TwipsPerPixelX()/20));
            }
            else
            {
                this.m_dDeviceRatio = Convert.ToDouble(LabelLineElementClass.GetDeviceCaps(hDC, 88))/72;
            }
        }

        public void Transform(esriTransformDirection direction, ITransformation transformation)
        {
            if (null != this.m_triangle)
            {
                ((ITransform2D) this.m_triangle).Transform(direction, transformation);
                IAffineTransformation2D affineTransformation2D = (IAffineTransformation2D) transformation;
                if (affineTransformation2D.YScale != 1)
                {
                    LabelLineElementClass mSize = this;
                    mSize.m_size = mSize.m_size*Math.Max(affineTransformation2D.YScale, affineTransformation2D.XScale);
                }
                this.RefreshTracker();
            }
        }

        private int TwipsPerPixelX()
        {
            return 16;
        }

        private int TwipsPerPixelY()
        {
            return 16;
        }

        private void UpdateElementSpatialRef()
        {
            if ((this.m_cachedDisplay == null || this.m_nativeSR == null || this.m_triangle == null
                ? false
                : null != this.m_cachedDisplay.DisplayTransformation.SpatialReference))
            {
                if (null == this.m_triangle.SpatialReference)
                {
                    this.m_triangle.SpatialReference = this.m_cachedDisplay.DisplayTransformation.SpatialReference;
                }
                this.m_triangle.Project(this.m_nativeSR);
                this.RefreshTracker();
            }
        }
    }
}