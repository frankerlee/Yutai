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
        private IActiveView iactiveView_0 = null;
        private IActiveView iactiveView_1 = null;
      
        private string string_0;

        public YTTransformation(IActiveView iactiveView_2)
        {
            if (iactiveView_2 is IPageLayout)
            {
                this.iactiveView_0 = iactiveView_2.FocusMap as IActiveView;
                this.iactiveView_1 = iactiveView_2;
            }
            else
            {
                this.iactiveView_0 = iactiveView_2;
            }
        }

        public IPoint GetJWD(IPoint ipoint_0)
        {
            IPoint point = (ipoint_0 as IClone).Clone() as IPoint;
            if (this.iactiveView_0.FocusMap.SpatialReference is IProjectedCoordinateSystem)
            {
                if (point.SpatialReference == null)
                {
                    point.SpatialReference = this.iactiveView_0.FocusMap.SpatialReference;
                }
                else if (point.SpatialReference is IUnknownCoordinateSystem)
                {
                    point.SpatialReference = this.iactiveView_0.FocusMap.SpatialReference;
                }
                point.Project((this.iactiveView_0.FocusMap.SpatialReference as IProjectedCoordinateSystem).GeographicCoordinateSystem);
            }
            return point;
        }

        public void TextWidth(ITextElement itextElement_0, out double double_0, out double double_1)
        {
            IActiveView view = this.iactiveView_1;
            if (view == null)
            {
                view = this.iactiveView_0;
            }
            double xSize = 0.0;
            double ySize = 0.0;
            double_0 = 0.0;
            double_1 = 0.0;
            double num3 = 2.54;
            try
            {
                IEnvelope bounds = new EnvelopeClass();
                view.ScreenDisplay.StartDrawing(view.ScreenDisplay.hDC, 0);
                itextElement_0.Symbol.GetTextSize(view.ScreenDisplay.hDC, view.ScreenDisplay.DisplayTransformation, itextElement_0.Text, out xSize, out ySize);
                (itextElement_0 as IElement).QueryBounds(view.ScreenDisplay, bounds);
                view.ScreenDisplay.FinishDrawing();
                if (view is IPageLayout)
                {
                    double_0 = xSize * (num3 / 72.0);
                    double_1 = ySize * (num3 / 72.0);
                }
                else
                {
                    double_0 = ((xSize * (num3 / 72.0)) / 100.0) * view.FocusMap.ReferenceScale;
                    double_1 = ((ySize * (num3 / 72.0)) / 100.0) * view.FocusMap.ReferenceScale;
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
            if (this.iactiveView_1 == null)
            {
                return ipoint_0;
            }
            this.iactiveView_1.ScreenDisplay.DisplayTransformation.FromMapPoint(ipoint_0, out num, out num2);
            IMapFrame frame = this.iactiveView_1.GraphicsContainer.FindFrame(this.iactiveView_1.FocusMap) as IMapFrame;
            IEnvelope mapBounds = frame.MapBounds;
            IEnvelope envelope = (frame as IElement).Geometry.Envelope;
            double num3 = mapBounds.Width / envelope.Width;
            double num4 = mapBounds.Height / envelope.Height;
            double num5 = ((ipoint_0.X - envelope.XMin) * num3) + mapBounds.XMin;
            double num6 = ((ipoint_0.Y - envelope.YMin) * num4) + mapBounds.YMin;
            this.iactiveView_0.ScreenDisplay.DisplayTransformation.ToMapPoint(num, num2);
            return new PointClass { X = num5, Y = num6 };
        }

        public IPoint ToMapPoint(double double_0, double double_1)
        {
            IPoint point = new PointClass();
            point.PutCoords(double_0, double_1);
            return this.ToMapPoint(point);
        }

        public IPoint ToPageLayoutPoint(IPoint ipoint_0)
        {
            int num3;
            int num4;
            if (this.iactiveView_1 == null)
            {
                return ipoint_0;
            }
            double[] inPoints = new double[] { ipoint_0.X, ipoint_0.Y };
            double[] outPoints = new double[2];
            IEnvelope extent = this.iactiveView_1.Extent;
            IEnvelope envelope4 = this.iactiveView_0.Extent;
            IMapFrame frame = this.iactiveView_1.GraphicsContainer.FindFrame(this.iactiveView_1.FocusMap) as IMapFrame;
            IEnvelope mapBounds = frame.MapBounds;
            if (mapBounds.IsEmpty)
            {
                mapBounds = (this.iactiveView_1.FocusMap as IActiveView).Extent;
            }
            IEnvelope envelope = (frame as IElement).Geometry.Envelope;
            double num = mapBounds.Width / envelope.Width;
            double num2 = mapBounds.Height / envelope.Height;
            (this.iactiveView_0.ScreenDisplay.DisplayTransformation as ITransformationGEN).TransformPointsFF(esriTransformDirection.esriTransformForward, ref inPoints, ref outPoints);
            double[] numArray4 = new double[2];
            this.iactiveView_0.ScreenDisplay.DisplayTransformation.FromMapPoint(ipoint_0, out num3, out num4);
            (this.iactiveView_0.ScreenDisplay.DisplayTransformation as ITransformationGEN).TransformPointsFF(esriTransformDirection.esriTransformReverse, ref outPoints, ref numArray4);
            (this.iactiveView_1.ScreenDisplay.DisplayTransformation as ITransformationGEN).TransformPointsFF(esriTransformDirection.esriTransformReverse, ref outPoints, ref numArray4);
            this.iactiveView_1.ScreenDisplay.DisplayTransformation.ToMapPoint(num3, num4);
            double num5 = ((ipoint_0.X - mapBounds.XMin) / num) + envelope.XMin;
            double num6 = ((ipoint_0.Y - mapBounds.YMin) / num2) + envelope.YMin;
            return new PointClass { X = num5, Y = num6 };
        }

        public IPoint ToPageLayoutPoint(double double_0, double double_1)
        {
            IPoint point = new PointClass();
            point.PutCoords(double_0, double_1);
            return this.ToPageLayoutPoint(point);
        }

        public IEnvelope MapExtent
        {
            get
            {
                return this.iactiveView_0.Extent;
            }
        }

        public string Name
        { get; set; }
    }
}

