// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  CmdFieldProperties.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/13  10:00
// 更新时间 :  2017/06/13  10:00

using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.TableEditor.Controls;
using Yutai.Plugins.TableEditor.Views;

namespace Yutai.Plugins.TableEditor.Commands.ContextMenu
{
    public class CmdFieldProperties : YutaiCommand
    {
        private CompContextMenuStrip _menuStrip;

        public CmdFieldProperties(IAppContext context, CompContextMenuStrip menuStrip)
        {
            _context = context;
            _menuStrip = menuStrip;
            OnCreate();
        }

        private void OnCreate()
        {
            base.m_caption = "字段属性";
            base.m_category = "TableEditorCMS";
            base.m_bitmap = null;
            base.m_name = "mnuFieldProperties";
            base._key = "mnuFieldProperties";
            base.m_toolTip = "字段属性";
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
                FrmFieldProperties view = new FrmFieldProperties(pField);
                if (view.ShowDialog() == DialogResult.OK)
                {
                    
                    IFieldEdit pFieldEdit = pField as IFieldEdit;
                    pFieldEdit.Name_2 = view.NewField.Name;
                    pFieldEdit.AliasName_2 = view.NewField.AliasName;
                    
                    _menuStrip.TableView.VirtualGridView.UpdateField(_menuStrip.ColumnIndex, pField);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }
    }
}