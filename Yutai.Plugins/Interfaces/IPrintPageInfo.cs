using System.Collections.Generic;
using System.ComponentModel;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Concrete;

namespace Yutai.Plugins.Interfaces
{
    
    public interface IPrintPageInfo
    {
        List<PrintPageElement> AutoElements { get; set; }
        IGeometry Boundary { get; set; }
        bool IsClip { get; set; }
        int PageID { get; set; }
        string PageName { get; set; }
        int TotalCount { get; set; }
        double Angle { get; set; }
        double Scale { get; set; }
        void Load(IFeature pFeature, string nameField);
    }

    public interface IPrintPageElement
    {
        string Name { get; set; }
        string AliasName { get; set; }
        string Value { get; set; }
    }
}