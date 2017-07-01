using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Raster
{
    public class RasterUtil
    {
        public RasterUtil()
        {
        }

        public IRasterValue createRasterValue(IRasterDataset irasterDataset_0)
        {
            IRasterStorageDef rasterStorageDefClass = new RasterStorageDef()
            {
                CompressionType = esriRasterCompressionType.esriRasterCompressionLZ77,
                PyramidResampleType = rstResamplingTypes.RSP_BilinearInterpolation,
                PyramidLevel = -1
            };
            return new RasterValue()
            {
                RasterDataset = irasterDataset_0,
                RasterStorageDef = rasterStorageDefClass
            };
        }

        public IRasterDataset createSDERasterDs(IRasterWorkspaceEx irasterWorkspaceEx_0, string string_0, int int_0,
            rstPixelType rstPixelType_0, ISpatialReference ispatialReference_0, IRasterStorageDef irasterStorageDef_0,
            IRasterDef irasterDef_0, string string_1)
        {
            IRasterDataset rasterDataset = null;
            if (irasterDef_0 == null)
            {
                irasterDef_0 = this.method_0(false, ispatialReference_0);
            }
            if (irasterStorageDef_0 == null)
            {
                irasterStorageDef_0 = this.method_1();
            }
            IGeometryDef geometryDef = this.method_2(ispatialReference_0);
            if (string_1.Length == 0)
            {
                string_1 = "DEFAULTS";
            }
            rasterDataset = irasterWorkspaceEx_0.CreateRasterDataset(string_0, int_0, rstPixelType_0,
                irasterStorageDef_0, string_1, irasterDef_0, geometryDef);
            return rasterDataset;
        }

        private IRasterDef method_0(bool bool_0, ISpatialReference ispatialReference_0)
        {
            IRasterDef rasterDefClass = new RasterDef()
            {
                Description = "Raster Dataset"
            };
            if (ispatialReference_0 == null)
            {
                ispatialReference_0 = new UnknownCoordinateSystem() as ISpatialReference;
            }
            rasterDefClass.SpatialReference = ispatialReference_0;
            return rasterDefClass;
        }

        private IRasterStorageDef method_1()
        {
            IRasterStorageDef rasterStorageDefClass = new RasterStorageDef()
            {
                CompressionType = esriRasterCompressionType.esriRasterCompressionLZ77,
                PyramidLevel = 2,
                PyramidResampleType = rstResamplingTypes.RSP_BilinearInterpolation,
                TileHeight = 128,
                TileWidth = 128
            };
            return rasterStorageDefClass;
        }

        private IGeometryDef method_2(ISpatialReference ispatialReference_0)
        {
            IGeometryDefEdit @class = new GeometryDef() as IGeometryDefEdit;
            @class.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
            @class.AvgNumPoints_2 = 4;
            @class.GridCount_2 = 1;
            IGeometryDefEdit geometryDefClass = @class as IGeometryDefEdit;
            geometryDefClass.GridSize_2[0] = 1000;
            if (ispatialReference_0 == null)
            {
                ispatialReference_0 = new UnknownCoordinateSystem() as ISpatialReference;
            }
            geometryDefClass.SpatialReference_2 = ispatialReference_0;
            return geometryDefClass;
        }

        public static void ReplacePixel(IRasterDataset2 irasterDataset2_0)
        {
            IRaster2 raster2 = irasterDataset2_0.CreateFullRaster() as IRaster2;
            IPnt pntClass = new Pnt()
            {
                X = 128,
                Y = 128
            };
            raster2.CreateCursorEx(pntClass);
            IRasterEdit rasterEdit = raster2 as IRasterEdit;
            if (rasterEdit.CanEdit())
            {
                long count = (long) (irasterDataset2_0 as IRasterBandCollection).Count;
                IRasterProps rasterProp = raster2 as IRasterProps;
                object noDataValue = rasterProp.NoDataValue;
                rasterProp.NoDataValue = 255;
                (rasterProp as ISaveAs).SaveAs("j:\\image1.tif", null, "TIFF");
                Marshal.ReleaseComObject(rasterEdit);
            }
        }

        public void ToRasterCatalog(IList ilist_0, IFeatureClass ifeatureClass_0)
        {
            IFeatureCursor featureCursor = ifeatureClass_0.Insert(false);
            IRasterCatalog ifeatureClass0 = ifeatureClass_0 as IRasterCatalog;
            for (int i = 0; i < ilist_0.Count; i++)
            {
                IRasterDataset rasterDataset = (ilist_0[i] as IName).Open() as IRasterDataset;
                IFeatureBuffer featureBuffer = ifeatureClass_0.CreateFeatureBuffer();
                featureBuffer.Value[ifeatureClass0.RasterFieldIndex] = this.createRasterValue(rasterDataset);
                featureCursor.InsertFeature(featureBuffer);
            }
        }

        public void WriteToSDEFromPixelArray(IRasterWorkspaceEx irasterWorkspaceEx_0, string string_0)
        {
            IRasterDataset rasterDataset = irasterWorkspaceEx_0.CreateRasterDataset(string_0, 3, rstPixelType.PT_SHORT,
                new RasterStorageDef(), "", new RasterDef(), null);
            IRaster raster = rasterDataset.CreateDefaultRaster();
            IRasterProps rasterProp = raster as IRasterProps;
            int num = 1000;
            int num1 = 1000;
            IEnvelope envelopeClass = new Envelope()
            {
                XMin = 100,
                XMax = 500,
                YMin = 100,
                YMax = 500
            } as IEnvelope;
            rasterProp.Extent = envelopeClass;
            rasterProp.Width = 1000;
            rasterProp.Height = 1000;
            IPnt pntClass = new Pnt();
            pntClass.SetCoords((double) 1000, (double) 1000);
            IPixelBlock3 pixelBlock3 = raster.CreatePixelBlock(pntClass) as IPixelBlock3;
            pntClass.SetCoords(0, 0);
            for (int i = 0; i < 3; i++)
            {
                object pixelData = pixelBlock3.PixelData[i];
                for (int j = 0; j < num; j++)
                {
                    int num2 = 0;
                    while (num2 < num1)
                    {
                        num2++;
                    }
                }
                pixelBlock3.PixelData[i] = pixelData;
            }
            (raster as IRasterEdit).Write(pntClass, pixelBlock3 as IPixelBlock);
        }
    }
}