namespace Yutai.ArcGIS.Catalog.VCT
{
    public class CoFeatureFactory
    {
        public static ICoFeature CreateFeature(ICoLayer icoLayer_0, CoFeatureType coFeatureType_0)
        {
            switch (coFeatureType_0)
            {
                case CoFeatureType.Point:
                    return new CoPointFeature(icoLayer_0);

                case CoFeatureType.Polygon:
                    return new CoPolygonFeature(icoLayer_0);

                case CoFeatureType.Annotation:
                    return new CoAnnotationFeature(icoLayer_0);

                case CoFeatureType.Polyline:
                    return new CoPolylineFeature(icoLayer_0);
            }
            return null;
        }
    }
}

