namespace JLK.Catalog
{
    using ESRI.ArcGIS.esriSystem;
    using System;

    public interface IGxObject
    {
        void Attach(IGxObject igxObject_0, IGxCatalog igxCatalog_0);
        void Detach();
        void Refresh();

        string BaseName { get; }

        string Category { get; }

        UID ClassID { get; }

        string FullName { get; }

        IName InternalObjectName { get; }

        bool IsValid { get; }

        string Name { get; }

        IGxObject Parent { get; }
    }
}

