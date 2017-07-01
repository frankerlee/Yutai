using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public abstract class EXListViewSubItemAB : ListViewItem.ListViewSubItem
    {
        private string string_0;

        public EXListViewSubItemAB()
        {
            this.string_0 = "";
        }

        public EXListViewSubItemAB(string string_1)
        {
            this.string_0 = "";
            base.Text = string_1;
        }

        public abstract int DoDraw(DrawListViewSubItemEventArgs drawListViewSubItemEventArgs_0, int int_0,
            EXColumnHeader excolumnHeader_0);

        public string MyValue
        {
            get { return this.string_0; }
            set { this.string_0 = value; }
        }
    }
}