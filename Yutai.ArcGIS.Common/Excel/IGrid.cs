using System.Drawing;

namespace Yutai.ArcGIS.Common.Excel
{
    /// <summary>
    /// IGrid网格最基本的接口。
    /// 
    /// 作 者：长江支流(周方勇)
    /// Email：flygoldfish@163.com  QQ：150439795
    /// 网 址：www.webmis.com.cn
    /// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
    /// 
    /// </summary>
    public interface IGrid
    {

        //*****************属性*****************	
        #region 网格起点及高、宽、使用的字体

        /// <summary>
        /// 网格起点坐标
        /// </summary>
        Point Location
        {
            get;
            set;
        }

        /// <summary>
        /// 网格宽
        /// </summary>
        int Width
        {
            get;
            set;
        }

        /// <summary>
        /// 网格高
        /// </summary>
        int Height
        {
            get;
            set;
        }

        /// <summary>
        /// 网格文本字体
        /// </summary>
        Font Font
        {
            get;
            set;
        }

        #endregion

        #region 网格对齐、网格线、合并、边框方式

        /// <summary>
        /// 网格整体对齐方式
        /// </summary>
        AlignFlag AlignMent
        {
            get;
            set;
        }

        /// <summary>
        /// 网格线类型
        /// </summary>
        GridLineFlag Line
        {
            get;
            set;
        }

        /// <summary>
        /// 单元格合并方式
        /// </summary>
        GridMergeFlag Merge
        {
            get;
            set;
        }

        /// <summary>
        /// 网格边框类型
        /// </summary>
        GridBorderFlag Border
        {
            get;
            set;
        }

        #endregion

        #region 行列数、固定行列数

        /// <summary>
        /// 行数
        /// </summary>
        int Rows
        {
            get;
            set;
        }

        /// <summary>
        /// 列数
        /// </summary>
        int Cols
        {
            get;
            set;
        }

        /// <summary>
        /// 固定行数
        /// </summary>
        int FixedRows
        {
            get;
            set;
        }

        /// <summary>
        /// 固定列数
        /// </summary>
        int FixedCols
        {
            get;
            set;
        }

        #endregion

        #region 理想行高、列宽、及对应的行高、列宽、列对齐数组

        /// <summary>
        /// 获取或设置首选行高
        /// </summary>
        int PreferredRowHeight
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置默认的列宽
        /// </summary>
        int PreferredColWidth
        {
            get;
            set;
        }

        //*********下面是对应的数组*********
        /// <summary>
        /// 获取或设置行高数组
        /// </summary>
        /// <returns></returns>
        int[] RowsHeight
        {
            get;
            set;
        }

        /// <summary>
        /// 返回列宽数组
        /// </summary>
        /// <returns></returns>
        int[] ColsWidth
        {
            get;
            set;
        }

        /// <summary>
        /// 返回列对齐数组
        /// </summary>
        /// <returns></returns>
        AlignFlag[] ColsAlignment
        {
            get;
            set;
        }

        #endregion

        #region 单元格文本

        /// <summary>
        /// 获取或设置当前单元格文本
        /// </summary>		
        string Text
        {
            get;
            set;
        }

        /// <summary>
        /// 获取指定行列单元格文本
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        string get_TextMatrix(int row, int col);

        /// <summary>
        /// 设置指定单元格文本
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="textMatrix"></param>
        /// <returns></returns>
        void set_TextMatrix(int row, int col, string textMatrix);

        /// <summary>
        /// 获取或设置二维网格的文本
        /// </summary>
        string[,] GridText
        {
            get;
            set;
        }

        #endregion


        #region 当前行列、选定行列数

        /// <summary>
        /// 当前行
        /// </summary>
        int Row
        {
            get;
            set;
        }

        /// <summary>
        /// 当前列
        /// </summary>
        int Col
        {
            get;
            set;
        }

        /// <summary>
        /// 选择行
        /// </summary>
        int RowSel
        {
            get;
            set;
        }

        /// <summary>
        /// 选择列
        /// </summary>
        int ColSel
        {
            get;
            set;
        }

        #endregion

        #region 获取或设置某行/列的行高/列宽

        /// <summary>
        /// 获取指定行的行高
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        int get_RowHeight(int index);

        /// <summary>
        /// 设置指定行的行高
        /// </summary>
        /// <param name="index"></param>
        /// <param name="rowHeight"></param>
        void set_RowHeight(int index, int rowHeight);

        /// <summary>
        /// 获取指定列的列宽
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        int get_ColWidth(int index);

        /// <summary>
        /// 设置指定列的列宽
        /// </summary>
        /// <param name="index"></param>
        /// <param name="colWidth"></param>
        /// <returns></returns>
        void set_ColWidth(int index, int colWidth);

        /// <summary>
        /// 设置水平列对齐方式
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        AlignFlag get_ColAlignment(int index);

        /// <summary>
        /// 获取水平列对齐方式
        /// </summary>
        /// <param name="index"></param>
        /// <param name="colAlignment"></param>
        void set_ColAlignment(int index, AlignFlag colAlignment);

        #endregion






        /*
		bool get_ColIsVisible(int index);
		bool set_ColIsVisible(int index);



		bool get_RowIsVisible(int index);

		bool get_MergeCol(int index);
		void set_MergeCol(int index,bool mergeCol);

		bool get_MergeRow(int index);
		bool set_MergeRow(int index,bool mergeRow);

		*/

    }//End Class
}//End NameSpace