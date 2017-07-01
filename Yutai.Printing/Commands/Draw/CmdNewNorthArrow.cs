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
    public class CmdNewNorthArrow : YutaiCommand
    {
        public override bool Enabled
        {
            get { return this._context.ActiveView is IPageLayout; }
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "指北针";
            this.m_message = "在布局视图中插入指北针";
            this.m_toolTip = "指北针";
            this.m_category = "Printing";
            this.m_category = "Printing";

            base.m_bitmap = Properties.Resources.icon_north_arrow;
            base.m_name = "Printing_NewNAElement";
            _key = "Printing_NewNAElement";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdNewNorthArrow(IAppContext context)
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
                return false;
            }

            try
            {
                frmSymbolSelector frmSymbolSelector = new frmSymbolSelector();
                if (frmSymbolSelector == null)
                {
                    return false;
                }
                INorthArrow northArrow = new MarkerNorthArrow();
                frmSymbolSelector.SetSymbol(northArrow);
                frmSymbolSelector.SetStyleGallery(this._context.StyleGallery);
                if (frmSymbolSelector.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    northArrow = (frmSymbolSelector.GetSymbol() as INorthArrow);
                    if (northArrow == null)
                    {
                        result = false;
                        return result;
                    }
                    northArrow.Refresh();
                    IEnvelope envelope = new Envelope() as IEnvelope;
                    envelope.PutCoords(5.0, 10.0, 8.0, 13.0);
                    IEnvelope envelope2 = new Envelope() as IEnvelope;
                    northArrow.QueryBounds(this._context.ActiveView.ScreenDisplay, envelope, envelope2);
                    UID clsid = new UID
                    {
                        Value = "esriCarto.MarkerNorthArrow"
                    };
                    IMapFrame mapFrame = graphicsContainer.FindFrame(activeView.FocusMap) as IMapFrame;
                    IMapSurroundFrame mapSurroundFrame = mapFrame.CreateSurroundFrame(clsid, northArrow);
                    IElement element = mapSurroundFrame as IElement;
                    element.Geometry = envelope2;
                    INewElementOperation operation = new NewElementOperation
                    {
                        ActiveView = this._context.ActiveView,
                        Element = element
                    };
                    this._context.OperationStack.Do(operation);
                    result = true;
                    return result;
                }
            }
            catch
            {
            }
            result = false;

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