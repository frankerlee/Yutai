using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
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
                (this.mapTemplateElement_0 as MapTemplateTextElement).Text = this.textBox1.Text;
                (this.mapTemplateElement_0 as MapTemplateTextElement).IsVerticalText = this.checkBox1.Checked;
            }
        }

        private void btnExpress_Click(object sender, EventArgs e)
        {
            frmExpressBulider bulider2 = new frmExpressBulider
            {
                MapTemplate = this.mapTemplateElement_0.MapTemplate,
                Expression = this.textBox1.Text
            };
            if (bulider2.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = bulider2.Expression;
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

        private void method_0()
        {
            if (this.mapTemplateElement_0 is MapTemplateTextElement)
            {
                this.bool_0 = false;
                this.textBox1.Text = (this.mapTemplateElement_0 as MapTemplateTextElement).Text;
                this.checkBox1.Checked = (this.mapTemplateElement_0 as MapTemplateTextElement).IsVerticalText;
                this.bool_0 = true;
            }
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_0();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.MapTemplateElement = object_0 as MapCartoTemplateLib.MapTemplateElement;
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
            this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
            this.method_0();
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

        public MapCartoTemplateLib.MapTemplateElement MapTemplateElement
        {
            set
            {
                this.mapTemplateElement_0 = value;
                this.method_0();
            }
        }

        public string Title
        {
            get { return "文本"; }
            set { }
        }
    }
}