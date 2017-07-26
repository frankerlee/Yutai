using System;
using System.Collections.Generic;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Editor.Helper;

namespace Yutai.Pipeline.Editor.Classes
{
    public class MultiCheQiModel
    {
        private double _xLength = -1;
        private readonly List<CheQiFieldMapping> _fieldMappingList;

        private readonly IFeatureLayer _featureLayer;
        private readonly IFeature _feature;
        private IPolyline _polyline;

        public MultiCheQiModel(IFeatureLayer featureLayer, IFeature feature, List<IFieldSetting> fieldSettingList, IPolyline polyline)
        {
            _featureLayer = featureLayer;
            _feature = feature;
            _polyline = polyline;
            _fieldMappingList = new List<CheQiFieldMapping>();

            foreach (IFieldSetting fieldSetting in fieldSettingList)
            {
                _fieldMappingList.Add(new CheQiFieldMapping(_feature, fieldSetting));
            }
        }

        public IColor Color => GetFeatureLayerColor(_featureLayer, _feature);

        public List<CheQiFieldMapping> FieldMappingList => _fieldMappingList;

        public double XLength
        {
            get
            {
                if (_xLength < 0)
                {
                    _xLength = 0;

                    foreach (CheQiFieldMapping cheQiFieldMapping in _fieldMappingList)
                    {
                        if (string.IsNullOrWhiteSpace(cheQiFieldMapping.FieldSetting.FieldName))
                            continue;
                        _xLength += cheQiFieldMapping.FieldSetting.Length;
                    }

                    _xLength += 10;
                }
                return _xLength;
            }
        }

        public IFeature Feature => _feature;

        public IColor GetFeatureLayerColor(IFeatureLayer pFeatureLayer, IFeature pFeature)
        {
            if (pFeature == null) return null;
            IColor result = new RgbColorClass();
            IGeoFeatureLayer pGeoFt = pFeatureLayer as IGeoFeatureLayer;
            if (pGeoFt == null)
                return new RgbColorClass();
            IFeatureRenderer pFeatureRender = pGeoFt.Renderer;
            if (pFeatureRender != null)
            {
                if (pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    ILineSymbol pLineSymbol = null;
                    pLineSymbol = (ILineSymbol)pFeatureRender.SymbolByFeature[pFeature];
                    result = pLineSymbol.Color;
                }
                else if (pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                {
                    IFillSymbol pFillSymboll = (IFillSymbol)pFeatureRender.SymbolByFeature[pFeature];
                    result = pFillSymboll.Color;
                }
                else if (pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint)
                {
                    IMarkerSymbol pMarkerSymbol = (IMarkerSymbol)pFeatureRender.SymbolByFeature[pFeature];
                    result = pMarkerSymbol.Color;
                }
            }
            return result;
        }

        public double Distance
        {
            get
            {
                IPolyline polyline = _feature.Shape as IPolyline;
                IPoint instersectPoint = GeometryHelper.GetIntersectPoint(_polyline, polyline);
                return GeometryHelper.GetDistance(instersectPoint, _polyline.FromPoint);
            }
        }
    }
}
