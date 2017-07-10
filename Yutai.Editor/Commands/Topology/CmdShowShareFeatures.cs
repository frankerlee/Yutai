using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;
using System.Windows.Forms;
using Yutai.Plugins.Editor.Forms;
using Yutai.Plugins.Enums;

namespace Yutai.Plugins.Editor.Commands.Topology
{
    public class CmdShowShareFeatures : YutaiCommand
    {
        public override bool Enabled
        {
            get
            {
                return this._context.FocusMap != null && this._context.FocusMap.LayerCount != 0 && (!(this._context.Hook is IApplication) || (this._context.Hook as IApplication).CanEdited) && (Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace != null && CmdSelectTopology.m_TopologyGraph != null && CmdSelectTopology.m_TopologyGraph.SelectionParents.Count > 0);
            }
        }

        public CmdShowShareFeatures(IAppContext context)
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
            this.m_bitmap = Properties.Resources.ShowShareFeatures;
            this.m_name = "Editor_Topology_ShowShareFeatures";
            _key= "Editor_Topology_ShowShareFeatures";
            _itemType= RibbonItemType.Button;
            this.m_caption = "显示共享要素";
            this.m_category = "拓扑";
            this.m_message = "显示共享要素对话框";
            this.m_toolTip = "显示共享要素";
        }

        public override void OnClick()
        {
            frmShowShareFeature _frmShowShareFeature = new frmShowShareFeature()
            {
                TopologyGraph = CmdSelectTopology.m_TopologyGraph,
                FocusMap = this._context.FocusMap
            };
            _frmShowShareFeature.ShowDialog();
        }
    }
}