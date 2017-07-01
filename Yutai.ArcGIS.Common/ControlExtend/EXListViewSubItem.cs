using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class EXListViewSubItem : EXListViewSubItemAB
    {
        public EXListViewSubItem()
        {
        }

        public EXListViewSubItem(string string_1)
        {
            base.Text = string_1;
        }

        public override int DoDraw(DrawListViewSubItemEventArgs drawListViewSubItemEventArgs_0, int int_0,
            EXColumnHeader excolumnHeader_0)
        {
            return int_0;
        }
    }
}