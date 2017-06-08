using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.UI.Controls
{
    public partial class DockPanelControlBase : UserControl, IDockPanelView
    {
        private bool _shown = false;
     

        public DockPanelControlBase()
        {
            Load += (s, e) => _shown = true;
        }

        public bool IsDockVisible
        {
            get
            {
                // Visible property returns true before it was shown for the first time
                if (!_shown)
                {
                    return false;
                }

                return Visible;
            }
        }
        public int TabPosition { get; set; }
       
        public virtual void SetFocus()
        {
            Focus();
        }

        public virtual string DockName { get;  }
        public virtual Bitmap Image { get; }
        public virtual string Caption { get; set; }
        public virtual Size DefaultSize { get {return new Size(100,150);} }
        public virtual DockPanelState DefaultDock { get {return DockPanelState.None;} }
        public virtual string DefaultNestDockName { get; }
    }
}
