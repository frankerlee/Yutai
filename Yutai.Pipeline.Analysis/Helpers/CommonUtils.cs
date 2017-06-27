using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Helpers
{
    public class CommonUtils
    {

        public static double ConvertPixelsToMapUnits(IActiveView pActiveView, double pPixel)
        {
            int num = pActiveView.ScreenDisplay.DisplayTransformation.get_DeviceFrame().right -
                      pActiveView.ScreenDisplay.DisplayTransformation.get_DeviceFrame().left;
            if (num == 0)
            {
                return pPixel;
            }
            double num4 = pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.Width / ((double)num);
            return (pPixel * num4);
        }
        public static ILayer GetLayerByName(IMap pMap, string strDstName)
        {
            ILayer layer;
            int layerCount = pMap.LayerCount;
            int num = 0;
            while (true)
            {
                if (num < layerCount)
                {
                    ILayer layer1 = pMap.Layer[num];
                    if (layer1 is ICompositeLayer)
                    {
                        ILayer layer2 = CommonUtils.VerifyLayer(layer1, strDstName);
                        if (layer2 != null)
                        {
                            layer = layer2;
                            break;
                        }
                    }
                    else if (layer1.Name == strDstName)
                    {
                        layer = layer1;
                        break;
                    }
                    num++;
                }
                else
                {
                    layer = null;
                    break;
                }
            }
            return layer;
        }
        public static void NumberText_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            char keyChar = e.KeyChar;
            if (keyChar != '\b')
            {
                switch (keyChar)
                {
                    case '.':
                        {
                            if ((textBox.Text.IndexOf('.') != -1 ? false : textBox.SelectionStart != 0))
                            {
                                break;
                            }
                            else
                            {
                                e.KeyChar = '\0';
                                break;
                            }
                        }
                    case '/':
                        {
                            e.KeyChar = '\0';
                            break;
                        }
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        {
                            break;
                        }
                    default:
                        {
                            goto case '/';
                        }
                }
            }
        }

        public static void DeleteAllElements(IMap pMap)
        {
            ((IGraphicsContainer)pMap).DeleteAllElements();
            ((IActiveView)pMap).Refresh();
        }

        public static string GetFeatureClassNameFromLayerName(IPipelineConfig config, string strLayerName)
        {
            object valueFromTable = GetValueFromTable(config,"EmFeatureClassInfo", "层名", strLayerName, "表名");
            return (valueFromTable != null ? (string)valueFromTable : "");
        }

        public static Color GetFeatureColor(IMap pMap, string strLayerName, IFeature pFeaVal)
        {
            Color black;
            try
            {
                ILayer layerByFeatureClassName = GetLayerByFeatureClassName(pMap, strLayerName);
                if (layerByFeatureClassName != null)
                {
                    esriGeometryType geometryType = pFeaVal.Shape.GeometryType;
                    ISymbol symbolByFeature = ((IGeoFeatureLayer)layerByFeatureClassName).Renderer.get_SymbolByFeature(pFeaVal);
                    IRgbColor rgbColorClass = new RgbColor();
                    IColor color = null;
                    if (geometryType == (esriGeometryType)3)
                    {
                        if (symbolByFeature is ISimpleLineSymbol)
                        {
                            color = ((ISimpleLineSymbol)symbolByFeature).Color;
                        }
                        if (symbolByFeature is ILineSymbol)
                        {
                            color = ((ILineSymbol)symbolByFeature).Color;
                        }
                    }
                    if (geometryType == (esriGeometryType)1)
                    {
                        if (symbolByFeature is IMarkerFillSymbol)
                        {
                            color = ((IMarkerFillSymbol)symbolByFeature).Color;
                        }
                        if (symbolByFeature is IMarkerLineSymbol)
                        {
                            color = ((IMarkerLineSymbol)symbolByFeature).Color;
                        }
                        if (symbolByFeature is IMarkerSymbol)
                        {
                            color = ((IMarkerSymbol)symbolByFeature).Color;
                        }
                    }
                    if (color != null)
                    {
                        rgbColorClass.RGB=(color.RGB);
                        int red = rgbColorClass.Red;
                        int green = rgbColorClass.Green;
                        black = Color.FromArgb(red, green, rgbColorClass.Blue);
                    }
                    else
                    {
                        black = Color.Black;
                    }
                }
                else
                {
                    black = Color.Black;
                }
            }
            catch (Exception exception)
            {
                black = Color.Black;
            }
            return black;
        }

        public static int GetFieldIndex(ITable pTable, string strFieldName)
        {
            return pTable.Fields.FindField(strFieldName);
        }

        public static float GetFloatThreePoint(float fVal)
        {
            return Convert.ToSingle(fVal.ToString("f3"));
        }

        public static ILayer GetLayerByFeatureClassName(IMap pMap, string strDstName)
        {
            ILayer layer;
            int layerCount = pMap.LayerCount;
            int num = 0;
            while (true)
            {
                if (num < layerCount)
                {
                    ILayer layer1 = pMap.get_Layer(num);
                    if (layer1 is ICompositeLayer)
                    {
                        ILayer layer2 =CommonUtils.VerifyLayer(layer1, strDstName);
                        if (layer2 != null)
                        {
                            layer = layer2;
                            break;
                        }
                    }
                    else if (layer1 is IFeatureLayer && (layer1 as IFeatureLayer).FeatureClass.AliasName == strDstName)
                    {
                        layer = layer1;
                        break;
                    }
                    num++;
                }
                else
                {
                    layer = null;
                    break;
                }
            }
            return layer;
        }

        public static void GetMaxAndMinValue(string strVal, out double dMaxValue, out double dMinValue)
        {
            dMinValue = -1;
            dMaxValue = -1;
            int num = (strVal.IndexOf('[') > strVal.IndexOf('(') ? strVal.IndexOf('[') : strVal.IndexOf('('));
            int num1 = (strVal.IndexOf(']') > strVal.IndexOf(')') ? strVal.IndexOf(']') : strVal.IndexOf(')'));
            string str = strVal.Substring(num + 1, num1 - num - 1);
            string[] strArrays = str.Split(new char[] { ',' });
            for (int i = 0; i < (int)strArrays.Length; i++)
            {
                double num2 = Convert.ToDouble(strArrays[i]);
                if (i == 0)
                {
                    double num3 = num2;
                    double num4 = num3;
                    dMaxValue = num3;
                    dMinValue = num4;
                }
                dMinValue = (dMinValue < num2 ? dMinValue : num2);
                dMaxValue = (dMaxValue > num2 ? dMaxValue : num2);
            }
        }

        public static string GetPipeCatNameFromLayerName(IPipelineConfig config, string strLayerName)
        {
            object valueFromTable =CommonUtils.GetValueFromTable(config,"EmFeatureClassInfo", "层名", strLayerName, "管线类别");
            return (valueFromTable != null ? (string)valueFromTable : "");
        }

        public static object GetPipeLineAlarmHrzDist(IPipelineConfig config, string strPipeLine1, string strPipeLine2)
        {
            return CommonUtils.GetValueFromTable(config,"EmPipeDists", "管线1", strPipeLine1, "管线2", strPipeLine2, "水平净距");
        }

        public static object GetPipeLineAlarmHrzDist2(IPipelineConfig config,string strPipeLine1, string strPipeLine2, IFeature pFeature1, IFeature pFeature2)
        {
            object obj;
            double num;
            double num1;
            double num2;
            double num3;
            double num4;
            double num5;
            double num6;
            double num7;
            ITable table =CommonUtils.GetTable(config,"EMMETAPIPELEVELSPACE");
            if (table != null)
            {
                string[] strArrays = new string[] { "(管线1 = '", strPipeLine1, "' AND 管线2 = '", strPipeLine2, "')" };
                string str = string.Concat(strArrays);
                strArrays = new string[] { str, " OR (管线1 = '", strPipeLine2, "' AND 管线2 = '", strPipeLine1, "')" };
                string str1 = string.Concat(strArrays);
                IQueryFilter queryFilterClass = new QueryFilter();
                queryFilterClass.WhereClause=(str1);
                ICursor cursor = table.Search(queryFilterClass, false);
                IRow row = cursor.NextRow();
                int fieldIndex =CommonUtils.GetFieldIndex(table, "条件1");
                int fieldIndex1 =CommonUtils.GetFieldIndex(table, "条件2");
                int fieldIndex2 =CommonUtils.GetFieldIndex(table, "范围1");
                int fieldIndex3 =CommonUtils.GetFieldIndex(table, "范围2");
                int fieldIndex4 =CommonUtils.GetFieldIndex(table, "限距");
                string str2 = "";
                string str3 = "";
                string str4 = "";
                string str5 = "";
                double num8 = -1;
                double num9 = -1;
                double num10 = -1;
                double num11 = -1;
                while (row != null)
                {
                    str2 =CommonUtils.ObjToString(row.get_Value(fieldIndex));
                    str3 =CommonUtils.ObjToString(row.get_Value(fieldIndex1));
                    str4 =CommonUtils.ObjToString(row.get_Value(fieldIndex2));
                    str5 =CommonUtils.ObjToString(row.get_Value(fieldIndex3));
                    double num12 =CommonUtils.ObjToDouble(row.get_Value(fieldIndex4));
                    if ((str2 != "" ? false : str3 == ""))
                    {
                        num8 = num12;
                    }
                    
                    string lineTableFieldName = config.GetSpecialField(pFeature1.Class.AliasName, PipeConfigWordHelper.LineWords.GJ).Name; //CommonUtils.AppContext.PipeConfig.GetLineTableFieldName("管径");
                    if ((pFeature1 == null ? false : str2 == lineTableFieldName))
                    {
                        int num13 = pFeature1.Fields.FindField(lineTableFieldName);
                        if (num13 != -1)
                        {
                            string str6 =CommonUtils.ObjToString(pFeature1.get_Value(num13));
                            double num14 = -1;
                            try
                            {
                                num14 = Convert.ToDouble(str6);
                            }
                            catch (Exception exception)
                            {
                            }
                           CommonUtils.GetMaxAndMinValue(str4, out num, out num1);
                            if ((num14 > num ? false : num14 >= num1))
                            {
                                num9 = num12;
                            }
                        }
                    }
                    string lineTableFieldName1 = config.GetSpecialField(pFeature2.Class.AliasName, PipeConfigWordHelper.LineWords.GJ).Name;
                    if (str3 == lineTableFieldName)
                    {
                        int num15 = pFeature2.Fields.FindField(lineTableFieldName1);
                        if (num15 != -1)
                        {
                            string str7 =CommonUtils.ObjToString(pFeature2.get_Value(num15));
                            double num16 = -1;
                            try
                            {
                                num16 = Convert.ToDouble(str7);
                            }
                            catch (Exception exception1)
                            {
                            }
                           CommonUtils.GetMaxAndMinValue(str5, out num2, out num3);
                            if ((num16 > num2 ? false : num16 >= num3))
                            {
                                num10 = num12;
                            }
                        }
                    }
                    lineTableFieldName = "压力";
                    if ((pFeature1 == null ? false : str2 == lineTableFieldName))
                    {
                        int num17 = pFeature1.Fields.FindField(lineTableFieldName);
                        if (num17 != -1)
                        {
                            string str8 =CommonUtils.ObjToString(pFeature1.get_Value(num17));
                            if (str8 == "低压")
                            {
                                str8 = "0.043";
                            }
                            else if (str8 == "中压")
                            {
                                str8 = "0.2";
                            }
                            else if (str8 == "高压")
                            {
                                str8 = "0.8";
                            }
                            double num18 = -1;
                            try
                            {
                                num18 = Convert.ToDouble(str8);
                            }
                            catch (Exception exception2)
                            {
                            }
                           CommonUtils.GetMaxAndMinValue(str4, out num4, out num5);
                            if ((num18 > num4 ? false : num18 >= num5))
                            {
                                num9 = num12;
                            }
                        }
                    }
                    lineTableFieldName = "压力";
                    if (str3 == lineTableFieldName)
                    {
                        int num19 = pFeature2.Fields.FindField(lineTableFieldName);
                        if (num19 != -1)
                        {
                            string str9 =CommonUtils.ObjToString(pFeature2.get_Value(num19));
                            if (str9 == "低压")
                            {
                                str9 = "0.043";
                            }
                            else if (str9 == "中压")
                            {
                                str9 = "0.2";
                            }
                            else if (str9 == "高压")
                            {
                                str9 = "0.8";
                            }
                            double num20 = -1;
                            try
                            {
                                num20 = Convert.ToDouble(str9);
                            }
                            catch (Exception exception3)
                            {
                            }
                           CommonUtils.GetMaxAndMinValue(str5, out num6, out num7);
                            if ((num20 > num6 ? false : num20 >= num7))
                            {
                                num10 = num12;
                            }
                        }
                    }
                    lineTableFieldName = "埋设方式";
                    if ((pFeature1 == null ? false : str2 == lineTableFieldName))
                    {
                        int num21 = pFeature1.Fields.FindField(lineTableFieldName);
                        if (num21 != -1 &&CommonUtils.ObjToString(pFeature1.get_Value(num21)).Trim().ToUpper() == str4.ToUpper())
                        {
                            num9 = num12;
                        }
                    }
                    if (str3 == lineTableFieldName)
                    {
                        int num22 = pFeature2.Fields.FindField(lineTableFieldName);
                        if (num22 != -1 &&CommonUtils.ObjToString(pFeature2.get_Value(num22)).Trim().ToUpper() == str5.ToUpper())
                        {
                            num9 = num12;
                        }
                    }
                    row = cursor.NextRow();
                }
                Marshal.ReleaseComObject(cursor);
                double num23 = -1;
                if (num11 != -1)
                {
                    num23 = num11;
                }
                if (num10 != -1)
                {
                    num23 = (num23 == -1 ? num10 : Math.Min(num23, num10));
                }
                if (num9 != -1)
                {
                    num23 = (num23 == -1 ? num9 : Math.Min(num23, num9));
                }
                if (num8 != -1)
                {
                    num23 = (num23 == -1 ? num8 : Math.Min(num23, num8));
                }
                obj = num23;
            }
            else
            {
                obj = null;
            }
            return obj;
        }

        public static float GetPipeLineAlarmHrzDistByFeatureClassName(IPipelineConfig config, string strFeaClassName1, string strFeaClassName2)
        {
            float single;
            string str;
            string str1;
            object valueFromTable =CommonUtils.GetValueFromTable(config,"EMFeatureClassInfo", "要素类", strFeaClassName1, "管线类别");
            if (valueFromTable != null)
            {
                str = (!Convert.IsDBNull(valueFromTable) ? valueFromTable.ToString() : "");
                valueFromTable =CommonUtils.GetValueFromTable(config,"EMFeatureClassInfo", "要素类", strFeaClassName2, "管线类别");
                if (valueFromTable != null)
                {
                    str1 = (!Convert.IsDBNull(valueFromTable) ? valueFromTable.ToString() : "");
                    if (str.Length > 2)
                    {
                        str = str.Substring(0, 2);
                    }
                    if (str1.Length > 2)
                    {
                        str1 = str1.Substring(0, 2);
                    }
                    object pipeLineAlarmHrzDist =CommonUtils.GetPipeLineAlarmHrzDist(config, str, str1);
                    if (pipeLineAlarmHrzDist != null)
                    {
                        single = (!Convert.IsDBNull(pipeLineAlarmHrzDist) ? Convert.ToSingle(pipeLineAlarmHrzDist) : 0f);
                    }
                    else
                    {
                        single = 0f;
                    }
                }
                else
                {
                    single = 0f;
                }
            }
            else
            {
                single = 0f;
            }
            return single;
        }

        public static float GetPipeLineAlarmHrzDistByFeatureClassName2(IPipelineConfig config,string strFeaClassName1, string strFeaClassName2, IFeature pFeature1, IFeature pFeature2)
        {
            float single;
            string str;
            string str1;
            object valueFromTable =CommonUtils.GetValueFromTable(config,"EMFeatureClassInfo", "要素类", strFeaClassName1.ToUpper(), "管线类别");
            if (valueFromTable != null)
            {
                str = (!Convert.IsDBNull(valueFromTable) ? valueFromTable.ToString() : "");
                valueFromTable =CommonUtils.GetValueFromTable(config,"EMFeatureClassInfo", "要素类", strFeaClassName2.ToUpper(), "管线类别");
                if (valueFromTable != null)
                {
                    str1 = (!Convert.IsDBNull(valueFromTable) ? valueFromTable.ToString() : "");
                    object pipeLineAlarmHrzDist2 =CommonUtils.GetPipeLineAlarmHrzDist2(config,str, str1, pFeature1, pFeature2);
                    if (pipeLineAlarmHrzDist2 != null)
                    {
                        single = (!Convert.IsDBNull(pipeLineAlarmHrzDist2) ? Convert.ToSingle(pipeLineAlarmHrzDist2) : 0f);
                    }
                    else
                    {
                        single = 0f;
                    }
                }
                else
                {
                    single = 0f;
                }
            }
            else
            {
                single = 0f;
            }
            return single;
        }

        public static object GetPipeLineAlarmVerDist(IPipelineConfig config, string strPipeLine1, string strPipeLine2, string strBuryKind1, string strBuryKind2)
        {
            object obj;
            ITable table =CommonUtils.GetTable(config,"EMMetaPipeAplombSpace");
            if (table != null)
            {
                string[] strArrays = new string[] { "(上面管线1 = '", strPipeLine1, "' AND 下面管线2 = '", strPipeLine2, "')" };
                string str = string.Concat(strArrays);
                strArrays = new string[] { str, "OR (上面管线1 = '", strPipeLine2, "' AND 下面管线2 = '", strPipeLine1, "')" };
                string str1 = string.Concat(strArrays);
                IQueryFilter queryFilterClass = new QueryFilter();
                queryFilterClass.WhereClause=(str1);
                ICursor cursor = table.Search(queryFilterClass, false);
                IRow row = cursor.NextRow();
                int fieldIndex =CommonUtils.GetFieldIndex(table, "条件1");
                int num =CommonUtils.GetFieldIndex(table, "条件2");
                int fieldIndex1 =CommonUtils.GetFieldIndex(table, "限距");
                double num1 = -1;
                double num2 = -1;
                double num3 = -1;
                double num4 = -1;
                while (row != null)
                {
                    string str2 =CommonUtils.ObjToString(row.get_Value(fieldIndex));
                    string str3 =CommonUtils.ObjToString(row.get_Value(num));
                    double num5 =CommonUtils.ObjToDouble(row.get_Value(fieldIndex1));
                    if ((str2 == strBuryKind1 ? false : str3 != strBuryKind2))
                    {
                        num1 = num5;
                    }
                    if ((str2 != strBuryKind1 ? false : str3 != strBuryKind2))
                    {
                        num2 = num5;
                    }
                    if ((str2 == strBuryKind1 ? false : str3 == strBuryKind2))
                    {
                        num3 = num5;
                    }
                    if ((str2 != strBuryKind1 ? false : str3 == strBuryKind2))
                    {
                        num4 = num5;
                    }
                    row = cursor.NextRow();
                }
                Marshal.ReleaseComObject(cursor);
                double num6 = -1;
                if (num4 != -1)
                {
                    num6 = num4;
                }
                if (num3 != -1)
                {
                    num6 = (num6 == -1 ? num3 : Math.Min(num6, num3));
                }
                if (num2 != -1)
                {
                    num6 = (num6 == -1 ? num2 : Math.Min(num6, num2));
                }
                if (num1 != -1)
                {
                    num6 = (num6 == -1 ? num1 : Math.Min(num6, num1));
                }
                obj = num6;
            }
            else
            {
                obj = null;
            }
            return obj;
        }

        public static float GetPipeLineAlarmVerDistByFeatureClassName(IPipelineConfig config, string strFeaClassName1, string strFeaClassName2, string strBuryKind1, string strBuryKind2)
        {
            float single;
            string str;
            string str1;
            object valueFromTable =CommonUtils.GetValueFromTable(config,"EMFeatureClassInfo", "要素类", strFeaClassName1.ToUpper(), "管线类别");
            if (valueFromTable != null)
            {
                str = (!Convert.IsDBNull(valueFromTable) ? valueFromTable.ToString() : "");
                valueFromTable =CommonUtils.GetValueFromTable(config,"EMFeatureClassInfo", "要素类", strFeaClassName2.ToUpper(), "管线类别");
                if (valueFromTable != null)
                {
                    str1 = (!Convert.IsDBNull(valueFromTable) ? valueFromTable.ToString() : "");
                    object pipeLineAlarmVerDist =CommonUtils.GetPipeLineAlarmVerDist(config,str, str1, strBuryKind1, strBuryKind2);
                    if (valueFromTable != null)
                    {
                        single = (!Convert.IsDBNull(pipeLineAlarmVerDist) ? Convert.ToSingle(pipeLineAlarmVerDist) : 0f);
                    }
                    else
                    {
                        single = 0f;
                    }
                }
                else
                {
                    single = 0f;
                }
            }
            else
            {
                single = 0f;
            }
            return single;
        }

        public static IPolyline GetPolylineDeepCopy(IPolyline pSrcLine)
        {
            IPolyline polylineClass = new Polyline() as IPolyline;
            IPointCollection pointCollection = (IPointCollection)pSrcLine;
            IPointCollection pointCollection1 = (IPointCollection)polylineClass;
            for (int i = 0; i < pointCollection.PointCount; i++)
            {
                IPoint point = pointCollection.get_Point(i);
                IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
                pointClass.X=(point.X);
                pointClass.Y=(point.Y);
                pointClass.Z=(point.Z);
                pointClass.M=(point.M);
                object missing = Type.Missing;
                pointCollection1.AddPoint(pointClass, ref missing, ref missing);
            }
            return polylineClass;
        }

        public static string GetSmpClassName(string strSrcName)
        {
            string str;
            int num = strSrcName.LastIndexOf('.');
            str = (num != -1 ? strSrcName.Substring(num + 1) : strSrcName);
            return str;
        }

        public static string GetSmpClassNameByLayer(ILayer pLayer)
        {
            return CommonUtils.GetSmpClassName((pLayer as IFeatureLayer).FeatureClass.AliasName);
        }

        public static ITable GetTable(IPipelineConfig config,string strTableName)
        {
            ITable table;
            if (config.Workspace != null)
            {
                IFeatureWorkspace workspace = config.Workspace as IFeatureWorkspace;
                if (!(config.Workspace as IWorkspace2).get_NameExists((esriDatasetType)10, strTableName))
                {
                    table = null;
                }
                else
                {
                    table = workspace.OpenTable(strTableName);
                }
            }
            else
            {
                table = null;
            }
            return table;
        }

        public static object GetValueFromTable(IPipelineConfig config,string strTableName, string strKeyFieldName, string strKeyFieldValue, string strDstFieldName)
        {
            object obj;
            ITable table =CommonUtils.GetTable(config,strTableName);
            if (table != null)
            {
                string str = string.Concat(strKeyFieldName, " = '", strKeyFieldValue, "'");
                IQueryFilter queryFilterClass = new QueryFilter();
                queryFilterClass.WhereClause=(str);
                ICursor cursor = table.Search(queryFilterClass, false);
                IRow row = cursor.NextRow();
                int fieldIndex =CommonUtils.GetFieldIndex(table, strDstFieldName);
                if (row != null)
                {
                    object value = row.get_Value(fieldIndex);
                    Marshal.ReleaseComObject(cursor);
                    obj = value;
                }
                else
                {
                    Marshal.ReleaseComObject(cursor);
                    obj = null;
                }
            }
            else
            {
                obj = null;
            }
            return obj;
        }

        public static object GetValueFromTable(IPipelineConfig config, string strTableName, string strKeyFieldName1, string strKeyFieldValue1, string strKeyFieldName2, string strKeyFieldValue2, string strDstFieldName)
        {
            object obj;
            ITable table =CommonUtils.GetTable(config,strTableName);
            if (table != null)
            {
                string[] strArrays = new string[] { strKeyFieldName1, " = '", strKeyFieldValue1, "' AND ", strKeyFieldName2, " = '", strKeyFieldValue2, "'" };
                string str = string.Concat(strArrays);
                IQueryFilter queryFilterClass = new QueryFilter();
                queryFilterClass.WhereClause=(str);
                ICursor cursor = table.Search(queryFilterClass, false);
                IRow row = cursor.NextRow();
                int fieldIndex =CommonUtils.GetFieldIndex(table, strDstFieldName);
                if (row != null)
                {
                    object value = row.get_Value(fieldIndex);
                    Marshal.ReleaseComObject(cursor);
                    obj = value;
                }
                else
                {
                    Marshal.ReleaseComObject(cursor);
                    obj = null;
                }
            }
            else
            {
                obj = null;
            }
            return obj;
        }

        public static bool IsPipeLine(IPipelineConfig config, string strLayerName)
        {
            object valueFromTable =CommonUtils.GetValueFromTable(config,"EmFeatureClassInfo", "层名", strLayerName, "管线类别");
            return (valueFromTable == null ? false : !Convert.IsDBNull(valueFromTable));
        }

        public static bool IsPipeLineByFeatureClassName(IPipelineConfig config, string strFeaClassName)
        {
            bool flag;
            object valueFromTable =CommonUtils.GetValueFromTable(config,"EmFeatureClassInfo", "要素类", strFeaClassName, "管线类别");
            if (valueFromTable != null)
            {
                flag = (!Convert.IsDBNull(valueFromTable) ? ((string)valueFromTable).Trim() != "" : false);
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public static void NewLineElement(IMap pMap, IPolyline pPolyLine)
        {
            IActiveView pActiveView = pMap as IActiveView;
            IGraphicsContainer activeView = (IGraphicsContainer)pActiveView;
            RubberLine rubberLineClass = new RubberLine();
            ISimpleLineSymbol simpleLineSymbolClass = new SimpleLineSymbol();
            IRgbColor rgbColorClass = new RgbColor();
            rgbColorClass.Red=(0);
            rgbColorClass.Green=(255);
            rgbColorClass.Blue=(255);
            simpleLineSymbolClass.Color=(rgbColorClass);
            simpleLineSymbolClass.Width=(8);
            simpleLineSymbolClass.Style=(esriSimpleLineStyle)(2);
            LineElement lineElementClass = new LineElement();
            ((IElement)lineElementClass).Geometry=(pPolyLine);
            activeView.AddElement(lineElementClass, 0);
            pActiveView.PartialRefresh((esriViewDrawPhase)8, null, null);
        }

        public static void NewPointElement(IMap pMap , IPoint pPoint)
        {
            IActiveView pActiveView = pMap as IActiveView;
            IGraphicsContainer activeView = (IGraphicsContainer)pActiveView;
            RubberPoint rubberPointClass = new RubberPoint();
            ISimpleMarkerSymbol simpleMarkerSymbolClass = new SimpleMarkerSymbol();
            IRgbColor rgbColorClass = new RgbColor();
            rgbColorClass.Red=(0);
            rgbColorClass.Green=(255);
            rgbColorClass.Blue=(255);
            simpleMarkerSymbolClass.Color=(rgbColorClass);
            simpleMarkerSymbolClass.Size=(8);
            simpleMarkerSymbolClass.Style=(0);
            LineElement lineElementClass = new LineElement();
            ((IElement)lineElementClass).Geometry=(pPoint);
            activeView.AddElement(lineElementClass, 0);
            pActiveView.Refresh();
        }

        public static void NewPolygonElement(IMap pMap, IPolygon pPolygon)
        {
            IActiveView pActiveView = pMap as IActiveView;
            IGraphicsContainer activeView = (IGraphicsContainer)pActiveView;
            RubberPolygon rubberPolygonClass = new RubberPolygon();
            ISimpleLineSymbol simpleLineSymbolClass = new SimpleLineSymbol();
            IRgbColor rgbColorClass = new RgbColor();
            rgbColorClass.Red=(0);
            rgbColorClass.Green=(255);
            rgbColorClass.Blue=(255);
            simpleLineSymbolClass.Color=(rgbColorClass);
            simpleLineSymbolClass.Width=(8);
            simpleLineSymbolClass.Style= (esriSimpleLineStyle)(2);
            PolygonElement polygonElementClass = new PolygonElement();
            polygonElementClass.Geometry=(pPolygon);
            activeView.AddElement(polygonElementClass, 0);
            pActiveView.PartialRefresh((esriViewDrawPhase)8, null, null);
        }

        public static void NewPolygonElement(IMap pMap, IPolygon pPolygon, bool bRefresh)
        {
            IActiveView pActiveView=pMap as IActiveView;
            
            IGraphicsContainer activeView = (IGraphicsContainer)pActiveView;
            RubberPolygon rubberPolygonClass = new RubberPolygon();
            ISimpleLineSymbol simpleLineSymbolClass = new SimpleLineSymbol();
            IRgbColor rgbColorClass = new RgbColor();
            rgbColorClass.Red=(0);
            rgbColorClass.Green=(255);
            rgbColorClass.Blue=(255);
            simpleLineSymbolClass.Color=(rgbColorClass);
            simpleLineSymbolClass.Width=(8);
            simpleLineSymbolClass.Style=(esriSimpleLineStyle)(2);
            PolygonElement polygonElementClass = new PolygonElement();
            polygonElementClass.Geometry=(pPolygon);
            activeView.AddElement(polygonElementClass, 0);
            if (bRefresh)
            {
                pActiveView.Refresh();
            }
        }

        public static void NewPolygonElementTran(IMap pMap, IPolygon pPolygon, bool bRefresh)
        {
            IActiveView pActiveView = pMap as IActiveView;
            IGraphicsContainer activeView = (IGraphicsContainer)pActiveView;
            RubberPolygon rubberPolygonClass = new RubberPolygon();
            ISimpleLineSymbol simpleLineSymbolClass = new SimpleLineSymbol();
            IRgbColor rgbColorClass = new RgbColor();
            rgbColorClass.Red=(0);
            rgbColorClass.Green=(255);
            rgbColorClass.Blue=(255);
            rgbColorClass.Transparency=(1);
            IRgbColor rgbColor = new RgbColor();
            rgbColor.Red=(255);
            rgbColor.Green=(0);
            rgbColor.Blue=(0);
            rgbColor.Transparency=(1);
            simpleLineSymbolClass.Color=(rgbColorClass);
            simpleLineSymbolClass.Width=(8);
            simpleLineSymbolClass.Style= esriSimpleLineStyle.esriSLSSolid;
            ISimpleFillSymbol simpleFillSymbolClass = new SimpleFillSymbol();
            ((ISymbol)simpleFillSymbolClass).ROP2=(esriRasterOpCode)(10);
            simpleFillSymbolClass.Color=(rgbColorClass);
            simpleFillSymbolClass.Color.Transparency=(1);
            IElement polygonElementClass = new PolygonElement();
            polygonElementClass.Geometry=(pPolygon);
            (polygonElementClass as IFillShapeElement).Symbol=(simpleFillSymbolClass);
            activeView.AddElement(polygonElementClass, 0);
            if (bRefresh)
            {
                pActiveView.Refresh();
            }
        }

        public static double ObjToDouble(object objVal)
        {
            return ((Convert.IsDBNull(objVal) ? false : objVal != null) ? Convert.ToDouble(objVal) : 0);
        }

        public static string ObjToString(object objVal)
        {
            return ((Convert.IsDBNull(objVal) ? false : objVal != null) ? objVal.ToString() : "");
        }

        public static void ScaleToGeo(IMap pMap, IGeometry pGeo)
        {
            IEnvelope envelope = pGeo.Envelope;
            envelope.Expand(3, 3, true);
            ((IActiveView)pMap).Extent=(envelope);
        }

        public static void ScaleToTwoGeo(IMap pMap, IGeometry pGeo1, IGeometry pGeo2)
        {
            IEnvelope envelope = pGeo1.Envelope;
            IEnvelope envelope1 = pGeo2.Envelope;
            IEnvelope envelope2 = envelope;
            envelope2.Union(envelope1);
            if ((((IActiveView)pMap).Extent.LowerLeft.X > envelope2.LowerLeft.X || ((IActiveView)pMap).Extent.LowerLeft.Y > envelope2.LowerLeft.Y || ((IActiveView)pMap).Extent.UpperRight.X < envelope2.UpperRight.X ? true : ((IActiveView)pMap).Extent.UpperRight.Y < envelope2.UpperRight.Y))
            {
                envelope2.Expand(1.5, 1.5, true);
                ((IActiveView)pMap).Extent = (envelope2);
            }
        }

        public static void Show(ILayer pLay)
        {
            IFeatureLayer featureLayer = pLay as IFeatureLayer;
            MessageBox.Show(string.Concat(featureLayer.Name, pLay.Name));
        }

        public static void ThrougAllLayer(IMap pMap,CommonUtils.DealLayer dlFunVal)
        {
            int layerCount = pMap.LayerCount;
            for (int i = 0; i < layerCount; i++)
            {
                ILayer layer = pMap.get_Layer(i);
                if (!(layer is ICompositeLayer))
                {
                    dlFunVal(layer);
                }
                else
                {
                   CommonUtils.VerifyLayer(layer, dlFunVal);
                }
            }
        }

        public static ILayer VerifyLayer(ILayer pLayVal,CommonUtils.DealLayer dlFunVal)
        {
            ICompositeLayer compositeLayer = pLayVal as ICompositeLayer;
            int count = compositeLayer.Count;
            for (int i = 0; i < count; i++)
            {
                ILayer layer = compositeLayer.get_Layer(i);
                if (!(layer is ICompositeLayer))
                {
                    dlFunVal(layer);
                }
                else
                {
                   CommonUtils.VerifyLayer(layer, dlFunVal);
                }
            }
            return null;
        }

        public static ILayer VerifyLayer(ILayer pLayVal, string strDstName)
        {
            ILayer layer;
            ICompositeLayer compositeLayer = pLayVal as ICompositeLayer;
            int count = compositeLayer.Count;
            int num = 0;
            while (true)
            {
                if (num < count)
                {
                    ILayer layer1 = compositeLayer.get_Layer(num);
                    if (layer1 is ICompositeLayer)
                    {
                        ILayer layer2 =CommonUtils.VerifyLayer(layer1, strDstName);
                        if (layer2 != null)
                        {
                            layer = layer2;
                            break;
                        }
                    }
                    else if (layer1 is IFeatureLayer && (layer1 as IFeatureLayer).FeatureClass.AliasName == strDstName)
                    {
                        layer = layer1;
                        break;
                    }
                    num++;
                }
                else
                {
                    layer = null;
                    break;
                }
            }
            return layer;
        }

        public delegate void DealLayer(ILayer pLayer);

        public static IAppContext AppContext { get; set; }
    }
}
