using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Identifer.Query;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Identifer.Commands
{
    class CmdSetSelectableLayer:YutaiCommand
    {
        public CmdSetSelectableLayer(IAppContext context)
        {
            OnCreate(context);
        }
        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }
        public override void OnClick()
        {
            frmSetSelectableLayer queryBuilder = new frmSetSelectableLayer()
            {
                FocusMap = _context.MapControl.Map as IMap
            };
            queryBuilder.ShowDialog();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "设置可选图层";
            base.m_category = "Query";
            base.m_bitmap = Properties.Resources.icon_set_selectable;
            base.m_name = "Query.Setting.SetSelectableLayer";
            base._key = "Query.Setting.SetSelectableLayer";
            base.m_toolTip = "设置可选图层";
            base.m_checked = false;
            base.m_enabled = true;
            base.DisplayStyleYT= DisplayStyleYT.ImageAndText;
            base.TextImageRelationYT= TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT= ToolStripItemImageScalingYT.None;
            base._itemType = RibbonItemType.Button;
        }
    }
}
