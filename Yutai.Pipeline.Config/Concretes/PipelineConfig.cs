using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Pipeline.Config.Concretes
{
    public class PipelineConfig:IPipelineConfig
    {
        private List<IPipelineTemplate> _templates;
        private List<IPipelineLayer> _layers;
        private string _xmlFile;

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
            throw new NotImplementedException();
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
