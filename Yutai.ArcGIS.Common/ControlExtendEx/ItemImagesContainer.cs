using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtendEx
{
    public partial class ItemImagesContainer : Form
    {
        private Container container_0 = null;
        private Image image_0 = null;
        private System.Windows.Forms.ImageList imageList_0 = null;
        private string string_0 = string.Empty;

        public event AfterSelectEventHandler AfterSelectEvent;

        public ItemImagesContainer()
        {
            this.InitializeComponent();
            this.imageListBox.Size = new Size(base.Size.Width - 2, base.Size.Height - 2);
            base.TopLevel = false;
            Bitmap image = new Bitmap(16, 16);
            Graphics graphics = Graphics.FromImage(image);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, image.Width - 1, image.Height - 1);
            graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black)), rect);
            graphics.FillRectangle(new SolidBrush(Color.White), rect);
            this.image_0 = Image.FromHbitmap(image.GetHbitmap());
            graphics.Dispose();
            image.Dispose();
            this.imageListBox.DrawMode = DrawMode.OwnerDrawFixed;
            this.imageListBox.ItemHeight = 25;
        }

 private void imageListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (((e.Index >= 0) && (this.imageListBox.Items.Count > 0)) && (e.Index < this.imageListBox.Items.Count))
            {
                e.DrawBackground();
                e.DrawFocusRectangle();
                if (((this.ImageList == null) || (this.ImageList.Images.Count == 0)) || (e.Index == (this.imageListBox.Items.Count - 1)))
                {
                    e.Graphics.DrawImage(this.image_0, new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 5, this.image_0.Width, this.image_0.Height));
                    e.Graphics.DrawString(this.imageListBox.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), (float) ((e.Bounds.X + 16) + 3), (float) (e.Bounds.Y + 5));
                }
                else
                {
                    if (e.Index < (this.imageListBox.Items.Count - 1))
                    {
                        e.Graphics.DrawImage(this.ImageList.Images[e.Index], new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 5, this.ImageList.ImageSize.Width, this.ImageList.ImageSize.Height));
                    }
                    else
                    {
                        e.Graphics.DrawImage(this.image_0, new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 5, this.image_0.Width, this.image_0.Height));
                    }
                    e.Graphics.DrawString(this.imageListBox.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), (float) ((e.Bounds.X + this.ImageList.Images[e.Index].Width) + 3), (float) (e.Bounds.Y + 5));
                }
            }
        }

        private void imageListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.imageListBox.SelectedItem.ToString().CompareTo("(none)") == 0)
            {
                this.SelectedItem = "(none)";
            }
            else
            {
                this.SelectedItem = this.imageListBox.SelectedIndex.ToString();
            }
            this.OnAfterSelectEvent(true);
        }

        private void imageListBox_SizeChanged(object sender, EventArgs e)
        {
            base.Size = new Size(this.imageListBox.Size.Width, this.imageListBox.Size.Height);
        }

 private void ItemImagesContainer_Load(object sender, EventArgs e)
        {
            this.imageListBox.SelectedIndex = -1;
        }

        protected virtual void OnAfterSelectEvent(bool bool_0)
        {
            if (this.AfterSelectEvent != null)
            {
                this.AfterSelectEvent(bool_0);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Browsable(false)]
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
                    this.imageListBox.Items.Clear();
                    this.imageListBox.Items.Add("(none)");
                }
                if ((this.imageList_0 != null) && (this.imageList_0.Images.Count > 0))
                {
                    this.imageListBox.Items.Clear();
                    this.imageListBox.ItemHeight = this.imageList_0.ImageSize.Height + 10;
                    for (int i = 0; i < this.imageList_0.Images.Count; i++)
                    {
                        this.imageListBox.Items.Add(i);
                    }
                    this.imageListBox.Items.Add("(none)");
                }
            }
        }

        public Image ListBoxIcon
        {
            get
            {
                return this.image_0;
            }
        }

        public string SelectedItem
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

        public delegate void AfterSelectEventHandler(bool bool_0);
    }
}

