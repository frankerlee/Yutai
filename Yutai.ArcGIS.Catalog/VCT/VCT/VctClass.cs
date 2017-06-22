using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Catalog.VCT.VCT
{
    public class VctClass : IDisposable
    {
        private ArrayList arrayList_0 = new ArrayList();
        private ArrayList arrayList_1 = new ArrayList();
        private ArrayList arrayList_2 = new ArrayList();
        private const string CAnnotationBegin = "ANNOTATIONBEGIN";
        private const string CAnnotationEnd = "ANNOTATIONEND";
        private const string CAttributeBegin = "ATTRIBUTEBEGIN";
        private const string CAttributeEnd = "ATTRIBUTEEND";
        private const string CFeatureCodeBegin = "FEATURECODEBEGIN";
        private const string CFeatureCodeEnd = "FEATURECODEEND";
        private char char_0 = ',';
        private const string CHeadBegin = "HEADBEGIN";
        private const string CHeadEnd = "HEADEND";
        private const string CLineBegin = "LINEBEGIN";
        private const string CLineEnd = "LINEEND";
        private CoLayerHead coLayerHead_0 = new CoLayerHead();
        private const string CPointBegin = "POINTBEGIN";
        private const string CPointEnd = "POINTEND";
        private const string CPolygonBegin = "POLYGONBEGIN";
        private const string CPolygonEnd = "POLYGONEND";
        private const string CTableStructureBegin = "TABLESTRUCTUREBEGIN";
        private const string CTableStructureEnd = "TABLESTRUCTUREEND";
        internal DataFile dataFile = new DataFile();
        private FeatureOIDMapCollection featureOIDMapCollection_0 = new FeatureOIDMapCollection();
        private int int_0 = 0;
        private List<ICoLayer> list_0 = new List<ICoLayer>();
        private long long_0 = 1L;
        private StreamWriter streamWriter_0 = null;
        private string string_0 = string.Empty;
        private string string_1 = "";
        private string string_2 = "标签{0}和{1}没有成对出现。";
        private string string_3 = "标签{0}没有出现。";
        private string string_4 = string.Empty;

        public VctClass(string string_5)
        {
            this.string_1 = CommonHelper.GetTempFile("log");
            try
            {
                if (File.Exists(this.string_1))
                {
                    FileInfo info = new FileInfo(this.string_1) {
                        Attributes = FileAttributes.Normal
                    };
                    File.Delete(this.string_1);
                }
                this.streamWriter_0 = File.CreateText(this.string_1);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.ToString());
            }
            this.string_0 = string_5;
            this.dataFile.fileName = string_5;
            using (StreamReader reader = new StreamReader(this.string_0, Encoding.GetEncoding(0)))
            {
                string str2;
                string str3;
                DateTime time;
                ICoLayer layer;
                bool flag = false;
                this.method_0(reader, "HEADBEGIN");
            Label_0149:
                if (reader.EndOfStream)
                {
                    goto Label_04CF;
                }
                string str = reader.ReadLine();
                while (str.Length <= 0)
                {
                    str = reader.ReadLine();
                }
                if (str.ToUpper().Trim() == "HEADEND")
                {
                    goto Label_04CD;
                }
                string[] strArray = str.Split(new char[] { ':' });
                if (strArray.Length == 2)
                {
                    str2 = strArray[1].Trim();
                    int result = 0;
                    int.TryParse(str2, out result);
                    double num2 = 0.0;
                    double.TryParse(str2, out num2);
                    str3 = strArray[0].ToUpper().Trim();
                    switch (str3)
                    {
                        case "DATAMARK":
                            this.coLayerHead_0.Datamark = str2;
                            break;

                        case "VERSION":
                            this.coLayerHead_0.Version = str2;
                            break;

                        case "UNIT":
                            this.coLayerHead_0.Unit = str2;
                            break;

                        case "DIM":
                            this.coLayerHead_0.Dim = result;
                            break;

                        case "TOPO":
                            this.coLayerHead_0.Topo = result;
                            break;

                        case "COORDINATE":
                            this.coLayerHead_0.Coordinate = str2;
                            break;

                        case "PROJECTION":
                            this.coLayerHead_0.Projection = str2;
                            break;

                        case "SPHEROID":
                            this.coLayerHead_0.Spheroid = str2;
                            break;

                        case "PARAMETERS":
                            this.coLayerHead_0.Parameters = str2;
                            break;

                        case "MERIDINAN":
                            this.coLayerHead_0.Meridian = result;
                            break;

                        case "MINX":
                            this.coLayerHead_0.MinPoint.X = num2;
                            break;

                        case "MINY":
                            this.coLayerHead_0.MinPoint.Y = num2;
                            break;

                        case "MAXX":
                            this.coLayerHead_0.MaxPoint.X = num2;
                            break;

                        case "MAXY":
                            this.coLayerHead_0.MaxPoint.Y = num2;
                            break;

                        case "SCALE":
                            this.coLayerHead_0.ScaleM = result;
                            break;

                        case "DATE":
                            goto Label_0474;

                        case "SEPARATOR":
                            goto Label_0489;
                    }
                }
                goto Label_0149;
            Label_0474:
                time = DateTime.Now;
                try
                {
                    time = Convert.ToDateTime(str2);
                }
                catch
                {
                }
                goto Label_04BB;
            Label_0489:
                if (str2.Length == 1)
                {
                    this.char_0 = str2[0];
                }
                else
                {
                    this.WriteLog("Separator字符不正确，将采用默认字符“,”。");
                }
                goto Label_0149;
            Label_04BB:
                this.coLayerHead_0.Date = time;
                goto Label_0149;
            Label_04CD:
                flag = true;
            Label_04CF:
                if (!flag)
                {
                    this.WriteLog(string.Format(this.string_2, "HEADBEGIN", "HEADEND"));
                }
                flag = false;
                this.method_0(reader, "FEATURECODEBEGIN");
                while (!reader.EndOfStream)
                {
                    str = reader.ReadLine();
                    while (str.Length <= 0)
                    {
                        str = reader.ReadLine();
                    }
                    if (str.ToUpper().Trim() == "FEATURECODEEND")
                    {
                        goto Label_07AF;
                    }
                    strArray = str.Split(new char[] { this.Separator });
                    layer = null;
                    if (strArray.Length < 3)
                    {
                        goto Label_0714;
                    }
                    layer = new CoLayerClass();
                    List<string> list = new List<string> {
                        strArray[0].Trim()
                    };
                    layer.Tag = list;
                    layer.AliasName = strArray[1].Trim();
                    str3 = strArray[2].Trim().ToUpper();
                    switch (str3)
                    {
                        case null:
                            break;

                        case "POINT":
                            layer.LayerType = CoLayerType.Point;
                            goto Label_065B;

                        case "LINE":
                            layer.LayerType = CoLayerType.Line;
                            goto Label_065B;

                        case "POLYGON":
                            layer.LayerType = CoLayerType.Region;
                            goto Label_065B;

                        default:
                            if (!(str3 == "ANNOTATION"))
                            {
                                if (!(str3 == "IMAGE"))
                                {
                                    break;
                                }
                                this.WriteLog("行\"" + str + "\"中图层类型IMAGE不能被读取。");
                            }
                            else
                            {
                                layer.LayerType = CoLayerType.Annotation;
                            }
                            goto Label_065B;
                    }
                    layer.LayerType = CoLayerType.Region;
                    this.WriteLog(string.Format("行\"{0}\"不能获取图层类型，将采用多边形为默认类型", str));
                Label_065B:
                    if (this.coLayerHead_0.Version.Length > 0)
                    {
                        if ((this.coLayerHead_0.Version.Substring(0, 1) == "2") && (strArray.Length >= 7))
                        {
                            layer.Name = strArray[6].Trim();
                        }
                        else if ((this.coLayerHead_0.Version.Substring(0, 1) == "1") && (strArray.Length >= 5))
                        {
                            layer.Name = strArray[4].Trim();
                        }
                        else
                        {
                            this.WriteLog("行\"" + str + "\"不能获取属性表名");
                        }
                    }
                    else
                    {
                        this.WriteLog("没有版本信息。");
                    }
                    goto Label_072B;
                Label_0714:
                    this.WriteLog("行\"" + str + "\"不能获取图层的信息");
                Label_072B:
                    if (layer != null)
                    {
                        layer.Parameter = this.coLayerHead_0;
                        ICoLayer layer2 = this.FindLayer(layer.Name);
                        if (layer2 == null)
                        {
                            this.list_0.Add(layer);
                            continue;
                        }
                        List<string> tag = layer2.Tag as List<string>;
                        if (tag == null)
                        {
                            tag = new List<string>();
                        }
                        tag.Add(strArray[0].Trim());
                        layer2.Tag = tag;
                    }
                }
                goto Label_07B1;
            Label_07AF:
                flag = true;
            Label_07B1:
                if (!flag)
                {
                    this.WriteLog(string.Format(this.string_2, "FEATURECODEBEGIN", "FEATURECODEEND"));
                }
                flag = false;
                this.method_0(reader, "TABLESTRUCTUREBEGIN");
                while (!reader.EndOfStream)
                {
                    str = reader.ReadLine();
                    while (str.Length <= 0)
                    {
                        str = reader.ReadLine();
                    }
                    if (str.ToUpper().Trim() == "TABLESTRUCTUREEND")
                    {
                        goto Label_0BDA;
                    }
                    strArray = str.Split(new char[] { this.Separator });
                    if (strArray.Length == 2)
                    {
                        string str4 = strArray[0].Trim();
                        int num4 = 0;
                        int.TryParse(strArray[1].Trim(), out num4);
                        layer = this.FindLayer(str4);
                        for (int i = 0; i < num4; i++)
                        {
                            int num7;
                            ICoField item = new CoFieldClass();
                            string str5 = reader.ReadLine();
                            while (str5.Length <= 0)
                            {
                                str5 = reader.ReadLine();
                            }
                            string[] strArray2 = str5.Split(new char[] { this.Separator });
                            if (strArray2.Length < 2)
                            {
                                goto Label_0B8D;
                            }
                            item.AliasName = item.Name = strArray2[0].Trim();
                            switch (strArray2[1].Trim().ToUpper())
                            {
                                case "SMALLINT":
                                case "INTEGER":
                                case "INT":
                                    item.Type = CoFieldType.整型;
                                    goto Label_0BA4;

                                case "CHAR":
                                case "VARCHAR":
                                {
                                    item.Type = CoFieldType.字符型;
                                    if (strArray2.Length <= 2)
                                    {
                                        break;
                                    }
                                    int num6 = 0;
                                    int.TryParse(strArray2[2].Trim(), out num6);
                                    item.Length = num6;
                                    goto Label_0BA4;
                                }
                                case "VARBIN":
                                    item.Type = CoFieldType.二进制;
                                    goto Label_0BA4;

                                case "FLOAT":
                                case "DOUBLE":
                                case "NUMERIC":
                                    item.Type = CoFieldType.浮点型;
                                    num7 = 0;
                                    if (strArray2.Length < 2)
                                    {
                                        goto Label_0AE0;
                                    }
                                    if (int.TryParse(strArray2[2].Trim(), out num7))
                                    {
                                        goto Label_0AD2;
                                    }
                                    item.Length = 28;
                                    this.WriteLog("行\"" + str5 + "\"不能获取浮点数的长度，将采用默认值28.");
                                    goto Label_0BA4;

                                case "DATE":
                                case "TIME":
                                case "DATETIME":
                                    item.Type = CoFieldType.日期型;
                                    goto Label_0BA4;

                                case "BOOLEAN":
                                    item.Type = CoFieldType.布尔型;
                                    goto Label_0BA4;

                                default:
                                    item.Type = CoFieldType.字符型;
                                    this.WriteLog("行\"" + str5 + "\"不能获取字段类型，将采用字符串作为默认类型");
                                    goto Label_0BA4;
                            }
                            item.Length = 20;
                            this.WriteLog("行\"" + str5 + "\"不能获取字符长度，将采用20作为默认长度");
                            goto Label_0BA4;
                        Label_0AD2:
                            item.Length = num7;
                            goto Label_0BA4;
                        Label_0AE0:
                            if (strArray2.Length >= 3)
                            {
                                num7 = 0;
                                if (!int.TryParse(strArray2[3].Trim(), out num7))
                                {
                                    item.Precision = 8;
                                    this.WriteLog("行\"" + str5 + "\"不能获取浮点数的小数位，将采用默认值8.");
                                }
                                else
                                {
                                    item.Precision= num7;
                                }
                            }
                            else
                            {
                                item.Length = 28;
                                item.Precision = 8;
                                this.WriteLog("行\"" + str5 + "\"不能获取浮点数长度和精度，将采用8位小数位。");
                            }
                            goto Label_0BA4;
                        Label_0B8D:
                            this.WriteLog("行\"" + str5 + "\"不能获取字段信息");
                        Label_0BA4:
                            if (layer != null)
                            {
                                layer.Fields.Add(item);
                            }
                        }
                    }
                }
                goto Label_0BDC;
            Label_0BDA:
                flag = true;
            Label_0BDC:
                if (!flag)
                {
                    this.WriteLog(string.Format(this.string_2, "TABLESTRUCTUREBEGIN", "TABLESTRUCTUREEND"));
                }
            }
        }

        public void CreateMap()
        {
            try
            {
                string str2;
                string str3;
                int num3;
                int num4;
                this.arrayList_0.Clear();
                this.arrayList_1.Clear();
                this.arrayList_2.Clear();
                this.featureOIDMapCollection_0.Clear();
                ICoLayer[] layers = this.Layers;
                for (int i = 0; i < layers.Length; i++)
                {
                    this.arrayList_0.Add(null);
                    this.arrayList_1.Add(null);
                    this.arrayList_2.Add(null);
                }
                StreamReader reader = new StreamReader(this.string_0, Encoding.Default);
                string str = string.Empty;
                this.long_0 = 1L;
                this.dataFile.Position = 0L;
                this.dataFile.Map.Add(this.dataFile.Position);
                int num2 = -1;
                bool flag = false;
                this.method_0(reader, "POINTBEGIN");
                while (!reader.EndOfStream)
                {
                    str2 = this.method_1(ref reader);
                    while (str2.Trim().Length <= 0)
                    {
                        str2 = this.method_1(ref reader);
                    }
                    if (str2.ToUpper().Trim() == "POINTEND")
                    {
                        goto Label_019F;
                    }
                    str3 = this.method_1(ref reader);
                    if (str != str3)
                    {
                        str = str3;
                        num2 = this.FindLayerIndex(str, CoLayerType.Point);
                        if ((num2 != -1) && (this.arrayList_0[num2] == null))
                        {
                            this.arrayList_0[num2] = this.long_0 - 2L;
                        }
                    }
                    this.method_1(ref reader);
                    this.method_1(ref reader);
                    this.method_1(ref reader);
                }
                goto Label_01A2;
            Label_019F:
                flag = true;
            Label_01A2:
                if (!flag)
                {
                    this.WriteLog(string.Format(this.string_2, "POINTBEGIN", "POINTEND"));
                }
                flag = false;
                str = string.Empty;
                this.method_0(reader, "LINEBEGIN");
            Label_01D7:
                if (!reader.EndOfStream)
                {
                    str2 = this.method_1(ref reader);
                    while (str2.Trim().Length <= 0)
                    {
                        str2 = this.method_1(ref reader);
                    }
                    FeatureOIDMap map = this.method_2(str2);
                    if (str2.ToUpper().Trim() != "LINEEND")
                    {
                        str3 = this.method_1(ref reader);
                        if (str != str3)
                        {
                            str = str3;
                            num2 = this.FindLayerIndex(str, CoLayerType.Line);
                            if ((num2 != -1) && (this.arrayList_0[num2] == null))
                            {
                                this.arrayList_0[num2] = this.long_0 - 2L;
                            }
                        }
                        this.method_1(ref reader);
                        this.method_1(ref reader);
                        num3 = 0;
                        int.TryParse(this.method_1(ref reader).Trim(), out num3);
                        num4 = 0;
                        while (num4 < num3)
                        {
                            string str4 = this.method_1(ref reader);
                            try
                            {
                                CoPointClass class2 = this.LineToPoint(str4);
                                map.PointArray.Add(class2);
                            }
                            catch (Exception exception1)
                            {
                                this.WriteLog(str4 + "\r\n" + exception1.ToString());
                            }
                            num4++;
                        }
                        goto Label_01D7;
                    }
                    flag = true;
                }
                if (!flag)
                {
                    this.WriteLog(string.Format(this.string_2, "LINEBEGIN", "LINEEND"));
                }
                flag = false;
                str = string.Empty;
                this.method_0(reader, "POLYGONBEGIN");
                while (!reader.EndOfStream)
                {
                    str2 = this.method_1(ref reader);
                    while (str2.Trim().Length <= 0)
                    {
                        str2 = this.method_1(ref reader);
                    }
                    if (str2.ToUpper().Trim() == "POLYGONEND")
                    {
                        goto Label_04DF;
                    }
                    str3 = this.method_1(ref reader);
                    if (str != str3)
                    {
                        str = str3;
                        num2 = this.FindLayerIndex(str, CoLayerType.Region);
                        if ((num2 != -1) && (this.arrayList_0[num2] == null))
                        {
                            this.arrayList_0[num2] = this.long_0 - 2L;
                        }
                    }
                    string str5 = this.method_1(ref reader);
                    str5 = this.method_1(ref reader);
                    if (this.LineToPoint(str5) == null)
                    {
                        str5 = this.method_1(ref reader);
                    }
                    num3 = 0;
                    int.TryParse(this.method_1(ref reader).Trim(), out num3);
                    switch (this.coLayerHead_0.Topo)
                    {
                        case 0:
                            goto Label_048F;

                        case 1:
                        {
                            int num6 = num3 / 8;
                            if ((num3 % 8) > 0)
                            {
                                num6++;
                            }
                            num4 = 0;
                            while (num4 < num6)
                            {
                                this.method_1(ref reader);
                                num4++;
                            }
                            continue;
                        }
                        default:
                        {
                            continue;
                        }
                    }
                Label_045E:
                    num4 = 0;
                    while (num4 < num3)
                    {
                        this.method_1(ref reader);
                        num4++;
                    }
                    int.TryParse(this.method_1(ref reader).Trim(), out num3);
                Label_048F:
                    if (num3 != 0)
                    {
                        goto Label_045E;
                    }
                }
                goto Label_04E2;
            Label_04DF:
                flag = true;
            Label_04E2:
                if (!flag)
                {
                    this.WriteLog(string.Format(this.string_2, "POLYGONBEGIN", "POLYGONEND"));
                }
                flag = false;
                str = string.Empty;
                this.method_0(reader, "ANNOTATIONBEGIN");
                while (!reader.EndOfStream)
                {
                    str2 = this.method_1(ref reader);
                    while (str2.Trim().Length <= 0)
                    {
                        str2 = this.method_1(ref reader);
                    }
                    if (str2.ToUpper().Trim() == "ANNOTATIONEND")
                    {
                        goto Label_0621;
                    }
                    str3 = this.method_1(ref reader);
                    if (str != str3)
                    {
                        str = str3;
                        num2 = this.FindLayerIndex(str, CoLayerType.Annotation);
                        if ((num2 != -1) && (this.arrayList_0[num2] == null))
                        {
                            this.arrayList_0[num2] = this.long_0 - 2L;
                        }
                    }
                    this.method_1(ref reader);
                    this.method_1(ref reader);
                    this.method_1(ref reader);
                    this.method_1(ref reader);
                    this.method_1(ref reader);
                    this.method_1(ref reader);
                    this.method_1(ref reader);
                    this.method_1(ref reader);
                    this.method_1(ref reader);
                }
                goto Label_0624;
            Label_0621:
                flag = true;
            Label_0624:
                if (!flag)
                {
                    this.WriteLog(string.Format(this.string_2, "ANNOTATIONBEGIN", "ANNOTATIONEND"));
                }
                flag = false;
                this.method_0(reader, "ATTRIBUTEBEGIN");
                while (!reader.EndOfStream)
                {
                    string str6 = this.method_1(ref reader);
                    while (str6.Trim().Length <= 0)
                    {
                        str6 = this.method_1(ref reader);
                    }
                    if (str6.Trim().ToUpper() == "ATTRIBUTEEND")
                    {
                        goto Label_073F;
                    }
                    num2 = this.FindLayerIndexByName(str6);
                    if (num2 != -1)
                    {
                        this.arrayList_1[num2] = this.long_0;
                    }
                    int num7 = 0;
                    for (string str7 = this.method_1(ref reader); str7.Trim().ToUpper() != "TABLEEND"; str7 = this.method_1(ref reader))
                    {
                        if (str7.Trim().Length > 0)
                        {
                            num7++;
                        }
                    }
                    if (num2 != -1)
                    {
                        this.arrayList_2[num2] = num7;
                    }
                }
                goto Label_0742;
            Label_073F:
                flag = true;
            Label_0742:
                if (!flag)
                {
                    this.WriteLog(string.Format(this.string_2, "ATTRIBUTEBEGIN", "ATTRIBUTEEND"));
                }
                reader.Close();
                reader.Dispose();
                reader = null;
            }
            catch (Exception exception2)
            {
                this.WriteLog(exception2.ToString());
            }
        }

        internal StreamReader CreateReader(long long_1)
        {
            FileStream stream = new FileStream(this.dataFile.fileName, FileMode.Open, FileAccess.Read);
            BufferedStream stream2 = new BufferedStream(stream, FileConfig.STREAM_BUFFER_SIZE);
            StreamReader reader = null;
            int num = ((int) long_1) / FileConfig.MAP_DISTANCE;
            long offset = long.Parse(this.dataFile.Map[num].ToString());
            stream2.Seek(offset, SeekOrigin.Begin);
            bool flag = true;
            reader = new StreamReader(stream2, Encoding.Default);
            reader.DiscardBufferedData();
            for (int i = 1; i <= (long_1 - (num * FileConfig.MAP_DISTANCE)); i++)
            {
                string str = reader.ReadLine();
                offset += this.method_3(str) + 2;
                if (flag)
                {
                    flag = false;
                }
            }
            if (!flag)
            {
                stream2.Seek(offset, SeekOrigin.Begin);
                reader = new StreamReader(stream2, Encoding.Default);
            }
            return reader;
        }

        public void Dispose()
        {
            this.featureOIDMapCollection_0.Dispose();
            this.featureOIDMapCollection_0 = null;
            this.list_0.Clear();
            this.list_0 = null;
            this.arrayList_1.Clear();
            this.arrayList_1 = null;
            this.arrayList_2.Clear();
            this.arrayList_2 = null;
            this.arrayList_0.Clear();
            this.arrayList_0 = null;
            this.dataFile = null;
            if (this.streamWriter_0 != null)
            {
                this.streamWriter_0.Flush();
                this.streamWriter_0.Close();
                this.streamWriter_0 = null;
            }
        }

        internal ICoLayer FindLayer(string string_5)
        {
            foreach (ICoLayer layer in this.Layers)
            {
                if (layer.Name.ToUpper() == string_5.ToUpper())
                {
                    return layer;
                }
            }
            return null;
        }

        internal int FindLayerIndex(string string_5, CoLayerType coLayerType_0)
        {
            for (int i = 0; i < this.list_0.Count; i++)
            {
                List<string> tag = this.Layers[i].Tag as List<string>;
                if ((tag != null) && (this.Layers[i].LayerType == coLayerType_0))
                {
                    foreach (string str in tag)
                    {
                        if (str.Trim().ToUpper() == string_5)
                        {
                            return i;
                        }
                    }
                }
            }
            return -1;
        }

        internal int FindLayerIndexByName(string string_5)
        {
            for (int i = 0; i < this.list_0.Count; i++)
            {
                if (this.Layers[i].Name.Trim().ToUpper() == string_5.ToUpper())
                {
                    return i;
                }
            }
            return -1;
        }

        internal FeatureOIDMap GetFeatureLineIndex(int int_1)
        {
            return this.featureOIDMapCollection_0.GetIndexByOID(int_1);
        }

        internal long GetLayerAttributeIndex(ICoLayer icoLayer_0)
        {
            int num = this.FindLayerIndexByName(icoLayer_0.Name);
            if ((num != -1) && (this.arrayList_1[num] != null))
            {
                return (long) int.Parse(this.arrayList_1[num].ToString());
            }
            return -1L;
        }

        internal int GetLayerFeatureCount(ICoLayer icoLayer_0)
        {
            int num = this.FindLayerIndexByName(icoLayer_0.Name);
            if ((num != -1) && (this.arrayList_2[num] != null))
            {
                int num2 = int.Parse(this.arrayList_2[num].ToString());
                if (num2 > 0)
                {
                    return num2;
                }
                return 0;
            }
            return 0;
        }

        internal long GetLayerGraphIndex(ICoLayer icoLayer_0)
        {
            int num = this.FindLayerIndexByName(icoLayer_0.Name);
            if ((num != -1) && (this.arrayList_0[num] != null))
            {
                return (long) int.Parse(this.arrayList_0[num].ToString());
            }
            return -1L;
        }

        internal Color LineToColor(string string_5)
        {
            string[] strArray = string_5.Split(new char[] { this.Separator });
            if (strArray.Length > 2)
            {
                int result = 0;
                int num2 = 0;
                int num3 = 0;
                int.TryParse(strArray[0].Trim(), out result);
                int.TryParse(strArray[1].Trim(), out num2);
                int.TryParse(strArray[2].Trim(), out num3);
                return Color.FromArgb(result, num2, num3);
            }
            if (strArray.Length > 0)
            {
                int num4 = 0;
                int.TryParse(strArray[0].Trim(), out num4);
                return Color.FromArgb(num4);
            }
            this.WriteLog("字符串：" + string_5 + "不能转换成颜色。");
            return Color.Black;
        }

        internal CoPointClass LineToPoint(string string_5)
        {
            CoPointClass class2 = new CoPointClass();
            if (string_5 == null)
            {
                return class2;
            }
            string[] strArray = string_5.Split(new char[] { this.Separator });
            if (strArray.Length > 1)
            {
                double result = 0.0;
                double.TryParse(strArray[0], out result);
                double num2 = 0.0;
                double.TryParse(strArray[1], out num2);
                class2.X = result;
                class2.Y = num2;
                return class2;
            }
            if (strArray.Length > 2)
            {
                double num3 = 0.0;
                double.TryParse(strArray[2], out num3);
                class2.Z = num3;
                return class2;
            }
            return null;
        }

        private bool method_0(StreamReader streamReader_0, string string_5)
        {
            while (!streamReader_0.EndOfStream)
            {
                if (this.method_1(ref streamReader_0).ToUpper().Trim() == string_5.ToUpper())
                {
                    return true;
                }
            }
            return false;
        }

        private string method_1(ref StreamReader streamReader_0)
        {
            if (!streamReader_0.EndOfStream)
            {
                string str = streamReader_0.ReadLine();
                this.dataFile.Position += this.method_3(str) + 2;
                if ((this.long_0 % ((long) FileConfig.MAP_DISTANCE)) == 0L)
                {
                    this.dataFile.Map.Add(this.dataFile.Position);
                }
                this.long_0 += 1L;
                return str;
            }
            return string.Empty;
        }

        private FeatureOIDMap method_2(string string_5)
        {
            int result = 0;
            int.TryParse(string_5, out result);
            FeatureOIDMap map = new FeatureOIDMap(result);
            this.featureOIDMapCollection_0.Add(map);
            return map;
        }

        private int method_3(string string_5)
        {
            int num = 0;
            for (int i = 0; i < string_5.Length; i++)
            {
                if (string_5[i] > '\x0080')
                {
                    num += 2;
                }
                else
                {
                    num++;
                }
            }
            return num;
        }

        internal void WriteLog(string string_5)
        {
            try
            {
                if (this.streamWriter_0 == null)
                {
                    this.streamWriter_0 = File.CreateText(this.string_1);
                }
                if (this.int_0 == 0)
                {
                    this.streamWriter_0.WriteLine("文件名：" + this.string_0);
                }
                this.int_0++;
                this.streamWriter_0.WriteLine(this.int_0.ToString() + "： " + string_5);
            }
            catch (Exception)
            {
            }
        }

        public string FileName
        {
            get
            {
                return this.string_0;
            }
        }

        internal CoLayerHead Head
        {
            get
            {
                return this.coLayerHead_0;
            }
        }

        public ICoLayer[] Layers
        {
            get
            {
                return this.list_0.ToArray();
            }
        }

        public string LogFileName
        {
            get
            {
                return this.string_1;
            }
        }

        internal char Separator
        {
            get
            {
                return this.char_0;
            }
        }
    }
}

