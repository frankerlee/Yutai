using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.TableEditor.Controls;
using Yutai.Plugins.TableEditor.Views;

namespace Yutai.Plugins.TableEditor.Commands
{
    class CmdJoinDatasource : YutaiCommand
    {
        private ITableEditorView _view;

        public CmdJoinDatasource(IAppContext context, ITableEditorView view)
        {
            _context = context;
            _view = view;
            OnCreate();
        }

        private void OnCreate()
        {
            base.m_caption = "连接表";
            base.m_category = "TableEditor";
            base.m_bitmap = null;
            base.m_name = "tedTools.mnuJoinDatasource";
            base._key = "tedTools.mnuJoinDatasource";
            base.m_toolTip = "连接表";
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
                TableJoins frm = new TableJoins();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (frm._joinsGrid1.JoinModels == null || frm._joinsGrid1.JoinModels.Count <= 0)
                        return;
                    foreach (JoinModel joinModel in frm._joinsGrid1.JoinModels)
                    {
                        _view.CurrentGridView.VirtualGridView.JoinTable(joinModel.Table as IFeatureClass, joinModel.FromField, joinModel.ToField, joinModel.Fields.Split(',').ToList());
                    }
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
