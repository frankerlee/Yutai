using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Common.Wrapper
{
	public class LegendItemObject
	{
		private ILegendItem ilegendItem_0 = null;

		public ILegendItem LegendItem
		{
			get
			{
				return this.ilegendItem_0;
			}
			set
			{
				this.ilegendItem_0 = null;
			}
		}

		public LegendItemObject(ILegendItem ilegendItem_1)
		{
			this.ilegendItem_0 = ilegendItem_1;
		}

		public override string ToString()
		{
			return this.ilegendItem_0.Layer.Name;
		}
	}
}
