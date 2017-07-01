using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public partial class TemplatePropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IContainer icontainer_0 = null;

        public event OnValueChangeEventHandler OnValueChange;

        public TemplatePropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                if (this.textBox1.Text.Trim().Length != 0)
                {
                    CartoTemplateClass.Name = this.textBox1.Text;
                    CartoTemplateClass.Description = this.textBox2.Text;
                    if ((this.textBox3.Text.Length > 0) && File.Exists(this.textBox3.Text))
                    {
                        XmlDocument document = new XmlDocument();
                        document.Load(this.textBox3.Text);
                        CartoTemplateClass.LegendInfo = document.InnerXml;
                    }
                    try
                    {
                        CartoTemplateClass.Scale = double.Parse(this.txtScale.Text);
                    }
                    catch
                    {
                    }
                    try
                    {
                        CartoTemplateClass.Width = double.Parse(this.txtWidth.Text);
                    }
                    catch
                    {
                    }
                    try
                    {
                        CartoTemplateClass.Height = double.Parse(this.txtHeight.Text);
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "*.xml|*.xml"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox3.Text = dialog.FileName;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
                this.bool_1 = true;
            }
        }

        public bool CanApply()
        {
            if (this.textBox1.Text.Trim().Length == 0)
            {
                return false;
            }
            try
            {
                CartoTemplateClass.Scale = double.Parse(this.txtScale.Text);
            }
            catch
            {
                return false;
            }
            try
            {
                CartoTemplateClass.Width = double.Parse(this.txtWidth.Text);
            }
            catch
            {
                return false;
            }
            try
            {
                CartoTemplateClass.Height = double.Parse(this.txtHeight.Text);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void Cancel()
        {
            this.bool_1 = false;
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
        }

        private void TemplatePropertyPage_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = CartoTemplateClass.Name;
            this.textBox2.Text = CartoTemplateClass.Description;
            this.txtScale.Text = CartoTemplateClass.Scale.ToString();
            this.txtWidth.Text = CartoTemplateClass.Width.ToString();
            this.txtHeight.Text = CartoTemplateClass.Height.ToString();
            this.bool_0 = true;
        }

        private void txtHeight_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
                this.bool_1 = true;
            }
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
            get { return "图幅"; }
            set { }
        }
    }
}