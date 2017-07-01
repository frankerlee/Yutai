using ESRI.ArcGIS.Carto;
using System;
using Yutai.ArcGIS.Common.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public class CmdDeleteElement : YutaiCommand
    {
        public override bool Enabled
        {
            get { return this._context.FocusMap != null && this.GetGraphicsSelect().ElementSelectionCount > 0; }
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "删除";
            this.m_toolTip = "删除";
            this.m_category = "元素";
            base.m_bitmap = Properties.Resources.icon_delete;
            base.m_name = "Printing_DeleteElement";
            _key = "Printing_DeleteElement";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdDeleteElement(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            this.DeleteElement();
        }


        private IGraphicsContainerSelect GetGraphicsSelect()
        {
            IGraphicsContainerSelect result;
            try
            {
                IActiveView activeView = this._context.ActiveView;
                IGraphicsContainer graphicsContainer = activeView.GraphicsContainer;
                if (graphicsContainer == null)
                {
                    result = null;
                    return result;
                }
                IViewManager viewManager = activeView as IViewManager;
                ISelection arg_2E_0 = viewManager.ElementSelection;
                result = (graphicsContainer as IGraphicsContainerSelect);
                return result;
            }
            catch
            {
            }
            result = null;
            return result;
        }

        private bool DeleteElement()
        {
            IActiveView activeView = this._context.ActiveView;
            IDeleteElementOperation deleteElementOperation = new DeleteElementOperation();
            deleteElementOperation.ActiveView = activeView;
            deleteElementOperation.Elements = this.GetGraphicsSelect().SelectedElements;
            this._context.OperationStack.Do(deleteElementOperation);
            return true;
        }
    }
}