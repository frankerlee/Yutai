using System;
using System.Drawing;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Common.Display
{
	public class ScaleGeometryFeedback : IDisplayFeedback, IScaleGeometryFeedback
	{
		private IGeometry igeometry_0 = null;

		private IGeometry igeometry_1 = null;

		private IPoint ipoint_0 = null;

		private ISymbol isymbol_0 = null;

		private IScreenDisplay iscreenDisplay_0 = null;

		private IPoint ipoint_1 = null;

		private IPoint ipoint_2 = null;

		private double double_0 = 1;

		private double double_1 = 1;

		public IPoint AnchorPoint
		{
			set
			{
				this.ipoint_1 = value;
			}
		}

		public IScreenDisplay Display
		{
			set
			{
				this.iscreenDisplay_0 = value;
			}
		}

		public double Scale
		{
			get
			{
				return this.double_1;
			}
		}

		public ISymbol Symbol
		{
			get
			{
				return this.isymbol_0;
			}
			set
			{
				this.isymbol_0 = null;
			}
		}

		public ScaleGeometryFeedback()
		{
		}

		public void MoveTo(IPoint ipoint_3)
		{
			if ((this.isymbol_0 == null ? false : this.iscreenDisplay_0 != null))
			{
				Graphics graphic = Graphics.FromHwnd(new IntPtr(this.iscreenDisplay_0.hWnd));
				IntPtr hdc = graphic.GetHdc();
				this.isymbol_0.ROP2 = esriRasterOpCode.esriROPXOrPen;
				this.isymbol_0.SetupDC(hdc.ToInt32(), this.iscreenDisplay_0.DisplayTransformation);
				if (this.ipoint_2 != null)
				{
					this.isymbol_0.Draw(this.igeometry_1);
				}
				this.igeometry_1 = (this.igeometry_0 as IClone).Clone() as IGeometry;
				double num = CommonHelper.distance(ipoint_3, this.ipoint_1);
				this.double_1 = num / this.double_0;
				(this.igeometry_1 as ITransform2D).Scale(this.ipoint_1, this.double_1, this.double_1);
				this.isymbol_0.Draw(this.igeometry_1);
				this.isymbol_0.ResetDC();
				this.ipoint_2 = ipoint_3;
				graphic.ReleaseHdc(hdc);
				graphic.Dispose();
			}
		}

		public void Refresh(int int_0)
		{
			if ((this.isymbol_0 == null ? false : this.iscreenDisplay_0 != null))
			{
				Graphics graphic = null;
				IntPtr hdc = (IntPtr)0;
				if (int_0 == 0)
				{
					graphic = Graphics.FromHwnd(new IntPtr(this.iscreenDisplay_0.hWnd));
					hdc = graphic.GetHdc();
					int_0 = hdc.ToInt32();
				}
				this.isymbol_0.ROP2 = esriRasterOpCode.esriROPXOrPen;
				this.isymbol_0.SetupDC(int_0, this.iscreenDisplay_0.DisplayTransformation);
				this.ipoint_2 = null;
				this.isymbol_0.ROP2 = esriRasterOpCode.esriROPXOrPen;
				this.isymbol_0.SetupDC(int_0, this.iscreenDisplay_0.DisplayTransformation);
				this.isymbol_0.Draw(this.igeometry_1);
				this.isymbol_0.ResetDC();
				if (graphic != null)
				{
					graphic.ReleaseHdc(hdc);
				}
			}
		}

		public void Start(IPoint ipoint_3, IGeometry igeometry_2)
		{
			this.ipoint_0 = ipoint_3;
			this.igeometry_0 = igeometry_2;
			this.igeometry_1 = (this.igeometry_0 as IClone).Clone() as IGeometry;
			this.double_0 = CommonHelper.distance(this.ipoint_0, this.ipoint_1);
			if (this.igeometry_0.GeometryType == esriGeometryType.esriGeometryPolyline)
			{
				this.isymbol_0 = new SimpleLineSymbol() as ISymbol;
			}
			else if (this.igeometry_0.GeometryType == esriGeometryType.esriGeometryPolygon)
			{
				this.isymbol_0 = new SimpleFillSymbol() as ISymbol;
				(this.isymbol_0 as ISimpleFillSymbol).Style = esriSimpleFillStyle.esriSFSNull;
			}
		}

		public IGeometry Stop()
		{
			return this.igeometry_1;
		}
	}
}