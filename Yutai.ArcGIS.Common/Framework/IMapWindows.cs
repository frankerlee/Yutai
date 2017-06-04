namespace Yutai.ArcGIS.Common.Framework
{
	public interface IMapWindows
	{
		object ActiveObject
		{
			get;
		}

		object Hook
		{
			get;
		}

		void RefreshMap();
	}
}
