using System.Drawing;
using ESRI.ArcGIS.SystemUI;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Common.BaseClasses
{
    public abstract class BaseTool : BaseCommand, ITool
    {
        protected System.Windows.Forms.Cursor m_cursor;
        protected bool m_deactivate;

     
        protected BaseTool(IAppContext context):base( context) { 
            this.m_deactivate = true;
        }

        protected BaseTool(Bitmap bitmap_0, string string_0, string string_1, System.Windows.Forms.Cursor cursor_0, int int_0, string string_2, string string_3, string string_4, string string_5) : base(bitmap_0, string_0, string_1, int_0, string_2, string_3, string_4, string_5)
        {
            this.m_deactivate = true;
            this.m_cursor = cursor_0;
        }

        public virtual bool Deactivate()
        {
            return this.m_deactivate;
        }

        public virtual bool OnContextMenu(int int_0, int int_1)
        {
            return false;
        }

        public virtual void OnDblClick()
        {
        }

        public virtual void OnKeyDown(int int_0, int int_1)
        {
        }

        public virtual void OnKeyUp(int int_0, int int_1)
        {
        }

        public virtual void OnMouseDown(int int_0, int int_1, int int_2, int int_3)
        {
        }

        public virtual void OnMouseMove(int int_0, int int_1, int int_2, int int_3)
        {
        }

        public virtual void OnMouseUp(int int_0, int int_1, int int_2, int int_3)
        {
        }

        public virtual void Refresh(int int_0)
        {
        }

        public virtual int Cursor
        {
            get
            {
                if (this.m_cursor != null)
                {
                    return this.m_cursor.Handle.ToInt32();
                }
                return 0;
            }
        }
    }
}

