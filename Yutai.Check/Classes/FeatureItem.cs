using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Check.Classes
{
    public class FeatureItem
    {
        private int _oid;
        private IFeature _mainFeature;
        private List<FeatureItem> _subFeatureItems;
        private string _remarks;

        public FeatureItem(IFeature feature)
        {
            _oid = feature.OID;
            _mainFeature = feature;
            _subFeatureItems = new List<FeatureItem>();
        }

        [Browsable(true)]
        [Description("编号"), Category("Dev"), DefaultValue(0)]
        public int OID
        {
            get { return _oid; }
            set { _oid = value; }
        }

        public IFeature MainFeature
        {
            get { return _mainFeature; }
            set { _mainFeature = value; }
        }

        public List<FeatureItem> SubFeatureItems
        {
            get { return _subFeatureItems; }
            set { _subFeatureItems = value; }
        }

        public string Remarks
        {
            get
            {
                return _remarks;
            }
            set { _remarks = value; }
        }
    }
}
