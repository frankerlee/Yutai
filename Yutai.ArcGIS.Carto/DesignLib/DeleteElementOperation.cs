using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class DeleteElementOperation : IOperation, IDeleteElementOperation
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
            IGraphicsContainer container = this.iactiveView_0 as IGraphicsContainer;
            this.ienumElement_0.Reset();
            IElement data = this.ienumElement_0.Next();
            IEnvelope envelope = null;
            while (data != null)
            {
                if (bool_0)
                {
                    if (envelope == null)
                    {
                        envelope = data.Geometry.Envelope;
                    }
                    else
                    {
                        envelope.Union(data.Geometry.Envelope);
                    }
                    this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, data, null);
                    container.DeleteElement(data);
                }
                else
                {
                    container.AddElement(data, -1);
                }
                ElementChangeEvent.DeleteElement(data);
                data = this.ienumElement_0.Next();
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

        public IActiveView ActiveView
        {
            set
            {
                this.iactiveView_0 = value;
            }
        }

        public bool CanRedo
        {
            get
            {
                return true;
            }
        }

        public bool CanUndo
        {
            get
            {
                return true;
            }
        }

        public IEnumElement Elements
        {
            set
            {
                this.ienumElement_0 = value;
            }
        }

        public string MenuString
        {
            get
            {
                return "删除";
            }
        }

        public IPoint Point
        {
            set
            {
                this.ipoint_0 = value;
            }
        }
    }
}

