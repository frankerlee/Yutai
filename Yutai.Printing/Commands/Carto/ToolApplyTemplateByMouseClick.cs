using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Carto;
using Yutai.ArcGIS.Carto.Library;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Controls.Controls;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands.Carto
{
    public sealed class ToolApplyTemplateByMouseClick : YutaiTool
    {
        public override bool Enabled
        {
            get
            {
                //if (!(_context.ActiveView is IPageLayout)) return false;

                return this._context.FocusMap != null &&
                       (this._context.FocusMap.MapUnits == esriUnits.esriMeters ||
                        this._context.FocusMap.MapUnits == esriUnits.esriKilometers ||
                        this._context.FocusMap.SpatialReference is IProjectedCoordinateSystem);
            }
        }


        public override void OnCreate(object hook)
        {
            m_category = "制图工具";
            m_caption = "点击";
            m_message = "鼠标点击，加载制图模板";
            m_toolTip = "鼠标点击制图";

            base.m_bitmap = Properties.Resources.icon_map_marker;
            base.m_name = "Printing_ApplyTemplateByMouseClick";
            _key = "Printing_ApplyTemplateByMouseClick";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;
            _context = hook as IAppContext;
        }

        public ToolApplyTemplateByMouseClick(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }


        public override void OnClick()
        {
            _context.SetCurrentTool(this);
        }

        private void ClearElements(IActiveView pActiveView)
        {
            IGraphicsContainer graphicsContainer = pActiveView.GraphicsContainer;
            IMapFrame mapFrame = graphicsContainer.FindFrame(pActiveView.FocusMap) as IMapFrame;
            (mapFrame as IMapGrids).ClearMapGrids();
            graphicsContainer.Reset();
            IElement element = graphicsContainer.Next();
            new List<IElement>();
            try
            {
                graphicsContainer.DeleteAllElements();
                graphicsContainer.Reset();
                element = graphicsContainer.Next();
                if (element != null)
                {
                    graphicsContainer.DeleteElement(element);
                }
                graphicsContainer.AddElement(mapFrame as IElement, -1);
                pActiveView.FocusMap = mapFrame.Map;
            }
            catch (Exception)
            {
            }
        }

        public override void OnMouseDown(int button, int shift, int x, int y)
        {
            if (this._context.FocusMap.MapUnits == esriUnits.esriUnknownUnits)
            {
                System.Windows.Forms.MessageBox.Show("请设置地图单位后，再执行该命令!");
            }
            else
            {
                IPoint point = (this._context.FocusMap as IActiveView).ScreenDisplay.DisplayTransformation.ToMapPoint(
                    x, y);
                double X;
                double Y;
                if (this._context.FocusMap.MapUnits == esriUnits.esriKilometers)
                {
                    X = point.X*1000.0;
                    Y = point.Y*1000.0;
                }
                else
                {
                    X = point.X;
                    Y = point.Y;
                }
                frmMapTemplateApplyWizard frmMapTemplateApplyWizard = new frmMapTemplateApplyWizard();
                frmMapTemplateApplyWizard.SetMouseClick(X, Y);
                if (frmMapTemplateApplyWizard.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    _context.MainView.ActivatePageLayout();
                    if (this._context.ActiveView != null)
                    {
                        this.ClearElements(this._context.ActiveView as IActiveView);
                        frmMapTemplateApplyWizard.MapTemplateHelp.ApplyMapTemplate(
                            this._context.ActiveView as IPageLayout);
                    }
                }
            }
        }
    }
}