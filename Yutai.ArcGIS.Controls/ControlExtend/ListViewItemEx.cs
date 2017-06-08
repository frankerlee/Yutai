using System.Windows.Forms;

namespace Yutai.ArcGIS.Controls.ControlExtend
{
    public class ListViewItemEx : ListViewItem
    {
        private object m_Style;
        private string m_styleFilename;

        public ListViewItemEx(string[] s) : base(s)
        {
            this.m_Style = null;
            this.m_styleFilename = "";
        }

        public object Style
        {
            get
            {
                return this.m_Style;
            }
            set
            {
                this.m_Style = value;
            }
        }

        public string StyleFileName
        {
            get
            {
                return this.m_styleFilename;
            }
            set
            {
                this.m_styleFilename = value;
            }
        }
    }
}

