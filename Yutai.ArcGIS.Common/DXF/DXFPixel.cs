namespace Yutai.ArcGIS.Common.DXF
{
	public class DXFPixel : DXFFigure
	{
		public DXFPoint Point
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

		public DXFPixel()
		{
		}

		public DXFPixel(DXFData aData) : base(aData)
		{
		}

		public override void ExportAsDXF(DXFExport ADXFExport)
		{
			ADXFExport.AddName("POINT", "AcDbPoint");
			ADXFExport.AddColor(this.data);
			ADXFExport.Add3DPoint(10, this.data.point);
		}
	}
}