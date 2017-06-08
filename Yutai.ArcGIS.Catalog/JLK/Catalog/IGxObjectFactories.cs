namespace JLK.Catalog
{
    using ESRI.ArcGIS.esriSystem;
    using System;

    public interface IGxObjectFactories
    {
        void EnableGxObjectFactory(int int_0, bool bool_0);

        int Count { get; }

        IEnumGxObjectFactory EnabledGxObjectFactories { get; }

        IGxObjectFactory this[int int_0] { get; }

        UID this[int int_0] { get; }

        bool this[int int_0] { get; }
    }
}

