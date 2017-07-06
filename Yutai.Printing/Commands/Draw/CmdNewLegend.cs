using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using System;
using System.Drawing;
using Yutai.ArcGIS.Carto.UI;
using Yutai.ArcGIS.Common.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public class CmdNewLegend : YutaiCommand
    {
        public override bool Enabled
        {
            get { return this._context.ActiveView is IPageLayout; }
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "";
            this.m_message = "将图例添加到布局视图";
            this.m_toolTip = "图例";
            this.m_category = "Printing";
            base.m_bitmap = Properties.Resources.icon_legend;
            base.m_name = "Printing_NewLegendElement";
            _key = "Printing_NewLegendElement";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdNewLegend(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }


        public override void OnClick()
        {
            LegendWizard legendWizard = new LegendWizard();
            legendWizard.StyleGallery = this._context.StyleGallery;
            IGraphicsContainer graphicsContainer = this._context.ActiveView as IGraphicsContainer;
            IMapFrame mapFrame = graphicsContainer.FindFrame(this._context.FocusMap) as IMapFrame;
            IMapSurroundFrame mapSurroundFrame = mapFrame.CreateSurroundFrame(new UID
            {
                Value = "esriCarto.Legend"
            }, null);
            legendWizard.InitialLegendFrame = mapSurroundFrame;
            legendWizard.FocusMap = this._context.FocusMap;
            legendWizard.ShowDialog();
            IEnvelope envelope = new Envelope() as IEnvelope;
            envelope.PutCoords(0.0, 0.0, 5.0, 10.0);
            IEnvelope envelope2 = new Envelope() as IEnvelope;
            mapSurroundFrame.MapSurround.QueryBounds(this._context.ActiveView.ScreenDisplay, envelope, envelope2);
            (mapSurroundFrame as IElement).Geometry = envelope2;
            INewElementOperation newElementOperation = new NewElementOperation();
            newElementOperation.ActiveView = this._context.ActiveView;
            newElementOperation.Element = (mapSurroundFrame as IElement);
            this._context.OperationStack.Do(newElementOperation);
        }
    }
}