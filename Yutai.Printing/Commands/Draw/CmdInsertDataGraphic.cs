using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Carto;
using Yutai.ArcGIS.Common.ExtendClass;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public class CmdInsertDataGraphic : YutaiCommand
    {
        public override bool Enabled
        {
            get { return this._context.ActiveView is IPageLayout; }
        }

        public CmdInsertDataGraphic(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "图表";
            base.m_toolTip = "插入对象";
            base.m_message = "插入对象";
            base.m_category = "Printing";
            base.m_bitmap = Properties.Resources.icon_dataGraphic;
            base.m_name = "Printing_NewDataGraphic";
            _key = "Printing_NewDataGraphic";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }

        public override void OnClick()
        {
            int num = -1;
            if (this._context.Hook is IPageLayoutControl)
            {
                num = (this._context.Hook as IPageLayoutControl).hWnd;
            }
            else if (this._context.Hook is IMapControl2)
            {
                num = (this._context.Hook as IMapControl2).hWnd;
            }
            else if (this._context.Hook is IApplication)
            {
                if ((this._context.Hook as IApplication).Hook is IPageLayoutControl)
                {
                    num = ((this._context.Hook as IApplication).Hook as IPageLayoutControl).hWnd;
                }
                else if ((this._context.Hook as IApplication).Hook is IMapControl2)
                {
                    num = ((this._context.Hook as IApplication).Hook as IMapControl2).hWnd;
                }
            }
            if (num != -1)
            {
                DataGraphicsElement element = new DataGraphicsElement();
                INewElementOperation operation = new NewElementOperation
                {
                    ActiveView = this._context.ActiveView,
                    Element = element
                };
                this._context.OperationStack.Do(operation);
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }


        private IActiveView GetActiveView()
        {
            IActiveView focusMap = null;


            if (this._context.MainView.ControlType == GISControlType.PageLayout)
            {
                focusMap =
                    (this._context.MainView.ActiveGISControl as IPageLayoutControl).ActiveView.FocusMap as IActiveView;
            }

            return focusMap;
        }
    }
}