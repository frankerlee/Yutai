using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GeoAnalyst;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Symbol;

namespace Yutai.ArcGIS.Common.Raster
{
    public class RasterUtility
    {
        public static void BuildPyramid(IRaster iraster_0)
        {
            IRasterBand band = ((IRasterBandCollection) iraster_0).Item(0);
            if (!((IRasterPyramid) band).Present)
            {
                ((IRasterPyramid) band).Create();
            }
        }

        public static void BuildPyramid(IRasterDataset irasterDataset_0)
        {
            if (!((IRasterPyramid) irasterDataset_0).Present)
            {
                ((IRasterPyramid) irasterDataset_0).Create();
            }
        }

        public static IRaster buildSlope(string string_0, string string_1, string string_2)
        {
            IWorkspaceFactory factory = new RasterWorkspaceFactory();
            IRasterDataset dataset = ((IRasterWorkspace) factory.OpenFromFile(string_0, 0)).OpenRasterDataset(string_1);
            ISurfaceOp op = new RasterSurfaceOp() as ISurfaceOp;
            IRasterAnalysisEnvironment environment = (IRasterAnalysisEnvironment) op;
            IWorkspaceFactory factory2 = new RasterWorkspaceFactory();
            IWorkspace workspace2 = factory2.OpenFromFile(string_2, 0);
            environment.OutWorkspace = workspace2;
            object zFactor = new object();
            zFactor = Missing.Value;
            return
                (IRaster)
                op.Slope((IGeoDataset) dataset, esriGeoAnalysisSlopeEnum.esriGeoAnalysisSlopeDegrees, ref zFactor);
        }

        public static bool ChangeRenderToUVRenderer(IRasterLayer irasterLayer_0, string string_0)
        {
            bool flag;
            bool flag3;
            IRaster raster = irasterLayer_0.Raster;
            IRasterBand band = (raster as IRasterBandCollection).Item(0);
            band.HasTable(out flag);
            if (!flag)
            {
                return false;
            }
            ITable attributeTable = band.AttributeTable;
            int pCount = attributeTable.RowCount(null);
            IRandomColorRamp ramp = new RandomColorRamp
            {
                Size = pCount,
                Seed = 100
            };
            ramp.CreateRamp(out flag3);
            IRasterUniqueValueRenderer renderer = new RasterUniqueValueRenderer();
            IRasterRenderer renderer2 = renderer as IRasterRenderer;
            renderer2.Raster = raster;
            renderer2.Update();
            renderer.HeadingCount = 1;
            renderer.set_Heading(0, "All Data Values");
            renderer.set_ClassCount(0, pCount);
            renderer.Field = string_0;
            int index = attributeTable.FindField(string_0);
            for (int i = 0; i < pCount; i++)
            {
                object obj2 = attributeTable.GetRow(i).get_Value(index);
                renderer.AddValue(0, i, obj2);
                renderer.set_Label(0, i, obj2.ToString());
                ISimpleFillSymbol symbol = new SimpleFillSymbol
                {
                    Color = ramp.get_Color(i)
                };
                renderer.set_Symbol(0, i, symbol as ISymbol);
            }
            renderer2.Update();
            irasterLayer_0.Renderer = renderer as IRasterRenderer;
            return true;
        }

        private static void ConvertShape2Raster(string string_0, double double_0, string string_1)
        {
            string directoryName;
            ArgumentException exception;
            string fileNameWithoutExtension;
            IWorkspace workspace;
            Exception exception2;
            IFeatureClass class2;
            IWorkspace workspace3;
            try
            {
                directoryName = System.IO.Path.GetDirectoryName(string_0);
            }
            catch (ArgumentException exception1)
            {
                exception = exception1;
                Console.WriteLine(exception.Message);
                Console.WriteLine("ConvertShape2Raster - invalid input Shapefile path for {0}", string_0);
                return;
            }
            try
            {
                fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(string_0);
            }
            catch (ArgumentException exception3)
            {
                exception = exception3;
                Console.WriteLine(exception.Message);
                Console.WriteLine("ConvertShape2Raster - invalid input Shapefile name for {0}", string_0);
                return;
            }
            IWorkspaceFactory factory = new ShapefileWorkspaceFactory();
            try
            {
                workspace = factory.OpenFromFile(directoryName, 0);
            }
            catch (Exception exception4)
            {
                exception2 = exception4;
                Console.WriteLine("Unable to find and open the shapefile workspace: {0}.", directoryName);
                Console.WriteLine(exception2.Message);
                return;
            }
            IFeatureWorkspace workspace2 = (IFeatureWorkspace) workspace;
            try
            {
                class2 = workspace2.OpenFeatureClass(fileNameWithoutExtension);
            }
            catch (Exception exception5)
            {
                exception2 = exception5;
                Console.WriteLine("Unable to find and open the shapefile: {0}.", fileNameWithoutExtension);
                Console.WriteLine(exception2.Message);
                return;
            }
            IGeoDataset dataset = (IGeoDataset) class2;
            IWorkspaceFactory factory2 = new RasterWorkspaceFactory();
            try
            {
                workspace3 = factory2.OpenFromFile(directoryName, 0);
            }
            catch (Exception exception6)
            {
                exception2 = exception6;
                Console.WriteLine("Unable to find and open the raster workspace: {0}.", directoryName);
                Console.WriteLine(exception2.Message);
                return;
            }
            IConversionOp op = new RasterConversionOp() as IConversionOp;
            try
            {
                IRasterAnalysisEnvironment environment = (IRasterAnalysisEnvironment) op;
                environment.OutWorkspace = workspace3;
                object cellSizeProvider = double_0;
                environment.SetCellSize(esriRasterEnvSettingEnum.esriRasterEnvValue, ref cellSizeProvider);
                object extent = dataset.Extent;
                object snapRasterData = 0;
                environment.SetExtent(esriRasterEnvSettingEnum.esriRasterEnvValue, ref extent, ref snapRasterData);
            }
            catch (Exception exception7)
            {
                exception2 = exception7;
                Console.WriteLine(
                    "Unable to set either the output workspace, or extent, or cell size for raster dataset");
                Console.WriteLine(exception2.Message);
                return;
            }
            op.ToRasterDataset(dataset, "GRID", workspace3, string_1);
        }

        public static IRasterCatalog createCatalog(IRasterWorkspaceEx irasterWorkspaceEx_0, string string_0,
            string string_1, string string_2, ISpatialReference ispatialReference_0,
            ISpatialReference ispatialReference_1, bool bool_0, IFields ifields_0, string string_3)
        {
            if (ifields_0 == null)
            {
                ifields_0 = createFields(string_1, string_2, bool_0, ispatialReference_0, ispatialReference_1);
            }
            if (string_3.Length == 0)
            {
                string_3 = "DEFAULTS";
            }
            IRasterCatalog catalog = null;
            try
            {
                catalog = irasterWorkspaceEx_0.CreateRasterCatalog(string_0, ifields_0, string_2, string_1, string_3);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            return catalog;
        }

        private static IFields createFields(string string_0, string string_1, bool bool_0,
            ISpatialReference ispatialReference_0, ISpatialReference ispatialReference_1)
        {
            IFieldsEdit edit = new Fields() as IFieldsEdit;
            edit.AddField(createOIDField("ObjectID"));
            edit.AddField(createNameField("name"));
            edit.AddField(createRasterField(string_0, bool_0, ispatialReference_1));
            edit.AddField(createShapeField(string_1, ispatialReference_0));
            return edit;
        }

        public static IRasterDataset createFileRasterDataset(IRasterWorkspace2 irasterWorkspace2_0, string string_0,
            int int_0, rstPixelType rstPixelType_0, ISpatialReference ispatialReference_0)
        {
            try
            {
                IRasterDataset dataset = null;
                IPoint origin = new Point();
                origin.PutCoords(0.0, 0.0);
                if (ispatialReference_0 == null)
                {
                    ispatialReference_0 = new UnknownCoordinateSystem() as ISpatialReference;
                }
                dataset = irasterWorkspace2_0.CreateRasterDataset(string_0, "IMAGINE Image", origin, 200, 100, 1.0, 1.0,
                    int_0, rstPixelType_0, ispatialReference_0, true);
                IRawPixels pixels = null;
                IPixelBlock3 block = null;
                IPnt tlc = null;
                IPnt size = null;
                IRasterBandCollection bands = (IRasterBandCollection) dataset;
                pixels = (IRawPixels) bands.Item(0);
                IRasterProps props = (IRasterProps) pixels;
                tlc = new DblPnt();
                tlc.SetCoords(0.0, 0.0);
                size = new DblPnt();
                size.SetCoords((double) props.Width, (double) props.Height);
                block = (IPixelBlock3) pixels.CreatePixelBlock(size);
                pixels.Read(tlc, (IPixelBlock) block);
                object[,] objArray = (object[,]) block.get_PixelDataByRef(0);
                for (int i = 0; i < props.Width; i++)
                {
                    for (int j = 0; j < props.Height; j++)
                    {
                        objArray[i, j] = (i*j)%255;
                    }
                }
                object cache = pixels.AcquireCache();
                pixels.Write(tlc, (IPixelBlock) block);
                pixels.ReturnCache(cache);
                return dataset;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                return null;
            }
        }

        private static IGeometryDef createGeometryDef(ISpatialReference ispatialReference_0)
        {
            IGeometryDefEdit edit = new GeometryDef() as IGeometryDefEdit;
            edit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
            edit.AvgNumPoints_2 = 4;
            edit.GridCount_2 = 1;
            edit.set_GridSize(0, 1000.0);
            if (ispatialReference_0 == null)
            {
                ispatialReference_0 = new UnknownCoordinateSystem() as ISpatialReference;
            }
            (ispatialReference_0 as ISpatialReferenceResolution).set_XYResolution(true, 0.001);
            (ispatialReference_0 as ISpatialReferenceResolution).SetDefaultXYResolution();
            edit.SpatialReference_2 = ispatialReference_0;
            return edit;
        }

        public static IGeometryDef CreateGeometryDef(ISpatialReference ispatialReference_0)
        {
            IGeometryDefEdit geometryDefEdit = new GeometryDef() as IGeometryDefEdit;
            geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
            geometryDefEdit.AvgNumPoints_2 = 4;
            if (ispatialReference_0 == null)
            {
                ispatialReference_0 = new UnknownCoordinateSystem() as ISpatialReference;
            }
            geometryDefEdit.SpatialReference_2 = ispatialReference_0;
            return geometryDefEdit;
        }

        private static IField createNameField(string string_0)
        {
            IFieldEdit field = new Field() as IFieldEdit;
            field.Name_2 = string_0;
            field.Type_2 = esriFieldType.esriFieldTypeString;
            return field;
        }

        private static IField createOIDField(string string_0)
        {
            IFieldEdit field = new Field() as IFieldEdit;
            field.Name_2 = string_0;
            field.Type_2 = esriFieldType.esriFieldTypeOID;
            return field;
        }

        public static IRasterDef CreateRasterDef(bool bool_0, ISpatialReference ispatialReference_0)
        {
            IRasterDef def = new RasterDef
            {
                Description = "Raster Dataset"
            };
            if (ispatialReference_0 == null)
            {
                ispatialReference_0 = new UnknownCoordinateSystem() as ISpatialReference;
            }
            def.SpatialReference = ispatialReference_0;
            return def;
        }

        private static IField createRasterField(string string_0, bool bool_0, ISpatialReference ispatialReference_0)
        {
            IFieldEdit2 edit = new Field() as IFieldEdit2;
            IRasterDef def = new RasterDef
            {
                Description = "this is a raster catalog",
                IsManaged = bool_0
            };
            if (string_0.Length == 0)
            {
                string_0 = "RASTER";
            }
            edit.Name_2 = string_0;
            edit.Type_2 = esriFieldType.esriFieldTypeRaster;
            if (ispatialReference_0 == null)
            {
                ispatialReference_0 = new UnknownCoordinateSystem() as ISpatialReference;
            }
            (ispatialReference_0 as ISpatialReferenceResolution).get_XYResolution(true);
            (ispatialReference_0 as ISpatialReferenceResolution).set_XYResolution(true, 0.001);
            (ispatialReference_0 as ISpatialReferenceResolution).SetDefaultXYResolution();
            def.SpatialReference = ispatialReference_0;
            edit.RasterDef = def;
            return edit;
        }

        public static IRasterStorageDef CreateRasterStorageDef()
        {
            return new RasterStorageDef
            {
                CompressionType = esriRasterCompressionType.esriRasterCompressionRLE,
                PyramidLevel = 2,
                PyramidResampleType = rstResamplingTypes.RSP_BilinearInterpolation,
                TileHeight = 128,
                TileWidth = 128
            };
        }

        public static IRasterDataset CreateRasterSurf(string string_0, string string_1, string string_2, IPoint ipoint_0,
            int int_0, int int_1, double double_0, double double_1, rstPixelType rstPixelType_0,
            ISpatialReference2 ispatialReference2_0, bool bool_0)
        {
            IWorkspaceFactory factory = new RasterWorkspaceFactory();
            IRasterWorkspace2 workspace2 = factory.OpenFromFile(string_0, 0) as IRasterWorkspace2;
            return workspace2.CreateRasterDataset(string_1, string_2, ipoint_0, int_0, int_1, double_0, double_1, 1,
                rstPixelType_0, ispatialReference2_0, bool_0);
        }

        public static IRasterDataset CreateRasterSurf(string string_0, string string_1, string string_2, IPoint ipoint_0,
            int int_0, int int_1, double double_0, double double_1, int int_2, rstPixelType rstPixelType_0,
            ISpatialReference2 ispatialReference2_0, bool bool_0)
        {
            try
            {
                IWorkspaceFactory o = new RasterWorkspaceFactory();
                IWorkspace workspace = o.OpenFromFile(string_0, 0);
                IRasterDataset dataset = (workspace as IRasterWorkspace2).CreateRasterDataset(string_1, string_2,
                    ipoint_0, int_0, int_1, double_0, double_1, int_2, rstPixelType_0, ispatialReference2_0, bool_0);
                Marshal.ReleaseComObject(o);
                o = null;
                Marshal.ReleaseComObject(workspace);
                workspace = null;
                GC.Collect();
                return dataset;
            }
            catch (Exception exception)
            {
                exception.ToString();
            }
            return null;
        }

        public static IRasterValue createRasterValue(IRasterDataset irasterDataset_0)
        {
            IRasterStorageDef def = new RasterStorageDef
            {
                CompressionType = esriRasterCompressionType.esriRasterCompressionLZ77,
                PyramidResampleType = rstResamplingTypes.RSP_BilinearInterpolation,
                PyramidLevel = -1
            };
            return new RasterValue {RasterDataset = irasterDataset_0, RasterStorageDef = def};
        }

        public static IRasterDataset CreateSDERasterDs(IRasterWorkspaceEx irasterWorkspaceEx_0, string string_0,
            int int_0, rstPixelType rstPixelType_0, ISpatialReference ispatialReference_0,
            IRasterStorageDef irasterStorageDef_0, IRasterDef irasterDef_0, string string_1)
        {
            IRasterDataset dataset = null;
            IGeometryDef geometryDef = null;
            if (irasterDef_0 == null)
            {
                irasterDef_0 = CreateRasterDef(false, ispatialReference_0);
            }
            if (irasterStorageDef_0 == null)
            {
                irasterStorageDef_0 = CreateRasterStorageDef();
            }
            geometryDef = CreateGeometryDef(irasterDef_0.SpatialReference);
            if (string_1.Length == 0)
            {
                string_1 = "DEFAULTS";
            }
            try
            {
                dataset = irasterWorkspaceEx_0.CreateRasterDataset(string_0, int_0, rstPixelType_0, irasterStorageDef_0,
                    string_1, irasterDef_0, geometryDef);
            }
            catch (COMException exception)
            {
                if (exception.ErrorCode == -2147155646)
                {
                    MessageBox.Show("对象名 [" + string_0 + "] 不符合命名要求，请输入合适的对象名!");
                    return dataset;
                }
                MessageBox.Show(exception.Message);
            }
            return dataset;
        }

        private static IField createShapeField(string string_0, ISpatialReference ispatialReference_0)
        {
            IFieldEdit edit = new Field() as IFieldEdit;
            if (string_0.Length == 0)
            {
                string_0 = "SHAPE";
            }
            edit.Name_2 = string_0;
            edit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            edit.GeometryDef_2 = createGeometryDef(ispatialReference_0);
            return edit;
        }

        private static IField createXMLField()
        {
            IFieldEdit field = new Field() as IFieldEdit;
            field.Name_2 = "METADATA";
            field.Type_2 = esriFieldType.esriFieldTypeBlob;
            return field;
        }

        public static IRawPixels GetRawPixels(IRasterDataset irasterDataset_0, int int_0)
        {
            IRasterBandCollection bands = irasterDataset_0 as IRasterBandCollection;
            return (bands.Item(int_0) as IRawPixels);
        }

        public static IRasterDataset OpenRasterDataset(string string_0, string string_1)
        {
            try
            {
                return OpenRasterWorkspace(string_0).OpenRasterDataset(string_1);
            }
            catch (Exception exception)
            {
                exception.ToString();
            }
            return null;
        }

        public static IRasterWorkspace OpenRasterWorkspace(string string_0)
        {
            try
            {
                IWorkspaceFactory factory = new RasterWorkspaceFactory();
                return (factory.OpenFromFile(string_0, 0) as IRasterWorkspace);
            }
            catch
            {
            }
            return null;
        }

        public static void QueryPixelBlock(ITinSurface itinSurface_0, double double_0, double double_1, double double_2,
            double double_3, esriRasterizationType esriRasterizationType_0, object object_0, object object_1)
        {
            int length = ((float[,]) object_1).GetLength(0);
            int num2 = ((float[,]) object_1).GetLength(1);
            IPoint pPoint = new Point();
            int num3 = 0;
            Label_0024:
            if (num3 >= num2)
            {
                return;
            }
            int num4 = 0;
            while (true)
            {
                if (num4 < length)
                {
                    pPoint.X = double_0 + (num4*double_2);
                    pPoint.Y = double_1 - (num3*double_3);
                    float slopeDegrees = 0f;
                    try
                    {
                        switch (esriRasterizationType_0)
                        {
                            case esriRasterizationType.esriDegreeSlopeAsRaster:
                                slopeDegrees = (float) itinSurface_0.GetSlopeDegrees(pPoint);
                                break;

                            case esriRasterizationType.esriPercentageSlopeAsRaster:
                                slopeDegrees = (float) itinSurface_0.GetSlopePercent(pPoint);
                                break;

                            case esriRasterizationType.esriDegreeAspectAsRaster:
                                slopeDegrees = (float) itinSurface_0.GetAspectDegrees(pPoint);
                                break;

                            case esriRasterizationType.esriElevationAsRaster:
                                slopeDegrees = (float) itinSurface_0.GetElevation(pPoint);
                                break;
                        }
                        if (!double.IsNaN((double) slopeDegrees))
                        {
                            ((float[,]) object_1)[num4, num3] = slopeDegrees;
                        }
                        else
                        {
                            ((float[,]) object_1)[num4, num3] = (float) object_0;
                        }
                    }
                    catch
                    {
                    }
                }
                else
                {
                    num3++;
                    goto Label_0024;
                }
                num4++;
            }
        }

        public static IRaster reprojectRasterDataset(string string_0, string string_1, int int_0)
        {
            IWorkspaceFactory factory = new RasterWorkspaceFactory();
            IRasterWorkspace workspace = factory.OpenFromFile(string_0, 0) as IRasterWorkspace;
            IRaster raster = workspace.OpenRasterDataset(string_1).CreateDefaultRaster();
            Console.WriteLine("Created default raster: " + string_1);
            SpatialReferenceEnvironment environment = new SpatialReferenceEnvironment();
            ISpatialReference reference = environment.CreateProjectedCoordinateSystem(int_0);
            IRasterProps props = raster as IRasterProps;
            Console.WriteLine("Orig Raster Coordinate System: ." + props.SpatialReference.Name.ToString());
            props.SpatialReference = reference;
            Console.WriteLine("New Raster Coordinate System: ." + props.SpatialReference.Name.ToString());
            props.Height /= 2;
            props.Width /= 2;
            return raster;
        }

        public static IRasterDataset TinToRaster(ITinAdvanced itinAdvanced_0,
            esriRasterizationType esriRasterizationType_0, string string_0, string string_1, rstPixelType rstPixelType_0,
            double double_0, IEnvelope ienvelope_0, bool bool_0)
        {
            IPoint lowerLeft = ienvelope_0.LowerLeft;
            lowerLeft.X -= double_0*0.5;
            lowerLeft.Y -= double_0*0.5;
            int num = (int) Math.Round((double) ((ienvelope_0.Width/double_0) + 1.0));
            int num2 = (int) Math.Round((double) ((ienvelope_0.Height/double_0) + 1.0));
            IGeoDataset dataset = itinAdvanced_0 as IGeoDataset;
            ISpatialReference2 spatialReference = dataset.SpatialReference as ISpatialReference2;
            IRasterDataset dataset2 = CreateRasterSurf(string_0, string_1, "GRID", lowerLeft, num, num2, double_0,
                double_0, rstPixelType_0, spatialReference, true);
            IRasterBandCollection bands = dataset2 as IRasterBandCollection;
            IRawPixels pixels = bands.Item(0) as IRawPixels;
            ITinSurface pSurface = itinAdvanced_0 as ITinSurface;
            pSurface.RasterInterpolationMethod = esriSurfaceInterpolationType.esriNaturalNeighborInterpolation;
            IRasterProps props = pixels as IRasterProps;
            object noDataValue = props.NoDataValue;
            IPnt tlc = new DblPnt();
            int num3 = 2048;
            int num4 = 2048;
            if (num < 2048)
            {
                num3 = num;
            }
            if (num2 < num4)
            {
                num4 = num2;
            }
            IPnt size = new DblPnt
            {
                X = num3,
                Y = num4
            };
            IPixelBlock pxls = pixels.CreatePixelBlock(size);
            object block = pxls.get_SafeArray(0);
            IPoint point2 = new Point();
            for (int i = 0; i < num2; i += num4)
            {
                for (int j = 0; j < num; j += num3)
                {
                    if ((num - j) < num3)
                    {
                        size.X = num - j;
                        pxls = pixels.CreatePixelBlock(size);
                        block = pxls.get_SafeArray(0);
                    }
                    point2.X = (lowerLeft.X + (j*double_0)) + (double_0*0.5);
                    point2.Y = (lowerLeft.Y + ((num2 - i)*double_0)) - (double_0*0.5);
                    IGeoDatabaseBridge2 bridge = new GeoDatabaseHelper() as IGeoDatabaseBridge2;
                    bridge.QueryPixelBlock(pSurface, point2.X, point2.Y, double_0, double_0, esriRasterizationType_0,
                        noDataValue, ref block);
                    tlc.X = j;
                    tlc.Y = i;
                    pxls.set_SafeArray(0, block);
                    pixels.Write(tlc, pxls);
                }
                bool flag = false;
                if (size.X != num3)
                {
                    size.X = num3;
                    flag = true;
                }
                if ((num2 - i) < num4)
                {
                    size.Y = num2 - i;
                }
                if (flag)
                {
                    block = pixels.CreatePixelBlock(size).get_SafeArray(0);
                }
            }
            return dataset2;
        }

        public static IRasterDataset TinToRaster(ITinAdvanced itinAdvanced_0,
            esriRasterizationType esriRasterizationType_0, string string_0, string string_1, string string_2,
            rstPixelType rstPixelType_0, double double_0, IEnvelope ienvelope_0, bool bool_0)
        {
            object obj3;
            IPoint lowerLeft = ienvelope_0.LowerLeft;
            lowerLeft.X -= double_0*0.5;
            lowerLeft.Y -= double_0*0.5;
            int num = ((int) Math.Round((double) (ienvelope_0.Width/double_0))) + 1;
            int num2 = ((int) Math.Round((double) (ienvelope_0.Height/double_0))) + 1;
            IGeoDataset dataset = itinAdvanced_0 as IGeoDataset;
            ISpatialReference2 spatialReference = dataset.SpatialReference as ISpatialReference2;
            IRasterDataset dataset2 = CreateRasterSurf(string_0, string_1, string_2, lowerLeft, num, num2, double_0,
                double_0, 1, rstPixelType_0, spatialReference, bool_0);
            IRawPixels rawPixels = GetRawPixels(dataset2, 0);
            object cache = rawPixels.AcquireCache();
            ITinSurface pSurface = itinAdvanced_0 as ITinSurface;
            IRasterProps o = rawPixels as IRasterProps;
            double zMin = itinAdvanced_0.Extent.ZMin;
            if (rstPixelType_0 == rstPixelType.PT_FLOAT)
            {
                obj3 = (float) (zMin - 1.0);
            }
            else
            {
                obj3 = (int) (zMin - 1.0);
            }
            o.NoDataValue = obj3;
            IPnt tlc = new DblPnt();
            int num4 = 2048;
            if (num < 2048)
            {
                num4 = num;
            }
            int num5 = 2048;
            if (num2 < 2048)
            {
                num5 = num2;
            }
            IPnt size = new DblPnt
            {
                X = num4,
                Y = num5
            };
            IPixelBlock3 block = rawPixels.CreatePixelBlock(size) as IPixelBlock3;
            ITrackCancel cancel = new CancelTracker
            {
                CancelOnClick = false,
                CancelOnKeyPress = true
            };
            int num6 = (int) (Math.Round((double) ((num/num4) + 0.49))*Math.Round((double) ((num2/num5) + 0.49)));
            if (num6 == 1)
            {
                itinAdvanced_0.TrackCancel = cancel;
            }
            IPoint point2 = new Point();
            object obj4 = block.get_PixelDataByRef(0);
            for (int i = 0; i < num2; i += num5)
            {
                for (int j = 0; j < num; j += num4)
                {
                    if ((num - j) < num4)
                    {
                        size.X = num - j;
                        block = rawPixels.CreatePixelBlock(size) as IPixelBlock3;
                        obj4 = block.get_PixelDataByRef(0);
                    }
                    point2.X = (lowerLeft.X + (j*double_0)) + (double_0*0.5);
                    point2.Y = (lowerLeft.Y + ((num2 - i)*double_0)) - (double_0*0.5);
                    IGeoDatabaseBridge2 bridge = new GeoDatabaseHelper() as IGeoDatabaseBridge2;
                    bridge.QueryPixelBlock(pSurface, point2.X, point2.Y, double_0, double_0, esriRasterizationType_0,
                        obj3, ref obj4);
                    tlc.X = j;
                    tlc.Y = i;
                    block.set_PixelData(0, obj4);
                    rawPixels.Write(tlc, block as IPixelBlock);
                }
                bool flag = false;
                if (size.X != num4)
                {
                    size.X = num4;
                    flag = true;
                }
                if ((num2 - i) < num5)
                {
                    size.Y = num2 - i;
                    flag = true;
                }
                if (flag)
                {
                    block = rawPixels.CreatePixelBlock(size) as IPixelBlock3;
                    obj4 = block.get_PixelDataByRef(0);
                }
            }
            rawPixels.ReturnCache(cache);
            Marshal.ReleaseComObject(cache);
            cache = null;
            Marshal.ReleaseComObject(rawPixels);
            rawPixels = null;
            Marshal.ReleaseComObject(block);
            block = null;
            Marshal.ReleaseComObject(o);
            o = null;
            obj4 = 0;
            GC.Collect();
            return dataset2;
        }

        public static void ToRasterCatalog(IList ilist_0, IFeatureClass ifeatureClass_0)
        {
            IFeatureCursor o = ifeatureClass_0.Insert(false);
            IRasterCatalog catalog = ifeatureClass_0 as IRasterCatalog;
            for (int i = 0; i < ilist_0.Count; i++)
            {
                IRasterDataset dataset = (ilist_0[i] as IName).Open() as IRasterDataset;
                IFeatureBuffer buffer = ifeatureClass_0.CreateFeatureBuffer();
                buffer.set_Value(catalog.RasterFieldIndex, createRasterValue(dataset));
                o.InsertFeature(buffer);
            }
            o.Flush();
            ComReleaser.ReleaseCOMObject(o);
        }

        public static void UsingRasterClassifyRendered(IRasterLayer irasterLayer_0, int int_0, string string_0)
        {
            try
            {
                bool flag;
                IRaster raster = irasterLayer_0.Raster;
                IRasterBand band = (raster as IRasterBandCollection).Item(0);
                band.HasTable(out flag);
                if (flag)
                {
                    int num2;
                    bool flag2;
                    IRasterClassifyColorRampRenderer renderer = new RasterClassifyColorRampRenderer();
                    IRasterRenderer renderer2 = renderer as IRasterRenderer;
                    renderer2.Raster = raster;
                    ITable attributeTable = band.AttributeTable;
                    ITableHistogram tableHistogram = new BasicTableHistogram() as ITableHistogram;
                    tableHistogram.Field = string_0;
                    tableHistogram.Table = attributeTable;
                    ITableHistogram histogram = tableHistogram as ITableHistogram;
                    double maximum = (histogram as IStatisticsResults).Maximum;
                    IClassify classify = new EqualInterval() as IClassify;
                    (classify as IClassifyMinMax).Minimum = (histogram as IStatisticsResults).Minimum;
                    (classify as IClassifyMinMax).Maximum = (histogram as IStatisticsResults).Maximum;
                    int_0--;
                    classify.Classify(ref int_0);
                    object classBreaks = classify.ClassBreaks;
                    UID classID = classify.ClassID;
                    IRasterClassifyUIProperties properties = renderer as IRasterClassifyUIProperties;
                    properties.ClassificationMethod = classID;
                    renderer.ClassCount = int_0;
                    renderer.ClassField = string_0;
                    for (num2 = 0; num2 < int_0; num2++)
                    {
                        renderer.set_Break(num2, ((double[]) classBreaks)[num2]);
                    }
                    IColorRamp ramp = ColorManage.CreateColorRamp();
                    ramp.Size = int_0;
                    ramp.CreateRamp(out flag2);
                    IFillSymbol symbol = new SimpleFillSymbol();
                    for (num2 = 0; num2 < renderer.ClassCount; num2++)
                    {
                        double num4;
                        symbol.Color = ramp.get_Color(num2);
                        renderer.set_Symbol(num2, symbol as ISymbol);
                        double num3 = ((double[]) classBreaks)[num2];
                        if (num2 == (renderer.ClassCount - 1))
                        {
                            num4 = maximum;
                        }
                        else
                        {
                            num4 = ((double[]) classBreaks)[num2 + 1];
                        }
                        renderer.set_Label(num2, num3.ToString() + "-" + num4.ToString());
                    }
                    renderer2.Update();
                    irasterLayer_0.Renderer = renderer as IRasterRenderer;
                }
                else
                {
                    UsingRasterClassifyRendered1(irasterLayer_0, int_0, string_0);
                }
            }
            catch (Exception exception)
            {
                CErrorLog.writeErrorLog(null, exception, "栅格渲染");
            }
        }

        internal static void UsingRasterClassifyRendered1(IRasterLayer irasterLayer_0, int int_0, string string_0)
        {
            bool flag;
            IRaster raster = irasterLayer_0.Raster;
            IRasterClassifyColorRampRenderer o = new RasterClassifyColorRampRenderer();
            IRasterRenderer renderer2 = o as IRasterRenderer;
            renderer2.Raster = raster;
            o.ClassField = string_0;
            o.ClassCount = int_0;
            IClassify classify = new EqualInterval() as IClassify;
            UID classID = classify.ClassID;
            IRasterClassifyUIProperties properties = o as IRasterClassifyUIProperties;
            properties.ClassificationMethod = classID;
            renderer2.Update();
            IColorRamp ramp = ColorManage.CreateColorRamp();
            ramp.Size = int_0;
            ramp.CreateRamp(out flag);
            IFillSymbol symbol = new SimpleFillSymbol();
            for (int i = 0; i < o.ClassCount; i++)
            {
                symbol.Color = ramp.get_Color(i);
                o.set_Symbol(i, symbol as ISymbol);
            }
            renderer2.Update();
            irasterLayer_0.Renderer = o as IRasterRenderer;
            Marshal.ReleaseComObject(o);
            o = null;
        }

        public static void UsingRasterStretchColorRampRenderer(IRasterLayer irasterLayer_0)
        {
            bool flag;
            IRaster raster = irasterLayer_0.Raster;
            IRasterStretchColorRampRenderer renderer = new RasterStretchColorRampRenderer();
            IRasterRenderer renderer2 = renderer as IRasterRenderer;
            renderer2.Raster = raster;
            renderer2.Update();
            IColor color = ColorManage.CreatColor(255, 0, 0);
            IColor color2 = ColorManage.CreatColor(0, 255, 0);
            IAlgorithmicColorRamp ramp = new AlgorithmicColorRamp
            {
                Size = 255,
                FromColor = color,
                ToColor = color2
            };
            ramp.CreateRamp(out flag);
            renderer.BandIndex = 0;
            renderer.ColorRamp = ramp;
            renderer2.Update();
            irasterLayer_0.Renderer = renderer as IRasterRenderer;
        }

        public static bool UsingRGBRenderer(IRasterLayer irasterLayer_0)
        {
            IRaster raster = irasterLayer_0.Raster;
            IRasterBandCollection bands = raster as IRasterBandCollection;
            if (bands.Count < 3)
            {
                return false;
            }
            IRasterRGBRenderer renderer = new RasterRGBRenderer();
            IRasterRenderer renderer2 = renderer as IRasterRenderer;
            renderer2.Raster = raster;
            renderer2.Update();
            renderer.RedBandIndex = 2;
            renderer.GreenBandIndex = 1;
            renderer.BlueBandIndex = 0;
            renderer2.Update();
            irasterLayer_0.Renderer = renderer as IRasterRenderer;
            return true;
        }
    }
}