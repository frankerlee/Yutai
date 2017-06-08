namespace JLK.Catalog
{
    using System;

    public interface IEnumGxObjectFactory
    {
        IGxObjectFactory Next();
        void Reset();
    }
}

