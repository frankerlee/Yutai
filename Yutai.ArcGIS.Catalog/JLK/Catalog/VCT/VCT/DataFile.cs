namespace JLK.Catalog.VCT.VCT
{
    using System;
    using System.Collections;

    internal class DataFile
    {
        private ArrayList arrayList_0 = new ArrayList();
        public string fileName = "";
        public long Position = 0L;

        public ArrayList Map
        {
            get
            {
                return this.arrayList_0;
            }
            set
            {
                this.arrayList_0 = value;
            }
        }
    }
}

