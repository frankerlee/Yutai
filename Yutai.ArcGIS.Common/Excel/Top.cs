using System.Drawing;

namespace Yutai.ArcGIS.Common.Excel
{
    public class Top : Printer
    {
        protected object _dataSource;
        protected string _text;
        protected DrawGrid mdrawGrid;

        public Top()
        {
            this.IsDrawAllPage = true;
            this._text = "";
            this.mdrawGrid = new DrawGrid();
            this.mdrawGrid.ColsAlignString = "LCR";
            this.mdrawGrid.Border = GridBorderFlag.None;
            this.mdrawGrid.Line = GridLineFlag.None;
            this.mdrawGrid.Merge = GridMergeFlag.None;
            this.Font = new Font("宋体", 11f);
            this.mdrawGrid.PreferredRowHeight = this.Font.Height;
            this.mdrawGrid.Initialize(1, 3);
        }

        public override void Draw()
        {
            base.Draw();
            this.mdrawGrid.Rectangle = new Rectangle(base.Rectangle.X + base.MoveX, base.Rectangle.Y + base.MoveY, base.Rectangle.Width, base.Rectangle.Height);
            this.mdrawGrid.Graphics = base.Graphics;
            this.mdrawGrid.Width = this.mdrawGrid.Rectangle.Width;
            this.mdrawGrid.ColsWidth = this.mdrawGrid.GetAverageColsWidth();
            this.mdrawGrid.Draw();
        }

        public virtual void SetText(string string_0)
        {
            this._text = string_0;
            this.SetText(string_0, '|');
        }

        public virtual void SetText(string string_0, char char_0)
        {
            this._text = string_0;
            string str = string_0;
            char ch = char_0;
            string[] strArray = str.Split(new char[] { ch });
            if (strArray.Length > 0)
            {
                this.mdrawGrid.SetText(0, 0, strArray[0]);
            }
            if (strArray.Length > 1)
            {
                this.mdrawGrid.SetText(0, 1, strArray[1]);
            }
            if (strArray.Length > 2)
            {
                this.mdrawGrid.SetText(0, 2, strArray[2]);
            }
        }

        public object DataSource
        {
            get
            {
                return this._dataSource;
            }
            set
            {
                if (value != null)
                {
                    if (value.GetType().ToString() == "System.String")
                    {
                        this.Text = (string) value;
                    }
                    else if (value.GetType().ToString() == "System.String[]")
                    {
                        string str = "";
                        string[] strArray = (string[]) value;
                        if (strArray.Length > 0)
                        {
                            for (int i = 0; i < strArray.Length; i++)
                            {
                                str = str + "|" + strArray[i];
                            }
                            str = str.Substring(1);
                            this.Text = str;
                        }
                        else
                        {
                            this.Text = "";
                        }
                    }
                }
                else
                {
                    this._dataSource = null;
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

        public string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
                this.SetText(this._text);
            }
        }
    }
}

