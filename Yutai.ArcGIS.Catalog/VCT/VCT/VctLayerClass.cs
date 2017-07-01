using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace Yutai.ArcGIS.Catalog.VCT.VCT
{
    public sealed class VctLayerClass : AbsCoConvert
    {
        private ICoLayer icoLayer_1 = null;
        private int int_0 = -1;
        private long long_0 = 0L;
        private long long_1 = 0L;
        private long long_2 = 0L;
        private long long_3 = 0L;
        private long long_4 = 0L;
        private StreamReader streamReader_0 = null;
        private StreamReader streamReader_1 = null;
        private string string_0 = "访问第{0}行时出现错误，详细错误信息如下：\r\n{1}";
        private VctClass vctClass_0 = null;

        public VctLayerClass(VctClass vctClass_1, ICoLayer icoLayer_2)
        {
            this.icoLayer_1 = icoLayer_2;
            this.vctClass_0 = vctClass_1;
            base.XpgisLayer = icoLayer_2;
            this.long_0 = vctClass_1.GetLayerGraphIndex(icoLayer_2);
            this.long_2 = vctClass_1.GetLayerAttributeIndex(icoLayer_2);
        }

        public override void Close()
        {
            if (this.streamReader_0 != null)
            {
                this.streamReader_0 = null;
            }
            if (this.streamReader_1 != null)
            {
                this.streamReader_1 = null;
            }
        }

        public override void Dispose()
        {
            this.Close();
            base.Dispose();
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override void Flush(CoLayerMapper coLayerMapper_0)
        {
            throw new NotImplementedException();
        }

        private CoPointCollection method_0(int int_1)
        {
            CoPointCollection points = new CoPointCollection();
            foreach (object obj2 in this.vctClass_0.GetFeatureLineIndex(int_1).PointArray)
            {
                if (obj2 is CoPointClass)
                {
                    points.Add(obj2 as CoPointClass);
                }
            }
            return points;
        }

        private string method_1(ref StreamReader streamReader_2)
        {
            this.long_3 += 1L;
            string str = streamReader_2.ReadLine();
            if (str != null)
            {
                return str;
            }
            return string.Empty;
        }

        public override int NextFeature()
        {
            ICoFeature feature = null;
            int num3;
            Exception exception;
            string str7;
            Color color;
            bool flag;
            string s = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            string[] strArray = new string[0];
            switch (base.XpgisLayer.LayerType)
            {
                case CoLayerType.Point:
                    goto Label_034A;

                case CoLayerType.Line:
                {
                    feature = new CoPolylineFeature(base.XpgisLayer);
                    s = string.Empty;
                    if (this.streamReader_0 == null)
                    {
                        if (this.long_1 != 0L)
                        {
                            this.streamReader_0 = this.vctClass_0.CreateReader(this.long_1 - 1L);
                            this.long_3 = this.long_1 - 1L;
                            this.long_1 = 0L;
                        }
                        else
                        {
                            this.streamReader_0 = this.vctClass_0.CreateReader(this.long_0 - 1L);
                            this.long_3 = this.long_0 - 1L;
                        }
                    }
                    s = this.method_1(ref this.streamReader_0);
                    while (s.Length <= 0)
                    {
                        s = this.method_1(ref this.streamReader_0);
                    }
                    str2 = this.method_1(ref this.streamReader_0);
                    if (this.vctClass_0.FindLayerIndex(str2, CoLayerType.Line) !=
                        this.vctClass_0.FindLayerIndexByName(base.XpgisLayer.Name))
                    {
                        return -1;
                    }
                    this.method_1(ref this.streamReader_0);
                    this.method_1(ref this.streamReader_0);
                    int num2 = 0;
                    int.TryParse(this.method_1(ref this.streamReader_0), out num2);
                    CoPointCollection points = new CoPointCollection();
                    for (num3 = 0; num3 < num2; num3++)
                    {
                        string str4 = this.method_1(ref this.streamReader_0);
                        try
                        {
                            CoPointClass class3 = this.vctClass_0.LineToPoint(str4);
                            points.Add(class3);
                        }
                        catch (Exception exception2)
                        {
                            exception = exception2;
                            this.vctClass_0.WriteLog(string.Format(this.string_0, this.long_3, "不能转换为坐标"));
                            this.streamReader_0.Close();
                            this.streamReader_0.Dispose();
                            this.streamReader_0 = null;
                            this.long_1 = this.long_3;
                            return -2;
                        }
                    }
                    (feature as CoPolylineFeature).Points.Add(points);
                    feature.OID = int.Parse(s);
                    goto Label_0AA8;
                }
                case CoLayerType.Region:
                {
                    int num4;
                    feature = new CoPolygonFeature(base.XpgisLayer);
                    s = string.Empty;
                    if (this.streamReader_0 == null)
                    {
                        this.streamReader_0 = this.vctClass_0.CreateReader(this.long_0 - 1L);
                        this.long_3 = this.long_0 - 1L;
                    }
                    for (s = this.method_1(ref this.streamReader_0);
                        s.Length <= 0;
                        s = this.method_1(ref this.streamReader_0))
                    {
                    }
                    str2 = this.method_1(ref this.streamReader_0);
                    if (this.vctClass_0.FindLayerIndex(str2, CoLayerType.Region) !=
                        this.vctClass_0.FindLayerIndexByName(base.XpgisLayer.Name))
                    {
                        return -1;
                    }
                    this.method_1(ref this.streamReader_0);
                    string str5 = this.method_1(ref this.streamReader_0);
                    if (this.vctClass_0.LineToPoint(str5) == null)
                    {
                        str5 = this.method_1(ref this.streamReader_0);
                    }
                    if (this.icoLayer_1.Parameter.Topo != 0)
                    {
                        if (this.icoLayer_1.Parameter.Topo == 1)
                        {
                            this.vctClass_0.LineToPoint(str5);
                            num4 = 0;
                            int.TryParse(this.method_1(ref this.streamReader_0).Trim(), out num4);
                            int num5 = 0;
                            num5 = num4/8;
                            if ((num4%8) > 0)
                            {
                                num5++;
                            }
                            (feature as CoPolygonFeature).Points.Add(new CoPointCollection());
                            for (num3 = 0; num3 < num5; num3++)
                            {
                                foreach (
                                    string str6 in
                                    this.method_1(ref this.streamReader_0).Split(new char[] {this.vctClass_0.Separator})
                                )
                                {
                                    int num7 = 0;
                                    int.TryParse(str6.Trim(), out num7);
                                    if (num7 != 0)
                                    {
                                        CoPointCollection points3 = this.method_0(Math.Abs(num7));
                                        if (num7 > 0)
                                        {
                                            foreach (CoPointClass class4 in points3)
                                            {
                                                (feature as CoPolygonFeature).Points[
                                                    (feature as CoPolygonFeature).Points.Count - 1].Add(class4);
                                            }
                                        }
                                        else if (num7 < 0)
                                        {
                                            for (int i = points3.Count - 1; i > -1; i--)
                                            {
                                                (feature as CoPolygonFeature).Points[
                                                    (feature as CoPolygonFeature).Points.Count - 1].Add(points3[i]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        (feature as CoPolygonFeature).Points.Add(new CoPointCollection());
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        this.vctClass_0.LineToPoint(str5);
                        num4 = 0;
                        int.TryParse(this.method_1(ref this.streamReader_0).Trim(), out num4);
                        while (num4 > 0)
                        {
                            (feature as CoPolygonFeature).Points.Add(new CoPointCollection());
                            num3 = 0;
                            while (num3 < num4)
                            {
                                CoPointClass class4 = this.vctClass_0.LineToPoint(this.streamReader_0.ReadLine().Trim());
                                if (class4 == null)
                                {
                                    this.vctClass_0.WriteLog(string.Format(this.string_0, this.long_3, "不能转换为坐标"));
                                    this.streamReader_0.Close();
                                    this.streamReader_0.Dispose();
                                    this.streamReader_0 = null;
                                    this.long_0 = this.long_3;
                                    return -2;
                                }
                                (feature as CoPolygonFeature).Points[(feature as CoPolygonFeature).Points.Count - 1].Add
                                    (class4);
                                num3++;
                            }
                            int.TryParse(this.method_1(ref this.streamReader_0).Trim(), out num4);
                        }
                    }
                    goto Label_0AA8;
                }
                case CoLayerType.Annotation:
                {
                    feature = new CoAnnotationFeature(base.XpgisLayer);
                    s = string.Empty;
                    if (this.streamReader_0 == null)
                    {
                        this.streamReader_0 = this.vctClass_0.CreateReader(this.long_0 - 1L);
                        this.long_3 = this.long_0 - 1L;
                    }
                    s = this.method_1(ref this.streamReader_0);
                    while (s.Trim().Length <= 0)
                    {
                        s = this.streamReader_0.ReadLine();
                    }
                    str2 = this.method_1(ref this.streamReader_0);
                    if (this.vctClass_0.FindLayerIndex(str2, CoLayerType.Annotation) !=
                        this.vctClass_0.FindLayerIndexByName(base.XpgisLayer.Name))
                    {
                        return -1;
                    }
                    this.method_1(ref this.streamReader_0);
                    str7 = this.method_1(ref this.streamReader_0).Trim();
                    color = this.vctClass_0.LineToColor(this.method_1(ref this.streamReader_0).Trim());
                    string[] strArray4 =
                        this.method_1(ref this.streamReader_0).Trim().Split(new char[] {this.vctClass_0.Separator});
                    int num9 = 0;
                    int.TryParse(strArray4[0].Trim(), out num9);
                    uint num10 = 0;
                    uint.TryParse(strArray4[1].Trim(), out num10);
                    flag = false;
                    string str8 = strArray4[2].Trim().ToUpper();
                    if ((str8 == null) || (!(str8 == "T") && !(str8 == "Y")))
                    {
                        flag = false;
                        break;
                    }
                    flag = true;
                    break;
                }
                default:
                    goto Label_0AA8;
            }
            float result = 8f;
            float.TryParse(this.method_1(ref this.streamReader_0).Trim(), out result);
            result /= 10f;
            float num12 = 8f;
            float.TryParse(this.method_1(ref this.streamReader_0).Trim(), out num12);
            num12 /= 8f;
            string str9 = this.method_1(ref this.streamReader_0).Trim();
            this.streamReader_0.ReadLine();
            CoPointClass item = this.vctClass_0.LineToPoint(this.method_1(ref this.streamReader_0).Trim());
            (feature as CoAnnotationFeature).Point.Add(item);
            (feature as CoAnnotationFeature).OID = int.Parse(s);
            (feature as CoAnnotationFeature).Color = color;
            (feature as CoAnnotationFeature).Text = str9;
            (feature as CoAnnotationFeature).Angle = 0.0;
            FontStyle regular = FontStyle.Regular;
            if (flag)
            {
                regular = FontStyle.Underline;
            }
            try
            {
                Font font = new Font(new FontFamily(str7), result, regular);
                (feature as CoAnnotationFeature).Font = font;
                goto Label_0AA8;
            }
            catch (Exception exception1)
            {
                exception = exception1;
                this.vctClass_0.WriteLog(string.Format(this.string_0, this.long_3, "不能转换为字体\r\n" + exception.ToString()));
                this.streamReader_0.Close();
                this.streamReader_0.Dispose();
                this.streamReader_0 = null;
                this.long_0 = this.long_3;
                return -2;
            }
            Label_034A:
            feature = new CoPointFeature(base.XpgisLayer);
            s = string.Empty;
            if (this.streamReader_0 == null)
            {
                this.streamReader_0 = this.vctClass_0.CreateReader(this.long_0 - 1L);
                this.long_3 = this.long_0 - 1L;
            }
            s = this.method_1(ref this.streamReader_0);
            while (s.Length <= 0)
            {
                s = this.method_1(ref this.streamReader_0);
            }
            str2 = this.method_1(ref this.streamReader_0);
            if (this.vctClass_0.FindLayerIndex(str2, CoLayerType.Point) !=
                this.vctClass_0.FindLayerIndexByName(base.XpgisLayer.Name))
            {
                return -1;
            }
            this.method_1(ref this.streamReader_0);
            this.method_1(ref this.streamReader_0);
            CoPointClass class2 = this.vctClass_0.LineToPoint(this.method_1(ref this.streamReader_0));
            if (class2 == null)
            {
                this.vctClass_0.WriteLog(string.Format(this.string_0, this.long_3, "不能转换为坐标"));
                this.streamReader_0.Close();
                this.streamReader_0.Dispose();
                this.streamReader_0 = null;
                this.long_0 = this.long_3;
                return -2;
            }
            (feature as CoPointFeature).Point.Add(class2);
            feature.OID = int.Parse(s);
            Label_0AA8:
            if (feature != null)
            {
                str3 = string.Empty;
                if (this.streamReader_1 == null)
                {
                    this.streamReader_1 = this.vctClass_0.CreateReader(this.long_2 - 1L);
                    this.long_4 = this.long_2 - 1L;
                }
                strArray = this.method_1(ref this.streamReader_1).Split(new char[] {this.vctClass_0.Separator});
                StringBuilder builder = new StringBuilder();
                string str10 = string.Empty;
                for (num3 = 0; num3 < feature.Values.Length; num3++)
                {
                    ICoField field = feature.Layer.GetField(num3);
                    if (strArray.Length > feature.Values.Length)
                    {
                        if (strArray.Length > (num3 + 1))
                        {
                            feature.SetValue(num3, strArray[num3 + 1]);
                        }
                        else
                        {
                            str10 = feature.OID.ToString();
                            builder.Append(field.ToString() + ",");
                        }
                    }
                    else if (strArray.Length > num3)
                    {
                        feature.SetValue(num3, strArray[num3]);
                    }
                    else
                    {
                        str10 = feature.OID.ToString();
                        builder.Append(field.ToString() + ",");
                    }
                }
                if (str10.Length > 0)
                {
                    this.vctClass_0.WriteLog(string.Format("要素{0}的字段{1}没有找到属性。", str10, builder.ToString()));
                }
                base.XpgisLayer.AppendFeature(feature);
                this.int_0++;
                return this.int_0;
            }
            return -1;
        }

        public override void Reset()
        {
            this.int_0 = -1;
            this.Close();
        }

        protected override void UpdateLayerStruct()
        {
        }

        public override int FeatureCount
        {
            get { return this.vctClass_0.GetLayerFeatureCount(base.XpgisLayer); }
        }
    }
}