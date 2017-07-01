using System;
using System.Collections;
using System.ComponentModel;

namespace Yutai.ArcGIS.Common.ControlExtendEx
{
    public sealed class ImageComboBoxItemCollection : IList, IEnumerable, ICollection
    {
        private ImageComboBox imageComboBox_0 = null;

        public ImageComboBoxItemCollection(ImageComboBox imageComboBox_1)
        {
            this.imageComboBox_0 = imageComboBox_1;
        }

        public int Add(ImageComboBoxItem imageComboBoxItem_0)
        {
            return this.imageComboBox_0.ComboBoxInsertItem(this.Count, imageComboBoxItem_0);
        }

        public void AddRange(ImageComboBoxItem[] imageComboBoxItem_0)
        {
            IEnumerator enumerator = imageComboBoxItem_0.GetEnumerator();
            while (enumerator.MoveNext())
            {
                this.imageComboBox_0.ComboBoxInsertItem(this.Count, (ImageComboBoxItem) enumerator.Current);
            }
        }

        public void Clear()
        {
            this.imageComboBox_0.ComboBoxClear();
        }

        public bool Contains(object object_0)
        {
            throw new NotSupportedException();
        }

        public IEnumerator GetEnumerator()
        {
            return this.imageComboBox_0.ComboBoxGetEnumerator();
        }

        public int IndexOf(object object_0)
        {
            throw new NotSupportedException();
        }

        public void Insert(int int_0, ImageComboBoxItem imageComboBoxItem_0)
        {
            this.imageComboBox_0.ComboBoxInsertItem(int_0, imageComboBoxItem_0);
        }

        public void Remove(ImageComboBoxItem imageComboBoxItem_0)
        {
            throw new NotSupportedException();
        }

        public void RemoveAt(int int_0)
        {
            this.imageComboBox_0.ComboBoxRemoveItemAt(int_0);
        }

        void ICollection.CopyTo(Array array_0, int int_0)
        {
            IEnumerator enumerator = this.GetEnumerator();
            while (enumerator.MoveNext())
            {
                array_0.SetValue(enumerator.Current, int_0++);
            }
        }

        int IList.Add(object object_0)
        {
            ImageComboBoxItem item = (ImageComboBoxItem) object_0;
            return this.Add(item);
        }

        bool IList.Contains(object object_0)
        {
            throw new NotSupportedException();
        }

        int IList.IndexOf(object object_0)
        {
            throw new NotSupportedException();
        }

        void IList.Insert(int int_0, object object_0)
        {
            ImageComboBoxItem item = (ImageComboBoxItem) object_0;
            this.Insert(int_0, item);
        }

        void IList.Remove(object object_0)
        {
            throw new NotSupportedException();
        }

        void IList.RemoveAt(int int_0)
        {
            this.RemoveAt(int_0);
        }

        public int Count
        {
            get { return this.imageComboBox_0.ComboBoxGetItemCount(); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        [Browsable(false)]
        public ImageComboBoxItem this[int int_0]
        {
            get { return this.imageComboBox_0.ComboBoxGetElement(int_0); }
            set { this.imageComboBox_0.ComboBoxSetElement(int_0, value); }
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        object ICollection.SyncRoot
        {
            get { return this; }
        }

        bool IList.IsFixedSize
        {
            get { return false; }
        }

        object IList.this[int int_0]
        {
            get { return this[int_0]; }
            set { this[int_0] = (ImageComboBoxItem) value; }
        }
    }
}