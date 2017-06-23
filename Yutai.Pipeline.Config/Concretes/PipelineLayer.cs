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
        private IPipePoint _pointLayer;
        private IPipeLine _lineLayer;
        private IPointAssist _pointAssistLayer;
        private ILineAssist _lineAssistLayer;
        private List<IPipelineTemplate> _templates;
        private string _name;
        private string _code;

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

        public IPipePoint PointLayer
        {
            get { return _pointLayer; }
            set { _pointLayer = value; }
        }

        public IPipeLine LineLayer
        {
            get { return _lineLayer; }
            set { _lineLayer = value; }
        }

        public IPointAssist PointAssistLayer
        {
            get { return _pointAssistLayer; }
            set { _pointAssistLayer = value; }
        }

        public ILineAssist LineAssistLayer
        {
            get { return _lineAssistLayer; }
            set { _lineAssistLayer = value; }
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
            XmlNode node = xml.SelectSingleNode($"/PipelineConfig/PipelineLayers/PipelineLayer[@Name='{_name}']/PointLayer");
            if (node?.Attributes != null)
            {
                string template = node.Attributes["Template"].Value;
                if (_templates == null || string.IsNullOrWhiteSpace(template))
                    _pointLayer = new PipePoint(node);
                else
                {
                    IPipelineTemplate pipelineTemplate = _templates.FirstOrDefault(c => c.Name == template);
                    if (pipelineTemplate != null)
                        _pointLayer = new PipePoint(node, pipelineTemplate);
                    else
                        _pointLayer = new PipePoint(node);
                }
            }
            node = xml.SelectSingleNode($"/PipelineConfig/PipelineLayers/PipelineLayer[@Name='{_name}']/LineLayer");

            if (node?.Attributes != null)
            {
                string template = node.Attributes["Template"].Value;

                if (_templates == null || string.IsNullOrWhiteSpace(template))
                    _lineLayer = new PipeLine(node);
                else
                {
                    IPipelineTemplate pipelineTemplate = _templates.FirstOrDefault(c => c.Name == template);
                    if (pipelineTemplate != null)
                        _lineLayer = new PipeLine(node, pipelineTemplate);
                    else
                        _lineLayer = new PipeLine(node);
                }
            }

            node = xml.SelectSingleNode($"/PipelineConfig/PipelineLayers/PipelineLayer[@Name='{_name}']/PointAssist");
            if (node?.Attributes != null)
            {
                string template = node.Attributes["Template"].Value;

                if (_templates == null || string.IsNullOrWhiteSpace(template))
                    _pointAssistLayer = new PointAssist(node);
                else
                {
                    IPipelineTemplate pipelineTemplate = _templates.FirstOrDefault(c => c.Name == template);
                    if (pipelineTemplate != null)
                        _pointAssistLayer = new PointAssist(node, pipelineTemplate);
                    else
                        _pointAssistLayer = new PointAssist(node);
                }
            }

            node = xml.SelectSingleNode($"/PipelineConfig/PipelineLayers/PipelineLayer[@Name='{_name}']/LineAssist");
            if (node?.Attributes != null)
            {
                string template = node.Attributes["Template"].Value;
                if (_templates == null || string.IsNullOrWhiteSpace(template))
                    _lineAssistLayer = new LineAssist(node);
                else
                {
                    IPipelineTemplate pipelineTemplate = _templates.FirstOrDefault(c => c.Name == template);
                    if (pipelineTemplate != null)
                        _lineAssistLayer = new LineAssist(node, pipelineTemplate);
                    else
                        _lineAssistLayer = new LineAssist(node);
                }
            }
        }

        public XmlNode ToXml(XmlDocument doc)
        {
            XmlNode layerNode = doc.CreateElement("PipelineLayer");
            if (_pointLayer != null) layerNode.AppendChild(_pointLayer.ToXml(doc));
            if (_lineLayer != null) layerNode.AppendChild(_lineLayer.ToXml(doc));
            if (_pointAssistLayer != null) layerNode.AppendChild(_pointAssistLayer.ToXml(doc));
            if (_lineAssistLayer != null) layerNode.AppendChild(_lineAssistLayer.ToXml(doc));
            return layerNode;
        }
    }
}
