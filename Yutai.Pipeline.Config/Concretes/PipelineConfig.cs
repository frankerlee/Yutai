using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ESRI.ArcGIS.Carto;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Pipeline.Config.Concretes
{
    public class PipelineConfig : IPipelineConfig
    {
        private List<IPipelineTemplate> _templates;
        private List<IPipelineLayer> _layers;
        private string _xmlFile;

        public PipelineConfig()
        {
            _templates = new List<IPipelineTemplate>();
            _layers = new List<IPipelineLayer>();
        }

        public List<IPipelineTemplate> Templates
        {
            get { return _templates; }
            set { _templates = value; }
        }

        public List<IPipelineLayer> Layers
        {
            get { return _layers; }
            set { _layers = value; }
        }

        public string XmlFile
        {
            get { return _xmlFile; }
            set { _xmlFile = value; }
        }

        public void LoadFromXml(string fileName)
        {
            _templates.Clear();
            _layers.Clear();
            _xmlFile = fileName;
            //首先读取Template
            XmlDocument doc = new XmlDocument();
            doc.Load(_xmlFile);
            XmlNodeList nodes = doc.SelectNodes("/PipelineConfig/LayerTemplates/Template");
            if (nodes != null)
                foreach (XmlNode node in nodes)
                {
                    IPipelineTemplate template = new PipelineTemplate(node);
                    _templates.Add(template);
                }
            nodes = doc.SelectNodes("/PipelineConfig/PipelineLayers/PipelineLayer");
            if (nodes != null)
                foreach (XmlNode node in nodes)
                {
                    IPipelineLayer layer = new PipelineLayer(node, _templates);
                    _layers.Add(layer);
                }

        }

        public void LoadFromMap(IMap pMap)
        {
            foreach (IPipelineLayer pipelineLayer in _layers)
            {
                if (pipelineLayer.PointLayer != null)
                {
                    IFeatureLayer featureLayer = GetFeatureLayerByLayerName(pMap, pipelineLayer.PointLayer.Name);
                    if (featureLayer != null)
                        pipelineLayer.PointLayer.AutoAssembly(featureLayer);
                }
                if (pipelineLayer.LineLayer != null)
                {
                    IFeatureLayer featureLayer = GetFeatureLayerByLayerName(pMap, pipelineLayer.LineLayer.Name);
                    if (featureLayer != null)
                        pipelineLayer.LineLayer.AutoAssembly(featureLayer);
                }
                if (pipelineLayer.PointAssistLayer != null)
                {
                    IFeatureLayer featureLayer = GetFeatureLayerByLayerName(pMap, pipelineLayer.PointAssistLayer.Name);
                    if (featureLayer != null)
                        pipelineLayer.PointAssistLayer.AutoAssembly(featureLayer);
                }
                if (pipelineLayer.LineAssistLayer != null)
                {
                    IFeatureLayer featureLayer = GetFeatureLayerByLayerName(pMap, pipelineLayer.LineAssistLayer.Name);
                    if (featureLayer != null)
                        pipelineLayer.LineAssistLayer.AutoAssembly(featureLayer);
                }
            }
        }

        public IFeatureLayer GetFeatureLayerByLayerName(IMap map, string layerName)
        {
            IEnumLayer enumLayer = map.Layers[null, true];
            enumLayer.Reset();
            ILayer layer;
            while ((layer = enumLayer.Next()) != null)
            {
                if (layer is IFeatureLayer && layer.Name == layerName)
                {
                    return layer as IFeatureLayer;
                }
            }
            return null;
        }

        public void SaveToXml(string fileName)
        {
            XmlDocument doc = new XmlDocument();

            XmlNode rootNode = doc.CreateElement("PipelineConfig");
            XmlNode layersNode = doc.CreateElement("PipelineLayers");
            foreach (IPipelineLayer pipelineLayer in _layers)
            {
                layersNode.AppendChild(pipelineLayer.ToXml(doc));
            }
            rootNode.AppendChild(layersNode);
            XmlNode templatesNode = doc.CreateElement("LayerTemplates");
            foreach (IPipelineTemplate pipelineTemplate in _templates)
            {
                templatesNode.AppendChild(pipelineTemplate.ToXml(doc));
            }
            rootNode.AppendChild(templatesNode);
            doc.AppendChild(rootNode);
            doc.Save(fileName);
        }

        public void Save()
        {
            XmlDocument doc = new XmlDocument();

            XmlNode rootNode = doc.CreateElement("PipelineConfig");
            XmlNode layersNode = doc.CreateElement("PipelineLayers");
            foreach (IPipelineLayer pipelineLayer in _layers)
            {
                layersNode.AppendChild(pipelineLayer.ToXml(doc));
            }
            rootNode.AppendChild(layersNode);
            XmlNode templatesNode = doc.CreateElement("LayerTemplates");
            foreach (IPipelineTemplate pipelineTemplate in _templates)
            {
                templatesNode.AppendChild(pipelineTemplate.ToXml(doc));
            }
            rootNode.AppendChild(templatesNode);
            doc.AppendChild(rootNode);
            doc.Save(_xmlFile);
        }
    }
}
