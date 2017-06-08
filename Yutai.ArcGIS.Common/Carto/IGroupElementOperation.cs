using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Common.Carto
{
	public interface IGroupElementOperation : IOperation
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