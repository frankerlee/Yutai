namespace JLK.Catalog
{
    using JLK.Utility;
    using System;

    public interface IGxApplication : IApplication
    {
        IGxCatalog GxCatalog { get; set; }

        IGxSelection GxSelection { get; set; }
    }
}

