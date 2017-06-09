namespace Yutai.ArcGIS.Catalog
{
    public interface IGxCatalog
    {
        void Close();
        IGxFolder ConnectFolder(string string_0);
        string ConstructFullName(IGxObject igxObject_0);
        void DisconnectFolder(string string_0);
        object GetObjectFromFullName(string string_0, out int int_0);
        void ObjectAdded(IGxObject igxObject_0);
        void ObjectChanged(IGxObject igxObject_0);
        void ObjectDeleted(IGxObject igxObject_0);
        void ObjectRefreshed(IGxObject igxObject_0);

        IGxFileFilter FileFilter { get; }

        string Location { set; }

        IGxObject SelectedObject { get; }

        IGxSelection Selection { get; }
    }
}

