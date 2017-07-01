namespace Yutai.ArcGIS.Common.BaseClasses
{
    public interface IDockContent
    {
        DockingStyle DefaultDockingStyle { get; }

        string Name { get; }

        string Text { get; }

        int Width { get; }
    }
}