using System;
using System.Drawing;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public class CmdUndoOperation : YutaiCommand
    {
        public override bool Enabled
        {
            get
            {
                return this._context.OperationStack.Count != 0 && this._context.OperationStack.UndoOperation != null;
            }
        }

        public override void OnCreate(object hook)
        {
            this.m_name = "UndoOperation";
            this.m_caption = "撤销";
            this.m_category = "编辑器";
            this.m_message = "撤销操作";
            this.m_toolTip = "撤销操作";
            base.m_bitmap = Properties.Resources.icon_undo;
            base.m_name = "Printing_UndoOperation";
            _key = "Printing_UndoOperation";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdUndoOperation(IAppContext context)
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
                if (this._context.OperationStack.UndoOperation != null)
                {
                    this._context.OperationStack.Undo();
                    this._context.ActiveView.Refresh();
                }
            }
            catch (Exception exception_)
            {
                //CErrorLog.writeErrorLog(this, exception_, "");
            }
        }
    }
}