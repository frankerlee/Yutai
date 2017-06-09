using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Yutai.ArcGIS.Common.Excel
{
    public class Sewing : IDisposable
    {
        private int int_0;
        private int int_1;
        private SewingDirectionFlag sewingDirectionFlag_0;

        public Sewing()
        {
            this.int_0 = 0;
            this.sewingDirectionFlag_0 = SewingDirectionFlag.Left;
            this.int_1 = 0;
        }

        public Sewing(int int_2) : this(int_2, SewingDirectionFlag.Left, 0)
        {
        }

        public Sewing(int int_2, SewingDirectionFlag sewingDirectionFlag_1) : this(int_2, sewingDirectionFlag_1, 0)
        {
        }

        public Sewing(int int_2, int int_3) : this(int_2, SewingDirectionFlag.Left, int_3)
        {
        }

        public Sewing(int int_2, SewingDirectionFlag sewingDirectionFlag_1, int int_3)
        {
            this.int_0 = int_2;
            this.sewingDirectionFlag_0 = sewingDirectionFlag_1;
            this.int_1 = int_3;
        }

        public virtual void Dispose()
        {
        }

        public void Draw(Graphics graphics_0)
        {
            int num;
            int num3;
            Rectangle rectangle;
            Font font = new Font("宋体", 8f);
            string s = "装                    订                    线";
            StringFormat format = new StringFormat {
                Alignment = StringAlignment.Center
            };
            int num2 = num = this.int_0;
            int num4 = num3 = this.int_1;
            Pen pen = new Pen(Color.Red) {
                DashStyle = DashStyle.Dot
            };
            if (this.sewingDirectionFlag_0 == SewingDirectionFlag.Left)
            {
                graphics_0.DrawLine(pen, num2, 0, num2, num4);
                format.FormatFlags = StringFormatFlags.DirectionVertical;
                int width = (int) graphics_0.MeasureString("装", font).Width;
                width /= 2;
                rectangle = new Rectangle(num2 - width, 0, num2 - width, num4);
                graphics_0.DrawString(s, font, Brushes.DodgerBlue, rectangle, format);
            }
            else if (this.sewingDirectionFlag_0 == SewingDirectionFlag.Top)
            {
                graphics_0.DrawLine(pen, 0, num, num3, num);
                int height = (int) graphics_0.MeasureString("装", font).Height;
                height /= 2;
                rectangle = new Rectangle(0, num - height, num3, num - height);
                graphics_0.DrawString(s, font, Brushes.DodgerBlue, rectangle, format);
            }
            pen.Dispose();
            font.Dispose();
            format.Dispose();
        }

        public int LineLen
        {
            get
            {
                return this.int_1;
            }
            set
            {
                this.int_1 = value;
            }
        }

        public int Margin
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

        public SewingDirectionFlag SewingDirection
        {
            get
            {
                return this.sewingDirectionFlag_0;
            }
            set
            {
                this.sewingDirectionFlag_0 = value;
            }
        }
    }
}

