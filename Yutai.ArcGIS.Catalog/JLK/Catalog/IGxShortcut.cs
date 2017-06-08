namespace JLK.Catalog
{
    using System;

    public interface IGxShortcut
    {
        IGxObject Target { get; set; }

        string TargetLocation { get; set; }
    }
}

