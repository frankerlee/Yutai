using ESRI.ArcGIS.Carto;
using System;
using System.Drawing;
using Yutai.ArcGIS.Carto.UI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public class CmdNewNeatline : YutaiCommand
    {
        public override bool Enabled
        {
            get { return this._context.ActiveView is IPageLayout; }
        }

        public override void OnCreate(object hook)
        {
            this.m_name = "InsertNeatline";
            this.m_caption = "";
            this.m_message = "在布局视图中插入轮廓线";
            this.m_toolTip = "轮廓线";
            this.m_category = "Printing";

            base.m_bitmap = Properties.Resources.icon_netline;
            base.m_name = "Printing_NewNeatline";
            _key = "Printing_NewNeatline";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdNewNeatline(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            this.method_0();
        }

        private bool method_0()
        {
            IActiveView activeView = this._context.ActiveView;
            IGraphicsContainer graphicsContainer = activeView.GraphicsContainer;
            bool result;
            if (graphicsContainer == null)
            {
                result = false;
            }
            else
            {
                new frmOutline
                {
                    PageLayout = activeView as IPageLayout
                }.ShowDialog();
                result = true;
            }
            return result;
        }

        public bool FocusOneElement(IPageLayout ipageLayout_0, IElement ielement_0)
        {
            bool result;
            try
            {
                IGraphicsContainerSelect graphicsContainerSelect = ipageLayout_0 as IGraphicsContainerSelect;
                if (graphicsContainerSelect == null)
                {
                    result = false;
                    return result;
                }
                IActiveView activeView = ipageLayout_0 as IActiveView;
                activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphicSelection, null, null);
                int elementSelectionCount = graphicsContainerSelect.ElementSelectionCount;
                if (elementSelectionCount >= 1)
                {
                    graphicsContainerSelect.UnselectAllElements();
                }
                graphicsContainerSelect.SelectElement(ielement_0);
                activeView.Refresh();
                result = true;
                return result;
            }
            catch
            {
            }
            result = false;
            return result;
        }
    }
}