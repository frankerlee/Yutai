using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Pipeline.Config.Concretes
{
    public class PipelineLayer : IPipelineLayer
    {
        private List<IPipelineTemplate> _templates;
        private string _name;
        private string _code;
        private List<IBasicLayerInfo> _layers;
        private string _autoNames;
        private IWorkspace _workspace;
        private string _classCode;

        public PipelineLayer()
        {
        }

        public PipelineLayer(IPipelineLayer layer, bool keepClass)
        {
            _name = layer.Name;
            _code = layer.Code;
            _classCode = layer.ClassCode;
            _autoNames = layer.AutoNames;
            _layers = new List<IBasicLayerInfo>();
            foreach (IBasicLayerInfo basicLayerInfo in layer.Layers)
            {
                IBasicLayerInfo newBasicLayerInfo = basicLayerInfo.Clone(keepClass);
                _layers.Add(newBasicLayerInfo);
            }

            if (keepClass)
            {
                _workspace = layer.Workspace;
            }
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

        public string ClassCode
        {
            get { return _classCode; }
            set { _classCode = value; }
        }

        public string AutoNames
        {
            get { return _autoNames; }
            set { _autoNames = value; }
        }

        public string FixAutoNames
        {
            get { return "/" + _autoNames + "/"; }
        }

        public List<IBasicLayerInfo> Layers
        {
            get { return _layers; }
            set { _layers = value; }
        }

        public IWorkspace Workspace
        {
            get { return _workspace; }

            set { _workspace = value; }
        }

        public List<IBasicLayerInfo> GetLayers(enumPipelineDataType dataType)
        {
            return (from c in _layers where c.DataType == dataType select c).ToList();
        }

        public void ReadFromXml(XmlNode xml)
        {
            if (xml == null)
                return;
            if (xml.Attributes != null)
            {
                _name = xml.Attributes["Name"] == null ? "" : xml.Attributes["Name"].Value;
                _code = xml.Attributes["Code"] == null ? "" : xml.Attributes["Code"].Value;
                _classCode = xml.Attributes["ClassCode"] == null ? "" : xml.Attributes["ClassCode"].Value;
                _autoNames = xml.Attributes["AutoNames"] == null ? "" : xml.Attributes["AutoNames"].Value;
            }
            XmlNodeList nodeList =
                xml.SelectNodes($"/PipelineConfig/PipelineLayers/PipelineLayer[@Name='{_name}']/Layers/Layer");
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
            XmlAttribute classcodeAttribute = doc.CreateAttribute("ClassCode");
            classcodeAttribute.Value = _classCode;
            XmlAttribute autoNamesAttribute = doc.CreateAttribute("AutoNames");
            autoNamesAttribute.Value = _autoNames;
            layerNode.Attributes.Append(nameAttribute);
            layerNode.Attributes.Append(codeAttribute);
            layerNode.Attributes.Append(classcodeAttribute);
            layerNode.Attributes.Append(autoNamesAttribute);
            XmlNode subNodes = doc.CreateElement("Layers");
            foreach (IBasicLayerInfo basicInfo in _layers)
            {
                XmlNode oneNode = basicInfo.ToXml(doc);
                subNodes.AppendChild(oneNode);
            }
            layerNode.AppendChild(subNodes);
            return layerNode;
        }

        //!+ 自动识别图层并匹配配置,这个时候是这个图层组中有图层已经被识别
        public bool OrganizeFeatureClass(IFeatureClass featureClass)
        {
            //! 因为图层是按照工作空间组织的，所以图层不可能被重复，也就是说一个工作控件里面的图层只可能识别一次
            string ownerName = ConfigHelper.GetClassOwnerName(((IDataset) featureClass).FullName.NameString);
            string baseName = ConfigHelper.GetClassShortName(featureClass);
            string classAliasName = featureClass.AliasName;
            string autoStr = "/" + _autoNames + "/";
            IBasicLayerInfo layerInfo =
                _layers.FirstOrDefault(
                    c => c.Name == baseName || c.AliasName == baseName || c.FixAutoNames.Contains("/" + baseName + "/"));
            if (layerInfo != null && layerInfo.FeatureClass == null)
            {
                layerInfo.FeatureClass = featureClass;
                return true;
            }
            return false;
        }

        public IPipelineLayer NewOrganizeFeatureClass(IFeatureClass featureClass)
        {
            string ownerName = ConfigHelper.GetClassOwnerName(((IDataset) featureClass).Name);
            string baseName = ConfigHelper.GetClassShortName(featureClass);
            string classAliasName = featureClass.AliasName;
            string autoStr = "/" + _autoNames + "/";
            IBasicLayerInfo layerInfo =
                _layers.FirstOrDefault(
                    c => c.Name == baseName || c.AliasName == baseName || autoStr.Contains("/" + baseName + "/"));
            if (layerInfo != null)
            {
                IPipelineLayer pipeLayer = new PipelineLayer(this, false);
                layerInfo =
                    pipeLayer.Layers.FirstOrDefault(
                        c => c.Name == baseName || c.AliasName == baseName || autoStr.Contains("/" + baseName + "/"));
                layerInfo.FeatureClass = featureClass;
                return pipeLayer;
            }
            return null;
        }

        public IPipelineLayer Clone(bool keepClass)
        {
            IPipelineLayer pClone = new PipelineLayer(this, keepClass);

            return pClone;
        }
    }
}