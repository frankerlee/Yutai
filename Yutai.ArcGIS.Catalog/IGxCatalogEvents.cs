namespace Yutai.ArcGIS.Catalog
{
    public interface IGxCatalogEvents
    {
        event OnObjectAddedEventHandler OnObjectAdded;

        event OnObjectChangedEventHandler OnObjectChanged;

        event OnObjectDeletedEventHandler OnObjectDeleted;

        event OnObjectRefreshedEventHandler OnObjectRefreshed;

        event OnRefreshAllEventHandler OnRefreshAll;
    }
}

