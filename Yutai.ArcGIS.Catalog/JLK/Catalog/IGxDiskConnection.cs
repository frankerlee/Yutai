namespace JLK.Catalog
{
    using System;

    public interface IGxDiskConnection
    {
        void RefreshStatus();

        bool HasCachedChildren { get; }
    }
}

