using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Carto.MapCartoTemplateLib;
using Yutai.ArcGIS.Carto.UI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands.View
{
    public class CmdMapGridProperty:YutaiCommand
    {
        public override bool Enabled
        {
            get { return this._context.ActiveView is IPageLayout; }
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "格网属性";
            this.m_toolTip = "格网属性";
            this.m_category = "页面操作";
            base.m_bitmap = Properties.Resources.PageFixZoomIn;
            base.m_name = "Printing_MapGrid";
            _key = "Printing_MapGrid";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdMapGridProperty(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            IPageLayout activeView = this._context.ActiveView as IPageLayout;
            IGraphicsContainer graphicsContainer = this._context.ActiveView.GraphicsContainer;
            IMapFrame mapFrame = graphicsContainer.FindFrame(this._context.FocusMap) as IMapFrame;
            GridAxisPropertyPage.m_pSG = this._context.StyleGallery;
            (new frmMapGridsProperty(activeView, mapFrame as IMapGrids)).ShowDialog();
            this._context.ActiveView.Refresh();
        }
    }
}
