using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.Utils.Paint;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Popup;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    [ToolboxItem(false)]
    internal class PopupImageComboBoxEditListBoxEx : PopupImageComboBoxEditListBox
    {
        private const int DegreeIndent = 0x10;
        private const int TextGlyphIndent = 4;

        public PopupImageComboBoxEditListBoxEx(PopupListBoxForm popupListBoxForm_0) : base(popupListBoxForm_0)
        {
            base.Paint += new PaintEventHandler(this.PopupImageComboBoxEditListBoxEx_Paint);
            base.DrawItem += new ListBoxDrawItemEventHandler(this.PopupImageComboBoxEditListBoxEx_DrawItem);
        }

        protected void DrawComboItem(ListBoxDrawItemEventArgs listBoxDrawItemEventArgs_0)
        {
            if (!this.RaiseCustomDraw(listBoxDrawItemEventArgs_0))
            {
                Brush foreBrush = null;
                ImageComboBoxItemEx item = listBoxDrawItemEventArgs_0.Item as ImageComboBoxItemEx;
                Rectangle bounds = Rectangle.Inflate(listBoxDrawItemEventArgs_0.Bounds, -2, -1);
                bounds.X += item.Degree * 0x10;
                Rectangle empty = Rectangle.Empty;
                ImageList images = this.Images as ImageList;
                if ((images != null) && !images.ImageSize.IsEmpty)
                {
                    empty.Size = images.ImageSize;
                    empty.Y = bounds.Y + (bounds.Height - empty.Size.Height);
                    empty.X = bounds.X;
                    if (this.GlyphAlignment == HorzAlignment.Near)
                    {
                        bounds.X += empty.Size.Width + 4;
                    }
                    else
                    {
                        empty.X = bounds.Right - empty.Size.Width;
                    }
                    bounds.Width -= empty.Size.Width + 4;
                    if (((item != null) && (item.ImageIndex >= 0)) && (item.ImageIndex < images.Images.Count))
                    {
                        GraphicsInfoArgs info = new GraphicsInfoArgs(new GraphicsCache(listBoxDrawItemEventArgs_0.Graphics), Rectangle.Empty);
                        XPaint.Graphics.DrawImage(info, images, item.ImageIndex, empty, true);
                    }
                }
                this.PaintAppearance.DrawString(listBoxDrawItemEventArgs_0.Cache, this.GetItemText(listBoxDrawItemEventArgs_0.Item), bounds, foreBrush);
                listBoxDrawItemEventArgs_0.Handled = true;
            }
        }

        private void PopupImageComboBoxEditListBoxEx_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            this.DrawComboItem(e);
        }

        private void PopupImageComboBoxEditListBoxEx_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}

