using System;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class RowDeleteEventArgs : EventArgs
    {
        private readonly int int_0;

        public RowDeleteEventArgs(int int_1)
        {
            this.int_0 = int_1;
        }

        public int Row
        {
            get
            {
                return this.int_0;
            }
        }
    }
}

