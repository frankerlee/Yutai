namespace Yutai.Catalog.VCT
{
   
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;


    public class ShapeLayerClass : AbsCoConvert
    {
        private CoLayerMapper coLayerMapper_0 = null;
        private double[] double_0;
        private double[] double_1;
        private IntPtr intptr_0 = IntPtr.Zero;
        private IntPtr intptr_1 = IntPtr.Zero;
        private IntPtr intptr_2 = IntPtr.Zero;
        private List<string> list_0 = new List<string>();
        private ShapeTool shapeTool_0 = new ShapeTool();
        private ShapeLib.ShapeType shapeType_0 = ShapeLib.ShapeType.Point;
        private string string_0;

        public ShapeLayerClass(string string_1, string string_2, ICoLayer icoLayer_1)
        {
            this.method_0(string_1);
            this.list_0.Clear();
            this.string_0 = string_1.ToUpper();
            if (this.string_0.EndsWith(".SHP"))
            {
                this.string_0 = this.string_0.Substring(0, this.string_0.Length - 4);
            }
            if (string_2.ToLower().Trim() == "point")
            {
                this.shapeType_0 = ShapeLib.ShapeType.Point;
            }
            if (string_2.ToLower().Trim() == "line")
            {
                this.shapeType_0 = ShapeLib.ShapeType.PolyLine;
            }
            if (string_2.ToLower().Trim() == "polygon")
            {
                this.shapeType_0 = ShapeLib.ShapeType.Polygon;
            }
            if (string_2.ToLower().Trim() == "text")
            {
                this.shapeType_0 = ShapeLib.ShapeType.Point;
            }
            this.intptr_0 = ShapeLib.SHPCreate(this.string_0, this.shapeType_0);
            if (!this.intptr_0.Equals(IntPtr.Zero))
            {
                this.intptr_1 = ShapeLib.DBFCreate(this.string_0);
                if (!this.intptr_1.Equals(IntPtr.Zero))
                {
                    this.method_1(icoLayer_1);
                }
            }
        }

        public override void Close()
        {
            try
            {
                if (this.intptr_0 != IntPtr.Zero)
                {
                    ShapeLib.SHPClose(this.intptr_0);
                    this.intptr_0 = IntPtr.Zero;
                }
            }
            catch
            {
            }
            try
            {
                if (this.intptr_1 != IntPtr.Zero)
                {
                    ShapeLib.DBFClose(this.intptr_1);
                    this.intptr_1 = IntPtr.Zero;
                }
            }
            catch
            {
            }
        }

        public override void Flush()
        {
            this.Flush(null);
        }

        public override void Flush(CoLayerMapper coLayerMapper_1)
        {
            try
            {
                this.coLayerMapper_0 = coLayerMapper_1;
                for (int i = 0; i < base.XpgisLayer.FeatureCount; i++)
                {
                    ICoFeature featureByIndex = base.XpgisLayer.GetFeatureByIndex(i);
                    this.method_2(featureByIndex);
                }
                base.XpgisLayer.RemoveAllFeature();
            }
            catch
            {
                base.XpgisLayer.RemoveAllFeature();
            }
        }

        public Vertex getInterMul(Vertex vertex_0, Vertex vertex_1)
        {
            Vertex vertex = new Vertex();
            vertex.set(0.0, 0.0, 0.0);
            vertex.I = (vertex_0.J * vertex_1.I) - (vertex_0.K * vertex_1.J);
            vertex.J = (vertex_0.K * vertex_1.I) - (vertex_0.I * vertex_1.K);
            vertex.K = (vertex_0.I * vertex_1.J) - (vertex_0.J * vertex_1.I);
            return vertex;
        }

        public double getJJ(Vertex vertex_0, Vertex vertex_1)
        {
            double d = 0.0;
            double num2 = 0.0;
            num2 = ((vertex_0.I * vertex_1.I) + (vertex_0.J * vertex_1.J)) + (vertex_0.K * vertex_1.K);
            double num3 = Math.Sqrt((Math.Pow(vertex_0.I, 2.0) + Math.Pow(vertex_0.J, 2.0)) + Math.Pow(vertex_0.K, 2.0));
            double num4 = Math.Sqrt((Math.Pow(vertex_1.I, 2.0) + Math.Pow(vertex_1.J, 2.0)) + Math.Pow(vertex_1.K, 2.0));
            d = num2 / (num3 * num4);
            double a = 0.0;
            a = (Math.Acos(d) * 180.0) / 3.1415926535897931;
            a = Math.Round(a);
            return (180.0 - a);
        }

        public Vertex getVertex(ICoPoint icoPoint_0, ICoPoint icoPoint_1)
        {
            Vertex vertex = new Vertex();
            vertex.set(0.0, 0.0, 0.0);
            vertex.set(icoPoint_1.X - icoPoint_0.X, icoPoint_1.Y - icoPoint_0.Y, icoPoint_1.Z - icoPoint_0.Z);
            return vertex;
        }

        private void method_0(string string_1)
        {
            try
            {
                FileInfo info = new FileInfo(string_1);
                string fullName = info.Directory.FullName;
                string name = info.Name;
                if (name.ToUpper().EndsWith(".SHP"))
                {
                    name = name.Substring(0, name.Length - 4);
                }
                string path = fullName + @"\" + name + ".SHP";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                path = fullName + @"\" + name + ".DBF";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                path = fullName + @"\" + name + ".SHX";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.ToString());
            }
        }

        private void method_1(ICoLayer icoLayer_1)
        {
            for (int i = 0; i < icoLayer_1.Fields.Count; i++)
            {
                ICoField field = icoLayer_1.Fields[i];
                string name = field.Name;
                switch (field.Type)
                {
                    case CoFieldType.整型:
                        ShapeLib.DBFAddField(this.intptr_1, name, ShapeLib.DBFFieldType.FTInteger, 0x10, 0);
                        this.list_0.Add(name);
                        break;

                    case CoFieldType.浮点型:
                    {
                        int length = field.Length;
                        int precision = field.Precision;
                        ShapeLib.DBFAddField(this.intptr_1, name, ShapeLib.DBFFieldType.FTDouble, length, precision);
                        this.list_0.Add(name);
                        break;
                    }
                    case CoFieldType.字符型:
                    {
                        int num4 = field.Length;
                        ShapeLib.DBFAddField(this.intptr_1, name, ShapeLib.DBFFieldType.FTString, num4, 0);
                        this.list_0.Add(name);
                        break;
                    }
                    case CoFieldType.日期型:
                        ShapeLib.DBFAddField(this.intptr_1, name, ShapeLib.DBFFieldType.FTInteger, 8, 0);
                        this.list_0.Add(name);
                        break;

                    case CoFieldType.二进制:
                        ShapeLib.DBFAddField(this.intptr_1, name, ShapeLib.DBFFieldType.FTLogical, 5, 0);
                        this.list_0.Add(name);
                        break;
                }
            }
        }

        private void method_2(ICoFeature icoFeature_0)
        {
            try
            {
                int[] numArray;
                ShapeLib.PartType[] typeArray;
                int num5;
                int count = 0;
                int num2 = 0;
                int num3 = 0;
                int index = 0;
                switch (icoFeature_0.Type)
                {
                    case CoFeatureType.Point:
                        foreach (ICoPoint point in (icoFeature_0 as ICoPointFeature).Point)
                        {
                            this.double_0 = new double[1];
                            this.double_1 = new double[1];
                            this.double_0[0] = point.X;
                            this.double_1[0] = point.Y;
                            this.intptr_2 = ShapeLib.SHPCreateObject(this.shapeType_0, -1, 0, null, null, 1, this.double_0, this.double_1, null, null);
                            num2 = ShapeLib.SHPWriteObject(this.intptr_0, -1, this.intptr_2);
                            ShapeLib.SHPDestroyObject(this.intptr_2);
                            this.method_3(icoFeature_0, num2);
                        }
                        return;

                    case CoFeatureType.Line:
                    case CoFeatureType.Annotation:
                        return;

                    case CoFeatureType.Polygon:
                    {
                        ICoPolygonFeature feature2 = icoFeature_0 as ICoPolygonFeature;
                        if (feature2 != null)
                        {
                            count = feature2.Points.Count;
                            numArray = new int[count];
                            typeArray = new ShapeLib.PartType[count];
                            typeArray[0] = ShapeLib.PartType.Ring;
                            numArray[0] = 0;
                            num3 = feature2.Points[0].Count;
                            for (num5 = 1; num5 < count; num5++)
                            {
                                numArray[num5] = num3;
                                typeArray[num5] = ShapeLib.PartType.Ring;
                                num3 += feature2.Points[num5].Count;
                            }
                            this.double_0 = new double[num3];
                            this.double_1 = new double[num3];
                            int num6 = 0;
                            foreach (CoPointCollection points in feature2.Points)
                            {
                                int num8;
                                double num7 = this.method_8(points);
                                if (num6 == 0)
                                {
                                    if (num7 > 0.0)
                                    {
                                        foreach (ICoPoint point2 in points)
                                        {
                                            this.double_0[index] = point2.X;
                                            this.double_1[index] = point2.Y;
                                            index++;
                                        }
                                    }
                                    else
                                    {
                                        num8 = points.Count - 1;
                                        while (num8 >= 0)
                                        {
                                            this.double_0[index] = points[num8].X;
                                            this.double_1[index] = points[num8].Y;
                                            index++;
                                            num8--;
                                        }
                                    }
                                }
                                else if (num7 > 0.0)
                                {
                                    for (num8 = points.Count - 1; num8 >= 0; num8--)
                                    {
                                        this.double_0[index] = points[num8].X;
                                        this.double_1[index] = points[num8].Y;
                                        index++;
                                    }
                                }
                                else
                                {
                                    foreach (ICoPoint point2 in points)
                                    {
                                        this.double_0[index] = point2.X;
                                        this.double_1[index] = point2.Y;
                                        index++;
                                    }
                                }
                                num6++;
                            }
                            this.intptr_2 = ShapeLib.SHPCreateObject(this.shapeType_0, -1, count, numArray, typeArray, num3, this.double_0, this.double_1, null, null);
                            try
                            {
                                num2 = ShapeLib.SHPWriteObject(this.intptr_0, -1, this.intptr_2);
                                ShapeLib.SHPDestroyObject(this.intptr_2);
                                this.method_3(icoFeature_0, num2);
                            }
                            catch
                            {
                            }
                        }
                        return;
                    }
                    case CoFeatureType.Polyline:
                        break;

                    default:
                        return;
                }
                ICoPolylineFeature feature = icoFeature_0 as ICoPolylineFeature;
                count = feature.Points.Count;
                numArray = new int[count];
                typeArray = new ShapeLib.PartType[count];
                typeArray[0] = ShapeLib.PartType.Ring;
                numArray[0] = 0;
                num3 = feature.Points[0].Count;
                for (num5 = 1; num5 < count; num5++)
                {
                    numArray[num5] = num3;
                    typeArray[num5] = ShapeLib.PartType.Ring;
                    num3 += feature.Points[num5].Count;
                }
                this.double_0 = new double[num3];
                this.double_1 = new double[num3];
                foreach (CoPointCollection points in feature.Points)
                {
                    foreach (ICoPoint point2 in points)
                    {
                        this.double_0[index] = point2.X;
                        this.double_1[index] = point2.Y;
                        index++;
                    }
                }
                this.intptr_2 = ShapeLib.SHPCreateObject(this.shapeType_0, -1, count, numArray, typeArray, num3, this.double_0, this.double_1, null, null);
                num2 = ShapeLib.SHPWriteObject(this.intptr_0, -1, this.intptr_2);
                ShapeLib.SHPDestroyObject(this.intptr_2);
                this.method_3(icoFeature_0, num2);
            }
            finally
            {
            }
        }

        private void method_3(ICoFeature icoFeature_0, int int_0)
        {
            try
            {
                int num2;
                DateTime time;
                if (this.coLayerMapper_0 == null)
                {
                    goto Label_016A;
                }
                int num = 0;
            Label_0011:
                if (num >= this.coLayerMapper_0.FieldRelation.Count)
                {
                    return;
                }
                CoFieldMapper mapper = this.coLayerMapper_0.FieldRelation[num];
                ICoField destField = mapper.DestField;
                ICoField sourceField = mapper.SourceField;
                object obj2 = "";
                try
                {
                    obj2 = icoFeature_0.GetValue(sourceField);
                }
                catch
                {
                    obj2 = "";
                }
                goto Label_010A;
            Label_006B:
                ShapeLib.DBFWriteStringAttribute(this.intptr_1, int_0, num2, obj2.ToString().ToUpper());
                goto Label_0101;
            Label_0088:
                ShapeLib.DBFWriteIntegerAttribute(this.intptr_1, int_0, num2, this.method_7(obj2.ToString()));
                goto Label_0101;
            Label_00A6:
                ShapeLib.DBFWriteDoubleAttribute(this.intptr_1, int_0, num2, this.method_6(obj2.ToString()));
                goto Label_0101;
            Label_00C4:
                ShapeLib.DBFWriteLogicalAttribute(this.intptr_1, int_0, num2, this.method_5(obj2.ToString()));
                goto Label_0101;
            Label_00E2:
                time = DateTime.Parse(obj2.ToString());
                ShapeLib.DBFWriteDateAttribute(this.intptr_1, int_0, num2, time);
            Label_0101:
                num++;
                goto Label_0011;
            Label_010A:
                num2 = ShapeLib.DBFGetFieldIndex(this.intptr_1, destField.Name);
                int num3 = 0;
                int num4 = 0;
                StringBuilder builder = new StringBuilder(destField.Name);
                switch (ShapeLib.DBFGetFieldInfo(this.intptr_1, num2, builder, ref num3, ref num4))
                {
                    case ShapeLib.DBFFieldType.FTString:
                        goto Label_006B;

                    case ShapeLib.DBFFieldType.FTInteger:
                        goto Label_0088;

                    case ShapeLib.DBFFieldType.FTDouble:
                        goto Label_00A6;

                    case ShapeLib.DBFFieldType.FTLogical:
                        goto Label_00C4;

                    case ShapeLib.DBFFieldType.FTInvalid:
                        goto Label_0101;

                    case ShapeLib.DBFFieldType.FTDate:
                        goto Label_00E2;

                    default:
                        goto Label_0101;
                }
            Label_016A:
                using (List<ICoField>.Enumerator enumerator = icoFeature_0.Layer.Fields.GetEnumerator())
                {
                Label_017D:
                    if (!enumerator.MoveNext())
                    {
                        return;
                    }
                    ICoField current = enumerator.Current;
                    obj2 = "";
                    try
                    {
                        obj2 = icoFeature_0.GetValue(current);
                    }
                    catch
                    {
                        obj2 = "";
                    }
                    goto Label_0256;
                Label_01B5:
                    ShapeLib.DBFWriteStringAttribute(this.intptr_1, int_0, num2, obj2.ToString().ToUpper());
                    goto Label_017D;
                Label_01D2:
                    ShapeLib.DBFWriteIntegerAttribute(this.intptr_1, int_0, num2, this.method_7(obj2.ToString()));
                    goto Label_017D;
                Label_01F0:
                    ShapeLib.DBFWriteDoubleAttribute(this.intptr_1, int_0, num2, this.method_6(obj2.ToString()));
                    goto Label_017D;
                Label_0211:
                    ShapeLib.DBFWriteLogicalAttribute(this.intptr_1, int_0, num2, this.method_5(obj2.ToString()));
                    goto Label_017D;
                Label_0232:
                    time = DateTime.Parse(obj2.ToString());
                    ShapeLib.DBFWriteDateAttribute(this.intptr_1, int_0, num2, time);
                    goto Label_017D;
                Label_0256:
                    num2 = ShapeLib.DBFGetFieldIndex(this.intptr_1, current.Name);
                    num3 = 0;
                    num4 = 0;
                    builder = new StringBuilder(current.Name);
                    switch (ShapeLib.DBFGetFieldInfo(this.intptr_1, num2, builder, ref num3, ref num4))
                    {
                        case ShapeLib.DBFFieldType.FTString:
                            goto Label_01B5;

                        case ShapeLib.DBFFieldType.FTInteger:
                            goto Label_01D2;

                        case ShapeLib.DBFFieldType.FTDouble:
                            goto Label_01F0;

                        case ShapeLib.DBFFieldType.FTLogical:
                            goto Label_0211;

                        case ShapeLib.DBFFieldType.FTDate:
                            goto Label_0232;
                    }
                    goto Label_017D;
                }
            }
            catch
            {
            }
        }

        private int method_4(string string_1)
        {
            for (int i = 0; i < this.list_0.Count; i++)
            {
                if (string_1 == this.list_0[i].ToUpper())
                {
                    return i;
                }
            }
            return 0;
        }

        private bool method_5(string string_1)
        {
            try
            {
                return bool.Parse(string_1);
            }
            catch
            {
                return false;
            }
        }

        private double method_6(string string_1)
        {
            double result = 0.0;
            try
            {
                double.TryParse(string_1, out result);
                return result;
            }
            catch
            {
                return result;
            }
        }

        private int method_7(string string_1)
        {
            int result = 0;
            try
            {
                int.TryParse(string_1, out result);
                return result;
            }
            catch
            {
                return result;
            }
        }

        private double method_8(CoPointCollection coPointCollection_0)
        {
            double num = 0.0;
            double num2 = 0.0;
            int count = coPointCollection_0.Count;
            CoPointCollection points = coPointCollection_0;
            for (int i = 0; i < (points.Count - 1); i++)
            {
                if (i == (coPointCollection_0.Count - 1))
                {
                    num2 = ((coPointCollection_0[0].Y + coPointCollection_0[i].Y) * (coPointCollection_0[0].X - coPointCollection_0[i].X)) / 2.0;
                }
                else
                {
                    num2 = ((coPointCollection_0[i + 1].Y + coPointCollection_0[i].Y) * (coPointCollection_0[i + 1].X - coPointCollection_0[i].X)) / 2.0;
                }
                num += num2;
            }
            return num;
        }

        public override int NextFeature()
        {
            throw new NotImplementedException();
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }

        protected override void UpdateLayerStruct()
        {
            throw new NotImplementedException();
        }

        public override int FeatureCount
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Vertex
        {
            public double I;
            public double J;
            public double K;
            public void set(double double_0, double double_1, double double_2)
            {
                this.I = double_0;
                this.J = double_1;
                this.K = double_2;
            }
        }
    }
}

