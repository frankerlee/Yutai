using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Common
{
    internal class Maps : System.IDisposable, IMaps
    {
        private System.Collections.ArrayList arrayList_0 = null;

        public int Count
        {
            get { return this.arrayList_0.Count; }
        }

        public IMap get_Item(int Index)
        {
            if (Index > this.arrayList_0.Count || Index < 0)
            {
                throw new System.Exception("Maps::get_Item:\r\nIndex is out of range!");
            }
            return this.arrayList_0[Index] as IMap;
        }


        public Maps()
        {
            this.arrayList_0 = new System.Collections.ArrayList();
        }

        public void Dispose()
        {
            if (this.arrayList_0 != null)
            {
                this.arrayList_0.Clear();
                this.arrayList_0 = null;
            }
        }

        public void RemoveAt(int int_0)
        {
            if (int_0 > this.arrayList_0.Count || int_0 < 0)
            {
                throw new System.Exception("Maps::RemoveAt:\r\nIndex is out of range!");
            }
            this.arrayList_0.RemoveAt(int_0);
        }

        public void Reset()
        {
            this.arrayList_0.Clear();
        }

        public void Remove(IMap imap_0)
        {
            this.arrayList_0.Remove(imap_0);
        }

        public IMap Create()
        {
            IMap map = new Map();
            this.arrayList_0.Add(map);
            return map;
        }

        public void Add(IMap imap_0)
        {
            if (imap_0 == null)
            {
                throw new System.Exception("Maps::Add:\r\nNew Map is mot initialized!");
            }
            this.arrayList_0.Add(imap_0);
        }
    }
}