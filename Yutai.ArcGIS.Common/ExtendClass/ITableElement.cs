using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.ExtendClass
{
	public interface ITableElement
	{
		/// <summary>
		/// 表格列数
		/// </summary>
		int ColumnNumber
		{
			get;
			set;
		}

		/// <summary>
		/// 内部水平线
		/// </summary>
		bool HasInnerHorizontalLine
		{
			get;
			set;
		}

		/// <summary>
		/// 内部竖直线
		/// </summary>
		bool HasInnerVerticalLine
		{
			get;
			set;
		}

		/// <summary>
		/// 左边线
		/// </summary>
		bool HasLeftBoundLine
		{
			get;
			set;
		}

		/// <summary>
		/// 下边线
		/// </summary>
		bool HasLowerBoundLine
		{
			get;
			set;
		}

		/// <summary>
		/// 右边线
		/// </summary>
		bool HasRightBoundLine
		{
			get;
			set;
		}

		/// <summary>
		/// 上边线
		/// </summary>
		bool HasUpperBoundLine
		{
			get;
			set;
		}

		/// <summary>
		/// 行数
		/// </summary>
		double Height
		{
			get;
			set;
		}

		/// <summary>
		/// 只有内部第一个水平线。仅当HasInnerHorizontalLine为true，该值才起作用
		/// </summary>
		bool OnlyFirstHorizontalLine
		{
			get;
			set;
		}

		/// <summary>
		/// 只有内部第一个竖直线。仅当HasInnerVerticalLine为true，该值才起作用
		/// </summary>
		bool OnlyFirstVerticalLine
		{
			get;
			set;
		}

		/// <summary>
		/// 表格行数
		/// </summary>
		int RowNumber
		{
			get;
			set;
		}

		/// <summary>
		/// 宽度
		/// </summary>
		double Width
		{
			get;
			set;
		}

		/// <summary>
		/// 创建表
		/// </summary>
		/// <param name="pAV"></param>
		/// <param name="Leftdown"></param>
		void CreateTable(IActiveView pAV, IPoint Leftdown, object tabcell);

		/// <summary>
		/// 获取单元格对应的元素
		/// </summary>
		/// <param name="row"></param>
		/// <param name="col"></param>
		/// <returns></returns>
		IElement GetCellElement(int row, int col);

		/// <summary>
		/// 设置表格内容
		/// </summary>
		/// <param name="row"></param>
		/// <param name="col"></param>
		/// <param name="element"></param>
		void SetTableCell(int row, int col, object element);
	}
}