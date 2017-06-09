namespace Yutai.ArcGIS.Catalog
{
    public interface IGxDiskConnection
    {
        void RefreshStatus();

        bool HasCachedChildren { get; }
    }
}

