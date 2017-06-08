using System;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.SystemUI;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Common.BaseClasses
{
    public abstract class BaseCommand : ICommand
    {
        private IntPtr intptr_0;
        protected System.Drawing.Bitmap m_bitmap;
        protected string m_caption;
        protected string m_category;
        protected bool m_checked;
        protected bool m_enabled;
        protected string m_helpFile;
        protected int m_helpID;
        protected IYTHookHelper m_HookHelper;
        protected string m_message;
        protected string m_name;
        protected string m_toolTip;
        private uint uint_0;

        protected BaseCommand(IAppContext context)
        {
            this.m_HookHelper = context as IYTHookHelper;
            
            this.uint_0 = 0x80070057;
            this.m_enabled = true;
        }

        protected BaseCommand(System.Drawing.Bitmap bitmap_0, string string_0, string string_1, int int_0, string string_2, string string_3, string string_4, string string_5)
        {
            
            this.uint_0 = 0x80070057;
            this.m_enabled = true;
            this.m_bitmap = bitmap_0;
            this.m_name = string_4;
            this.m_message = string_3;
            this.m_caption = string_0;
            this.m_category = string_1;
            this.m_toolTip = string_5;
            this.m_helpID = int_0;
            this.m_helpFile = string_2;
        }

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr intptr_1);
        ~BaseCommand()
        {
            if (this.intptr_0.ToInt32() != 0)
            {
                DeleteObject(this.intptr_0);
            }
        }

        private void method_0()
        {
            if (this.m_bitmap != null)
            {
                this.m_bitmap.MakeTransparent(this.m_bitmap.GetPixel(0, 0));
                this.intptr_0 = this.m_bitmap.GetHbitmap();
            }
        }

        public virtual void OnClick()
        {
        }

        public virtual void OnCreate(object object_0)
        {
            if (this.m_HookHelper.Hook != object_0)
            {
                this.m_HookHelper.Hook = object_0;
            }
        }

        public void UpdateBitmap(System.Drawing.Bitmap bitmap_0)
        {
            this.m_bitmap.Dispose();
            if (this.intptr_0.ToInt32() != 0)
            {
                DeleteObject(this.intptr_0);
            }
            this.intptr_0 = IntPtr.Zero;
            try
            {
                this.m_bitmap = bitmap_0;
                this.method_0();
                this.intptr_0 = this.m_bitmap.GetHbitmap();
            }
            catch
            {
                Marshal.ThrowExceptionForHR((int) this.uint_0);
            }
        }

        public virtual int Bitmap
        {
            get
            {
                if (this.m_bitmap == null)
                {
                    return 0;
                }
                if (this.intptr_0.ToInt32() == 0)
                {
                    this.method_0();
                }
                return this.intptr_0.ToInt32();
            }
        }

        public virtual string Caption
        {
            get
            {
                return this.m_caption;
            }
        }

        public virtual string Category
        {
            get
            {
                if ((this.m_category != null) && (this.m_category != ""))
                {
                    return this.m_category;
                }
                return "Misc.";
            }
        }

        public virtual bool Checked
        {
            get
            {
                return this.m_checked;
            }
        }

        public virtual bool Enabled
        {
            get
            {
                return this.m_enabled;
            }
        }

        public virtual int HelpContextID
        {
            get
            {
                return this.m_helpID;
            }
        }

        public virtual string HelpFile
        {
            get
            {
                return this.m_helpFile;
            }
        }

        protected IYTHookHelper HookHelper
        {
            get
            {
                return this.m_HookHelper;
            }
        }

        public virtual string Message
        {
            get
            {
                return this.m_message;
            }
        }

        public virtual string Name
        {
            get
            {
                return this.m_name;
            }
        }

        public virtual string Tooltip
        {
            get
            {
                return this.m_toolTip;
            }
        }
    }
}

