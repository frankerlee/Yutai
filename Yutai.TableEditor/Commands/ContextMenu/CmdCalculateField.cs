// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  CmdCalculateField.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/13  09:58
// 更新时间 :  2017/06/13  09:58

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.GeoDatabaseUI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.TableEditor.Controls;
using Yutai.Plugins.TableEditor.Views;

namespace Yutai.Plugins.TableEditor.Commands.ContextMenu
{
    public class CmdCalculateField : YutaiCommand
    {
        private CompContextMenuStrip _menuStrip;

        public CmdCalculateField(IAppContext context, CompContextMenuStrip menuStrip)
        {
            _context = context;
            _menuStrip = menuStrip;
            OnCreate();
        }

        private void OnCreate()
        {
            base.m_caption = "字段计算器";
            base.m_category = "TableEditorCMS";
            base.m_bitmap = null;
            base.m_name = "mnuCalculateField";
            base._key = "mnuCalculateField";
            base.m_toolTip = "字段计算器";
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
            if (Yutai.ArcGIS.Common.Editor.Editor.CheckIsEdit(_menuStrip.TableView.FeatureLayer))
            {
                ITable pTable = _menuStrip.TableView.VirtualGridView.FeatureLayer.FeatureClass as ITable;
                IField pField = pTable.Fields.Field[_menuStrip.ColumnIndex];
                FieldCalculator frm = new FieldCalculator(_menuStrip.TableView.FeatureLayer.FeatureClass.Fields, pField);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ICalculator pCalculator = new CalculatorClass();
                        ICursor pCursor = pTable.Search(null, false);
                        pCalculator.Cursor = pCursor;
                        pCalculator.Field = pField.Name;
                        pCalculator.Expression = frm.Expression;

                        pCalculator.Calculate();
                        Marshal.ReleaseComObject(pCursor);
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                    _menuStrip.TableView.VirtualGridView.ShowTable(null);
                }
            }
            else
            {
                MessageBox.Show(@"未启动编辑", Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}