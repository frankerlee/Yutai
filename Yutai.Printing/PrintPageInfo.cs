using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing
{
    public class PrintPageInfo : IPrintPageInfo
    {
        private Dictionary<string, string> _autoElements=new Dictionary<string, string>();
        private IGeometry _boundary;
        public int PageID { get; set; }
        public string PageName { get; set; }

        public IGeometry Boundary
        {
            get { return _boundary; }
            set { _boundary = value; }
        }

        public bool IsClip { get; set; }

        public Dictionary<string, string> AutoElements
        {
            get { return _autoElements; }
            set { _autoElements = value; }
        }


        public int TotalCount { get; set; }
        public double Angle { get; set; }
        public double Scale { get; set; }

        public void Load(IFeature pFeature, string nameField)
        {
            _boundary = pFeature.Shape;
            for (int i = 0; i < pFeature.Fields.FieldCount; i++)
            {
                IField pField = pFeature.Fields.Field[i];
                if (pField.Type == esriFieldType.esriFieldTypeGeometry) continue;
                if (pField.Name.Equals(nameField) || pField.AliasName.Equals(nameField))
                {
                    PageName = pFeature.Value[i].ToString();
                }
                if (pFeature.Value[i] == DBNull.Value)
                {
                    _autoElements.Add(pField.AliasName, "");
                }
                else
                {
                    _autoElements.Add(pField.AliasName, pFeature.Value[i].ToString());
                }
            }
        }
    }

}
