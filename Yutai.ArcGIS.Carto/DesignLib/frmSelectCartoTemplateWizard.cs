using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class frmSelectCartoTemplateWizard : Form
    {
        private Button btnLast;
        private Button btnNext;
        private Button button3;
        private CartoTemplateData cartoTemplateData_0 = null;
        private CatoTemplateApplySelect catoTemplateApplySelect_0 = new CatoTemplateApplySelect();
        private IContainer icontainer_0 = null;
        private int int_0 = 0;
        private Panel panel1;
        private TemplateParamSetPage templateParamSetPage_0 = new TemplateParamSetPage();

        public frmSelectCartoTemplateWizard()
        {
            this.InitializeComponent();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            switch (this.int_0)
            {
                case 0:
                    return;

                case 1:
                    this.btnNext.Text = "下一步>";
                    this.catoTemplateApplySelect_0.Visible = true;
                    this.templateParamSetPage_0.Visible = false;
                    break;
            }
            this.int_0--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            switch (this.int_0)
            {
                case 0:
                    this.cartoTemplateData_0 = this.catoTemplateApplySelect_0.CartoTemplateData;
                    if (this.cartoTemplateData_0 != null)
                    {
                        this.catoTemplateApplySelect_0.Visible = false;
                        this.templateParamSetPage_0.TemplateOID = this.cartoTemplateData_0.OID;
                        this.btnNext.Text = "完成";
                        this.templateParamSetPage_0.Visible = true;
                        break;
                    }
                    MessageBox.Show("请选择模板！");
                    return;

                case 1:
                    this.templateParamSetPage_0.Apply();
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

        private void frmSelectCartoTemplateWizard_Load(object sender, EventArgs e)
        {
            this.catoTemplateApplySelect_0.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(this.catoTemplateApplySelect_0);
            this.templateParamSetPage_0.Dock = DockStyle.Fill;
            this.templateParamSetPage_0.Visible = false;
            this.panel1.Controls.Add(this.templateParamSetPage_0);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectCartoTemplateWizard));
            this.button3 = new Button();
            this.btnNext = new Button();
            this.btnLast = new Button();
            this.panel1 = new Panel();
            base.SuspendLayout();
            this.button3.DialogResult = DialogResult.Cancel;
            this.button3.Location = new Point(0x181, 290);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x4b, 0x17);
            this.button3.TabIndex = 14;
            this.button3.Text = "取消";
            this.button3.UseVisualStyleBackColor = true;
            this.btnNext.Location = new Point(0x12d, 290);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(0x4b, 0x17);
            this.btnNext.TabIndex = 13;
            this.btnNext.Text = "下一步>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnLast.Location = new Point(220, 290);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(0x4b, 0x17);
            this.btnLast.TabIndex = 11;
            this.btnLast.Text = "<上一步";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1d3, 0x113);
            this.panel1.TabIndex = 12;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1d3, 0x145);
            base.Controls.Add(this.button3);
            base.Controls.Add(this.btnNext);
            base.Controls.Add(this.btnLast);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmSelectCartoTemplateWizard";
            this.Text = "选择模板";
            base.Load += new EventHandler(this.frmSelectCartoTemplateWizard_Load);
            base.ResumeLayout(false);
        }

        internal CartoTemplateData CartoTemplateData
        {
            get
            {
                return this.cartoTemplateData_0;
            }
        }

        public System.Collections.Hashtable Hashtable
        {
            set
            {
                this.templateParamSetPage_0.Hashtable = value;
            }
        }

        public IRow Row
        {
            get
            {
                return this.cartoTemplateData_0.Row;
            }
        }
    }
}

