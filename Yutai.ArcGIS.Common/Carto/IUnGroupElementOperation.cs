using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Common.Carto
{
	public interface IUnGroupElementOperation : IOperation
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