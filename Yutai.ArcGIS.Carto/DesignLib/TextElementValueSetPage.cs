using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public partial class TextElementValueSetPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IContainer icontainer_0 = null;

        public event OnValueChangeEventHandler OnValueChange;

        public TextElementValueSetPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                ElementWizardHelp.Text = this.textBox1.Text;
                ElementWizardHelp.IsVerticalText = this.checkBox1.Checked;
            }
        }

        public void Cancel()
        {
            this.bool_1 = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void method_0(object sender, EventArgs e)
        {
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void TextElementValueSetPage_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = ElementWizardHelp.Text;
            this.checkBox1.Checked = ElementWizardHelp.IsVerticalText;
            this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
            this.bool_0 = true;
        }

        public bool IsPageDirty
        {
            get { return this.bool_1; }
        }

        int IPropertyPage.Height
        {
            get { return base.Height; }
        }

        int IPropertyPage.Width
        {
            get { return base.Width; }
        }

        public string Title
        {
            get { return "文本"; }
            set { }
        }
    }
}