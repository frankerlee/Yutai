using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Common.Carto
{
	public class MoveElementOperation : IOperation, IMoveElementOperation
	{
		private IEnumElement ienumElement_0 = null;

		private IPoint ipoint_0 = null;

		private IActiveView iactiveView_0 = null;

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
				return "移动";
			}
		}

		public IPoint Point
		{
			set
			{
				this.ipoint_0 = value;
			}
		}

		public MoveElementOperation()
		{
		}

		public void Do()
		{
			this.method_0(true);
		}

		private void method_0(bool bool_0)
		{
			double x;
			double y;
			try
			{
				this.ienumElement_0.Reset();
				IElement element = this.ienumElement_0.Next();
				if (!bool_0)
				{
					x = -this.ipoint_0.X;
					y = -this.ipoint_0.Y;
				}
				else
				{
					x = this.ipoint_0.X;
					y = this.ipoint_0.Y;
				}
				while (element != null)
				{
					if (this.iactiveView_0 is IMap)
					{
						this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, element.Geometry.Envelope);
					}
					this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
					(element as ITransform2D).Move(x, y);
					this.iactiveView_0.GraphicsContainer.UpdateElement(element);
					this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
					if (this.iactiveView_0 is IMap)
					{
						this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, element.Geometry.Envelope);
					}
					ElementChangeEvent.ElementPositionChange(element);
					element = this.ienumElement_0.Next();
				}
			}
			catch (Exception exception)
			{
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
	}
}