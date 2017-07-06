using System;
using System.Drawing;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public class CmdRedoOperation : YutaiCommand
    {
        public override bool Enabled
        {
            get
            {
                return this._context.OperationStack.Count != 0 && this._context.OperationStack.RedoOperation != null;
            }
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "";
            this.m_category = "编辑器";
            this.m_message = "重做操作";
            this.m_toolTip = "重做操作";
            base.m_bitmap = Properties.Resources.icon_redo;
            base.m_name = "Printing_RedoOperation";
            _key = "Printing_RedoOperation";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdRedoOperation(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            try
            {
                if (this._context.OperationStack.RedoOperation != null)
                {
                    this._context.OperationStack.Redo();
                    this._context.ActiveView.Refresh();
                }
            }
            catch (Exception exception_)
            {
                CErrorLog.writeErrorLog(this, exception_, "");
            }
        }
    }
}