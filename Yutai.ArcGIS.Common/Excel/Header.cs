using System;

namespace Yutai.ArcGIS.Common.Excel
{
    public class Header : Outer
    {
        private const int CONST_MAX_ROWS = 10;
        private readonly int int_9;

        public Header()
        {
            this.int_9 = this.SetMaxRows();
        }

        public Header(int int_10, int int_11) : this()
        {
            this.Initialize(int_10, int_11);
        }

        public override void Initialize(int int_10, int int_11)
        {
            int num = int_10;
            if (num < 0)
            {
                num = 0;
            }
            if (num > this.int_9)
            {
                throw new Exception("行数限制在“" + this.int_9.ToString() + "”行以内！");
            }
            base.Initialize(num, int_11);
        }

        protected virtual int SetMaxRows()
        {
            return 10;
        }
    }
}