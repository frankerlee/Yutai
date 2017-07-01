using System.Drawing;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class EXImageListViewItem : EXListViewItem
    {
        private Image image_0;

        public EXImageListViewItem()
        {
        }

        public EXImageListViewItem(Image image_1)
        {
            this.image_0 = image_1;
        }

        public EXImageListViewItem(string string_1)
        {
            base.Text = string_1;
        }

        public EXImageListViewItem(string string_1, Image image_1)
        {
            this.image_0 = image_1;
            base.Text = string_1;
        }

        public EXImageListViewItem(string string_1, Image image_1, string string_2)
        {
            base.Text = string_1;
            this.image_0 = image_1;
            base.MyValue = string_2;
        }

        public Image MyImage
        {
            get { return this.image_0; }
            set { this.image_0 = value; }
        }
    }
}