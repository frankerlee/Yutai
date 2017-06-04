namespace Yutai.ArcGIS.Common.DXF
{
	public class DXFText : DXFFigure
	{
		public DXFText()
		{
		}

		public DXFText(DXFData aData) : base(aData)
		{
		}

		public override void ExportAsDXF(DXFExport ADXFExport)
		{
			ADXFExport.AddName("TEXT", "AcDbText");
			ADXFExport.AddColor(this.data);
			ADXFExport.Add3DPoint(10, this.data.point);
			ADXFExport.AddFloat(40, ADXFExport.MM(this.data.height));
			if (this.data.fScale != 0f)
			{
				ADXFExport.AddFloat(41, this.data.fScale);
			}
			if (this.data.rotation != 0f)
			{
				ADXFExport.AddFloat(50, this.data.rotation);
			}
			if (this.data.flags != 0)
			{
				ADXFExport.AddFloat(51, 15f);
			}
			if (this.data.hAlign != 0 || this.data.vAlign != 0)
			{
				if (this.data.hAlign != 0)
				{
					ADXFExport.AddInt(72, (int)this.data.hAlign);
				}
				ADXFExport.Add3DPoint(11, this.data.point1);
			}
			ADXFExport.current.Add("  1");
			ADXFExport.current.Add(this.data.text);
			ADXFExport.current.Add("100");
			ADXFExport.current.Add("AcDbText");
			if (this.data.vAlign != 0)
			{
				ADXFExport.AddInt(73, (int)this.data.vAlign);
			}
		}
	}
}