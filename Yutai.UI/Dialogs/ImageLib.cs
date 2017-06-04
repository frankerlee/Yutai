using System;
using System.Collections.Generic;
using System.Drawing;

namespace Yutai.UI.Dialogs
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
                new ImageLib().InitBmp(m_pList);
            }
        }

        private void InitBmp(List<Bitmap> list_0)
        {
            try
            {
                string[] strArray2 = new string[] {
                    "Yutai.UI.Resources.bmpRoot.bmp", "Yutai.UI.Resources.bmpDiskConnect.bmp", "Yutai.UI.Resources.bmpDBLink.bmp", "Yutai.UI.Resources.GISServerFolder.bmp", "Yutai.UI.Resources.AddWMSServer.bmp", "Yutai.UI.Resources.DiskConnectionError.bmp", "Yutai.UI.Resources.bmpFolder1.bmp", "Yutai.UI.Resources.bmpFolder2.bmp", "Yutai.UI.Resources.bmpAddLink.bmp", "Yutai.UI.Resources.bmpGDBLink1.bmp", "Yutai.UI.Resources.bmpGDBLink2.bmp", "Yutai.UI.Resources.AddArcGISServer.bmp", "Yutai.UI.Resources.AddArcIMSServer.bmp", "Yutai.UI.Resources.ArcGISServerUnConn.bmp", "Yutai.UI.Resources.ArcGISServerConn.bmp", "Yutai.UI.Resources.bmpPersonGDB.bmp",
                    "Yutai.UI.Resources.bmpProjection.bmp", "Yutai.UI.Resources.bmpRasterDataset.bmp", "Yutai.UI.Resources.bmpDataset.bmp", "Yutai.UI.Resources.bmpTable.bmp", "Yutai.UI.Resources.bmpPointFeature.bmp", "Yutai.UI.Resources.bmpLineFeature.bmp", "Yutai.UI.Resources.bmpFillFeatureClass.bmp", "Yutai.UI.Resources.ShapePoint.bmp", "Yutai.UI.Resources.ShapeLine.bmp", "Yutai.UI.Resources.ShapeFill.bmp", "Yutai.UI.Resources.bmpText.bmp", "Yutai.UI.Resources.bmpMXD.bmp", "Yutai.UI.Resources.bmpPMF.bmp", "Yutai.UI.Resources.bmpTopology.bmp", "Yutai.UI.Resources.bmpRelation.bmp", "Yutai.UI.Resources.ErrorShapeFile.bmp",
                    "Yutai.UI.Resources.bmpPointLayer.bmp", "Yutai.UI.Resources.bmpLineLayer.bmp", "Yutai.UI.Resources.bmpFillLayer.bmp", "Yutai.UI.Resources.bmpGroupLayer.bmp", "Yutai.UI.Resources.bmpRasterLayer.bmp", "Yutai.UI.Resources.bmpErrorLayer.bmp", "Yutai.UI.Resources.bmpGeometryNet.bmp", "Yutai.UI.Resources.bmpAnno.bmp", "Yutai.UI.Resources.bmpCADLayers.bmp", "Yutai.UI.Resources.bmpCADPoint.bmp", "Yutai.UI.Resources.bmpCADPolyline.bmp", "Yutai.UI.Resources.bmpCADPolygon.bmp", "Yutai.UI.Resources.bmpCADAnno.bmp", "Yutai.UI.Resources.bmpCADMultiPatch.bmp", "Yutai.UI.Resources.bmpRasterCatalog.bmp", "Yutai.UI.Resources.bmpTin.bmp",
                    "Yutai.UI.Resources.Word.bmp", "Yutai.UI.Resources.Excel.bmp", "Yutai.UI.Resources.ArcIMSServerUnConn.bmp", "Yutai.UI.Resources.ArcIMSServerConn.bmp", "Yutai.UI.Resources.AddServerObject.bmp", "Yutai.UI.Resources.MapServerStarted.bmp", "Yutai.UI.Resources.MapServerStoped.bmp", "Yutai.UI.Resources.MapServerPaused.bmp", "Yutai.UI.Resources.bmpCadDrawing.bmp", "Yutai.UI.Resources.bmpCoverage.bmp", "Yutai.UI.Resources.bmpAnnotationCoverage.bmp", "Yutai.UI.Resources.bmpPointCoverage.bmp", "Yutai.UI.Resources.bmpLineCoverage.bmp", "Yutai.UI.Resources.bmpPolygonCoverage.bmp", "Yutai.UI.Resources.bmpRegionCoverage.bmp", "Yutai.UI.Resources.bmpCoveragePointFeatClass.bmp",
                    "Yutai.UI.Resources.bmpCoverageArcFeatClass.bmp", "Yutai.UI.Resources.bmpCoveragePolygonFeatClass.bmp", "Yutai.UI.Resources.bmpCoverageNodeFeatClass.bmp", "Yutai.UI.Resources.bmpCoverageTicFeatClass.bmp", "Yutai.UI.Resources.bmpCoverageAnnoFeatclass.bmp", "Yutai.UI.Resources.bmpCoverageRouteFeatClass.bmp", "Yutai.UI.Resources.bmpCoverageRegionFeatClass.bmp", "Yutai.UI.Resources.bmpCoverageLabelFeatClass.bmp", "Yutai.UI.Resources.bmpCoverageLabelFeatClass.bmp", "Yutai.UI.Resources.NetworkDataset.bmp", "Yutai.UI.Resources.GDBFolder.bmp", "Yutai.UI.Resources.AddGDBServer.bmp", "Yutai.UI.Resources.GDBServer.bmp", "Yutai.UI.Resources.GDBsUnConnect.bmp", "Yutai.UI.Resources.DJStructbmp.bmp", "Yutai.UI.Resources.GeometryAny.bmp",
                    "Yutai.UI.Resources.GeometryServerStart.bmp", "Yutai.UI.Resources.GeometryServerStop.bmp", "Yutai.UI.Resources.GeometryServerPause.bmp", "Yutai.UI.Resources.GPServerStart.bmp", "Yutai.UI.Resources.GPServerStop.bmp", "Yutai.UI.Resources.GPServerPause.bmp", "Yutai.UI.Resources.SearchServerStart.bmp", "Yutai.UI.Resources.SearchServerStop.bmp", "Yutai.UI.Resources.SearchServerPause.bmp", "Yutai.UI.Resources.FeatureServices.bmp"
                };
                foreach (string str in strArray2)
                {
                    Bitmap item = new Bitmap(base.GetType().Assembly.GetManifestResourceStream(str));
                    list_0.Add(item);
                }
            }
            catch (Exception exception)
            {
                //Logger.Current.Error("",exception, "GxCatalog");
            }
        }

        private static void old_acctor_mc()
        {
            m_pList = new List<Bitmap>();
            if (m_pList.Count == 0)
            {
                new ImageLib().InitBmp(m_pList);
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