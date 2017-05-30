using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;
using Yutai.Controls;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Commands.MapLegend
{
    public class CmdLegendAddData:YutaiCommand
    {
        private IAppContext _context;
        private IMapLegendView _view;
        private bool _enabled;
        private ICommand _command;
        public CmdLegendAddData(IAppContext context, IMapLegendView view)
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
                if (_view.SelectedMap == null)
                {
                    if (_view.SelectedLayer == null) return false;
                    return _view.SelectedLayer is IGroupLayer ? true : false;
                }
                else
                {
                    return true;
                }
            }
        }

        private void OnCreate()
        {
            base.m_caption = "新增数据源";
            base.m_category = "TOC";
            base.m_bitmap = Properties.Resources.icon_layer_add;
            base.m_name = "mnuAddData";
            base._key = "mnuAddData";
            base.m_toolTip = "新增数据源";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.NormalItem;
           
        }
        public override void OnClick(object sender, EventArgs args)
        {
            if (_command == null)
            {
                _command = new ESRI.ArcGIS.Controls.ControlsAddDataCommandClass();
                _command.OnCreate(_context.MapControl);
            }
            _command.OnClick();
        }

        public override void OnCreate(object hook)
        {
            OnCreate();
        }
    }
}
