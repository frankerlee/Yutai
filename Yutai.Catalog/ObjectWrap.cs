using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Catalog
{
    public class ObjectWrap
    {
        protected object m_pobject = null;

        public object Object
        {
            get
            {
                return this.m_pobject;
            }
            set
            {
                this.m_pobject = null;
            }
        }

        public ObjectWrap(object object_0)
        {
            this.m_pobject = object_0;
        }

        public override string ToString()
        {
            string name;
            if (this.m_pobject is ILayer)
            {
                name = (this.m_pobject as ILayer).Name;
            }
            else if (this.m_pobject is IDataset)
            {
                name = (this.m_pobject as IDataset).BrowseName;
            }
            else if (this.m_pobject is ITable)
            {
                name = (this.m_pobject as IDataset).Name;
            }
            else if (!(this.m_pobject is IField))
            {
                name = (!(this.m_pobject is ISpatialReference) ? "" : (this.m_pobject as ISpatialReference).Name);
            }
            else
            {
                name = (this.m_pobject as IField).AliasName;
            }
            return name;
        }
    }
}