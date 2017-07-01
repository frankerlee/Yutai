namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal class ObjectClassShareData
    {
        public static bool m_IsInFeatureDataset;
        public static bool m_IsShapeFile;

        static ObjectClassShareData()
        {
            old_acctor_mc();
        }

        private static void old_acctor_mc()
        {
            m_IsShapeFile = false;
            m_IsInFeatureDataset = false;
        }
    }
}