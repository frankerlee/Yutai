using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.GlobeCore;
using ESRI.ArcGIS.SystemUI;
using Microsoft.Win32;

namespace Yutai.ArcGIS.Catalog
{
    public class CommonHelper
    {

        public const string C_TEMP_DIRECTORY = "TempDirectory";

        // Methods
        public static double AreaUnitConvert(string string_0, string string_1, double double_0)
        {
            double num = 0.0;
            double num2 = 0.0;
            string str = string_0;
            if (str != null)
            {
                if (str != "公顷")
                {
                    if (!(str == "亩"))
                    {
                        if (str == "平方米")
                        {
                            num = double_0;
                        }
                    }
                    else
                    {
                        num = (double_0/15.0)*10000.0;
                    }
                }
                else
                {
                    num = double_0*10000.0;
                }
            }
            str = string_1;
            switch (str)
            {
                case null:
                    return num2;

                case "公顷":
                    return (num/10000.0);
            }
            if (!(str == "亩"))
            {
                if (str == "平方米")
                {
                    num2 = num;
                }
                return num2;
            }
            return ((num/10000.0)*15.0);
        }

        public static List<T> ArrayToList<T>(T[] gparam_0)
        {
            List<T> list = new List<T>();
            foreach (T local in gparam_0)
            {
                list.Add(local);
            }
            return list;
        }

        public static double azimuth(IPoint ipoint_0, IPoint ipoint_1)
        {
            double x = ipoint_1.X - ipoint_0.X;
            double y = ipoint_1.Y - ipoint_0.Y;
            double num3 = Math.Atan2(y, x)*57.295779513082323;
            if (num3 < 0.0)
            {
                num3 = 360.0 + num3;
            }
            return num3;
        }

        public static double azimuth(double double_0, double double_1, double double_2, double double_3)
        {
            double x = double_2 - double_0;
            double y = double_3 - double_1;
            double num3 = Math.Atan2(y, x)*57.295779513082323;
            if (num3 < 0.0)
            {
                num3 = 360.0 + num3;
            }
            return num3;
        }

        public static string ConvertFieldValueToString(esriFieldType esriFieldType_0, object object_0)
        {
            switch (esriFieldType_0)
            {
                case esriFieldType.esriFieldTypeInteger:
                case esriFieldType.esriFieldTypeSingle:
                case esriFieldType.esriFieldTypeDouble:
                case esriFieldType.esriFieldTypeOID:
                    return object_0.ToString();

                case esriFieldType.esriFieldTypeString:
                    return ("'" + object_0.ToString() + "'");

                case esriFieldType.esriFieldTypeDate:
                    return object_0.ToString();

                case esriFieldType.esriFieldTypeGeometry:
                    return "几何数据";

                case esriFieldType.esriFieldTypeBlob:
                    return "长二进制串";
            }
            return "";
        }

        public static double ConvertPixelsToMapUnits(IActiveView iactiveView_0, double double_0)
        {
            int num = iactiveView_0.ScreenDisplay.DisplayTransformation.get_DeviceFrame().right -
                      iactiveView_0.ScreenDisplay.DisplayTransformation.get_DeviceFrame().left;
            if (num == 0)
            {
                return double_0;
            }
            double num4 = iactiveView_0.ScreenDisplay.DisplayTransformation.VisibleBounds.Width/((double) num);
            return (double_0*num4);
        }

        public static double ConvertPixelsToMapUnits(IScreenDisplay iscreenDisplay_0, double double_0)
        {
            int num = iscreenDisplay_0.DisplayTransformation.get_DeviceFrame().right -
                      iscreenDisplay_0.DisplayTransformation.get_DeviceFrame().left;
            if (num == 0)
            {
                return double_0;
            }
            double num4 = iscreenDisplay_0.DisplayTransformation.VisibleBounds.Width/((double) num);
            return (double_0*num4);
        }

        public static ToolStripItem CreateBarItem(ICommand icommand_0)
        {
            ToolStripItem item = new ToolStripButton
            {
                Name = icommand_0.Name,
                Tag = icommand_0,
                Text = icommand_0.Caption,
                Enabled = icommand_0.Enabled
            };
            if (icommand_0.Tooltip != null)
            {
                item.ToolTipText = icommand_0.Tooltip;
            }
            if (icommand_0.Bitmap != 0)
            {
                try
                {
                    IntPtr hbitmap = new IntPtr(icommand_0.Bitmap);
                    Bitmap bitmap = Image.FromHbitmap(hbitmap);
                    bitmap.MakeTransparent();
                    item.Image = bitmap;
                    item.DisplayStyle = ToolStripItemDisplayStyle.Image;
                }
                catch
                {
                }
                return item;
            }
            item.DisplayStyle = ToolStripItemDisplayStyle.Text;
            return item;
        }

        //public static BarButtonItem CreateJLKBarItem(BarManager barManager_0, ICommand icommand_0)
        //{
        //    BarButtonItem item = new BarButtonItem();
        //    barManager_0.Items.Add(item);
        //    item.Id = barManager_0.GetNewItemId();
        //    item.Name = icommand_0.Name;
        //    item.Tag = icommand_0;
        //    item.Caption = icommand_0.Caption;
        //    if (icommand_0.Tooltip != null)
        //    {
        //        item.Hint = icommand_0.Tooltip;
        //    }
        //    else
        //    {
        //        item.Hint = icommand_0.Caption;
        //    }
        //    item.Enabled = icommand_0.Enabled;
        //    if (icommand_0.Bitmap != 0)
        //    {
        //        try
        //        {
        //            IntPtr hbitmap = new IntPtr(icommand_0.Bitmap);
        //            Bitmap bitmap = Image.FromHbitmap(hbitmap);
        //            bitmap.MakeTransparent();
        //            item.Glyph = bitmap;
        //        }
        //        catch
        //        {
        //        }
        //    }
        //    return item;
        //}

        public string D2DMS(double double_0)
        {
            bool flag = false;
            if (double_0 < 0.0)
            {
                double_0 = -double_0;
                flag = true;
            }
            int num = (int) double_0;
            double_0 = (double_0 - num)*60.0;
            int num2 = (int) double_0;
            double num3 = Math.Round((double) ((double_0 - num2)*60.0), 2);
            string str = num.ToString() + "\x00b0" + num2.ToString("00") + "′" + num3.ToString("00.00") + "″";
            if (flag)
            {
                str = "-" + str;
            }
            return str;
        }

        public static double DefaultIndexGrid(IFeatureClass ifeatureClass_0)
        {
            int num5;
            if ((ifeatureClass_0.ShapeType == esriGeometryType.esriGeometryMultipoint) ||
                (ifeatureClass_0.ShapeType == esriGeometryType.esriGeometryPoint))
            {
                return DefaultIndexGridPoint(ifeatureClass_0);
            }
            ArrayList list = new ArrayList();
            int num2 = ifeatureClass_0.FeatureCount(null) - 1;
            if (num2 <= 0)
            {
                return 1000.0;
            }
            int num3 = num2*1;
            if (num3 > 1000)
            {
                num3 = 1000;
            }
            string name = ifeatureClass_0.Fields.get_Field(0).Name;
            for (int i = 0; i < num2; i += int.Parse(num5.ToString()))
            {
                list.Add(i);
                num5 = num2/num3;
            }
            double num6 = 1.0;
            double num7 = 0.0;
            double num8 = 100000000000;
            int num9 = 1;
            for (int j = 0; j < list.Count; j += 250)
            {
                int num11 = MinValue(list.Count - j, 250);
                string str2 = name + " IN(";
                for (int k = 0; k < num11; k++)
                {
                    str2 = str2 + list[j + k].ToString() + ",";
                }
                str2 = str2.Substring(0, str2.Length - 1) + ")";
                IQueryFilter filter = new QueryFilter
                {
                    WhereClause = str2
                };
                IFeatureCursor o = ifeatureClass_0.Search(filter, true);
                for (IFeature feature = o.NextFeature(); feature != null; feature = o.NextFeature())
                {
                    IEnvelope extent = feature.Extent;
                    num7 = MaxValue(num7, MaxValue(extent.Width, extent.Height));
                    num8 = MinValue(num8, MinValue(extent.Width, extent.Height));
                    if (num8 != 0.0)
                    {
                        num6 += MinValue(extent.Width, extent.Height)/MaxValue(extent.Width, extent.Height);
                    }
                    else
                    {
                        num6 += 0.0001;
                    }
                }
                ComReleaser.ReleaseCOMObject(o);
            }
            if ((num6/((double) num3)) > 0.5)
            {
                return ((num8 + ((num7 - num8)/2.0))*num9);
            }
            return ((num7/2.0)*num9);
        }

        protected static double DefaultIndexGridPoint(IFeatureClass ifeatureClass_0)
        {
            IGeoDataset dataset = (IGeoDataset) ifeatureClass_0;
            IEnvelope extent = dataset.Extent;
            long num2 = ifeatureClass_0.FeatureCount(null);
            if ((num2 == 0L) || extent.IsEmpty)
            {
                return 1000.0;
            }
            double num3 = extent.Height*extent.Width;
            return Math.Sqrt(num3/((double) num2));
        }

        public static void DeleteAllTempFile()
        {
            object registryValue = GetRegistryValue("TEMPFILELIST");
            if (registryValue is string[])
            {
                foreach (string str in registryValue as string[])
                {
                    DeleteFile(str);
                }
            }
            SetRegistryValue("TEMPFILELIST", new string[0], RegistryValueKind.MultiString);
        }

        private static void DeleteFile(string string_0)
        {
            string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(string_0);
            string extension = System.IO.Path.GetExtension(string_0);
            string path = string_0.Substring(0, (string_0.Length - fileNameWithoutExtension.Length) - extension.Length);
            if (Directory.Exists(path))
            {
                foreach (string str4 in Directory.GetFiles(path))
                {
                    try
                    {
                        if (str4.Contains(fileNameWithoutExtension))
                        {
                            FileInfo info = new FileInfo(str4)
                            {
                                Attributes = FileAttributes.Normal
                            };
                            File.Delete(str4);
                        }
                    }
                    catch (Exception exception)
                    {
                        Debug.WriteLine(exception.ToString());
                    }
                }
            }
        }

        public static double distance(IPoint ipoint_0, IPoint ipoint_1)
        {
            double num = ipoint_1.X - ipoint_0.X;
            double num2 = ipoint_1.Y - ipoint_0.Y;
            double d = (num*num) + (num2*num2);
            return Math.Sqrt(d);
        }

        public static double distance(double double_0, double double_1, double double_2, double double_3)
        {
            double num = double_0 - double_2;
            double num2 = double_1 - double_3;
            double d = (num*num) + (num2*num2);
            return Math.Sqrt(d);
        }

        public static double DistanceToLine(IPoint ipoint_0, IPoint ipoint_1, double double_0)
        {
            double_0 = (double_0/180.0)*3.1415926535897931;
            double num = ipoint_0.X - ipoint_1.X;
            double num2 = ipoint_0.Y - ipoint_1.Y;
            return ((num2*Math.Cos(double_0)) - (num*Math.Sin(double_0)));
        }

        public static bool FeatureClassHasData(IFeatureClass ifeatureClass_0)
        {
            return (ifeatureClass_0.FeatureCount(null) > 0);
        }

        public static string FormatLen(string string_0, string string_1, int int_0)
        {
            int length = string_0.Length;
            if (length >= int_0)
            {
                return string_0;
            }
            string str2 = string_0;
            for (int i = 0; i < (int_0 - length); i++)
            {
                str2 = string_1 + str2;
            }
            return str2;
        }

        public static string GetDataTypeDescriptor(esriNetworkAttributeDataType esriNetworkAttributeDataType_0)
        {
            switch (esriNetworkAttributeDataType_0)
            {
                case esriNetworkAttributeDataType.esriNADTInteger:
                    return "整型";

                case esriNetworkAttributeDataType.esriNADTFloat:
                    return "浮点";

                case esriNetworkAttributeDataType.esriNADTDouble:
                    return "双精度";

                case esriNetworkAttributeDataType.esriNADTBoolean:
                    return "布尔型";
            }
            return "未知";
        }

        public static object GetFeatuerValue(IFeature ifeature_0, string string_0)
        {
            int index = ifeature_0.Fields.FindField(string_0);
            if (index == -1)
            {
                return null;
            }
            return ifeature_0.get_Value(index);
        }

        public static string GetFeatureClassType(IFeatureClass ifeatureClass_0)
        {
            switch (ifeatureClass_0.ShapeType)
            {
                case esriGeometryType.esriGeometryPoint:
                    return " POINT";

                case esriGeometryType.esriGeometryMultipoint:
                    return " POINT";

                case esriGeometryType.esriGeometryPolyline:
                    return " LINE";

                case esriGeometryType.esriGeometryPolygon:
                    return " FILL";
            }
            return "";
        }

        public static string GetFeatureClassType(IFeatureClassName ifeatureClassName_0)
        {
            switch (ifeatureClassName_0.ShapeType)
            {
                case esriGeometryType.esriGeometryPoint:
                    return " POINT";

                case esriGeometryType.esriGeometryMultipoint:
                    return " POINT";

                case esriGeometryType.esriGeometryPolyline:
                    return " LINE";

                case esriGeometryType.esriGeometryPolygon:
                    return " FILL";
            }
            return "";
        }

        public static IFeatureLayer GetFeatureLayerByFeatureClassName(IMap imap_0, string string_0)
        {
            IEnumLayer layer = imap_0.get_Layers(null, true);
            layer.Reset();
            for (ILayer layer2 = layer.Next(); layer2 != null; layer2 = layer.Next())
            {
                if (layer2 is IFeatureLayer)
                {
                    IFeatureLayer layer3 = layer2 as IFeatureLayer;
                    IFeatureClass featureClass = layer3.FeatureClass;
                    if (featureClass != null)
                    {
                        string[] strArray = (featureClass as IDataset).Name.Split(new char[] {'.'});
                        string str = strArray[strArray.Length - 1];
                        if (str == string_0)
                        {
                            return (layer2 as IFeatureLayer);
                        }
                    }
                }
            }
            return null;
        }

        public static IPoint GetGD(IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2)
        {
            double num = azimuth(ipoint_0, ipoint_1);
            double num2 = Math.Abs(DistanceToLine(ipoint_2, ipoint_0, num));
            double d = ((num + 270.0)/180.0)*3.1415926535897931;
            return new ESRI.ArcGIS.Geometry.Point
            {
                X = ipoint_1.X + (num2*Math.Cos(d)),
                Y = ipoint_1.Y + (num2*Math.Sin(d))
            };
        }

        public static int GetLayerIndex(IGroupLayer igroupLayer_0, ILayer ilayer_0)
        {
            if (!((ilayer_0 is IGeoFeatureLayer) || (ilayer_0 is IGroupLayer)))
            {
                return (igroupLayer_0 as ICompositeLayer).Count;
            }
            int num2 = 0;
            for (int i = 0; i < (igroupLayer_0 as ICompositeLayer).Count; i++)
            {
                ILayer layer = (igroupLayer_0 as ICompositeLayer).get_Layer(i);
                if (layer is IGroupLayer)
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

        public static int GetLayerIndex(List<ILayer> list_0, ILayer ilayer_0)
        {
            if (!((ilayer_0 is IGeoFeatureLayer) || (ilayer_0 is IGroupLayer)))
            {
                return list_0.Count;
            }
            int num2 = 0;
            foreach (ILayer layer in list_0)
            {
                if (layer is IGroupLayer)
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

        public static int GetLayerIndexInMap(IBasicMap ibasicMap_0, ILayer ilayer_0)
        {
            for (int i = 0; i < ibasicMap_0.LayerCount; i++)
            {
                if (ibasicMap_0.get_Layer(i) == ilayer_0)
                {
                    return i;
                }
            }
            return -1;
        }

        public static string GetNetworkUnitTypeDescriptor(esriNetworkAttributeUnits esriNetworkAttributeUnits_0)
        {
            switch (esriNetworkAttributeUnits_0)
            {
                case esriNetworkAttributeUnits.esriNAUUnknown:
                    return "未知";

                case esriNetworkAttributeUnits.esriNAUInches:
                    return "英寸";

                case esriNetworkAttributeUnits.esriNAUFeet:
                    return "英尺";

                case esriNetworkAttributeUnits.esriNAUYards:
                    return "码";

                case esriNetworkAttributeUnits.esriNAUMiles:
                    return "英里";

                case esriNetworkAttributeUnits.esriNAUNauticalMiles:
                    return "海里";

                case esriNetworkAttributeUnits.esriNAUMillimeters:
                    return "毫米";

                case esriNetworkAttributeUnits.esriNAUCentimeters:
                    return "厘米";

                case esriNetworkAttributeUnits.esriNAUMeters:
                    return "米";

                case esriNetworkAttributeUnits.esriNAUKilometers:
                    return "公里";

                case esriNetworkAttributeUnits.esriNAUDecimalDegrees:
                    return "十进制度";

                case esriNetworkAttributeUnits.esriNAUDecimeters:
                    return "分米";

                case esriNetworkAttributeUnits.esriNAUSeconds:
                    return "秒";

                case esriNetworkAttributeUnits.esriNAUMinutes:
                    return "分钟";

                case esriNetworkAttributeUnits.esriNAUHours:
                    return "小时";

                case esriNetworkAttributeUnits.esriNAUDays:
                    return "天";
            }
            return "未知";
        }

        public static object GetRegistryValue(string string_0)
        {
            object obj2 = null;
            RegistryKey localMachine = Registry.LocalMachine;
            try
            {
                RegistryKey key2 =
                    localMachine.OpenSubKey(@"SOFTWARE\" + Application.CompanyName + @"\" + Application.ProductName);
                if (key2 == null)
                {
                    return obj2;
                }
                try
                {
                    obj2 = key2.GetValue(string_0);
                }
                finally
                {
                    key2.Close();
                }
            }
            finally
            {
                localMachine.Close();
            }
            return obj2;
        }

        public static IEnvelope GetSelectFeatureEnvelop(IFeatureLayer ifeatureLayer_0)
        {
            ICursor cursor;
            IFeatureSelection selection = ifeatureLayer_0 as IFeatureSelection;
            if (selection.SelectionSet.Count == 0)
            {
                return null;
            }
            IEnvelope extent = null;
            IEnvelope inEnvelope = null;
            IFeature feature = null;
            double dx = 5.0;
            selection.SelectionSet.Search(null, false, out cursor);
            IRow row = cursor.NextRow();
            while (true)
            {
                if (row == null)
                {
                    break;
                }
                feature = row as IFeature;
                if ((feature != null) && (feature.Shape != null))
                {
                    try
                    {
                        if (extent == null)
                        {
                            extent = feature.Extent;
                            if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                            {
                                extent.Expand(dx, dx, false);
                            }
                        }
                        else
                        {
                            inEnvelope = feature.Extent;
                            if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                            {
                                inEnvelope.Expand(dx, dx, false);
                            }
                            extent.Union(inEnvelope);
                        }
                    }
                    catch
                    {
                    }
                }
                row = cursor.NextRow();
            }
            ComReleaser.ReleaseCOMObject(cursor);
            return extent;
        }

        public static IEnvelope GetSelectFeatureEnvelop(IEnumFeature ienumFeature_0)
        {
            ienumFeature_0.Reset();
            IEnvelope envelope = null;
            IEnvelope inEnvelope = null;
            IFeature feature = ienumFeature_0.Next();
            double dx = 5.0;
            envelope = feature.Shape.Envelope;
            if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                envelope.Expand(dx, dx, false);
            }
            for (feature = ienumFeature_0.Next(); feature != null; feature = ienumFeature_0.Next())
            {
                inEnvelope = feature.Shape.Envelope;
                if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                {
                    inEnvelope.Expand(dx, dx, false);
                }
                envelope.Union(inEnvelope);
            }
            return envelope;
        }

        public static string GetShapeString(IFeatureClass ifeatureClass_0)
        {
            if (ifeatureClass_0 == null)
            {
                return "";
            }
            string str2 = "";
            switch (ifeatureClass_0.ShapeType)
            {
                case esriGeometryType.esriGeometryPoint:
                    str2 = "点";
                    break;

                case esriGeometryType.esriGeometryMultipoint:
                    str2 = "多点";
                    break;

                case esriGeometryType.esriGeometryPolyline:
                    str2 = "线";
                    break;

                case esriGeometryType.esriGeometryPolygon:
                    str2 = "多边形";
                    break;

                case esriGeometryType.esriGeometryMultiPatch:
                    str2 = "多面";
                    break;
            }
            int index = ifeatureClass_0.Fields.FindField(ifeatureClass_0.ShapeFieldName);
            IGeometryDef geometryDef = ifeatureClass_0.Fields.get_Field(index).GeometryDef;
            str2 = str2 + " ";
            if (geometryDef.HasZ)
            {
                str2 = str2 + "Z";
            }
            if (geometryDef.HasM)
            {
                str2 = str2 + "M";
            }
            return str2;
        }

        public static string GetSuffix()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        public static string GetTempFile(string string_0)
        {
            string suffix = GetSuffix();
            string item = GetTempPath() + suffix + "." + string_0;
            object registryValue = GetRegistryValue("TEMPFILELIST");
            List<string> list = new List<string>();
            if (registryValue is string[])
            {
                list = ArrayToList<string>(registryValue as string[]);
            }
            list.Add(item);
            SetRegistryValue("TEMPFILELIST", list.ToArray(), RegistryValueKind.MultiString);
            return item;
        }

        public static string GetTempPath()
        {
            string path = string.Empty;
            object registryValue = GetRegistryValue("TempDirectory");
            if (registryValue != null)
            {
                path = registryValue.ToString();
                if (path.Substring(path.Length - 1, 1) != @"\")
                {
                    path = path + @"\";
                }
            }
            if (!Directory.Exists(path))
            {
                path = System.IO.Path.GetTempPath();
            }
            return path;
        }

        public static void GetUniqueValues(ILayer ilayer_0, string string_0, IList ilist_0)
        {
            try
            {
                IAttributeTable table = ilayer_0 as IAttributeTable;
                if (table != null)
                {
                    GetUniqueValues(table.AttributeTable, string_0, ilist_0);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public static void GetUniqueValues(ITable itable_0, string string_0, IList ilist_0)
        {
            try
            {
                IQueryFilter queryFilter = new QueryFilter
                {
                    SubFields = string_0,
                    WhereClause = ""
                };
                (queryFilter as IQueryFilterDefinition).PostfixClause = "Order by " + string_0;
                ICursor o = itable_0.Search(queryFilter, false);
                IDataStatistics statistics = new DataStatistics
                {
                    Field = string_0,
                    Cursor = o
                };
                IEnumerator uniqueValues = statistics.UniqueValues;
                uniqueValues.Reset();
                while (uniqueValues.MoveNext())
                {
                    ilist_0.Add(uniqueValues.Current);
                }
                ComReleaser.ReleaseCOMObject(statistics);
                ComReleaser.ReleaseCOMObject(o);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public static void GetUniqueValuesEx(ILayer ilayer_0, string string_0, IList ilist_0)
        {
            try
            {
                IAttributeTable table = ilayer_0 as IAttributeTable;
                if (table != null)
                {
                    GetUniqueValues(table.AttributeTable, string_0, ilist_0);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public static void GetUniqueValuesEx(ITable itable_0, string string_0, IList ilist_0)
        {
            try
            {
                IQueryFilter queryFilter = new QueryFilter
                {
                    SubFields = string_0,
                    WhereClause = ""
                };
                (queryFilter as IQueryFilterDefinition).PostfixClause = "Order by " + string_0;
                ICursor o = itable_0.Search(queryFilter, false);
                IDataStatistics statistics = new DataStatistics
                {
                    Field = string_0,
                    Cursor = o
                };
                IEnumerator uniqueValues = statistics.UniqueValues;
                uniqueValues.Reset();
                int index = o.Fields.FindField(string_0);
                esriFieldType type = o.Fields.get_Field(index).Type;
                while (uniqueValues.MoveNext())
                {
                    ilist_0.Add(ConvertFieldValueToString(type, uniqueValues.Current));
                }
                ComReleaser.ReleaseCOMObject(statistics);
                ComReleaser.ReleaseCOMObject(o);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public static string GetUnit(esriUnits esriUnits_0)
        {
            switch (esriUnits_0)
            {
                case esriUnits.esriUnknownUnits:
                    return "未知单位";

                case esriUnits.esriInches:
                    return "英寸";

                case esriUnits.esriPoints:
                    return "点";

                case esriUnits.esriFeet:
                    return "英尺";

                case esriUnits.esriYards:
                    return "码";

                case esriUnits.esriMiles:
                    return "英里";

                case esriUnits.esriNauticalMiles:
                    return "海里";

                case esriUnits.esriMillimeters:
                    return "毫米";

                case esriUnits.esriCentimeters:
                    return "厘米";

                case esriUnits.esriMeters:
                    return "米";

                case esriUnits.esriKilometers:
                    return "公里";

                case esriUnits.esriDecimalDegrees:
                    return "度";

                case esriUnits.esriDecimeters:
                    return "分米";
            }
            return "未知单位";
        }

        public static string GetUsageTypeDescriptor(esriNetworkAttributeUsageType esriNetworkAttributeUsageType_0)
        {
            switch (esriNetworkAttributeUsageType_0)
            {
                case esriNetworkAttributeUsageType.esriNAUTCost:
                    return "费用";

                case esriNetworkAttributeUsageType.esriNAUTDescriptor:
                    return "描述符";

                case esriNetworkAttributeUsageType.esriNAUTRestriction:
                    return "约束";

                case esriNetworkAttributeUsageType.esriNAUTHierarchy:
                    return "层级";
            }
            return "未知";
        }

        public static bool IsNmuber(string string_0)
        {
            double num;
            return double.TryParse(string_0, out num);
        }

        public static bool LayerHasData(IFeatureLayer ifeatureLayer_0)
        {
            if (ifeatureLayer_0 is IGeoFeatureLayer)
            {
                return (ifeatureLayer_0.FeatureClass.FeatureCount(null) > 0);
            }
            if (ifeatureLayer_0 is IFDOGraphicsLayer)
            {
                return ((ifeatureLayer_0 as IAttributeTable).AttributeTable.RowCount(null) > 0);
            }
            return true;
        }

        public static double M2ToHectare(double double_0)
        {
            try
            {
                return (double_0*0.0001);
            }
            catch
            {
                return 0.0;
            }
        }

        public static double M2ToMeng(double double_0)
        {
            try
            {
                return (double_0*0.0015);
            }
            catch
            {
                return 0.0;
            }
        }

        private static double MaxValue(double double_0, double double_1)
        {
            return ((double_0 > double_1) ? double_0 : double_1);
        }

        private static int MaxValue(int int_0, int int_1)
        {
            return ((int_0 > int_1) ? int_0 : int_1);
        }

        private static long MaxValue(long long_0, long long_1)
        {
            return ((long_0 > long_1) ? long_0 : long_1);
        }

        public static double measureArea(IPoint ipoint_0, int int_0, ref IPointCollection ipointCollection_0)
        {
            object before = Missing.Value;
            if ((int_0 == 1) && (ipointCollection_0 == null))
            {
                ipointCollection_0 = new Polygon();
                ipointCollection_0.AddPoint(ipoint_0, ref before, ref before);
                return 0.0;
            }
            if (ipointCollection_0 != null)
            {
                IArea area;
                switch (int_0)
                {
                    case 1:
                        ipointCollection_0.AddPoint(ipoint_0, ref before, ref before);
                        if (ipointCollection_0.PointCount <= 1)
                        {
                            break;
                        }
                        area = (IArea) ipointCollection_0;
                        return Math.Abs(area.Area);

                    case 2:
                    {
                        if (ipointCollection_0.PointCount <= 1)
                        {
                            break;
                        }
                        IPointCollection points = new Polygon();
                        points.AddPointCollection(ipointCollection_0);
                        points.AddPoint(ipoint_0, ref before, ref before);
                        area = (IArea) points;
                        return Math.Abs(area.Area);
                    }
                    case 3:
                        ipointCollection_0.AddPoint(ipoint_0, ref before, ref before);
                        if (ipointCollection_0.PointCount <= 2)
                        {
                            break;
                        }
                        area = (IArea) ipointCollection_0;
                        return Math.Abs(area.Area);
                }
            }
            return -1.0;
        }

        public static double measureLength(IPoint ipoint_0, int int_0, ref IPoint ipoint_1, ref IPoint ipoint_2,
            ref double double_0)
        {
            ILine line = new Line();
            double length = 0.0;
            if ((int_0 == 1) && (ipoint_1 == null))
            {
                double_0 = 0.0;
                ipoint_1 = ipoint_0;
                return 0.0;
            }
            if (ipoint_1 != null)
            {
                switch (int_0)
                {
                    case 1:
                        line.PutCoords(ipoint_1, ipoint_0);
                        length = line.Length;
                        double_0 += length;
                        ipoint_1 = ipoint_0;
                        return length;

                    case 2:
                        line.PutCoords(ipoint_1, ipoint_0);
                        length = line.Length;
                        double_0 += length;
                        ipoint_2 = ipoint_0;
                        return length;

                    case 3:
                        line.PutCoords(ipoint_1, ipoint_2);
                        length = 0.0;
                        double_0 += line.Length;
                        return 0.0;
                }
            }
            return -1.0;
        }

        private double method_0(IActiveView iactiveView_0, IPoint ipoint_0, int int_0,
            ref INewLineFeedback inewLineFeedback_0, ref IPoint ipoint_1, ref IPoint ipoint_2, ref double double_0)
        {
            ILine line = new Line();
            double length = 0.0;
            if ((int_0 == 1) && (inewLineFeedback_0 == null))
            {
                double_0 = 0.0;
                inewLineFeedback_0 = new NewLineFeedback();
                inewLineFeedback_0.Display = iactiveView_0.ScreenDisplay;
                inewLineFeedback_0.Start(ipoint_0);
                ipoint_1 = ipoint_0;
                return 0.0;
            }
            if (inewLineFeedback_0 != null)
            {
                switch (int_0)
                {
                    case 1:
                        line.PutCoords(ipoint_1, ipoint_0);
                        length = line.Length;
                        double_0 += length;
                        inewLineFeedback_0.AddPoint(ipoint_0);
                        ipoint_1 = ipoint_0;
                        return length;

                    case 2:
                        line.PutCoords(ipoint_1, ipoint_0);
                        length = line.Length;
                        double_0 += length;
                        inewLineFeedback_0.MoveTo(ipoint_0);
                        ipoint_2 = ipoint_0;
                        return length;

                    case 3:
                        line.PutCoords(ipoint_1, ipoint_2);
                        length = 0.0;
                        double_0 += line.Length;
                        inewLineFeedback_0 = null;
                        iactiveView_0.Refresh();
                        return 0.0;
                }
            }
            return -1.0;
        }

        private double method_1(IActiveView iactiveView_0, IPoint ipoint_0, int int_0,
            ref INewPolygonFeedback inewPolygonFeedback_0, ref IPointCollection ipointCollection_0)
        {
            object before = Missing.Value;
            if ((int_0 == 1) && (inewPolygonFeedback_0 == null))
            {
                inewPolygonFeedback_0 = new NewPolygonFeedback();
                inewPolygonFeedback_0.Start(ipoint_0);
                inewPolygonFeedback_0.Display = iactiveView_0.ScreenDisplay;
                ipointCollection_0 = new Polygon();
                ipointCollection_0.AddPoint(ipoint_0, ref before, ref before);
                return 0.0;
            }
            if (inewPolygonFeedback_0 != null)
            {
                IArea area;
                switch (int_0)
                {
                    case 1:
                        inewPolygonFeedback_0.AddPoint(ipoint_0);
                        ipointCollection_0.AddPoint(ipoint_0, ref before, ref before);
                        break;

                    case 2:
                    {
                        inewPolygonFeedback_0.MoveTo(ipoint_0);
                        if (ipointCollection_0.PointCount <= 1)
                        {
                            break;
                        }
                        IPointCollection points = new Polygon();
                        points.AddPointCollection(ipointCollection_0);
                        points.AddPoint(ipoint_0, ref before, ref before);
                        area = (IArea) points;
                        return area.Area;
                    }
                    case 3:
                    {
                        IPolygon polygon = inewPolygonFeedback_0.Stop();
                        inewPolygonFeedback_0 = null;
                        ipointCollection_0 = null;
                        iactiveView_0.Refresh();
                        if (polygon.IsEmpty)
                        {
                            return -1.0;
                        }
                        area = (IArea) polygon;
                        return area.Area;
                    }
                }
            }
            return -1.0;
        }

        private static double MinValue(double double_0, double double_1)
        {
            return ((double_0 < double_1) ? double_0 : double_1);
        }

        private static int MinValue(int int_0, int int_1)
        {
            return ((int_0 < int_1) ? int_0 : int_1);
        }

        private static long MinValue(long long_0, long long_1)
        {
            return ((long_0 < long_1) ? long_0 : long_1);
        }

        public static void Pan2Feature(IActiveView iactiveView_0, IFeature ifeature_0)
        {
            try
            {
                IEnvelope envelope = null;
                envelope = ifeature_0.Shape.Envelope;
                if (ifeature_0.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                {
                    double dx = ConvertPixelsToMapUnits(iactiveView_0, 5.0);
                    envelope.Expand(dx, dx, false);
                }
                if (envelope != null)
                {
                    IEnvelope extent = iactiveView_0.Extent;
                    IPoint p = new ESRI.ArcGIS.Geometry.Point
                    {
                        X = (envelope.XMin + envelope.XMax)/2.0,
                        Y = (envelope.YMin + envelope.YMax)/2.0
                    };
                    extent.CenterAt(p);
                    iactiveView_0.Extent = extent;
                    iactiveView_0.Refresh();
                }
            }
            catch (Exception exception)
            {
                //  Logger.Current.Error("",exception, "");
            }
        }

        private static void ReturnMessages(ESRI.ArcGIS.Geoprocessor.Geoprocessor geoprocessor_0)
        {
            if (geoprocessor_0.MessageCount > 0)
            {
                for (int i = 0; i <= (geoprocessor_0.MessageCount - 1); i++)
                {
                    Console.WriteLine(geoprocessor_0.GetMessage(i));
                }
            }
        }

        public static bool RunTool(ESRI.ArcGIS.Geoprocessor.Geoprocessor geoprocessor_0, IGPProcess igpprocess_0,
            ITrackCancel itrackCancel_0)
        {
            geoprocessor_0.OverwriteOutput = true;
            geoprocessor_0.ClearMessages();
            try
            {
                geoprocessor_0.Execute(igpprocess_0, itrackCancel_0);
                ReturnMessages(geoprocessor_0);
                return true;
            }
            catch (Exception)
            {
                ReturnMessages(geoprocessor_0);
                return false;
            }
        }

        public static object SaveObject(IPersistStream ipersistStream_0)
        {
            IStream pstm = new MemoryBlobStream();
            ipersistStream_0.Save(pstm, 1);
            return pstm;
        }

        public static void SetFeatuerValue(IFeature ifeature_0, string string_0, object object_0)
        {
            int index = ifeature_0.Fields.FindField(string_0);
            if (index != -1)
            {
                ifeature_0.set_Value(index, object_0);
                ifeature_0.Store();
            }
        }

        public static void SetRegistryValue(string string_0, object object_0)
        {
            SetRegistryValue(string_0, object_0, RegistryValueKind.String);
        }

        public static void SetRegistryValue(string string_0, object object_0, RegistryValueKind registryValueKind_0)
        {
            RegistryKey localMachine = Registry.LocalMachine;
            RegistryKey key2 =
                localMachine.CreateSubKey(@"SOFTWARE\" + Application.CompanyName + @"\" + Application.ProductName);
            key2.SetValue(string_0, object_0, registryValueKind_0);
            key2.Close();
            localMachine.Close();
        }

        public static decimal TDec(object object_0)
        {
            try
            {
                return decimal.Parse(object_0.ToString());
            }
            catch
            {
                return 0M;
            }
        }

        public static int TInt(object object_0)
        {
            int result = 0;
            try
            {
                if (!int.TryParse(object_0.ToString(), out result))
                {
                    result = 0;
                }
            }
            catch
            {
                result = 0;
            }
            return result;
        }

        public static long TLng(object object_0)
        {
            try
            {
                return long.Parse(object_0.ToString());
            }
            catch
            {
                return 0L;
            }
        }

        public static double TNum(object object_0)
        {
            double result = 0.0;
            try
            {
                if (!double.TryParse(object_0.ToString(), out result))
                {
                    result = 0.0;
                }
            }
            catch
            {
                result = 0.0;
            }
            return result;
        }

        public static string TNV(object object_0)
        {
            try
            {
                return object_0.ToString();
            }
            catch
            {
                return "";
            }
        }

        public static string ToDateStr(string string_0)
        {
            if (string_0 == "")
            {
                return "";
            }
            try
            {
                DateTime time = Convert.ToDateTime(string_0);
                return (FormatLen(time.Year.ToString(), "0", 4) + "-" + FormatLen(time.Month.ToString(), "0", 2) + "-" +
                        FormatLen(time.Day.ToString(), "0", 2));
            }
            catch
            {
                return "";
            }
        }

        public static DateTime ToDateTime(string string_0)
        {
            DateTime now = DateTime.Now;
            if (string_0 != "")
            {
                try
                {
                    if (DateTime.TryParse(string_0, out now))
                    {
                        now = DateTime.Now;
                    }
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception.ToString());
                }
            }
            return now;
        }

        public static void UnionEnvelop(IEnvelope ienvelope_0, IEnvelope ienvelope_1)
        {
            if (ienvelope_0.IsEmpty)
            {
                if (ienvelope_0.IsEmpty)
                {
                    return;
                }
                ienvelope_0.PutCoords(ienvelope_1.XMin, ienvelope_1.YMin, ienvelope_1.XMax, ienvelope_1.YMax);
            }
            IEnvelope inEnvelope = ienvelope_1;
            if (((!(inEnvelope.SpatialReference is IUnknownCoordinateSystem) &&
                  !(ienvelope_0.SpatialReference is IUnknownCoordinateSystem)) && (ienvelope_0.SpatialReference != null)) &&
                (inEnvelope.SpatialReference != null))
            {
                if (
                    !((!(inEnvelope.SpatialReference is IUnknownCoordinateSystem) ||
                       !(ienvelope_0.SpatialReference is IUnknownCoordinateSystem))
                        ? ((ienvelope_0.SpatialReference != null) || (inEnvelope.SpatialReference != null))
                        : false))
                {
                    ienvelope_0.Union(inEnvelope);
                }
                else
                {
                    inEnvelope.Project(ienvelope_0.SpatialReference);
                    ienvelope_0.Union(inEnvelope);
                }
            }
        }

        public static List<T> UnionList<T>(List<T> list_0, List<T> list_1)
        {
            foreach (T local in list_1)
            {
                list_0.Add(local);
            }
            return list_0;
        }

        public static T[] UnionList<T>(T[] gparam_0, T[] gparam_1)
        {
            List<T> list = new List<T>();
            foreach (T local in gparam_1)
            {
                list.Add(local);
            }
            foreach (T local in gparam_0)
            {
                list.Add(local);
            }
            return list.ToArray();
        }

        public static void Zoom2Feature(IActiveView iactiveView_0, IFeature ifeature_0)
        {
            try
            {
                IEnvelope envelope = null;
                envelope = ifeature_0.Shape.Envelope;
                if (ifeature_0.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                {
                    double dx = ConvertPixelsToMapUnits(iactiveView_0, 5.0);
                    envelope.Expand(dx, dx, false);
                }
                if (envelope != null)
                {
                    iactiveView_0.Extent = envelope;
                    iactiveView_0.Refresh();
                }
            }
            catch (Exception exception)
            {
                // Logger.Current.Error("",exception, "");
            }
        }

        public static void Zoom2Features(IActiveView iactiveView_0, IArray iarray_0)
        {
            try
            {
                IEnvelope envelope = null;
                IEnvelope inEnvelope = null;
                double dx = ConvertPixelsToMapUnits(iactiveView_0, 5.0);
                for (int i = 0; i < iarray_0.Count; i++)
                {
                    IFeature feature = iarray_0.get_Element(i) as IFeature;
                    if (i == 0)
                    {
                        envelope = feature.Shape.Envelope;
                        if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                        {
                            envelope.Expand(dx, dx, false);
                        }
                    }
                    else
                    {
                        inEnvelope = feature.Shape.Envelope;
                        if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                        {
                            inEnvelope.Expand(dx, dx, false);
                        }
                        envelope.Union(inEnvelope);
                    }
                }
                if (envelope != null)
                {
                    iactiveView_0.Extent = envelope;
                    iactiveView_0.Refresh();
                }
            }
            catch (Exception exception)
            {
                // Logger.Current.Error("",exception, "");
            }
        }

        public static void Zoom2SelectedFeature(IScene iscene_0)
        {
            if (iscene_0.SelectionCount >= 1)
            {
                ISceneGraph sceneGraph;
                IFeature feature = null;
                IEnumFeature featureSelection = iscene_0.FeatureSelection as IEnumFeature;
                featureSelection.Reset();
                IEnvelope pExtent = null;
                double dx = 5.0;
                if (iscene_0.SelectionCount == 1)
                {
                    feature = featureSelection.Next();
                    pExtent = feature.Shape.Envelope;
                    if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                    {
                        pExtent.Expand(dx, dx, false);
                    }
                    if (iscene_0 is IGlobe)
                    {
                        ICamera camera = (iscene_0 as IGlobe).GlobeDisplay.ActiveViewer.Camera;
                        double num1 = (pExtent.XMin + pExtent.XMax)/2.0;
                        double num2 = (pExtent.YMin + pExtent.YMax)/2.0;
                        double num3 = (pExtent.ZMin + pExtent.ZMax)/2.0;
                        camera.ZoomToRect(pExtent);
                        (iscene_0 as IGlobe).GlobeDisplay.RefreshViewers();
                    }
                    else
                    {
                        sceneGraph = iscene_0.SceneGraph;
                        sceneGraph.ActiveViewer.Camera.SetDefaultsMBB(pExtent);
                        sceneGraph.RefreshViewers();
                    }
                }
                else
                {
                    pExtent = GetSelectFeatureEnvelop(featureSelection);
                    if (iscene_0 is IGlobe)
                    {
                        (iscene_0 as IGlobe).GlobeDisplay.ActiveViewer.Camera.ZoomToRect(pExtent);
                        (iscene_0 as IGlobe).GlobeDisplay.ActiveViewer.Redraw(true);
                    }
                    else
                    {
                        sceneGraph = iscene_0.SceneGraph;
                        sceneGraph.ActiveViewer.Camera.SetDefaultsMBB(pExtent);
                        sceneGraph.RefreshViewers();
                    }
                }
            }
        }

        public static void Zoom2SelectedFeature(IActiveView iactiveView_0)
        {
            try
            {
                IMap focusMap = iactiveView_0.FocusMap;
                if (focusMap != null)
                {
                    IFeature feature = null;
                    if (focusMap.SelectionCount != 0)
                    {
                        IEnumFeature featureSelection = focusMap.FeatureSelection as IEnumFeature;
                        if (focusMap.SelectionCount == 1)
                        {
                            feature = featureSelection.Next();
                            if (feature.Shape != null)
                            {
                                if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                                {
                                    (focusMap as IActiveView).Extent.CenterAt(feature.Shape as IPoint);
                                }
                                else
                                {
                                    (focusMap as IActiveView).Extent = feature.Shape.Envelope;
                                }
                                iactiveView_0.Refresh();
                            }
                        }
                        else
                        {
                            IEnvelope selectFeatureEnvelop = GetSelectFeatureEnvelop(featureSelection);
                            (focusMap as IActiveView).Extent = selectFeatureEnvelop;
                            iactiveView_0.Refresh();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //Logger.Current.Error("",exception, "");
            }
        }

        public static void Zoom2SelectedFeature(IScene iscene_0, IFeatureLayer ifeatureLayer_0)
        {
            try
            {
                IEnvelope selectFeatureEnvelop = GetSelectFeatureEnvelop(ifeatureLayer_0);
                if (selectFeatureEnvelop != null)
                {
                    if (iscene_0 is IGlobe)
                    {
                        (iscene_0 as IGlobe).GlobeDisplay.ActiveViewer.Camera.ZoomToRect(selectFeatureEnvelop);
                        (iscene_0 as IGlobe).GlobeDisplay.ActiveViewer.Redraw(true);
                    }
                    else
                    {
                        ISceneGraph sceneGraph = iscene_0.SceneGraph;
                        sceneGraph.ActiveViewer.Camera.SetDefaultsMBB(selectFeatureEnvelop);
                        sceneGraph.RefreshViewers();
                    }
                }
            }
            catch (Exception exception)
            {
                // Logger.Current.Error("",exception, "");
            }
        }

        public static void Zoom2SelectedFeature(IActiveView iactiveView_0, IFeatureLayer ifeatureLayer_0)
        {
            try
            {
                double dx = 5.0;
                IEnvelope selectFeatureEnvelop = GetSelectFeatureEnvelop(ifeatureLayer_0);
                if (selectFeatureEnvelop != null)
                {
                    selectFeatureEnvelop.Expand(dx, dx, false);
                    iactiveView_0.Extent = selectFeatureEnvelop;
                }
                iactiveView_0.Refresh();
            }
            catch (Exception exception)
            {
                // Logger.Current.Error("",exception, "");
            }
        }


    }
}
