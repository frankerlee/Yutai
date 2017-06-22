using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class XpProgressBar : Control
    {
        private bool bool_0 = true;
        private byte byte_0 = 2;
        private byte byte_1 = 6;
        private byte byte_2 = 150;
        private const string CategoryName = "Xp ProgressBar";
        private Color color_0 = Color.FromArgb(170, 240, 170);
        private Color color_1 = Color.FromArgb(10, 150, 10);
        private Color color_2 = Color.White;
        private Color color_3 = Color.Black;
        private GradientMode gradientMode_0 = GradientMode.VerticalCenter;
        private Image image_0 = null;
        private int int_0 = 100;
        private int int_1 = 0;
        private int int_2 = 50;
        private LinearGradientBrush linearGradientBrush_0;
        private LinearGradientBrush linearGradientBrush_1;
        private Pen pen_0 = new Pen(Color.FromArgb(239, 239, 239));
        private Pen pen_1 = new Pen(Color.FromArgb(104, 104, 104));
        private Pen pen_2 = new Pen(Color.FromArgb(190, 190, 190));
        private Rectangle rectangle_0;
        private Rectangle rectangle_1;
        private Rectangle rectangle_2;
        private Rectangle rectangle_3;
        private Rectangle rectangle_4;

        protected override void Dispose(bool bool_1)
        {
            if (!base.IsDisposed)
            {
                if (this.image_0 != null)
                {
                    this.image_0.Dispose();
                }
                if (this.linearGradientBrush_0 != null)
                {
                    this.linearGradientBrush_0.Dispose();
                }
                if (this.linearGradientBrush_1 != null)
                {
                    this.linearGradientBrush_1.Dispose();
                }
                base.Dispose(bool_1);
            }
        }

        private void method_0(Graphics graphics_0, int int_3)
        {
            graphics_0.FillRectangle(this.linearGradientBrush_0, 4 + (int_3 * (this.byte_0 + this.byte_1)), this.rectangle_1.Y + 1, this.byte_1, this.rectangle_1.Height);
            graphics_0.FillRectangle(this.linearGradientBrush_1, 4 + (int_3 * (this.byte_0 + this.byte_1)), this.rectangle_2.Y + 1, (int) this.byte_1, this.rectangle_2.Height - 1);
        }

        private void method_1()
        {
            this.method_2(false);
        }

        private void method_2(bool bool_1)
        {
            if (this.image_0 != null)
            {
                this.image_0.Dispose();
                this.image_0 = null;
            }
            if (bool_1)
            {
                base.Invalidate();
            }
        }

        private void method_3()
        {
            if (this.linearGradientBrush_0 != null)
            {
                this.linearGradientBrush_0.Dispose();
                this.linearGradientBrush_0 = null;
            }
            if (this.linearGradientBrush_1 != null)
            {
                this.linearGradientBrush_1.Dispose();
                this.linearGradientBrush_1 = null;
            }
        }

        private void method_4(Graphics graphics_0, Rectangle rectangle_5)
        {
            SizeF ef = graphics_0.MeasureString(this.Text, this.Font);
            float x = rectangle_5.X + ((rectangle_5.Width - ef.Width) / 2f);
            float y = rectangle_5.Y + ((rectangle_5.Height - ef.Height) / 2f);
            if (this.bool_0)
            {
                SolidBrush brush = new SolidBrush(Color.FromArgb(this.byte_2, Color.Black));
                graphics_0.DrawString(this.Text, this.Font, brush, (float) (x + 1f), (float) (y + 1f));
                brush.Dispose();
            }
            SolidBrush brush2 = new SolidBrush(this.color_3);
            graphics_0.DrawString(this.Text, this.Font, brush2, x, y);
            brush2.Dispose();
        }

        private void method_5()
        {
            this.method_3();
            switch (this.gradientMode_0)
            {
                case GradientMode.Vertical:
                    this.rectangle_1 = new Rectangle(0, 3, this.byte_1, base.Height - 7);
                    this.linearGradientBrush_0 = new LinearGradientBrush(this.rectangle_1, this.color_0, this.color_1, LinearGradientMode.Vertical);
                    this.rectangle_2 = new Rectangle(-100, -100, 1, 1);
                    this.linearGradientBrush_1 = new LinearGradientBrush(this.rectangle_2, this.color_1, this.color_0, LinearGradientMode.Horizontal);
                    break;

                case GradientMode.VerticalCenter:
                    this.rectangle_1 = new Rectangle(0, 2, this.byte_1, (base.Height / 2) + ((int) (base.Height * 0.05)));
                    this.linearGradientBrush_0 = new LinearGradientBrush(this.rectangle_1, this.color_0, this.color_1, LinearGradientMode.Vertical);
                    this.rectangle_2 = new Rectangle(0, this.rectangle_1.Bottom - 1, this.byte_1, (base.Height - this.rectangle_1.Height) - 4);
                    this.linearGradientBrush_1 = new LinearGradientBrush(this.rectangle_2, this.color_1, this.color_0, LinearGradientMode.Vertical);
                    break;

                case GradientMode.Horizontal:
                    this.rectangle_1 = new Rectangle(0, 3, this.byte_1, base.Height - 7);
                    this.linearGradientBrush_0 = new LinearGradientBrush(base.ClientRectangle, this.color_0, this.color_1, LinearGradientMode.Horizontal);
                    this.rectangle_2 = new Rectangle(-100, -100, 1, 1);
                    this.linearGradientBrush_1 = new LinearGradientBrush(this.rectangle_2, Color.Red, Color.Red, LinearGradientMode.Horizontal);
                    break;

                case GradientMode.HorizontalCenter:
                    this.rectangle_1 = new Rectangle(0, 3, this.byte_1, base.Height - 7);
                    this.linearGradientBrush_0 = new LinearGradientBrush(base.ClientRectangle, this.color_0, this.color_1, LinearGradientMode.Horizontal);
                    this.linearGradientBrush_0.SetBlendTriangularShape(0.5f);
                    this.rectangle_2 = new Rectangle(-100, -100, 1, 1);
                    this.linearGradientBrush_1 = new LinearGradientBrush(this.rectangle_2, Color.Red, Color.Red, LinearGradientMode.Horizontal);
                    break;

                case GradientMode.Diagonal:
                    this.rectangle_1 = new Rectangle(0, 3, this.byte_1, base.Height - 7);
                    this.linearGradientBrush_0 = new LinearGradientBrush(base.ClientRectangle, this.color_0, this.color_1, LinearGradientMode.ForwardDiagonal);
                    this.rectangle_2 = new Rectangle(-100, -100, 1, 1);
                    this.linearGradientBrush_1 = new LinearGradientBrush(this.rectangle_2, Color.Red, Color.Red, LinearGradientMode.Horizontal);
                    break;

                default:
                    this.linearGradientBrush_0 = new LinearGradientBrush(this.rectangle_1, this.color_0, this.color_1, LinearGradientMode.Vertical);
                    this.linearGradientBrush_1 = new LinearGradientBrush(this.rectangle_2, this.color_1, this.color_0, LinearGradientMode.Vertical);
                    break;
            }
            this.rectangle_0 = new Rectangle(base.ClientRectangle.X + 2, base.ClientRectangle.Y + 2, base.ClientRectangle.Width - 4, base.ClientRectangle.Height - 4);
            this.rectangle_3 = new Rectangle(base.ClientRectangle.X, base.ClientRectangle.Y, base.ClientRectangle.Width - 1, base.ClientRectangle.Height - 1);
            this.rectangle_4 = new Rectangle(base.ClientRectangle.X + 1, base.ClientRectangle.Y + 1, base.ClientRectangle.Width, base.ClientRectangle.Height);
        }

        protected override void OnPaint(PaintEventArgs paintEventArgs_0)
        {
            if (!base.IsDisposed)
            {
                int num = this.byte_1 + this.byte_0;
                float num2 = (base.Width - 6) + this.byte_0;
                if (this.image_0 == null)
                {
                    num2 = (base.Width - 6) + this.byte_0;
                    int num3 = (int) (num2 / ((float) num));
                    base.Width = 6 + (num * num3);
                    this.image_0 = new Bitmap(base.Width, base.Height);
                    Graphics graphics = Graphics.FromImage(this.image_0);
                    this.method_5();
                    graphics.Clear(this.color_2);
                    if (this.BackgroundImage != null)
                    {
                        TextureBrush brush = new TextureBrush(this.BackgroundImage, WrapMode.Tile);
                        graphics.FillRectangle(brush, 0, 0, base.Width, base.Height);
                        brush.Dispose();
                    }
                    graphics.DrawRectangle(this.pen_2, this.rectangle_4);
                    graphics.DrawRectangle(this.pen_1, this.rectangle_3);
                    graphics.DrawRectangle(this.pen_0, this.rectangle_0);
                    graphics.Dispose();
                }
                Image image = new Bitmap(this.image_0);
                Graphics graphics2 = Graphics.FromImage(image);
                int num4 = (int) ((((this.int_2 - this.int_1) / ((float) (this.int_0 - this.int_1))) * num2) / ((float) num));
                for (int i = 0; i < num4; i++)
                {
                    this.method_0(graphics2, i);
                }
                if (this.Text != string.Empty)
                {
                    graphics2.TextRenderingHint = TextRenderingHint.AntiAlias;
                    this.method_4(graphics2, base.ClientRectangle);
                }
                paintEventArgs_0.Graphics.DrawImage(image, paintEventArgs_0.ClipRectangle.X, paintEventArgs_0.ClipRectangle.Y, paintEventArgs_0.ClipRectangle, GraphicsUnit.Pixel);
                image.Dispose();
                graphics2.Dispose();
            }
        }

        protected override void OnPaintBackground(PaintEventArgs paintEventArgs_0)
        {
        }

        protected override void OnSizeChanged(EventArgs eventArgs_0)
        {
            if (!base.IsDisposed)
            {
                if (base.Height < 12)
                {
                    base.Height = 12;
                }
                base.OnSizeChanged(eventArgs_0);
                this.method_2(true);
            }
        }

        [RefreshProperties(RefreshProperties.Repaint), Category("Xp ProgressBar")]
        public override Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }
            set
            {
                base.BackgroundImage = value;
                this.method_1();
            }
        }

        [Category("Xp ProgressBar"), Description("The Back Color of the Progress Bar")]
        public Color ColorBackGround
        {
            get
            {
                return this.color_2;
            }
            set
            {
                this.color_2 = value;
                this.method_2(true);
            }
        }

        [Category("Xp ProgressBar"), Description("The Border Color of the gradient in the Progress Bar")]
        public Color ColorBarBorder
        {
            get
            {
                return this.color_0;
            }
            set
            {
                this.color_0 = value;
                this.method_2(true);
            }
        }

        [Description("The Center Color of the gradient in the Progress Bar"), Category("Xp ProgressBar")]
        public Color ColorBarCenter
        {
            get
            {
                return this.color_1;
            }
            set
            {
                this.color_1 = value;
                this.method_2(true);
            }
        }

        [RefreshProperties(RefreshProperties.Repaint), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DefaultValue(false), Category("Xp ProgressBar"), Description("Set to TRUE to reset all colors like the Windows XP Progress Bar ")]
        public bool ColorsXP
        {
            get
            {
                return false;
            }
            set
            {
                this.ColorBarBorder = Color.FromArgb(170, 240, 170);
                this.ColorBarCenter = Color.FromArgb(10, 150, 10);
                this.ColorBackGround = Color.White;
            }
        }

        [Category("Xp ProgressBar"), Description("The Color of the text displayed in the Progress Bar")]
        public Color ColorText
        {
            get
            {
                return this.color_3;
            }
            set
            {
                this.color_3 = value;
                if (this.Text != string.Empty)
                {
                    base.Invalidate();
                }
            }
        }

        protected override Size DefaultSize
        {
            get
            {
                return new Size(100, 29);
            }
        }

        [Category("Xp ProgressBar"), Description("The Style of the gradient bar in Progress Bar"), DefaultValue(1)]
        public GradientMode GradientStyle
        {
            get
            {
                return this.gradientMode_0;
            }
            set
            {
                if (this.gradientMode_0 != value)
                {
                    this.gradientMode_0 = value;
                    this.method_5();
                    base.Invalidate();
                }
            }
        }

        [Category("Xp ProgressBar"), Description("The Current Position of the Progress Bar"), RefreshProperties(RefreshProperties.Repaint)]
        public int Position
        {
            get
            {
                return this.int_2;
            }
            set
            {
                if (value > this.int_0)
                {
                    this.int_2 = this.int_0;
                }
                else if (value < this.int_1)
                {
                    this.int_2 = this.int_1;
                }
                else
                {
                    this.int_2 = value;
                }
                base.Invalidate();
            }
        }

        [Category("Xp ProgressBar"), RefreshProperties(RefreshProperties.Repaint), Description("The Max Position of the Progress Bar")]
        public int PositionMax
        {
            get
            {
                return this.int_0;
            }
            set
            {
                if (value > this.int_1)
                {
                    this.int_0 = value;
                    if (this.int_2 > this.int_0)
                    {
                        this.Position = this.int_0;
                    }
                    this.method_2(true);
                }
            }
        }

        [Description("The Min Position of the Progress Bar"), RefreshProperties(RefreshProperties.Repaint), Category("Xp ProgressBar")]
        public int PositionMin
        {
            get
            {
                return this.int_1;
            }
            set
            {
                if (value < this.int_0)
                {
                    this.int_1 = value;
                    if (this.int_2 < this.int_1)
                    {
                        this.Position = this.int_1;
                    }
                    this.method_2(true);
                }
            }
        }

        [Description("The number of Pixels between two Steeps in Progress Bar"), DefaultValue((byte) 2), Category("Xp ProgressBar")]
        public byte SteepDistance
        {
            get
            {
                return this.byte_0;
            }
            set
            {
                if (value >= 0)
                {
                    this.byte_0 = value;
                    this.method_2(true);
                }
            }
        }

        [Category("Xp ProgressBar"), Description("The number of Pixels of the Steeps in Progress Bar"), DefaultValue((byte) 6)]
        public byte SteepWidth
        {
            get
            {
                return this.byte_1;
            }
            set
            {
                if (value > 0)
                {
                    this.byte_1 = value;
                    this.method_2(true);
                }
            }
        }

        [Description("The Text displayed in the Progress Bar"), DefaultValue(""), Category("Xp ProgressBar")]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                if (base.Text != value)
                {
                    base.Text = value;
                    base.Invalidate();
                }
            }
        }

        [Description("Set the Text shadow in the Progress Bar"), DefaultValue(true), Category("Xp ProgressBar")]
        public bool TextShadow
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
                base.Invalidate();
            }
        }

        [DefaultValue((byte) 150), Description("Set the Alpha Channel of the Text shadow in the Progress Bar"), Category("Xp ProgressBar")]
        public byte TextShadowAlpha
        {
            get
            {
                return this.byte_2;
            }
            set
            {
                if (this.byte_2 != value)
                {
                    this.byte_2 = value;
                    this.TextShadow = true;
                }
            }
        }
    }
}

