using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class YTTransformation
    {
        private IActiveView m_pActiveView1 = null;
        private IActiveView m_pActiveView2 = null;
      
        private string string_0;

        public YTTransformation(IActiveView pActiveView)
        {
            if (pActiveView is IPageLayout)
            {
                this.m_pActiveView1 = pActiveView.FocusMap as IActiveView;
                this.m_pActiveView2 = pActiveView;
            }
            else
            {
                this.m_pActiveView1 = pActiveView;
            }
        }

        public IPoint GetJWD(IPoint pPoint)
        {
            IPoint point = (pPoint as IClone).Clone() as IPoint;
            if (this.m_pActiveView1.FocusMap.SpatialReference is IProjectedCoordinateSystem)
            {
                if (point.SpatialReference == null)
                {
                    point.SpatialReference = this.m_pActiveView1.FocusMap.SpatialReference;
                }
                else if (point.SpatialReference is IUnknownCoordinateSystem)
                {
                    point.SpatialReference = this.m_pActiveView1.FocusMap.SpatialReference;
                }
                point.Project((this.m_pActiveView1.FocusMap.SpatialReference as IProjectedCoordinateSystem).GeographicCoordinateSystem);
            }
            return point;
        }

        public void TextWidth(ITextElement pTextElement, out double width, out double height)
        {
            IActiveView view = this.m_pActiveView2;
            if (view == null)
            {
                view = this.m_pActiveView1;
            }
            double xSize = 0.0;
            double ySize = 0.0;
            width = 0.0;
            height = 0.0;
            double num3 = 2.54;
            try
            {
                IEnvelope bounds = new EnvelopeClass();
                view.ScreenDisplay.StartDrawing(view.ScreenDisplay.hDC, 0);
                pTextElement.Symbol.GetTextSize(view.ScreenDisplay.hDC, view.ScreenDisplay.DisplayTransformation, pTextElement.Text, out xSize, out ySize);
                (pTextElement as IElement).QueryBounds(view.ScreenDisplay, bounds);
                view.ScreenDisplay.FinishDrawing();
                if (view is IPageLayout)
                {
                    width = xSize * (num3 / 72.0);
                    height = ySize * (num3 / 72.0);
                }
                else
                {
                    width = ((xSize * (num3 / 72.0)) / 100.0) * view.FocusMap.ReferenceScale;
                    height = ((ySize * (num3 / 72.0)) / 100.0) * view.FocusMap.ReferenceScale;
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }

        public IPoint ToMapPoint(IPoint ipoint_0)
        {
            int num;
            int num2;
            if (this.m_pActiveView2 == null)
            {
                return ipoint_0;
            }
            this.m_pActiveView2.ScreenDisplay.DisplayTransformation.FromMapPoint(ipoint_0, out num, out num2);
            IMapFrame frame = this.m_pActiveView2.GraphicsContainer.FindFrame(this.m_pActiveView2.FocusMap) as IMapFrame;
            IEnvelope mapBounds = frame.MapBounds;
            IEnvelope envelope = (frame as IElement).Geometry.Envelope;
            double num3 = mapBounds.Width / envelope.Width;
            double num4 = mapBounds.Height / envelope.Height;
            double num5 = ((ipoint_0.X - envelope.XMin) * num3) + mapBounds.XMin;
            double num6 = ((ipoint_0.Y - envelope.YMin) * num4) + mapBounds.YMin;
            this.m_pActiveView1.ScreenDisplay.DisplayTransformation.ToMapPoint(num, num2);
            return new PointClass { X = num5, Y = num6 };
        }

        public IPoint ToMapPoint(double x, double y)
        {
            IPoint point = new PointClass();
            point.PutCoords(x, y);
            return this.ToMapPoint(point);
        }

        public IPoint ToPageLayoutPoint(IPoint ipoint_0)
        {
            int num3;
            int num4;
            if (this.m_pActiveView2 == null)
            {
                return ipoint_0;
            }
            double[] inPoints = new double[] { ipoint_0.X, ipoint_0.Y };
            double[] outPoints = new double[2];
            IEnvelope extent = this.m_pActiveView2.Extent;
            IEnvelope envelope4 = this.m_pActiveView1.Extent;
            IMapFrame frame = this.m_pActiveView2.GraphicsContainer.FindFrame(this.m_pActiveView2.FocusMap) as IMapFrame;
            IEnvelope mapBounds = frame.MapBounds;
            if (mapBounds.IsEmpty)
            {
                mapBounds = (this.m_pActiveView2.FocusMap as IActiveView).Extent;
            }
            IEnvelope envelope = (frame as IElement).Geometry.Envelope;
            double num = mapBounds.Width / envelope.Width;
            double num2 = mapBounds.Height / envelope.Height;
            (this.m_pActiveView1.ScreenDisplay.DisplayTransformation as ITransformationGEN).TransformPointsFF(esriTransformDirection.esriTransformForward, ref inPoints, ref outPoints);
            double[] numArray4 = new double[2];
            this.m_pActiveView1.ScreenDisplay.DisplayTransformation.FromMapPoint(ipoint_0, out num3, out num4);
            (this.m_pActiveView1.ScreenDisplay.DisplayTransformation as ITransformationGEN).TransformPointsFF(esriTransformDirection.esriTransformReverse, ref outPoints, ref numArray4);
            (this.m_pActiveView2.ScreenDisplay.DisplayTransformation as ITransformationGEN).TransformPointsFF(esriTransformDirection.esriTransformReverse, ref outPoints, ref numArray4);
            this.m_pActiveView2.ScreenDisplay.DisplayTransformation.ToMapPoint(num3, num4);
            double num5 = ((ipoint_0.X - mapBounds.XMin) / num) + envelope.XMin;
            double num6 = ((ipoint_0.Y - mapBounds.YMin) / num2) + envelope.YMin;
            return new PointClass { X = num5, Y = num6 };
        }

        public IPoint ToPageLayoutPoint(double x, double y)
        {
            IPoint point = new PointClass();
            point.PutCoords(x, y);
            return this.ToPageLayoutPoint(point);
        }

        public IEnvelope MapExtent
        {
            get
            {
                return this.m_pActiveView1.Extent;
            }
        }

        public string Name
        { get; set; }
    }
}

