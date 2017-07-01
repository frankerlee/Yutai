using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public partial class frmSelectCartoTemplateWizard : Form
    {
        private CartoTemplateData cartoTemplateData_0 = null;
        private CatoTemplateApplySelect catoTemplateApplySelect_0 = new CatoTemplateApplySelect();
        private IContainer icontainer_0 = null;
        private int int_0 = 0;
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

        private void frmSelectCartoTemplateWizard_Load(object sender, EventArgs e)
        {
            this.catoTemplateApplySelect_0.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(this.catoTemplateApplySelect_0);
            this.templateParamSetPage_0.Dock = DockStyle.Fill;
            this.templateParamSetPage_0.Visible = false;
            this.panel1.Controls.Add(this.templateParamSetPage_0);
        }

        internal CartoTemplateData CartoTemplateData
        {
            get { return this.cartoTemplateData_0; }
        }

        public System.Collections.Hashtable Hashtable
        {
            set { this.templateParamSetPage_0.Hashtable = value; }
        }

        public IRow Row
        {
            get { return this.cartoTemplateData_0.Row; }
        }
    }
}