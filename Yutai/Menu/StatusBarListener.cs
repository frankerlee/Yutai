using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using Yutai.Helper;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;
using Yutai.Properties;
using Yutai.Services.Serialization;
using Yutai.Shared;

namespace Yutai.Menu
{
    public class StatusBarListener
    {
        private readonly IAppContext _context;

        public StatusBarListener(IAppContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            _context = context;

           // InitStatusBar();

            //var appContext = context as AppContext;
            //if (appContext != null)
            //{
            //    appContext.Broadcaster.StatusItemClicked += PluginManager_MenuItemClicked;
            //}

            //AddMapEventHandlers();
        }

        private void AddMapEventHandlers()
        {
            IMapControl2 map = _context.MapControl as IMapControl2;
            IMapControlEvents2_Event mapEvents=map as IMapControlEvents2_Event;
            
            if (map == null)
            {
                throw new InvalidCastException("Map must implement IMap interface");
            }

            //mapEvents.OnExtentUpdated += MapEvents_OnExtentUpdated;
            //mapEvents.OnSelectionChanged += MapEvents_OnSelectionChanged;
            //mapEvents.OnMouseMove += MapEvents_OnMouseMove;

           
        }

        private void MapEvents_OnMouseMove(int button, int shift, int X, int Y, double mapX, double mapY)
        {
            var statusCoordinate = _context.StatusBar.FindItem(StatusBarKeys.XYCoordinates, Identity);
            statusCoordinate.Text = string.Format("X={0:#.####},Y={1:#.####}", mapX, mapY);
        }

        private void MapEvents_OnSelectionChanged()
        {
            var statusSelected = _context.StatusBar.FindItem(StatusBarKeys.SelectedCount, Identity);

            IMap pMap = _context.MapControl.Map as IMap;
            statusSelected.Text = "选择个数:"+pMap.SelectionCount.ToString();
        }

        private void MapEvents_OnExtentUpdated(object displayTransformation, bool sizeChanged, object newEnvelope)
        {
            var item = _context.StatusBar.FindItem(StatusBarKeys.MapScale, Identity);
            IMap pMap = _context.MapControl.Map as IMap;
            double scale = pMap.MapScale;
            string format = scale <= Int32.MaxValue ? "f0" : "e4";
            item.Text = string.Format("1:{0}", scale.ToString(format));
        }

        private void InitStatusBar()
        {
            var bar = _context.StatusBar;

            //var dropDown = bar.Items.AddSplitButton("Not defined", StatusBarKeys.ViewStyleDropDown, Identity);
            //dropDown.Icon = new MenuIcon(Resources.icon_crs_change);

            //var items = dropDown.SubItems;
            //items.AddButton("二维模式", StatusBarKeys.ViewStyle2D, Identity);
            //items.AddButton("三维模式", StatusBarKeys.ViewStyle3D, Identity);
            //items.AddButton("联动模式", StatusBarKeys.ViewStyleAll, Identity);
            //items.AddButton("视图设置", StatusBarKeys.ViewStyleConfig, Identity).BeginGroup = true; ;

            //dropDown.Update();

            //bar.Items.AddLabel("单位: ", StatusBarKeys.MapUnits, Identity).BeginGroup = true;
            //bar.Items.AddLabel("选择: ", StatusBarKeys.SelectedCount, Identity).BeginGroup = true;
            //bar.Items.AddLabel("坐标: ", StatusBarKeys.XYCoordinates, Identity).BeginGroup = true;

            //bar.AlignNewItemsRight = true;

            //bar.Items.AddLabel("", StatusBarKeys.MapScale, Identity);
            //bar.Items.AddLabel("底图", StatusBarKeys.TileProvider, Identity).BeginGroup = true;

            //var progressMsg = bar.Items.AddLabel("进程", StatusBarKeys.ProgressMsg, Identity);
            //progressMsg.BeginGroup = true;
            //progressMsg.Visible = false;

            //bar.Items.AddProgressBar(StatusBarKeys.ProgressBar, Identity).Visible = false;

            //bar.Update();
        }

        private void PluginManager_MenuItemClicked(object sender, MenuItemEventArgs e)
        {
            //var menuItem = sender as IMenuItem;
            //if (menuItem == null)
            //{
            //    throw new InvalidCastException("Invalid type of menu item. IMenuItem interface is expected");
            //}

            //switch (e.ItemKey)
            //{
            //    case StatusBarKeys.MapScale:
            //       // _context.Container.Run<SetScalePresenter>();
            //        break;
            //    //case StatusBarKeys.ProjectionDropDown:
            //    //    if (_context.Map.Projection.IsEmpty)
            //    //    {
            //    //        _context.ChangeProjection();
            //    //    }
            //    //    else
            //    //    {
            //    //        _context.ShowMapProjectionProperties();
            //    //    }
            //    //    break;
            //    case StatusBarKeys.ViewStyle2D:
            //        ((ISecureContext)_context).SetViewStyle(MapViewStyle.View2D);
            //        break;
            //    case StatusBarKeys.ViewStyle3D:
            //        ((ISecureContext)_context).SetViewStyle(MapViewStyle.View3D);
            //        break;
            //    case StatusBarKeys.ViewStyleAll:
            //        ((ISecureContext)_context).SetViewStyle(MapViewStyle.ViewAll);
            //        break;
               
            //    case StatusBarKeys.ViewStyleConfig:
            //        MessageBox.Show("进一步开发，预留快速二三维联动配置窗口启动");
            //        //var model = _context.Container.GetInstance<ConfigViewModel>();
            //        //model.UseSelectedPage = true;
            //        //model.SelectedPage = ConfigPageType.Projections;
            //        //_context.Container.Run<ConfigPresenter, ConfigViewModel>(model);
            //        break;
            //    default:
            //        // do nothing
            //        break;
            //}
        }

        private PluginIdentity Identity
        {
            get { return PluginIdentity.Default; }
        }

        protected IMenuItem FindItem(string itemKey)
        {
            return _context.StatusBar.FindItem(itemKey, Identity);
        }

        public void Update()
        {
            var bar = _context.StatusBar;

            UpdateSelectionMessage();

            var statusUnits = bar.FindItem(StatusBarKeys.MapUnits, Identity);
            statusUnits.Text = "单位: " + MapUnitsHelper.GetMapUnitsDesc((int)_context.MapControl.Map.MapUnits);

            //UpdateTmsProvider();

            //UpdateModifiedCount();
        }

        private void UpdateTmsProvider()
        {
            //string msg = "Base Layer: ";

            //if (_context.Map.TileProvider == Api.Enums.TileProvider.ProviderCustom)
            //{
            //    var tiles = _context.Map.Tiles;
            //    var provider = tiles.Providers.FirstOrDefault(p => p.Id == tiles.ProviderId);
            //    msg += provider != null ? provider.Name : "Not defined";
            //}
            //else
            //{
            //    msg += _context.Map.TileProvider.EnumToString();
            //}

            //var statusProvider = _context.StatusBar.FindItem(StatusBarKeys.TileProvider, Identity);
            //statusProvider.Text = msg;
        }

        private void UpdateModifiedCount()
        {
            //var status = _context.StatusBar.FindItem(StatusBarKeys.ModifiedCount, Identity);

            //var layer = _context.Map.Layers.Current;
            //if (layer != null && layer.IsVector && layer.FeatureSet.InteractiveEditing)
            //{
            //    int featureCount = layer.FeatureSet.Features.Count(f => f.Modified);
            //    status.Text = string.Format("Modified: {0} features", featureCount);
            //    status.Visible = true;
            //    return;
            //}

            //status.Visible = false;
        }

        private void UpdateSelectionMessage()
        {
            IMap pMap = _context.MapControl.Map;
            if (pMap == null) return;
            int count=pMap.SelectionCount;
            var status = _context.StatusBar.FindItem(StatusBarKeys.SelectedCount, Identity);
            if (status != null)
            {
                status.Text = string.Format("{0}个要素被选中", count);
                status.Visible = true;
            }
        }

        private void MapProjectionChanged(object sender, EventArgs e)
        {
            //var item = _context.StatusBar.FindItem(StatusBarKeys.ProjectionDropDown, Identity);
            //var p = _context.Map.Projection;
            //item.Text = !p.IsEmpty ? p.Name : "Not defined";
        }

        private void map_ExtentsChanged(object sender, EventArgs e)
        {
            
        }
    }
}
