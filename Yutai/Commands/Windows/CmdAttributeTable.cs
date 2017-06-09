// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  CmdAttributeTable.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/06  16:33
// 更新时间 :  2017/06/06  16:33

using System;
using DevExpress.XtraBars.Docking;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Services.Serialization;
using Yutai.UI.Docking;

namespace Yutai.Commands.Windows
{
    public class CmdAttributeTable : YutaiCommand
    {
        public CmdAttributeTable(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {
            DockPanel dock = _context.DockPanels.GetDockPanel("Plug_TableEditor_View");
            if (dock == null)
                return;
            dock.Visible = !dock.Visible;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "属性表";
            base.m_category = "Window";
            base.m_bitmap = Properties.Resources.icon_attribute_table;
            base.m_name = "Window.Common.AttributeTable";
            base._key = "Window_Common_AttributeTable";
            base.m_toolTip = "属性表";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
    }
}