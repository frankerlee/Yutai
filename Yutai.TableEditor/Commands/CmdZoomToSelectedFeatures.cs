// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  CmdZoomToSelectedFeatures.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/07  16:08
// 更新时间 :  2017/06/07  16:08

using System;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.TableEditor.Editor;
using Yutai.Plugins.TableEditor.Views;

namespace Yutai.Plugins.TableEditor.Commands
{
    public class CmdZoomToSelectedFeatures : YutaiCommand
    {
        private ITableEditorView _view;
        public CmdZoomToSelectedFeatures(IAppContext context, ITableEditorView view)
        {
            _context = context;
            _view = view;
            OnCreate();
        }

        private void OnCreate()
        {
            base.m_caption = "缩放到选择集";
            base.m_category = "TableEditor";
            base.m_bitmap = null;
            base.m_name = "tedSelection.mnuZoomToSelectedFeatures";
            base._key = "tedSelection.mnuZoomToSelectedFeatures";
            base.m_toolTip = "缩放到选择集";
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

        public void OnClick()
        {
            IGridView pGridView = _view.CurrentGridView;
            if (pGridView == null)
                return;
            if (pGridView.OID == -1)
                return;
            _view.MapView.ZoomToSelectedFeatures(_view.CurrentGridView.FeatureLayer);
        }
    }
}