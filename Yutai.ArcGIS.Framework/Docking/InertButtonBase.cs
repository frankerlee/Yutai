using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Framework.Docking
{
    internal abstract class InertButtonBase : Control
    {
        private bool m_isMouseOver = false;

        protected InertButtonBase()
        {
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (!this.IsMouseOver)
            {
                this.IsMouseOver = true;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (this.IsMouseOver)
            {
                this.IsMouseOver = false;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            bool flag = base.ClientRectangle.Contains(e.X, e.Y);
            if (this.IsMouseOver != flag)
            {
                this.IsMouseOver = flag;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.IsMouseOver && base.Enabled)
            {
                using (Pen pen = new Pen(this.ForeColor))
                {
                    e.Graphics.DrawRectangle(pen, Rectangle.Inflate(base.ClientRectangle, -1, -1));
                }
            }
            using (ImageAttributes attributes = new ImageAttributes())
            {
                ColorMap[] map = new ColorMap[2];
                map[0] = new ColorMap();
                map[0].OldColor = Color.FromArgb(0, 0, 0);
                map[0].NewColor = this.ForeColor;
                map[1] = new ColorMap();
                map[1].OldColor = this.Image.GetPixel(0, 0);
                map[1].NewColor = Color.Transparent;
                attributes.SetRemapTable(map);
                e.Graphics.DrawImage(this.Image, new Rectangle(0, 0, this.Image.Width, this.Image.Height), 0, 0, this.Image.Width, this.Image.Height, GraphicsUnit.Pixel, attributes);
            }
            base.OnPaint(e);
        }

        protected virtual void OnRefreshChanges()
        {
        }

        public void RefreshChanges()
        {
            if (!base.IsDisposed)
            {
                bool flag = base.ClientRectangle.Contains(base.PointToClient(Control.MousePosition));
                if (flag != this.IsMouseOver)
                {
                    this.IsMouseOver = flag;
                }
                this.OnRefreshChanges();
            }
        }

        protected override Size DefaultSize
        {
            get
            {
                return Resources.DockPane_Close.Size;
            }
        }

        public abstract Bitmap Image { get; }

        protected bool IsMouseOver
        {
            get
            {
                return this.m_isMouseOver;
            }
            private set
            {
                if (this.m_isMouseOver != value)
                {
                    this.m_isMouseOver = value;
                    base.Invalidate();
                }
            }
        }
    }
}

