using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Pipeline.Editor.Helper;

namespace Yutai.Pipeline.Editor.Classes
{
    public class CheQiFieldMapping
    {
        private readonly IFeature _feature;
        private readonly IFieldSetting _fieldSetting;

        public CheQiFieldMapping(IFeature feature, IFieldSetting fieldSetting)
        {
            _feature = feature;
            _fieldSetting = fieldSetting;
        }
        
        public string FieldValue
        {
            get { return CommonHelper.GetFlagInformationInFeature(_feature, _fieldSetting.Expression); }
        }

        public IFieldSetting FieldSetting
        {
            get { return _fieldSetting; }
        }
    }
}
