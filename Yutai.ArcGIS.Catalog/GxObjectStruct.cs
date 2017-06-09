using System.Runtime.InteropServices;

namespace Yutai.ArcGIS.Catalog
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct GxObjectStruct
    {
        public string Path;
        public string Type;
    }
}

