using System.Windows.Forms;

namespace Yutai.ArcGIS.Common
{
    public class BuildPyramidSet
    {
        public static DialogResult Show()
        {
            frmPyramidSet set = new frmPyramidSet();
            return set.ShowDialog();
        }
    }
}

