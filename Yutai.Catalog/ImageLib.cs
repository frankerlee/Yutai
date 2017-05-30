using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Mime;
using System.Reflection;
using Yutai.Shared;

namespace Yutai.Catalog
{
	public class ImageLib
	{
		private static List<Bitmap> m_pList;
	    private static ImageLib instance;

		static ImageLib Instance()
		{
		    if (instance == null)
		    {
		        instance=new ImageLib();
                //m_pList = new List<Bitmap>();
                //if (ImageLib.m_pList.Count == 0)
                //{
                //    instance.FillImage(ImageLib.m_pList);
                //}
            }
		    return instance;
		}

		public ImageLib()
		{
		}

		public static Bitmap GetSmallImage(int int_0)
		{
			return ImageLib.m_pList[int_0];
		}

		public static void Init()
		{
		    if (ImageLib.m_pList == null)
		    {
		        ImageLib.m_pList=new List<Bitmap>();
		    }
			if (ImageLib.m_pList.Count == 0)
			{
				Instance().FillImage(ImageLib.m_pList);
			}
		}

		private void FillImage(List<Bitmap> list_0)
		{
			try
			{
				string[] strArrays = new string[] { "Yutai.Catalog.Resources.bmpRoot.bmp", "Yutai.Catalog.Resources.bmpDiskConnect.bmp", "Yutai.Catalog.Resources.bmpDBLink.bmp", "Yutai.Catalog.Resources.GISServerFolder.bmp", "Yutai.Catalog.Resources.AddWMSServer.bmp", "Yutai.Catalog.Resources.DiskConnectionError.bmp", "Yutai.Catalog.Resources.bmpFolder1.bmp", "Yutai.Catalog.Resources.bmpFolder2.bmp", "Yutai.Catalog.Resources.bmpAddLink.bmp", "Yutai.Catalog.Resources.bmpGDBLink1.bmp", "Yutai.Catalog.Resources.bmpGDBLink2.bmp", "Yutai.Catalog.Resources.AddArcGISServer.bmp", "Yutai.Catalog.Resources.AddArcIMSServer.bmp", "Yutai.Catalog.Resources.ArcGISServerUnConn.bmp", "Yutai.Catalog.Resources.ArcGISServerConn.bmp", "Yutai.Catalog.Resources.bmpPersonGDB.bmp", "Yutai.Catalog.Resources.bmpProjection.bmp", "Yutai.Catalog.Resources.bmpRasterDataset.bmp", "Yutai.Catalog.Resources.bmpDataset.bmp", "Yutai.Catalog.Resources.bmpTable.bmp", "Yutai.Catalog.Resources.bmpPointFeature.bmp", "Yutai.Catalog.Resources.bmpLineFeature.bmp", "Yutai.Catalog.Resources.bmpFillFeatureClass.bmp", "Yutai.Catalog.Resources.ShapePoint.bmp", "Yutai.Catalog.Resources.ShapeLine.bmp", "Yutai.Catalog.Resources.ShapeFill.bmp", "Yutai.Catalog.Resources.bmpText.bmp", "Yutai.Catalog.Resources.bmpMXD.bmp", "Yutai.Catalog.Resources.bmpPMF.bmp", "Yutai.Catalog.Resources.bmpTopology.bmp", "Yutai.Catalog.Resources.bmpRelation.bmp", "Yutai.Catalog.Resources.ErrorShapeFile.bmp", "Yutai.Catalog.Resources.bmpPointLayer.bmp", "Yutai.Catalog.Resources.bmpLineLayer.bmp", "Yutai.Catalog.Resources.bmpFillLayer.bmp", "Yutai.Catalog.Resources.bmpGroupLayer.bmp", "Yutai.Catalog.Resources.bmpRasterLayer.bmp", "Yutai.Catalog.Resources.bmpErrorLayer.bmp", "Yutai.Catalog.Resources.bmpGeometryNet.bmp", "Yutai.Catalog.Resources.bmpAnno.bmp", "Yutai.Catalog.Resources.bmpCADLayers.bmp", "Yutai.Catalog.Resources.bmpCADPoint.bmp", "Yutai.Catalog.Resources.bmpCADPolyline.bmp", "Yutai.Catalog.Resources.bmpCADPolygon.bmp", "Yutai.Catalog.Resources.bmpCADAnno.bmp", "Yutai.Catalog.Resources.bmpCADMultiPatch.bmp", "Yutai.Catalog.Resources.bmpRasterCatalog.bmp", "Yutai.Catalog.Resources.bmpTin.bmp", "Yutai.Catalog.Resources.Word.bmp", "Yutai.Catalog.Resources.Excel.bmp", "Yutai.Catalog.Resources.ArcIMSServerUnConn.bmp", "Yutai.Catalog.Resources.ArcIMSServerConn.bmp", "Yutai.Catalog.Resources.AddServerObject.bmp", "Yutai.Catalog.Resources.MapServerStarted.bmp", "Yutai.Catalog.Resources.MapServerStoped.bmp", "Yutai.Catalog.Resources.MapServerPaused.bmp", "Yutai.Catalog.Resources.bmpCadDrawing.bmp", "Yutai.Catalog.Resources.bmpCoverage.bmp", "Yutai.Catalog.Resources.bmpAnnotationCoverage.bmp", "Yutai.Catalog.Resources.bmpPointCoverage.bmp", "Yutai.Catalog.Resources.bmpLineCoverage.bmp", "Yutai.Catalog.Resources.bmpPolygonCoverage.bmp", "Yutai.Catalog.Resources.bmpRegionCoverage.bmp", "Yutai.Catalog.Resources.bmpCoveragePointFeatClass.bmp", "Yutai.Catalog.Resources.bmpCoverageArcFeatClass.bmp", "Yutai.Catalog.Resources.bmpCoveragePolygonFeatClass.bmp", "Yutai.Catalog.Resources.bmpCoverageNodeFeatClass.bmp", "Yutai.Catalog.Resources.bmpCoverageTicFeatClass.bmp", "Yutai.Catalog.Resources.bmpCoverageAnnoFeatclass.bmp", "Yutai.Catalog.Resources.bmpCoverageRouteFeatClass.bmp", "Yutai.Catalog.Resources.bmpCoverageRegionFeatClass.bmp", "Yutai.Catalog.Resources.bmpCoverageLabelFeatClass.bmp", "Yutai.Catalog.Resources.bmpCoverageLabelFeatClass.bmp", "Yutai.Catalog.Resources.NetworkDataset.bmp", "Yutai.Catalog.Resources.GDBFolder.bmp", "Yutai.Catalog.Resources.AddGDBServer.bmp", "Yutai.Catalog.Resources.GDBServer.bmp", "Yutai.Catalog.Resources.GDBsUnConnect.bmp", "Yutai.Catalog.Resources.DJStructbmp.bmp", "Yutai.Catalog.Resources.GeometryAny.bmp", "Yutai.Catalog.Resources.GeometryServerStart.bmp", "Yutai.Catalog.Resources.GeometryServerStop.bmp", "Yutai.Catalog.Resources.GeometryServerPause.bmp", "Yutai.Catalog.Resources.GPServerStart.bmp", "Yutai.Catalog.Resources.GPServerStop.bmp", "Yutai.Catalog.Resources.GPServerPause.bmp", "Yutai.Catalog.Resources.SearchServerStart.bmp", "Yutai.Catalog.Resources.SearchServerStop.bmp", "Yutai.Catalog.Resources.SearchServerPause.bmp", "Yutai.Catalog.Resources.FeatureServices.bmp" };
				string[] strArrays1 = strArrays;
				for (int i = 0; i < (int)strArrays1.Length; i++)
				{
					string str = strArrays1[i];
					Bitmap bitmap = new Bitmap(this.GetType().Assembly.GetManifestResourceStream(str));
					list_0.Add(bitmap);
				}
			}
			catch (Exception exception)
			{
				//CErrorLog.writeErrorLog(null, exception, "GxCatalog");
                Logger.Current.Write(exception.Message,LogLevel.Error,null);
			}
		}

		private static void old_acctor_mc()
		{
			ImageLib.m_pList = new List<Bitmap>();
			if (ImageLib.m_pList.Count == 0)
			{
				(new ImageLib()).FillImage(ImageLib.m_pList);
			}
		}

		public static void Release()
		{
			for (int i = 0; i < ImageLib.m_pList.Count; i++)
			{
				ImageLib.m_pList[i].Dispose();
			}
			ImageLib.m_pList.Clear();
		}
	}
}