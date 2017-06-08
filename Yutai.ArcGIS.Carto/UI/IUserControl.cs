using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    internal interface IUserControl
    {
        void Apply();

        ILayer CurrentLayer { set; }

        bool Visible { get; set; }
    }
}

