using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    internal class EXComboBox : ComboBox
    {
        private Brush brush_0 = SystemBrushes.Highlight;

        public EXComboBox()
        {
            base.DrawMode = DrawMode.OwnerDrawFixed;
            base.DrawItem += new DrawItemEventHandler(this.EXComboBox_DrawItem);
        }

        private void EXComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index != -1)
            {
                Image myImage;
                int num2;
                e.DrawBackground();
                if ((e.State & DrawItemState.Selected) != DrawItemState.None)
                {
                    e.Graphics.FillRectangle(this.brush_0, e.Bounds);
                }
                EXItem item = (EXItem) base.Items[e.Index];
                Rectangle bounds = e.Bounds;
                int x = bounds.X + 2;
                if (item.GetType() == typeof(EXImageItem))
                {
                    EXImageItem item2 = (EXImageItem) item;
                    if (item2.MyImage != null)
                    {
                        myImage = item2.MyImage;
                        num2 = ((bounds.Y + (bounds.Height / 2)) - (myImage.Height / 2)) + 1;
                        e.Graphics.DrawImage(myImage, x, num2, myImage.Width, myImage.Height);
                        x += myImage.Width + 2;
                    }
                }
                else if (item.GetType() == typeof(EXMultipleImagesItem))
                {
                    EXMultipleImagesItem item3 = (EXMultipleImagesItem) item;
                    if (item3.MyImages != null)
                    {
                        for (int i = 0; i < item3.MyImages.Count; i++)
                        {
                            myImage = (Image) item3.MyImages[i];
                            num2 = ((bounds.Y + (bounds.Height / 2)) - (myImage.Height / 2)) + 1;
                            e.Graphics.DrawImage(myImage, x, num2, myImage.Width, myImage.Height);
                            x += myImage.Width + 2;
                        }
                    }
                }
                int num4 = (bounds.Y + (bounds.Height / 2)) - (e.Font.Height / 2);
                e.Graphics.DrawString(item.Text, e.Font, new SolidBrush(e.ForeColor), (float) x, (float) num4);
                e.DrawFocusRectangle();
            }
        }

        public Brush MyHighlightBrush
        {
            get
            {
                return this.brush_0;
            }
            set
            {
                this.brush_0 = value;
            }
        }

        public class EXImageItem : EXComboBox.EXItem
        {
            private Image image_0;

            public EXImageItem()
            {
            }

            public EXImageItem(Image image_1)
            {
                this.image_0 = image_1;
            }

            public EXImageItem(string string_2)
            {
                base.Text = string_2;
            }

            public EXImageItem(Image image_1, string string_2)
            {
                this.image_0 = image_1;
                base.MyValue = string_2;
            }

            public EXImageItem(string string_2, Image image_1)
            {
                base.Text = string_2;
                this.image_0 = image_1;
            }

            public EXImageItem(string string_2, Image image_1, string string_3)
            {
                base.Text = string_2;
                this.image_0 = image_1;
                base.MyValue = string_3;
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

        public class EXItem
        {
            private string string_0;
            private string string_1;

            public EXItem()
            {
                this.string_0 = "";
                this.string_1 = "";
            }

            public EXItem(string string_2)
            {
                this.string_0 = "";
                this.string_1 = "";
                this.string_0 = string_2;
            }

            public override string ToString()
            {
                return this.string_0;
            }

            public string MyValue
            {
                get
                {
                    return this.string_1;
                }
                set
                {
                    this.string_1 = value;
                }
            }

            public string Text
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

        public class EXMultipleImagesItem : EXComboBox.EXItem
        {
            private ArrayList arrayList_0;

            public EXMultipleImagesItem()
            {
            }

            public EXMultipleImagesItem(ArrayList arrayList_1)
            {
                this.arrayList_0 = arrayList_1;
            }

            public EXMultipleImagesItem(string string_2)
            {
                base.Text = string_2;
            }

            public EXMultipleImagesItem(ArrayList arrayList_1, string string_2)
            {
                this.arrayList_0 = arrayList_1;
                base.MyValue = string_2;
            }

            public EXMultipleImagesItem(string string_2, ArrayList arrayList_1)
            {
                base.Text = string_2;
                this.arrayList_0 = arrayList_1;
            }

            public EXMultipleImagesItem(string string_2, ArrayList arrayList_1, string string_3)
            {
                base.Text = string_2;
                this.arrayList_0 = arrayList_1;
                base.MyValue = string_3;
            }

            public ArrayList MyImages
            {
                get
                {
                    return this.arrayList_0;
                }
                set
                {
                    this.arrayList_0 = value;
                }
            }
        }
    }
}

