using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class frmParamProperty : Form
    {
        private bool bool_0 = false;
        private Button btnCancel;
        private Button btnOK;
        private IContainer icontainer_0 = null;
        private Label label1;
        private Label label2;
        private ParamInfo paramInfo_0 = null;
        private TextBox txtDescription;
        private TextBox txtName;

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

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void frmParamProperty_Load(object sender, EventArgs e)
        {
            if (this.paramInfo_0 != null)
            {
                this.txtName.Text = this.paramInfo_0.Name;
                this.txtDescription.Text = this.paramInfo_0.Description;
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmParamProperty));
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtName = new TextBox();
            this.txtDescription = new TextBox();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(11, 0x16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 0x3b);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "说明";
            this.txtName.Location = new Point(0x2e, 0x16);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(0xa9, 0x15);
            this.txtName.TabIndex = 2;
            this.txtDescription.Location = new Point(0x2e, 0x3b);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Size(0xa9, 0x7a);
            this.txtDescription.TabIndex = 3;
            this.btnOK.Location = new Point(0x5b, 0xbb);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x3b, 0x17);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x9c, 0xbb);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x3b, 0x17);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0xea, 0xde);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.txtDescription);
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon)resources.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmParamProperty";
            this.Text = "参数";
            base.Load += new EventHandler(this.frmParamProperty_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
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

