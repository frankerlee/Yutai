using System;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Menu;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    class CmdViewGeometryInfo : YutaiCommand
    {
        private GeometryInfoDockPanelService _dockService;
        public CmdViewGeometryInfo(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_sketch_direction;
            this.m_caption = "几何数据信息";
            this.m_category = "Edit";
            this.m_message = "几何数据信息";
            this.m_name = "Edit_ViewGeometryInfo";
            this._key = "Edit_ViewGeometryInfo";
            this.m_toolTip = "几何数据信息";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageBeforeText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get
            {
                bool result;
                if (_context.FocusMap == null)
                {
                    result = false;
                }
                else if (_context.FocusMap.LayerCount == 0)
                {
                    if(_dockService !=null) _dockService.Hide();
                    result = false;
                }
                else if (Yutai.ArcGIS.Common.Editor.Editor.EditMap != null && Yutai.ArcGIS.Common.Editor.Editor.EditMap != _context.FocusMap)
                {
                    if (_dockService != null) _dockService.Hide();
                    result = false;
                }
                else if (Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace != null)
                {
                    result = true;
                }
                else
                {
                    if (_dockService != null) _dockService.Hide();
                    result = false;
                }
                return result;
            }
        }




        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            try
            {
                if (_dockService == null)
                    _dockService = _context.Container.GetInstance<GeometryInfoDockPanelService>();
                this._dockService.Presenter.EditWorkspace = Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace as IWorkspace;
              
              
                if (_dockService.Visible == false)
                {
                    _dockService.Show();
                    return;
                }
              
            }
            catch (Exception exception_)
            {
                CErrorLog.writeErrorLog(this, exception_, "");
            }
        }
    }
}