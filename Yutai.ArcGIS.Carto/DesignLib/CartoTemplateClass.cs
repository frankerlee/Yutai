namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class CartoTemplateClass
    {
        private static string m_Description;
        private static double m_Height;
        private static double m_InOutDist;
        private static string m_LegendInfo;
        private static string m_Name;
        private static double m_OutBorderWidth;
        private static double m_Scale;
        private static double m_StartCoodinateMultiple;
        private static double m_TitleDist;
        private static string m_TuKuoInfo;
        private static double m_Width;
        private static double m_XInterval;
        private static double m_YInterval;

        static CartoTemplateClass()
        {
            old_acctor_mc();
        }

        private static void old_acctor_mc()
        {
            m_Name = "";
            m_Description = "";
            m_TuKuoInfo = "";
            m_InOutDist = 0.1;
            m_TitleDist = 0.1;
            m_XInterval = 100.0;
            m_YInterval = 100.0;
            m_OutBorderWidth = 0.1;
            m_StartCoodinateMultiple = 10.0;
            m_LegendInfo = "";
            m_Scale = 0.0;
            m_Width = 50.0;
            m_Height = 50.0;
        }

        public static void Reset()
        {
            m_Name = "";
            m_Description = "";
            m_TuKuoInfo = "";
            m_Scale = 0.0;
            m_Width = 50.0;
            m_Height = 50.0;
            m_InOutDist = 0.1;
            m_TitleDist = 0.1;
            m_XInterval = 100.0;
            m_YInterval = 100.0;
            m_OutBorderWidth = 0.1;
            m_StartCoodinateMultiple = 10.0;
            m_LegendInfo = "";
        }

        public static string Description
        {
            get
            {
                return m_Description;
            }
            set
            {
                m_Description = value;
            }
        }

        public static double Height
        {
            get
            {
                return m_Height;
            }
            set
            {
                m_Height = value;
            }
        }

        public static double InOutDist
        {
            get
            {
                return m_InOutDist;
            }
            set
            {
                m_InOutDist = value;
            }
        }

        public static string LegendInfo
        {
            get
            {
                return m_LegendInfo;
            }
            set
            {
                m_LegendInfo = value;
            }
        }

        public static string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        public static double OutBorderWidth
        {
            get
            {
                return m_OutBorderWidth;
            }
            set
            {
                m_OutBorderWidth = value;
            }
        }

        public static double Scale
        {
            get
            {
                return m_Scale;
            }
            set
            {
                m_Scale = value;
            }
        }

        public static double StartCoodinateMultiple
        {
            get
            {
                return m_StartCoodinateMultiple;
            }
            set
            {
                m_StartCoodinateMultiple = value;
            }
        }

        public static double TitleDist
        {
            get
            {
                return m_TitleDist;
            }
            set
            {
                m_TitleDist = value;
            }
        }

        public static string TuKuoInfo
        {
            get
            {
                return m_TuKuoInfo;
            }
            set
            {
                m_TuKuoInfo = value;
            }
        }

        public static double Width
        {
            get
            {
                return m_Width;
            }
            set
            {
                m_Width = value;
            }
        }

        public static double XInterval
        {
            get
            {
                return m_XInterval;
            }
            set
            {
                m_XInterval = value;
            }
        }

        public static double YInterval
        {
            get
            {
                return m_YInterval;
            }
            set
            {
                m_YInterval = value;
            }
        }
    }
}

