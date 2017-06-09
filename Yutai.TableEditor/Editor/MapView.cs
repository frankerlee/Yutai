// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  MapView.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/07  17:40
// 更新时间 :  2017/06/07  17:40

using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.Plugins.TableEditor.Editor
{
    public class MapView : IMapView
    {
        private double expandNum = 5;
        private IMap _map;
        private IActiveView _activeView;
        public MapView(IMap map)
        {
            _map = map;
            _activeView = _map as IActiveView;
        }
        
        public IFeature SelectFeature(IFeatureLayer featureLayer, int oid)
        {
            IFeature pFeature = featureLayer.FeatureClass.GetFeature(oid);
            _map.ClearSelection();
            _map.SelectFeature(featureLayer, pFeature);
            _activeView.Refresh();
            return pFeature;
        }

        public void SelectFeatures(IFeatureLayer featureLayer, List<int> oids)
        {
            _map.ClearSelection();
            foreach (int oid in oids)
            {
                _map.SelectFeature(featureLayer, featureLayer.FeatureClass.GetFeature(oid));
            }
            _activeView.Refresh();
        }

        public void ZoomToFeature(IFeatureLayer featureLayer, int oid)
        {
            IFeature pFeature = SelectFeature(featureLayer, oid);
            IGeometry pGeometry = pFeature.Shape;
            if (pGeometry == null || pGeometry.IsEmpty)
                return;
            IEnvelope pEnvelope = pGeometry.Envelope;
            if (pGeometry.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                pEnvelope.XMax += 20;
                pEnvelope.XMin -= 20;
                pEnvelope.YMax += 20;
                pEnvelope.YMin -= 20;
            }
            else
            {
                pEnvelope.Expand(expandNum, expandNum, false);
            }
            _activeView.Extent = pEnvelope;
            _activeView.Refresh();
        }

        public void ZoomToSelectedFeatures(IFeatureLayer featureLayer)
        {
            MapHelper.Zoom2SelectedFeature(_activeView, featureLayer);
        }

        public void PanToFeature(IFeatureLayer featureLayer, int oid)
        {
            IFeature pFeature = SelectFeature(featureLayer, oid);
            IGeometry pGeometry = pFeature.Shape;
            if (pGeometry == null || pGeometry.IsEmpty)
                return;
            IEnvelope pEnvelope = _activeView.Extent;
            IEnvelope pEnvelopeCenter = pGeometry.Envelope;
            double centerX = (pEnvelopeCenter.XMax + pEnvelopeCenter.XMin)/2;
            double centerY = (pEnvelopeCenter.YMax + pEnvelopeCenter.YMin)/2;
            IPoint point = new MapPointClass();
            point.PutCoords(centerX, centerY);
            pEnvelope.CenterAt(point);
            _activeView.Extent = pEnvelope;
            _activeView.Refresh();
        }
    }
}