namespace Yutai.ArcGIS.Common.BaseClasses
{
    public interface IProcessStepEvents
    {
        event SetMaxValueEventHandler SetMaxValue;

        event SetMinValueEventHandler SetMinValue;

        event SetPositionEventHandler SetPosition;

        event SetStepValueEventHandler SetStepValue;

        event StepEventHandler Step;
    }
}