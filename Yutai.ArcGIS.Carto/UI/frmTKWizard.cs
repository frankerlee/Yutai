using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class frmTKWizard : Form
    {
        private Button btnCancel;
        private Button btnLast;
        private Button btnNext;
        private IActiveView iactiveView_0 = null;
        private IContainer icontainer_0 = null;
        private YTTKAssiatant jlktkassiatant_0 = new YTTKAssiatant();
        private Panel panel1;
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

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTKWizard));
            this.btnNext = new Button();
            this.btnLast = new Button();
            this.btnCancel = new Button();
            this.panel1 = new Panel();
            base.SuspendLayout();
            this.btnNext.Location = new Point(0x16b, 0x182);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(0x40, 0x18);
            this.btnNext.TabIndex = 10;
            this.btnNext.Text = "下一步>";
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnLast.Enabled = false;
            this.btnLast.Location = new Point(0x121, 0x182);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(0x40, 0x18);
            this.btnLast.TabIndex = 8;
            this.btnLast.Text = "<上一步";
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x1b1, 0x182);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1f6, 0x170);
            this.panel1.TabIndex = 11;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1f6, 0x1a7);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnNext);
            base.Controls.Add(this.btnLast);
            base.Controls.Add(this.btnCancel);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmTKWizard";
            this.Text = "图框向导";
            base.Load += new EventHandler(this.frmTKWizard_Load);
            base.ResumeLayout(false);
        }

        public IActiveView ActiveView
        {
            set
            {
                this.iactiveView_0 = value;
            }
        }
    }
}

