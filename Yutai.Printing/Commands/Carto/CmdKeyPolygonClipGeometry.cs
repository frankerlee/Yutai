using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
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
    public sealed class CmdKeyPolygonClipGeometry : YutaiCommand
    {
        public override bool Enabled
        {
            get { return this._context.FocusMap.LayerCount > 0; }
        }


        public override void OnCreate(object hook)
        {
            this.m_category = "制图工具";
            this.m_caption = "输入";
            this.m_message = "设置多边形裁剪区";
            this.m_toolTip = "设置多边形裁剪区";
            this.m_bitmap = Properties.Resources.icon_clip_polygon;
            base.m_name = "Printing_ImportPolygonClipGeometry";
            _key = "Printing_ImportPolygonClipGeometry";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdKeyPolygonClipGeometry(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }


        public override void OnClick()
        {
            frmInputCoordinate frmInputCoordinate = new frmInputCoordinate();
            frmInputCoordinate.Map = this._context.FocusMap;
            if (frmInputCoordinate.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if ((frmInputCoordinate.Geometry as ITopologicalOperator).IsSimple)
                {
                    frmInputCoordinate.Geometry.SpatialReference = this._context.FocusMap.SpatialReference;
                    this._context.FocusMap.ClipGeometry = frmInputCoordinate.Geometry;
                    (this._context.FocusMap as IActiveView).Extent = frmInputCoordinate.Geometry.Envelope;
                    this._context.ActiveView.Refresh();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("多边形非简单多边形!");
                }
            }
        }
    }
}