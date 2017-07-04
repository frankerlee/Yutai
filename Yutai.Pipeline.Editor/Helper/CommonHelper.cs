using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Config.Concretes;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Pipeline.Editor.Helper
{
    class CommonHelper
    {
        public static ILayer GetLayerByName(IMap map, string layerName, bool isFeatureLayer = false)
        {
            try
            {
                IEnumLayer enumLayer = map.Layers[null, false];
                enumLayer.Reset();
                ILayer layer;
                while ((layer = enumLayer.Next()) != null)
                {
                    if (isFeatureLayer && (layer is IFeatureLayer) == false)
                        continue;
                    if (layer.Name == layerName)
                    {
                        return layer;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public static int IsFromPoint(IPoint point, IPolyline polyline)
        {
            double dis = GetDistance(point, polyline.FromPoint);
            if (dis < 0.001)
                return 1;
            dis = GetDistance(point, polyline.ToPoint);
            if (dis < 0.001)
                return -1;
            return 0;
        }

        public static double GetDistance(IPoint point1, IPoint point2)
        {
            return Math.Sqrt((point1.X - point2.X) * (point1.X - point2.X) + (point1.Y - point2.Y) * (point1.Y - point2.Y));
        }
    }
}
