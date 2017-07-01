using System;
using System.Drawing;

namespace Yutai.ArcGIS.Common.Excel
{
    public class MultiHeader : Header
    {
        private bool bool_2;
        private const int CONST_MAX_ROWS = 3;
        private float float_0;

        public MultiHeader()
        {
            this.IsDrawAllPage = true;
            base.mdrawGrid.AlignMent = AlignFlag.Center;
            base.mdrawGrid.Border = GridBorderFlag.Single;
            base.mdrawGrid.Line = GridLineFlag.Both;
            base.IsAverageColsWidth = false;
            this.bool_2 = false;
            this.float_0 = 2f;
            base.mdrawGrid.Merge = GridMergeFlag.Any;
            this.Font = new Font("宋体", 12f, FontStyle.Bold);
            base.mdrawGrid.PreferredRowHeight = this.Font.Height + 10;
        }

        public MultiHeader(int int_10, int int_11) : this()
        {
            base.Initialize(int_10, int_11);
            string str = "";
            for (int i = 0; i < int_11; i++)
            {
                str = str + "C";
            }
            base.mdrawGrid.ColsAlignString = str;
        }

        public override void Draw()
        {
            base.Draw();
            if (this.bool_2)
            {
                this.DrawDiagonalLine(this.float_0);
            }
        }

        protected void DrawDiagonalLine(float float_1)
        {
            try
            {
                int x = base.mdrawGrid.Rectangle.X;
                int y = base.mdrawGrid.Rectangle.Y;
                int num3 = x + base.mdrawGrid.ColsWidth[0];
                int num4 = y + ((int) (base.mdrawGrid.PreferredRowHeight*this.float_0));
                base.Graphics.SetClip(new Rectangle(x, y, base.mdrawGrid.ColsWidth[0],
                    base.mdrawGrid.PreferredRowHeight*base.mdrawGrid.Rows));
                base.Graphics.DrawLine(Pens.Black, x, y, num3, num4);
            }
            catch (Exception)
            {
            }
            finally
            {
                base.Graphics.ResetClip();
            }
        }

        protected override int SetMaxRows()
        {
            return 3;
        }

        public void SetMergeTextOnColSel(int int_10, int int_11, int int_12, string string_0)
        {
            base.mdrawGrid.SetTextOnColSel(int_10, int_11, int_12, string_0);
        }

        public void SetMergeTextOnRowSel(int int_10, int int_11, int int_12, string string_0)
        {
            base.mdrawGrid.SetTextOnRowSel(int_10, int_11, int_12, string_0);
        }

        public string ColsAlign
        {
            get { return base.mdrawGrid.ColsAlignString; }
            set { base.mdrawGrid.ColsAlignString = value; }
        }

        public float DiagonalLineRows
        {
            get { return this.float_0; }
            set { this.float_0 = value; }
        }

        public bool IsDrawDiagonalLine
        {
            get { return this.bool_2; }
            set { this.bool_2 = value; }
        }
    }
}