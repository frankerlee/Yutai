namespace JLK.Catalog
{
    using System;
    using System.Runtime.InteropServices;

    public interface IGxFileFilter
    {
        void AddFileType(string string_0, string string_1, string string_2);
        void DeleteFileType(int int_0);
        bool Filter(string string_0);
        int FindFileType(string string_0);
        void GetFileType(int int_0, out string string_0, out string string_1, out string string_2, out int int_1, out int int_2);

        int FileTypeCount { get; }
    }
}

