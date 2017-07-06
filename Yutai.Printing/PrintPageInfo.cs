using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Carto.MapCartoTemplateLib;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing
{
    [DefaultProperty("PageName")]
    public class PrintPageInfo : IPrintPageInfo
    {
        private List<PrintPageElement> _autoElements=new List<PrintPageElement>();
        private IGeometry _boundary;

        [CategoryAttribute("基本信息"),ReadOnly(true), DisplayName("页数")]
        public int PageID { get; set; }
        [CategoryAttribute("基本信息"),DisplayName("名称")]
        public string PageName { get; set; }
       [Browsable(false)]
        public IGeometry Boundary
        {
            get { return _boundary; }
            set { _boundary = value; }
        }
        [CategoryAttribute("基本信息"), DisplayName("是否遮挡"),Description("如果选择遮挡，本页面范围外的图形将不会打印")]
        public bool IsClip { get; set; }

      

        [CategoryAttribute("基本信息"), ReadOnly(true), DisplayName("总页数")]
        public int TotalCount { get; set; }
        [CategoryAttribute("基本信息"), ReadOnly(true), DisplayName("旋转角度")]
        public double Angle { get; set; }
        [CategoryAttribute("基本信息"), ReadOnly(true), DisplayName("比例尺")]
        public double Scale { get; set; }

        [CategoryAttribute("图幅信息"), DisplayName("图幅信息")]
        public List<PrintPageElement> AutoElements
        {
            get { return _autoElements; }

            set { _autoElements = value; }
        }

        public void Load(IFeature pFeature, string nameField)
        {
            _boundary = pFeature.Shape;
            string pName = FeatureHelper.GetFeatureStringValue(pFeature, nameField);
            PageName = pName;
            _autoElements.Clear();
            for (int i = 0; i < pFeature.Fields.FieldCount; i++)
            {
                IField pField = pFeature.Fields.Field[i];
                if (pField.Type == esriFieldType.esriFieldTypeGeometry) continue;
                
               string pValue = FeatureHelper.GetFeatureStringValue(pFeature, pField.Name);
                _autoElements.Add(new PrintPageElement(pField.Name,pField.AliasName, pValue));
            }
        }
    }

    

}
