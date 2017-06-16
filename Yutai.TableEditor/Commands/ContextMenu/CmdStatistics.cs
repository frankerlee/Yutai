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
using Yutai.Plugins.TableEditor.Views;

namespace Yutai.Plugins.TableEditor.Commands.ContextMenu
{
    class CmdStatistics : YutaiCommand
    {
        private CompContextMenuStrip _menuStrip;
        public CmdStatistics(IAppContext context, CompContextMenuStrip menuStrip)
        {
            _context = context;
            _menuStrip = menuStrip;
            OnCreate();
        }

        private void OnCreate()
        {
            base.m_caption = "统计";
            base.m_category = "TableEditorCMS";
            base.m_bitmap = null;
            base.m_name = "mnuStatistics";
            base._key = "mnuStatistics";
            base.m_toolTip = "统计";
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
            string fieldName = _menuStrip.TableView.VirtualGridView.Table.Columns[_menuStrip.ColumnIndex].ColumnName;
            int idx = _menuStrip.TableView.FeatureLayer.FeatureClass.FindField(fieldName);
            IField pField = _menuStrip.TableView.FeatureLayer.FeatureClass.Fields.Field[idx];
            if (pField.Type == esriFieldType.esriFieldTypeDouble ||
                    pField.Type == esriFieldType.esriFieldTypeInteger ||
                    pField.Type == esriFieldType.esriFieldTypeSingle ||
                    pField.Type == esriFieldType.esriFieldTypeSmallInteger)
            {
                FieldStatistics frm = new FieldStatistics(_menuStrip.TableView, fieldName);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show(@"统计信息不适用于文本字段。");
            }
        }

    }
}
