namespace JLK.Catalog
{
    using JLK.Utility;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

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
                    "JLK.Catalog.Resources.bmpRoot.bmp", "JLK.Catalog.Resources.bmpDiskConnect.bmp", "JLK.Catalog.Resources.bmpDBLink.bmp", "JLK.Catalog.Resources.GISServerFolder.bmp", "JLK.Catalog.Resources.AddWMSServer.bmp", "JLK.Catalog.Resources.DiskConnectionError.bmp", "JLK.Catalog.Resources.bmpFolder1.bmp", "JLK.Catalog.Resources.bmpFolder2.bmp", "JLK.Catalog.Resources.bmpAddLink.bmp", "JLK.Catalog.Resources.bmpGDBLink1.bmp", "JLK.Catalog.Resources.bmpGDBLink2.bmp", "JLK.Catalog.Resources.AddArcGISServer.bmp", "JLK.Catalog.Resources.AddArcIMSServer.bmp", "JLK.Catalog.Resources.ArcGISServerUnConn.bmp", "JLK.Catalog.Resources.ArcGISServerConn.bmp", "JLK.Catalog.Resources.bmpPersonGDB.bmp", 
                    "JLK.Catalog.Resources.bmpProjection.bmp", "JLK.Catalog.Resources.bmpRasterDataset.bmp", "JLK.Catalog.Resources.bmpDataset.bmp", "JLK.Catalog.Resources.bmpTable.bmp", "JLK.Catalog.Resources.bmpPointFeature.bmp", "JLK.Catalog.Resources.bmpLineFeature.bmp", "JLK.Catalog.Resources.bmpFillFeatureClass.bmp", "JLK.Catalog.Resources.ShapePoint.bmp", "JLK.Catalog.Resources.ShapeLine.bmp", "JLK.Catalog.Resources.ShapeFill.bmp", "JLK.Catalog.Resources.bmpText.bmp", "JLK.Catalog.Resources.bmpMXD.bmp", "JLK.Catalog.Resources.bmpPMF.bmp", "JLK.Catalog.Resources.bmpTopology.bmp", "JLK.Catalog.Resources.bmpRelation.bmp", "JLK.Catalog.Resources.ErrorShapeFile.bmp", 
                    "JLK.Catalog.Resources.bmpPointLayer.bmp", "JLK.Catalog.Resources.bmpLineLayer.bmp", "JLK.Catalog.Resources.bmpFillLayer.bmp", "JLK.Catalog.Resources.bmpGroupLayer.bmp", "JLK.Catalog.Resources.bmpRasterLayer.bmp", "JLK.Catalog.Resources.bmpErrorLayer.bmp", "JLK.Catalog.Resources.bmpGeometryNet.bmp", "JLK.Catalog.Resources.bmpAnno.bmp", "JLK.Catalog.Resources.bmpCADLayers.bmp", "JLK.Catalog.Resources.bmpCADPoint.bmp", "JLK.Catalog.Resources.bmpCADPolyline.bmp", "JLK.Catalog.Resources.bmpCADPolygon.bmp", "JLK.Catalog.Resources.bmpCADAnno.bmp", "JLK.Catalog.Resources.bmpCADMultiPatch.bmp", "JLK.Catalog.Resources.bmpRasterCatalog.bmp", "JLK.Catalog.Resources.bmpTin.bmp", 
                    "JLK.Catalog.Resources.Word.bmp", "JLK.Catalog.Resources.Excel.bmp", "JLK.Catalog.Resources.ArcIMSServerUnConn.bmp", "JLK.Catalog.Resources.ArcIMSServerConn.bmp", "JLK.Catalog.Resources.AddServerObject.bmp", "JLK.Catalog.Resources.MapServerStarted.bmp", "JLK.Catalog.Resources.MapServerStoped.bmp", "JLK.Catalog.Resources.MapServerPaused.bmp", "JLK.Catalog.Resources.bmpCadDrawing.bmp", "JLK.Catalog.Resources.bmpCoverage.bmp", "JLK.Catalog.Resources.bmpAnnotationCoverage.bmp", "JLK.Catalog.Resources.bmpPointCoverage.bmp", "JLK.Catalog.Resources.bmpLineCoverage.bmp", "JLK.Catalog.Resources.bmpPolygonCoverage.bmp", "JLK.Catalog.Resources.bmpRegionCoverage.bmp", "JLK.Catalog.Resources.bmpCoveragePointFeatClass.bmp", 
                    "JLK.Catalog.Resources.bmpCoverageArcFeatClass.bmp", "JLK.Catalog.Resources.bmpCoveragePolygonFeatClass.bmp", "JLK.Catalog.Resources.bmpCoverageNodeFeatClass.bmp", "JLK.Catalog.Resources.bmpCoverageTicFeatClass.bmp", "JLK.Catalog.Resources.bmpCoverageAnnoFeatclass.bmp", "JLK.Catalog.Resources.bmpCoverageRouteFeatClass.bmp", "JLK.Catalog.Resources.bmpCoverageRegionFeatClass.bmp", "JLK.Catalog.Resources.bmpCoverageLabelFeatClass.bmp", "JLK.Catalog.Resources.bmpCoverageLabelFeatClass.bmp", "JLK.Catalog.Resources.NetworkDataset.bmp", "JLK.Catalog.Resources.GDBFolder.bmp", "JLK.Catalog.Resources.AddGDBServer.bmp", "JLK.Catalog.Resources.GDBServer.bmp", "JLK.Catalog.Resources.GDBsUnConnect.bmp", "JLK.Catalog.Resources.DJStructbmp.bmp", "JLK.Catalog.Resources.GeometryAny.bmp", 
                    "JLK.Catalog.Resources.GeometryServerStart.bmp", "JLK.Catalog.Resources.GeometryServerStop.bmp", "JLK.Catalog.Resources.GeometryServerPause.bmp", "JLK.Catalog.Resources.GPServerStart.bmp", "JLK.Catalog.Resources.GPServerStop.bmp", "JLK.Catalog.Resources.GPServerPause.bmp", "JLK.Catalog.Resources.SearchServerStart.bmp", "JLK.Catalog.Resources.SearchServerStop.bmp", "JLK.Catalog.Resources.SearchServerPause.bmp", "JLK.Catalog.Resources.FeatureServices.bmp"
                 };
                foreach (string str in strArray2)
                {
                    Bitmap item = new Bitmap(base.GetType().Assembly.GetManifestResourceStream(str));
                    list_0.Add(item);
                }
            }
            catch (Exception exception)
            {
                CErrorLog.writeErrorLog(null, exception, "GxCatalog");
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

