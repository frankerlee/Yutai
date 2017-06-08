namespace JLK.Catalog
{
    using System;

    public interface IGxObjectContainer
    {
        IGxObject AddChild(IGxObject igxObject_0);
        void DeleteChild(IGxObject igxObject_0);
        void SearchChildren(string string_0, IGxObjectArray igxObjectArray_0);

        bool AreChildrenViewable { get; }

        IEnumGxObject Children { get; }

        bool HasChildren { get; }
    }
}

