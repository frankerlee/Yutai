namespace JLK.Catalog
{
    using System;

    public interface IGxFile
    {
        void Close(bool bool_0);
        void Edit();
        void New();
        void Open();
        void Save();

        string Path { get; set; }
    }
}

