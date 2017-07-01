using System;
using System.Collections;

namespace Yutai.ArcGIS.Catalog.VCT.VCT
{
    internal class FeatureOIDMapCollection : ArrayList, IDisposable
    {
        private bool bool_0 = false;

        public override int Add(object object_0)
        {
            this.bool_0 = false;
            return base.Add(object_0);
        }

        public override void AddRange(ICollection icollection_0)
        {
            this.bool_0 = false;
            base.AddRange(icollection_0);
        }

        public override void Clear()
        {
            this.bool_0 = false;
            base.Clear();
        }

        public void Dispose()
        {
            foreach (object obj2 in this)
            {
                if (obj2 is FeatureOIDMap)
                {
                    (obj2 as FeatureOIDMap).Dispose();
                }
            }
            this.Clear();
        }

        public FeatureOIDMap GetIndexByOID(int int_0)
        {
            if (!this.bool_0)
            {
                this.Sort();
            }
            FeatureOIDMap map = new FeatureOIDMap(int_0);
            int num = 0;
            int num2 = this.Count - 1;
            while (num < num2)
            {
                int num3 = (num + num2)/2;
                FeatureOIDMap map2 = this[num3] as FeatureOIDMap;
                if (map2.CompareTo(map) < 0)
                {
                    num = num3 + 1;
                }
                else
                {
                    if (map2.CompareTo(map) <= 0)
                    {
                        return map2;
                    }
                    num2 = num3 - 1;
                }
            }
            return (this[num] as FeatureOIDMap);
        }

        public override void Insert(int int_0, object object_0)
        {
            this.bool_0 = false;
            base.Insert(int_0, object_0);
        }

        public override void InsertRange(int int_0, ICollection icollection_0)
        {
            this.bool_0 = false;
            base.InsertRange(int_0, icollection_0);
        }

        public override void Remove(object object_0)
        {
            this.bool_0 = false;
            base.Remove(object_0);
        }

        public override void RemoveAt(int int_0)
        {
            this.bool_0 = false;
            base.RemoveAt(int_0);
        }

        public override void RemoveRange(int int_0, int int_1)
        {
            this.bool_0 = false;
            base.RemoveRange(int_0, int_1);
        }

        public override void Sort()
        {
            this.bool_0 = true;
            base.Sort();
        }
    }
}