using System.Collections.Generic;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Common.Carto
{
    public class GroupElementOperation : IOperation, IGroupElementOperation
    {
        private IEnumElement ienumElement_0 = null;

        private IActiveView iactiveView_0 = null;

        private IGroupElement2 igroupElement2_0 = null;

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
            get { return "组合"; }
        }

        public GroupElementOperation()
        {
        }

        public void Do()
        {
            this.method_0();
        }

        private void method_0()
        {
            IGraphicsContainer graphicsContainer = this.iactiveView_0.GraphicsContainer;
            IGraphicsContainerSelect graphicsContainerSelect = graphicsContainer as IGraphicsContainerSelect;
            this.ienumElement_0.Reset();
            IElement element = this.ienumElement_0.Next();
            List<IElement> elements = new List<IElement>();
            if (this.igroupElement2_0 == null)
            {
                this.igroupElement2_0 = new GroupElement() as IGroupElement2;
                while (element != null)
                {
                    this.igroupElement2_0.AddElement(element);
                    elements.Add(element);
                    element = this.ienumElement_0.Next();
                }
            }
            for (int i = 0; i < elements.Count; i++)
            {
                graphicsContainer.DeleteElement(elements[i]);
            }
            graphicsContainer.AddElement(this.igroupElement2_0 as IElement, -1);
            graphicsContainerSelect.SelectElement(this.igroupElement2_0 as IElement);
        }

        public void Redo()
        {
            this.method_0();
        }

        public void Undo()
        {
            IGraphicsContainer graphicsContainer = this.iactiveView_0.GraphicsContainer;
            IGraphicsContainerSelect graphicsContainerSelect = graphicsContainer as IGraphicsContainerSelect;
            graphicsContainerSelect.UnselectAllElements();
            graphicsContainer.DeleteElement(this.igroupElement2_0 as IElement);
            this.ienumElement_0.Reset();
            for (IElement i = this.ienumElement_0.Next(); i != null; i = this.ienumElement_0.Next())
            {
                graphicsContainer.AddElement(i, -1);
                graphicsContainerSelect.SelectElement(i);
            }
        }
    }
}