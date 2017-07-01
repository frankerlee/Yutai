using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Common
{
    public class SpatialReferenctOperator
    {
        public static void ChangeCoordinateSystem(IGeodatabaseRelease igeodatabaseRelease_0,
            ISpatialReference ispatialReference_0, bool bool_0)
        {
            if (ispatialReference_0 != null)
            {
                bool geoDatasetPrecision = GeodatabaseTools.GetGeoDatasetPrecision(igeodatabaseRelease_0);
                IControlPrecision2 controlPrecision = ispatialReference_0 as IControlPrecision2;
                if (controlPrecision.IsHighPrecision != geoDatasetPrecision)
                {
                    controlPrecision.IsHighPrecision = geoDatasetPrecision;
                    double num;
                    double num2;
                    double num3;
                    double num4;
                    ispatialReference_0.GetDomain(out num, out num2, out num3, out num4);
                    if (bool_0)
                    {
                        ISpatialReferenceResolution spatialReferenceResolution =
                            ispatialReference_0 as ISpatialReferenceResolution;
                        spatialReferenceResolution.ConstructFromHorizon();
                        spatialReferenceResolution.SetDefaultXYResolution();
                        ISpatialReferenceTolerance spatialReferenceTolerance =
                            ispatialReference_0 as ISpatialReferenceTolerance;
                        spatialReferenceTolerance.SetDefaultXYTolerance();
                    }
                }
            }
        }

        public static ISpatialReference ConstructCoordinateSystem(IGeodatabaseRelease igeodatabaseRelease_0)
        {
            bool geoDatasetPrecision = GeodatabaseTools.GetGeoDatasetPrecision(igeodatabaseRelease_0);

            ISpatialReference spatialReference = new UnknownCoordinateSystem() as ISpatialReference;
            IControlPrecision2 controlPrecision = spatialReference as IControlPrecision2;
            controlPrecision.IsHighPrecision = geoDatasetPrecision;
            ISpatialReferenceResolution spatialReferenceResolution = spatialReference as ISpatialReferenceResolution;
            spatialReferenceResolution.ConstructFromHorizon();
            spatialReferenceResolution.SetDefaultXYResolution();
            ISpatialReferenceTolerance spatialReferenceTolerance = spatialReference as ISpatialReferenceTolerance;
            spatialReferenceTolerance.SetDefaultXYTolerance();
            return spatialReference;
        }

        public static bool ValideFeatureClass(IGeoDataset igeoDataset_0)
        {
            IEnvelope extent = igeoDataset_0.Extent;
            bool result;
            if (extent.IsEmpty)
            {
                result = true;
            }
            else
            {
                if (igeoDataset_0 is IFeatureClass)
                {
                    if (!CommonHelper.FeatureClassHasData(igeoDataset_0 as IFeatureClass))
                    {
                        result = true;
                        return result;
                    }
                }
                else if (igeoDataset_0 is IFeatureLayer && !CommonHelper.LayerHasData(igeoDataset_0 as IFeatureLayer))
                {
                    result = true;
                    return result;
                }
                ISpatialReference spatialReference = igeoDataset_0.SpatialReference;
                if (spatialReference is IProjectedCoordinateSystem)
                {
                    double falseEasting = (spatialReference as IProjectedCoordinateSystem).FalseEasting;
                    double falseNorthing = (spatialReference as IProjectedCoordinateSystem).FalseNorthing;
                    if (falseEasting > 0.0 && extent.XMin < falseEasting - 500000.0)
                    {
                        result = false;
                        return result;
                    }
                    ILinearUnit coordinateUnit = (spatialReference as IProjectedCoordinateSystem).CoordinateUnit;
                    double num = -10001858.0/coordinateUnit.MetersPerUnit + falseNorthing;
                    double num2 = 10001858.0/coordinateUnit.MetersPerUnit + falseNorthing;
                    if (extent.YMin < num || extent.YMax > num2)
                    {
                        result = false;
                        return result;
                    }
                }
                else if (spatialReference is IGeographicCoordinateSystem)
                {
                    if (extent.XMin < -360.0 || extent.XMax > 360.0)
                    {
                        result = false;
                        return result;
                    }
                    if (extent.YMin < -100.0 || extent.YMax > 100.0)
                    {
                        result = false;
                        return result;
                    }
                }
                result = true;
            }
            return result;
        }
    }
}