using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    internal class ListViewItemEx : ListViewItem
    {
        private object object_0;

        public ListViewItemEx(string[] string_0) : base(string_0)
        {
            this.object_0 = null;
        }

        public object Style
        {
            get
            {
                return this.object_0;
            }
            set
            {
                this.object_0 = value;
            }
        }
    }
}

