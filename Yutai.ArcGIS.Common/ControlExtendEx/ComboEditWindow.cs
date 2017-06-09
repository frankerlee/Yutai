namespace JLK.ControlExtendEx
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public sealed class ComboEditWindow : NativeWindow
    {
        private const int EC_LEFTMARGIN = 1;
        private const int EC_RIGHTMARGIN = 2;
        private const int EM_SETMARGINS = 0xd3;
        private Graphics graphics_0;
        private Image image_0 = null;
        private ImageComboBox imageComboBox_0 = null;
        private int int_0 = 0;
        private int int_1 = -1;
        private Struct3 struct3_0 = new Struct3();
        private const int WM_CHAR = 0x102;
        private const int WM_GETTEXT = 13;
        private const int WM_GETTEXTLENGTH = 14;
        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;
        private const int WM_LBUTTONDOWN = 0x201;
        private const int WM_PAINT = 15;
        private const int WM_SETCURSOR = 0x20;

        public void AssignTextBoxHandle(ImageComboBox imageComboBox_1)
        {
            this.imageComboBox_0 = imageComboBox_1;
            this.struct3_0.cbSize = Marshal.SizeOf(this.struct3_0);
            GetComboBoxInfo(this.imageComboBox_0.Handle, ref this.struct3_0);
            if (!base.Handle.Equals(IntPtr.Zero))
            {
                this.ReleaseHandle();
            }
            base.AssignHandle(this.struct3_0.hwndEdit);
        }

        public void DrawImage()
        {
            if (this.CurrentIcon != null)
            {
                this.graphics_0 = Graphics.FromHwnd(base.Handle);
                bool flag = false;
                if ((this.imageComboBox_0.RightToLeft == RightToLeft.Inherit) && (this.imageComboBox_0.Parent.RightToLeft == RightToLeft.Yes))
                {
                    flag = true;
                }
                if ((this.imageComboBox_0.RightToLeft == RightToLeft.Yes) || flag)
                {
                    this.graphics_0.DrawImage(this.CurrentIcon, (float) (this.graphics_0.VisibleClipBounds.Width - this.CurrentIcon.Width), (float) 0f);
                }
                else if ((this.imageComboBox_0.RightToLeft == RightToLeft.No) || flag)
                {
                    this.graphics_0.DrawImage(this.CurrentIcon, 0, 0);
                }
                this.graphics_0.Flush();
                this.graphics_0.Dispose();
            }
        }

        [DllImport("user32", CharSet=CharSet.Auto)]
        private static extern IntPtr FindWindowEx(IntPtr intptr_0, IntPtr intptr_1, [MarshalAs(UnmanagedType.LPTStr)] string string_0, [MarshalAs(UnmanagedType.LPTStr)] string string_1);
        [DllImport("user32")]
        private static extern bool GetComboBoxInfo(IntPtr intptr_0, ref Struct3 struct3_1);
        [DllImport("user32", CharSet=CharSet.Auto)]
        private static extern int SendMessage(IntPtr intptr_0, int int_2, int int_3, int int_4);
        public void SetImageToDraw()
        {
            try
            {
                if (this.imageComboBox_0 != null)
                {
                    this.int_1 = this.imageComboBox_0.SelectedIndex;
                    if (this.int_1 > -1)
                    {
                        int imageIndex = this.imageComboBox_0.Items[this.int_1].ImageIndex;
                        if (imageIndex != -1)
                        {
                            this.CurrentIcon = new Bitmap(this.imageComboBox_0.ImageList.Images[imageIndex], this.imageComboBox_0.ItemHeight, this.imageComboBox_0.ItemHeight);
                            if (this.imageComboBox_0.RightToLeft == RightToLeft.Yes)
                            {
                                this.CurrentIcon.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            }
                            this.int_0 = this.CurrentIcon.Width + 2;
                        }
                        else
                        {
                            this.CurrentIcon = null;
                            this.int_0 = 0;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public void SetMargin()
        {
            int num = 0;
            int num2 = 0;
            if (this.imageComboBox_0 != null)
            {
                if (this.imageComboBox_0.RightToLeft == RightToLeft.Yes)
                {
                    num2 = this.int_0;
                    SendMessage(base.Handle, 0xd3, 2, num2 * 0x10000);
                    SendMessage(base.Handle, 0xd3, 1, num);
                }
                else if (this.imageComboBox_0.RightToLeft == RightToLeft.No)
                {
                    num = this.int_0;
                    SendMessage(base.Handle, 0xd3, 1, this.int_0);
                    SendMessage(base.Handle, 0xd3, 2, num2 * 0x10000);
                }
            }
        }

        protected override void WndProc(ref Message message_0)
        {
            switch (message_0.Msg)
            {
                case 13:
                    base.WndProc(ref message_0);
                    this.DrawImage();
                    break;

                case 14:
                    base.WndProc(ref message_0);
                    this.DrawImage();
                    break;

                case 15:
                    base.WndProc(ref message_0);
                    this.DrawImage();
                    break;

                case 0x100:
                    base.WndProc(ref message_0);
                    this.DrawImage();
                    break;

                case 0x101:
                    base.WndProc(ref message_0);
                    this.DrawImage();
                    break;

                case 0x102:
                    base.WndProc(ref message_0);
                    this.DrawImage();
                    break;

                case 0x201:
                    base.WndProc(ref message_0);
                    this.DrawImage();
                    break;

                default:
                    base.WndProc(ref message_0);
                    break;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        public Image CurrentIcon
        {
            get
            {
                return this.image_0;
            }
            set
            {
                this.image_0 = value;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Struct2
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Struct3
        {
            public int cbSize;
            public ComboEditWindow.Struct2 rcItem;
            public ComboEditWindow.Struct2 rcButton;
            public IntPtr stateButton;
            public IntPtr hwndCombo;
            public IntPtr hwndEdit;
            public IntPtr hwndList;
        }
    }
}

