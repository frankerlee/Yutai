using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Steema.TeeChart;
using Steema.TeeChart.Styles;


namespace Yutai.ArcGIS.Common.ExtendClass
{
    [Guid("C86B5915-443F-4108-8FE7-386FCBD038DB")]
    public class DataGraphicsElement : IElement, IClone, ITransform2D, IElementProperties, IElementProperties2,
        IBoundsProperties, IPersistVariant
    {
        // Fields
        private ChartType _Type = ChartType.Line;
        public const string CLASSGUID = "C86B5915-443F-4108-8FE7-386FCBD038DB";
        private bool m_bLocked = false;
        private Chart m_chart = new Chart();
        private bool m_FixedAspectRatio = true;
        private double m_OldObjectX = 0.0;
        private double m_OldObjectY = 0.0;
        private IDisplay m_pCachedDisplay = null;
        private object m_pCustomProperty = null;
        private IFillSymbol m_pFillSymbol1;
        private IEnvelope m_pGeometry = null;
        private ISelectionTracker m_pSelectionTracker = null;
        private double m_radio = 1.0;
        private string m_sElementName = "DataGraphicsElement";
        private string m_sElementType = "DataGraphicsElement";

        // Methods
        public DataGraphicsElement()
        {
            this.m_pSelectionTracker = new EnvelopeTracker();
            this.m_pSelectionTracker.Locked = false;
            this.m_pSelectionTracker.ShowHandles = true;
            this.m_pGeometry = new Envelope() as IEnvelope;
            this.m_pGeometry.PutCoords(4.0, 4.0, 18.5, 16.4);
            this.m_pFillSymbol1 = new SimpleFillSymbol();
            (this.m_pFillSymbol1 as ISimpleFillSymbol).Style = esriSimpleFillStyle.esriSFSNull;
            this.m_chart.Header.Text = "实例";
            Series s = new Bar
            {
                Title = "实例1"
            };
            s.Add(new List<double> {30.0, 50.0, 60.0, 80.0});
            this.m_chart.Series.Add(s);
            s = new Bar
            {
                Title = "实例1"
            };
            s.Add(new List<double> {20.0, 50.0, 20.0, 60.0});
            this.m_chart.Series.Add(s);
        }

        public void Activate(IDisplay Display)
        {
            this.m_pCachedDisplay = Display;
            this.m_pSelectionTracker.Display = this.m_pCachedDisplay as IScreenDisplay;
            this.RefreshTracker();
        }

        public void AddSeries(ISeriesProperties pSeries)
        {
        }

        public void AddSeries(IList<double> data)
        {
        }

        private static void ArcGISCategoryRegistration(Type registerType)
        {
            MxCommands.Register(string.Format(@"HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID));
        }

        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            MxCommands.Unregister(string.Format(@"HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID));
        }

        public void Assign(IClone src)
        {
            src = this;
        }

        public bool CanRotate()
        {
            return false;
        }

        public IClone Clone()
        {
            return null;
        }

        private Series CreateSeriesType(DataTable dt, string Name, string XValues, string YValues, string Label)
        {
            Series series;
            switch (this._Type)
            {
                case ChartType.Bar:
                    series = new Bar();
                    break;

                case ChartType.Pie:
                    series = new Pie();
                    break;

                case ChartType.Area:
                    series = new Area();
                    break;

                default:
                    series = new Steema.TeeChart.Styles.Line();
                    break;
            }
            series.Title = Name;
            series.DataSource = dt;
            series.XValues.DataMember = XValues;
            series.XValues.Order = ValueListOrder.Ascending;
            series.YValues.DataMember = YValues;
            series.YValues.Order = ValueListOrder.None;
            return series;
        }

        public void Deactivate()
        {
        }

        public void Draw(IDisplay Display, ITrackCancel trackCancel)
        {
            int num;
            int num2;
            int num3;
            int num4;
            IPoint mapPoint = new ESRI.ArcGIS.Geometry.Point();
            mapPoint.PutCoords(this.m_pGeometry.XMin, this.m_pGeometry.YMin);
            Display.DisplayTransformation.FromMapPoint(mapPoint, out num, out num2);
            mapPoint.PutCoords(this.m_pGeometry.XMax, this.m_pGeometry.YMax);
            Display.DisplayTransformation.FromMapPoint(mapPoint, out num3, out num4);
            double num5 = num3 - num;
            double num6 = num2 - num4;
            IntPtr hdc = new IntPtr(Display.hDC);
            Graphics g = Graphics.FromHdc(hdc);
            Rectangle r = new Rectangle(num, num4, (int) num5, (int) num6);
            this.m_chart.Draw(g, r);
            g.Dispose();
        }

        public bool HitTest(double x, double y, double Tolerance)
        {
            IPoint other = new ESRI.ArcGIS.Geometry.Point();
            other.PutCoords(x, y);
            IRelationalOperator @operator = new ESRI.ArcGIS.Geometry.Polygon() as IRelationalOperator;
            this.QueryOutline(this.m_pCachedDisplay, @operator as IPolygon);
            return !@operator.Disjoint(other);
        }

        public bool IsEqual(IClone other)
        {
            return false;
        }

        public bool IsIdentical(IClone other)
        {
            return (other == this);
        }

        public void Load(IVariantStream Stream)
        {
        }

        public void Move(double dx, double dy)
        {
            (this.m_pGeometry as ITransform2D).Move(dx, dy);
            this.RefreshTracker();
        }

        public void MoveVector(ILine v)
        {
            (this.m_pGeometry as ITransform2D).MoveVector(v);
            this.RefreshTracker();
        }

        public void QueryBounds(IDisplay Display, IEnvelope Bounds)
        {
            IPolygon boundary = new ESRI.ArcGIS.Geometry.Polygon() as IPolygon;
            boundary.SetEmpty();
            ((ISymbol) this.m_pFillSymbol1).QueryBoundary(Display.hDC, Display.DisplayTransformation, this.m_pGeometry,
                boundary);
            Bounds.XMin = boundary.Envelope.XMin;
            Bounds.XMax = boundary.Envelope.XMax;
            Bounds.YMin = boundary.Envelope.YMin;
            Bounds.YMax = boundary.Envelope.YMax;
            Bounds.SpatialReference = boundary.Envelope.SpatialReference;
        }

        public void QueryOutline(IDisplay Display, IPolygon Outline)
        {
            IPolygon boundary = new ESRI.ArcGIS.Geometry.Polygon() as IPolygon;
            boundary.SetEmpty();
            ((ISymbol) this.m_pFillSymbol1).QueryBoundary(Display.hDC, Display.DisplayTransformation, this.m_pGeometry,
                boundary);
            ((IPointCollection) Outline).AddPointCollection((IPointCollection) boundary);
        }

        private void RefreshTracker()
        {
            if (this.m_pCachedDisplay != null)
            {
                IGeometry geometry = new ESRI.ArcGIS.Geometry.Polygon() as IGeometry;
                this.QueryOutline(this.m_pCachedDisplay, geometry as IPolygon);
                this.m_pSelectionTracker.Geometry = geometry.Envelope;
            }
        }

        [ComRegisterFunction, ComVisible(false)]
        private static void RegisterFunction(Type registerType)
        {
            ArcGISCategoryRegistration(registerType);
        }

        public void Rotate(IPoint Origin, double rotationAngle)
        {
        }

        public void Save(IVariantStream Stream)
        {
        }

        public void Scale(IPoint Origin, double sx, double sy)
        {
            (this.m_pGeometry as ITransform2D).Scale(Origin, sx, sy);
            this.RefreshTracker();
        }

        public void Transform(esriTransformDirection direction, ITransformation transformation)
        {
            object before = Missing.Value;
            IPointCollection points = new ESRI.ArcGIS.Geometry.Polygon();
            points.AddPoint(this.m_pGeometry.LowerLeft, ref before, ref before);
            points.AddPoint(this.m_pGeometry.LowerRight, ref before, ref before);
            points.AddPoint(this.m_pGeometry.UpperRight, ref before, ref before);
            points.AddPoint(this.m_pGeometry.UpperLeft, ref before, ref before);
            (points as IPolygon).Close();
            (points as IGeometry).SpatialReference = this.m_pGeometry.SpatialReference;
            (points as ITransform2D).Transform(direction, transformation);
            IEnvelope envelope = (points as IPolygon).Envelope;
            this.m_pGeometry = envelope;
            this.RefreshTracker();
        }

        [ComVisible(false), ComUnregisterFunction]
        private static void UnregisterFunction(Type registerType)
        {
            ArcGISCategoryUnregistration(registerType);
        }

        // Properties
        public bool AutoTransform
        {
            get { return true; }
            set { }
        }

        public string ChartTitle
        {
            get { return this.m_chart.Header.Text; }
            set { this.m_chart.Header.Text = value; }
        }

        public object CustomProperty
        {
            get { return this.m_pCustomProperty; }
            set { this.m_pCustomProperty = value; }
        }

        public bool FixedAspectRatio
        {
            get { return false; }
            set { }
        }

        public bool FixedSize
        {
            get { return true; }
        }

        public IGeometry Geometry
        {
            get { return this.m_pGeometry; }
            set { this.m_pGeometry = value.Envelope; }
        }

        public UID ID
        {
            get { return new UID {Value = "{C86B5915-443F-4108-8FE7-386FCBD038DB}"}; }
        }

        public bool Locked
        {
            get { return this.m_bLocked; }
            set { this.m_bLocked = value; }
        }

        public string Name
        {
            get { return this.m_sElementName; }
            set { this.m_sElementName = value; }
        }

        public double ReferenceScale
        {
            get { return 0.0; }
            set { }
        }

        public ISelectionTracker SelectionTracker
        {
            get { return this.m_pSelectionTracker; }
        }

        public string Type
        {
            get { return this.m_sElementType; }
            set { this.m_sElementType = value; }
        }

        // Nested Types
        public enum ChartType
        {
            Line,
            Bar,
            Pie,
            Area
        }
    }
}