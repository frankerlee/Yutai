using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class EXColumnHeader : ColumnHeader
    {
        public EXColumnHeader()
        {
        }

        public EXColumnHeader(string string_0)
        {
            base.Text = string_0;
        }

        public EXColumnHeader(string string_0, int int_0)
        {
            base.Text = string_0;
            base.Width = int_0;
        }
    }
}