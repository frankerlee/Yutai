using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.SystemUI;
using Yutai.Plugins.Enums;

namespace Yutai.Plugins.Concrete
{
    public abstract class YutaiTool:YutaiCommand,ITool
    {
        protected Cursor m_cursor;
        protected bool m_deactivate;
       

        protected YutaiTool()
        {
            this.m_deactivate = true;
            base._itemType = RibbonItemType.Tool;
        }

        protected YutaiTool(System.Drawing.Bitmap bitmap, string caption, string category, int helpContextId, string helpFile, string message, string name, string toolTip):base(bitmap,  caption,  category,  helpContextId,  helpFile,  message,  name,  toolTip)
        {
            
        base._itemType= RibbonItemType.Tool;
            this.m_deactivate = true;
        }

        public virtual void OnMouseDown(int button, int Shift, int x, int y)
        {
           
        }

        public virtual void OnMouseMove(int Button, int Shift, int x, int y)
        {
           
        }

        public virtual void OnMouseUp(int button, int shift, int x, int y)
        {
           
        }

        public virtual void OnDblClick()
        {
           
        }

        public virtual void OnKeyDown(int keyCode, int Shift)
        {
           
        }

        public virtual void OnKeyUp(int keyCode, int shift)
        {
           
        }

        public virtual bool OnContextMenu(int x, int y)
        {
            return false;
        }

        public virtual void Refresh(int hdc)
        {
           
        }

        public virtual bool Deactivate()
        {
            return this.m_deactivate;
        }

        public virtual int Cursor
        {
            get
            {
                int num;
                num = (this.m_cursor == null) ? 0 : this.m_cursor.Handle.ToInt32();
                return num;
            }
        }

        //public override RibbonItemType ItemType
        //{
        //    get { return _itemType; }
        //    set { _itemType = value; }
        //}
    }

  
}
