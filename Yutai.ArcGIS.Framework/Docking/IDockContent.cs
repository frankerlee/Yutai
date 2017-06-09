namespace Yutai.ArcGIS.Framework.Docking
{
    public interface IDockContent
    {
        DockContentHandler DockHandler { get; }
        string Name { get; }
        int Width { get; set; }
    }
}

