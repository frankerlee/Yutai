using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class MoveElementOperation : IOperation, IMoveElementOperation
    {
        private IActiveView iactiveView_0 = null;
        private IEnumElement ienumElement_0 = null;
        private IPoint ipoint_0 = null;

        public void Do()
        {
            this.method_0(true);
        }

        private void method_0(bool bool_0)
        {
            double x;
            double y;
            this.ienumElement_0.Reset();
            IElement data = this.ienumElement_0.Next();
            if (bool_0)
            {
                x = this.ipoint_0.X;
                y = this.ipoint_0.Y;
            }
            else
            {
                x = -this.ipoint_0.X;
                y = -this.ipoint_0.Y;
            }
            while (data != null)
            {
                if (this.iactiveView_0 is IMap)
                {
                    this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, data.Geometry.Envelope);
                }
                this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, data, null);
                (data as ITransform2D).Move(x, y);
                this.iactiveView_0.GraphicsContainer.UpdateElement(data);
                this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, data, null);
                if (this.iactiveView_0 is IMap)
                {
                    this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, data.Geometry.Envelope);
                }
                ElementChangeEvent.ElementPositionChange(data);
                data = this.ienumElement_0.Next();
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

        public IEnumElement Elements
        {
            set { this.ienumElement_0 = value; }
        }

        public string MenuString
        {
            get { return "移动"; }
        }

        public IPoint Point
        {
            set { this.ipoint_0 = value; }
        }
    }
}