using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.TableEditor.Controls;
using Yutai.Plugins.TableEditor.Editor;
using Yutai.Plugins.TableEditor.Views;

namespace Yutai.Plugins.TableEditor.Commands.ContextMenu
{
    class CmdSortAscending : YutaiCommand
    {
        private CompContextMenuStrip _menuStrip;

        public CmdSortAscending(IAppContext context, CompContextMenuStrip menuStrip)
        {
            _context = context;
            _menuStrip = menuStrip;
            OnCreate();
        }

        private void OnCreate()
        {
            base.m_caption = "升序排列";
            base.m_category = "TableEditorCMS";
            base.m_bitmap = null;
            base.m_name = "mnuSortAscending";
            base._key = "mnuSortAscending";
            base.m_toolTip = "升序排列";
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
            _menuStrip.TableView.VirtualGridView.Sort(_menuStrip.ColumnIndex, ListSortDirection.Descending);
        }
    }
}