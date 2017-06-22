using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public partial class frmParamProperty : Form
    {
        private bool bool_0 = false;
        private IContainer icontainer_0 = null;
        private ParamInfo paramInfo_0 = null;

        public frmParamProperty()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtName.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入参数名称!");
            }
            else
            {
                if (this.paramInfo_0 == null)
                {
                    this.paramInfo_0 = new ParamInfo();
                }
                this.paramInfo_0.Caption = this.txtName.Text.Trim();
                this.paramInfo_0.Name = this.txtName.Text;
                this.paramInfo_0.Description = this.txtDescription.Text;
                base.DialogResult = DialogResult.OK;
            }
        }

 private void frmParamProperty_Load(object sender, EventArgs e)
        {
            if (this.paramInfo_0 != null)
            {
                this.txtName.Text = this.paramInfo_0.Name;
                this.txtDescription.Text = this.paramInfo_0.Description;
            }
        }

 public ParamInfo ParamInfo
        {
            get
            {
                return this.paramInfo_0;
            }
            set
            {
                this.paramInfo_0 = value;
            }
        }
    }
}

