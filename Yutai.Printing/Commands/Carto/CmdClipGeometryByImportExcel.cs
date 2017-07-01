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
    public sealed class CmdClipGeometryByImportExcel : YutaiCommand
    {
        public override bool Enabled
        {
            get { return this._context.FocusMap.LayerCount > 0; }
        }


        public override void OnCreate(object hook)
        {
            this.m_category = "制图工具";
            this.m_caption = "导入";
            this.m_message = "导入裁剪区";
            this.m_toolTip = "导入裁剪区";

            base.m_bitmap = Properties.Resources.icon_clip_excel;
            base.m_name = "Printing_ClipByImportExcel";
            _key = "Printing_ClipByImportExcel";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdClipGeometryByImportExcel(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            frmImportExcelSet frmImportExcelSet = new frmImportExcelSet();
            if (frmImportExcelSet.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                IPolygon polygon = frmImportExcelSet.Geometry as IPolygon;
                if (polygon == null)
                {
                    System.Windows.Forms.MessageBox.Show("多边形创建失败!");
                }
                else if ((polygon as IArea).Area == 0.0)
                {
                    System.Windows.Forms.MessageBox.Show("多边形面积为0!");
                }
                else if ((polygon as ITopologicalOperator).IsSimple)
                {
                    polygon.SpatialReference = this._context.FocusMap.SpatialReference;
                    this._context.FocusMap.ClipGeometry = polygon;
                    this._context.ActiveView.Extent = polygon.Envelope;
                    (this._context.FocusMap as IActiveView).Extent = polygon.Envelope;
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