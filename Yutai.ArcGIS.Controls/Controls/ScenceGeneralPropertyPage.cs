using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Analyst3D;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Controls.Controls
{
    public partial class ScenceGeneralPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private IScene m_Scence = null;
        private string m_Title = "常规";

        public event OnValueChangeEventHandler OnValueChange;

        public ScenceGeneralPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                this.m_Scence.Description = this.textBox1.Text;
                try
                {
                    this.m_Scence.ExaggerationFactor = double.Parse(this.comboBox1.Text);
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
                aspectRatio = this.m_Scence.Extent.Width/this.m_Scence.Extent.Height;
            }
            catch
            {
            }
            this.m_Scence.SuggestExaggerationFactor(aspectRatio, out num2);
            this.comboBox1.Text = num2.ToString("0.##");
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
            this.m_IsPageDirty = true;
        }

        public void Cancel()
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
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
            this.textBox1.Text = this.m_Scence.Description;
            this.comboBox1.Text = this.m_Scence.ExaggerationFactor.ToString();
            this.m_CanDo = true;
        }

        public void SetObjects(object @object)
        {
            this.m_Scence = @object as IScene;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        public bool IsPageDirty
        {
            get { return this.m_IsPageDirty; }
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
            set { this.m_Scence = value; }
        }

        public string Title
        {
            get { return this.m_Title; }
            set { this.m_Title = value; }
        }
    }
}