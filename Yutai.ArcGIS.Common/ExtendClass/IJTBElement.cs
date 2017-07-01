using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Common.ExtendClass
{
    /// <summary>
    /// 接图表接口定义
    /// </summary>
    public interface IJTBElement
    {
        /// <summary>
        /// 获取或设置下部图名
        /// </summary>
        string BottomName { get; set; }

        /// <summary>
        /// 获取或设置左下边图名
        /// </summary>
        string LeftBottomName { get; set; }

        /// <summary>
        /// 获取或设置左边图名
        /// </summary>
        string LeftName { get; set; }

        /// <summary>
        /// 获取或设置左上边图名
        /// </summary>
        string LeftTopName { get; set; }

        /// <summary>
        /// 获取或设置线符号
        /// </summary>
        ILineSymbol LineSymbol { get; set; }

        /// <summary>
        /// 获取或设置右下边图名
        /// </summary>
        string RightBottomName { get; set; }

        /// <summary>
        /// 获取或设置右边图名
        /// </summary>
        string RightName { get; set; }

        /// <summary>
        /// 获取或设置右上边图名
        /// </summary>
        string RightTopName { get; set; }

        /// <summary>
        /// 获取或设置文本符号
        /// </summary>
        ITextSymbol TextSymbol { get; set; }

        /// <summary>
        /// 获取或设置本图图名
        /// </summary>
        string TFName { get; set; }

        /// <summary>
        /// 获取或设置上部图名
        /// </summary>
        string TopName { get; set; }
    }
}