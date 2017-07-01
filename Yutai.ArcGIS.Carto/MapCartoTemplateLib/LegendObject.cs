using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    internal class LegendObject
    {
        internal static bool ApplyOk;
        internal static bool InitOK;
        internal static ILegend m_pLegend;

        static LegendObject()
        {
            old_acctor_mc();
        }

        private static void old_acctor_mc()
        {
            m_pLegend = null;
            ApplyOk = false;
            InitOK = false;
        }
    }
}