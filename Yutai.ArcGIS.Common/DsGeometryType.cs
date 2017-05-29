using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.ArcGIS.Common
{
    /// <summary>
    /// 几何形状类型的枚举类
    /// 用于画图时识别几何形状的类型
    /// </summary>
    public enum DsGeometryType
    {
        /// <summary>
        /// 空几何形状
        /// </summary>
        dsGTNone = 0,
        /// <summary>
        /// 点几何形状
        /// </summary>
        dsGTPoint = 1,
        /// <summary>
        /// 直线几何形状
        /// </summary>
        dsGTLine = 2,
        /// <summary>
        /// 多义线几何形状
        /// </summary>
        dsGTPolyline = 3,
        /// <summary>
        /// 矩形几何形状
        /// </summary>
        dsGTRectangle = 4,
        /// <summary>
        /// 圆几何形状
        /// </summary>
        dsGTCircle = 5,
        /// <summary>
        /// 椭圆几何形状
        /// </summary>
        dsGTEllipse = 6,
        /// <summary>
        /// 面几何形状
        /// </summary>
        dsGTPolygon = 7,
        /// <summary>
        /// 矩面几何形状
        /// </summary>
        dsGTRectangularPolygon = 8,
    }
}
