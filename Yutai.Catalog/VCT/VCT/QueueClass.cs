namespace Yutai.Catalog.VCT.VCT
{
    using System;
    using System.Collections.Generic;

    internal class QueueClass
    {
        private List<string> list_0 = new List<string>();
        private uint uint_0 = 0;

        public QueueClass(uint uint_1)
        {
            this.uint_0 = uint_1;
        }

        public string Push(string string_0)
        {
            this.list_0.Insert(0, string_0);
            if (this.list_0.Count > this.uint_0)
            {
                this.list_0.Remove(this.list_0[(int) this.uint_0]);
            }
            return string_0;
        }
    }
}

