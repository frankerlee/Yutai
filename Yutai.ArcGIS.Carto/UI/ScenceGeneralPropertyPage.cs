using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Analyst3D;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class ScenceGeneralPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IContainer icontainer_0 = null;
        private IScene iscene_0 = null;
        private string string_0 = "常规";

        public event OnValueChangeEventHandler OnValueChange;

        public ScenceGeneralPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                this.iscene_0.Description = this.textBox1.Text;
                try
                {
                    this.iscene_0.ExaggerationFactor = double.Parse(this.comboBox1.Text);
                }
                catch
                {
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double num2;
            double aspectRatio = 0.12;
            try
            {
                aspectRatio = this.iscene_0.Extent.Width/this.iscene_0.Extent.Height;
            }
            catch
            {
            }
            this.iscene_0.SuggestExaggerationFactor(aspectRatio, out num2);
            this.comboBox1.Text = num2.ToString("0.##");
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
            this.bool_1 = true;
        }

        public void Cancel()
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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

        public void ResetControl()
        {
        }

        private void ScenceGeneralPropertyPage_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = this.iscene_0.Description;
            this.comboBox1.Text = this.iscene_0.ExaggerationFactor.ToString();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.iscene_0 = object_0 as IScene;
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

        public IScene Scence
        {
            set { this.iscene_0 = value; }
        }

        public string Title
        {
            get { return this.string_0; }
            set { this.string_0 = value; }
        }
    }
}