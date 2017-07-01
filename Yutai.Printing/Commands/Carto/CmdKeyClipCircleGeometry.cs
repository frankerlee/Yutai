using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Yutai.ArcGIS.Carto.Library;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands.Carto
{
    public sealed class CmdKeyClipCircleGeometry : YutaiCommand
    {
        public override bool Enabled
        {
            get { return this._context.FocusMap != null && this._context.FocusMap.LayerCount > 0; }
        }


        public override void OnCreate(object hook)
        {
            this.m_category = "制图工具";
            this.m_caption = "输入";
            this.m_message = "设置圆形裁剪区";
            this.m_toolTip = "设置圆形裁剪区";
            this.m_name = "CircleClipSetCommand";
            base.m_bitmap = Properties.Resources.icon_clip_input;
            base.m_name = "Printing_KeyClipCircleGeometry";
            _key = "Printing_KeyClipCircleGeometry";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdKeyClipCircleGeometry(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            frmCircleInput frmCircleInput = new frmCircleInput();
            frmCircleInput.Map = this._context.FocusMap;
            if (frmCircleInput.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                frmCircleInput.Geometry.SpatialReference = this._context.FocusMap.SpatialReference;
                this._context.FocusMap.ClipGeometry = frmCircleInput.Geometry;
                (this._context.FocusMap as IActiveView).Extent = frmCircleInput.Geometry.Envelope;
                this._context.ActiveView.Refresh();
            }
        }
    }
}