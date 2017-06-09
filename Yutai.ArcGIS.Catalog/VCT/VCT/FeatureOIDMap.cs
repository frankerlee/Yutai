using System;
using System.Collections;

namespace Yutai.ArcGIS.Catalog.VCT.VCT
{
    internal class FeatureOIDMap : IDisposable, IComparable
    {
        public int ObjectID;
        public ArrayList PointArray;

        public FeatureOIDMap(int int_0)
        {
            this.ObjectID = 0;
            this.PointArray = new ArrayList();
            this.ObjectID = int_0;
        }

        public FeatureOIDMap(int int_0, ArrayList arrayList_0) : this(int_0)
        {
            this.PointArray = arrayList_0;
        }

        public int CompareTo(object object_0)
        {
            if (!(object_0 is FeatureOIDMap))
            {
                throw new Exception("对象不是有效的类型：" + typeof(FeatureOIDMap).ToString());
            }
            FeatureOIDMap map = object_0 as FeatureOIDMap;
            return (this.ObjectID - map.ObjectID);
        }

        public void Dispose()
        {
            this.PointArray.Clear();
            this.PointArray = null;
        }
    }
}

