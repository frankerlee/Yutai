using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Plugins.Interfaces
{
    public interface IShapeConstructorTool
    {
        esriGeometryType GeometryType { get; }
    }
}