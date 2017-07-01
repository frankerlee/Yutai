using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class EXMultipleImagesListViewSubItem : EXListViewSubItemAB
    {
        private ArrayList arrayList_0;

        public EXMultipleImagesListViewSubItem()
        {
        }

        public EXMultipleImagesListViewSubItem(ArrayList arrayList_1)
        {
            this.arrayList_0 = arrayList_1;
        }

        public EXMultipleImagesListViewSubItem(string string_1)
        {
            base.Text = string_1;
        }

        public EXMultipleImagesListViewSubItem(ArrayList arrayList_1, string string_1)
        {
            this.arrayList_0 = arrayList_1;
            base.MyValue = string_1;
        }

        public EXMultipleImagesListViewSubItem(string string_1, ArrayList arrayList_1, string string_2)
        {
            base.Text = string_1;
            this.arrayList_0 = arrayList_1;
            base.MyValue = string_2;
        }

        public override int DoDraw(DrawListViewSubItemEventArgs drawListViewSubItemEventArgs_0, int int_0,
            EXColumnHeader excolumnHeader_0)
        {
            if ((this.MyImages != null) && (this.MyImages.Count > 0))
            {
                for (int i = 0; i < this.MyImages.Count; i++)
                {
                    Image image = (Image) this.MyImages[i];
                    int y = (drawListViewSubItemEventArgs_0.Bounds.Y + (drawListViewSubItemEventArgs_0.Bounds.Height/2)) -
                            (image.Height/2);
                    drawListViewSubItemEventArgs_0.Graphics.DrawImage(image, int_0, y, image.Width, image.Height);
                    int_0 += image.Width + 2;
                }
            }
            return int_0;
        }

        public ArrayList MyImages
        {
            get { return this.arrayList_0; }
            set { this.arrayList_0 = value; }
        }
    }
}