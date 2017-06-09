// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  CmdBuildQuery.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/09  09:10
// 更新时间 :  2017/06/09  09:10

using System;
using System.Windows.Forms;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Identifer.Query;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.TableEditor.Views;

namespace Yutai.Plugins.TableEditor.Commands
{
    public class CmdBuildQuery : YutaiCommand
    {
        private ITableEditorView _view;
        public CmdBuildQuery(IAppContext context, ITableEditorView view)
        {
            _context = context;
            _view = view;
            OnCreate();
        }

        private void OnCreate()
        {
            base.m_caption = "按属性选择";
            base.m_category = "TableEditor";
            base.m_bitmap = null;
            base.m_name = "tedSelection.mnuBuildQuery";
            base._key = "tedSelection.mnuBuildQuery";
            base.m_toolTip = "按属性选择";
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
            string whereCaluse = null;
            frmSimpleAttributeQueryBuilder frm = new frmSimpleAttributeQueryBuilder()
            {
                CurrentLayer = _view.CurrentGridView.FeatureLayer,
            };
            if (frm.ShowDialog() == DialogResult.OK)
            {
                whereCaluse = frm.WhereCaluse;
            }
            _view.CurrentGridView.ReloadData(whereCaluse);
        }
    }
}