using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Pipeline.Config.Concretes
{
    public class PipelineLayer : IPipelineLayer
    {
        private List<IPipelineTemplate> _templates;
        private string _name;
        private string _code;
        private List<IBasicLayerInfo> _layers;

        public PipelineLayer()
        {
        }

        public PipelineLayer(XmlNode xmlNode)
        {
            ReadFromXml(xmlNode);
        }

        public PipelineLayer(XmlNode xmlNode, List<IPipelineTemplate> templates)
        {
            _templates = templates;
            ReadFromXml(xmlNode);
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public List<IBasicLayerInfo> Layers
        {
            get { return _layers; }
            set { _layers = value; }
        }

        public void ReadFromXml(XmlNode xml)
        {
            if (xml == null)
                return;
            if (xml.Attributes != null)
            {
                _name = xml.Attributes["Name"].Value;
                _code = xml.Attributes["Code"].Value;
            }
            XmlNodeList nodeList = xml.SelectNodes($"/PipelineConfig/PipelineLayers/PipelineLayer[@Name='{_name}']/Layers/Layer");
            foreach (XmlNode node in nodeList)
            {
                string template = node.Attributes["Template"].Value;
                IBasicLayerInfo layerInfo;
                if (_templates == null || string.IsNullOrWhiteSpace(template))
                    layerInfo = new BasicLayerInfo(node);
                else
                {
                    IPipelineTemplate pipelineTemplate = _templates.FirstOrDefault(c => c.Name == template);
                    if (pipelineTemplate != null)
                        layerInfo = new BasicLayerInfo(node, pipelineTemplate);
                    else
                        layerInfo = new BasicLayerInfo(node);
                }
                 _layers.Add(layerInfo);
            }
           
        }

        public XmlNode ToXml(XmlDocument doc)
        {
            XmlNode layerNode = doc.CreateElement("PipelineLayer");
            XmlAttribute nameAttribute = doc.CreateAttribute("Name");
            nameAttribute.Value = _name;
            XmlAttribute codeAttribute = doc.CreateAttribute("Code");
            codeAttribute.Value = _code;
            layerNode.Attributes.Append(nameAttribute);
            layerNode.Attributes.Append(codeAttribute);
            XmlNode subNodes = doc.CreateElement("Layers");
            foreach (IBasicLayerInfo basicInfo in _layers)
            {
                XmlNode oneNode = basicInfo.ToXml(doc);
                subNodes.AppendChild(oneNode);
            }
            layerNode.AppendChild(subNodes);
            return layerNode;
        }
    }
}
