using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Plugins.Scene.Helpers
{
    internal sealed class SurfaceInfo
    {
        internal static ILayer m_pSelectLayer;

        public static ISurface GetCurrentSurface(ISceneGraph isceneGraph_0)
        {
            IBasicMap basicMap = isceneGraph_0.Scene as IBasicMap;
            short num = 0;
            ISurface result;
            while ((int)num < basicMap.LayerCount)
            {
                ISurface surfaceFromLayer = SurfaceInfo.GetSurfaceFromLayer(basicMap.get_Layer((int)num));
                if (surfaceFromLayer != null)
                {
                    result = surfaceFromLayer;
                    return result;
                }
                num += 1;
            }
            result = null;
            return result;
        }

        public static void GetCurrentSurfaceMax(ILayer ilayer_0, out double double_0, out double double_1)
        {
            ISurface surfaceFromLayer = SurfaceInfo.GetSurfaceFromLayer(ilayer_0);
            if (surfaceFromLayer != null)
            {
                if (surfaceFromLayer is ITinAdvanced)
                {
                    ITinAdvanced tinAdvanced = surfaceFromLayer as ITinAdvanced;
                    IEnvelope extent = tinAdvanced.Extent;
                    double_0 = ((extent.Width > extent.Height) ? extent.Width : extent.Height);
                    double_1 = extent.ZMax;
                }
                else
                {
                    IRasterSurface rasterSurface = surfaceFromLayer as IRasterSurface;
                    IRasterProps rasterProps = rasterSurface.Raster as IRasterProps;
                    IEnvelope extent = rasterProps.Extent;
                    double_0 = ((extent.Width > extent.Height) ? extent.Width : extent.Height);
                    IRasterBandCollection rasterBandCollection = rasterProps as IRasterBandCollection;
                    IRasterBand rasterBand = rasterBandCollection.Item(0);
                    IRasterStatistics statistics = rasterBand.Statistics;
                    double_1 = statistics.Maximum;
                }
            }
            else
            {
                double_0 = 0.0;
                double_1 = 0.0;
            }
        }

        public static ISurface GetBaseSurface(ILayer ilayer_0)
        {
            ISurface result = null;
            ILayerExtensions layerExtensions = ilayer_0 as ILayerExtensions;
            for (int i = 0; i <= layerExtensions.ExtensionCount - 1; i++)
            {
                if (layerExtensions.get_Extension(i) is I3DProperties)
                {
                    I3DProperties i3DProperties = layerExtensions.get_Extension(i) as I3DProperties;
                    if (i3DProperties.BaseOption == esriBaseOption.esriBaseSurface && i3DProperties.BaseSurface != null)
                    {
                        result = (i3DProperties.BaseSurface as ISurface);
                    }
                    return result;
                }
            }
            return result;
        }

        public static ISurface GetSurfaceFromLayer(ILayer ilayer_0)
        {
            ISurface surface = null;
            if (ilayer_0 == null)
            {
                surface = null;
            }
            else if (ilayer_0 is ITinLayer)
            {
                ITinLayer tinLayer = ilayer_0 as ITinLayer;
                surface = (tinLayer.Dataset as ISurface);
            }
            else if (ilayer_0 is IRasterLayer)
            {
                IRasterLayer rasterLayer = ilayer_0 as IRasterLayer;
                surface = SurfaceInfo.GetBaseSurface(rasterLayer);
                if (surface == null)
                {
                    IRasterBandCollection rasterBandCollection = rasterLayer.Raster as IRasterBandCollection;
                    IRasterBand rasterBand = rasterBandCollection.Item(0);
                    surface = (new RasterSurface
                    {
                        RasterBand = rasterBand
                    } as ISurface);
                }
            }
            return surface;
        }
    }
}