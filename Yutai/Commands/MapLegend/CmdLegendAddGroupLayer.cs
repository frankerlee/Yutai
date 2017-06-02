using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using Yutai.Controls;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Commands.MapLegend
{
    class CmdLegendAddGroupLayer : YutaiCommand
    {
        private IAppContext _context;
        private IMapLegendView _view;
        private bool _enabled;

        public CmdLegendAddGroupLayer(IAppContext context, IMapLegendView view)
        {
            _context = context;
            _view = view;
            OnCreate();
        }

        public override bool Enabled
        {
            get
            {
                if (_view == null) return false;
                if (_view.SelectedItemType == esriTOCControlItem.esriTOCControlItemMap) return true;
                if (_view.SelectedItemType == esriTOCControlItem.esriTOCControlItemLayer)
                {
                    if (_view.SelectedLayer is IGroupLayer) return true;
                    return false;
                }
                return false;
            }
        }

        private void OnCreate()
        {
            base.m_caption = "新建组合图层";
            base.m_category = "TOC";
            base.m_bitmap = Properties.Resources.icon_layer_add;
            base.m_name = "mnuNewGroupLayer";
            base._key = "mnuNewGroupLayer";
            base.m_toolTip = "新建组合图层";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.NormalItem;
        }
        public override void OnClick(object sender, EventArgs args)
        {
            IGroupLayer grpLayer = new GroupLayerClass() {Name="新图层组"};
            if (_view.SelectedItemType == esriTOCControlItem.esriTOCControlItemLayer)
            {
                ((IGroupLayer)_view.SelectedLayer).Add(grpLayer);
            }
            else if (_view.SelectedItemType == esriTOCControlItem.esriTOCControlItemMap)
            {
                _view.SelectedMap.AddLayer(grpLayer);
            }
           _view.TocControl.Update();
        }

        public override void OnCreate(object hook)
        {
            OnCreate();
        }
    }
}
