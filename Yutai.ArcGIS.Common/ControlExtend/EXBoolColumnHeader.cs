using System.Drawing;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class EXBoolColumnHeader : EXColumnHeader
    {
        private bool bool_0;
        private Image image_0;
        private Image image_1;

        public EXBoolColumnHeader()
        {
            this.method_0();
        }

        public EXBoolColumnHeader(string string_0)
        {
            this.method_0();
            base.Text = string_0;
        }

        public EXBoolColumnHeader(string string_0, int int_0)
        {
            this.method_0();
            base.Text = string_0;
            base.Width = int_0;
        }

        public EXBoolColumnHeader(string string_0, Image image_2, Image image_3)
        {
            this.method_0();
            base.Text = string_0;
            this.image_0 = image_2;
            this.image_1 = image_3;
        }

        public EXBoolColumnHeader(string string_0, Image image_2, Image image_3, int int_0)
        {
            this.method_0();
            base.Text = string_0;
            this.image_0 = image_2;
            this.image_1 = image_3;
            base.Width = int_0;
        }

        private void method_0()
        {
            this.bool_0 = false;
        }

        public bool Editable
        {
            get { return this.bool_0; }
            set { this.bool_0 = value; }
        }

        public Image FalseImage
        {
            get { return this.image_1; }
            set { this.image_1 = value; }
        }

        public Image TrueImage
        {
            get { return this.image_0; }
            set { this.image_0 = value; }
        }
    }
}