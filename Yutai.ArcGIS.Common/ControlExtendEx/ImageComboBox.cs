namespace JLK.ControlExtendEx
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;

    [Designer(typeof(ControlDesigner)), ToolboxBitmap(typeof(ImageComboBox), "ComboBMP.bmp"), ToolboxItem(true)]
    public class ImageComboBox : ComboBox
    {
        private ComboEditWindow comboEditWindow_0 = new ComboEditWindow();
        private System.Windows.Forms.DrawMode drawMode_0 = System.Windows.Forms.DrawMode.OwnerDrawFixed;
        private ImageComboBoxItemCollection imageComboBoxItemCollection_0;
        private System.Windows.Forms.ImageList imageList_0 = null;
        private int int_0 = 0;
        private int int_1 = 8;
        private object[] object_0 = new object[2];
        private string string_0 = string.Empty;
        private string string_1 = string.Empty;

        public ImageComboBox()
        {
            this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.object_0[0] = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.object_0[1] = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            base.ItemHeight = 15;
            this.imageComboBoxItemCollection_0 = new ImageComboBoxItemCollection(this);
        }

        public int ComboBoxAddItem(ImageComboBoxItem imageComboBoxItem_0)
        {
            imageComboBoxItem_0.Text = (imageComboBoxItem_0.Text.Length == 0) ? (imageComboBoxItem_0.GetType().Name + base.Items.Count.ToString()) : imageComboBoxItem_0.Text;
            base.Items.Add(imageComboBoxItem_0);
            return (base.Items.Count - 1);
        }

        public void ComboBoxClear()
        {
            base.Items.Clear();
        }

        public bool ComboBoxContains(ImageComboBoxItem imageComboBoxItem_0)
        {
            return base.Items.Contains(imageComboBoxItem_0);
        }

        public ImageComboBoxItem ComboBoxGetElement(int int_2)
        {
            return (ImageComboBoxItem) base.Items[int_2];
        }

        public IEnumerator ComboBoxGetEnumerator()
        {
            return base.Items.GetEnumerator();
        }

        public int ComboBoxGetItemCount()
        {
            return base.Items.Count;
        }

        public int ComboBoxInsertItem(int int_2, ImageComboBoxItem imageComboBoxItem_0)
        {
            imageComboBoxItem_0.Text = (imageComboBoxItem_0.Text.Length == 0) ? (imageComboBoxItem_0.GetType().Name + int_2.ToString()) : imageComboBoxItem_0.Text;
            base.Items.Insert(int_2, imageComboBoxItem_0);
            return int_2;
        }

        public void ComboBoxRemoveItem(ImageComboBoxItem imageComboBoxItem_0)
        {
            base.Items.Remove(imageComboBoxItem_0);
        }

        public void ComboBoxRemoveItemAt(int int_2)
        {
            base.Items.RemoveAt(int_2);
        }

        public void ComboBoxSetElement(int int_2, object object_1)
        {
            base.Items[int_2] = object_1;
        }

        [DllImport("user32", CharSet=CharSet.Auto)]
        private static extern IntPtr FindWindowEx(IntPtr intptr_0, IntPtr intptr_1, [MarshalAs(UnmanagedType.LPTStr)] string string_2, [MarshalAs(UnmanagedType.LPTStr)] string string_3);
        private void method_0(DrawItemEventArgs drawItemEventArgs_0)
        {
            drawItemEventArgs_0.DrawFocusRectangle();
            drawItemEventArgs_0.DrawBackground();
            ImageComboBoxItem item = this.Items[drawItemEventArgs_0.Index];
            if (item.Font == null)
            {
                item.Font = this.Font;
            }
            StringFormat format = new StringFormat {
                FormatFlags = StringFormatFlags.NoClip
            };
            int num = item.IndentLevel * this.Indent;
            if (item.ImageIndex != -1)
            {
                RectangleF ef;
                Rectangle rectangle2;
                Image original = this.ImageList.Images[item.ImageIndex];
                Bitmap image = new Bitmap(original, drawItemEventArgs_0.Bounds.Height - 1, drawItemEventArgs_0.Bounds.Height - 1);
                int height = image.Height;
                int width = image.Width;
                int num4 = 1 + (item.IndentLevel * this.int_1);
                if (this.RightToLeft == RightToLeft.Yes)
                {
                    image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    format.Alignment = StringAlignment.Far;
                    ef = new RectangleF((float) (drawItemEventArgs_0.Bounds.X - num4), (float) drawItemEventArgs_0.Bounds.Y, (float) (((drawItemEventArgs_0.Bounds.Width - width) - num) - num4), (float) drawItemEventArgs_0.Bounds.Height);
                    drawItemEventArgs_0.Graphics.DrawString(item.Text, item.Font, new SolidBrush(drawItemEventArgs_0.ForeColor), ef, format);
                    rectangle2 = new Rectangle((drawItemEventArgs_0.Bounds.X + num4) + (drawItemEventArgs_0.Bounds.Width - (width + num)), drawItemEventArgs_0.Bounds.Y, width, height);
                    drawItemEventArgs_0.Graphics.DrawImage(image, rectangle2);
                }
                else
                {
                    format.Alignment = StringAlignment.Near;
                    ef = new RectangleF((float) (((drawItemEventArgs_0.Bounds.X + width) + num) + num4), (float) drawItemEventArgs_0.Bounds.Y, (float) (((drawItemEventArgs_0.Bounds.Width - width) - num) - num4), (float) drawItemEventArgs_0.Bounds.Height);
                    drawItemEventArgs_0.Graphics.DrawString(item.Text, item.Font, new SolidBrush(drawItemEventArgs_0.ForeColor), ef, format);
                    rectangle2 = new Rectangle((drawItemEventArgs_0.Bounds.X + num4) + num, drawItemEventArgs_0.Bounds.Y, width, height);
                    drawItemEventArgs_0.Graphics.DrawImage(image, rectangle2);
                }
            }
            else if (this.RightToLeft == RightToLeft.Yes)
            {
                format.Alignment = StringAlignment.Far;
                drawItemEventArgs_0.Graphics.DrawString(item.Text, item.Font, new SolidBrush(drawItemEventArgs_0.ForeColor), new RectangleF((float) drawItemEventArgs_0.Bounds.X, (float) drawItemEventArgs_0.Bounds.Y, (float) (drawItemEventArgs_0.Bounds.Width - num), (float) drawItemEventArgs_0.Bounds.Height), format);
            }
            else
            {
                format.Alignment = StringAlignment.Near;
                drawItemEventArgs_0.Graphics.DrawString(item.Text, item.Font, new SolidBrush(drawItemEventArgs_0.ForeColor), new RectangleF((float) (drawItemEventArgs_0.Bounds.X + num), (float) drawItemEventArgs_0.Bounds.Y, (float) drawItemEventArgs_0.Bounds.Width, (float) drawItemEventArgs_0.Bounds.Height), format);
            }
        }

        protected override void OnDrawItem(DrawItemEventArgs drawItemEventArgs_0)
        {
            base.OnDrawItem(drawItemEventArgs_0);
            if (((drawItemEventArgs_0.Index >= 0) && (this.Items.Count > 0)) && (drawItemEventArgs_0.Index < this.Items.Count))
            {
                this.method_0(drawItemEventArgs_0);
            }
        }

        protected override void OnHandleCreated(EventArgs eventArgs_0)
        {
            base.OnHandleCreated(eventArgs_0);
            if ((base.DropDownStyle == ComboBoxStyle.DropDown) || (base.DropDownStyle == ComboBoxStyle.Simple))
            {
                this.comboEditWindow_0.AssignTextBoxHandle(this);
            }
        }

        protected override void OnMeasureItem(MeasureItemEventArgs measureItemEventArgs_0)
        {
            base.OnMeasureItem(measureItemEventArgs_0);
            if ((base.DataSource == null) && (((measureItemEventArgs_0.Index >= 0) && (this.Items.Count > 0)) && (measureItemEventArgs_0.Index < this.Items.Count)))
            {
                Font font = (this.Items[measureItemEventArgs_0.Index].Font == null) ? this.Font : this.Items[measureItemEventArgs_0.Index].Font;
                SizeF ef = measureItemEventArgs_0.Graphics.MeasureString(this.Items[measureItemEventArgs_0.Index].Text, font);
                measureItemEventArgs_0.ItemHeight = (int) ef.Height;
                measureItemEventArgs_0.ItemWidth = (int) ef.Width;
            }
        }

        protected override void OnRightToLeftChanged(EventArgs eventArgs_0)
        {
            base.OnRightToLeftChanged(eventArgs_0);
            if ((base.DropDownStyle == ComboBoxStyle.DropDown) || (base.DropDownStyle == ComboBoxStyle.Simple))
            {
                this.comboEditWindow_0.SetMargin();
                this.comboEditWindow_0.SetImageToDraw();
            }
        }

        protected override void OnSelectedIndexChanged(EventArgs eventArgs_0)
        {
            base.OnSelectedIndexChanged(eventArgs_0);
            object obj1 = base.Items[this.SelectedIndex];
            this.comboEditWindow_0.SetImageToDraw();
            this.comboEditWindow_0.SetMargin();
        }

        protected override void OnVisibleChanged(EventArgs eventArgs_0)
        {
            base.OnVisibleChanged(eventArgs_0);
            if (this.SelectedIndex > -1)
            {
                object obj1 = base.Items[this.SelectedIndex];
                this.comboEditWindow_0.SetImageToDraw();
                this.comboEditWindow_0.SetMargin();
            }
        }

        [Editor(typeof(DropDownDrawModes), typeof(UITypeEditor)), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Browsable(true)]
        public System.Windows.Forms.DrawMode DrawMode
        {
            get
            {
                DropDownDrawModes.List = this.object_0;
                return this.drawMode_0;
            }
            set
            {
                DropDownDrawModes.List = this.object_0;
                if (value == System.Windows.Forms.DrawMode.OwnerDrawFixed)
                {
                    this.drawMode_0 = System.Windows.Forms.DrawMode.OwnerDrawFixed;
                    base.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
                }
                else
                {
                    if (value != System.Windows.Forms.DrawMode.OwnerDrawVariable)
                    {
                        throw new Exception("The JLK.ControlExtend.ImageComboBox does not support the " + value.ToString() + " mode.");
                    }
                    this.drawMode_0 = System.Windows.Forms.DrawMode.OwnerDrawVariable;
                    base.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
                }
            }
        }

        [Browsable(true), Description("The ImageList control from which the images to be displayed with the items are taken."), Category("Behavior"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public System.Windows.Forms.ImageList ImageList
        {
            get
            {
                return this.imageList_0;
            }
            set
            {
                this.imageList_0 = value;
                DropDownImages.imageList = this.imageList_0;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Browsable(true), Description("The Indentation width of an item in pixels."), Category("Behavior")]
        public int Indent
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int ItemHeight
        {
            get
            {
                return base.ItemHeight;
            }
            set
            {
                base.ItemHeight = value;
            }
        }

        [MergableProperty(false), Localizable(true), Description("The collection of items in the JLK.ControlExtend.ImageComboBox."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Editor(typeof(CollectionEditor), typeof(UITypeEditor)), Category("Behavior"), TypeConverter(typeof(ImageComboBoxItemCollection))]
        public ImageComboBoxItemCollection Items
        {
            get
            {
                this.ImageList = this.imageList_0;
                return this.imageComboBoxItemCollection_0;
            }
            set
            {
                if (base.DataSource != null)
                {
                    throw new Exception("The Items cannot be used concurrently with the DataSource.");
                }
                this.imageComboBoxItemCollection_0 = value;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Struct4
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
    }
}

