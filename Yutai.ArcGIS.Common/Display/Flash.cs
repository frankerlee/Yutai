using System;
using System.Threading;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.GlobeCore;
using Yutai.Shared;

namespace Yutai.ArcGIS.Common.Display
{
    public class Flash
    {
        public Flash()
        {
        }

        public static void FlashFeature(IBasicMap ibasicMap_0, IFeature ifeature_0)
        {
            if (ifeature_0 != null)
            {
                if (ibasicMap_0 is IGlobe)
                {
                    IDisplay3D globeDisplay = (ibasicMap_0 as IGlobe).GlobeDisplay as IDisplay3D;
                    IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
                    IEnvelope envelope = ifeature_0.Shape.Envelope;
                    pointClass.X = (envelope.XMin + envelope.XMax)/2;
                    pointClass.Y = (envelope.YMin + envelope.YMax)/2;
                    globeDisplay.FlashLocation(pointClass);
                }
                else if (ibasicMap_0 is IScene)
                {
                    ((ibasicMap_0 as IScene).SceneGraph as IDisplay3D).AddFlashFeature(ifeature_0.Shape);
                    ((ibasicMap_0 as IScene).SceneGraph as IDisplay3D).FlashFeatures();
                }
                else if (ibasicMap_0 is IActiveView)
                {
                    Flash.FlashGeometry((ibasicMap_0 as IActiveView).ScreenDisplay, ifeature_0.Shape);
                }
            }
        }

        public static void FlashFeature(IScreenDisplay iscreenDisplay_0, IFeature ifeature_0)
        {
            if (ifeature_0 != null)
            {
                Flash.FlashGeometry(iscreenDisplay_0, ifeature_0.Shape);
            }
        }

        public static void FlashGeometry(IScreenDisplay iscreenDisplay_0, IGeometry igeometry_0)
        {
            if (igeometry_0 != null)
            {
                iscreenDisplay_0.StartDrawing(0, -1);
                switch (igeometry_0.GeometryType)
                {
                    case esriGeometryType.esriGeometryPoint:
                    {
                        Flash.FlashPoint(iscreenDisplay_0, igeometry_0);
                        break;
                    }
                    case esriGeometryType.esriGeometryMultipoint:
                    {
                        Flash.FlashMultiPoint(iscreenDisplay_0, igeometry_0);
                        break;
                    }
                    case esriGeometryType.esriGeometryPolyline:
                    {
                        Flash.FlashLine(iscreenDisplay_0, igeometry_0);
                        break;
                    }
                    case esriGeometryType.esriGeometryPolygon:
                    {
                        Flash.FlashPolygon(iscreenDisplay_0, igeometry_0);
                        break;
                    }
                }
                iscreenDisplay_0.FinishDrawing();
            }
        }

        public static void FlashGeometry(IScreenDisplay iscreenDisplay_0, IGeometry igeometry_0, ISymbol isymbol_0)
        {
            if (igeometry_0 != null)
            {
                ISymbol symbol = (isymbol_0 as IClone).Clone() as ISymbol;
                iscreenDisplay_0.StartDrawing(0, -1);
                symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
                iscreenDisplay_0.SetSymbol(isymbol_0);
                switch (igeometry_0.GeometryType)
                {
                    case esriGeometryType.esriGeometryPoint:
                    {
                        iscreenDisplay_0.DrawPoint(igeometry_0);
                        Thread.Sleep(300);
                        iscreenDisplay_0.DrawPoint(igeometry_0);
                        goto case esriGeometryType.esriGeometryPolygon;
                    }
                    case esriGeometryType.esriGeometryMultipoint:
                    case esriGeometryType.esriGeometryPolyline:
                    case esriGeometryType.esriGeometryPolygon:
                    {
                        iscreenDisplay_0.FinishDrawing();
                        break;
                    }
                    default:
                    {
                        goto case esriGeometryType.esriGeometryPolygon;
                    }
                }
            }
        }

        private static void FlashLine(IScreenDisplay iscreenDisplay_0, IGeometry igeometry_0)
        {
            ISymbol simpleLineSymbolClass = (ISymbol) (new SimpleLineSymbol()
            {
                Width = 4
            });
            simpleLineSymbolClass.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
            iscreenDisplay_0.SetSymbol(simpleLineSymbolClass);
            iscreenDisplay_0.DrawPolyline(igeometry_0);
            Thread.Sleep(300);
            iscreenDisplay_0.DrawPolyline(igeometry_0);
        }

        private static void FlashMultiPoint(IScreenDisplay iscreenDisplay_0, IGeometry igeometry_0)
        {
            try
            {
                ISimpleMarkerSymbol simpleMarkerSymbolClass = new SimpleMarkerSymbol()
                {
                    Style = esriSimpleMarkerStyle.esriSMSCircle
                };
                (new RgbColor()).Green = 128;
                ISymbol symbol = (ISymbol) simpleMarkerSymbolClass;
                symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
                for (int i = 0; i < (igeometry_0 as IPointCollection).PointCount; i++)
                {
                    iscreenDisplay_0.SetSymbol(symbol);
                    iscreenDisplay_0.DrawPoint((igeometry_0 as IPointCollection).Point[i]);
                    Thread.Sleep(300);
                    iscreenDisplay_0.DrawPoint((igeometry_0 as IPointCollection).Point[i]);
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
        }

        private static void FlashPoint(IScreenDisplay iscreenDisplay_0, IGeometry igeometry_0)
        {
            ISymbol simpleMarkerSymbolClass = (ISymbol) (new SimpleMarkerSymbol()
            {
                Style = esriSimpleMarkerStyle.esriSMSCircle
            });
            simpleMarkerSymbolClass.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
            iscreenDisplay_0.SetSymbol(simpleMarkerSymbolClass);
            iscreenDisplay_0.DrawPoint(igeometry_0);
            Thread.Sleep(300);
            iscreenDisplay_0.DrawPoint(igeometry_0);
        }

        private static void FlashPolygon(IScreenDisplay iscreenDisplay_0, IGeometry igeometry_0)
        {
            ISymbol simpleFillSymbolClass = (ISymbol) (new SimpleFillSymbol()
            {
                Outline = null
            });
            simpleFillSymbolClass.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
            iscreenDisplay_0.SetSymbol(simpleFillSymbolClass);
            iscreenDisplay_0.DrawPolygon(igeometry_0);
            Thread.Sleep(300);
            iscreenDisplay_0.DrawPolygon(igeometry_0);
        }

        public static void FlashSelectedFeature(IScreenDisplay iscreenDisplay_0, IFeatureLayer ifeatureLayer_0)
        {
            ICursor cursor;
            IFeatureSelection ifeatureLayer0 = ifeatureLayer_0 as IFeatureSelection;
            if (ifeatureLayer0.SelectionSet.Count != 0)
            {
                ifeatureLayer0.SelectionSet.Search(null, false, out cursor);
                for (IRow i = cursor.NextRow(); i != null; i = cursor.NextRow())
                {
                    Flash.FlashFeature(iscreenDisplay_0, i as IFeature);
                }
                ComReleaser.ReleaseCOMObject(cursor);
            }
        }

        public static void FlashSelectedFeature(IBasicMap ibasicMap_0, IFeatureLayer ifeatureLayer_0)
        {
            ICursor cursor;
            IFeatureSelection ifeatureLayer0 = ifeatureLayer_0 as IFeatureSelection;
            if (ifeatureLayer0.SelectionSet.Count != 0)
            {
                ifeatureLayer0.SelectionSet.Search(null, false, out cursor);
                for (IRow i = cursor.NextRow(); i != null; i = cursor.NextRow())
                {
                    Flash.FlashFeature(ibasicMap_0, i as IFeature);
                }
                ComReleaser.ReleaseCOMObject(cursor);
            }
        }

        public static void RefreshLayerWithSelection(IMap imap_0, IEnvelope ienvelope_0,
            esriViewDrawPhase esriViewDrawPhase_0)
        {
            if (imap_0 != null)
            {
                IActiveView imap0 = (IActiveView) imap_0;
                if (ienvelope_0 == null)
                {
                    IEnumFeature featureSelection = (IEnumFeature) imap_0.FeatureSelection;
                    featureSelection.Reset();
                    IFeature feature = featureSelection.Next();
                    ienvelope_0 = feature.Extent;
                    while (feature != null)
                    {
                        ienvelope_0.Union(feature.Extent);
                        feature = featureSelection.Next();
                    }
                }
                for (int i = 0; i < imap_0.LayerCount; i++)
                {
                    IFeatureLayer layer = (IFeatureLayer) imap_0.Layer[i];
                    if (((IFeatureSelection) layer).SelectionSet.Count > 0)
                    {
                        imap0.PartialRefresh(esriViewDrawPhase_0, layer, ienvelope_0);
                    }
                }
            }
        }
    }
}