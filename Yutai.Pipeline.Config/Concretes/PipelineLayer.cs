using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Pipeline.Config.Concretes
{
    public class PipelineLayer:IPipelineLayer
    {
        private IPipePoint _pointLayer;
        private IPipeLine _lineLayer;
        private IPointAssist _pointAssistLayer;
        private ILineAssist _lineAssistLayer;

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

        public void LoadFromXml(string fileName)
        {
            throw new NotImplementedException();
        }

        public XmlNode ToXml()
        {
            throw new NotImplementedException();
        }
    }
}
