using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Common.Carto
{
	public class ResizeElementOperation : IOperation, IResizeElementOperation
	{
		private IElement ielement_0 = null;

		private IGeometry igeometry_0 = null;

		private IEnvelope ienvelope_0 = null;

		private IEnvelope ienvelope_1 = null;

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
			get
			{
				return "缩放元素";
			}
		}

		public ResizeElementOperation()
		{
		}

		public void Do()
		{
			this.method_0(true);
		}

		private void method_0(bool bool_0)
		{
			double width = this.ienvelope_0.Width;
			double height = this.ienvelope_0.Height;
			if (width == 0)
			{
				this.ienvelope_0.Width = 1;
				this.ienvelope_1.Width = 1;
			}
			if (height == 0)
			{
				this.ienvelope_0.Height = 1;
				this.ienvelope_1.Height = 1;
			}
			IAffineTransformation2D affineTransformation2DClass = new AffineTransformation2D() as IAffineTransformation2D;
			affineTransformation2DClass.DefineFromEnvelopes(this.ienvelope_0, this.ienvelope_1);
			ITransform2D ielement0 = this.ielement_0 as ITransform2D;
			if (!bool_0)
			{
				ielement0.Transform(esriTransformDirection.esriTransformReverse, affineTransformation2DClass);
			}
			else
			{
				ielement0.Transform(esriTransformDirection.esriTransformForward, affineTransformation2DClass);
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