using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.UI.Menu;

namespace Yutai.Menu
{
    internal class MenuUpdater : MenuServiceBase
    {
        private readonly IMap  _map;
        private readonly IActiveView _activeView;
        private List<string> _commandKeys;

        public MenuUpdater(IAppContext context, PluginIdentity identity,List<string> commandKeys )
            : base(context, identity)
        {
            _map = context.MapControl.Map;
            _activeView = context.MapControl.Map as IActiveView;
            _commandKeys = commandKeys;
        }

        public void Update(bool rendered)
        {
            //UpdateToolbars(rendered);

            UpdateMenu();
        }

        private void UpdateMenu()
        {
            _context.RibbonMenu.UpdateMenu();
           
            //foreach(var key in _commandKeys)
            //{
            //    IRibbonMenuItem item=_context.RibbonMenu.FindItem(key);
            //    if (item == null) continue;
            //    item.ToolStripItem.Enabled = ((YutaiCommand) item.Item).Enabled;
            //    if (item.ToolStripItem is ToolStripButton && item.Item.ItemType== RibbonItemType.Tool)
            //    {
            //        ((ToolStripButton) item.ToolStripItem).Checked = item.Key == _context.CurrentToolName;
            //    }
            //}
            //var layer = _legend.Layers.Current;
            //FindMenuItem(MenuKeys.RemoveLayer).Enabled = layer != null;
            //FindMenuItem(MenuKeys.LayerClearSelection).Enabled = layer != null && layer.IsVector;
            //FindMenuItem(MenuKeys.ClearLayers).Enabled = _map.Layers.Any();
            //FindMenuItem(MenuKeys.ClearSelection).Enabled = _map.Layers.Any();
            //FindMenuItem(MenuKeys.ZoomToLayer).Enabled = _map.Layers.Any();
            //FindMenuItem(MenuKeys.ZoomToBaseLayer).Enabled = !_map.Projection.IsEmpty && _map.Tiles.Visible;

            //var config = AppConfig.Instance;
            //FindMenuItem(MenuKeys.ShowCoordinates).Checked = config.ShowCoordinates;
            //FindMenuItem(MenuKeys.ShowScalebar).Checked = config.ShowScalebar;
            //FindMenuItem(MenuKeys.ShowZoombar).Checked = config.ShowZoombar;
            //FindMenuItem(MenuKeys.ShowRedrawTime).Checked = config.ShowRedrawTime;

            //FindMenuItem(MenuKeys.ZoomNext).Enabled = _map.ExtentHistoryUndoCount > 0;
            //FindMenuItem(MenuKeys.ZoomPrev).Enabled = _map.ExtentHistoryRedoCount > 0;

            //需要增加对按钮的状态控制
        }

        private void UpdateToolbars(bool rendered)
        {
            // mapControl plays the role of the model here
            //FindToolbarItem(MenuKeys.ZoomIn).Checked = _map.MapCursor == MapCursor.ZoomIn;
            //FindToolbarItem(MenuKeys.ZoomOut).Checked = _map.MapCursor == MapCursor.ZoomOut;
            //FindToolbarItem(MenuKeys.Pan).Checked = _map.MapCursor == MapCursor.Pan;

            //FindToolbarItem(MenuKeys.ZoomPrev).Enabled = _map.ExtentHistoryUndoCount > 0;
            //FindToolbarItem(MenuKeys.ZoomNext).Enabled = _map.ExtentHistoryRedoCount > 0;

            //bool selection = _map.MapCursor == MapCursor.Selection && !_map.IsCustomSelectionMode;
            //FindToolbarItem(MenuKeys.SelectByRectangle).Checked = selection;
            //FindToolbarItem(MenuKeys.SelectByPolygon).Checked = _map.MapCursor == MapCursor.SelectByPolygon;

            //bool selectionCursor = selection || _map.MapCursor == MapCursor.SelectByPolygon;
            //FindToolbarItem(MenuKeys.SelectDropDown).Checked = selectionCursor;

            //bool distance = _map.Measuring.Type == MeasuringType.Distance;
            //FindToolbarItem(MenuKeys.MeasureArea).Checked = _map.MapCursor == MapCursor.Measure && !distance;
            //FindToolbarItem(MenuKeys.MeasureDistance).Checked = _map.MapCursor == MapCursor.Measure && distance;

            //FindToolbarItem(MenuKeys.FindLocation).Enabled = !_map.Projection.IsEmpty;

            //bool hasFeatureSet = false;

            //bool hasLayer = _context.Legend.SelectedLayerHandle != -1;
            //if (hasLayer)
            //{
            //    var fs = _context.Map.GetFeatureSet(_context.Legend.SelectedLayerHandle);
            //    if (fs != null)
            //    {
            //        FindToolbarItem(MenuKeys.ClearSelection).Enabled = fs.NumSelected > 0;
            //        FindToolbarItem(MenuKeys.ZoomToSelected).Enabled = fs.NumSelected > 0;
            //        hasFeatureSet = true;
            //    }
            //}

            //if (!hasFeatureSet)
            //{
            //    FindToolbarItem(MenuKeys.ClearSelection).Enabled = false;
            //    FindToolbarItem(MenuKeys.ZoomToSelected).Enabled = false;
            //}

            //FindToolbarItem(MenuKeys.RemoveLayer).Enabled = hasLayer;

            //toolSearch.Enabled = true;
            //toolSearch.Text = "Find location";
            //if (App.Map.Count > 0 && !App.Map.Measuring.IsUsingEllipsoid)
            //{
            //    toolSearch.Enabled = false;
            //    toolSearch.Text = "Unsupported projection. Search isn't allowed.";
            //}
        }
    }
}
