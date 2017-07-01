using System;
using System.Collections.Generic;
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
    class CmdRemoveField : YutaiCommand
    {
        private CompContextMenuStrip _menuStrip;

        public CmdRemoveField(IAppContext context, CompContextMenuStrip menuStrip)
        {
            _context = context;
            _menuStrip = menuStrip;
            OnCreate();
        }

        private void OnCreate()
        {
            base.m_caption = "删除字段";
            base.m_category = "TableEditorCMS";
            base.m_bitmap = null;
            base.m_name = "mnuRemoveField";
            base._key = "mnuRemoveField";
            base.m_toolTip = "删除字段";
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
                IClass pClass = _menuStrip.TableView.FeatureLayer.FeatureClass;
                IField pField = pClass.Fields.Field[_menuStrip.ColumnIndex];
                string msg = $"确定要删除字段：{pField.Name}？";
                if (MessageBox.Show(msg, null, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    pClass.DeleteField(pField);
                    _menuStrip.TableView.VirtualGridView.RemoveField(_menuStrip.ColumnIndex);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}