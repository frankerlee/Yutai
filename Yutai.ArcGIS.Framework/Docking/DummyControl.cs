using System.Windows.Forms;

namespace Yutai.ArcGIS.Framework.Docking
{
    internal class DummyControl : Control
    {
        public DummyControl()
        {
            base.SetStyle(ControlStyles.Selectable, false);
        }
    }
}

