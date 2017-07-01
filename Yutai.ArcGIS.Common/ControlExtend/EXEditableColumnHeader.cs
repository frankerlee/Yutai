using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class EXEditableColumnHeader : EXColumnHeader
    {
        private Control control_0;

        public EXEditableColumnHeader()
        {
        }

        public EXEditableColumnHeader(string string_0)
        {
            base.Text = string_0;
        }

        public EXEditableColumnHeader(string string_0, int int_0)
        {
            base.Text = string_0;
            base.Width = int_0;
        }

        public EXEditableColumnHeader(string string_0, Control control_1)
        {
            base.Text = string_0;
            this.MyControl = control_1;
        }

        public EXEditableColumnHeader(string string_0, Control control_1, int int_0)
        {
            base.Text = string_0;
            this.MyControl = control_1;
            base.Width = int_0;
        }

        public Control MyControl
        {
            get { return this.control_0; }
            set
            {
                this.control_0 = value;
                this.control_0.Visible = false;
                this.control_0.Tag = "not_init";
            }
        }
    }
}