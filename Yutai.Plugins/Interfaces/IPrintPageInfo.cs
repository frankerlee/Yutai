using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Plugins.Interfaces
{
    public interface IPrintPageInfo
    {
        Dictionary<string, string> AutoElements { get; set; }
        IGeometry Boundary { get; set; }
        bool IsClip { get; set; }
        int PageID { get; set; }
        string PageName { get; set; }
        int TotalCount { get; set; }
        double Angle { get; set; }
        double Scale { get; set; }
        void Load(IFeature pFeature, string nameField);
    }
}