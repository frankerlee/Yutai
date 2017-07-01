using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class DeleteElement : BaseCommand
    {
        private IYTHookHelper _hookHelper;

        public DeleteElement()
        {
            base.m_caption = "删除";
            base.m_toolTip = "删除";
            base.m_name = "DeleteElement";
            base.m_category = "元素";
        }

        private IGraphicsContainerSelect method_0()
        {
            try
            {
                IActiveView activeView = this._hookHelper.ActiveView;
                IGraphicsContainer graphicsContainer = activeView.GraphicsContainer;
                if (graphicsContainer == null)
                {
                    return null;
                }
                IViewManager manager = activeView as IViewManager;
                ISelection elementSelection = manager.ElementSelection;
                return (graphicsContainer as IGraphicsContainerSelect);
            }
            catch
            {
            }
            return null;
        }

        private bool method_1()
        {
            IActiveView activeView = this._hookHelper.ActiveView;
            IDeleteElementOperation operation = new DeleteElementOperation
            {
                ActiveView = activeView,
                Elements = this.method_0().SelectedElements
            };
            this._hookHelper.OperationStack.Do(operation);
            activeView = null;
            return true;
        }

        public override void OnClick()
        {
            this.method_1();
        }

        public override void OnCreate(object object_0)
        {
            this._hookHelper.Hook = object_0;
        }

        public override bool Enabled
        {
            get
            {
                if (this._hookHelper.FocusMap == null)
                {
                    return false;
                }
                return (this.method_0().ElementSelectionCount > 0);
            }
        }
    }
}