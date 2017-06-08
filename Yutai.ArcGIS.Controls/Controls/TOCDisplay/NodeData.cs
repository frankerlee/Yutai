using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.Controls.TOCDisplay
{
    internal class NodeData
    {
        private IBasicMap m_pMap = null;
        private object m_pParent = null;
        private object m_pTag = null;

        public IBasicMap Map
        {
            get
            {
                return this.m_pMap;
            }
            set
            {
                this.m_pMap = value;
            }
        }

        public object Parent
        {
            get
            {
                return this.m_pParent;
            }
            set
            {
                this.m_pParent = value;
            }
        }

        public object Tag
        {
            get
            {
                return this.m_pTag;
            }
            set
            {
                this.m_pTag = value;
            }
        }
    }
}

