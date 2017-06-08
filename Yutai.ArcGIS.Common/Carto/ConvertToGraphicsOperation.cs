using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Common.Carto
{
	public class ConvertToGraphicsOperation : IOperation, IConvertToGraphicsOperation
	{
		private IEnumElement ienumElement_0 = null;

		private IList ilist_0 = new ArrayList();

		private IActiveView iactiveView_0 = null;

		private List<IElement> list_0 = new List<IElement>();

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
				this.list_0.Clear();
				this.ienumElement_0 = value;
				this.ienumElement_0.Reset();
				for (IElement i = this.ienumElement_0.Next(); i != null; i = this.ienumElement_0.Next())
				{
					this.list_0.Add(i);
				}
			}
		}

		public string MenuString
		{
			get
			{
				return "转换为图形";
			}
		}

		public ConvertToGraphicsOperation()
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
			graphicsContainerSelect.UnselectAllElements();
			this.ienumElement_0.Reset();
			IElement element = this.ienumElement_0.Next();
			this.ilist_0.Clear();
			while (element != null)
			{
				if ((!(element is IGraphicsComposite) ? false : !(element is IMapFrame)))
				{
					IEnumElement graphics = (element as IGraphicsComposite).Graphics[this.iactiveView_0.ScreenDisplay, null];
					graphics.Reset();
					for (IElement i = graphics.Next(); i != null; i = graphics.Next())
					{
						graphicsContainer.AddElement(i, -1);
						graphicsContainerSelect.SelectElement(element);
						this.ilist_0.Add(i);
					}
				}
				element = this.ienumElement_0.Next();
			}
			foreach (IElement list0 in this.list_0)
			{
				graphicsContainer.DeleteElement(list0);
			}
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
			for (int i = 0; i < this.ilist_0.Count; i++)
			{
				graphicsContainer.DeleteElement(this.ilist_0[i] as IElement);
			}
			this.ienumElement_0.Reset();
			for (IElement j = this.ienumElement_0.Next(); j != null; j = this.ienumElement_0.Next())
			{
				if ((!(j is IGraphicsComposite) ? false : !(j is IMapFrame)))
				{
					graphicsContainer.AddElement(j, -1);
					graphicsContainerSelect.SelectElement(j);
				}
			}
		}
	}
}