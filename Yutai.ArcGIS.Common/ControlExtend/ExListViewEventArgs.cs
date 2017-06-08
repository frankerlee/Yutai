namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class ExListViewEventArgs
    {
        private EXListViewItem exlistViewItem_0;
        private int int_0 = -1;
        private object object_0;

        public ExListViewEventArgs(EXListViewItem exlistViewItem_1, int int_1, object object_1)
        {
            this.exlistViewItem_0 = exlistViewItem_1;
            this.int_0 = int_1;
            this.object_0 = object_1;
        }

        public int ColumIndex
        {
            get
            {
                return this.int_0;
            }
        }

        public EXListViewItem EXListViewItem
        {
            get
            {
                return this.exlistViewItem_0;
            }
        }

        public object OldValue
        {
            get
            {
                return this.object_0;
            }
        }
    }
}

