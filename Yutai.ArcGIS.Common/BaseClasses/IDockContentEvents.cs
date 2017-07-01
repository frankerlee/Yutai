namespace Yutai.ArcGIS.Common.BaseClasses
{
    public interface IDockContentEvents
    {
        event DockContentVisibleChangeHander DockContentVisibleChange;

        void DockContentVisibleChanges();
    }
}