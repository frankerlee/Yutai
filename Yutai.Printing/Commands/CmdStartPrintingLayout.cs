using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public class CmdStartLayout : YutaiCommand
    {
        private PrintingPlugin _plugin;
       
      

        static CmdStartLayout()
        {
           
        }

        public CmdStartLayout(IAppContext context, BasePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin as PrintingPlugin;
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_layout_start;
            this.m_caption = "启动制图";
            this.m_category = "Layout";
            this.m_message = "启动制图";
            this.m_name = "Layout_StartLayout";
            this._key = "Layout_StartLayout";
            this.m_toolTip = "启动制图";
            _context = hook as IAppContext;
            this._itemType= RibbonItemType.Button;;
            _needUpdateEvent = true;
        }
        
        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

      

        public override void OnClick()
        {
           _context.MainView.ActivatePageLayout();
        }
    }
}
