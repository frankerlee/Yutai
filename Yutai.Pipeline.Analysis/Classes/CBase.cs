using System;
using System.Collections.Generic;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Classes
{
    public class CBase
    {
        public IAppContext m_app;

        public List<IFeatureLayer> m_listLayers = new List<IFeatureLayer>();

        public CBase()
        {
        }

        public void AddFeatureLayer(IFeatureLayer iFLayer, List<IFeatureLayer> pListLayers)
        {
            try
            {
                if (iFLayer != null)
                {
                    IFeatureClass featureClass = iFLayer.FeatureClass;
                    if ((featureClass.ShapeType == (esriGeometryType) 13 ? true : featureClass.ShapeType == (esriGeometryType)3))
                    {
                        INetworkClass networkClass = featureClass as INetworkClass;
                        if ((networkClass == null ? false : networkClass.GeometricNetwork != null))
                        {
                            pListLayers.Add(iFLayer);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }

        public void AddGroupLayer(IGroupLayer iGLayer, List<IFeatureLayer> pListLayers)
        {
            ICompositeLayer compositeLayer = (ICompositeLayer)iGLayer;
            if (compositeLayer != null)
            {
                int count = compositeLayer.Count;
                for (int i = 0; i < count; i++)
                {
                    this.AddLayer(compositeLayer.get_Layer(i), pListLayers);
                }
            }
        }

        public void AddLayer(ILayer ipLay, List<IFeatureLayer> pListLayers)
        {
            if (ipLay is IFeatureLayer)
            {
                this.AddFeatureLayer((IFeatureLayer)ipLay, pListLayers);
            }
            else if (ipLay is IGroupLayer)
            {
                this.AddGroupLayer((IGroupLayer)ipLay, pListLayers);
            }
        }
    }
}