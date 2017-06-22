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
    public class PipelineConfig:IPipelineConfig
    {
        private List<IPipelineTemplate> _templates;
        private List<IPipelineLayer> _layers;
        private string _xmlFile;

        public PipelineConfig()
        {
            _templates=new List<IPipelineTemplate>();
            _layers=new List<IPipelineLayer>();
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
            //首先读取Template
            XmlDocument doc=new XmlDocument();
            doc.Load(fileName);
            XmlNodeList nodes = doc.SelectNodes("/PipelineConfig/LayerTemplates/Template");
            foreach (XmlNode node in nodes)
            {
                IPipelineTemplate template=new PipelineTemplate(node);
                _templates.Add(template);
            }
        }

        public void LoadFromMap(IMap pMap)
        {
            throw new NotImplementedException();
        }

        public void SaveToXml(string fileName)
        {
            throw new NotImplementedException();
        }
      

        public void Save()
        {
            throw new NotImplementedException();
        }

     
    }
}
