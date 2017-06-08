using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class EXListViewItem : ListViewItem
    {
        private string string_0;

        public EXListViewItem()
        {
        }

        public EXListViewItem(string string_1)
        {
            base.Text = string_1;
        }

        public EXListViewItem(string[] string_1) : base(string_1)
        {
        }

        public string MyValue
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}

