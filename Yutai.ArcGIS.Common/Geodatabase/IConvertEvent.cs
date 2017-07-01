namespace Yutai.ArcGIS.Common.Geodatabase
{
    public interface IConvertEvent
    {
        event FinishHander FinishEvent;

        event SetFeatureClassNameEnventHandler SetFeatureClassNameEnvent;

        event SetFeatureCountEnventHandler SetFeatureCountEnvent;

        event SetMaxValueHandler SetMaxValueEvent;

        event SetMessageHandler SetMessageEvent;

        event SetMinValueHandler SetMinValueEvent;

        event SetPositionHandler SetPositionEvent;
    }
}