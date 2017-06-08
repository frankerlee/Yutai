using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Common.Carto
{
	public class UnGroupElementOperation : IOperation, IUnGroupElementOperation
	{
		private IEnumElement ienumElement_0 = null;

		private IActiveView iactiveView_0 = null;

		private IGroupElement2 igroupElement2_0 = null;

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
				return "取消组合";
			}
		}

		public UnGroupElementOperation()
		{
		}

		public void Do()
		{
			this.method_0();
		}

		private void method_0()
		{
			this.ienumElement_0.Reset();
			IElement element = this.ienumElement_0.Next();
			IGraphicsContainer graphicsContainer = this.iactiveView_0.GraphicsContainer;
			IGraphicsContainerSelect graphicsContainerSelect = graphicsContainer as IGraphicsContainerSelect;
			graphicsContainerSelect.UnselectAllElements();
			graphicsContainer.DeleteElement(element);
			while (element != null)
			{
				if (element is IGroupElement)
				{
					IEnumElement elements = (element as IGroupElement).Elements;
					elements.Reset();
					for (IElement i = elements.Next(); i != null; i = elements.Next())
					{
						(element as IElementProperties2).AutoTransform = true;
						graphicsContainer.AddElement(i, -1);
						graphicsContainerSelect.SelectElement(i);
					}
				}
				element = this.ienumElement_0.Next();
			}
		}

		public void Redo()
		{
			this.method_0();
		}

		public void Undo()
		{
			this.ienumElement_0.Reset();
			IElement element = this.ienumElement_0.Next();
			IGraphicsContainer graphicsContainer = this.iactiveView_0.GraphicsContainer;
			IGraphicsContainerSelect graphicsContainerSelect = graphicsContainer as IGraphicsContainerSelect;
			graphicsContainerSelect.UnselectAllElements();
			while (element != null)
			{
				if (element is IGroupElement)
				{
					IEnumElement elements = (element as IGroupElement).Elements;
					elements.Reset();
					for (IElement i = elements.Next(); i != null; i = elements.Next())
					{
						graphicsContainer.DeleteElement(i);
					}
					graphicsContainer.AddElement(element, -1);
					graphicsContainerSelect.SelectElement(element);
				}
				element = this.ienumElement_0.Next();
			}
		}
	}
}