using System.Collections;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Common.Symbol
{
    public class EnumBSTRClass : IEnumBSTR, IBStringArray
    {
        private IList ilist_0 = new ArrayList();

        private int int_0 = 0;

        public int Count
        {
            get { return this.ilist_0.Count; }
        }

        public string String(int int_1)
        {
            string str;
            if ((int_1 < 0 ? false : int_1 < this.ilist_0.Count))
            {
                str = this.ilist_0[int_1].ToString();
            }
            else
            {
                str = null;
            }
            return str;
        }

        public EnumBSTRClass()
        {
        }

        public void AddString(string string_0)
        {
            this.ilist_0.Add(string_0);
        }

        public string Next()
        {
            string str;
            if (this.int_0 != this.ilist_0.Count)
            {
                EnumBSTRClass int0 = this;
                int0.int_0 = int0.int_0 + 1;
                str = this.ilist_0[this.int_0 - 1].ToString();
            }
            else
            {
                str = null;
            }
            return str;
        }

        public void RemoveAll()
        {
            this.ilist_0.Clear();
        }

        public void RemoveString(int int_1)
        {
            if ((int_1 < 0 ? false : int_1 < this.ilist_0.Count))
            {
                this.ilist_0.RemoveAt(int_1);
            }
        }

        public void Reset()
        {
            this.int_0 = 0;
        }
    }
}