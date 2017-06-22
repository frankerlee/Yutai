using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Yutai.ArcGIS.Common.ExtendClass;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class frmJTBElement : Form
    {
        private Container container_0 = null;
        private IJTBElement ijtbelement_0 = null;
        private string string_0 = "";

        public frmJTBElement()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.ijtbelement_0.BottomName = this.txtB.Text;
            this.ijtbelement_0.TFName = this.txtC.Text;
            this.ijtbelement_0.LeftName = this.txtL.Text;
            this.ijtbelement_0.LeftBottomName = this.txtLB.Text;
            this.ijtbelement_0.LeftTopName = this.txtLT.Text;
            this.ijtbelement_0.RightName = this.txtR.Text;
            this.ijtbelement_0.RightBottomName = this.txtRB.Text;
            this.ijtbelement_0.RightTopName = this.txtRT.Text;
            this.ijtbelement_0.TopName = this.txtT.Text;
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

 private void frmJTBElement_Load(object sender, EventArgs e)
        {
            this.txtB.Text = this.ijtbelement_0.BottomName;
            this.txtC.Text = this.ijtbelement_0.TFName;
            this.txtC.Text = this.string_0;
            this.txtL.Text = this.ijtbelement_0.LeftName;
            this.txtLB.Text = this.ijtbelement_0.LeftBottomName;
            this.txtLT.Text = this.ijtbelement_0.LeftTopName;
            this.txtR.Text = this.ijtbelement_0.RightName;
            this.txtRB.Text = this.ijtbelement_0.RightBottomName;
            this.txtRT.Text = this.ijtbelement_0.RightTopName;
            this.txtT.Text = this.ijtbelement_0.TopName;
        }

 public IJTBElement JTBElement
        {
            set
            {
                this.ijtbelement_0 = value;
            }
        }

        public string MapName
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}

