using ESRI.ArcGIS.Carto;

namespace Yutai.Plugins.Editor
{
    public class EditorSettings
    {
        public EditorSettings()
        {
            SelectionEnvironment=new SelectionEnvironment();
        }

        private double QueryTorelance { get; set; }
        public ISelectionEnvironment SelectionEnvironment { get; set; }
        public IFeatureLayer CurrentLayer { get; set; }
    }
}
