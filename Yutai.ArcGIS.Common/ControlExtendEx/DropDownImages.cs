namespace JLK.ControlExtendEx
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;

    public sealed class DropDownImages : UITypeEditor
    {
        public static ImageList imageList;
        private ItemImagesContainer itemImagesContainer_0 = new ItemImagesContainer();
        private IWindowsFormsEditorService iwindowsFormsEditorService_0;

        static DropDownImages()
        {
            old_acctor_mc();
        }

        public DropDownImages()
        {
            this.itemImagesContainer_0.AfterSelectEvent += new ItemImagesContainer.AfterSelectEventHandler(this.itemImagesContainer_0_AfterSelectEvent);
        }

        public override object EditValue(ITypeDescriptorContext itypeDescriptorContext_0, IServiceProvider iserviceProvider_0, object object_0)
        {
            this.iwindowsFormsEditorService_0 = (IWindowsFormsEditorService) iserviceProvider_0.GetService(typeof(IWindowsFormsEditorService));
            this.itemImagesContainer_0.ImageList = imageList;
            if (this.iwindowsFormsEditorService_0 != null)
            {
                this.iwindowsFormsEditorService_0.DropDownControl(this.itemImagesContainer_0);
                if (this.itemImagesContainer_0.SelectedItem != string.Empty)
                {
                    return this.itemImagesContainer_0.SelectedItem.ToString();
                }
            }
            return object_0;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext itypeDescriptorContext_0)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override bool GetPaintValueSupported(ITypeDescriptorContext itypeDescriptorContext_0)
        {
            return true;
        }

        private void itemImagesContainer_0_AfterSelectEvent(bool bool_0)
        {
            if (bool_0)
            {
                this.iwindowsFormsEditorService_0.CloseDropDown();
            }
        }

        private static void old_acctor_mc()
        {
            imageList = null;
        }

        public override void PaintValue(PaintValueEventArgs paintValueEventArgs_0)
        {
            if (paintValueEventArgs_0.Value.ToString().CompareTo("(none)") == 0)
            {
                paintValueEventArgs_0.Graphics.DrawImage(this.itemImagesContainer_0.ListBoxIcon, paintValueEventArgs_0.Bounds);
                string s = paintValueEventArgs_0.Value.ToString();
                paintValueEventArgs_0.Graphics.DrawString(s, this.itemImagesContainer_0.Font, new SolidBrush(this.itemImagesContainer_0.ForeColor), new Rectangle(paintValueEventArgs_0.Bounds.Width + 10, 0, ((int) paintValueEventArgs_0.Graphics.MeasureString(s, this.itemImagesContainer_0.Font).Width) + 10, paintValueEventArgs_0.Bounds.Height));
            }
            if ((imageList != null) && (imageList.Images.Count > 0))
            {
                Image image = imageList.Images[Convert.ToInt32(paintValueEventArgs_0.Value.ToString())];
                paintValueEventArgs_0.Graphics.DrawImage(image, paintValueEventArgs_0.Bounds);
            }
        }
    }
}

