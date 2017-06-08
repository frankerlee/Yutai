namespace JLK.Catalog
{
    using ESRI.ArcGIS.esriSystem;
    using System;
    using System.Runtime.InteropServices;

    public interface IGxPasteTargetHelper
    {
        bool CanPaste(IName iname_0, IGxObject igxObject_0, out bool bool_0);
        bool Paste(IName iname_0, IGxObject igxObject_0, out bool bool_0);
    }
}

