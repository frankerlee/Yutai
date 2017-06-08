using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class frmNewHatchClassName : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private Container container_0 = null;
        private string string_0 = "";
        private TextEdit txtHatchClassName;

        public frmNewHatchClassName()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.string_0 = this.txtHatchClassName.Text;
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewHatchClassName));
            this.txtHatchClassName = new TextEdit();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.txtHatchClassName.Properties.BeginInit();
            base.SuspendLayout();
            this.txtHatchClassName.EditValue = "Class Name";
            this.txtHatchClassName.Location = new Point(8, 0x10);
            this.txtHatchClassName.Name = "txtHatchClassName";
            this.txtHatchClassName.Size = new Size(0xe0, 0x15);
            this.txtHatchClassName.TabIndex = 0;
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x58, 0x30);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x40, 0x18);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0xa8, 0x30);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0xf8, 0x55);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.txtHatchClassName);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = ((System.Drawing.Icon)resources.GetObject("$Icon"));
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmNewHatchClassName";
            this.Text = "指定新刻度类的名称";
            this.txtHatchClassName.Properties.EndInit();
            base.ResumeLayout(false);
        }

        public string HatchClassName
        {
            get
            {
                return this.string_0;
            }
        }
    }
}

