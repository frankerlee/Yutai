namespace JLK.Catalog
{
    using ESRI.ArcGIS.esriSystem;
    using System;

    public interface IGxPasteTarget
    {
        bool CanPaste(IEnumName ienumName_0, ref bool bool_0);
        bool Paste(IEnumName ienumName_0, ref bool bool_0);
    }
}

