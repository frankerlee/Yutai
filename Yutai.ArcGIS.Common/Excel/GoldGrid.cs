using System;
using System.Drawing;

namespace Yutai.ArcGIS.Common.Excel
{

    /// <summary>
    /// GoldGrid 的摘要说明。
    /// 
    /// 作 者：长江支流(周方勇)
    /// Email：flygoldfish@163.com  QQ：150439795
    /// 网 址：www.webmis.com.cn
    /// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
    /// 
    /// </summary>
    public class GoldGrid : GridBase
    {
        public GoldGrid()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //			

        }

        #region	_arrStrGrid相关的属性与方法 GridText获取设置_arrStrGrid、SetText(int row,int col,string text)、GetText(int row,int col)获取单元格文本

        /// <summary>
        /// 设置指定单元格文本
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="text"></param>
        public void SetText(int row, int col, string text)
        {
            _arrStrGrid[row, col] = text;
        }
        public void SetText(string text)
        {
            _arrStrGrid[RowSel, ColSel] = text;
        }

        /// <summary>
        /// 获取指定单元格文本
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public string GetText(int row, int col)
        {
            return _arrStrGrid[row, col];
        }
        public string GetText()
        {
            return _arrStrGrid[RowSel, ColSel];
        }

        #endregion

        private string _colsAlignList = "";             //列对齐列表,由Left，Center，Right第一个字母组成的串的对齐方式的串

        /// <summary>
        /// 获取或设置列对齐列表对象，由Left，Center，Right第一个字母组成的串的对齐方式的串
        /// </summary>
        public string ColsAlignString
        {
            get
            {
                return this._colsAlignList;
            }
            set
            {
                if (value != null)
                {
                    this._colsAlignList = value;
                }
            }
        }


        //重要属性									
        private object _DataSource = null;              //△会改变行列及其它属性
        #region DataSource
        /// <summary>
        /// 或取或设置数据源，可以是任意的二维数组型的，如二维数组、网格等
        /// </summary>
        public object DataSource
        {
            get
            {
                return this._DataSource;
            }
            set
            {
                if (value != null)
                {
                    _DataSource = value;

                    //将数据转换成二维数组
                    switch (value.GetType().ToString())
                    {
                        case "System.String":           //字符串
                                                        //...
                            break;
                        case "System.String[]":         //一维数组
                            string[] arr1 = (System.String[])_DataSource;
                            string[,] arr2 = new string[1, arr1.Length];
                            for (int i = 0; i < arr1.Length; i++)
                            {
                                arr2[0, i] = arr1[i];
                            }
                            this.DataSource = arr2;
                            break;
                        case "System.String[,]":        //二维数组
                            this.GridText = (System.String[,])_DataSource;
                            break;
                        case "System.Data.DataTable":   //数据表格							
                            this.GridText = ToArrFromDataTable((System.Data.DataTable)_DataSource);
                            break;
                        case "System.Windows.Forms.DataGrid":
                            this.GridText = ToArrFromDataGrid((System.Windows.Forms.DataGrid)_DataSource);
                            break;
                        case "System.Web.UI.WebControls.DataGrid":
                            this.GridText = ToArrFromDataGrid((System.Web.UI.WebControls.DataGrid)_DataSource);
                            break;
                        case "System.Web.UI.HtmlControls.HtmlTable":
                            this.GridText = ToArrFromHtmlTable((System.Web.UI.HtmlControls.HtmlTable)_DataSource);
                            break;
                            //...太多了，不够的自己加，只要能转换成二维数组
                            //						case "MSHFlexGrid的类型，自己转吧";
                            //							this.GridText = ToArrFromMSHFlexGrid((MSHFlexGrid的类型，自己转吧)_DataSource);
                            //							break;
                    }


                }
            }
        }
        #endregion


        public void SetTextOnRowSel(int rowSel, int startCol, int endCol, string text)
        {
            for (int i = startCol; i <= endCol; i++)
            {
                SetText(rowSel, i, text);
            }
        }

        public void SetTextOnColSel(int colSel, int startRow, int endRow, string text)
        {
            for (int i = startRow; i <= endRow; i++)
            {
                SetText(i, colSel, text);
            }
        }

        /// <summary>
        /// 查找当前单元格左顶宽高
        /// </summary>
        /// <returns></returns>
        public CellRectangle GetMergeCell()
        {
            return GetMergeCell(this.Location, _arrStrGrid, this.PreferredRowHeight, this.ColsWidth, RowSel, ColSel);
        }



        /// <summary>
        /// 初始列对齐字符串，在设置AlignMent、Cols时调用
        /// </summary>
        private void InitColsAlignString()
        {
            string malignString = "";
            string malignChar = "";

            switch (this.AlignMent)
            {
                case AlignFlag.Left:
                    malignChar = "L";
                    break;
                case AlignFlag.Center:
                    malignChar = "C";
                    break;
                case AlignFlag.Right:
                    malignChar = "R";
                    break;
            }

            for (int i = _colsAlignList.Length; i < this.Cols; i++)
            {
                malignString += malignChar;
            }
            this._colsAlignList = malignString;

            if (_colsAlignList.Length > Cols)
            {
                _colsAlignList.Substring(0, Cols);
            }
        }


        #region 所有要用到的二维表格的东东都放到这里吧，不过，最好独立于一个类中
        public string[,] ToArrFromDataTable(System.Data.DataTable source)
        {
            if (source == null)
            {
                return new string[0, 0];
            }

            int mRows, mCols;
            string[,] arrGridText;

            mRows = source.Rows.Count;
            mCols = source.Columns.Count;

            arrGridText = new string[mRows, mCols];

            for (int i = 0; i < mRows; i++)
            {
                for (int j = 0; j < mCols; j++)
                {
                    arrGridText[i, j] = source.Rows[i][j].ToString();
                }
            }

            return arrGridText;
        }

        public string[,] ToArrFromDataGrid(System.Windows.Forms.DataGrid source)
        {

            if (source == null)
            {
                return new string[0, 0];
            }

            int mRows = 0;
            int mCols = 0;
            string[,] arrGridText;

            //TimeDef.Start();	//用捕获错误的方法第一次耗时太大，50行6列的基本上用了4秒多，而第二次只用20.0288毫秒

            //用时：4.656696秒
            //用时：4656.696毫秒
            //可能原因是第一次要加载mscorlib.resources.dll

            try
            {
                string s = "";
                for (int i = 0; i < int.MaxValue - 1; i++)
                {
                    s = source[0, i].ToString();
                    mCols++;
                }
            }
            catch (Exception e) { }

            try
            {
                string s = "";
                for (int i = 0; i < int.MaxValue - 1; i++)
                {
                    s = source[i, 0].ToString();
                    mRows++;
                }
            }
            catch (Exception e) { }
            //TimeDef.End();


            arrGridText = new string[mRows, mCols];

            try
            {

                for (int i = 0; i < mRows; i++)
                {
                    for (int j = 0; j < mCols; j++)
                    {
                        arrGridText[i, j] = source[i, j].ToString();
                    }
                }
            }
            catch (Exception e)
            { }

            return arrGridText;
        }

        public string[,] ToArrFromDataGrid(System.Web.UI.WebControls.DataGrid source)
        {
            if (source == null)
            {
                return new string[0, 0];
            }

            int mRows = 0;
            int mCols = 0;
            string[,] arrGridText;

            mRows = source.Items.Count;
            mCols = source.Columns.Count;

            arrGridText = new string[mRows, mCols];

            for (int i = 0; i < mRows; i++)
            {
                for (int j = 0; j < mCols; j++)
                {
                    arrGridText[i, j] = source.Items[i].Cells[j].Text;
                }
            }

            return arrGridText;
        }

        public string[,] ToArrFromHtmlTable(System.Web.UI.HtmlControls.HtmlTable source)
        {
            if (source == null)
            {
                return new string[0, 0];
            }

            int mRows = source.Rows.Count;
            int mCols = source.Rows[0].Cells.Count;
            string[,] arrGridText = new string[mRows, mCols];

            for (int i = 0; i < mRows; i++)
            {
                for (int j = 0; j < mCols; j++)
                {
                    arrGridText[i, j] = source.Rows[i].Cells[j].InnerText;
                }
            }

            return arrGridText;
        }
        #endregion


        #region protected virtual GetAlignFlag[] GetColsAlign(string alignment)	
        /// <summary>
        /// 返回由Left，Center，Right第一个字母组成的串的对齐方式的数组
        /// </summary>
        /// <param name="Alignment">由Left，Center，Right第一个字母组成的串</param>
        /// <returns></returns>
        protected virtual AlignFlag[] GetColsAlign(string alignment)
        {
            if (alignment == null || alignment.Length == 0)
            {
                return (new AlignFlag[0]);
            }
            int len = alignment.Length;
            AlignFlag[] arrAlign = new AlignFlag[len];
            string strAlign = "";

            for (int i = 0; i < len; i++)
            {
                strAlign = alignment.Substring(i, 1).ToUpper();
                switch (strAlign)
                {
                    //					case "L":
                    //						break;
                    case "C":
                        arrAlign[i] = AlignFlag.Center;
                        break;
                    case "R":
                        arrAlign[i] = AlignFlag.Right;
                        break;
                    default:
                        arrAlign[i] = AlignFlag.Left;
                        break;
                }
            }
            return arrAlign;
        }
        #endregion

        #region protected virtual Cell GetMergeCell(Point GridLocation,string[,] arrStrGrid,int rowHeight,int[] ArrColWidth,int rowSel,int colSel)
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
        protected virtual CellRectangle GetMergeCell(Point GridLocation, string[,] arrStrGrid, int rowHeight, int[] ArrColWidth, int rowSel, int colSel)
        {
            CellRectangle cell = new CellRectangle(0, 0, 0, 0);

            int lngRows = arrStrGrid.GetLength(0);  //行数
            int lngCols = arrStrGrid.GetLength(1);  //列数

            int lngMergeRows = 1;                   //合并的行数
            int lngMergeCols = 1;                   //合并的列数

            int lngStartRow = rowSel;               //记录与此单元格合并的起始行
            int lngEndRow = rowSel;                 //以便计算高及起点Y坐标

            int lngStartCol = colSel;               //记录与此单元格合并的起始列
            int lngEndCol = colSel;                 //以便计算宽及起点X坐标

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

}//End Namespace
