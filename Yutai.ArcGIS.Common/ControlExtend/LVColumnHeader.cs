using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class LVColumnHeader : ColumnHeader
    {
        private ListViewColumnStyle listViewColumnStyle_0;

        public LVColumnHeader()
        {
            this.listViewColumnStyle_0 = ListViewColumnStyle.ReadOnly;
        }

        public LVColumnHeader(ListViewColumnStyle listViewColumnStyle_1)
        {
            this.listViewColumnStyle_0 = listViewColumnStyle_1;
        }

        public ListViewColumnStyle ColumnStyle
        {
            get { return this.listViewColumnStyle_0; }
            set { this.listViewColumnStyle_0 = value; }
        }
    }
}