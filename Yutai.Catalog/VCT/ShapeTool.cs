namespace Yutai.Catalog.VCT
{
    using ESRI.ArcGIS.Carto;
    using ESRI.ArcGIS.Geometry;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Reflection;

    public class ShapeTool
    {
        private ICoLayer icoLayer_0 = new CoLayerClass();

        public ICoFeature GeometryToXpgisFeature(IGeometry igeometry_0)
        {
            if (igeometry_0 == null)
            {
                return null;
            }
            ICoFeature feature2 = null;
            switch (igeometry_0.GeometryType)
            {
                case esriGeometryType.esriGeometryPoint:
                    feature2 = CoFeatureFactory.CreateFeature(this.icoLayer_0, CoFeatureType.Point);
                    break;

                case esriGeometryType.esriGeometryPolyline:
                    feature2 = CoFeatureFactory.CreateFeature(this.icoLayer_0, CoFeatureType.Polyline);
                    break;

                case esriGeometryType.esriGeometryPolygon:
                    feature2 = CoFeatureFactory.CreateFeature(this.icoLayer_0, CoFeatureType.Polygon);
                    break;
            }
            if (feature2 != null)
            {
                switch (feature2.Type)
                {
                    case CoFeatureType.Point:
                        this.method_2(feature2, igeometry_0);
                        return feature2;

                    case CoFeatureType.Line:
                        return feature2;

                    case CoFeatureType.Polygon:
                        this.method_0(feature2, igeometry_0);
                        return feature2;

                    case CoFeatureType.Annotation:
                        this.method_3(feature2, igeometry_0);
                        return feature2;

                    case CoFeatureType.Polyline:
                        this.method_1(feature2, igeometry_0);
                        return feature2;
                }
            }
            return feature2;
        }

        private void method_0(ICoFeature icoFeature_0, IGeometry igeometry_0)
        {
            ICoPolygonFeature feature = icoFeature_0 as ICoPolygonFeature;
            IPolygon polygon = igeometry_0 as IPolygon;
            if ((feature != null) && (polygon != null))
            {
                IGeometryCollection geometrys = polygon as IGeometryCollection;
                if (geometrys != null)
                {
                    for (int i = 0; i < geometrys.GeometryCount; i++)
                    {
                        IPointCollection points = geometrys.get_Geometry(i) as IPointCollection;
                        if (points != null)
                        {
                            CoPointCollection item = new CoPointCollection();
                            for (int j = 0; j < points.PointCount; j++)
                            {
                                IPoint point = points.get_Point(j);
                                if (point != null)
                                {
                                    item.Add(new CoPointClass(point.X, point.Y, point.Z));
                                }
                            }
                            feature.Points.Add(item);
                        }
                    }
                }
            }
        }

        private void method_1(ICoFeature icoFeature_0, IGeometry igeometry_0)
        {
            ICoPolylineFeature feature = icoFeature_0 as ICoPolylineFeature;
            IPolyline polyline = igeometry_0 as IPolyline;
            if ((feature != null) && (polyline != null))
            {
                IGeometryCollection geometrys = polyline as IGeometryCollection;
                if (geometrys != null)
                {
                    for (int i = 0; i < geometrys.GeometryCount; i++)
                    {
                        CoPointCollection item = new CoPointCollection();
                        IPointCollection points2 = geometrys.get_Geometry(i) as IPointCollection;
                        if (points2 != null)
                        {
                            for (int j = 0; j < points2.PointCount; j++)
                            {
                                IPoint point = points2.get_Point(j);
                                item.Add(new CoPointClass(point.X, point.Y, point.Z));
                            }
                            feature.Points.Add(item);
                        }
                    }
                }
            }
        }

        private void method_2(ICoFeature icoFeature_0, IGeometry igeometry_0)
        {
            ICoPointFeature feature = icoFeature_0 as ICoPointFeature;
            IPoint point = igeometry_0 as IPoint;
            if ((feature != null) && (point != null))
            {
                feature.Point.Add(new CoPointClass(point.X, point.Y, point.Z));
            }
        }

        private void method_3(ICoFeature icoFeature_0, IGeometry igeometry_0)
        {
            ICoAnnotationFeature feature = icoFeature_0 as ICoAnnotationFeature;
            IAnnotationFeature feature2 = igeometry_0 as IAnnotationFeature;
            if (((feature != null) && (feature2 != null)) && (feature2.Annotation != null))
            {
                ITextElement annotation = feature2.Annotation as ITextElement;
                if (annotation != null)
                {
                    IPoint geometry = ((IElement) annotation).Geometry as IPoint;
                    if (geometry != null)
                    {
                        feature.Point.Add(new CoPointClass(geometry.X, geometry.Y, geometry.Z));
                        feature.Text = annotation.Text;
                        feature.Angle = annotation.Symbol.Angle;
                        feature.Color = Color.FromArgb(annotation.Symbol.Color.RGB);
                        FontStyle regular = FontStyle.Regular;
                        if (annotation.Symbol.Font.Bold)
                        {
                            regular = FontStyle.Bold;
                        }
                        if (annotation.Symbol.Font.Italic)
                        {
                            regular = FontStyle.Italic;
                        }
                        if (annotation.Symbol.Font.Strikethrough)
                        {
                            regular = FontStyle.Strikeout;
                        }
                        if (annotation.Symbol.Font.Underline)
                        {
                            regular = FontStyle.Underline;
                        }
                        Font font = new Font(annotation.Symbol.Font.Name, (float) annotation.Symbol.Font.Size, regular);
                        feature.Font = font;
                    }
                }
            }
        }

        private IPoint[] method_4(CoPointCollection coPointCollection_0)
        {
            List<IPoint> list = new List<IPoint>();
            foreach (ICoPoint point in coPointCollection_0)
            {
                IPoint item = new ESRI.ArcGIS.Geometry.Point
                {
                    X = point.X,
                    Y = point.Y,
                    Z = point.Z
                };
                list.Add(item);
            }
            return list.ToArray();
        }

        public IGeometry XpgisFeatureToGeometry(ICoFeature icoFeature_0)
        {
            object before = Missing.Value;
            switch (icoFeature_0.Type)
            {
                case CoFeatureType.Point:
                {
                    IPoint[] pointArray2 = this.method_4((icoFeature_0 as ICoPointFeature).Point);
                    int index = 0;
                    if (0 >= pointArray2.Length)
                    {
                        break;
                    }
                    return pointArray2[index];
                }
                case CoFeatureType.Polygon:
                {
                    IGeometryCollection geometrys2 = new Polygon() as IGeometryCollection;
                    foreach (CoPointCollection points in (icoFeature_0 as ICoPolygonFeature).Points)
                    {
                        IPointCollection points3 = new Ring();
                        foreach (IPoint point2 in this.method_4(points))
                        {
                            points3.AddPoint(point2, ref before, ref before);
                        }
                        geometrys2.AddGeometry((IGeometry) points3, ref before, ref before);
                    }
                    ((IPolygon) geometrys2).SimplifyPreserveFromTo();
                    return (IGeometry) geometrys2;
                }
                case CoFeatureType.Polyline:
                {
                    IGeometryCollection geometrys = new Polyline() as IGeometryCollection;
                    foreach (CoPointCollection points in (icoFeature_0 as ICoPolylineFeature).Points)
                    {
                        IPoint[] pointArray3 = this.method_4(points);
                        IPointCollection points2 = new Path();
                        for (int i = 0; i < pointArray3.Length; i++)
                        {
                            points2.AddPoint(pointArray3[i], ref before, ref before);
                        }
                        geometrys.AddGeometry((IGeometry) points2, ref before, ref before);
                    }
                    return (IGeometry) geometrys;
                }
            }
            return null;
        }
    }
}

