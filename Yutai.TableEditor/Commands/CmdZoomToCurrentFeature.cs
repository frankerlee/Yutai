// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  CmdZoomToCurrentFeature.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/07  16:08
// 更新时间 :  2017/06/07  16:08

using System;
using ESRI.ArcGIS.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.TableEditor.Editor;
using Yutai.Plugins.TableEditor.Views;

namespace Yutai.Plugins.TableEditor.Commands
{
    public class CmdZoomToCurrentFeature : YutaiCommand
    {
        private ITableEditorView _view;

        public CmdZoomToCurrentFeature(IAppContext context, ITableEditorView view)
        {
            _context = context;
            _view = view;
            OnCreate();
        }

        private void OnCreate()
        {
            base.m_caption = "缩放到当前要素";
            base.m_category = "TableEditor";
            base.m_bitmap = null;
            base.m_name = "tedSelection.mnuZoomToCurrentFeature";
            base._key = "tedSelection.mnuZoomToCurrentFeature";
            base.m_toolTip = "缩放到当前要素";
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
            ITableView pGridView = _view.CurrentGridView;
            if (pGridView == null)
                return;
            if (pGridView.CurrentOID == -1)
                return;
            _view.MapView.ZoomToFeature(_view.CurrentGridView.FeatureLayer, _view.CurrentGridView.CurrentOID);
        }
    }
}