using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using Syncfusion.Windows.Forms.Tools;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Identifer.Query;
using Yutai.Plugins.Interfaces;
using IActiveViewEvents_Event = ESRI.ArcGIS.Carto.IActiveViewEvents_Event;

namespace Yutai.Plugins.Identifer.Commands
{
    public class CmdSetCurrentLayer : YutaiCommand, ICommandComboBox
    {
        private IdentifierPlugin _plugin;
        private string _caption;
        private bool _showCaption;
        private int _layoutType;
        private List<object> _items;
        private IMapControlEvents2_Event _mapEvents;
        private IActiveViewEvents_Event _activeViewEvents;
        private string _selectedText;
        private BarEditItem _linkCombo;

        public CmdSetCurrentLayer(IAppContext context, BasePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin as IdentifierPlugin;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
        }

        public BarEditItem LinkComboBox
        {
            get { return _linkCombo; }
            set { _linkCombo = value; }
        }

        public bool DropDownList
        {
            get { return true; }
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "选择图层：";
            base.m_category = "Query";
            base.m_bitmap = Properties.Resources.QueryAttribute;
            base.m_name = "Query_ccSetting_SetCurrentleLayer";
            base._key = "Query_ccSetting_SetCurrentleLayer";
            base.m_toolTip = "选择图层";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.ComboBox;
            _items = new List<object>();
            _layoutType = 0;
            _showCaption = true;
            InitEventListener();
        }

        private void InitEventListener()
        {
            _mapEvents = (IMapControlEvents2_Event) _context.MapControl;
            _mapEvents.OnMapReplaced += _mapEvents_OnMapReplaced;
            _activeViewEvents = _context.MapControl.ActiveView as IActiveViewEvents_Event;
            if (_activeViewEvents != null)
            {
                _activeViewEvents.ItemAdded += _activeViewEvents_ItemAdded;
                _activeViewEvents.ItemDeleted += _activeViewEvents_ItemDeleted;
            }
        }

        private void _activeViewEvents_ItemDeleted(object Item)
        {
            InitLayers();
        }

        private void _activeViewEvents_ItemAdded(object Item)
        {
            InitLayers();
        }

        private void _mapEvents_OnMapReplaced(object newMap)
        {
            InitLayers();
        }

        public string Caption
        {
            get { return base.m_caption; }
            set { base.m_caption = value; }
        }

        public bool ShowCaption
        {
            get { return _showCaption; }
            set { _showCaption = value; }
        }

        public int LayoutType
        {
            get { return _layoutType; }
            set { _layoutType = value; }
        }

        object[] ICommandComboBox.Items
        {
            get { return _items.ToArray(); }
            set
            {
                _items.Clear();
                for (int i = 0; i < value.Length; i++)
                {
                    _items.Add(value[i]);
                }
            }
        }


        private void InitLayers()
        {
            _items.Clear();
            IMap pMap = _context.MapControl.Map;
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                ILayer layer = pMap.Layer[i];
                if (layer is IGroupLayer)
                {
                    this.FillCompLayer(layer as ICompositeLayer);
                }
                else if (layer is IFeatureLayer)
                {
                    IFeatureLayer featureLayer = layer as IFeatureLayer;
                    LayerItem item = new LayerItem(featureLayer.Name, featureLayer);
                    _items.Add(item);
                }
            }

            if (_linkCombo != null)
            {
                ((RepositoryItemComboBox) _linkCombo.Edit).Items.Clear();
                ((RepositoryItemComboBox) _linkCombo.Edit).Items.AddRange(_items.ToArray());
            }
        }

        private void FillCompLayer(ICompositeLayer compLayer)
        {
            for (int i = 0; i < compLayer.Count; i++)
            {
                ILayer layer = compLayer.Layer[i];
                if (layer is IGroupLayer)
                {
                    this.FillCompLayer(layer as ICompositeLayer);
                }
                else if (layer is IFeatureLayer)
                {
                    IFeatureLayer featureLayer = layer as IFeatureLayer;
                    LayerItem item = new LayerItem(featureLayer.Name, featureLayer);
                    _items.Add(item);
                }
            }
        }

        public void OnEditValueChanged(object sender, EventArgs args)
        {
            BarEditItem barItem = sender as BarEditItem;

            LayerItem item = barItem.EditValue as LayerItem;
            if (item != null)
                _plugin.QuerySettings.CurrentLayer = item.Value as IFeatureLayer;
            else
                _plugin.QuerySettings.CurrentLayer = null;
        }

        public string SelectedText
        {
            get { return _selectedText; }
            set { _selectedText = value; }
        }
    }
}