using ESRI.ArcGIS.Carto;
using System;
using System.Drawing;
using Yutai.ArcGIS.Common.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public class CmdUnGroupElement : YutaiCommand
    {
        private IGroupElement2 igroupElement2_0 = null;

        public override bool Enabled
        {
            get
            {
                bool result;
                if (this._context.FocusMap == null)
                {
                    result = false;
                }
                else
                {
                    this.igroupElement2_0 = null;
                    IGraphicsContainerSelect graphicsContainerSelect =
                        this._context.ActiveView.GraphicsContainer as IGraphicsContainerSelect;
                    if (graphicsContainerSelect.ElementSelectionCount == 0)
                    {
                        result = false;
                    }
                    else
                    {
                        IEnumElement selectedElements = graphicsContainerSelect.SelectedElements;
                        for (IElement element = selectedElements.Next();
                            element != null;
                            element = selectedElements.Next())
                        {
                            if (element is IGroupElement)
                            {
                                this.igroupElement2_0 = (element as IGroupElement2);
                                result = true;
                                return result;
                            }
                        }
                        result = false;
                    }
                }
                return result;
            }
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "";
            this.m_toolTip = "取消组合";
            this.m_category = "制图";


            base.m_bitmap = Properties.Resources.icon_ungroup;
            base.m_name = "Printing_UngroupElement";
            _key = "Printing_UngroupElement";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdUnGroupElement(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            IGraphicsContainer graphicsContainer = this._context.ActiveView.GraphicsContainer;
            IGraphicsContainerSelect graphicsContainerSelect = graphicsContainer as IGraphicsContainerSelect;
            IUnGroupElementOperation unGroupElementOperation = new UnGroupElementOperation();
            unGroupElementOperation.ActiveView = this._context.ActiveView;
            unGroupElementOperation.Elements = graphicsContainerSelect.SelectedElements;
            this._context.OperationStack.Do(unGroupElementOperation);
            this._context.ActiveView.Refresh();
        }
    }
}