using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using System;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.Carto;
using Yutai.ArcGIS.Controls.SymbolUI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public class CmdNewScaleBar : YutaiCommand
    {
        public override bool Enabled
        {
            get { return this._context.ActiveView is IPageLayout; }
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "比例尺";
            this.m_message = "在布局视图中插入比例尺";
            this.m_toolTip = "比例尺";
            this.m_category = "Printing";
            base.m_bitmap = Properties.Resources.ScaleBar;
            base.m_name = "Printing_NewScaleBar";
            _key = "Printing_NewScaleBar";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdNewScaleBar(IAppContext context)
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
                try
                {
                    frmSymbolSelector frmSymbolSelector = new frmSymbolSelector();
                    if (frmSymbolSelector == null)
                    {
                        result = false;
                        return result;
                    }
                    IScaleBar scaleBar = new HollowScaleBar();
                    frmSymbolSelector.SetSymbol(scaleBar);
                    frmSymbolSelector.SetStyleGallery(this._context.StyleGallery);
                    if (frmSymbolSelector.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        scaleBar = (frmSymbolSelector.GetSymbol() as IScaleBar);
                        if (scaleBar == null)
                        {
                            result = false;
                            return result;
                        }
                        UID clsid = new UID
                        {
                            Value = "esriCarto.ScaleBar"
                        };
                        IMapFrame mapFrame = graphicsContainer.FindFrame(activeView.FocusMap) as IMapFrame;
                        scaleBar.Units = mapFrame.Map.DistanceUnits;
                        IMapSurroundFrame mapSurroundFrame = mapFrame.CreateSurroundFrame(clsid, scaleBar);
                        IElement element = mapSurroundFrame as IElement;
                        IEnvelope envelope = new Envelope() as IEnvelope;
                        envelope.PutCoords(2.0, 8.0, 9.0, 10.0);
                        IEnvelope envelope2 = new Envelope() as IEnvelope;
                        scaleBar.QueryBounds(this._context.ActiveView.ScreenDisplay, envelope, envelope2);
                        element.Geometry = envelope2;
                        INewElementOperation newElementOperation = new NewElementOperation();
                        newElementOperation.ActiveView = this._context.ActiveView;
                        newElementOperation.Element = element;
                        this._context.OperationStack.Do(newElementOperation);
                        result = true;
                        return result;
                    }
                }
                catch
                {
                }
                result = false;
            }
            return result;
        }
    }
}