using System.Collections.Generic;
using Yutai.Plugins.Scene.Forms;

namespace Yutai.Plugins.Scene.Helpers
{
    internal class ViewersManager
    {
        private static List<frmSecondaryViewer> m_pList;

        public static List<frmSecondaryViewer> ViewerList
        {
            get
            {
                return ViewersManager.m_pList;
            }
        }

        static ViewersManager()
        {
            // 注意: 此类型已标记为 'beforefieldinit'.
            ViewersManager.old_acctor_mc();
        }

        private static void old_acctor_mc()
        {
            ViewersManager.m_pList = new List<frmSecondaryViewer>();
        }
    }
}