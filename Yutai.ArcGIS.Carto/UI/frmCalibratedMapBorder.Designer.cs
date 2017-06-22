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
    partial class frmCalibratedMapBorder
    {
        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
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
            this.txtBorderWidth.Location = new Point(71, 69);
            this.txtBorderWidth.Name = "txtBorderWidth";
            this.txtBorderWidth.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtBorderWidth.Properties.DisplayFormat.FormatString = "0.####";
            this.txtBorderWidth.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.txtBorderWidth.Properties.EditFormat.FormatString = "0.####";
            this.txtBorderWidth.Properties.EditFormat.FormatType = FormatType.Numeric;
            int[] bits2 = new int[4];
            bits2[0] = 100;
            this.txtBorderWidth.Properties.MaxValue = new decimal(bits2);
            this.txtBorderWidth.Size = new Size(80, 21);
            this.txtBorderWidth.TabIndex = 78;
            this.colorForegroundColor.EditValue = Color.Empty;
            this.colorForegroundColor.Location = new Point(71, 40);
            this.colorForegroundColor.Name = "colorForegroundColor";
            this.colorForegroundColor.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorForegroundColor.Size = new Size(48, 21);
            this.colorForegroundColor.TabIndex = 77;
            this.colorEditBackgroundColor.EditValue = Color.Empty;
            this.colorEditBackgroundColor.Location = new Point(71, 0);
            this.colorEditBackgroundColor.Name = "colorEditBackgroundColor";
            this.colorEditBackgroundColor.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEditBackgroundColor.Size = new Size(48, 21);
            this.colorEditBackgroundColor.TabIndex = 76;
            this.colorEditBackgroundColor.EditValueChanged += new EventHandler(this.colorEditBackgroundColor_EditValueChanged);
            this.label6.AutoSize = true;
            this.label6.Location = new Point(12, 40);
            this.label6.Name = "label6";
            this.label6.Size = new Size(53, 12);
            this.label6.TabIndex = 75;
            this.label6.Text = "空白颜色";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(12, 74);
            this.label3.Name = "label3";
            this.label3.Size = new Size(29, 12);
            this.label3.TabIndex = 74;
            this.label3.Text = "宽度";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(53, 12);
            this.label1.TabIndex = 72;
            this.label1.Text = "填充颜色";
            this.chkAlternating.Location = new Point(14, 134);
            this.chkAlternating.Name = "chkAlternating";
            this.chkAlternating.Properties.Caption = "使用双向交替边框";
            this.chkAlternating.Size = new Size(147, 19);
            this.chkAlternating.TabIndex = 82;
            int[] bits3 = new int[4];
            this.txtInterval.EditValue = new decimal(bits3);
            this.txtInterval.Location = new Point(71, 96);
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
            this.txtInterval.Size = new Size(80, 21);
            this.txtInterval.TabIndex = 81;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(12, 101);
            this.label5.Name = "label5";
            this.label5.Size = new Size(29, 12);
            this.label5.TabIndex = 80;
            this.label5.Text = "间距";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(157, 74);
            this.label2.Name = "label2";
            this.label2.Size = new Size(17, 12);
            this.label2.TabIndex = 83;
            this.label2.Text = "点";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(157, 101);
            this.label4.Name = "label4";
            this.label4.Size = new Size(17, 12);
            this.label4.TabIndex = 84;
            this.label4.Text = "点";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(55, 186);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(64, 24);
            this.btnOK.TabIndex = 86;
            this.btnOK.Text = "确定";
            this.btnOK.Visible = false;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(127, 186);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(64, 24);
            this.btnCancel.TabIndex = 85;
            this.btnCancel.Text = "取消";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(202, 222);
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
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
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

       
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private CheckEdit chkAlternating;
        private ColorEdit colorEditBackgroundColor;
        private ColorEdit colorForegroundColor;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private SpinEdit txtBorderWidth;
        private SpinEdit txtInterval;
    }
}