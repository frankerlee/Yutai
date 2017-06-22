using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtendEx
{
    [ToolboxItem(false)]
    public sealed class ImageListBox : ListBox
    {
        private Icon icon_0 = null;
        private System.Windows.Forms.ImageList imageList_0 = null;

        public ImageListBox()
        {
            Bitmap image = new Bitmap(16, 16);
            Graphics graphics = Graphics.FromImage(image);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, image.Width - 1, image.Height - 1);
            graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black)), rect);
            graphics.FillRectangle(new SolidBrush(Color.Transparent), rect);
            this.icon_0 = Icon.FromHandle(image.GetHicon());
            graphics.Dispose();
            image.Dispose();
            this.ListBoxIcon = this.icon_0;
            this.ItemHeight = 25;
            this.DrawMode = DrawMode.OwnerDrawFixed;
        }

        protected override void OnDrawItem(DrawItemEventArgs drawItemEventArgs_0)
        {
            base.OnDrawItem(drawItemEventArgs_0);
            if (((drawItemEventArgs_0.Index >= 0) && (base.Items.Count > 0)) && (drawItemEventArgs_0.Index < base.Items.Count))
            {
                drawItemEventArgs_0.DrawBackground();
                drawItemEventArgs_0.DrawFocusRectangle();
                if (((this.ImageList == null) || (this.ImageList.Images.Count == 0)) || (drawItemEventArgs_0.Index == (base.Items.Count - 1)))
                {
                    drawItemEventArgs_0.Graphics.DrawIcon(this.icon_0, new Rectangle(drawItemEventArgs_0.Bounds.X + 2, drawItemEventArgs_0.Bounds.Y + 5, this.icon_0.Width, this.icon_0.Height));
                    drawItemEventArgs_0.Graphics.DrawString(base.Items[drawItemEventArgs_0.Index].ToString(), drawItemEventArgs_0.Font, new SolidBrush(drawItemEventArgs_0.ForeColor), (float) ((drawItemEventArgs_0.Bounds.X + 16) + 3), (float) (drawItemEventArgs_0.Bounds.Y + 5));
                }
                else
                {
                    if (drawItemEventArgs_0.Index < (base.Items.Count - 1))
                    {
                        drawItemEventArgs_0.Graphics.DrawImage(this.ImageList.Images[drawItemEventArgs_0.Index], new Rectangle(drawItemEventArgs_0.Bounds.X + 2, drawItemEventArgs_0.Bounds.Y + 5, this.ImageList.ImageSize.Width, this.ImageList.ImageSize.Height));
                    }
                    else
                    {
                        drawItemEventArgs_0.Graphics.DrawIcon(this.icon_0, new Rectangle(drawItemEventArgs_0.Bounds.X + 2, drawItemEventArgs_0.Bounds.Y + 5, this.icon_0.Width, this.icon_0.Height));
                    }
                    drawItemEventArgs_0.Graphics.DrawString(base.Items[drawItemEventArgs_0.Index].ToString(), drawItemEventArgs_0.Font, new SolidBrush(drawItemEventArgs_0.ForeColor), (float) ((drawItemEventArgs_0.Bounds.X + this.ImageList.Images[drawItemEventArgs_0.Index].Width) + 3), (float) (drawItemEventArgs_0.Bounds.Y + 5));
                }
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public System.Windows.Forms.ImageList ImageList
        {
            get
            {
                return this.imageList_0;
            }
            set
            {
                this.imageList_0 = value;
                if ((this.imageList_0 == null) || (this.imageList_0.Images.Count == 0))
                {
                    base.Items.Clear();
                    base.Items.Add("(none)");
                }
                if ((this.imageList_0 != null) && (this.imageList_0.Images.Count > 0))
                {
                    base.Items.Clear();
                    this.ItemHeight = this.imageList_0.ImageSize.Height + 10;
                    for (int i = 0; i < this.imageList_0.Images.Count; i++)
                    {
                        base.Items.Add(i);
                    }
                    base.Items.Add("(none)");
                }
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Icon ListBoxIcon
        {
            get
            {
                return this.icon_0;
            }
            set
            {
                this.icon_0 = value;
            }
        }
    }
}

