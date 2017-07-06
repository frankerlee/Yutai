using ESRI.ArcGIS.Carto;
using System;
using System.Drawing;
using Yutai.ArcGIS.Common.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public class CmdGroupElement : YutaiCommand
    {
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
                    IGraphicsContainerSelect graphicsContainerSelect =
                        this._context.ActiveView.GraphicsContainer as IGraphicsContainerSelect;
                    result = (graphicsContainerSelect.ElementSelectionCount > 1);
                }
                return result;
            }
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "";
            this.m_toolTip = "组合";
            this.m_category = "制图";
            this.m_name = "GroupElement";
            base.m_bitmap = Properties.Resources.icon_group;
            base.m_name = "Printing_GroupElement";
            _key = "Printing_GroupElement";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdGroupElement(IAppContext context)
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
            IEnumElement selectedElements = graphicsContainerSelect.SelectedElements;
            IGroupElementOperation groupElementOperation = new GroupElementOperation();
            groupElementOperation.ActiveView = this._context.ActiveView;
            groupElementOperation.Elements = selectedElements;
            this._context.OperationStack.Do(groupElementOperation);
            this._context.ActiveView.Refresh();
            //DocumentManager.DocumentChanged(this._context.Hook);
        }
    }
}