using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Controls
{
    internal  class LocalCommonHelper
    {
        public static int GetLayerIndex(IMap imap_0, ILayer ilayer_0)
        {
            if (ilayer_0 is IFDOGraphicsLayer)
            {
                return 0;
            }
            if (!((ilayer_0 is IGeoFeatureLayer) || (ilayer_0 is IGroupLayer)))
            {
                return imap_0.LayerCount;
            }
            if (ilayer_0 is IGdbRasterCatalogLayer)
            {
                return imap_0.LayerCount;
            }
            int num2 = 0;
            for (int i = 0; i < imap_0.LayerCount; i++)
            {
                ILayer layer = imap_0.get_Layer(i);
                if (layer is IFDOGraphicsLayer)
                {
                    num2++;
                }
                else if (layer is IGroupLayer)
                {
                    num2++;
                }
                else
                {
                    if (!(layer is IGeoFeatureLayer))
                    {
                        return num2;
                    }
                    if (ilayer_0 is IGroupLayer)
                    {
                        return num2;
                    }
                    esriGeometryType shapeType = (layer as IGeoFeatureLayer).FeatureClass.ShapeType;
                    esriGeometryType type2 = (ilayer_0 as IGeoFeatureLayer).FeatureClass.ShapeType;
                    switch (type2)
                    {
                        case esriGeometryType.esriGeometryMultipoint:
                        case esriGeometryType.esriGeometryPoint:
                            return num2;
                    }
                    if (type2 == esriGeometryType.esriGeometryPolyline)
                    {
                        switch (shapeType)
                        {
                            case esriGeometryType.esriGeometryPolygon:
                            case esriGeometryType.esriGeometryPolyline:
                                return num2;
                        }
                    }
                    else if ((type2 == esriGeometryType.esriGeometryPolygon) &&
                             (shapeType == esriGeometryType.esriGeometryPolygon))
                    {
                        return num2;
                    }
                    num2++;
                }
            }
            return num2;
        }
    }
}
