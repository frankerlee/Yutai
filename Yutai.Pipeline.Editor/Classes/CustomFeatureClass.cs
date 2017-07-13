using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Pipeline.Editor.Classes
{
    public class CustomFeatureClass
    {
        #region 字段

        #region 名称

        public string StartKeyField { get; set; }

        public string EndKeyField { get; set; }

        public string XField { get; set; }
        public string YField { get; set; }
        public string ZField { get; set; }

        public string StartDepthFieldName { get; set; }
        public string EndDepthFieldName { get; set; }
        public string StartHeightFieldName { get; set; }
        public string EndHeightFieldName { get; set; }

        public string GXLXFieldName { get; set; }
        public string UniqueFieldName { get; set; }

        public string FilterFieldName { get; set; }
        #endregion

        #region 索引

        private int _idxGxlxField = -1;
        private int _idxStartDepthField = -1;
        private int _idxEndDepthField = -1;

        private int _idxStartKeyField = -1;
        private int _idxEndKeyField = -1;
        private int _idxXField = -1;
        private int _idxYField = -1;
        private int _idxZField = -1;
        private int _idxStartHeightField = -1;
        private int _idxEndHeightField = -1;
        private int _idxUniqueField = -1;

        private int _idxFilterField = -1;

        public int IdxStartKeyField
        {
            get
            {
                if (_idxStartKeyField == -1 && FeatureClass != null && !string.IsNullOrEmpty(StartKeyField))
                    _idxStartKeyField = FeatureClass.FindField(StartKeyField);
                return _idxStartKeyField;
            }
            set { _idxStartKeyField = value; }
        }

        public int IdxEndKeyField
        {
            get
            {
                if (_idxEndKeyField == -1 && FeatureClass != null && !string.IsNullOrEmpty(EndKeyField))
                    _idxEndKeyField = FeatureClass.FindField(EndKeyField);
                return _idxEndKeyField;
            }
            set { _idxEndKeyField = value; }
        }

        public int IdxXField
        {
            get
            {
                if (_idxXField == -1 && FeatureClass != null && !string.IsNullOrEmpty(XField))
                    _idxXField = FeatureClass.FindField(XField);
                return _idxXField;
            }
            set { _idxXField = value; }
        }

        public int IdxYField
        {
            get
            {
                if (_idxYField == -1 && FeatureClass != null && !string.IsNullOrEmpty(YField))
                    _idxYField = FeatureClass.FindField(YField);
                return _idxYField;
            }
            set { _idxYField = value; }
        }

        public int IdxZField
        {
            get
            {
                if (_idxZField == -1 && FeatureClass != null && !string.IsNullOrEmpty(ZField))
                    _idxZField = FeatureClass.FindField(ZField);
                return _idxZField;
            }
            set { _idxZField = value; }
        }

        public int IdxStartDepthField
        {
            get
            {
                if (_idxStartDepthField == -1 && FeatureClass != null && !string.IsNullOrEmpty(StartDepthFieldName))
                    _idxStartDepthField = FeatureClass.FindField(StartDepthFieldName);
                return _idxStartDepthField;
            }
            set { _idxStartDepthField = value; }
        }

        public int IdxEndDepthField
        {
            get
            {
                if (_idxEndDepthField == -1 && FeatureClass != null && !string.IsNullOrEmpty(EndDepthFieldName))
                    _idxEndDepthField = FeatureClass.FindField(EndDepthFieldName);
                return _idxEndDepthField;
            }
            set { _idxEndDepthField = value; }
        }

        public int IdxStartHeightField
        {
            get
            {
                if (_idxStartHeightField == -1 && FeatureClass != null && !string.IsNullOrEmpty(StartHeightFieldName))
                    _idxStartHeightField = FeatureClass.FindField(StartHeightFieldName);
                return _idxStartHeightField;
            }
            set { _idxStartHeightField = value; }
        }

        public int IdxEndHeightField
        {
            get
            {
                if (_idxEndHeightField == -1 && FeatureClass != null && !string.IsNullOrEmpty(EndHeightFieldName))
                    _idxEndHeightField = FeatureClass.FindField(EndHeightFieldName);
                return _idxEndHeightField;
            }
            set { _idxEndHeightField = value; }
        }

        public int IdxGXLXField
        {
            get
            {
                if (_idxGxlxField == -1 && FeatureClass != null)
                    _idxGxlxField = FeatureClass.FindField(GXLXFieldName);
                return _idxGxlxField;
            }
            set { _idxGxlxField = value; }
        }

        public int IdxUniqueField
        {
            get
            {
                if (_idxUniqueField == -1 && FeatureClass != null && !string.IsNullOrEmpty(UniqueFieldName))
                    _idxUniqueField = FeatureClass.FindField(UniqueFieldName);
                return _idxUniqueField;
            }
            set { _idxUniqueField = value; }
        }

        public int IdxFilterField
        {
            get
            {
                if (_idxFilterField == -1 && FeatureClass != null && !string.IsNullOrEmpty(FilterFieldName))
                    _idxFilterField = FeatureClass.FindField(FilterFieldName);
                return _idxFilterField;
            }
            set { _idxFilterField = value; }
        }

        #endregion

        #endregion

        private string _aname;
        private bool _isComplete = false;
        private int _num = 0;
        private int _featureCount = -1;
        private IFeatureClass _featureClass;

        public string AName
        {
            get { return _aname; }
            set { _aname = value; }
        }

        public IFeatureClass FeatureClass
        {
            get { return _featureClass; }
            set
            {
                _num = 0;
                _featureCount = -1;
                _featureClass = value;
            }
        }

        public bool AddZValue { get; set; }

        public override string ToString()
        {
            return AName;
        }
        
        public bool UseAttributeValue { get; set; }

        public string GXLXCode { get; set; }

        public CustomFeatureClass RelateFeatureClass { get; set; }

        public List<CustomField> CustomFields { get; set; }

        public string[] FilterList { get; set; } 
        
        public bool IsComplete
        {
            get { return _isComplete; }
            set { _isComplete = value; }
        }

        public int Num
        {
            get { return _num; }
            set { _num = value; }
        }

        public int FeatureCount
        {
            get
            {
                if (_featureCount == -1 && FeatureClass != null)
                    _featureCount = FeatureClass.FeatureCount(null);
                return _featureCount;
            }
            set { _featureCount = value; }
        }
    }
}
