// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  CmdOpenAttributeTable.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/06  11:16
// 更新时间 :  2017/06/06  11:16

using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using Yutai.Controls;
using Yutai.Plugins;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;
using Yutai.Services.Serialization;
using Yutai.UI.Docking;

namespace Yutai.Commands.MapLegend
{
    public class CmdOpenAttributeTable : YutaiCommand
    {

        private IMapLegendView _view;


        public CmdOpenAttributeTable(IAppContext context, IMapLegendView view)
        {
            _context = context;
            _view = view;
            OnCreate();
        }

        private void OnCreate()
        {
            base.m_caption = "打开属性表";
            base.m_category = "TOC";
            base.m_bitmap = Properties.Resources.icon_attribute_table;
            base.m_name = "mnuOpenAttributeTable";
            base._key = "mnuOpenAttributeTable";
            base.m_toolTip = "打开属性表";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            OnCreate();
        }

        public override void OnClick()
        {

            if (_view.SelectedItemType == esriTOCControlItem.esriTOCControlItemLayer && _view.SelectedLayer != null)
            {
                if (_view.SelectedLayer is IFeatureLayer)
                {
                    var args = new PluginMessageEventArgs(PluginMessages.ShowAttributeTable);
                    _context.Broadcaster.BroadcastEvent(t=>t.MessageBroadcasted_, _view, args);
                    // 显示属性表
                    //IDockPanel dock = _context.DockPanels.Find(DockPanelKeys.TableEditor);
                    //if (dock == null)
                    //    return;
                    //dock.Visible = true;
                }
            }
        }
    }
}