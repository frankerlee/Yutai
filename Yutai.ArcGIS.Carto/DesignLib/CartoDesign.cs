using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class CartoDesign
    {
        private static IRow m_pRow;

        static CartoDesign()
        {
            old_acctor_mc();
        }

        private static void old_acctor_mc()
        {
            m_pRow = null;
        }

        public static IRow Row
        {
            get
            {
                return m_pRow;
            }
            set
            {
                m_pRow = value;
            }
        }
    }
}

