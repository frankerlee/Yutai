using System;
using System.Drawing;

namespace Yutai.ArcGIS.Common.Excel
{
    /// <summary>
    /// 绘制网格，核心是二维数组，可以是DataGrid、MsHFlexGrid、DataTable、HtmlTable等二维表现形式的数据源。
    /// 
    /// 作 者：长江支流(周方勇)
    /// Email：flygoldfish@163.com  QQ：150439795
    /// 网 址：www.webmis.com.cn
    /// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
    /// 
    /// </summary>
    public class DrawGrid : GoldGrid, IDraw, IDisposable
    {
        //************字    段************		
        private Graphics _graphics;             //绘图表面
        private Rectangle _rectangle;           //绘制区

        //绘笔
        private Brush _brush;
        private Pen _pen;

        #region IDraw 成员
        /// <summary>
        /// 获取或设置绘图表面
        /// </summary>
        public Graphics Graphics
        {
            get
            {
                return this._graphics;
            }
            set
            {
                this._graphics = value;
            }
        }

        /// <summary>
        /// 获取或设置绘制区域
        /// </summary>
        public System.Drawing.Rectangle Rectangle
        {
            get
            {
                return _rectangle;
            }
            set
            {
                _rectangle = value;
            }
        }

        /// <summary>
        /// 画笔
        /// </summary>
        public Pen Pen
        {
            get
            {
                return _pen;
            }
            set
            {
                if (value != null)
                {
                    _pen = value;
                }
            }
        }

        /// <summary>
        /// 画刷
        /// </summary>
        public Brush Brush
        {
            get
            {
                return _brush;
            }
            set
            {
                if (value != null)
                {
                    _brush = value;
                }
            }
        }

        #endregion

        public DrawGrid()
        {
            _pen = new Pen(Color.Black);
            _brush = Brushes.Black;
        }

        public DrawGrid(int rows, int cols)
        {
            this.Initialize(rows, cols);
        }

        public override void Dispose()
        {
            base.Dispose();
            this._graphics.Dispose();
        }

        /// <summary>
        /// 绘制网格线，不包括边界线
        /// </summary>
        public void DrawGridLine()
        {
            DrawGridLine(this._graphics, this._rectangle, this.Pen, this.GridText, this.PreferredRowHeight, this.ColsWidth, this.Line, this.Border, new PointF(1.0F, 1.0F), this.Merge);
        }

        /// <summary>
        /// 绘制网格文本
        /// </summary>
        public void DrawGridText()
        {
            DrawGridText(this._graphics, this._rectangle, this.Brush, this.GridText, this.PreferredRowHeight, this.ColsWidth, this.ColsAlignString, this.Font, new PointF(1.0F, 1.0F), this.Merge);
        }

        /// <summary>
        /// 绘制边框
        /// </summary>
        public void DrawGridBorder()
        {
            DrawGridBorder(this._graphics, this._rectangle, this.Pen, this.Border);
        }

        /// <summary>
        /// 绘制网格络及文本和边框
        /// </summary>
        public void Draw()
        {
            DrawGridLine();

            /*
			//测试绘制用的时间
			System.DateTime dt1;
			dt1 = System.DateTime.Now;
			Console.WriteLine(dt1.ToString() + dt1.Millisecond.ToString());
			*/
            //______________________		
            DrawGridText();
            //______________________

            /*
			System.DateTime dt2;
			dt2 = System.DateTime.Now;
			Console.WriteLine(dt2.ToString() + dt2.Millisecond.ToString());

			TimeSpan aa = dt2 - dt1;
			double secondDef = aa.TotalSeconds;
			Console.WriteLine(secondDef.ToString());
			*/
            DrawGridBorder();

        }

        #region	绘制核心

        #region 画标准横坚网格线核心 protected void DrawGridLine(Graphics g,Rectangle p_rec,int p_rows,int p_cols,int p_rowHeight,int[] p_arrColsWidth,GridLineFlag p_gridLineFlag,GridBorderFlag p_gridBorderFlag,PointF p_scaleXY)
        /// <summary>
        /// 画网格线，标准备的横竖线交叉的线
        /// </summary>
        /// <param name="g">绘图表面</param>
        /// <param name="p_rec">绘图区</param>
        /// <param name="p_pen">绘图线的笔，可以定义颜色与线宽</param>
        /// <param name="p_rows">行数</param>
        /// <param name="p_cols">列数</param>
        /// <param name="p_rowHeight">行高</param>
        /// <param name="p_arrColsWidth">列宽</param>
        /// <param name="p_gridLineFlag">网格线类型</param>
        /// <param name="p_gridBorderFlag">边框类型</param>
        /// <param name="p_scaleXY">水平与垂直方向缩放量</param>
        /// <remarks>
        /// 作    者：周方勇
        /// 修改日期：2004-08-07
        /// </remarks>
        protected void DrawGridLine(Graphics g, Rectangle p_rec, Pen p_pen, int p_rows, int p_cols, int p_rowHeight, int[] p_arrColsWidth, GridLineFlag p_gridLineFlag, GridBorderFlag p_gridBorderFlag, PointF p_scaleXY)
        {
            //缩放矩阵，用于绘图
            Rectangle rec = new Rectangle(p_rec.X, p_rec.Y, p_rec.Width, p_rec.Height);

            //缩放程序
            this.TransGrid(g, rec, p_scaleXY);

            #region 有网格线才画
            if (p_gridLineFlag != GridLineFlag.None)
            {
                int lngRows = p_rows;   //arrStrGrid.GetLength(0);			//行数，也可由二维数组算出
                int lngCols = p_cols;   //arrStrGrid.GetLength(1);			//列数

                int lngRowIndex;        //当前行
                int lngColIndex;        //当前列

                //起止坐标
                int X1, X2, Y1, Y2;

                int lngLineLen;         //线长
                int lngLineHei;         //线高


                //计算坐标、线长、线高
                lngLineLen = rec.Width;
                lngLineHei = rec.Height;

                #region 包括横线就画
                if (p_gridLineFlag == GridLineFlag.Horizontal || p_gridLineFlag == GridLineFlag.Both)
                {
                    //******先画横线******
                    X1 = rec.X;
                    Y1 = rec.Y;
                    X2 = X1 + lngLineLen;

                    //最上边与最下边的线不画
                    for (lngRowIndex = 1; lngRowIndex < lngRows; lngRowIndex++)
                    {
                        Y1 += p_rowHeight;                      //这里可以换成行高数组

                        //Y1 += p_arrRowsWidth[lngRowIndex - 1];//这里可以换成行高数组

                        Y2 = Y1;
                        g.DrawLine(p_pen, X1, Y1, X2, Y2);
                    }
                }
                #endregion

                #region 包括竖线就画
                if (p_gridLineFlag == GridLineFlag.Vertical || p_gridLineFlag == GridLineFlag.Both)
                {
                    //******再画竖线******
                    //列宽
                    int[] mArrColWidth = new int[lngCols];
                    mArrColWidth = p_arrColsWidth;

                    //Y不变
                    X1 = rec.X;
                    Y1 = rec.Y;
                    Y2 = Y1 + lngLineHei;

                    //最左边与右边的线不画
                    for (lngColIndex = 0; lngColIndex < lngCols - 1; lngColIndex++)
                    {
                        X1 += mArrColWidth[lngColIndex];
                        X2 = X1;
                        g.DrawLine(p_pen, X1, Y1, X2, Y2);
                    }
                }
                #endregion

            }//End If
            #endregion

            //******边框******
            if (p_gridBorderFlag != GridBorderFlag.None)
            {
                this.DrawGridBorder(g, rec, p_pen, p_gridBorderFlag);
            }

            //重置，不再变换
            this.ResetTransGrid();

        }
        #endregion

        #region protected void DrawGridLine(Graphics g,Rectangle p_rec,Pen p_pen,string[,] arrStrGrid,int p_rowHeight,int[] p_arrColsWidth,GridLineFlag p_gridLineFlag,GridBorderFlag p_gridBorderFlag,PointF p_scaleXY)
        /// <summary>
        /// 画网格线，标准备的横竖线交叉的线
        /// </summary>
        /// <param name="g">绘图表面</param>
        /// <param name="p_rec">绘图区</param>
        /// <param name="p_pen">绘图线的笔，可以定义颜色与线宽</param>
        /// <param name="arrStrGrid">二维数组，对应了网格行、列数</param>
        /// <param name="p_rowHeight">行高</param>
        /// <param name="p_arrColsWidth">列宽</param>
        /// <param name="p_gridLineFlag">网格线类型</param>
        /// <param name="p_gridBorderFlag">边框类型</param>
        /// <param name="p_scaleXY">水平与垂直方向缩放量</param>
        /// <remarks>
        /// 作    者：周方勇
        /// 修改日期：2004-08-07
        /// </remarks>
        protected void DrawGridLine(Graphics g, Rectangle p_rec, Pen p_pen, string[,] arrStrGrid, int p_rowHeight, int[] p_arrColsWidth, GridLineFlag p_gridLineFlag, GridBorderFlag p_gridBorderFlag, PointF p_scaleXY)
        {
            int lngRows = arrStrGrid.GetLength(0);          //行数
            int lngCols = arrStrGrid.GetLength(1);          //列数
            DrawGridLine(g, p_rec, p_pen, lngRows, lngCols, p_rowHeight, p_arrColsWidth, p_gridLineFlag, p_gridBorderFlag, p_scaleXY);
        }
        #endregion

        #region 画合并线的核心 protected void DrawGridMergeLine(Graphics g,Rectangle p_rec,Pen p_pen,string[,] arrStrGrid,int p_rowHeight,int[] p_arrColsWidth,GridLineFlag p_gridLineFlag,GridBorderFlag p_gridBorderFlag,PointF p_scaleXY,GridMergeFlag gridMergeFlag)
        /// <summary>
        /// 画网格线，根据合并方式判断相邻单元格内容一格一格的画
        /// </summary>
        /// <param name="g">绘图表面</param>
        /// <param name="p_rec">绘图区</param>
        /// <param name="p_pen">绘图线的笔，可以定义颜色与线宽</param>
        /// <param name="arrStrGrid">二维数组</param>
        /// <param name="p_rowHeight">行高</param>
        /// <param name="p_arrColsWidth">列宽</param>
        /// <param name="p_gridLineFlag">网格线类型</param>
        /// <param name="p_gridBorderFlag">边框类型</param>
        /// <param name="p_scaleXY">水平与垂直方向缩放量</param>
        /// <param name="gridMergeFlag">网格单元格合并方式</param>
        /// <remarks>
        /// 作    者：周方勇
        /// 修改日期：2004-08-07
        /// </remarks>
        protected void DrawGridMergeLine(Graphics g, Rectangle p_rec, Pen p_pen, string[,] arrStrGrid, int p_rowHeight, int[] p_arrColsWidth, GridLineFlag p_gridLineFlag, GridBorderFlag p_gridBorderFlag, PointF p_scaleXY, GridMergeFlag gridMergeFlag)
        {
            //缩放矩阵，用于绘图
            Rectangle rec = new Rectangle(p_rec.X, p_rec.Y, p_rec.Width, p_rec.Height);

            int lngRows = arrStrGrid.GetLength(0);          //行数
            int lngCols = arrStrGrid.GetLength(1);          //列数

            //网格不合并直接画标准网格线，否则一个单元格一个单元格的画
            if (gridMergeFlag == GridMergeFlag.None)
            {
                this.DrawGridLine(g, rec, p_pen, lngRows, lngCols, p_rowHeight, p_arrColsWidth, p_gridLineFlag, p_gridBorderFlag, p_scaleXY);
                return;
            }
            else
            {
                #region 有网格线才画
                if (p_gridLineFlag != GridLineFlag.None)
                {
                    //变换
                    this.TransGrid(g, rec, p_scaleXY);

                    //起止坐标
                    int X1, X2, Y1, Y2;

                    //列宽
                    int[] mArrColWidth = new int[lngCols];
                    mArrColWidth = p_arrColsWidth;

                    #region	画单元格线

                    //边界不画
                    for (int i = 0; i < lngRows; i++)
                    {
                        X1 = rec.X;
                        Y1 = rec.Y;

                        for (int j = 0; j < lngCols; j++)
                        {
                            //－－－－－水平线－－－－－
                            X2 = X1 + mArrColWidth[j];

                            Y1 = rec.Y + p_rowHeight * i;       //****可用行高数组
                            Y2 = Y1;
                            //画第二行开始及以下的横线，当前行与上一行文本不同
                            if (i > 0)
                            {
                                //任意合并，只要相邻单元格内容不同就画线，即只要相邻单元格内容相同就合并
                                if (gridMergeFlag == GridMergeFlag.Any)
                                {
                                    //画线(条件:此列不合并 || 文本空 || 当前行与上一行文本不同)
                                    if (arrStrGrid[i, j] == "" || arrStrGrid[i, j] != arrStrGrid[i - 1, j])
                                    {
                                        g.DrawLine(p_pen, X1, Y1, X2, Y2);
                                    }
                                }
                            }

                            //－－－－－'竖线－－－－－
                            //画第二列以后的竖线，当前列与上一列比较
                            if (j > 0)
                            {
                                Y2 = Y2 + p_rowHeight;          //****可用行高数组
                                X2 = X1;
                                //任意合并，只要相邻单元格内容不同就画线，即只要相邻单元格内容相同就合并
                                if (gridMergeFlag == GridMergeFlag.Any)
                                {
                                    //画线(条件:此行不合并 || 文本空 || 当前列与上一列文本不同)
                                    if (arrStrGrid[i, j] == "" || arrStrGrid[i, j] != arrStrGrid[i, j - 1])
                                    {
                                        g.DrawLine(p_pen, X1, Y1, X2, Y2);
                                    }
                                }
                            }

                            //下一列,宽加上						
                            X1 += mArrColWidth[j];

                        }//End For 列	
                    }//End For 行					
                    #endregion

                    //******边框******
                    if (p_gridBorderFlag != GridBorderFlag.None)
                    {
                        this.DrawGridBorder(g, rec, p_pen, p_gridBorderFlag);
                    }

                    //重置，不再变换
                    this.ResetTransGrid();
                }//End If
                #endregion

            }//End If		
        }//End Function
        #endregion

        #region 标准不合并网格的文本 protected void DrawGridText(Graphics g,Rectangle p_rec,Brush p_brush,string[,] arrStrGrid,int p_rowHeight,int[] p_arrColsWidth,string alignment,Font p_font,PointF p_scaleXY)
        /// <summary>
        /// 绘制网格文本，标准的行与列单元格，无合并
        /// </summary>
        /// <param name="g">绘图表面</param>
        /// <param name="p_rec">绘图区</param>
        /// <param name="p_brush">绘图文本的画刷，可以定义颜色</param>
        /// <param name="arrStrGrid">二维字符数组（网格）</param>
        /// <param name="p_rowHeight">固定行高</param>
        /// <param name="p_arrColsWidth">列宽数组，为null时则平均列宽</param>
        /// <param name="alignment">由Left，Center，Right对齐方式第一个字母组成的串</param>
        /// <param name="p_scaleXY">指定X与Y向缩放比例值</param>
        /// <remarks>
        /// 作    者：周方勇
        /// 修改日期：2004-08-07
        /// </remarks>
        protected void DrawGridText(Graphics g, Rectangle p_rec, Brush p_brush, string[,] arrStrGrid, int p_rowHeight, int[] p_arrColsWidth, string alignment, Font p_font, PointF p_scaleXY)
        {
            try
            {
                //缩放矩阵，用于绘图
                Rectangle rec = new Rectangle(p_rec.X, p_rec.Y, p_rec.Width, p_rec.Height);

                Font font = p_font;
                if (font == null)
                {
                    font = new Font("宋体", 12.0F);
                }

                int lngRows = arrStrGrid.GetLength(0);          //行数
                int lngCols = arrStrGrid.GetLength(1);          //列数

                //列宽
                int[] mArrColWidth = new int[lngCols];
                mArrColWidth = p_arrColsWidth;

                //列对齐方式
                AlignFlag[] arrAlign;
                arrAlign = this.GetColsAlign(alignment);

                //变换
                this.TransGrid(g, rec, p_scaleXY);

                //起止坐标
                int X1, Y1, width;

                #region	画单元格文本

                StringFormat sf = new StringFormat();           //字符格式
                sf.LineAlignment = StringAlignment.Center;      //垂直居中
                sf.FormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoWrap;

                for (int i = 0; i < lngRows; i++)
                {
                    X1 = rec.X;
                    Y1 = rec.Y + p_rowHeight * i;                   //****可用行数组

                    for (int j = 0; j < lngCols; j++)
                    {
                        width = mArrColWidth[j];

                        Rectangle recCell = new Rectangle(X1, Y1, width, p_rowHeight + 4);  //实际上居中会稍微偏上，因为字体有预留边距

                        sf.Alignment = StringAlignment.Near;                //默认左对齐						

                        if (arrAlign.Length > j)
                        {
                            if (arrAlign[j] == AlignFlag.Center)
                            {
                                sf.Alignment = StringAlignment.Center;      //居中
                            }
                            else if (arrAlign[j] == AlignFlag.Right)
                            {
                                sf.Alignment = StringAlignment.Far;     //居右
                            }
                        }

                        g.DrawString(arrStrGrid[i, j], font, p_brush, recCell, sf);

                        X1 += width;

                    }//End For 列	

                }//End For 行					
                #endregion

                //重置，不再变换
                this.ResetTransGrid();

                //				font.Dispose();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            finally
            {

            }

        }//End Function
        #endregion

        #region 合并方式下的网格文本 protected void DrawGridText(Graphics g,Rectangle p_rec,Brush p_brush,string[,] arrStrGrid,int p_rowHeight,int[] p_arrColsWidth,string alignment,Font p_font,PointF p_scaleXY,GridMergeFlag gridMergeFlag)
        /// <summary>
        /// 绘制网格文本，标准的行与列单元格，无合并
        /// </summary>
        /// <param name="g">绘图表面</param>
        /// <param name="p_rec">绘图区</param>
        /// <param name="p_brush">绘图文本的画刷，可以定义颜色</param>
        /// <param name="arrStrGrid">二维字符数组（网格）</param>
        /// <param name="p_rowHeight">固定行高</param>
        /// <param name="p_arrColsWidth">列宽数组，为null时则平均列宽</param>
        /// <param name="alignment">由Left，Center，Right对齐方式第一个字母组成的串</param>
        /// <param name="p_scaleXY">指定X与Y向缩放比例值</param>
        /// <remarks>
        /// 作    者：周方勇
        /// 修改日期：2004-08-07
        /// </remarks>
        protected void DrawGridMergeText(Graphics g, Rectangle p_rec, Brush p_brush, string[,] arrStrGrid, int p_rowHeight, int[] p_arrColsWidth, string alignment, Font p_font, PointF p_scaleXY, GridMergeFlag gridMergeFlag)
        {
            if (gridMergeFlag == GridMergeFlag.None)
            {
                DrawGridText(g, p_rec, p_brush, arrStrGrid, p_rowHeight, p_arrColsWidth, alignment, p_font, p_scaleXY);
                return;
            }

            try
            {
                //缩放矩阵，用于绘图
                Rectangle rec = new Rectangle(p_rec.X, p_rec.Y, p_rec.Width, p_rec.Height);

                Font font = p_font;
                if (font == null)
                {
                    font = new Font("宋体", 12.0F);
                }

                int lngRows = arrStrGrid.GetLength(0);          //行数
                int lngCols = arrStrGrid.GetLength(1);          //列数

                //列宽
                int[] mArrColWidth = new int[lngCols];
                mArrColWidth = p_arrColsWidth;

                //列对齐方式
                AlignFlag[] arrAlign;
                arrAlign = this.GetColsAlign(alignment);

                //变换
                this.TransGrid(g, rec, p_scaleXY);

                #region	画单元格文本

                StringFormat sf = new StringFormat();           //字符格式
                sf.LineAlignment = StringAlignment.Center;      //垂直居中
                sf.FormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoWrap;

                CellRectangle cell = new CellRectangle(rec.X, rec.Y, 0, p_rowHeight);   //单元格

                for (int i = 0; i < lngRows; i++)
                {
                    for (int j = 0; j < lngCols; j++)
                    {
                        //.....
                        cell = this.GetMergeCell(new Point(rec.X, rec.Y), arrStrGrid, p_rowHeight, mArrColWidth, i, j);

                        Rectangle recCell = new Rectangle(cell.Left, cell.Top, cell.Width, cell.Height + 4);  //实际上居中会稍微偏上，因为字体有预留边距

                        sf.Alignment = StringAlignment.Near;                //默认左对齐						

                        if (arrAlign.Length > j)
                        {
                            if (arrAlign[j] == AlignFlag.Center)
                            {
                                sf.Alignment = StringAlignment.Center;      //居中
                            }
                            else if (arrAlign[j] == AlignFlag.Right)
                            {
                                sf.Alignment = StringAlignment.Far;     //居右
                            }
                        }

                        g.DrawString(arrStrGrid[i, j], font, p_brush, recCell, sf);
                    }//End For 列	

                }//End For 行					
                #endregion

                //重置，不再变换
                this.ResetTransGrid();

                //font.Dispose();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            finally
            {

            }
        }//End Function
        #endregion

        #region protected void DrawGridBorder(Graphics g,Rectangle rec,Pen p_pen,GridBorderFlag p_gridBorderFlag)
        /// <summary>
        /// 绘制网格边框
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rec"></param>
        /// <param name="p_gridBorderFlag"></param>
        protected void DrawGridBorder(Graphics g, Rectangle rec, Pen p_pen, GridBorderFlag p_gridBorderFlag)
        {
            //无边框就退出
            if (p_gridBorderFlag == GridBorderFlag.None)
            {
                return;
            }

            float penwidth = 1F;        //笔宽
            int movXY = 0;              //根据笔的粗细要相应的调整矩形

            switch (p_gridBorderFlag)
            {
                case GridBorderFlag.Single:
                    break;
                case GridBorderFlag.SingleBold:
                    penwidth = 2F;
                    break;
                case GridBorderFlag.Double:
                    //双线内边框
                    g.DrawRectangle(p_pen, rec);
                    movXY = 2;
                    break;
                case GridBorderFlag.DoubleBold:
                    //双线内边框
                    g.DrawRectangle(p_pen, rec);
                    penwidth = 2F;
                    movXY = 3;
                    break;
            }


            Pen pen = (Pen)(p_pen.Clone());
            pen.Width = penwidth;

            //g.DrawRectangle(pen,rec);			

            Rectangle recBorder = rec;
            recBorder.X = rec.X - movXY;
            recBorder.Y = rec.Y - movXY;
            recBorder.Width = rec.Width + movXY * 2;
            recBorder.Height = rec.Height + movXY * 2;
            //外边框
            g.DrawRectangle(pen, recBorder);

            pen.Dispose();
        }

        #endregion

        /// <summary>
        /// 变换网格
        /// </summary>
        /// <param name="g"></param>
        /// <param name="p_rec"></param>
        /// <param name="p_scaleXY"></param>
        private void TransGrid(Graphics g, Rectangle p_rec, PointF p_scaleXY)
        {
            //坐标平移，使绘制的缩放图相对于原矩阵
            float translateX = 0.0f;
            float translateY = 0.0f;

            //缩放变换
            if (!p_scaleXY.IsEmpty)
            {
                g.ScaleTransform(p_scaleXY.X, p_scaleXY.Y);
            }
        }

        private void ResetTransGrid()
        {
            //this.Graphics.ResetTransform();
        }
        #endregion


        #region 画合并线的核心 protected void DrawGridLine(Graphics g,Rectangle p_rec,Pen p_pen,string[,] arrStrGrid,int p_rowHeight,int[] p_arrColsWidth,GridLineFlag p_gridLineFlag,GridBorderFlag p_gridBorderFlag,PointF p_scaleXY,GridMergeFlag gridMergeFlag)
        /// <summary>
        /// 画网格线，根据合并方式判断相邻单元格内容一格一格的画
        /// </summary>
        /// <param name="g">绘图表面</param>
        /// <param name="p_rec">绘图区</param>
        /// <param name="p_pen">绘图线的笔，可以定义颜色与线宽</param>
        /// <param name="arrStrGrid">二维数组</param>
        /// <param name="p_rowHeight">行高</param>
        /// <param name="p_arrColsWidth">列宽</param>
        /// <param name="p_gridLineFlag">网格线类型</param>
        /// <param name="p_gridBorderFlag">边框类型</param>
        /// <param name="p_scaleXY">水平与垂直方向缩放量</param>
        /// <param name="gridMergeFlag">网格单元格合并方式</param>
        /// <remarks>
        /// 作    者：周方勇
        /// 修改日期：2004-08-07
        /// </remarks>
        protected void DrawGridLine(Graphics g, Rectangle p_rec, Pen p_pen, string[,] arrStrGrid, int p_rowHeight, int[] p_arrColsWidth, GridLineFlag p_gridLineFlag, GridBorderFlag p_gridBorderFlag, PointF p_scaleXY, GridMergeFlag gridMergeFlag)
        {
            //缩放矩阵，用于绘图
            Rectangle rec = new Rectangle(p_rec.X, p_rec.Y, p_rec.Width, p_rec.Height);

            int lngRows = arrStrGrid.GetLength(0);          //行数
            int lngCols = arrStrGrid.GetLength(1);          //列数

            //网格不合并直接画标准网格线，否则一个单元格一个单元格的画
            if (gridMergeFlag == GridMergeFlag.None)
            {
                this.DrawGridLine(g, rec, p_pen, lngRows, lngCols, p_rowHeight, p_arrColsWidth, p_gridLineFlag, p_gridBorderFlag, p_scaleXY);
                return;
            }
            else
            {
                #region 有网格线才画
                if (p_gridLineFlag != GridLineFlag.None)
                {
                    //变换
                    this.TransGrid(g, rec, p_scaleXY);

                    //起止坐标
                    int X1, X2, Y1, Y2;

                    //列宽
                    int[] mArrColWidth = new int[lngCols];
                    mArrColWidth = p_arrColsWidth;

                    #region	画单元格线

                    //边界不画
                    for (int i = 0; i < lngRows; i++)
                    {
                        X1 = rec.X;
                        Y1 = rec.Y;

                        for (int j = 0; j < lngCols; j++)
                        {
                            //－－－－－水平线－－－－－
                            X2 = X1 + mArrColWidth[j];

                            Y1 = rec.Y + p_rowHeight * i;       //****可用行高数组
                            Y2 = Y1;
                            //画第二行开始及以下的横线，当前行与上一行文本不同
                            if (i > 0)
                            {
                                switch (gridMergeFlag)
                                {
                                    case GridMergeFlag.None:
                                        //无合并，直接画线
                                        g.DrawLine(p_pen, X1, Y1, X2, Y2);
                                        break;
                                    case GridMergeFlag.Row:
                                        //仅行上相邻列相同就合并，即合并列
                                        //画线(条件:无条件，只要是行上进行列合并，水平线肯定画)
                                        g.DrawLine(p_pen, X1, Y1, X2, Y2);
                                        break;
                                    case GridMergeFlag.Col:
                                        //仅列上相邻行相同就合并，即合并列
                                        //画线(条件:此列不合并 || 文本空 || 当前行与上一行文本不同)
                                        if (arrStrGrid[i, j] == "" || arrStrGrid[i, j] != arrStrGrid[i - 1, j])
                                        {
                                            g.DrawLine(p_pen, X1, Y1, X2, Y2);
                                        }
                                        break;
                                    case GridMergeFlag.Any:
                                        //任意合并，只要相邻单元格内容不同就画线，即只要相邻单元格内容相同就合并
                                        //画线(条件: 文本空 || 当前行与上一行文本不同)
                                        if (arrStrGrid[i, j] == "" || arrStrGrid[i, j] != arrStrGrid[i - 1, j])
                                        {
                                            g.DrawLine(p_pen, X1, Y1, X2, Y2);
                                        }
                                        break;
                                }

                            }

                            //－－－－－'竖线－－－－－
                            //画第二列以后的竖线，当前列与上一列比较
                            if (j > 0)
                            {
                                Y2 = Y2 + p_rowHeight;          //****可用行高数组
                                X2 = X1;

                                switch (gridMergeFlag)
                                {
                                    case GridMergeFlag.None:
                                        //无合并，直接画线
                                        g.DrawLine(p_pen, X1, Y1, X2, Y2);
                                        break;
                                    case GridMergeFlag.Row:
                                        //仅行上相邻列相同就合并，即合并列
                                        //画线(条件:此行不合并 || 文本空 || 当前列与上一列文本不同)
                                        if (arrStrGrid[i, j] == "" || arrStrGrid[i, j] != arrStrGrid[i, j - 1])
                                        {
                                            g.DrawLine(p_pen, X1, Y1, X2, Y2);
                                        }
                                        break;
                                    case GridMergeFlag.Col:
                                        //仅列上相邻行相同就合并，即合并列
                                        //画线(条件:无，列竖线要画)
                                        g.DrawLine(p_pen, X1, Y1, X2, Y2);
                                        break;
                                    case GridMergeFlag.Any:
                                        //任意合并，只要相邻单元格内容不同就画线，即只要相邻单元格内容相同就合并
                                        //画线(条件: 文本空 || 当前行与上一行文本不同)
                                        if (arrStrGrid[i, j] == "" || arrStrGrid[i, j] != arrStrGrid[i, j - 1])
                                        {
                                            g.DrawLine(p_pen, X1, Y1, X2, Y2);
                                        }
                                        break;
                                }
                            }

                            //下一列,宽加上						
                            X1 += mArrColWidth[j];

                        }//End For 列	
                    }//End For 行					
                    #endregion

                    //******边框******
                    if (p_gridBorderFlag != GridBorderFlag.None)
                    {
                        this.DrawGridBorder(g, rec, p_pen, p_gridBorderFlag);
                    }

                    //重置，不再变换
                    this.ResetTransGrid();
                }//End If
                #endregion

            }//End If		
        }//End Function
        #endregion


        protected void DrawGridText(Graphics g, Rectangle p_rec, Brush p_brush, string[,] arrStrGrid, int p_rowHeight, int[] p_arrColsWidth, string alignment, Font p_font, PointF p_scaleXY, GridMergeFlag gridMergeFlag)
        {
            if (gridMergeFlag == GridMergeFlag.None)
            {
                DrawGridText(g, p_rec, p_brush, arrStrGrid, p_rowHeight, p_arrColsWidth, alignment, p_font, p_scaleXY);
                return;
            }

            try
            {
                //缩放矩阵，用于绘图
                Rectangle rec = new Rectangle(p_rec.X, p_rec.Y, p_rec.Width, p_rec.Height);

                Font font = p_font;
                if (font == null)
                {
                    font = new Font("宋体", 12.0F);
                }

                int lngRows = arrStrGrid.GetLength(0);          //行数
                int lngCols = arrStrGrid.GetLength(1);          //列数

                //列宽
                int[] mArrColWidth = new int[lngCols];
                mArrColWidth = p_arrColsWidth;

                //列对齐方式
                AlignFlag[] arrAlign;
                arrAlign = this.GetColsAlign(alignment);

                //变换
                this.TransGrid(g, rec, p_scaleXY);

                #region	画单元格文本

                StringFormat sf = new StringFormat();           //字符格式
                sf.LineAlignment = StringAlignment.Center;      //垂直居中
                sf.FormatFlags = StringFormatFlags.LineLimit;   //| StringFormatFlags.NoWrap; //可换行否

                CellRectangle cell = new CellRectangle(rec.X, rec.Y, 0, p_rowHeight);   //单元格

                for (int i = 0; i < lngRows; i++)
                {
                    for (int j = 0; j < lngCols; j++)
                    {
                        //.....
                        cell = this.GetMergeCell(new Point(rec.X, rec.Y), arrStrGrid, p_rowHeight, mArrColWidth, i, j, gridMergeFlag);

                        Rectangle recCell = new Rectangle(cell.Left, cell.Top, cell.Width, cell.Height + 4);  //实际上居中会稍微偏上，因为字体有预留边距

                        sf.Alignment = StringAlignment.Near;                //默认左对齐						

                        if (arrAlign.Length > j)
                        {
                            if (arrAlign[j] == AlignFlag.Center)
                            {
                                sf.Alignment = StringAlignment.Center;      //居中
                            }
                            else if (arrAlign[j] == AlignFlag.Right)
                            {
                                sf.Alignment = StringAlignment.Far;     //居右
                            }
                        }

                        g.DrawString(arrStrGrid[i, j], font, p_brush, recCell, sf);
                    }//End For 列	

                }//End For 行					
                #endregion

                //重置，不再变换
                this.ResetTransGrid();

                //font.Dispose();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            finally
            {

            }
        }//End Function


        #region protected virtual Cell GetMergeCell(Point GridLocation,string[,] arrStrGrid,int rowHeight,int[] ArrColWidth,int rowSel,int colSel,GridMergeFlag gridMergeFlag)

        /// <summary>
        /// 任意合并方式下返回指定单元格左顶宽高
        /// </summary>
        /// <param name="GridLocation">网格起点坐标</param>
        /// <param name="arrStrGrid">二维网格</param>
        /// <param name="rowHeight">行高</param>
        /// <param name="ArrColWidth">列宽数组</param>
        /// <param name="rowSel">指定单元格行</param>
        /// <param name="colSel">指定单元格列</param>
        /// <returns></returns>
        protected virtual CellRectangle GetMergeCell(Point GridLocation, string[,] arrStrGrid, int rowHeight, int[] ArrColWidth, int rowSel, int colSel, GridMergeFlag gridMergeFlag)
        {
            CellRectangle cell = new CellRectangle(0, 0, 0, 0);

            int lngRows = arrStrGrid.GetLength(0);  //行数
            int lngCols = arrStrGrid.GetLength(1);  //列数

            int lngMergeRows = 1;                   //合并的行数（本身为1）
            int lngMergeCols = 1;                   //合并的列数

            int lngStartRow = rowSel;               //记录与此单元格合并的起始行
            int lngEndRow = rowSel;                 //以便计算高及起点Y坐标

            int lngStartCol = colSel;               //记录与此单元格合并的起始列
            int lngEndCol = colSel;                 //以便计算宽及起点X坐标

            if (gridMergeFlag == GridMergeFlag.Any || gridMergeFlag == GridMergeFlag.Col || gridMergeFlag == GridMergeFlag.ColDependOnBeforeGroup)
            {
                //计算在"列"上进行行合并时起始行与合并的多少
                //往上查合并(列不变)
                for (int rowIndex = rowSel - 1; rowIndex >= 0; rowIndex--)
                {
                    if (arrStrGrid[rowSel, colSel] == arrStrGrid[rowIndex, colSel])
                    {
                        lngMergeRows++;
                        lngStartRow--;
                    }
                    else
                    {
                        break;
                    }
                }
                //往下查合并(列不变)
                for (int rowIndex = rowSel + 1; rowIndex < lngRows; rowIndex++)
                {
                    if (arrStrGrid[rowSel, colSel] == arrStrGrid[rowIndex, colSel])
                    {
                        lngMergeRows++;
                        lngEndRow++;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (gridMergeFlag == GridMergeFlag.Any || gridMergeFlag == GridMergeFlag.Row)
            {

                //计算在"行"上进行列合并时起始列与合并的多少
                //往左查合并(行不变)
                for (int colIndex = colSel - 1; colIndex >= 0; colIndex--)
                {
                    if (arrStrGrid[rowSel, colSel] == arrStrGrid[rowSel, colIndex])
                    {
                        lngMergeCols++;
                        lngStartCol--;
                    }
                    else
                    {
                        break;
                    }
                }
                //往右查合并(行不变)
                for (int colIndex = colSel + 1; colIndex < lngCols; colIndex++)
                {
                    if (arrStrGrid[rowSel, colSel] == arrStrGrid[rowSel, colIndex])
                    {
                        lngMergeCols++;
                        lngEndCol++;
                    }
                    else
                    {
                        break;
                    }
                }

            }

            //******************计算左顶宽高******************
            int cellLeft = GridLocation.X;
            int cellTop = GridLocation.Y + lngStartRow * rowHeight; //若行高不是固定行高，可以计算之前行的行高总和

            int cellWidth = 0;
            int cellHeight = 0;

            //单元格合并列的前边的单元格列宽和
            for (int i = lngStartCol - 1; i >= 0; i--)
            {
                cellLeft += ArrColWidth[i];
            }

            //单元格合并列列宽和
            for (int i = lngStartCol; i <= lngEndCol; i++)
            {
                cellWidth += ArrColWidth[i];
            }

            cellHeight = lngMergeRows * rowHeight;                  //若行高不是固定行高，可以计算所有行的行高总和

            cell = new CellRectangle(cellLeft, cellTop, cellWidth, cellHeight);

            return cell;
        }

        #endregion

    }//End Class
}//End NameSpace


