using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Common.Carto
{
	public interface IConvertToGraphicsOperation : IOperation
	{
		IActiveView ActiveView
		{
			set;
		}

		IEnumElement Elements
		{
			set;
		}
	}
}