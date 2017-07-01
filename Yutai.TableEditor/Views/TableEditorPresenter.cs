// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  TableEditorPresenter.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/06  15:16
// 更新时间 :  2017/06/06  15:16

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;
using Yutai.Plugins.TableEditor.Enums;

namespace Yutai.Plugins.TableEditor.Views
{
    public class TableEditorPresenter : CommandDispatcher<ITableEditorView, TableEditorCommand>, IDockPanelPresenter
    {
        private readonly IAppContext _context;
        private TableEditorPlugin _plugin;

        public TableEditorPresenter(IAppContext context, ITableEditorView view, TableEditorPlugin plugin) : base(view)
        {
            if (context == null) throw new ArgumentNullException("context");
            _context = context;
            _plugin = plugin;
        }

        public override void RunCommand(TableEditorCommand command)
        {
            switch (command)
            {
                case TableEditorCommand.Clear:

                    break;
            }
        }

        public Control GetInternalObject()
        {
            return View as Control;
        }

        public void OpenTable(IFeatureLayer layer)
        {
            if (layer == null) throw new ArgumentNullException("layer");
            View.OpenTable(layer);
        }
    }
}