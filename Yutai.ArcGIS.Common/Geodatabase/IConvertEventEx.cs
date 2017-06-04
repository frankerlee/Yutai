namespace Yutai.ArcGIS.Common.Geodatabase
{
	public interface IConvertEventEx : IConvertEvent
	{
		event SetFeatureClassMaxValueHandler SetFeatureClassMaxValueEvent;

		event SetFeatureClassMinValueHandler SetFeatureClassMinValueEvent;

		event SetFeatureClassPositionHandler SetFeatureClassPositionEvent;

		event SetHandleFeatureInfoHandler SetHandleFeatureInfoEvent;
	}
}