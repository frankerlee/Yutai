using System.Collections.Generic;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtenders
{
    internal class Floaties : List<IFloaty>
    {
        public IFloaty Find(Control control_0)
        {
            foreach (Floaty floaty in this)
            {
                if (floaty.DockState.Container.Equals(control_0))
                {
                    return floaty;
                }
            }
            return null;
        }
    }
}