using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using Yutai.Controls;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.UI.Dialogs;

namespace Yutai.Commands.MapLegend
{
    public class CmdExportData : YutaiCommand
    {
        private IAppContext _context;
        private IMapLegendView _view;
        private bool _enabled;
        private ICommand _command;
        public CmdExportData(IAppContext context, IMapLegendView view)
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
                if (_view.SelectedItemType == esriTOCControlItem.esriTOCControlItemLayer)
                {
                    if (_view.SelectedLayer == null) return false;
                    if (_view.SelectedLayer is IGroupLayer) return false;
                    else
                        return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void OnCreate()
        {
            base.m_caption = "导出图层";
            base.m_category = "TOC";
            base.m_bitmap = null;
            base.m_name = "mnuExportData";
            base._key = "mnuExportData";
            base.m_toolTip = "导出图层";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.NormalItem;

        }
        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            OnCreate();
        }

       
        public void OnClick()
        {

            if (_view.SelectedItemType == esriTOCControlItem.esriTOCControlItemMap)
            {
                return;
            }
            frmExportData data = new frmExportData();
            data.FocusMap = _view.SelectedMap;
            data.FeatureLayer = _view.SelectedLayer as IFeatureLayer;
            data.ShowDialog();

        }
    }
}