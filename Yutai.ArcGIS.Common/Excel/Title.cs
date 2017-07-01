using System.Drawing;

namespace Yutai.ArcGIS.Common.Excel
{
    internal class Title : Caption
    {
        public Title()
        {
            this.IsDrawAllPage = true;
            this.Font = new Font("黑体", 21f, FontStyle.Bold);
            base.MaxRows = 2;
        }

        public Title(string string_1) : this()
        {
            base.Text = string_1;
        }

        public override void Draw()
        {
            base.Draw();
            float width = base.Rectangle.Width;
            int num2 = base.Text.LastIndexOf("\n");
            if (num2 > 0)
            {
                string str = base.Text.Substring(num2 + 1);
                width = base.TextWidth(str);
            }
            float num3 = (((base.PrinterMargins.Width - width)/2f) + base.PrinterMargins.Left) + base.MoveX;
            float num4 = base.Rectangle.Y + base.Rectangle.Height;
            float num5 = num3 + width;
            base.Graphics.DrawLine(base.Pen, num3, num4 - 4f, num5, num4 - 4f);
            base.Graphics.DrawLine(base.Pen, num3, num4 - 2f, num5, num4 - 2f);
        }
    }
}