using System;

namespace Yutai.ArcGIS.Common.DXF
{
	public class DXFLine : DXFFigure, ICloneable
	{
		public DXFPoint EndPoint
		{
			get
			{
				return this.data.point1;
			}
			set
			{
				this.data.point1 = (DXFPoint)value.Clone();
			}
		}

		public DXFPoint StartPoint
		{
			get
			{
				return this.data.point;
			}
			set
			{
				this.data.point = (DXFPoint)value.Clone();
			}
		}

		public DXFLine()
		{
		}

		public DXFLine(DXFData aData) : base(aData)
		{
		}

		public object Clone()
		{
			DXFLine dXFLine = new DXFLine()
			{
				StartPoint = (DXFPoint)this.StartPoint.Clone(),
				EndPoint = (DXFPoint)this.EndPoint.Clone()
			};
			return dXFLine;
		}

		public override void ExportAsDXF(DXFExport ADXFExport)
		{
			ADXFExport.AddName("LINE", "AcDbLine");
			ADXFExport.AddColor(this.data);
			ADXFExport.AddThickness(this.data);
			ADXFExport.Add3DPoint(10, this.data.point);
			ADXFExport.Add3DPoint(11, this.data.point1);
		}
	}
}