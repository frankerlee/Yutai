using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using Newtonsoft.Json;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Plugins.Template.Interfaces;

namespace Yutai.Plugins.Template.Concretes
{

    //public class YTDataset
    //{
    //    public string Name { get; set; }
    //    public string AliasName { get; set; }

    //    public string BaseName { get; set; }
    //    public int ID { get; set; }

    //    public YTDataset()
    //    {
    //        ID = 0;
    //        Name = "";
    //        AliasName = "";
    //        BaseName = "";
    //    }

    //    public YTDataset(IRow pRow)
    //    {
    //        ID = pRow.OID;
    //        Name = FeatureHelper.GetRowValue(pRow, "Dataset").ToString();
    //        AliasName = FeatureHelper.GetRowValue(pRow, "AliasName").ToString();
    //        BaseName = FeatureHelper.GetRowValue(pRow, "BaseName").ToString();
    //    }

    //    public bool UpdateRow(IRow pRow)
    //    {
    //        pRow.Value[pRow.Fields.FindField("Dataset")] = Name;
    //        pRow.Value[pRow.Fields.FindField("AliasName")] = AliasName;
    //        pRow.Value[pRow.Fields.FindField("BaseName")] = BaseName;
    //        pRow.Store();
    //        ID = pRow.OID;
    //        return true;
    //    }

    //}
    public class YTDomain : IYTDomain
    {
        private int _id;
        private string _name;
        private string _description;
        private List<YTDomainValue> _valuePairs;
        private string _domainValues;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public List<YTDomainValue> ValuePairs
        {
            get { return _valuePairs; }
            set { _valuePairs = value; }
        }

        public string DomainValues
        {
            get { return _domainValues; }
            set { _domainValues = value; }
        }

        public bool UpdateRow(IRow pRow)
        {
            pRow.Value[pRow.Fields.FindField("DomainName")] = _name;
            pRow.Value[pRow.Fields.FindField("DomainDescription")] = _description;
            _domainValues = JsonConvert.SerializeObject(_valuePairs);
            pRow.Value[pRow.Fields.FindField("DomainValues")] = _domainValues;
            pRow.Store();
            _id = pRow.OID;
            return true;
        }

        public YTDomain()
        {
            _id = 0;
            _name = "";
            _description = "";
            _valuePairs=new List<YTDomainValue>();
            _domainValues = "";
        }
        public YTDomain(IRow pRow)
        {
            _id = pRow.OID;
            _name=  FeatureHelper.GetRowValue(pRow, "DomainName").ToString();
            _description =  FeatureHelper.GetRowValue(pRow, "DomainDescription").ToString();
            _domainValues =  FeatureHelper.GetRowValue(pRow, "DomainValues").ToString();
            if (string.IsNullOrEmpty(_domainValues))
            {
                _valuePairs=new List<YTDomainValue>();
            }
            else
            {
                _valuePairs = JsonConvert.DeserializeObject<List<YTDomainValue>>(_domainValues) ;
            }
            
        }
    }
}