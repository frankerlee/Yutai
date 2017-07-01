using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class ResizeElementOperation : IOperation, IResizeElementOperation
    {
        private IActiveView iactiveView_0 = null;
        private IElement ielement_0 = null;
        private IEnvelope ienvelope_0 = null;
        private IEnvelope ienvelope_1 = null;
        private IGeometry igeometry_0 = null;

        public void Do()
        {
            this.method_0(true);
        }

        private void method_0(bool bool_0)
        {
            double width = this.ienvelope_0.Width;
            double height = this.ienvelope_0.Height;
            if (width == 0.0)
            {
                this.ienvelope_0.Width = 1.0;
                this.ienvelope_1.Width = 1.0;
            }
            if (height == 0.0)
            {
                this.ienvelope_0.Height = 1.0;
                this.ienvelope_1.Height = 1.0;
            }
            IAffineTransformation2D transformation = new AffineTransformation2DClass();
            transformation.DefineFromEnvelopes(this.ienvelope_0, this.ienvelope_1);
            ITransform2D transformd = this.ielement_0 as ITransform2D;
            if (bool_0)
            {
                transformd.Transform(esriTransformDirection.esriTransformForward, transformation);
            }
            else
            {
                transformd.Transform(esriTransformDirection.esriTransformReverse, transformation);
            }
        }

        public void Redo()
        {
            this.method_0(true);
        }

        public void Undo()
        {
            this.method_0(false);
        }

        public IActiveView ActiveView
        {
            set { this.iactiveView_0 = value; }
        }

        public bool CanRedo
        {
            get { return true; }
        }

        public bool CanUndo
        {
            get { return true; }
        }

        public IElement Element
        {
            set
            {
                this.ielement_0 = value;
                this.ienvelope_0 = this.ielement_0.Geometry.Envelope;
            }
        }

        public IGeometry Geometry
        {
            set
            {
                this.igeometry_0 = value;
                this.ienvelope_1 = this.igeometry_0.Envelope;
            }
        }

        public string MenuString
        {
            get { return "缩放元素"; }
        }
    }
}