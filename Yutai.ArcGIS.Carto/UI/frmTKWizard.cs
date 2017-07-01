using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class frmTKWizard : Form
    {
        private IActiveView iactiveView_0 = null;
        private IContainer icontainer_0 = null;
        private YTTKAssiatant jlktkassiatant_0 = new YTTKAssiatant();
        private short short_0 = 1;
        private TFInfoPage tfinfoPage_0 = new TFInfoPage();
        private TFTextInfoPage tftextInfoPage_0 = new TFTextInfoPage();
        private TKConfigPage tkconfigPage_0 = new TKConfigPage();
        private TKStyleSelectPage tkstyleSelectPage_0 = new TKStyleSelectPage();

        public frmTKWizard()
        {
            this.InitializeComponent();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            switch (this.short_0)
            {
                case 1:
                    return;

                case 2:
                    this.tkstyleSelectPage_0.Visible = true;
                    this.tfinfoPage_0.Visible = false;
                    this.btnLast.Enabled = false;
                    break;

                case 3:
                    this.tfinfoPage_0.Visible = true;
                    this.tkconfigPage_0.Visible = false;
                    break;

                case 4:
                    this.tkconfigPage_0.Visible = true;
                    this.tftextInfoPage_0.Visible = false;
                    this.btnNext.Text = "下一步>";
                    break;
            }
            this.short_0 = (short) (this.short_0 - 1);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            switch (this.short_0)
            {
                case 1:
                    if (!this.tkstyleSelectPage_0.Apply())
                    {
                        return;
                    }
                    this.tfinfoPage_0.Init();
                    this.tkstyleSelectPage_0.Visible = false;
                    this.tfinfoPage_0.Visible = true;
                    this.btnLast.Enabled = true;
                    break;

                case 2:
                    if (!this.tfinfoPage_0.Apply())
                    {
                        return;
                    }
                    this.tkconfigPage_0.Init();
                    this.tfinfoPage_0.Visible = false;
                    this.tkconfigPage_0.Visible = true;
                    break;

                case 3:
                    if (!this.tkconfigPage_0.Apply())
                    {
                        return;
                    }
                    this.tftextInfoPage_0.Init();
                    this.tkconfigPage_0.Visible = false;
                    this.tftextInfoPage_0.Visible = true;
                    this.btnNext.Text = "完成";
                    break;

                case 4:
                    if (this.tftextInfoPage_0.Apply())
                    {
                        this.jlktkassiatant_0.CreateTK(this.iactiveView_0 as IPageLayout);
                        base.DialogResult = DialogResult.OK;
                    }
                    return;
            }
            this.short_0 = (short) (this.short_0 + 1);
        }

        private void frmTKWizard_Load(object sender, EventArgs e)
        {
            this.tkstyleSelectPage_0.YTTKAssiatant = this.jlktkassiatant_0;
            this.tfinfoPage_0.YTTKAssiatant = this.jlktkassiatant_0;
            this.tkconfigPage_0.YTTKAssiatant = this.jlktkassiatant_0;
            this.tftextInfoPage_0.YTTKAssiatant = this.jlktkassiatant_0;
            this.panel1.Controls.Add(this.tkstyleSelectPage_0);
            this.panel1.Controls.Add(this.tfinfoPage_0);
            this.tfinfoPage_0.Visible = false;
            this.panel1.Controls.Add(this.tkconfigPage_0);
            this.tkconfigPage_0.Visible = false;
            this.panel1.Controls.Add(this.tftextInfoPage_0);
            this.tftextInfoPage_0.Visible = false;
        }

        public IActiveView ActiveView
        {
            set { this.iactiveView_0 = value; }
        }
    }
}