using System.Collections;

namespace Yutai.ArcGIS.Common.Geodatabase
{
    internal class CassLayer
    {
        public string Name = "";

        public int Type = 0;

        public string FeatureType = "";

        public bool HasClosedLine = false;

        public bool HasUnClosedLine = false;

        public IList AttributeList = new ArrayList();

        public CassLayer()
        {
        }
    }
}