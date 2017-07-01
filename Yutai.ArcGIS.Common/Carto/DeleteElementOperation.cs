using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Common.Carto
{
    public class DeleteElementOperation : IOperation, IDeleteElementOperation
    {
        private IEnumElement ienumElement_0 = null;

        private IPoint ipoint_0 = null;

        private IActiveView iactiveView_0 = null;

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
            get { return "删除"; }
        }

        public IPoint Point
        {
            set { this.ipoint_0 = value; }
        }

        public DeleteElementOperation()
        {
        }

        public void Do()
        {
            this.method_0(true);
        }

        private void method_0(bool bool_0)
        {
            IGraphicsContainer iactiveView0 = this.iactiveView_0 as IGraphicsContainer;
            this.ienumElement_0.Reset();
            IElement element = this.ienumElement_0.Next();
            IEnvelope envelope = null;
            while (element != null)
            {
                if (!bool_0)
                {
                    iactiveView0.AddElement(element, -1);
                }
                else
                {
                    if (envelope != null)
                    {
                        envelope.Union(element.Geometry.Envelope);
                    }
                    else
                    {
                        envelope = element.Geometry.Envelope;
                    }
                    this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
                    iactiveView0.DeleteElement(element);
                }
                ElementChangeEvent.DeleteElement(element);
                element = this.ienumElement_0.Next();
            }
            this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        public void Redo()
        {
            this.method_0(true);
        }

        public void Undo()
        {
            this.method_0(false);
        }
    }
}