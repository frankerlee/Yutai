using System;
using System.Collections;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.Controls
{
    internal class Maps : IMaps, IDisposable
    {
        private ArrayList m_array = null;

        public Maps()
        {
            this.m_array = new ArrayList();
        }

        public void Add(IMap Map)
        {
            if (Map == null)
            {
                throw new Exception("Maps::Add:\r\nNew Map is mot initialized!");
            }
            this.m_array.Add(Map);
        }

        public IMap Create()
        {
            IMap map = new MapClass();
            this.m_array.Add(map);
            return map;
        }

        public void Dispose()
        {
            if (this.m_array != null)
            {
                this.m_array.Clear();
                this.m_array = null;
            }
        }

        public IMap get_Item(int Index)
        {
            if ((Index > this.m_array.Count) || (Index < 0))
            {
                throw new Exception("Maps::get_Item:\r\nIndex is out of range!");
            }
            return (this.m_array[Index] as IMap);
        }

        public void Remove(IMap Map)
        {
            this.m_array.Remove(Map);
        }

        public void RemoveAt(int Index)
        {
            if ((Index > this.m_array.Count) || (Index < 0))
            {
                throw new Exception("Maps::RemoveAt:\r\nIndex is out of range!");
            }
            this.m_array.RemoveAt(Index);
        }

        public void Reset()
        {
            this.m_array.Clear();
        }

        public int Count
        {
            get { return this.m_array.Count; }
        }
    }
}