using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Identifer.Helpers;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;

namespace Yutai.Plugins.Identifer.Commands
{
    class CmdSelectByGraphics:YutaiCommand
    {
     
        private IdentifierPlugin _plugin;
        public override bool Enabled
        {
            get
            {
                bool flag;
                if (_context.MapControl.Map != null)
                {
                    if ((_context.MapControl.Map.LayerCount <= 0 ? true : (_context.MapControl.Map as IGraphicsContainerSelect).ElementSelectionCount <= 0))
                    {
                        flag = false;
                        return flag;
                    }
                    flag = true;
                    return flag;
                }
                flag = false;
                return flag;
            }
        }

        public CmdSelectByGraphics(IAppContext context,BasePlugin plugin)
        {
            this.m_bitmap = Properties.Resources.icon_select_graphic;
            this.m_caption = "用图形选择";
            this.m_category = "Query";
            this.m_message = "用图形选择";
            this.m_name = "Query_SelectionTools_Mouse_SelectByGraphics";
            this._key = "Query_SelectionTools_Mouse_SelectByGraphics";
            this.m_toolTip = "用图形选择";
            _context = context;
            _plugin = plugin as IdentifierPlugin;
        }

        public override void OnCreate(object hook)
        {

        }

        public override void OnClick()
        {
            ISelectionEnvironment selectionEnvironmentClass= _plugin.QuerySettings.SelectionEnvironment;
            IMap pMap = _context.MapControl.Map;
            IEnumElement selectedElements = (pMap as IGraphicsContainerSelect).SelectedElements;
            selectedElements.Reset();
            IElement i = selectedElements.Next();
            if (i != null)
            {
                (pMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                try
                {
                    pMap.SelectByShape(i.Geometry, selectionEnvironmentClass, false);
                }
                catch
                {
                }
                esriSelectionResultEnum combinationMethod = selectionEnvironmentClass.CombinationMethod;
                selectionEnvironmentClass.CombinationMethod = esriSelectionResultEnum.esriSelectionResultAdd;
                for (i = selectedElements.Next(); i != null; i = selectedElements.Next())
                {
                    try
                    {
                        pMap.SelectByShape(i.Geometry, selectionEnvironmentClass, false);
                    }
                    catch
                    {
                    }
                }
                (pMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                selectionEnvironmentClass.CombinationMethod = combinationMethod;
            }
            else
            {
                MessageService.Current.Info("请先选择准备用于选择要素的图形！");
            }
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }
    }
}
