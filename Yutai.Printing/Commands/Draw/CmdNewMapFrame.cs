using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using System;
using System.Drawing;
using Yutai.ArcGIS.Common.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public class CmdNewMapFrame : YutaiCommand
    {
        private int button = 0;

        public override bool Enabled
        {
            get { return this._context.ActiveView is IPageLayout; }
        }

        public override void OnCreate(object hook)
        {
            this.m_name = "InsertMapFrame";
            this.m_caption = "数据框";
            this.m_message = "新建数据框";
            this.m_toolTip = "数据框";
            this.m_category = "制图";
            base.m_bitmap = Properties.Resources.icon_mapframe;
            base.m_name = "Printing_NewMapFrame";
            _key = "Printing_NewMapFrame";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdNewMapFrame(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }


        public override void OnClick()
        {
            this.AddMapFrame();
        }

        public bool AddMapFrame()
        {
            IActiveView arg_0B_0 = this._context.ActiveView;
            IGraphicsContainer graphicsContainer = this._context.ActiveView as IGraphicsContainer;
            bool result;
            if (graphicsContainer == null)
            {
                result = false;
            }
            else
            {
                IMapFrame mapFrame = new MapFrame() as IMapFrame;
                IMap map = new Map();
                this.button++;
                map.Name = string.Format("新建地图 {0}", this.button);
                mapFrame.Map = map;
                IElement element = mapFrame as IElement;
                IEnvelope envelope = new Envelope() as IEnvelope;
                envelope.PutCoords(0.0, 0.0, 5.0, 5.0);
                element.Geometry = envelope;
                INewElementOperation operation = new NewElementOperation
                {
                    ActiveView = this._context.ActiveView,
                    Element = element
                };
                this._context.OperationStack.Do(operation);
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