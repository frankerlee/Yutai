// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  CmdShowAllFields.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/13  11:31
// 更新时间 :  2017/06/13  11:31

using System;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.TableEditor.Views;

namespace Yutai.Plugins.TableEditor.Commands
{
    public class CmdShowAllFields : YutaiCommand
    {
        private ITableEditorView _view;
        public CmdShowAllFields(IAppContext context, ITableEditorView view)
        {
            _context = context;
            _view = view;
            OnCreate();
        }

        private void OnCreate()
        {
            base.m_caption = "打开所有字段";
            base.m_category = "TableEditor";
            base.m_bitmap = null;
            base.m_name = "tedFields.mnuShowAllFields";
            base._key = "tedFields.mnuShowAllFields";
            base.m_toolTip = "打开所有字段";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.CheckBox;
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
            _view.CurrentGridView.VirtualGridView.ShowAllFields();
        }
    }
}