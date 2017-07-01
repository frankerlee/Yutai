using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Catalog
{
    public interface IGxPasteTarget
    {
        bool CanPaste(IEnumName ienumName_0, ref bool bool_0);
        bool Paste(IEnumName ienumName_0, ref bool bool_0);
    }
}