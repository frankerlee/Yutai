namespace Yutai.ArcGIS.Catalog
{
    public interface IEnumGxObjectFactory
    {
        IGxObjectFactory Next();
        void Reset();
    }
}

