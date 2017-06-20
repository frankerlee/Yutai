using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Carto.UI
{
    public class frmCalibratedMapBorder : Form
    {
        private bool bool_0 = true;
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private CheckEdit chkAlternating;
        private ColorEdit colorEditBackgroundColor;
        private ColorEdit colorForegroundColor;
        private ICalibratedMapGridBorder icalibratedMapGridBorder_0 = null;
        private IContainer icontainer_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private SpinEdit txtBorderWidth;
        private SpinEdit txtInterval;

        public frmCalibratedMapBorder()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            IColor foregroundColor = this.icalibratedMapGridBorder_0.ForegroundColor;
            this.method_2(this.colorForegroundColor, foregroundColor);
            this.icalibratedMapGridBorder_0.ForegroundColor = foregroundColor;
            foregroundColor = this.icalibratedMapGridBorder_0.BackgroundColor;
            this.method_2(this.colorEditBackgroundColor, foregroundColor);
            this.icalibratedMapGridBorder_0.BackgroundColor = foregroundColor;
            if (this.txtInterval.Value > 0M)
            {
                this.icalibratedMapGridBorder_0.Interval = (double) this.txtInterval.Value;
            }
            this.icalibratedMapGridBorder_0.BorderWidth = (double) this.txtBorderWidth.Value;
            this.icalibratedMapGridBorder_0.Alternating = this.chkAlternating.Checked;
            base.DialogResult = DialogResult.OK;
        }

        private void colorEditBackgroundColor_EditValueChanged(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void frmCalibratedMapBorder_Load(object sender, EventArgs e)
        {
            this.bool_0 = false;
            this.txtBorderWidth.Value = (decimal) this.icalibratedMapGridBorder_0.BorderWidth;
            this.txtInterval.Value = (decimal) this.icalibratedMapGridBorder_0.Interval;
            this.chkAlternating.Checked = this.icalibratedMapGridBorder_0.Alternating;
            this.method_3(this.colorEditBackgroundColor, this.icalibratedMapGridBorder_0.BackgroundColor);
            this.method_3(this.colorForegroundColor, this.icalibratedMapGridBorder_0.ForegroundColor);
            this.bool_0 = true;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCalibratedMapBorder));
            this.txtBorderWidth = new SpinEdit();
            this.colorForegroundColor = new ColorEdit();
            this.colorEditBackgroundColor = new ColorEdit();
            this.label6 = new Label();
            this.label3 = new Label();
            this.label1 = new Label();
            this.chkAlternating = new CheckEdit();
            this.txtInterval = new SpinEdit();
            this.label5 = new Label();
            this.label2 = new Label();
            this.label4 = new Label();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.txtBorderWidth.Properties.BeginInit();
            this.colorForegroundColor.Properties.BeginInit();
            this.colorEditBackgroundColor.Properties.BeginInit();
            this.chkAlternating.Properties.BeginInit();
            this.txtInterval.Properties.BeginInit();
            base.SuspendLayout();
            int[] bits = new int[4];
            this.txtBorderWidth.EditValue = new decimal(bits);
            this.txtBorderWidth.Location = new Point(0x47, 0x45);
            this.txtBorderWidth.Name = "txtBorderWidth";
            this.txtBorderWidth.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtBorderWidth.Properties.DisplayFormat.FormatString = "0.####";
            this.txtBorderWidth.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.txtBorderWidth.Properties.EditFormat.FormatString = "0.####";
            this.txtBorderWidth.Properties.EditFormat.FormatType = FormatType.Numeric;
            int[] bits2 = new int[4];
            bits2[0] = 100;
            this.txtBorderWidth.Properties.MaxValue = new decimal(bits2);
            this.txtBorderWidth.Size = new Size(80, 0x15);
            this.txtBorderWidth.TabIndex = 0x4e;
            this.colorForegroundColor.EditValue = Color.Empty;
            this.colorForegroundColor.Location = new Point(0x47, 40);
            this.colorForegroundColor.Name = "colorForegroundColor";
            this.colorForegroundColor.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorForegroundColor.Size = new Size(0x30, 0x15);
            this.colorForegroundColor.TabIndex = 0x4d;
            this.colorEditBackgroundColor.EditValue = Color.Empty;
            this.colorEditBackgroundColor.Location = new Point(0x47, 0);
            this.colorEditBackgroundColor.Name = "colorEditBackgroundColor";
            this.colorEditBackgroundColor.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEditBackgroundColor.Size = new Size(0x30, 0x15);
            this.colorEditBackgroundColor.TabIndex = 0x4c;
            this.colorEditBackgroundColor.EditValueChanged += new EventHandler(this.colorEditBackgroundColor_EditValueChanged);
            this.label6.AutoSize = true;
            this.label6.Location = new Point(12, 40);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x35, 12);
            this.label6.TabIndex = 0x4b;
            this.label6.Text = "空白颜色";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(12, 0x4a);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 12);
            this.label3.TabIndex = 0x4a;
            this.label3.Text = "宽度";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0x48;
            this.label1.Text = "填充颜色";
            this.chkAlternating.Location = new Point(14, 0x86);
            this.chkAlternating.Name = "chkAlternating";
            this.chkAlternating.Properties.Caption = "使用双向交替边框";
            this.chkAlternating.Size = new Size(0x93, 0x13);
            this.chkAlternating.TabIndex = 0x52;
            int[] bits3 = new int[4];
            this.txtInterval.EditValue = new decimal(bits3);
            this.txtInterval.Location = new Point(0x47, 0x60);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtInterval.Properties.DisplayFormat.FormatString = "0.####";
            this.txtInterval.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.txtInterval.Properties.EditFormat.FormatString = "0.####";
            this.txtInterval.Properties.EditFormat.FormatType = FormatType.Numeric;
            int[] bits4 = new int[4];
            bits4[0] = 100;
            this.txtInterval.Properties.MaxValue = new decimal(bits4);
            int[] bits5 = new int[4];
            bits5[0] = 100;
            bits5[3] = -2147483648;
            this.txtInterval.Properties.MinValue = new decimal(bits5);
            this.txtInterval.Size = new Size(80, 0x15);
            this.txtInterval.TabIndex = 0x51;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(12, 0x65);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 12);
            this.label5.TabIndex = 80;
            this.label5.Text = "间距";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x9d, 0x4a);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x11, 12);
            this.label2.TabIndex = 0x53;
            this.label2.Text = "点";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x9d, 0x65);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x11, 12);
            this.label4.TabIndex = 0x54;
            this.label4.Text = "点";
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x37, 0xba);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x40, 0x18);
            this.btnOK.TabIndex = 0x56;
            this.btnOK.Text = "确定";
            this.btnOK.Visible = false;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x7f, 0xba);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 0x55;
            this.btnCancel.Text = "取消";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0xca, 0xde);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.chkAlternating);
            base.Controls.Add(this.txtInterval);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.txtBorderWidth);
            base.Controls.Add(this.colorForegroundColor);
            base.Controls.Add(this.colorEditBackgroundColor);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmCalibratedMapBorder";
            this.Text = "校准框属性";
            base.Load += new EventHandler(this.frmCalibratedMapBorder_Load);
            this.txtBorderWidth.Properties.EndInit();
            this.colorForegroundColor.Properties.EndInit();
            this.colorEditBackgroundColor.Properties.EndInit();
            this.chkAlternating.Properties.EndInit();
            this.txtInterval.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void method_0(uint uint_0, out int int_0, out int int_1, out int int_2)
        {
            uint num = uint_0 & 0xff0000;
             int_2 = (int) (num >> 0x10);
            num = uint_0 & 0xff00;
            int_1 = (int) (num >> 8);
            num = uint_0 & 0xff;
            int_0 = (int) num;
        }

        private int method_1(int int_0, int int_1, int int_2)
        {
            uint num = 0;
            num = (uint) (0 | int_2);
            num = num << 8;
            num |=(uint) int_1;
            num = num << 8;
            return (int) (num | int_0);
        }

        private void method_2(ColorEdit colorEdit_0, IColor icolor_0)
        {
            icolor_0.NullColor = colorEdit_0.Color.IsEmpty;
            if (!colorEdit_0.Color.IsEmpty)
            {
                icolor_0.Transparency = colorEdit_0.Color.A;
                icolor_0.RGB = this.method_1(colorEdit_0.Color.R, colorEdit_0.Color.G, colorEdit_0.Color.B);
            }
        }

        private void method_3(ColorEdit colorEdit_0, IColor icolor_0)
        {
            if (icolor_0.NullColor)
            {
                colorEdit_0.Color = Color.Empty;
            }
            else
            {
                int num2;
                int num3;
                int num4;
                int rGB = icolor_0.RGB;
               this.method_0((uint) rGB, out num2, out num3, out num4);
                colorEdit_0.Color = Color.FromArgb(icolor_0.Transparency, num2, num3, num4);
            }
        }

        public ICalibratedMapGridBorder CalibratedMapGridBorder
        {
            get
            {
                return this.icalibratedMapGridBorder_0;
            }
            set
            {
                this.icalibratedMapGridBorder_0 = value;
            }
        }
    }
}

