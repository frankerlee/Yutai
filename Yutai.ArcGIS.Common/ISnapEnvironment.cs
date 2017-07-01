using System.Drawing;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common
{
    public interface ISnapEnvironment : IEngineSnapEnvironment
    {
        double MapUnitTolerance { get; }

        bool SnapPoint(IPoint ipoint_0, IPoint ipoint_1);
    }
}