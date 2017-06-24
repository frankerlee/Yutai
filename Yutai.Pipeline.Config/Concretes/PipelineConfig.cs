using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Shared;

namespace Yutai.Pipeline.Config.Concretes
{
    public class PipelineConfig : IPipelineConfig
    {
        private List<IPipelineTemplate> _templates;
        private List<IPipelineLayer> _layers;
        private string _xmlFile;
        private IMap _map;
        private IActiveViewEvents_Event viewEvents;
        private string _configDatabaseName;
        private IFeatureWorkspace _workspace;

        public PipelineConfig()
        {
            _templates = new List<IPipelineTemplate>();
            _layers = new List<IPipelineLayer>();
        }

        public string ConfigDatabaseName
        {
            get { return _configDatabaseName; }
            set { _configDatabaseName = value; }
        }

        public IFeatureWorkspace Workspace
        {
            get { return _workspace; }
            set { _workspace = value; }
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

            _configDatabaseName = doc.SelectSingleNode("/PipelineConfig/ConfigDatabase").Value;
            string fullPath = FileHelper.GetFullPath(_configDatabaseName);
            _workspace = Yutai.ArcGIS.Common.Helpers.WorkspaceHelper.GetAccessWorkspace(fullPath) as IFeatureWorkspace;
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
          
        }

        public bool LinkMap(IMap pMap)
        {
            try
            {
                if (this.viewEvents != null)
                {
                    viewEvents.ItemAdded -= ViewEventsOnItemAdded;
                    viewEvents.ItemDeleted -= ViewEventsOnItemDeleted;
                }
            }
            catch (System.Exception ex)
            {

            }
            IActiveView pView = pMap as IActiveView;
            viewEvents = pView as IActiveViewEvents_Event;
            viewEvents.ItemAdded += ViewEventsOnItemAdded;
            viewEvents.ItemDeleted += ViewEventsOnItemDeleted;
            _map = pMap;
            LinkFeatureLayers();
            return true;
        }

        public bool IsPipelineLayer(string classAliasName)
        {
            IBasicLayerInfo layer;
            foreach (IPipelineLayer pipelineLayer in _layers)
            {
                layer = pipelineLayer.Layers.FirstOrDefault(c => c.FeatureClass.AliasName == classAliasName);
                if (layer != null) return true;
            }
            return false;
        }

        public bool IsPipelineLayer(string classAliasName,enumPipelineDataType dataType)
        {
            IBasicLayerInfo layer;
            foreach (IPipelineLayer pipelineLayer in _layers)
            {
                layer = pipelineLayer.Layers.FirstOrDefault(c => c.FeatureClass.AliasName == classAliasName);
                if (layer != null)
                {
                    if(layer.DataType == dataType)
                         return true;
                }
            }
            return false;
        }

        public IBasicLayerInfo GetBasicLayerInfo(IFeatureClass pClass)
        {
            IBasicLayerInfo layer;
            foreach (IPipelineLayer pipelineLayer in _layers)
            {
                layer = pipelineLayer.Layers.FirstOrDefault(c => c.FeatureClass == pClass);
                if (layer != null) return layer;
            }
            return null;
        }

        public IBasicLayerInfo GetBasicLayerInfo(string pClassAliasName)
        {
            IBasicLayerInfo layer;
            foreach (IPipelineLayer pipelineLayer in _layers)
            {
                layer = pipelineLayer.Layers.FirstOrDefault(c => c.FeatureClass.AliasName == pClassAliasName);
                if (layer != null) return layer;
            }
            return null;
        }

        public IYTField GetSpecialField(string classAliasName, string typeWord)
        {
            IBasicLayerInfo layer = GetBasicLayerInfo(classAliasName);
            if (layer == null) return null;
            IYTField field = layer.Fields.FirstOrDefault(c => c.Name == typeWord);
            return field;
        }


        //! 目前只是按照名字对照去识别管线图层，后期可能需要动态的识别管线图层，并进行判断确定是否为合法的管线图层
        private void LinkFeatureLayers()
        {
            foreach (IPipelineLayer pipelineLayer in _layers)
            {
                foreach (IBasicLayerInfo layerInfo in pipelineLayer.Layers)
                {
                    IFeatureLayer pLayer = CommonHelper.GetFeatureLayerByFeatureClassName(_map, layerInfo.Name);
                    if(pLayer != null)
                    {
                        //开始判断字段
                        string[] validateNames = layerInfo.ValidateKeys.Split('/');
                        for(int i=0; i<validateNames.Length;i++)
                        {
                            int fieldIndex = pLayer.FeatureClass.Fields.FindField(validateNames[i]);
                            if (fieldIndex < 0) continue;
                        }
                        layerInfo.FeatureClass = pLayer.FeatureClass;
                        continue;
                    }
                }
            }
        }

        private void ViewEventsOnItemDeleted(object item)
        {
            LinkFeatureLayers();
        }

        private void ViewEventsOnItemAdded(object item)
        {
            LinkFeatureLayers();
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
            XmlAttribute dbAttribute = doc.CreateAttribute("ConfigDatabase");
            dbAttribute.Value = _configDatabaseName;
            rootNode.Attributes.Append(dbAttribute);
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
