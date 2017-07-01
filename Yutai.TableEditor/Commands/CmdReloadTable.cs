using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.TableEditor.Views;

namespace Yutai.Plugins.TableEditor.Commands
{
    class CmdReloadTable : YutaiCommand
    {
        private ITableEditorView _view;

        public CmdReloadTable(IAppContext context, ITableEditorView view)
        {
            _context = context;
            _view = view;
            OnCreate();
        }

        private void OnCreate()
        {
            base.m_caption = "重新载入表";
            base.m_category = "TableEditor";
            base.m_bitmap = null;
            base.m_name = "tedLayout.mnuReloadTable";
            base._key = "tedLayout.mnuReloadTable";
            base.m_toolTip = "重新载入表";
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
            _view.CurrentGridView.VirtualGridView.ShowTable(null);
        }
    }
}