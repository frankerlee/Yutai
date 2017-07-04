using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Carto;
using Yutai.ArcGIS.Carto.Library;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Controls.Controls;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands.Carto
{
    public sealed class CmdApplyTFMapTemplateByDataRange : YutaiCommand
    {
        private PrintingPlugin _plugin;
        public override bool Enabled
        {
            get
            {
                return this._context.FocusMap.MapUnits == esriUnits.esriMeters ||
                       this._context.FocusMap.MapUnits == esriUnits.esriKilometers ||
                       this._context.FocusMap.SpatialReference is IProjectedCoordinateSystem;
            }
        }

        public override void OnCreate(object hook)
        {
            this.m_category = "制图";
            this.m_caption = "范围";
            this.m_message = "添加制图模板";
            this.m_toolTip = "添加制图模板";

            base.m_bitmap = Properties.Resources.icon_map_fence;
            base.m_name = "Printing_ApplyTFMapTemplateByDataRange";
            _key = "Printing_ApplyTFMapTemplateByDataRange";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdApplyTFMapTemplateByDataRange(IAppContext context,PrintingPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }


        private void ClearElements(IActiveView pActiveView)
        {
            IGraphicsContainer graphicsContainer = pActiveView.GraphicsContainer;
            IMapFrame mapFrame = graphicsContainer.FindFrame(pActiveView.FocusMap) as IMapFrame;
            (mapFrame as IMapGrids).ClearMapGrids();
            graphicsContainer.Reset();
            IElement element = graphicsContainer.Next();
            List<IElement> list = new List<IElement>();
            while (element != null)
            {
                if (element != mapFrame)
                {
                    list.Add(element);
                }
                element = graphicsContainer.Next();
            }
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

        public override void OnClick()
        {
            if (this._context.FocusMap.MapUnits == esriUnits.esriUnknownUnits)
            {
                System.Windows.Forms.MessageBox.Show("请设置地图单位后，再执行该命令!");
            }
            else
            {
                frmMapTemplateApplyWizard frmMapTemplateApplyWizard = new frmMapTemplateApplyWizard(_plugin.TemplateGallery);
                frmMapTemplateApplyWizard.FixDataRange = true;
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