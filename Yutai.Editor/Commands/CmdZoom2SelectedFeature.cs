using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Forms;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    class CmdZoom2SelectedFeature:YutaiCommand
    {
        public CmdZoom2SelectedFeature(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_edit_zoom2selection;
            this.m_caption = "缩放到选中要素";
            this.m_category = "Edit";
            this.m_message = "缩放到选中要素";
            this.m_name = "Edit_Zoom2Selection";
            this._key = "Edit_Zoom2Selection";
            this.m_toolTip = "缩放到选中要素";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get
            {
                return _context.FocusMap != null && _context.FocusMap.SelectionCount > 0;
            }
        }




        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            Yutai.ArcGIS.Common.Helpers.CommonHelper.Zoom2SelectedFeature(_context.ActiveView as IActiveView);
        }
        
    }

    class CmdDeleteSelectedFeature : YutaiCommand
    {
        public CmdDeleteSelectedFeature(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_edit_delete;
            this.m_caption = "删除要素";
            this.m_category = "Edit";
            this.m_message = "删除要素";
            this.m_name = "Edit_DeleteSelection";
            this._key = "Edit_DeleteSelection";
            this.m_toolTip = "删除要素";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get
            {
                return _context.FocusMap != null && _context.FocusMap.SelectionCount > 0;
            }
        }




        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            Yutai.ArcGIS.Common.Editor.Editor.DeletedSelectedFeatures(_context.FocusMap, Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace as IWorkspace);
        }

    }


    class CmdClearSelectedFeature : YutaiCommand
    {
        public CmdClearSelectedFeature(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_edit_clearselection;
            this.m_caption = "清除选中要素";
            this.m_category = "Edit";
            this.m_message = "清除选中要素";
            this.m_name = "Edit_ClearSelection";
            this._key = "Edit_ClearSelection";
            this.m_toolTip = "清除选中要素";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get
            {
                return _context.FocusMap != null && _context.FocusMap.SelectionCount > 0;
            }
        }




        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            (_context.FocusMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, false, null);
            _context.FocusMap.ClearSelection();
            (_context.FocusMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, false, null);
        }

    }
}
