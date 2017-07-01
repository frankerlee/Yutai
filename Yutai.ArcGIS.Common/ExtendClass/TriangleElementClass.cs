using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.ExtendClass
{
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("57A47BEA-0830-48b2-8E20-8C872149F275")]
    [ProgId("JLK.ExtendClass.TriangleElementClass")]
    public sealed class TriangleElementClass : ITriangleElement, IElement, IElementProperties, IElementProperties2,
        IElementProperties3, IBoundsProperties, ITransform2D, IGraphicElement, IPersistVariant, IClone,
        IDocumentVersionSupportGEN
    {
        private const double c_Cosine30 = 0.866025403784439;

        private const double c_Deg2Rad = 0.0174532925199433;

        private const double c_Rad2Deg = 57.2957795130823;

        private const int c_Version = 2;

        public const string CLASSGUID = "57A47BEA-0830-48b2-8E20-8C872149F275";

        public const int LOGPIXELSX = 88;

        public const int LOGPIXELSY = 90;

        private IPolygon m_triangle = null;

        private IPoint m_pointGeometry = null;

        private ISimpleFillSymbol m_fillSymbol = null;

        private double m_rotation = 0;

        private double m_size = 20;

        private ISelectionTracker m_selectionTracker = null;

        private IDisplay m_cachedDisplay = null;

        private ISpatialReference m_nativeSR = null;

        private string m_elementName = string.Empty;

        private string m_elementType = "TriangleElement";

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

        public double Angle
        {
            get { return this.m_rotation; }
            set { this.m_rotation = value; }
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

        public ISimpleFillSymbol FillSymbol
        {
            get { return this.m_fillSymbol; }
            set { this.m_fillSymbol = value; }
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
            get { return this.Clone(this.m_pointGeometry) as IGeometry; }
            set
            {
                try
                {
                    this.m_pointGeometry = this.Clone(value) as IPoint;
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
                    Value = "{57A47BEA-0830-48b2-8E20-8C872149F275}"
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

        public double Size
        {
            get { return this.m_size; }
            set { this.m_size = value; }
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

        public string Type
        {
            get { return this.m_elementType; }
            set { this.m_elementType = value; }
        }

        public TriangleElementClass()
        {
            this.m_triangle = new Polygon() as IPolygon;
            this.m_triangle.SetEmpty();
            this.InitMembers();
        }

        public void Activate(IDisplay Display)
        {
            this.m_cachedDisplay = Display;
            this.SetupDeviceRatio(Display.hDC, Display);
            if (this.m_triangle.IsEmpty)
            {
                this.BuildTriangleGeometry(this.m_pointGeometry);
            }
            this.RefreshTracker();
        }

        public void Assign(IClone src)
        {
            if (null == src)
            {
                throw new COMException("Invalid objact.");
            }
            if (!(src is TriangleElementClass))
            {
                throw new COMException("Bad object type.");
            }
            TriangleElementClass triangleElementClass = (TriangleElementClass) src;
            this.m_elementName = triangleElementClass.Name;
            this.m_elementType = triangleElementClass.Type;
            this.m_autoTrans = triangleElementClass.AutoTransform;
            this.m_scaleRef = triangleElementClass.ReferenceScale;
            this.m_rotation = triangleElementClass.Angle;
            this.m_size = triangleElementClass.Size;
            this.m_anchorPointType = triangleElementClass.AnchorPoint;
            IObjectCopy objectCopyClass = new ObjectCopy();
            if (null != triangleElementClass.CustomProperty)
            {
                if (triangleElementClass.CustomProperty is IClone)
                {
                    this.m_customProperty = ((IClone) triangleElementClass.CustomProperty).Clone();
                }
                else if (triangleElementClass.CustomProperty is IPersistStream)
                {
                    this.m_customProperty = objectCopyClass.Copy(triangleElementClass.CustomProperty);
                }
                else if (triangleElementClass.CustomProperty.GetType().IsSerializable)
                {
                    MemoryStream memoryStream = new MemoryStream();
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(memoryStream, triangleElementClass.CustomProperty);
                    memoryStream = new MemoryStream(memoryStream.ToArray());
                    this.m_customProperty = binaryFormatter.Deserialize(memoryStream);
                }
            }
            if (null == triangleElementClass.SpatialReference)
            {
                this.m_nativeSR = null;
            }
            else
            {
                this.m_nativeSR = objectCopyClass.Copy(triangleElementClass.SpatialReference) as ISpatialReference;
            }
            if (null == triangleElementClass.FillSymbol)
            {
                this.m_fillSymbol = null;
            }
            else
            {
                this.m_fillSymbol = objectCopyClass.Copy(triangleElementClass.FillSymbol) as ISimpleFillSymbol;
            }
            if (null == triangleElementClass.Geometry)
            {
                this.m_triangle = null;
                this.m_pointGeometry = null;
            }
            else
            {
                this.m_triangle = objectCopyClass.Copy(triangleElementClass.Geometry) as IPolygon;
                this.m_pointGeometry = objectCopyClass.Copy(((IArea) this.m_triangle).Centroid) as IPoint;
            }
        }

        private void BuildTriangleGeometry(IPoint pointGeometry)
        {
            try
            {
                if ((this.m_triangle == null || pointGeometry == null ? false : null != this.m_cachedDisplay))
                {
                    this.m_triangle.SpatialReference = pointGeometry.SpatialReference;
                    this.m_triangle.SetEmpty();
                    object value = Missing.Value;
                    IPointCollection mTriangle = (IPointCollection) this.m_triangle;
                    double map = this.PointsToMap(this.m_cachedDisplay.DisplayTransformation, this.m_size);
                    double x = pointGeometry.X;
                    double y = pointGeometry.Y;
                    IPoint pointClass = new ESRI.ArcGIS.Geometry.Point()
                    {
                        X = x + map*0.866025403784439,
                        Y = y - 0.5*map
                    };
                    mTriangle.AddPoint(pointClass, ref value, ref value);
                    pointClass = new ESRI.ArcGIS.Geometry.Point()
                    {
                        X = x,
                        Y = y + map
                    };
                    mTriangle.AddPoint(pointClass, ref value, ref value);
                    pointClass = new ESRI.ArcGIS.Geometry.Point()
                    {
                        X = x - map*0.866025403784439,
                        Y = y - 0.5*map
                    };
                    mTriangle.AddPoint(pointClass, ref value, ref value);
                    this.m_triangle.Close();
                    if (this.m_rotation != 0)
                    {
                        ((ITransform2D) mTriangle).Rotate(pointGeometry, this.m_rotation*0.0174532925199433);
                    }
                    return;
                }
                else
                {
                    return;
                }
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception.Message);
            }
        }

        public bool CanRotate()
        {
            return true;
        }

        public IClone Clone()
        {
            TriangleElementClass triangleElementClass = new TriangleElementClass();
            triangleElementClass.Assign(this);
            return triangleElementClass;
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
            ICharacterMarkerSymbol characterMarkerSymbolClass = new CharacterMarkerSymbol()
            {
                Color = this.m_fillSymbol.Color,
                Angle = this.m_rotation,
                Size = this.m_size,
                //Font = ESRI.ArcGIS.ADF.Local.Converter.ToStdFont(new Font("ESRI Default Marker", (float)this.m_size, FontStyle.Regular)),
                CharacterIndex = 184
            };
            IMarkerElement markerElement = new MarkerElement() as IMarkerElement;
            markerElement.Symbol = characterMarkerSymbolClass;
            IMarkerElement markerElementClass = markerElement as IMarkerElement;
            IPoint point = ((IClone) this.m_pointGeometry).Clone() as IPoint;
            IElement element = (IElement) markerElementClass;
            element.Geometry = point;

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
                if (this.m_triangle.IsEmpty)
                {
                    this.BuildTriangleGeometry(this.m_pointGeometry);
                }
                Display.SetSymbol((ISymbol) this.m_fillSymbol);
                Display.DrawPolygon(this.m_triangle);
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
            this.SetDefaultDymbol();
        }

        public bool IsEqual(IClone other)
        {
            if (null == other)
            {
                throw new COMException("Invalid objact.");
            }
            if (!(other is TriangleElementClass))
            {
                throw new COMException("Bad object type.");
            }
            TriangleElementClass triangleElementClass = (TriangleElementClass) other;
            return ((!(triangleElementClass.Name == this.m_elementName) ||
                     !(triangleElementClass.Type == this.m_elementType) ||
                     triangleElementClass.AutoTransform != this.m_autoTrans ||
                     triangleElementClass.ReferenceScale != this.m_scaleRef ||
                     triangleElementClass.Angle != this.m_rotation || triangleElementClass.Size != this.m_size ||
                     triangleElementClass.AnchorPoint != this.m_anchorPointType ||
                     !((IClone) triangleElementClass.Geometry).IsEqual((IClone) this.m_triangle) ||
                     !((IClone) triangleElementClass.FillSymbol).IsEqual((IClone) this.m_fillSymbol)
                ? true
                : !((IClone) triangleElementClass.SpatialReference).IsEqual((IClone) this.m_nativeSR))
                ? false
                : true);
        }

        public bool IsIdentical(IClone other)
        {
            if (null == other)
            {
                throw new COMException("Invalid objact.");
            }
            if (!(other is TriangleElementClass))
            {
                throw new COMException("Bad object type.");
            }
            return ((TriangleElementClass) other != this ? false : true);
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
            this.m_fillSymbol = Stream.Read() as ISimpleFillSymbol;
            this.m_pointGeometry = Stream.Read() as IPoint;
            this.m_triangle = Stream.Read() as IPolygon;
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
                ((ITransform2D) this.m_pointGeometry).Move(dx, dy);
                this.RefreshTracker();
            }
        }

        public void MoveVector(ILine v)
        {
            if (null != this.m_triangle)
            {
                ((ITransform2D) this.m_triangle).MoveVector(v);
                ((ITransform2D) this.m_pointGeometry).MoveVector(v);
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
                ((ITransform2D) this.m_pointGeometry).Rotate(Origin, rotationAngle);
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
            Stream.Write(this.m_pointGeometry);
            Stream.Write(this.m_triangle);
            Stream.Write(this.m_rotation);
        }

        public void Scale(IPoint Origin, double sx, double sy)
        {
            if (null != this.m_triangle)
            {
                ((ITransform2D) this.m_triangle).Scale(Origin, sx, sy);
                ((ITransform2D) this.m_pointGeometry).Scale(Origin, sx, sy);
                if (this.m_autoTrans)
                {
                    TriangleElementClass mSize = this;
                    mSize.m_size = mSize.m_size*Math.Max(sx, sy);
                }
                this.RefreshTracker();
            }
        }

        private void SetDefaultDymbol()
        {
            IColor rGBColor = ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.Black);
            ISimpleLineSymbol simpleLineSymbolClass = new SimpleLineSymbol()
            {
                Style = esriSimpleLineStyle.esriSLSSolid,
                Width = 1,
                Color = rGBColor
            };
            rGBColor = ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.Navy);
            if (null == this.m_fillSymbol)
            {
                this.m_fillSymbol = new SimpleFillSymbol();
            }
            this.m_fillSymbol.Color = rGBColor;
            this.m_fillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
            this.m_fillSymbol.Outline = simpleLineSymbolClass;
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
                this.m_dDeviceRatio = Convert.ToDouble(TriangleElementClass.GetDeviceCaps(hDC, 88))/72;
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
                    TriangleElementClass mSize = this;
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