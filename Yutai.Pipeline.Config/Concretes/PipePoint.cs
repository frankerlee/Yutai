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
    public class PipePoint:IPipePoint
    {
        private string _typeName;
        private IYTField _noField;
        private IYTField _xField;
        private IYTField _yField;
        private IYTField _zField;
        private IYTField _depthField;
        private IYTField _featureField;
        private IYTField _subsidField;
        private IYTField _pStyleField;
        private IYTField _pdsField;
        private IYTField _codeField;
        private IYTField _mapNoField;
        private IYTField _useStatusField;
        private IYTField _bCodeField;
        private IYTField _rotangField;
        private IYTField _roadCodeField;
        private IYTField _mDateField;
        private IYTField _pcjhField;
        private IYTField _remarkField;
        private string _name;
        private string _aliasName;
        private bool _visible;
        private string _templateName;

        public PipePoint()
        {
            
        }

        public PipePoint(XmlNode xmlNode)
        {

        }

        public PipePoint(XmlNode xmlNode, IPipeTemplate template)
        {

        }


        public string TypeName
        {
            get { return _typeName; }
            set { _typeName = value; }
        }

        public IYTField NoField
        {
            get { return _noField; }
            set { _noField = value; }
        }

        public IYTField XField
        {
            get { return _xField; }
            set { _xField = value; }
        }

        public IYTField YField
        {
            get { return _yField; }
            set { _yField = value; }
        }

        public IYTField ZField
        {
            get { return _zField; }
            set { _zField = value; }
        }

        public IYTField DepthField
        {
            get { return _depthField; }
            set { _depthField = value; }
        }

        public IYTField FeatureField
        {
            get { return _featureField; }
            set { _featureField = value; }
        }

        public IYTField SubsidField
        {
            get { return _subsidField; }
            set { _subsidField = value; }
        }

        public IYTField PStyleField
        {
            get { return _pStyleField; }
            set { _pStyleField = value; }
        }

        public IYTField PDSField
        {
            get { return _pdsField; }
            set { _pdsField = value; }
        }

        public IYTField CodeField
        {
            get { return _codeField; }
            set { _codeField = value; }
        }

        public IYTField MapNoField
        {
            get { return _mapNoField; }
            set { _mapNoField = value; }
        }

        public IYTField UseStatusField
        {
            get { return _useStatusField; }
            set { _useStatusField = value; }
        }

        public IYTField BCodeField
        {
            get { return _bCodeField; }
            set { _bCodeField = value; }
        }

        public IYTField RotangField
        {
            get { return _rotangField; }
            set { _rotangField = value; }
        }

        public IYTField RoadCodeField
        {
            get { return _roadCodeField; }
            set { _roadCodeField = value; }
        }

        public IYTField MDateField
        {
            get { return _mDateField; }
            set { _mDateField = value; }
        }

        public IYTField PCJHField
        {
            get { return _pcjhField; }
            set { _pcjhField = value; }
        }

        public IYTField RemarkField
        {
            get { return _remarkField; }
            set { _remarkField = value; }
        }

        public void AutoAssembly(IFeatureLayer pLayer)
        {
            throw new NotImplementedException();
        }

        public void ReadFromXml(XmlNode xml)
        {
            throw new NotImplementedException();
        }

        public XmlNode ToXml()
        {
            throw new NotImplementedException();
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string AliasName
        {
            get { return _aliasName; }
            set { _aliasName = value; }
        }

        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        public string TemplateName
        {
            get { return _templateName; }
            set { _templateName = value; }
        }

        public void LoadTemplate(IPipeTemplate template)
        {
            throw new NotImplementedException();
        }
    }
}
