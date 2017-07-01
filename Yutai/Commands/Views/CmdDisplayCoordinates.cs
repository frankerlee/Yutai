using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Shared;

namespace Yutai.Commands.Views
{
    public class CmdDisplayCoordinates : YutaiCommand
    {
        private bool _isDisplay = false;

        public CmdDisplayCoordinates(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            _isDisplay = !_isDisplay;
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            m_caption = "";
            m_bitmap = Properties.Resources.icon_coordinate;
            m_name = "Status_Coordinates";
            _key = "Status_Coordinates";
            m_category = "状态栏";
            m_toolTip = "显示图形窗口当前坐标,点击可以切换显示状态";
            m_message = "";
            _itemType = RibbonItemType.Label;
            IMapControl2 map = _context.MapControl as IMapControl2;
            IMapControlEvents2_Event mapEvents = map as IMapControlEvents2_Event;
            _isDisplay = true;
            if (map == null)
            {
                throw new InvalidCastException("Map must implement IMap interface");
            }

            mapEvents.OnMouseMove += MapEvents_OnMouseMove;
        }

        private void MapEvents_OnMouseMove(int button, int shift, int x, int y, double mapx, double mapy)
        {
            if (!_isDisplay) return;
            string coords = string.Format("X={0:#.####},Y={1:#.####}", mapx, mapy);
            _context.RibbonMenu.SetStatusValue(m_name, coords);
        }
    }

    public class CmdDisplayScale : YutaiCommand
    {
        private bool _isDisplay = false;

        public CmdDisplayScale(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            _isDisplay = !_isDisplay;
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            m_caption = "";
            m_bitmap = Properties.Resources.icon_scale_small;
            m_name = "Status_Scale";
            _key = "Status_Scale";
            m_category = "状态栏";
            m_toolTip = "显示图形窗口当前比例";
            m_message = "";
            _itemType = RibbonItemType.Label;
            IMapControl2 map = _context.MapControl as IMapControl2;
            IMapControlEvents2_Event mapEvents = map as IMapControlEvents2_Event;
            _isDisplay = true;
            if (map == null)
            {
                throw new InvalidCastException("Map must implement IMap interface");
            }

            mapEvents.OnExtentUpdated += MapEvents_OnExtentUpdated;
        }

        private void MapEvents_OnExtentUpdated(object displaytransformation, bool sizechanged, object newenvelope)
        {
            IMap pMap = _context.MapControl.Map as IMap;
            double scale = pMap.MapScale;
            string format = scale <= Int32.MaxValue ? "f0" : "e4";
            _context.RibbonMenu.SetStatusValue(m_name, string.Format("1:{0}", scale.ToString(format)));
        }
    }

    public class CmdDisplayMsg : YutaiCommand
    {
        private bool _isDisplay = false;

        public CmdDisplayMsg(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            _isDisplay = !_isDisplay;
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            m_caption = "";
            m_bitmap = Properties.Resources.icon_message_small;
            m_name = "Status_Message";
            _key = "Status_Message";
            m_category = "状态栏";
            m_toolTip = "显示提示信息";
            m_message = "显示提示信息";
            _isDisplay = true;
            _itemType = RibbonItemType.Label;
        }
    }

    public class CmdDisplayUnits : YutaiCommand
    {
        private bool _isDisplay = false;

        public CmdDisplayUnits(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            _isDisplay = !_isDisplay;
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            m_caption = "";
            m_bitmap = Properties.Resources.icon_units_small;
            m_name = "Status_Units";
            _key = "Status_Units";
            m_category = "状态栏";
            m_toolTip = "显示图形窗口当前单位";
            m_message = "";
            _itemType = RibbonItemType.Label;
            IMapControl2 map = _context.MapControl as IMapControl2;
            IMapControlEvents2_Event mapEvents = map as IMapControlEvents2_Event;
            _isDisplay = true;
            if (map == null)
            {
                throw new InvalidCastException("Map must implement IMap interface");
            }

            mapEvents.OnMapReplaced += MapEventsOnOnMapReplaced;
        }

        private void MapEventsOnOnMapReplaced(object newMap)
        {
            string format = MapUnitsHelper.GetMapUnitsDesc((int) _context.MapControl.Map.MapUnits);
            if (_context.RibbonMenu == null) return;
            _context.RibbonMenu.SetStatusValue(m_name, format);
        }
    }

    public class CmdDisplaySelection : YutaiCommand
    {
        private bool _isDisplay = false;

        public CmdDisplaySelection(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            _isDisplay = !_isDisplay;
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            m_caption = "";
            m_bitmap = Properties.Resources.icon_selection_small;
            m_name = "Status_SelectionCount";
            _key = "Status_SelectionCount";
            m_category = "状态栏";
            m_toolTip = "显示图形窗口当前选择个数,点击可以切换显示状态";
            m_message = "";
            _itemType = RibbonItemType.Label;
            IMapControl2 map = _context.MapControl as IMapControl2;
            IMapControlEvents2_Event mapEvents = map as IMapControlEvents2_Event;
            _isDisplay = true;
            if (map == null)
            {
                throw new InvalidCastException("Map must implement IMap interface");
            }

            mapEvents.OnSelectionChanged += MapEventsOnOnSelectionChanged;
        }

        private void MapEventsOnOnSelectionChanged()
        {
            if (!_isDisplay) return;
            IMap pMap = _context.MapControl.Map as IMap;
            _context.RibbonMenu.SetStatusValue(m_name, "选择个数:" + pMap.SelectionCount.ToString());
        }

        private void MapEvents_OnMouseMove(int button, int shift, int x, int y, double mapx, double mapy)
        {
            string coords = string.Format("X={0:#.####},Y={1:#.####}", mapx, mapy);
            _context.RibbonMenu.SetStatusValue(m_name, coords);
        }
    }
}