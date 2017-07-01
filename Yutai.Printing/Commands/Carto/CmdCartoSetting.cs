using ESRI.ArcGIS.Controls;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using Yutai.ArcGIS.Carto.Library;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands.Carto
{
    public sealed class CmdCartoSetting : YutaiCommand
    {
        public override void OnCreate(object hook)
        {
            this.m_category = "制图工具";
            this.m_caption = "设置";
            this.m_message = "制图参数设置";
            this.m_toolTip = "制图参数设置";

            base.m_bitmap = Properties.Resources.icon_map_index;
            base.m_name = "Printing_CartoSetting";
            _key = "Printing_CartoSetting";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdCartoSetting(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            frmCartoConfig frmCartoConfig = new frmCartoConfig();
            frmCartoConfig.ShowDialog();
        }
    }
}