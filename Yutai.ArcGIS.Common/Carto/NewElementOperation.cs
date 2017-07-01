using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Common.Carto
{
    public class NewElementOperation : IOperation, INewElementOperation
    {
        private IElement ielement_0 = null;

        private IActiveView iactiveView_0 = null;

        private IActiveView iactiveView_1 = null;

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

        public IActiveView ContainHook
        {
            set { this.iactiveView_1 = null; }
        }

        public IElement Element
        {
            set { this.ielement_0 = value; }
        }

        public string MenuString
        {
            get { return "新建"; }
        }

        public NewElementOperation()
        {
        }

        public void Do()
        {
            this.iactiveView_0.GraphicsContainer.AddElement(this.ielement_0, -1);
            if ((this.iactiveView_1 == null ? false : this.iactiveView_1 != this.iactiveView_0))
            {
                this.iactiveView_1.GraphicsContainer.AddElement(this.ielement_0, -1);
            }
            ElementOperator.FocusOneElement(this.iactiveView_0, this.ielement_0);
        }

        public void Redo()
        {
            this.iactiveView_0.GraphicsContainer.AddElement(this.ielement_0, -1);
            if ((this.iactiveView_1 == null ? false : this.iactiveView_1 != this.iactiveView_0))
            {
                this.iactiveView_1.GraphicsContainer.AddElement(this.ielement_0, -1);
            }
            ElementOperator.FocusOneElement(this.iactiveView_0, this.ielement_0);
        }

        public void Undo()
        {
            this.iactiveView_0.GraphicsContainer.DeleteElement(this.ielement_0);
            if ((this.iactiveView_1 == null ? false : this.iactiveView_1 != this.iactiveView_0))
            {
                IGraphicsContainer graphicsContainer = this.iactiveView_1.GraphicsContainer;
                this.iactiveView_1.GraphicsContainer.DeleteElement(this.ielement_0);
            }
            this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }
    }
}