using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public partial class YTWizardForm : Form
    {
        private IContainer icontainer_0 = null;
        private int int_0 = 0;
        private TemplatePropertyPage templatePropertyPage_0 = new TemplatePropertyPage();
        private TFInfoPage tfinfoPage_0 = new TFInfoPage();

        public YTWizardForm()
        {
            this.InitializeComponent();
            CartoTemplateClass.Reset();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            switch (this.int_0)
            {
                case 0:
                    return;

                case 1:
                    this.btnNext.Text = "下一步>";
                    this.templatePropertyPage_0.Visible = true;
                    this.tfinfoPage_0.Visible = false;
                    break;
            }
            this.int_0--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            switch (this.int_0)
            {
                case 0:
                    if (!this.templatePropertyPage_0.CanApply())
                    {
                        return;
                    }
                    this.templatePropertyPage_0.Apply();
                    this.templatePropertyPage_0.Visible = false;
                    this.tfinfoPage_0.Visible = true;
                    this.btnNext.Text = "完成";
                    break;

                case 1:
                    this.tfinfoPage_0.Apply();
                    base.DialogResult = DialogResult.OK;
                    return;
            }
            this.int_0++;
        }

        private void JLKWizardForm_Load(object sender, EventArgs e)
        {
            this.templatePropertyPage_0.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(this.templatePropertyPage_0);
            this.tfinfoPage_0.Dock = DockStyle.Fill;
            this.tfinfoPage_0.Visible = false;
            this.panel1.Controls.Add(this.tfinfoPage_0);
        }
    }
}