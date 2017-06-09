namespace JLK.ControlExtendEx
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;

    public sealed class DropDownDrawModes : UITypeEditor
    {
        private IWindowsFormsEditorService iwindowsFormsEditorService_0;
        public static object[] List;
        private ListBox listBox_0 = new ListBox();

        public DropDownDrawModes()
        {
            this.listBox_0.BorderStyle = BorderStyle.None;
            this.listBox_0.Click += new EventHandler(this.listBox_0_Click);
        }

        public override object EditValue(ITypeDescriptorContext itypeDescriptorContext_0, IServiceProvider iserviceProvider_0, object object_0)
        {
            this.listBox_0.Items.Clear();
            this.listBox_0.Items.AddRange(List);
            this.listBox_0.Height = this.listBox_0.PreferredHeight;
            this.iwindowsFormsEditorService_0 = (IWindowsFormsEditorService) iserviceProvider_0.GetService(typeof(IWindowsFormsEditorService));
            if (this.iwindowsFormsEditorService_0 != null)
            {
                this.iwindowsFormsEditorService_0.DropDownControl(this.listBox_0);
                return this.listBox_0.SelectedItem;
            }
            return object_0;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext itypeDescriptorContext_0)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        private void listBox_0_Click(object sender, EventArgs e)
        {
            this.iwindowsFormsEditorService_0.CloseDropDown();
        }
    }
}

