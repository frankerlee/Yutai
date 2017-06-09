namespace Yutai.ArcGIS.Catalog
{
    public interface IGxSelection
    {
        void Clear(object object_0);
        bool IsSelected(IGxObject igxObject_0);
        void Select(IGxObject igxObject_0, bool bool_0, object object_0);
        void SetLocation(IGxObject igxObject_0, object object_0);
        void Unselect(IGxObject igxObject_0, object object_0);

        int Count { get; }

        bool DelayEvents { get; set; }

        IGxObject FirstObject { get; }

        IGxObject Location { get; }

        IEnumGxObject SelectedObjects { get; }
    }
}

