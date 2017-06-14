// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  CmdAddField.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/12  12:58
// 更新时间 :  2017/06/12  12:58

using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.TableEditor.Views;
using Yutai.UI.Dialogs;

namespace Yutai.Plugins.TableEditor.Commands
{
    public class CmdAddField : YutaiCommand
    {
        private ITableEditorView _view;

        public CmdAddField(IAppContext context, ITableEditorView view)
        {
            _context = context;
            _view = view;
            OnCreate();
        }

        private void OnCreate()
        {
            base.m_caption = "添加字段";
            base.m_category = "TableEditor";
            base.m_bitmap = null;
            base.m_name = "tedFields.mnuAddField";
            base._key = "tedFields.mnuAddField";
            base.m_toolTip = "添加字段";
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
                FrmFieldProperties view = new FrmFieldProperties();
                if (view.ShowDialog() == DialogResult.OK)
                {
                    IClass pClass = _view.CurrentGridView.FeatureLayer.FeatureClass;
                    pClass.AddField(view.NewField);
                    _view.CurrentGridView.VirtualGridView.AddColumnToGrid(view.NewField);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}