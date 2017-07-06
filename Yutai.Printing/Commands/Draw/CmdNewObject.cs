using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using System;
using System.Windows.Forms;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Carto;
using Yutai.ArcGIS.Common.ExtendClass;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public class CmdNewObject : YutaiCommand
    {
        public override bool Enabled
        {
            get { return this._context.ActiveView is IPageLayout; }
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "";
            this.m_message = "插入对象";
            this.m_toolTip = "插入对象";
            this.m_category = "制图";
            base.m_bitmap = Properties.Resources.icon_object;
            base.m_name = "Printing_NewObject";
            _key = "Printing_NewObject";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdNewObject(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            int num = -1;
            if (this._context.MainView.ControlType == GISControlType.PageLayout)
            {
                num = (this._context.MainView.ActiveGISControl as IPageLayoutControl).hWnd;
            }
            else if (this._context.MainView.ControlType == GISControlType.MapControl)
            {
                num = (this._context.MainView.ActiveGISControl as IMapControl2).hWnd;
            }

            if (num != -1)
            {
                ArcGIS.Common.ExtendClass.IOleFrame oleFrame = new OleFrame();
                if (oleFrame.CreateOleClientItem(this._context.ActiveView.ScreenDisplay, num))
                {
                    INewElementOperation operation = new NewElementOperation
                    {
                        ActiveView = this._context.ActiveView,
                        Element = oleFrame as IElement
                    };
                    this._context.OperationStack.Do(operation);
                }
            }
        }
    }
}