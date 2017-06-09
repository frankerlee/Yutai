using System.Collections;

namespace Yutai.ArcGIS.Catalog
{
    public class GxObjectArray : IEnumGxObject, IGxObjectArray
    {
        private IEnumerator ienumerator_0 = null;
        private IList ilist_0 = new ArrayList();
        private int int_0 = 0;

        public GxObjectArray()
        {
            this.ienumerator_0 = this.ilist_0.GetEnumerator();
        }

        public void Empty()
        {
            this.int_0 = 0;
            this.ilist_0.Clear();
        }

        public void Insert(int int_1, IGxObject igxObject_0)
        {
            if (this.ilist_0.Count == 0)
            {
                this.ilist_0.Add(igxObject_0);
            }
            else if (int_1 == -1)
            {
                this.ilist_0.Add(igxObject_0);
            }
            else if (int_1 > this.ilist_0.Count)
            {
                this.ilist_0.Add(igxObject_0);
            }
            else
            {
                this.ilist_0.Insert(int_1, igxObject_0);
            }
        }

        public IGxObject Item(int int_1)
        {
            if ((int_1 < 0) || (int_1 >= this.ilist_0.Count))
            {
                return null;
            }
            return (this.ilist_0[int_1] as IGxObject);
        }

        public IGxObject Next()
        {
            try
            {
                if (this.ienumerator_0.MoveNext())
                {
                    return (this.ienumerator_0.Current as IGxObject);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public void Remove(int int_1)
        {
            this.ilist_0.RemoveAt(int_1);
        }

        public void Reset()
        {
            this.ienumerator_0 = this.ilist_0.GetEnumerator();
            this.ienumerator_0.Reset();
            this.int_0 = 0;
        }

        public int Count
        {
            get
            {
                return this.ilist_0.Count;
            }
        }
    }
}

