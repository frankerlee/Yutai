using System.Collections;

namespace Yutai.ArcGIS.Common.Geodatabase
{
    public abstract class EnumImpl : IObjectProvider, IEnumerable
    {
        private readonly int int_0;

        public int Count
        {
            get { return this.int_0; }
        }

        protected EnumImpl(int int_1)
        {
            this.int_0 = int_1;
        }

        public virtual IEnumerator GetEnumerator()
        {
            return new IndexedEnum(this);
        }

        public abstract object GetObj(int int_1);
    }
}