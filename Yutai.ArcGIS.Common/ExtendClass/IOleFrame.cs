using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Common.ExtendClass
{
    /// <summary>
    /// OLE对象元素接口
    /// </summary>
    public interface IOleFrame
    {
        /// <summary>
        /// 创建OLE对象元素
        /// </summary>
        /// <param name="Display">屏幕显示对象</param>
        /// <param name="hWnd">窗口句柄</param>
        /// <returns>成功true</returns>
        bool CreateOleClientItem(IDisplay Display, int hWnd);

        /// <summary>
        /// 编辑OLE对象元素
        /// </summary>
        /// <param name="hWnd"></param>
        void Edit(int hWnd);

        event OLEEditCompleteHandler OLEEditComplete;
    }
}