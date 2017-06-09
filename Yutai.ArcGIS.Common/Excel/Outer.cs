using System;
using System.Drawing;

namespace Yutai.ArcGIS.Common.Excel
{
    public class Outer : Printer, IDisposable
    {
        private bool bool_1 = true;
        protected bool mblnHadInitialized = false;
        protected DrawGrid mdrawGrid;

        public Outer()
        {
            this.IsDrawAllPage = false;
            this.mdrawGrid = new DrawGrid();
            this.mdrawGrid.AlignMent = AlignFlag.Left;
            this.mdrawGrid.ColsAlignString = "";
            this.mdrawGrid.Border = GridBorderFlag.None;
            this.mdrawGrid.Line = GridLineFlag.None;
            this.mdrawGrid.Merge = GridMergeFlag.None;
            this.Font = new Font("宋体", 11f);
            this.mdrawGrid.PreferredRowHeight = this.Font.Height;
            this.mdrawGrid.Initialize(0, 0);
        }

        public override void Dispose()
        {
            this.mdrawGrid.Dispose();
        }

        public override void Draw()
        {
            if (!this.mblnHadInitialized)
            {
                throw new Exception("对象的行列数还未初始，请用Initialize（）进行操作！");
            }
            base.Draw();
            this.mdrawGrid.Rectangle = new Rectangle(base.Rectangle.X + base.MoveX, base.Rectangle.Y + base.MoveY, base.Rectangle.Width, base.Rectangle.Height);
            this.mdrawGrid.Graphics = base.Graphics;
            if (this.bool_1)
            {
                this.mdrawGrid.Width = this.mdrawGrid.Rectangle.Width;
                this.mdrawGrid.ColsWidth = this.mdrawGrid.GetAverageColsWidth();
            }
            this.mdrawGrid.Draw();
        }

        public virtual string GetText(int int_9, int int_10)
        {
            return this.mdrawGrid.GetText(int_9, int_10);
        }

        public virtual void Initialize(int int_9, int int_10)
        {
            this.mblnHadInitialized = true;
            this.mdrawGrid.Initialize(int_9, int_10);
        }

        public virtual void SetText(char char_0, char char_1, string string_0)
        {
            this.mdrawGrid.SetText(char_0, char_1, string_0);
        }

        public virtual void SetText(int int_9, int int_10, string string_0)
        {
            this.mdrawGrid.SetText(int_9, int_10, string_0);
        }

        public bool CanDraw
        {
            get
            {
                return this.mblnHadInitialized;
            }
        }

        public int Cols
        {
            get
            {
                return this.mdrawGrid.Cols;
            }
        }

        public int[] ColsWidth
        {
            get
            {
                return this.mdrawGrid.ColsWidth;
            }
            set
            {
                this.mdrawGrid.ColsWidth = value;
            }
        }

        public object DataSource
        {
            get
            {
                return this.mdrawGrid.DataSource;
            }
            set
            {
                this.mdrawGrid.DataSource = value;
                if (((this.DataSource.GetType().ToString() == "System.String[]") || (this.DataSource.GetType().ToString() == "System.String[,]")) || (this.DataSource.GetType().ToString() == "System.Data.DataTable"))
                {
                    this.mblnHadInitialized = true;
                }
            }
        }

        public override int Height
        {
            get
            {
                return (this.mdrawGrid.Rows * this.mdrawGrid.PreferredRowHeight);
            }
        }

        public bool IsAverageColsWidth
        {
            get
            {
                return this.bool_1;
            }
            set
            {
                this.bool_1 = value;
            }
        }

        public int RowHeight
        {
            get
            {
                return this.mdrawGrid.PreferredRowHeight;
            }
            set
            {
                this.mdrawGrid.PreferredRowHeight = value;
            }
        }

        public int Rows
        {
            get
            {
                return this.mdrawGrid.Rows;
            }
        }

        public string[,] Text
        {
            get
            {
                return this.mdrawGrid.GridText;
            }
            set
            {
                this.mdrawGrid.GridText = value;
                this.mblnHadInitialized = true;
            }
        }
    }
}

