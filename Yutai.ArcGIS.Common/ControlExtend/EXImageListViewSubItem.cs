using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class EXImageListViewSubItem : EXListViewSubItemAB
    {
        private Image image_0;

        public EXImageListViewSubItem()
        {
        }

        public EXImageListViewSubItem(Image image_1)
        {
            this.image_0 = image_1;
        }

        public EXImageListViewSubItem(string string_1)
        {
            base.Text = string_1;
        }

        public EXImageListViewSubItem(Image image_1, string string_1)
        {
            this.image_0 = image_1;
            base.MyValue = string_1;
        }

        public EXImageListViewSubItem(string string_1, Image image_1, string string_2)
        {
            base.Text = string_1;
            this.image_0 = image_1;
            base.MyValue = string_2;
        }

        public override int DoDraw(DrawListViewSubItemEventArgs drawListViewSubItemEventArgs_0, int int_0, EXColumnHeader excolumnHeader_0)
        {
            if (this.MyImage != null)
            {
                Image myImage = this.MyImage;
                int y = (drawListViewSubItemEventArgs_0.Bounds.Y + (drawListViewSubItemEventArgs_0.Bounds.Height / 2)) - (myImage.Height / 2);
                drawListViewSubItemEventArgs_0.Graphics.DrawImage(myImage, int_0, y, myImage.Width, myImage.Height);
                int_0 += myImage.Width + 2;
            }
            return int_0;
        }

        public Image MyImage
        {
            get
            {
                return this.image_0;
            }
            set
            {
                this.image_0 = value;
            }
        }
    }
}

