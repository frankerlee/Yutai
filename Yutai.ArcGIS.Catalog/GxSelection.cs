using System;

namespace Yutai.ArcGIS.Catalog
{
    public class GxSelection : IGxSelection, IGxSelectionEvents
    {
        private bool bool_0 = false;
        private IGxObject igxObject_0 = null;
        private IGxObjectArray igxObjectArray_0 = new GxObjectArray();

        public event OnSelectionChangedEventHandler OnSelectionChanged;

        public void Clear(object object_0)
        {
            this.igxObjectArray_0.Empty();
            if (this.OnSelectionChanged != null)
            {
                this.OnSelectionChanged(this, null);
            }
        }

        public bool IsSelected(IGxObject igxObject_1)
        {
            for (int i = 0; i < this.igxObjectArray_0.Count; i++)
            {
                if (this.igxObjectArray_0.Item(i) == igxObject_1)
                {
                    return true;
                }
            }
            return false;
        }

        public void Select(IGxObject igxObject_1, bool bool_1, object object_0)
        {
            if (!bool_1)
            {
                this.igxObjectArray_0.Empty();
            }
            this.igxObjectArray_0.Insert(-1, igxObject_1);
            if (object_0 != null)
            {
                try
                {
                    if (Convert.ToBoolean(object_0) && (this.OnSelectionChanged != null))
                    {
                        this.OnSelectionChanged(this, igxObject_1);
                    }
                }
                catch
                {
                }
            }
            else if (this.OnSelectionChanged != null)
            {
                this.OnSelectionChanged(this, igxObject_1);
            }
        }

        public void SetLocation(IGxObject igxObject_1, object object_0)
        {
            this.igxObject_0 = igxObject_1;
        }

        public void Unselect(IGxObject igxObject_1, object object_0)
        {
            for (int i = 0; i < this.igxObjectArray_0.Count; i++)
            {
                if (this.igxObjectArray_0.Item(i) == igxObject_1)
                {
                    this.igxObjectArray_0.Remove(i);
                    if (this.OnSelectionChanged != null)
                    {
                        this.OnSelectionChanged(this, igxObject_1);
                    }
                    break;
                }
            }
        }

        public int Count
        {
            get { return this.igxObjectArray_0.Count; }
        }

        public bool DelayEvents
        {
            get { return this.bool_0; }
            set { this.bool_0 = false; }
        }

        public IGxObject FirstObject
        {
            get
            {
                if (this.igxObjectArray_0.Count == 0)
                {
                    return null;
                }
                return this.igxObjectArray_0.Item(0);
            }
        }

        public IGxObject Location
        {
            get { return this.igxObject_0; }
        }

        public IEnumGxObject SelectedObjects
        {
            get { return (this.igxObjectArray_0 as IEnumGxObject); }
        }
    }
}