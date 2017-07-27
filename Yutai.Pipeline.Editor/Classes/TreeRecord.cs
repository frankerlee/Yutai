using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Pipeline.Editor.Classes
{
    public class TreeRecord
    {
        private bool _isDomain;
        private string _fName;
        private object _doaminValue;
        private int _id;
        private esriFieldType _fieldType;
        private int _fieldID;

        public TreeRecord()
        {

        }
        public TreeRecord(bool isDomain, esriFieldType fieldType, string fName, object fValue, object domainValue, int fieldID, int id, object secondValue, object mergeValue)
        {
            _isDomain = isDomain;
            _fName = fName;
            _firstvalue = fValue;
            _doaminValue = domainValue;
            _id = id;
            _fieldType = fieldType;
            _fieldID = fieldID;
            _secondValue = secondValue;
            _mergeValue = mergeValue;
        }

        public bool IsDomain
        {
            get { return _isDomain; }
            set { _isDomain = value; }
        }

        public string FName
        {
            get { return _fName; }
            set { _fName = value; }
        }


        private object _firstvalue;

        public object FirstValue
        {
            get { return _firstvalue; }
            set { _firstvalue = value; }
        }

        private object _secondValue;

        public object SecondValue
        {
            get { return _secondValue; }
            set { _secondValue = value; }
        }

        private object _mergeValue;

        public object MergeValue
        {
            get { return _mergeValue; }
            set { _mergeValue = value; }
        }


        public object DoaminValue
        {
            get { return _doaminValue; }
            set { _doaminValue = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public esriFieldType FieldType
        {
            get { return _fieldType; }
            set { _fieldType = value; }
        }

        public int FieldId
        {
            get { return _fieldID; }
            set { _fieldID = value; }
        }
    }
}
