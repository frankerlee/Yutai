using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using Yutai.ArcGIS.Carto.UI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Commands.Views
{
    public class CmdStartMapFrameProps : YutaiCommand
    {
        public CmdStartMapFrameProps(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            frmDataFrameProperty _frmDataFrameProperty = new frmDataFrameProperty()
            {
                FocusMap = this._context.FocusMap as IBasicMap
            };
            if (_frmDataFrameProperty.ShowDialog() == DialogResult.OK)
            {
                this._context.MapDocumentChanged();
                this._context.ActiveView.Refresh();
            }
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "地图属性";
            base.m_category = "视图";
            base.m_bitmap = Properties.Resources.icon_new_map;
            base.m_name = "View_MapFrameProps";
            base._key = "View_MapFrameProps";
            base.m_toolTip = "地图属性";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
    }
}