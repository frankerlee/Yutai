using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Common.Carto
{
	public interface IEditElementPropertiesOperation : IOperation
	{
		IActiveView ActiveView
		{
			set;
		}

		IElement Element
		{
			set;
		}
	}
}