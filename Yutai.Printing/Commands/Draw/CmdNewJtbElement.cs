using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Carto.UI;
using Yutai.ArcGIS.Common.Carto;
using Yutai.ArcGIS.Common.ExtendClass;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public class CmdNewJtbElement : YutaiCommand
    {
        private System.Windows.Forms.TextBox textBox_0 = new System.Windows.Forms.TextBox();

        public override bool Enabled
        {
            get { return this._context.ActiveView is IPageLayout; }
        }


        public override void OnCreate(object hook)
        {
            this.m_caption = "";
            this.m_message = "插入接图表";
            this.m_toolTip = "标题";
            this.m_category = "Printing";

            base.m_bitmap = Properties.Resources.icon_jtb;
            base.m_name = "Printing_NewJTBElement";
            _key = "Printing_NewJTBElement";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdNewJtbElement(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            IEnvelope extent = (this._context.ActiveView).Extent;
            double xMin = extent.XMin;
            double y = extent.YMax + 1.0;
            IElement element = new JTBElement();
            IPoint point = new Point();
            point.PutCoords(xMin, y);
            element.Geometry = point;
            if (new frmJTBElement
                {
                    JTBElement = element as IJTBElement
                }.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                IGraphicsContainer graphicsContainer = this._context.ActiveView.GraphicsContainer;
                graphicsContainer.AddElement(element, 0);
                ElementOperator.FocusOneElement(graphicsContainer as IActiveView, element);
            }
        }
    }
}