using System;
using ESRI.ArcGIS.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands.Topology
{
    public class CmdClearSelectedTopologyElement : YutaiCommand
    {
        public override bool Enabled
        {
            get
            {
                bool flag;
                flag = (FixTopologyErrorTool.m_pTopoErroeSelection.Count <= 0 ? false : true);
                return flag;
            }
        }

        public CmdClearSelectedTopologyElement(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }


        public override void OnCreate(object object_0)
        {
            _context = object_0 as IAppContext;
            this.m_caption = "清除选中的拓扑元素";
            this.m_toolTip = "清除选中的拓扑元素";
            this.m_name = "Editor_Topology_ClearSelectedTopologyElement";
            _key = "Editor_Topology_ClearSelectedTopologyElement";
            _itemType= RibbonItemType.Button;
            this.m_category = "拓扑";
            this.m_bitmap =Properties.Resources.ClearSelectedTopologyElement;
        }

        public override void OnClick()
        {
            FixTopologyErrorTool.m_pTopoErroeSelection.Clear();
            (this._context.FocusMap as IActiveView).Refresh();
        }
    }
}