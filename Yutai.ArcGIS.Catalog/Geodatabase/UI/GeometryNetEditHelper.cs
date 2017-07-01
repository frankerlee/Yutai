using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal class GeometryNetEditHelper
    {
        internal static GeometryNetEditHelper GNEditHelper;
        private IFeatureDataset ifeatureDataset_0 = null;
        private IGeometricNetwork igeometricNetwork_0 = null;

        static GeometryNetEditHelper()
        {
            old_acctor_mc();
        }

        internal static void Init()
        {
            GNEditHelper = new GeometryNetEditHelper();
        }

        private static void old_acctor_mc()
        {
            GNEditHelper = null;
        }

        internal IGeometricNetwork GeometricNetwork
        {
            get { return this.igeometricNetwork_0; }
            set { this.igeometricNetwork_0 = value; }
        }
    }
}