using System;
using System.Collections;
using System.Drawing;

namespace Yutai.ArcGIS.Common.Excel
{
    /// <summary>
    /// 二维网格基类。
    /// 
    /// 作 者：长江支流(周方勇)
    /// Email：flygoldfish@163.com  QQ：150439795
    /// 网 址：www.webmis.com.cn
    /// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
    /// 
    /// </summary>
    public class GridBase : IGrid, IDisposable
    {
        //注意，带△的，一般改变后会联带其它数据的改变，因此特写出相应的过程 “ChangeField属性名（）”
        //如行数、列数、行高、列宽的改变会引起相应的改变

        //用ArrayList便于插入行列，因为ArrayList有Insert()方法，直接用RowHeight/PreferredColWidth初始插入的行高或列宽
        protected ArrayList _arrRowsHeight;                 //行高数组，内部用，可用Set/GetRowHeight()设置或取指定行的行高
        protected ArrayList _arrColsWidth;                  //列宽数组，内部用，可用Set/GetColWidth()设置或取指定列的列宽
        protected ArrayList _arrColsAlign;                  //列对齐数组，内部用，可用Set/GetColAlign()设置或取指定列的列宽
        protected string[,] _arrStrGrid = new string[0, 0]; //△网格数据(二维数组)，属性改变会重新设置行与列数

        //我想到一个很好的方法，解决了行列的插入、增加，还能容纳单元格文本、行高、列宽等等，这样不用重新定义数组什么的，效率大大提高
        #region 想法
        //用DataTable，将所要保留的东东以二维表的形式保存，每一个字段是一个要记载的东东

        //protected System.Data.DataTable mdtGridAttributes = new System.Data.DataTable("GridAttributes");	//记录网格属性

        /*
		 * mdataTable.Rows.Count		为网格的行数
		 * mdataTable.Columns.Count		为网格的列数
		 * mdataTable.Rows.Add()		可以新增加一行
		 * mdataTable.Rows.InsertAt()	可以插入一行
		_____________________________________________________________________________________________
			|		| 行高	| 		| 	...
		____|_______|_______|_______|________________________________________________________________
			|		|	18	|		|
--》行指针__|_______|_______|_______|________________________________________________________________
		 	|		| ...	|		| 	...
		____|_______|_______|_______|________________________________________________________________
			|		|	18	|		|
		____|_______|_______|_______|________________________________________________________________
		
		行		列		文本	图像	前景色	背景色	。。。
		0		0		0*0		无		blank	white
		0		1		0*1		无		blank	white
		...				...
		100		100		100*100	无		blank	white
		*/
        #endregion


        #region IGridBase 成员*********网格通用*********

        private Point _location = new Point(0, 0);          //网格起点坐标
        private int _Width = 300;                           //网格宽
        private int _Height = 200;                          //网格高
        private Font _font = new Font("宋体", 10);            //文本字体

        #region 网格起点及宽、高

        /// <summary>
        /// 网格起点坐标
        /// </summary>
        public Point Location
        {
            get
            {
                return this._location;
            }
            set
            {
                this._location = value;
            }
        }

        /// <summary>
        /// 获取对象的宽
        /// </summary>
        public int Width
        {
            get
            {
                return this._Width;
            }
            set
            {
                this._Width = this.GetValidIntValue(value);
            }
        }

        /// <summary>
        /// 获取对象的高
        /// </summary>
        public int Height
        {
            get
            {
                return this._Height;
            }
            set
            {
                this._Height = this.GetValidIntValue(value);
            }
        }

        /// <summary>
        /// 网格文本字体，默认字体名为宋体大小为10。改变字体，只有在重绘时才起作用
        /// </summary>
        public Font Font
        {
            get
            {
                return this._font;
            }
            set
            {
                this._font = value;
            }
        }

        #endregion

        private AlignFlag _alignFlag = AlignFlag.Left;          //△网格整体对齐方式，属性改变会改变列宽字符串列表
        private GridLineFlag _gridLineFlag = GridLineFlag.Both;     //网格线类型
        private GridMergeFlag _gridMergeFlag = GridMergeFlag.None;      //单元格合并方式
        private GridBorderFlag _gridBorderFlag = GridBorderFlag.Single; //网格边框类型

        #region 字段属性AlignMent\Line\Merge\Border

        /// <summary>
        /// 网格整体对齐方式
        /// </summary>
        public AlignFlag AlignMent
        {
            get
            {
                return this._alignFlag;
            }
            set
            {
                this._alignFlag = value;
                ChangeFieldAlignMent();
            }
        }

        /// <summary>
        /// 网格线类型
        /// </summary>
        public GridLineFlag Line
        {
            get
            {
                return this._gridLineFlag;
            }
            set
            {
                this._gridLineFlag = value;
            }
        }

        /// <summary>
        /// 单元格合并方式
        /// </summary>
        public GridMergeFlag Merge
        {
            get
            {
                return this._gridMergeFlag;
            }
            set
            {
                this._gridMergeFlag = value;
            }
        }

        /// <summary>
        /// 网格边框类型
        /// </summary>
        public GridBorderFlag Border
        {
            get
            {
                return this._gridBorderFlag;
            }
            set
            {
                this._gridBorderFlag = value;
            }
        }

        #endregion

        private int _rows = 0;                      //△行数，属性改变会重定义数组，并且会重设置行高
        private int _cols = 0;                      //△列数，属性改变会重定义数组，并会重新设置列对齐列表

        private int _fixedRows = 0;                 //固定行数，打印时也重复打印，滚动滚动条时固定
        private int _fixedCols = 0;                 //固定列数，打印时也重复打印，滚动滚动条时固定

        private int _row = 0;                       //当前行，当前选定范围起始行
        private int _col = 0;                       //当前列，当前选定范围起始列

        private int _rowSel = 0;                        //当前选定范围结束行
        private int _colSel = 0;                        //当前选定范围结束列

        //获取有效的行列数、整数，这是对属性赋值时要调用的
        private int GetValidIntValue(int val)
        {
            int mval = val;
            if (mval < 0)
            {
                mval = 0;
            }
            if (mval > int.MaxValue)
            {
                mval = int.MaxValue;
            }
            return mval;
        }

        #region 字段属性Rows\Cols\FixedRows\FixedCols\Row\Col\RowSel\ColSel（行列数、固定行列数、当前行列、选定行列数）
        /// <summary>
        /// 行数，注意改变行数会，会影响网格文本，并且会重设置行高
        /// </summary>
        public int Rows
        {
            get
            {
                return this._rows;
            }
            set
            {
                this._rows = GetValidIntValue(value);
                //改变了行字段
                this.ChangeFieldRows();
            }
        }

        /// <summary>
        /// 列数，注意改变列数，会影响网格文本
        /// </summary>
        public int Cols
        {
            get
            {
                return this._cols;
            }
            set
            {
                this._cols = GetValidIntValue(value);
                //改变了列字段
                this.ChangeFieldCols();
            }
        }

        /// <summary>
        /// 固定行数，打印时也重复打印，滚动滚动条时固定。最小为0，最大为行数。
        /// </summary>
        public int FixedRows
        {
            get
            {
                return this._fixedRows;
            }
            set
            {
                this._fixedRows = GetValidIntValue(value);
                if (this._fixedRows > this.Rows)
                {
                    this._fixedRows = this.Rows;
                }
            }
        }

        /// <summary>
        /// 固定列数，打印时也重复打印，滚动滚动条时固定。最小为0，最大为列数。
        /// </summary>
        public int FixedCols
        {
            get
            {
                return this._fixedCols;
            }
            set
            {
                this._fixedCols = GetValidIntValue(value);
                if (this._fixedCols > this.Cols)
                {
                    this._fixedCols = this.Cols;
                }
            }
        }

        /// <summary>
        /// 当前行，或当前选定范围起始行
        /// </summary>
        public int Row
        {
            get
            {
                return this._row;
            }
            set
            {
                this._row = GetValidIntValue(value);

                if (this._row >= this.Rows)
                {
                    this._row = this.Rows - 1;
                }
            }
        }

        /// <summary>
        /// 当前列，或当前选定范围起始列
        /// </summary>
        public int Col
        {
            get
            {
                return this._col;
            }
            set
            {
                this._col = GetValidIntValue(value);

                if (this._col >= this.Cols)
                {
                    this._col = this.Cols - 1;
                }
            }
        }


        /// <summary>
        /// 选定行，即当前选定范围结束行
        /// </summary>
        public int RowSel
        {
            get
            {
                return this._rowSel;
            }
            set
            {
                int mrow = GetValidIntValue(value);
                //选定行为0到最后一行
                if (mrow >= this._rows)
                {
                    mrow = this._rows - 1;
                }
                this._rowSel = mrow;
            }
        }

        /// <summary>
        /// 选定列，即当前选定范围结束列
        /// </summary>
        public int ColSel
        {
            get
            {
                return this._colSel;
            }
            set
            {
                int mcol = GetValidIntValue(value);
                //选定列为0到最后一列
                if (mcol >= this._cols)
                {
                    mcol = this._cols - 1;
                }
                this._rowSel = mcol;

            }
        }

        #endregion

        private int _rowheight = 20;                    //△行高，用于初始每行的行高。属性改变会使_arrRowsHeight重新设置
        private int _colWidth = 75;                 //△列宽，用于初始每列的列宽。属性改变会使_arrColsWidth重新设置，基本是5个汉字的宽

        #region （每）行高、列宽 及RowHeight、PreferredColWidth及返回对应的数组

        /// <summary>
        /// 获取或设置首选行高，以像素为单位
        /// </summary>
        public int PreferredRowHeight
        {
            get
            {
                return this._rowheight;
            }
            set
            {
                this._rowheight = GetValidIntValue(value);
                this.ChangeFieldPreferredRowHeight();
            }
        }

        /// <summary>
        /// 获取或设置默认的列宽，以像素为单位
        /// </summary>
        public int PreferredColWidth
        {
            get
            {

                return this._rowheight;
            }
            set
            {
                this._rowheight = GetValidIntValue(value);
                this.ChangeFieldPreferredColWidth();
            }
        }

        /// <summary>
        /// 获取指定行的行高
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int get_RowHeight(int index)
        {
            return (int)(_arrRowsHeight[index]);
        }

        /// <summary>
        /// 设置指定行的行高
        /// </summary>
        /// <param name="index"></param>
        /// <param name="rowHeight"></param>
        public void set_RowHeight(int index, int rowHeight)
        {
            int mRowHeight = GetValidIntValue(rowHeight);
            _arrRowsHeight[index] = mRowHeight;
        }

        /// <summary>
        /// 获取指定列的列宽
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int get_ColWidth(int index)
        {
            return (int)(_arrColsWidth[index]);
        }

        /// <summary>
        /// 设置指定列的列宽
        /// </summary>
        /// <param name="index"></param>
        /// <param name="colWidth"></param>
        /// <returns></returns>
        public void set_ColWidth(int index, int colWidth)
        {
            int mcolWidth = GetValidIntValue(colWidth);
            _arrColsWidth[index] = mcolWidth;
        }

        /// <summary>
        /// 设置水平列对齐方式
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public AlignFlag get_ColAlignment(int index)
        {
            return (AlignFlag)(_arrColsAlign[index]);
        }

        /// <summary>
        /// 获取水平列对齐方式
        /// </summary>
        /// <param name="index"></param>
        /// <param name="colAlignment"></param>
        public void set_ColAlignment(int index, AlignFlag colAlignment)
        {
            _arrColsAlign[index] = colAlignment;
        }


        /// <summary>
        /// 获取或设置行高数组，设置值数组长度少于行数时只赋数组指定部分，其余的行高不变。
        /// </summary>
        /// <returns></returns>
        public int[] RowsHeight
        {
            get
            {
                //定义返回的列宽数组
                int[] arr = new int[this._rows];

                int c = 0;

                for (int i = 0; i < this._rows; i++)
                {
                    arr[i] = (int)(this._arrRowsHeight[i]);
                }

                return arr;
            }
            set
            {
                int c = 0;
                int[] arr = value;

                for (int i = 0; i < this._rows && i < arr.Length; i++)
                {
                    this._arrRowsHeight[i] = arr[i];
                }
            }
        }

        /// <summary>
        /// 获取或设置列宽数组，设置值数组长度少于列数时只赋数组指定部分，其余的列宽不变。
        /// </summary>
        /// <returns></returns>
        public int[] ColsWidth
        {
            get
            {
                //定义返回的列宽数组
                int[] arr = new int[this._cols];

                int c = 0;

                for (int i = 0; i < this._cols; i++)
                {
                    arr[i] = (int)(this._arrColsWidth[i]);
                }

                return arr;
            }
            set
            {
                int c = 0;
                int[] arr = value;

                for (int i = 0; i < this._cols && i < arr.Length; i++)
                {
                    this._arrColsWidth[i] = arr[i];
                }
            }
        }

        /// <summary>
        /// 返回列对齐数组
        /// </summary>
        /// <returns></returns>
        public AlignFlag[] ColsAlignment
        {
            get
            {

                //定义返回的列宽数组
                AlignFlag[] arr = new AlignFlag[this._cols];

                int c = 0;

                for (int i = 0; i < this._cols; i++)
                {
                    arr[i] = (AlignFlag)(this._arrColsAlign[i]);
                }

                return arr;
            }
            set
            {

                //定义返回的列宽数组
                AlignFlag[] arr = new AlignFlag[this._cols];

                int c = 0;

                for (int i = 0; i < this._cols && i < arr.Length; i++)
                {
                    this._arrColsAlign[i] = arr[i];
                }
            }
        }

        #endregion

        #region 单元格文本

        /// <summary>
        /// 获取或设置当前单元格文本
        /// </summary>		
        public string Text
        {
            get
            {
                return _arrStrGrid[this._row, this._col];
            }
            set
            {
                _arrStrGrid[this._row, this._col] = value;
            }
        }

        /// <summary>
        /// 获取指定行列单元格文本
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public string get_TextMatrix(int row, int col)
        {
            return _arrStrGrid[row, col];
        }

        /// <summary>
        /// 设置指定单元格文本
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="textMatrix"></param>
        /// <returns></returns>
        public void set_TextMatrix(int row, int col, string textMatrix)
        {
            _arrStrGrid[row, col] = textMatrix;
        }

        #endregion

        #endregion

        //Begin*****IGridBase支持函数*****
        #region 相应字段属性改变时，引发的过程调用

        //改变了行高字段，当RowHeight属性set时调用
        protected virtual void ChangeFieldPreferredRowHeight()
        {
            InitRowHeight();
        }

        //改变了行高字段，当PreferredColWidth属性set时调用
        protected virtual void ChangeFieldPreferredColWidth()
        {
            InitColWidth();
        }

        //改变了对齐方式字段，当AlignMent属性set时调用
        protected virtual void ChangeFieldAlignMent()
        {
            InitColAlignMent();
        }

        //改变了列字段，当Rows属性set时调用
        protected virtual void ChangeFieldRows()
        {
            ReDimArrString(ref _arrStrGrid, this._rows, this._cols);
            ResetRowHeight();
        }

        //改变了列字段，当Cols属性set时调用
        protected virtual void ChangeFieldCols()
        {
            ReDimArrString(ref _arrStrGrid, this._rows, this._cols);
            ResetColWidth();
        }

        #endregion

        #region 改变默认的行高、列宽、列对齐时调用 protected void InitRowHeight/InitColWidth()/InitColAlignMent()

        /// <summary>
        /// 初始设置行高
        /// </summary>
        protected void InitRowHeight()
        {
            this._arrRowsHeight = new ArrayList();

            for (int i = 0; i < this._rows; i++)
            {
                this._arrRowsHeight.Add(this._rowheight);
            }
        }

        /// <summary>
        /// 初始设置列宽
        /// </summary>
        protected void InitColWidth()
        {
            this._arrColsWidth = new ArrayList();

            for (int i = 0; i < this._cols; i++)
            {
                this._arrColsWidth.Add(this._colWidth);
            }
        }

        /// <summary>
        /// 初始设置列对齐
        /// </summary>
        protected void InitColAlignMent()
        {
            this._arrColsAlign = new ArrayList();

            for (int i = 0; i < this._cols; i++)
            {
                this._arrColsAlign.Add(this._alignFlag);
            }
        }

        #endregion

        #region 改变行列数时被调用 protected void ResetRowHeight/ResetColWidth()

        /// <summary>
        /// 改变行列数时，重新设置行高
        /// </summary>
        protected void ResetRowHeight()
        {
            int c = 0;
            c = this._arrRowsHeight.Count - this._cols;

            //行变少，清除多余的
            if (c > 0)
            {
                c = System.Math.Abs(c);
                for (int i = 0; i < c; i++)
                {
                    this._arrRowsHeight.RemoveAt(this._arrRowsHeight.Count - 1);
                }
            }
            else if (c < 0)
            {
                //行增加，用初始行高
                c = System.Math.Abs(c);
                for (int i = 0; i < c; i++)
                {
                    this._arrRowsHeight.Add(this._rowheight);
                }
            }
        }

        /// <summary>
        /// 改变行列数时，重新设置列宽
        /// </summary>
        protected void ResetColWidth()
        {
            int c = 0;
            c = this._arrColsWidth.Count - this._cols;

            //列变少，清除多余的
            if (c > 0)
            {
                c = System.Math.Abs(c);
                for (int i = 0; i < c; i++)
                {
                    this._arrColsWidth.RemoveAt(this._arrColsWidth.Count - 1);
                }
            }
            else if (c < 0)
            {
                //列增加，用初始列宽
                c = System.Math.Abs(c);
                for (int i = 0; i < c; i++)
                {
                    this._arrColsWidth.Add(this._colWidth);
                }
            }
        }

        #endregion
        //End*****IGridBase支持函数*****


        #region IDisposable 成员

        /// <summary>
        /// 释放由此对象所用的所有资源
        /// </summary>
        public virtual void Dispose()
        {
            this.Font.Dispose();
        }

        #endregion

        #region 构造函数

        public GridBase() : this(3, 4)
        {

        }

        public GridBase(int rows, int cols)
        {
            //行列改变要触发其它的改变
            this._rows = rows;
            this._cols = cols;

            Initialize(rows, cols);

        }

        #endregion

        /// <summary>
        /// 创建当前对象的浅表副本
        /// </summary>
        /// <returns></returns>
        public IGrid Clone()
        {
            return (IGrid)(base.MemberwiseClone());
        }

        /// <summary>
        /// 初始对象的行列数，注意带参构造函数自动调用此方法
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        public void Initialize(int rows, int cols)
        {
            this._rows = rows;
            this._cols = cols;

            ReDimArrString(ref _arrStrGrid, rows, cols);

            InitRowHeight();
            InitColWidth();
            InitColAlignMent();
        }

        /// <summary>
        /// 返回对象所有列宽和
        /// </summary>
        public int GetAllColsWidthSum()
        {
            #region 实现...
            int mwidth = 0;
            for (int i = 0; i < this.Cols; i++)
            {
                mwidth += (int)_arrColsWidth[i];
            }
            return mwidth;
            #endregion
        }

        /// <summary>
        /// 返回对象所有行高和
        /// </summary>
        public int GetAllRowsHeightSum()
        {
            #region 实现...
            int mheight = 0;
            for (int i = 0; i < this.Rows; i++)
            {
                mheight += (int)_arrRowsHeight[i];
            }
            return mheight;
            #endregion
        }


        public string[,] GridText
        {
            get
            {
                return _arrStrGrid;
            }
            set
            {
                _arrStrGrid = value;

                //重新设置行列数
                this._rows = _arrStrGrid.GetLength(0);
                this._cols = _arrStrGrid.GetLength(1);

                InitRowHeight();
                InitColWidth();
                InitColAlignMent();

            }
        }

        /// <summary>
        /// 返回网格的平均列宽数组
        /// </summary>
        /// <param name="IsAverageWidth">是否平均网格的宽为列宽</param>
        /// <returns></returns>
        public int[] GetAverageColsWidth()
        {
            #region 实现...

            //定义返回的列宽数组
            int[] arrReturn = new int[this._cols];

            int c = 0;

            //平均列宽
            int avgWidth = this._Width / this._cols;
            for (int i = 0; i < this._cols - 1; i++)
            {
                arrReturn[i] = avgWidth;
                c++;
            }
            //最后一列为剩下的值，这样做是为了平均时产生的小数和与总宽不相等
            arrReturn[arrReturn.Length - 1] = this._Width - avgWidth * c;

            return arrReturn;

            #endregion
        }


        //支持函数
        #region	重定义二维字符数组并保留原有数据 protected void ReDimArrString(string[,] arrStr,int rows,int cols)
        /// <summary>
        /// 用指定的行列重定义二维字符数组，数组将保留交集范围内的原有数据，扩充的用空串""填充
        /// </summary>
        /// <param name="arrStr">原二维字符数组</param>
        /// <param name="rows">新行数</param>
        /// <param name="cols">新列数</param>
        protected void ReDimArrString(ref string[,] arrStr, int rows, int cols)
        {
            if (arrStr == null || arrStr.Length == 0)
            {
                arrStr = new string[rows, cols];
                //用""填充
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        arrStr[i, j] = "";
                    }
                }

            }
            else
            {
                string[,] arr = new string[rows, cols];
                int mOriginalRows = arrStr.GetLength(0);        //原有的行数
                int mOriginalCols = arrStr.GetLength(1);        //原有的列数

                int mBackRows = 0;                              //需要备份原有数据的行列数，以便用原有数据初始
                int mBackcols = 0;

                //排列组合...

                #region 增加了行数，列可能变大也可能变小
                if (rows >= mOriginalRows)
                {
                    //列变大
                    if (cols >= mOriginalCols)
                    {   //完全在原有网格范围内或完全包含原有网格，用原有数据初始
                        mBackRows = mOriginalRows;
                        mBackcols = mOriginalCols;

                        //扩大的列用""初始
                        for (int i = 0; i < mOriginalRows; i++)
                        {
                            for (int j = 0; j < cols; j++)
                            {
                                arr[i, j] = "";
                            }
                        }

                        //扩大的行用""初始
                        for (int i = mOriginalRows; i < rows; i++)
                        {
                            for (int j = 0; j < cols; j++)
                            {
                                arr[i, j] = "";
                            }
                        }

                    }

                    //列变小
                    if (cols <= mOriginalCols)
                    {
                        mBackRows = mOriginalRows;
                        mBackcols = cols;

                        //行扩的用""初始
                        for (int i = mOriginalRows; i < rows; i++)
                        {
                            for (int j = 0; j < mOriginalCols; j++)
                            {
                                arr[i, j] = "";
                            }
                        }
                    }
                }
                #endregion


                #region 减少了行数，列可能变大也可能变小
                if (rows <= mOriginalRows)
                {
                    //列变大
                    if (cols >= mOriginalCols)
                    {
                        mBackRows = rows;
                        mBackcols = mOriginalCols;

                        //行在原有范围内，但列扩大了，把扩大的用""初始
                        for (int i = 0; i < mOriginalRows; i++)
                        {
                            for (int j = mOriginalCols; j < cols; j++)
                            {
                                arr[i, j] = "";
                            }
                        }
                    }

                    //列变小
                    if (cols <= mOriginalCols)
                    {   //完全在原有网格范围内或完全包含原有网格，用原有数据初始
                        mBackRows = rows;
                        mBackcols = cols;
                    }
                }
                #endregion


                //用原有数据填充
                for (int i = 0; i < mBackRows; i++)
                {
                    for (int j = 0; j < mBackcols; j++)
                    {
                        arr[i, j] = arrStr[i, j];
                    }
                }

                arrStr = arr;
            }
        }
        #endregion

    }//End Class

}//End Namespace
