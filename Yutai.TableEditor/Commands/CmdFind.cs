// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  CmdFind.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/13  16:59
// 更新时间 :  2017/06/13  16:59

using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.TableEditor.Views;

namespace Yutai.Plugins.TableEditor.Commands
{
    public class CmdFind : YutaiCommand
    {
        private ITableEditorView _view;

        public CmdFind(IAppContext context, ITableEditorView view)
        {
            _context = context;
            _view = view;
            OnCreate();
        }

        private void OnCreate()
        {
            base.m_caption = "查找";
            base.m_category = "TableEditor";
            base.m_bitmap = null;
            base.m_name = "tedTools.mnuFind";
            base._key = "tedTools.mnuFind";
            base.m_toolTip = "查找";
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
            try
            {
                FindReplace frm = new FindReplace(_context, _view.CurrentGridView.VirtualGridView);
                frm.TopMost = true;
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}