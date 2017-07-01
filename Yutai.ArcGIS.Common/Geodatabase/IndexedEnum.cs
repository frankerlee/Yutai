using System.Collections;

namespace Yutai.ArcGIS.Common.Geodatabase
{
    public class IndexedEnum : IEnumerator
    {
        public readonly int Count;

        protected int eIdx = -1;

        private readonly IObjectProvider iobjectProvider_0;

        public object Current
        {
            get { return this.iobjectProvider_0.GetObj(this.eIdx); }
        }

        internal IndexedEnum(IObjectProvider iobjectProvider_1)
        {
            this.iobjectProvider_0 = iobjectProvider_1;
        }

        public virtual bool MoveNext()
        {
            IndexedEnum indexedEnum = this;
            int num = indexedEnum.eIdx + 1;
            int num1 = num;
            indexedEnum.eIdx = num;
            return num1 < this.iobjectProvider_0.Count;
        }

        public virtual void Reset()
        {
            this.eIdx = -1;
        }
    }
}