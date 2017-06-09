namespace Yutai.ArcGIS.Catalog
{
    public interface IGxShortcut
    {
        IGxObject Target { get; set; }

        string TargetLocation { get; set; }
    }
}

