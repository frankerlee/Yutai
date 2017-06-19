using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.ArcGIS.Common.Framework;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using IToolContextMenu = Yutai.Plugins.Interfaces.IToolContextMenu;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdEditTarget : YutaiTool,  IToolContextMenu
    {
        private EditTools pEditTools = null;

        private bool bool_0 = false;

       

        public override bool Enabled
        {
            get
            {
                bool result;
                if (_context.FocusMap == null)
                {
                    result = false;
                }
                else
                {
                    if ((_context.FocusMap.LayerCount == 0 || Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace == null || Yutai.ArcGIS.Common.Editor.Editor.EditMap == null) && _context.CurrentTool == this)
                    {
                        _context.CurrentTool = null;
                    }
                    result = (this.pEditTools != null && this.pEditTools.Enabled);
                }
                return result;
            }
        }

        public override int Cursor
        {
            get
            {
                return this.pEditTools.Cursor.Handle.ToInt32();
            }
        }

      
        public object ContextMenu
        {
            get
            {
                return null;
            }
        }

        public System.Windows.Forms.Keys Keys
        {
            get
            {
                return (System.Windows.Forms.Keys)131141;
            }
        }

        public CmdEditTarget(IAppContext context)
        {
           OnCreate(context);
        }

        public override void OnClick()
        {
            _context.SetCurrentTool(this);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_edit_target;
            this.m_cursor = new System.Windows.Forms.Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Plugins.Editor.Resources.Cursor.Edit.cur"));
            this.m_caption = "要素编辑";
            this.m_category = "编辑器";
            this.m_toolTip = "编辑";
            this.m_message = "编辑";
            this.m_name = "Edit_TargetTool";
            this._key = "Edit_TargetTool";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageBeforeText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Tool;
            try
            {
                this.pEditTools = new EditTools(_context);
            }
            catch (Exception)
            {
            }
          
        }

        public override void OnClick(object sender, EventArgs args)
        {
            if (this.pEditTools != null)
            {
                this.pEditTools.OnCreate(_context);
            }
            OnClick();
        }

       

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            this.pEditTools.OnMouseDown(Button, Shift, X, Y);
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            this.pEditTools.OnMouseUp(Button, Shift, X, Y);

        }

        public override void OnKeyDown(int int_0, int int_1)
        {
            this.pEditTools.OnKeyDown(int_0, int_1);
        }

        public override void OnKeyUp(int int_0, int int_1)
        {
            this.pEditTools.OnKeyUp(int_0, int_1);
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            this.pEditTools.OnMouseMove(Button, Shift, X, Y);
        }

        public override void OnDblClick()
        {
            this.pEditTools.OnDblClick();
        }

        public void Init()
        {
            this.pEditTools.Init();
        }

        public string[] ContextMenuKeys { get { return pEditTools.ContextMenuKeys; } }
    }
}
