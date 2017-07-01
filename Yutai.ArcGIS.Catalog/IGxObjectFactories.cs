using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Catalog
{
    public interface IGxObjectFactories
    {
        int Count { get; }

        IEnumGxObjectFactory EnabledGxObjectFactories { get; }

        bool IsEnabled { get; }

        UID GxObjectFactoryCLSID { get; }

        IGxObjectFactory GxObjectFactory { get; }

        void EnableGxObjectFactory(int int_0, bool bool_0);
    }
}