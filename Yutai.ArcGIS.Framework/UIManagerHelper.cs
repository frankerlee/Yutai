using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Framework
{
    public class UIManagerHelper
    {
        private static IBarManager m_BarManager;
        private static IDockManagerWrap m_pDockManagerWrap;

        static UIManagerHelper()
        {
            old_acctor_mc();
        }

        public static void DockWindows(object object_0, Form form_0, Bitmap bitmap_0)
        {
            if (m_pDockManagerWrap != null)
            {
                m_pDockManagerWrap.DockWindows(object_0, form_0, bitmap_0);
            }
        }

        public static void Message(MSGTYPE msgtype_0, object object_0)
        {
            if (m_BarManager != null)
            {
                m_BarManager.Message(msgtype_0, object_0);
            }
        }

        private static void old_acctor_mc()
        {
            m_pDockManagerWrap = null;
            m_BarManager = null;
        }

        public static IBarManager BarManager
        {
            get
            {
                return m_BarManager;
            }
            set
            {
                m_BarManager = value;
            }
        }

        public static IDockManagerWrap DockManagerWrap
        {
            get
            {
                return m_pDockManagerWrap;
            }
            set
            {
                m_pDockManagerWrap = value;
            }
        }
    }
}

