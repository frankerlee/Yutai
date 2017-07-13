using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Pipeline.Editor.Classes
{
    class CustomPoint
    {
        private string _no;
        private IPoint _point;

        public string No
        {
            get { return _no; }
            set { _no = value; }
        }

        public IPoint Point
        {
            get { return _point; }
            set { _point = value; }
        }
    }
}
