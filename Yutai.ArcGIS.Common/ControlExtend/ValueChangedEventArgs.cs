using System;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class ValueChangedEventArgs : EventArgs
    {
        private readonly int int_0;
        private readonly int int_1;
        private readonly object object_0;

        public ValueChangedEventArgs(object object_1, int int_2, int int_3)
        {
            this.object_0 = object_1;
            this.int_0 = int_2;
            this.int_1 = int_3;
        }

        public int Column
        {
            get
            {
                return this.int_1;
            }
        }

        public object NewValue
        {
            get
            {
                return this.object_0;
            }
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

