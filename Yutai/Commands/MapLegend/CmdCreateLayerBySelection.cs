using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;
using Yutai.Controls;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Commands.MapLegend
{
    public class CmdCreateLayerBySelection : YutaiCommand
    {
        private IAppContext _context;
        private IMapLegendView _view;
        private bool _enabled;
        private ICommand _command;
        public CmdCreateLayerBySelection(IAppContext context, IMapLegendView view)
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
                    if (_view.SelectedLayer is IFeatureLayer) return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
        }

        private void OnCreate()
        {
            base.m_caption = "由选择集创建图层";
            base.m_category = "TOC";
            base.m_bitmap = null;
            base.m_name = "mnuCreateLayerBySelect";
            base._key = "mnuCreateLayerBySelect";
            base.m_toolTip = "由选择集创建图层";
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

        private string GetLayerName(IBasicMap map, string basic)
        {
            string lyrName = basic;
            int num = 1;
            for (int i = 0; i < map.LayerCount; i++)
            {
                if (map.Layer[i].Name == lyrName)
                {
                    lyrName = basic + num.ToString();
                    num++;
                }
            }
            return lyrName;
        }
        public void OnClick()
        {

            IFeatureLayer pLayer = _view.SelectedLayer as IFeatureLayer;
            IFeatureSelection selection = pLayer as IFeatureSelection;
            if (selection.SelectionSet.Count > 0)
            {
                string lyrName = GetLayerName(_view.SelectedMap, "选择集");
                IFeatureLayer newLayer = ((IFeatureLayerDefinition2)pLayer).CreateSelectionLayer(lyrName, true, "", "");
                selection.Clear();
                _view.SelectedMap.AddLayer(newLayer);
                (_view.SelectedMap as IActiveView).Refresh();
            }
        }
    }
}