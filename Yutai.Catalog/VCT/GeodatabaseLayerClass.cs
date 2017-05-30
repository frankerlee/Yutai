using ESRI.ArcGIS.Display;
using stdole;

namespace Yutai.Catalog.VCT
{
    using ESRI.ArcGIS.Carto;
   
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using ESRI.ArcGIS.Geometry;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public sealed class GeodatabaseLayerClass : AbsCoConvert
    {
        private bool bool_0 = false;
        private CoLayerMapper coLayerMapper_0 = null;
        private IFeatureClass ifeatureClass_0 = null;
        private IFeatureCursor ifeatureCursor_0 = null;
        private IFeatureCursor ifeatureCursor_1 = null;

        public GeodatabaseLayerClass(IFeatureClass ifeatureClass_1)
        {
            this.ifeatureClass_0 = ifeatureClass_1;
            this.ifeatureCursor_0 = this.ifeatureClass_0.Search(null, false);
            this.UpdateLayerStruct();
        }

        public override void Close()
        {
        }

        public override void Dispose()
        {
            this.Close();
            if (this.ifeatureCursor_0 != null)
            {
                Marshal.ReleaseComObject(this.ifeatureCursor_0);
                this.ifeatureCursor_0 = null;
            }
            if (this.ifeatureCursor_1 != null)
            {
                Marshal.ReleaseComObject(this.ifeatureCursor_1);
                this.ifeatureCursor_1 = null;
            }
            base.Dispose();
        }

        ~GeodatabaseLayerClass()
        {
            this.Dispose();
        }

        public override void Flush()
        {
            this.Flush(null);
        }

        public override void Flush(CoLayerMapper coLayerMapper_1)
        {
            int num;
            IFeatureBuffer buffer;
            this.coLayerMapper_0 = coLayerMapper_1;
            if (this.ifeatureClass_0.FeatureType == esriFeatureType.esriFTAnnotation)
            {
                this.bool_0 = false;
            }
            else
            {
                this.bool_0 = true;
            }
            if (this.bool_0)
            {
                if (this.ifeatureCursor_1 == null)
                {
                    this.ifeatureCursor_1 = this.ifeatureClass_0.Insert(true);
                }
                for (num = 0; num < base.XpgisLayer.FeatureCount; num++)
                {
                    buffer = this.method_9(base.XpgisLayer.GetFeatureByIndex(num));
                    if (buffer != null)
                    {
                        this.ifeatureCursor_1.InsertFeature(buffer);
                    }
                }
                try
                {
                    this.ifeatureCursor_1.Flush();
                }
                catch
                {
                }
            }
            else
            {
                IFeatureClassWrite write = this.ifeatureClass_0 as IFeatureClassWrite;
                ISet features = new Set();
                for (num = 0; num < base.XpgisLayer.FeatureCount; num++)
                {
                    buffer = this.method_9(base.XpgisLayer.GetFeatureByIndex(num));
                    if (buffer != null)
                    {
                        features.Add(buffer as IFeature);
                    }
                }
                write.WriteFeatures(features);
            }
            base.XpgisLayer.RemoveAllFeature();
        }

        private ICoFeature method_0(IFeature ifeature_0)
        {
            if ((ifeature_0 == null) || (ifeature_0.Shape == null))
            {
                if (ifeature_0.HasOID)
                {
                    Debug.WriteLine("丢失要素：" + ifeature_0.OID.ToString());
                }
                else
                {
                    Debug.WriteLine("丢失要素：" + ifeature_0.ToString());
                }
                return null;
            }
            ICoFeature feature2 = null;
            IFeatureClass class2 = ifeature_0.Class as IFeatureClass;
            if (class2 != null)
            {
                switch (class2.FeatureType)
                {
                    case esriFeatureType.esriFTSimple:
                        switch (ifeature_0.Shape.GeometryType)
                        {
                            case esriGeometryType.esriGeometryPoint:
                                feature2 = CoFeatureFactory.CreateFeature(base.XpgisLayer, CoFeatureType.Point);
                                goto Label_00E5;

                            case esriGeometryType.esriGeometryMultipoint:
                                goto Label_00E5;

                            case esriGeometryType.esriGeometryPolyline:
                                feature2 = CoFeatureFactory.CreateFeature(base.XpgisLayer, CoFeatureType.Polyline);
                                goto Label_00E5;

                            case esriGeometryType.esriGeometryPolygon:
                                feature2 = CoFeatureFactory.CreateFeature(base.XpgisLayer, CoFeatureType.Polygon);
                                goto Label_00E5;
                        }
                        break;

                    case esriFeatureType.esriFTAnnotation:
                        feature2 = CoFeatureFactory.CreateFeature(base.XpgisLayer, CoFeatureType.Annotation);
                        break;
                }
            }
        Label_00E5:
            if (feature2 != null)
            {
                switch (feature2.Type)
                {
                    case CoFeatureType.Point:
                        this.method_4(feature2, ifeature_0);
                        break;

                    case CoFeatureType.Polygon:
                        this.method_2(feature2, ifeature_0);
                        break;

                    case CoFeatureType.Annotation:
                        this.method_5(feature2, ifeature_0);
                        break;

                    case CoFeatureType.Polyline:
                        this.method_3(feature2, ifeature_0);
                        break;
                }
            }
            this.method_1(feature2, ifeature_0);
            return feature2;
        }

        private void method_1(ICoFeature icoFeature_0, IFeature ifeature_0)
        {
            if ((icoFeature_0 == null) || (ifeature_0 == null))
            {
                Debug.WriteLine("ArcgisConvertClass.SetXpgisFeautreValue()的任何参数都不能为空");
            }
            else
            {
                int num2;
                for (int i = 0; i < icoFeature_0.Layer.Fields.Count; i++)
                {
                    ICoField field = icoFeature_0.Layer.Fields[i];
                    num2 = ifeature_0.Fields.FindField(field.Name);
                    if (num2 != -1)
                    {
                        object obj2 = ifeature_0.get_Value(num2);
                        if (icoFeature_0.Values.Length > i)
                        {
                            icoFeature_0.SetValue(num2, obj2);
                        }
                        else
                        {
                            icoFeature_0.AppendValue(obj2);
                        }
                    }
                }
                IFeatureClass class2 = ifeature_0.Class as IFeatureClass;
                if (class2 != null)
                {
                    num2 = ifeature_0.Fields.FindField(class2.OIDFieldName);
                    if (num2 != -1)
                    {
                        int result = 0;
                        int.TryParse(ifeature_0.get_Value(num2).ToString(), out result);
                        icoFeature_0.OID = result;
                    }
                }
            }
        }

        private void method_10(IFeatureBuffer ifeatureBuffer_0, ICoFeature icoFeature_0)
        {
            foreach (ICoField field in icoFeature_0.Layer.Fields)
            {
                ICoField field2 = field;
                if (this.coLayerMapper_0 != null)
                {
                    field2 = this.coLayerMapper_0.FindDestField(field);
                }
                if (field2 == null)
                {
                    field2 = field;
                }
                int index = ifeatureBuffer_0.Fields.FindField(field2.Name);
                if ((index > -1) && (index < ifeatureBuffer_0.Fields.FieldCount))
                {
                    Exception exception;
                    try
                    {
                        object obj2 = icoFeature_0.GetValue(field.Name);
                        IField field3 = ifeatureBuffer_0.Fields.get_Field(index);
                        if (field3 != null)
                        {
                            object obj3 = Class4.ToObjectFun(obj2, field3);
                            switch (field3.Type)
                            {
                                case esriFieldType.esriFieldTypeOID:
                                case esriFieldType.esriFieldTypeGeometry:
                                {
                                    continue;
                                }
                            }
                            ifeatureBuffer_0.set_Value(index, obj3);
                        }
                        else
                        {
                            try
                            {
                                ifeatureBuffer_0.set_Value(index, obj2);
                            }
                            catch (Exception exception1)
                            {
                                exception = exception1;
                                Debug.WriteLine(exception.Message);
                            }
                        }
                    }
                    catch (Exception exception2)
                    {
                        exception = exception2;
                        Debug.WriteLine(exception.Message);
                    }
                }
            }
        }

        private ITextElement method_11(string string_0, double double_0, decimal decimal_0, string string_1, int int_0, IPoint ipoint_0)
        {
            ITextSymbol symbol = new TextSymbol {
                HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft,
                VerticalAlignment = esriTextVerticalAlignment.esriTVACenter,
                Angle = double_0,
                Size = Convert.ToDouble(decimal_0)
            };
            IFontDisp disp = (IFontDisp) new StdFont();
            disp.Size = decimal_0;
            disp.Name = string_1;
            disp.Bold = false;
            disp.Italic = false;
            disp.Underline = false;
            disp.Strikethrough = false;
            symbol.Font = disp;
            IRgbColor color = new RgbColor();
            color = this.method_13(int_0);
            symbol.Color = color;
            ITextElement element = new TextElement() as ITextElement;
            element.ScaleText = false;
            element.Text = string_0;
            element.Symbol = symbol;
            
            IElement element2 = (IElement) element;
            if (ipoint_0 != null)
            {
                element2.Geometry = ipoint_0;
            }
            return element;
        }

        private object method_12(esriFieldType esriFieldType_0)
        {
            switch (esriFieldType_0)
            {
                case esriFieldType.esriFieldTypeSmallInteger:
                case esriFieldType.esriFieldTypeInteger:
                case esriFieldType.esriFieldTypeSingle:
                case esriFieldType.esriFieldTypeOID:
                case esriFieldType.esriFieldTypeGlobalID:
                    return 0;

                case esriFieldType.esriFieldTypeDouble:
                    return 0.0;

                case esriFieldType.esriFieldTypeString:
                case esriFieldType.esriFieldTypeGUID:
                case esriFieldType.esriFieldTypeXML:
                    return " ";

                case esriFieldType.esriFieldTypeDate:
                    return "1900-1-1";

                case esriFieldType.esriFieldTypeGeometry:
                case esriFieldType.esriFieldTypeBlob:
                case esriFieldType.esriFieldTypeRaster:
                    return DBNull.Value;
            }
            return DBNull.Value;
        }

        private IRgbColor method_13(int int_0)
        {
            int num = int_0;
            IRgbColor color = new RgbColor();
            int num2 = int_0 / 0x10000;
            num -= num2 * 0x10000;
            int num3 = num / 0x100;
            num -= num3 * 0x100;
            int num4 = num;
            color.Red = num2;
            color.Green = num3;
            color.Blue = num4;
            color.NullColor = false;
            return color;
        }

        private IPolyline method_14(IPoint[] ipoint_0)
        {
            object before = Missing.Value;
            IPointCollection points = new Polyline();
            foreach (IPoint point in ipoint_0)
            {
                points.AddPoint(point, ref before, ref before);
            }
            return (points as IPolyline);
        }

        private IPoint[] method_15(CoPointCollection coPointCollection_0)
        {
            List<IPoint> list = new List<IPoint>();
            foreach (ICoPoint point in coPointCollection_0)
            {
                IPoint item = new ESRI.ArcGIS.Geometry.Point() 
                {
                    X = point.X,
                    Y = point.Y,
                    Z = point.Z
                } as IPoint;
                list.Add(item);
            }
            return list.ToArray();
        }

        private string method_16(IDataset idataset_0)
        {
            if (idataset_0 != null)
            {
                string str;
                string str2;
                string str3;
                (idataset_0.Workspace as ISQLSyntax).ParseTableName(idataset_0.Name, out str, out str2, out str3);
                return str3;
            }
            return "";
        }

        private void method_2(ICoFeature icoFeature_0, IFeature ifeature_0)
        {
            ICoPolygonFeature feature = icoFeature_0 as ICoPolygonFeature;
            IPolygon shape = ifeature_0.Shape as IPolygon;
            if ((feature != null) && (shape != null))
            {
                IGeometryCollection geometrys = shape as IGeometryCollection;
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

        private void method_3(ICoFeature icoFeature_0, IFeature ifeature_0)
        {
            ICoPolylineFeature feature = icoFeature_0 as ICoPolylineFeature;
            IPolyline shape = ifeature_0.Shape as IPolyline;
            if ((feature != null) && (shape != null))
            {
                IGeometryCollection geometrys = shape as IGeometryCollection;
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

        private void method_4(ICoFeature icoFeature_0, IFeature ifeature_0)
        {
            ICoPointFeature feature = icoFeature_0 as ICoPointFeature;
            IPoint shape = ifeature_0.Shape as IPoint;
            if ((feature != null) && (shape != null))
            {
                feature.Point.Add(new CoPointClass(shape.X, shape.Y, shape.Z));
            }
        }

        private void method_5(ICoFeature icoFeature_0, IFeature ifeature_0)
        {
            ICoAnnotationFeature feature = icoFeature_0 as ICoAnnotationFeature;
            IAnnotationFeature feature2 = ifeature_0 as IAnnotationFeature;
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

        private List<IRing> method_6(IPolygon ipolygon_0)
        {
            IPolygon4 polygon = ipolygon_0 as IPolygon4;
            List<IRing> list = new List<IRing>();
            IEnumGeometry exteriorRingBag = polygon.ExteriorRingBag as IEnumGeometry;
            exteriorRingBag.Reset();
            for (IRing ring = exteriorRingBag.Next() as IRing; ring != null; ring = exteriorRingBag.Next() as IRing)
            {
                list.Add(ring);
            }
            return list;
        }

        private List<IRing> method_7(IRing iring_0, IPolygon ipolygon_0)
        {
            List<IRing> list = new List<IRing>();
            IPolygon4 polygon = ipolygon_0 as IPolygon4;
            IEnumGeometry geometry = polygon.get_InteriorRingBag(iring_0) as IEnumGeometry;
            geometry.Reset();
            for (IRing ring = geometry.Next() as IRing; ring != null; ring = geometry.Next() as IRing)
            {
                list.Add(ring);
            }
            return list;
        }

        private List<IRing> method_8(IPolygon ipolygon_0)
        {
            List<IRing> list = new List<IRing>();
            foreach (IRing ring in this.method_6(ipolygon_0))
            {
                foreach (IRing ring2 in this.method_7(ring, ipolygon_0))
                {
                    list.Add(ring2);
                }
            }
            return list;
        }

        private IFeatureBuffer method_9(ICoFeature icoFeature_0)
        {
            object before = Missing.Value;
            IFeatureBuffer buffer = null;
            if (this.bool_0)
            {
                buffer = this.ifeatureClass_0.CreateFeatureBuffer();
            }
            else
            {
                buffer = this.ifeatureClass_0.CreateFeature() as IFeatureBuffer;
            }
            if (buffer != null)
            {
                buffer.Shape = null;
            }
            switch (icoFeature_0.Type)
            {
                case CoFeatureType.Point:
                    if ((this.ifeatureClass_0.FeatureType != esriFeatureType.esriFTAnnotation) && (this.ifeatureClass_0.ShapeType == esriGeometryType.esriGeometryPoint))
                    {
                        foreach (IPoint point in this.method_15((icoFeature_0 as ICoPointFeature).Point))
                        {
                            buffer.Shape = point;
                        }
                    }
                    break;

                case CoFeatureType.Polygon:
                    if ((this.ifeatureClass_0.FeatureType != esriFeatureType.esriFTAnnotation) && (this.ifeatureClass_0.ShapeType == esriGeometryType.esriGeometryPolygon))
                    {
                        IGeometryCollection geometrys2 = new Polygon() as IGeometryCollection;
                        foreach (CoPointCollection points in (icoFeature_0 as ICoPolygonFeature).Points)
                        {
                            IPointCollection points3 = new Ring();
                            foreach (IPoint point2 in this.method_15(points))
                            {
                                points3.AddPoint(point2, ref before, ref before);
                            }
                            geometrys2.AddGeometry((IGeometry) points3, ref before, ref before);
                        }
                        ((IPolygon) geometrys2).SimplifyPreserveFromTo();
                        buffer.Shape = (IGeometry) geometrys2;
                    }
                    break;

                case CoFeatureType.Annotation:
                    if (this.ifeatureClass_0.FeatureType == esriFeatureType.esriFTAnnotation)
                    {
                        IAnnotationFeature feature = buffer as IAnnotationFeature;
                        IPoint point = new ESRI.ArcGIS.Geometry.Point() as IPoint;
                        ICoAnnotationFeature feature2 = icoFeature_0 as ICoAnnotationFeature;
                        point.X = feature2.Point[0].X;
                        point.Y = feature2.Point[0].Y;
                        point.Z = feature2.Point[0].Z;
                        ITextElement element = this.method_11(feature2.Text, 0.0, (decimal) feature2.Font.Size, feature2.Font.Name, feature2.Color.ToArgb(), point);
                        feature.Annotation = (IElement) element;
                    }
                    break;

                case CoFeatureType.Polyline:
                    if ((this.ifeatureClass_0.FeatureType != esriFeatureType.esriFTAnnotation) && (this.ifeatureClass_0.ShapeType == esriGeometryType.esriGeometryPolyline))
                    {
                        IGeometryCollection geometrys = new Polyline() as IGeometryCollection;
                        foreach (CoPointCollection points in (icoFeature_0 as ICoPolylineFeature).Points)
                        {
                            IPoint[] pointArray = this.method_15(points);
                            IPointCollection points2 = new ESRI.ArcGIS.Geometry.Path() as IPointCollection;
                            for (int i = 0; i < pointArray.Length; i++)
                            {
                                points2.AddPoint(pointArray[i], ref before, ref before);
                            }
                            geometrys.AddGeometry((IGeometry) points2, ref before, ref before);
                        }
                        buffer.Shape = (IGeometry) geometrys;
                    }
                    break;
            }
            if (buffer != null)
            {
                if (buffer.Shape != null)
                {
                    this.method_10(buffer, icoFeature_0);
                    return buffer;
                }
                return null;
            }
            return null;
        }

        public override int NextFeature()
        {
            IFeature feature = this.ifeatureCursor_0.NextFeature();
            if (feature == null)
            {
                return -1;
            }
            ICoFeature feature2 = this.method_0(feature);
            if (feature2 != null)
            {
                base.XpgisLayer.AppendFeature(feature2);
            }
            if (feature.HasOID)
            {
                return feature.OID;
            }
            return 0;
        }

        public override void Reset()
        {
            this.ifeatureCursor_0 = this.ifeatureClass_0.Search(null, false);
        }

        protected override void UpdateLayerStruct()
        {
            base.XpgisLayer.Fields.Clear();
            if (this.ifeatureClass_0 != null)
            {
                base.XpgisLayer.AliasName = this.ifeatureClass_0.AliasName;
                base.XpgisLayer.Name = this.method_16(this.ifeatureClass_0 as IDataset);
                for (int i = 0; i < this.ifeatureClass_0.Fields.FieldCount; i++)
                {
                    ICoField item = new CoFieldClass();
                    IField field2 = this.ifeatureClass_0.Fields.get_Field(i);
                    switch (field2.Type)
                    {
                        case esriFieldType.esriFieldTypeSmallInteger:
                        case esriFieldType.esriFieldTypeInteger:
                        case esriFieldType.esriFieldTypeOID:
                            item.Type = CoFieldType.整型;
                            break;

                        case esriFieldType.esriFieldTypeSingle:
                        case esriFieldType.esriFieldTypeDouble:
                            item.Type = CoFieldType.浮点型;
                            break;

                        case esriFieldType.esriFieldTypeString:
                            item.Type = CoFieldType.字符型;
                            break;

                        case esriFieldType.esriFieldTypeDate:
                            item.Type = CoFieldType.日期型;
                            break;

                        case esriFieldType.esriFieldTypeBlob:
                            item.Type = CoFieldType.二进制;
                            break;
                    }
                    item.Name = field2.Name;
                    item.AliasName = field2.AliasName;
                    item.Length = field2.Length;
                    item.Precision = field2.Precision;
                    item.Required = field2.Required;
                    base.XpgisLayer.Fields.Add(item);
                }
            }
        }

        public override int FeatureCount
        {
            get
            {
                if (this.ifeatureClass_0 != null)
                {
                    return this.ifeatureClass_0.FeatureCount(null);
                }
                return -1;
            }
        }

        private class Class4
        {
            private static MemoryBlobStream ToBlob(object object_0)
            {
                MemoryBlobStream class2 = new MemoryBlobStream();
                if (object_0 != null)
                {
                    string path = object_0.ToString();
                    try
                    {
                        if (File.Exists(path))
                        {
                            class2.LoadFromFile(path);
                        }
                        else
                        {
                            return class2;
                        }
                    }
                    catch
                    {
                    }
                }
                return class2;
            }

            private static DateTime ToDateTimeFun(object object_0, DateTime dateTime_0)
            {
                try
                {
                    DateTime now = DateTime.Now;
                    DateTime.TryParse(object_0.ToString(), out now);
                    return now;
                }
                catch
                {
                    return dateTime_0;
                }
            }

            private static double ToDoubleFun(object object_0, double double_0)
            {
                double result = 0.0;
                if ((object_0 == null) || (object_0.ToString().Trim() == ""))
                {
                    return result;
                }
                try
                {
                    if (double.TryParse(object_0.ToString(), out result))
                    {
                        return result;
                    }
                    return double_0;
                }
                catch
                {
                    return double_0;
                }
            }

            private static int ToIntFun(object object_0, int int_0)
            {
                int result = 0;
                if ((object_0 != null) && (object_0.ToString().Trim() != ""))
                {
                    try
                    {
                        if (object_0.ToString().IndexOf('.') != -1)
                        {
                            double num3 = 0.0;
                            double.TryParse(object_0.ToString(), out num3);
                            result = Convert.ToInt32(num3);
                        }
                        else
                        {
                            int.TryParse(object_0.ToString(), out result);
                        }
                    }
                    catch
                    {
                        result = 0;
                    }
                }
                return result;
            }

            public static object ToObjectFun(object object_0, IField ifield_0)
            {
                object obj2 = null;
                switch (ifield_0.Type)
                {
                    case esriFieldType.esriFieldTypeSmallInteger:
                        return ToSmallIntFun(object_0, 0);

                    case esriFieldType.esriFieldTypeInteger:
                        return ToIntFun(object_0, 0);

                    case esriFieldType.esriFieldTypeSingle:
                    case esriFieldType.esriFieldTypeDouble:
                        return ToDoubleFun(object_0, 0.0);

                    case esriFieldType.esriFieldTypeString:
                        obj2 = ToStringFun(object_0, "");
                        if (obj2.ToString().Length > ifield_0.Length)
                        {
                            obj2 = obj2.ToString().Substring(0, ifield_0.Length);
                        }
                        return obj2;

                    case esriFieldType.esriFieldTypeDate:
                        return ToDateTimeFun(object_0, DateTime.Now);

                    case esriFieldType.esriFieldTypeOID:
                    case esriFieldType.esriFieldTypeGeometry:
                        return obj2;

                    case esriFieldType.esriFieldTypeBlob:
                        return ToBlob(object_0);
                }
                return obj2;
            }

            private static short ToSmallIntFun(object object_0, short short_0)
            {
                short num = 0;
                if ((object_0 != null) && (object_0.ToString().Trim() != ""))
                {
                    try
                    {
                        if (object_0.ToString().IndexOf('.') != -1)
                        {
                            double result = 0.0;
                            double.TryParse(object_0.ToString(), out result);
                            num = Convert.ToInt16(result);
                        }
                        else
                        {
                            int num4 = 0;
                            int.TryParse(object_0.ToString(), out num4);
                            try
                            {
                                num = (short) num4;
                            }
                            catch
                            {
                                num = 0;
                            }
                        }
                    }
                    catch
                    {
                        num = 0;
                    }
                }
                return num;
            }

            private static string ToStringFun(object object_0, string string_0)
            {
                if (object_0 == null)
                {
                    return "";
                }
                try
                {
                    return object_0.ToString();
                }
                catch
                {
                    return string_0;
                }
            }
        }
    }
}

