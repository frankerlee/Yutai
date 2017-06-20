using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Location;

namespace Yutai.ArcGIS.Carto.UI
{
    public class frmHatchAlignment : Form
    {
        private SimpleButton btnOK;
        private Container container_0 = null;
        private IHatchDefinition ihatchDefinition_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private RadioButton rdoCenter;
        private RadioButton rdoLeft;
        private RadioButton rdoRight;
        private SimpleButton simpleButton2;
        private SpinEdit txtSupplementalAngle;

        public frmHatchAlignment()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.rdoLeft.Checked)
            {
                this.ihatchDefinition_0.Alignment = esriHatchAlignmentType.esriHatchAlignLeft;
            }
            if (this.rdoCenter.Checked)
            {
                this.ihatchDefinition_0.Alignment = esriHatchAlignmentType.esriHatchAlignCenter;
            }
            if (this.rdoRight.Checked)
            {
                this.ihatchDefinition_0.Alignment = esriHatchAlignmentType.esriHatchAlignRight;
            }
            if (this.ihatchDefinition_0 is IHatchLineDefinition)
            {
                (this.ihatchDefinition_0 as IHatchLineDefinition).SupplementalAngle = (double) this.txtSupplementalAngle.Value;
            }
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmHatchAlignment_Load(object sender, EventArgs e)
        {
            if (this.ihatchDefinition_0 != null)
            {
                if (this.ihatchDefinition_0 is IHatchLineDefinition)
                {
                    this.txtSupplementalAngle.Value = (decimal) (this.ihatchDefinition_0 as IHatchLineDefinition).SupplementalAngle;
                    this.rdoCenter.Enabled = true;
                }
                else
                {
                    this.rdoCenter.Enabled = false;
                    this.txtSupplementalAngle.Enabled = false;
                }
                switch (this.ihatchDefinition_0.Alignment)
                {
                    case esriHatchAlignmentType.esriHatchAlignRight:
                        this.rdoLeft.Checked = false;
                        this.rdoCenter.Checked = false;
                        this.rdoRight.Checked = true;
                        break;

                    case esriHatchAlignmentType.esriHatchAlignCenter:
                        this.rdoLeft.Checked = false;
                        this.rdoCenter.Checked = true;
                        this.rdoRight.Checked = false;
                        break;

                    case esriHatchAlignmentType.esriHatchAlignLeft:
                        this.rdoLeft.Checked = true;
                        this.rdoCenter.Checked = false;
                        this.rdoRight.Checked = false;
                        break;
                }
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHatchAlignment));
            this.rdoLeft = new RadioButton();
            this.rdoCenter = new RadioButton();
            this.rdoRight = new RadioButton();
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtSupplementalAngle = new SpinEdit();
            this.label3 = new Label();
            this.btnOK = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.txtSupplementalAngle.Properties.BeginInit();
            base.SuspendLayout();
            this.rdoLeft.Location = new Point(0x10, 40);
            this.rdoLeft.Name = "rdoLeft";
            this.rdoLeft.Size = new Size(0x40, 0x10);
            this.rdoLeft.TabIndex = 0;
            this.rdoLeft.Text = "左边";
            this.rdoCenter.Location = new Point(0x58, 40);
            this.rdoCenter.Name = "rdoCenter";
            this.rdoCenter.Size = new Size(0x40, 0x10);
            this.rdoCenter.TabIndex = 1;
            this.rdoCenter.Text = "中心";
            this.rdoRight.Location = new Point(160, 40);
            this.rdoRight.Name = "rdoRight";
            this.rdoRight.Size = new Size(0x40, 0x10);
            this.rdoRight.TabIndex = 2;
            this.rdoRight.Text = "右边";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x40);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x101, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "刻度线垂直于基线。补角能被加到计算的角度上";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0xdd, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "刻度线能在基线的左边、右边或基线中心";
            int[] bits = new int[4];
            this.txtSupplementalAngle.EditValue = new decimal(bits);
            this.txtSupplementalAngle.Location = new Point(0x10, 0x58);
            this.txtSupplementalAngle.Name = "txtSupplementalAngle";
            this.txtSupplementalAngle.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtSupplementalAngle.Size = new Size(0x58, 0x15);
            this.txtSupplementalAngle.TabIndex = 5;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x80, 0x60);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x11, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "度";
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0xa8, 120);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.simpleButton2.DialogResult = DialogResult.Cancel;
            this.simpleButton2.Location = new Point(0xf8, 120);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(0x38, 0x18);
            this.simpleButton2.TabIndex = 8;
            this.simpleButton2.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(330, 0x9d);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.txtSupplementalAngle);
            base.Controls.Add(this.rdoRight);
            base.Controls.Add(this.rdoCenter);
            base.Controls.Add(this.rdoLeft);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmHatchAlignment";
            this.Text = "刻度线方向";
            base.Load += new EventHandler(this.frmHatchAlignment_Load);
            this.txtSupplementalAngle.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public IHatchDefinition HatchDefinition
        {
            get
            {
                return this.ihatchDefinition_0;
            }
            set
            {
                this.ihatchDefinition_0 = value;
            }
        }
    }
}

