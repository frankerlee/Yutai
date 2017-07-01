using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Concrete
{
    public class YutaiMenuCommand : YutaiCommand
    {
        public override void OnClick(object sender, EventArgs args)
        {
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
        }

        public YutaiMenuCommand(string category, string key, string name, string caption, string tooltip)
        {
            this.m_name = name;
            this.m_caption = caption;
            this.m_category = category;
            this.m_toolTip = tooltip;
            this._key = key;
            InitType();
        }

        public YutaiMenuCommand(RibbonItemType itemType, string category, string key, string name, string caption,
            string tooltip, string message)
        {
            this.m_name = name;
            this.m_message = message;
            this.m_caption = caption;
            this.m_category = category;
            this.m_toolTip = tooltip;
            this._key = key;
            this._itemType = itemType;
        }


        public YutaiMenuCommand(System.Drawing.Bitmap bitmap, string caption, string category, int helpContextId,
            string helpFile, string message, string name, string toolTip)
        {
            this.m_bitmap = bitmap;
            this.m_name = name;
            this.m_message = message;
            this.m_caption = caption;
            this.m_category = category;
            this.m_toolTip = toolTip;
            this.m_helpID = helpContextId;
            this.m_helpFile = helpFile;
            InitType();
        }
    }
}