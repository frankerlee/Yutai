using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Identifer.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Identifer.Commands
{
    class CmdZoomToSelection:YutaiCommand
    {
        private IAppContext _context;
        public override bool Enabled
        {
            get
            {
                bool flag;
                flag = (this._context.MapControl.Map == null || this._context.MapControl.Map.SelectionCount <= 0 ? false : true);
                return flag;
            }
        }

        public CmdZoomToSelection(IAppContext context)
        {
            this.m_bitmap = Properties.Resources.ZoomSelection;
            this.m_caption = "缩放到选中要素";
            this.m_category = "Query";
            this.m_message = "缩放到选中要素";
            this.m_name = "Query.SelectionTools.ZoomToSelection";
            this._key = "Query.SelectionTools.ZoomToSelection";
            this.m_toolTip = "缩放到选中要素";
            _context = context;
        }

        public override void OnCreate(object hook)
        {
           
        }

        public override void OnClick()
        {
            CommonHelper.Zoom2SelectedFeature(this._context.MapControl.ActiveView);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }
    }
}
