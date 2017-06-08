namespace JLK.Catalog
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct GxObjectStruct
    {
        public string Path;
        public string Type;
    }
}

