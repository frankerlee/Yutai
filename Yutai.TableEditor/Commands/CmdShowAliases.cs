// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  CmdShowAliases.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/12  16:31
// 更新时间 :  2017/06/12  16:31

using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.TableEditor.Views;

namespace Yutai.Plugins.TableEditor.Commands
{
    public class CmdShowAliases : YutaiCommand
    {
        private ITableEditorView _view;
        public CmdShowAliases(IAppContext context, ITableEditorView view)
        {
            _context = context;
            _view = view;
            OnCreate();
        }

        private void OnCreate()
        {
            base.m_caption = "显示字段别名";
            base.m_category = "TableEditor";
            base.m_bitmap = null;
            base.m_name = "tedFields.mnuShowAliases";
            base._key = "tedFields.mnuShowAliases";
            base.m_toolTip = "切换显示字段名称/别名";
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
            base.m_checked = !base.m_checked;
            if (base.m_checked)
                _view.CurrentGridView.VirtualGridView.ShowAlias();
            else
                _view.CurrentGridView.VirtualGridView.ShowName();
            base.m_caption = base.m_checked ? "显示字段名称" : "显示字段别名";
        }
    }
}