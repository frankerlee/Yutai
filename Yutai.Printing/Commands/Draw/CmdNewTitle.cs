using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using System;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Printing.Forms;

namespace Yutai.Plugins.Printing.Commands
{
    public class CmdNewTitle : YutaiCommand
    {
        public override bool Enabled
        {
            get { return this._context.ActiveView is IPageLayout; }
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "";
            this.m_message = "插入标题";
            this.m_toolTip = "标题";
            this.m_category = "制图";
            base.m_bitmap = Properties.Resources.icon_title;
            base.m_name = "Printing_NewTitle";
            _key = "Printing_NewTitle";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdNewTitle(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            frmInputText frmInputText = new frmInputText();
            if (frmInputText.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string text = frmInputText.txtText.Text;
                IElement element = new TextElement();
                ITextElement textElement = element as ITextElement;
                ITextSymbol textSymbol = new TextSymbol();
                textSymbol.Text = text;
                textSymbol.Size = 48.0;
                IActiveView activeView = this._context.ActiveView as IActiveView;
                activeView.ScreenDisplay.StartDrawing(0, 0);
                double num;
                double num2;
                textSymbol.GetTextSize(activeView.ScreenDisplay.WindowDC, activeView.ScreenDisplay.DisplayTransformation,
                    text, out num, out num2);
                activeView.ScreenDisplay.FinishDrawing();
                textSymbol.Text = text;
                num *= 0.0353;
                num2 *= 0.0353;
                IEnvelope extent = (this._context.ActiveView as IActiveView).Extent;
                double num3;
                double num4;
                double num5;
                double num6;
                extent.QueryCoords(out num3, out num4, out num5, out num6);
                num3 = (num3 + num5)/2.0;
                num4 = num6 - 3.0;
                num5 = num3 + 1.2*num;
                num6 = num4 + 1.2*num2;
                num3 = (double) ((int) num3);
                num4 = (double) ((int) num4);
                num5 = (double) ((int) num5);
                num6 = (double) ((int) num6);
                extent.PutCoords(num3, num4, num5, num6);
                textElement.Symbol = textSymbol;
                textElement.Text = text;
                IPoint point = new ESRI.ArcGIS.Geometry.Point();
                point.PutCoords(num3, num4);
                element.Geometry = point;
                IGraphicsContainer arg_181_0 = activeView.GraphicsContainer;
                (element as IElementProperties2).AutoTransform = true;
                INewElementOperation operation = new NewElementOperation
                {
                    ActiveView = this._context.ActiveView,
                    Element = element
                };
                this._context.OperationStack.Do(operation);
            }
        }
    }
}