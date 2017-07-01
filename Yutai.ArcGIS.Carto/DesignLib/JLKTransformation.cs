using System;
using System.Diagnostics;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class JLKTransformation
    {
        private IActiveView iactiveView_0 = null;
        private IActiveView iactiveView_1 = null;

        public JLKTransformation(IActiveView iactiveView_2)
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
                itextElement_0.Symbol.GetTextSize(view.ScreenDisplay.hDC, view.ScreenDisplay.DisplayTransformation,
                    itextElement_0.Text, out xSize, out ySize);
                (itextElement_0 as IElement).QueryBounds(view.ScreenDisplay, bounds);
                view.ScreenDisplay.FinishDrawing();
                if (view is IPageLayout)
                {
                    double_0 = xSize*(num3/72.0);
                    double_1 = ySize*(num3/72.0);
                }
                else
                {
                    double_0 = ((xSize*(num3/72.0))/100.0)*view.FocusMap.ReferenceScale;
                    double_1 = ((ySize*(num3/72.0))/100.0)*view.FocusMap.ReferenceScale;
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
            return this.iactiveView_0.ScreenDisplay.DisplayTransformation.ToMapPoint(num, num2);
        }

        public IPoint ToMapPoint(double double_0, double double_1)
        {
            IPoint point = new PointClass();
            point.PutCoords(double_0, double_1);
            return this.ToMapPoint(point);
        }

        public IPoint ToPageLayoutPoint(IPoint ipoint_0)
        {
            int num;
            int num2;
            if (this.iactiveView_1 == null)
            {
                return ipoint_0;
            }
            this.iactiveView_0.ScreenDisplay.DisplayTransformation.FromMapPoint(ipoint_0, out num, out num2);
            return this.iactiveView_1.ScreenDisplay.DisplayTransformation.ToMapPoint(num, num2);
        }

        public IPoint ToPageLayoutPoint(double double_0, double double_1)
        {
            IPoint point = new PointClass();
            point.PutCoords(double_0, double_1);
            return this.ToPageLayoutPoint(point);
        }

        public IEnvelope MapExtent
        {
            get { return this.iactiveView_0.Extent; }
        }
    }
}