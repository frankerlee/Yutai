using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Carto;
using Yutai.ArcGIS.Common.ExtendClass;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Printing.Forms;

namespace Yutai.Plugins.Printing.Commands
{
    public class CmdInsertFeactionText : YutaiCommand
    {
        public override bool Enabled
        {
            get { return this._context.ActiveView is IPageLayout; }
        }

        public CmdInsertFeactionText(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "";
            base.m_toolTip = "插入分式文本";
            base.m_message = "插入分式文本";
            base.m_category = "Printing";
            base.m_bitmap = Properties.Resources.icon_feaction;
            base.m_name = "Printing_NewFractionText";
            _key = "Printing_NewFractionText";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }

        public override void OnClick()
        {
            frmFractionTextElement frmFractionTextElement = new frmFractionTextElement();
            if (frmFractionTextElement.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                IElement element = new FractionTextElement();
                (element as IFractionTextElement).DenominatorText = frmFractionTextElement.txtDenominatorText.Text;
                (element as IFractionTextElement).NumeratorText = frmFractionTextElement.txtNumeratorText.Text;
                IActiveView activeView = this._context.ActiveView as IActiveView;
                activeView.ScreenDisplay.StartDrawing(0, 0);
                IPoint geometry = new Point
                {
                    X = 3.0,
                    Y = 3.0
                };
                IPolygon polygon = new Polygon() as IPolygon;
                ((element as IFractionTextElement).NumeratorTextSymbol as ISymbol).QueryBoundary(
                    activeView.ScreenDisplay.hDC, activeView.ScreenDisplay.DisplayTransformation, geometry, polygon);
                double width = polygon.Envelope.Width;
                double height = polygon.Envelope.Height;
                ((element as IFractionTextElement).DenominatorTextSymbol as ISymbol).QueryBoundary(
                    activeView.ScreenDisplay.hDC, activeView.ScreenDisplay.DisplayTransformation, geometry, polygon);
                double width2 = polygon.Envelope.Width;
                double height2 = polygon.Envelope.Height;
                double num = (width > width2) ? width : width2;
                double num2 = height + height2;
                activeView.ScreenDisplay.FinishDrawing();
                IEnvelope extent = (this._context.ActiveView as IActiveView).Extent;
                double num3;
                double num4;
                double num5;
                double num6;
                extent.QueryCoords(out num3, out num4, out num5, out num6);
                num3 = (num3 + num5)/2.0;
                num4 = (num4 + num6)/2.0;
                num5 = num3 + 1.1*num;
                num6 = num4 + 1.1*num2;
                extent.PutCoords(num3, num4, num5, num6);
                IPoint point = new Point();
                point.PutCoords(num3, num4);
                element.Geometry = extent;
                (element as IElementProperties2).AutoTransform = true;
                INewElementOperation operation = new NewElementOperation
                {
                    ActiveView = this._context.ActiveView,
                    Element = element
                };
                this._context.OperationStack.Do(operation);
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }
    }
}