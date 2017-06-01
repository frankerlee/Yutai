using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Plugins.Enums;

namespace Yutai.Plugins.Concrete
{
    public class RibbonTabItem:YutaiMenuCommand
    {
        public RibbonTabItem(string category, string key, string name, string caption, string tooltip) : base(category, key, name, caption, tooltip)
        {
        }

        public RibbonTabItem(RibbonItemType itemType, string category, string key, string name, string caption, string tooltip, string message) : base(itemType, category, key, name, caption, tooltip, message)
        {
        }

        public RibbonTabItem(Bitmap bitmap, string caption, string category, int helpContextId, string helpFile, string message, string name, string toolTip) : base(bitmap, caption, category, helpContextId, helpFile, message, name, toolTip)
        {
        }
    }
}
