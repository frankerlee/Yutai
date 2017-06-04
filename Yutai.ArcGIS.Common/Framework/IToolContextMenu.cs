namespace Yutai.ArcGIS.Common.Framework
{
	public interface IToolContextMenu
	{
		IPopuMenuWrap PopuMenu
		{
			set;
		}

		void Init();
	}
}
