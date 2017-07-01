using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Catalog
{
    public interface IGxPasteTargetHelper
    {
        bool CanPaste(IName iname_0, IGxObject igxObject_0, out bool bool_0);
        bool Paste(IName iname_0, IGxObject igxObject_0, out bool bool_0);
    }
}