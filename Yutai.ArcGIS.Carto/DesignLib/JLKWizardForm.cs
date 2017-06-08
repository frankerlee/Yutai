using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class JLKWizardForm : Form
    {
        private Button btnLast;
        private Button btnNext;
        private Button button3;
        private IContainer icontainer_0 = null;
        private int int_0 = 0;
        private Panel panel1;
        private TemplatePropertyPage templatePropertyPage_0 = new TemplatePropertyPage();
        private TFInfoPage tfinfoPage_0 = new TFInfoPage();

        public JLKWizardForm()
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

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(JLKWizardForm));
            this.panel1 = new Panel();
            this.btnLast = new Button();
            this.btnNext = new Button();
            this.button3 = new Button();
            base.SuspendLayout();
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x179, 0xea);
            this.panel1.TabIndex = 8;
            this.btnLast.Location = new Point(0x8a, 0xf9);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(0x4b, 0x17);
            this.btnLast.TabIndex = 0;
            this.btnLast.Text = "<上一步";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.btnNext.Location = new Point(0xdb, 0xf9);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(0x4b, 0x17);
            this.btnNext.TabIndex = 9;
            this.btnNext.Text = "下一步>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.button3.DialogResult = DialogResult.Cancel;
            this.button3.Location = new Point(0x12f, 0xf9);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x4b, 0x17);
            this.button3.TabIndex = 10;
            this.button3.Text = "取消";
            this.button3.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x179, 0x113);
            base.Controls.Add(this.button3);
            base.Controls.Add(this.btnNext);
            base.Controls.Add(this.btnLast);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon)resources.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "JLKWizardForm";
            this.Text = "模板向导";
            base.Load += new EventHandler(this.JLKWizardForm_Load);
            base.ResumeLayout(false);
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

