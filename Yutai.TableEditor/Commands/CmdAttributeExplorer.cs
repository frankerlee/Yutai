// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  CmdAttributeExplorer.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/12  10:52
// 更新时间 :  2017/06/12  10:52

using System;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.TableEditor.Views;

namespace Yutai.Plugins.TableEditor.Commands
{
    public class CmdAttributeExplorer : YutaiCommand
    {
        private ITableEditorView _view;
        public CmdAttributeExplorer(IAppContext context, ITableEditorView view)
        {
            _context = context;
            _view = view;
            OnCreate();
        }

        private void OnCreate()
        {
            base.m_caption = "属性资源管理器";
            base.m_category = "TableEditor";
            base.m_bitmap = null;
            base.m_name = "tedSelection.mnuAttributeExplorer";
            base._key = "tedSelection.mnuAttributeExplorer";
            base.m_toolTip = "属性资源管理器";
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
            AttributeExplorer frm = new AttributeExplorer(_context, _view.MapView, _view.CurrentGridView.FeatureLayer, _view.CurrentGridView.VirtualGridView.StrGeometry);
            frm.ShowDialog();
        }
    }
}