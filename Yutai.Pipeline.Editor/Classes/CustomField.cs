using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Pipeline.Editor.Classes
{
    public class CustomField
    {
        private string _aliasName;
        private string _name;
        private int _index;
        private string _dName;
        private esriFieldType _type;
        private int _dIndex;

        public string AliasName
        {
            get { return _aliasName; }
            set { _aliasName = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        public string DName
        {
            get { return _dName; }
            set { _dName = value; }
        }

        public int DIndex
        {
            get { return _dIndex; }
            set { _dIndex = value; }
        }

        public esriFieldType Type
        {
            get { return _type; }
            set { _type = value; }
        }
    }
}
