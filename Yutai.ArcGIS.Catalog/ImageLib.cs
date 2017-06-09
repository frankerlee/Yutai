using System;
using System.Collections.Generic;
using System.Drawing;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog
{
    public class ImageLib
    {
        private static List<Bitmap> m_pList;

        static ImageLib()
        {
            old_acctor_mc();
        }

        public static Bitmap GetSmallImage(int int_0)
        {
            return m_pList[int_0];
        }

        public static void Init()
        {
            if (m_pList.Count == 0)
            {
                new ImageLib().method_0(m_pList);
            }
        }

        private void method_0(List<Bitmap> list_0)
        {
            try
            {
                string[] strArray2 = new string[] { 
                    "Yutai.ArcGIS.Catalog.Resources.bmpRoot.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpDiskConnect.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpDBLink.bmp", "Yutai.ArcGIS.Catalog.Resources.GISServerFolder.bmp", "Yutai.ArcGIS.Catalog.Resources.AddWMSServer.bmp", "Yutai.ArcGIS.Catalog.Resources.DiskConnectionError.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpFolder1.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpFolder2.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpAddLink.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpGDBLink1.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpGDBLink2.bmp", "Yutai.ArcGIS.Catalog.Resources.AddArcGISServer.bmp", "Yutai.ArcGIS.Catalog.Resources.AddArcIMSServer.bmp", "Yutai.ArcGIS.Catalog.Resources.ArcGISServerUnConn.bmp", "Yutai.ArcGIS.Catalog.Resources.ArcGISServerConn.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpPersonGDB.bmp", 
                    "Yutai.ArcGIS.Catalog.Resources.bmpProjection.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpRasterDataset.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpDataset.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpTable.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpPointFeature.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpLineFeature.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpFillFeatureClass.bmp", "Yutai.ArcGIS.Catalog.Resources.ShapePoint.bmp", "Yutai.ArcGIS.Catalog.Resources.ShapeLine.bmp", "Yutai.ArcGIS.Catalog.Resources.ShapeFill.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpText.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpMXD.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpPMF.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpTopology.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpRelation.bmp", "Yutai.ArcGIS.Catalog.Resources.ErrorShapeFile.bmp", 
                    "Yutai.ArcGIS.Catalog.Resources.bmpPointLayer.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpLineLayer.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpFillLayer.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpGroupLayer.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpRasterLayer.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpErrorLayer.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpGeometryNet.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpAnno.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpCADLayers.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpCADPoint.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpCADPolyline.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpCADPolygon.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpCADAnno.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpCADMultiPatch.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpRasterCatalog.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpTin.bmp", 
                    "Yutai.ArcGIS.Catalog.Resources.Word.bmp", "Yutai.ArcGIS.Catalog.Resources.Excel.bmp", "Yutai.ArcGIS.Catalog.Resources.ArcIMSServerUnConn.bmp", "Yutai.ArcGIS.Catalog.Resources.ArcIMSServerConn.bmp", "Yutai.ArcGIS.Catalog.Resources.AddServerObject.bmp", "Yutai.ArcGIS.Catalog.Resources.MapServerStarted.bmp", "Yutai.ArcGIS.Catalog.Resources.MapServerStoped.bmp", "Yutai.ArcGIS.Catalog.Resources.MapServerPaused.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpCadDrawing.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpCoverage.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpAnnotationCoverage.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpPointCoverage.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpLineCoverage.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpPolygonCoverage.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpRegionCoverage.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpCoveragePointFeatClass.bmp", 
                    "Yutai.ArcGIS.Catalog.Resources.bmpCoverageArcFeatClass.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpCoveragePolygonFeatClass.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpCoverageNodeFeatClass.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpCoverageTicFeatClass.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpCoverageAnnoFeatclass.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpCoverageRouteFeatClass.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpCoverageRegionFeatClass.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpCoverageLabelFeatClass.bmp", "Yutai.ArcGIS.Catalog.Resources.bmpCoverageLabelFeatClass.bmp", "Yutai.ArcGIS.Catalog.Resources.NetworkDataset.bmp", "Yutai.ArcGIS.Catalog.Resources.GDBFolder.bmp", "Yutai.ArcGIS.Catalog.Resources.AddGDBServer.bmp", "Yutai.ArcGIS.Catalog.Resources.GDBServer.bmp", "Yutai.ArcGIS.Catalog.Resources.GDBsUnConnect.bmp", "Yutai.ArcGIS.Catalog.Resources.DJStructbmp.bmp", "Yutai.ArcGIS.Catalog.Resources.GeometryAny.bmp", 
                    "Yutai.ArcGIS.Catalog.Resources.GeometryServerStart.bmp", "Yutai.ArcGIS.Catalog.Resources.GeometryServerStop.bmp", "Yutai.ArcGIS.Catalog.Resources.GeometryServerPause.bmp", "Yutai.ArcGIS.Catalog.Resources.GPServerStart.bmp", "Yutai.ArcGIS.Catalog.Resources.GPServerStop.bmp", "Yutai.ArcGIS.Catalog.Resources.GPServerPause.bmp", "Yutai.ArcGIS.Catalog.Resources.SearchServerStart.bmp", "Yutai.ArcGIS.Catalog.Resources.SearchServerStop.bmp", "Yutai.ArcGIS.Catalog.Resources.SearchServerPause.bmp", "Yutai.ArcGIS.Catalog.Resources.FeatureServices.bmp"
                 };
                foreach (string str in strArray2)
                {
                    Bitmap item = new Bitmap(base.GetType().Assembly.GetManifestResourceStream(str));
                    list_0.Add(item);
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("",exception, "GxCatalog");
            }
        }

        private static void old_acctor_mc()
        {
            m_pList = new List<Bitmap>();
            if (m_pList.Count == 0)
            {
                new ImageLib().method_0(m_pList);
            }
        }

        public static void Release()
        {
            for (int i = 0; i < m_pList.Count; i++)
            {
                m_pList[i].Dispose();
            }
            m_pList.Clear();
        }
    }
}

