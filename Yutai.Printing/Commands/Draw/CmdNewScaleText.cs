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
    public class CmdNewScaleText : YutaiCommand
    {
        public override bool Enabled
        {
            get { return this._context.ActiveView is IPageLayout; }
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "";
            this.m_message = "在布局视图中插入比例尺文本描述";
            this.m_toolTip = "比例尺文本";
            this.m_category = "制图";
            base.m_bitmap = Properties.Resources.ScaleText;
            base.m_name = "Printing_NewScaleText";
            _key = "Printing_NewScaleText";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdNewScaleText(IAppContext context)
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
                    IScaleText scaleText = new ScaleText();
                    frmSymbolSelector.SetSymbol(scaleText);
                    frmSymbolSelector.SetStyleGallery(this._context.StyleGallery);
                    if (frmSymbolSelector.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        scaleText = (frmSymbolSelector.GetSymbol() as IScaleText);
                        if (scaleText == null)
                        {
                            result = false;
                            return result;
                        }
                        UID clsid = new UID
                        {
                            Value = "esriCarto.ScaleText"
                        };
                        IMapFrame mapFrame = graphicsContainer.FindFrame(activeView.FocusMap) as IMapFrame;
                        scaleText.MapUnits = mapFrame.Map.MapUnits;
                        IMapSurroundFrame mapSurroundFrame = mapFrame.CreateSurroundFrame(clsid, scaleText);
                        IElement element = mapSurroundFrame as IElement;
                        IEnvelope envelope = new Envelope() as IEnvelope;
                        envelope.PutCoords(5.0, 13.0, 8.0, 14.0);
                        IEnvelope envelope2 = new Envelope() as IEnvelope;
                        scaleText.QueryBounds(this._context.ActiveView.ScreenDisplay, envelope, envelope2);
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
            }
            return result;
        }
    }
}