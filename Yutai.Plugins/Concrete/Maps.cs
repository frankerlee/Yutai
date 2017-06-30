using System;
using System.Collections.Generic;
using ESRI.ArcGIS.Carto;

namespace Yutai.Plugins.Concrete
{
    public class Maps : IMaps, IDisposable
    {
        private IList<IMap> _mapList = null;

        #region class constructor
        public Maps()
        {
            _mapList = new List<IMap>();
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (_mapList != null)
            {
                _mapList.Clear();
                _mapList = null;
            }
        }

        #endregion

        #region IMaps Members

   
        public void RemoveAt(int Index)
        {
            if (Index > _mapList.Count || Index < 0)
                throw new Exception("Maps::RemoveAt:\r\n索引越界!");

            _mapList.RemoveAt(Index);
        }

    
        public void Reset()
        {
            _mapList.Clear();
        }

  
        public int Count
        {
            get
            {
                return _mapList.Count;
            }
        }

    
        public IMap get_Item(int Index)
        {
            if (Index > _mapList.Count || Index < 0)
                throw new Exception("Maps::get_Item:\r\n索引值越界!");

            return _mapList[Index];
        }

 
        public void Remove(IMap Map)
        {
            _mapList.Remove(Map);
        }

     
        public IMap Create()
        {
            IMap newMap = new MapClass();
            _mapList.Add(newMap);

            return newMap;
        }

   
        public void Add(IMap Map)
        {
            if (Map == null)
                throw new Exception("Maps::Add:\r\n新地图没有初始化!");

            _mapList.Add(Map);
        }

        #endregion
    }
}