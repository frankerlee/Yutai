using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public class GridHatchPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private StyleButton btnStyle;
        private CheckEdit chkHatchDirectional;
        private ComboBoxEdit comboBoxEdit1;
        private ComboBoxEdit comboBoxEdit2;
        private Container components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label1;
        private Label label13;
        private Label label14;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private string m_Title = "细切线";
        private SpinEdit txtAxisTickOffset;
        private TextEdit txtHatchIntervalXDegree;
        private TextEdit txtHatchIntervalXMinute;
        private TextEdit txtHatchIntervalXSecond;
        private TextEdit txtHatchIntervalYDegree;
        private TextEdit txtHatchIntervalYMinute;
        private TextEdit txtHatchIntervalYSecond;
        private SpinEdit txtHatchLength;

        public event OnValueChangeEventHandler OnValueChange;

        public GridHatchPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                IGridHatch pMapGrid = GridAxisPropertyPage.m_pMapGrid as IGridHatch;
                double num = double.Parse(this.txtHatchIntervalXDegree.Text);
                double num2 = double.Parse(this.txtHatchIntervalXMinute.Text);
                double num3 = double.Parse(this.txtHatchIntervalXSecond.Text);
                pMapGrid.HatchIntervalX = (num + (num2 / 60.0)) + (num3 / 3600.0);
                num = double.Parse(this.txtHatchIntervalYDegree.Text);
                num2 = double.Parse(this.txtHatchIntervalYMinute.Text);
                num3 = double.Parse(this.txtHatchIntervalYSecond.Text);
                pMapGrid.HatchIntervalY = (num + (num2 / 60.0)) + (num3 / 3600.0);
                pMapGrid.HatchLength = double.Parse(this.txtHatchLength.Text);
                pMapGrid.HatchDirectional = this.chkHatchDirectional.Checked;
                if (this.btnStyle.Style is ILineSymbol)
                {
                    pMapGrid.HatchLineSymbol = this.btnStyle.Style as ILineSymbol;
                }
                else
                {
                    pMapGrid.HatchMarkerSymbol = this.btnStyle.Style as IMarkerSymbol;
                }
                (pMapGrid as IGridAxisTicks).AxisTickOffset = double.Parse(this.txtAxisTickOffset.Text);
            }
        }

        public void Cancel()
        {
        }

        private void chkHatchDirectional_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        public void DegreeToDMS(double Degree, out double d, out double m, out double s)
        {
            int num = Math.Sign(Degree);
            Degree = Math.Abs(Degree);
            d = Math.Floor(Degree);
            Degree = (Degree - d) * 60.0;
            m = Math.Floor(Degree);
            s = (Degree - m) * 60.0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void GridHatchPropertyPage_Load(object sender, EventArgs e)
        {
            double num;
            double num2;
            double num3;
            IGridHatch pMapGrid = GridAxisPropertyPage.m_pMapGrid as IGridHatch;
            this.DegreeToDMS(pMapGrid.HatchIntervalX, out num, out num2, out num3);
            this.txtHatchIntervalXDegree.Text = num.ToString();
            this.txtHatchIntervalXMinute.Text = num2.ToString();
            this.txtHatchIntervalXSecond.Text = num3.ToString();
            this.DegreeToDMS(pMapGrid.HatchIntervalY, out num, out num2, out num3);
            this.txtHatchIntervalYDegree.Text = num.ToString();
            this.txtHatchIntervalYMinute.Text = num2.ToString();
            this.txtHatchIntervalYSecond.Text = num3.ToString();
            this.txtHatchLength.Text = pMapGrid.HatchLength.ToString();
            this.chkHatchDirectional.Checked = pMapGrid.HatchDirectional;
            if (pMapGrid.HatchLineSymbol != null)
            {
                this.btnStyle.Style = pMapGrid.HatchLineSymbol;
            }
            else
            {
                this.btnStyle.Style = pMapGrid.HatchMarkerSymbol;
            }
            this.txtAxisTickOffset.Text = (pMapGrid as IGridAxisTicks).AxisTickOffset.ToString();
            this.m_CanDo = true;
        }

        public void Hide()
        {
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.label9 = new Label();
            this.label8 = new Label();
            this.label7 = new Label();
            this.txtHatchIntervalYSecond = new TextEdit();
            this.txtHatchIntervalYMinute = new TextEdit();
            this.txtHatchIntervalXSecond = new TextEdit();
            this.txtHatchIntervalXMinute = new TextEdit();
            this.label3 = new Label();
            this.label4 = new Label();
            this.txtHatchIntervalYDegree = new TextEdit();
            this.txtHatchIntervalXDegree = new TextEdit();
            this.label5 = new Label();
            this.label6 = new Label();
            this.groupBox2 = new GroupBox();
            this.label13 = new Label();
            this.label14 = new Label();
            this.txtHatchLength = new SpinEdit();
            this.comboBoxEdit1 = new ComboBoxEdit();
            this.btnStyle = new StyleButton();
            this.chkHatchDirectional = new CheckEdit();
            this.groupBox3 = new GroupBox();
            this.comboBoxEdit2 = new ComboBoxEdit();
            this.txtAxisTickOffset = new SpinEdit();
            this.label1 = new Label();
            this.groupBox1.SuspendLayout();
            this.txtHatchIntervalYSecond.Properties.BeginInit();
            this.txtHatchIntervalYMinute.Properties.BeginInit();
            this.txtHatchIntervalXSecond.Properties.BeginInit();
            this.txtHatchIntervalXMinute.Properties.BeginInit();
            this.txtHatchIntervalYDegree.Properties.BeginInit();
            this.txtHatchIntervalXDegree.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.txtHatchLength.Properties.BeginInit();
            this.comboBoxEdit1.Properties.BeginInit();
            this.chkHatchDirectional.Properties.BeginInit();
            this.groupBox3.SuspendLayout();
            this.comboBoxEdit2.Properties.BeginInit();
            this.txtAxisTickOffset.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtHatchIntervalYSecond);
            this.groupBox1.Controls.Add(this.txtHatchIntervalYMinute);
            this.groupBox1.Controls.Add(this.txtHatchIntervalXSecond);
            this.groupBox1.Controls.Add(this.txtHatchIntervalXMinute);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtHatchIntervalYDegree);
            this.groupBox1.Controls.Add(this.txtHatchIntervalXDegree);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x110, 0x70);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "细切线间隔";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0xb0, 0x18);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x11, 0x11);
            this.label9.TabIndex = 0x19;
            this.label9.Text = "秒";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x88, 0x18);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x11, 0x11);
            this.label8.TabIndex = 0x18;
            this.label8.Text = "分";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x60, 0x18);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x11, 0x11);
            this.label7.TabIndex = 0x17;
            this.label7.Text = "度";
            this.txtHatchIntervalYSecond.EditValue = "";
            this.txtHatchIntervalYSecond.Location = new Point(160, 80);
            this.txtHatchIntervalYSecond.Name = "txtHatchIntervalYSecond";
            this.txtHatchIntervalYSecond.Size = new Size(0x20, 0x17);
            this.txtHatchIntervalYSecond.TabIndex = 0x16;
            this.txtHatchIntervalYSecond.EditValueChanged += new EventHandler(this.txtHatchIntervalXDegree_EditValueChanged);
            this.txtHatchIntervalYMinute.EditValue = "";
            this.txtHatchIntervalYMinute.Location = new Point(120, 80);
            this.txtHatchIntervalYMinute.Name = "txtHatchIntervalYMinute";
            this.txtHatchIntervalYMinute.Size = new Size(0x20, 0x17);
            this.txtHatchIntervalYMinute.TabIndex = 0x15;
            this.txtHatchIntervalYMinute.EditValueChanged += new EventHandler(this.txtHatchIntervalXDegree_EditValueChanged);
            this.txtHatchIntervalXSecond.EditValue = "";
            this.txtHatchIntervalXSecond.Location = new Point(160, 0x30);
            this.txtHatchIntervalXSecond.Name = "txtHatchIntervalXSecond";
            this.txtHatchIntervalXSecond.Size = new Size(0x20, 0x17);
            this.txtHatchIntervalXSecond.TabIndex = 20;
            this.txtHatchIntervalXSecond.EditValueChanged += new EventHandler(this.txtHatchIntervalXDegree_EditValueChanged);
            this.txtHatchIntervalXMinute.EditValue = "";
            this.txtHatchIntervalXMinute.Location = new Point(120, 0x30);
            this.txtHatchIntervalXMinute.Name = "txtHatchIntervalXMinute";
            this.txtHatchIntervalXMinute.Size = new Size(0x20, 0x17);
            this.txtHatchIntervalXMinute.TabIndex = 0x13;
            this.txtHatchIntervalXMinute.EditValueChanged += new EventHandler(this.txtHatchIntervalXDegree_EditValueChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(160, 0x48);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0, 0x11);
            this.label3.TabIndex = 0x12;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(160, 0x30);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0, 0x11);
            this.label4.TabIndex = 0x11;
            this.txtHatchIntervalYDegree.EditValue = "";
            this.txtHatchIntervalYDegree.Location = new Point(0x40, 80);
            this.txtHatchIntervalYDegree.Name = "txtHatchIntervalYDegree";
            this.txtHatchIntervalYDegree.Size = new Size(0x30, 0x17);
            this.txtHatchIntervalYDegree.TabIndex = 0x10;
            this.txtHatchIntervalYDegree.EditValueChanged += new EventHandler(this.txtHatchIntervalXDegree_EditValueChanged);
            this.txtHatchIntervalXDegree.EditValue = "";
            this.txtHatchIntervalXDegree.Location = new Point(0x40, 0x30);
            this.txtHatchIntervalXDegree.Name = "txtHatchIntervalXDegree";
            this.txtHatchIntervalXDegree.Size = new Size(0x30, 0x17);
            this.txtHatchIntervalXDegree.TabIndex = 15;
            this.txtHatchIntervalXDegree.EditValueChanged += new EventHandler(this.txtHatchIntervalXDegree_EditValueChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x10, 80);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 0x11);
            this.label5.TabIndex = 14;
            this.label5.Text = "经度";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x10, 0x30);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x1d, 0x11);
            this.label6.TabIndex = 13;
            this.label6.Text = "纬度";
            this.groupBox2.Controls.Add(this.chkHatchDirectional);
            this.groupBox2.Controls.Add(this.btnStyle);
            this.groupBox2.Controls.Add(this.comboBoxEdit1);
            this.groupBox2.Controls.Add(this.txtHatchLength);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Location = new Point(8, 0x80);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x110, 120);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "细切线标记";
            this.label13.AutoSize = true;
            this.label13.Location = new Point(0x10, 0x38);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x1d, 0x11);
            this.label13.TabIndex = 14;
            this.label13.Text = "长度";
            this.label14.AutoSize = true;
            this.label14.Location = new Point(0x10, 0x18);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x1d, 0x11);
            this.label14.TabIndex = 13;
            this.label14.Text = "符号";
            int[] bits = new int[4];
            this.txtHatchLength.EditValue = new decimal(bits);
            this.txtHatchLength.Location = new Point(0x38, 0x38);
            this.txtHatchLength.Name = "txtHatchLength";
            this.txtHatchLength.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtHatchLength.Properties.UseCtrlIncrement = false;
            this.txtHatchLength.Size = new Size(80, 0x17);
            this.txtHatchLength.TabIndex = 15;
            this.txtHatchLength.EditValueChanged += new EventHandler(this.txtHatchLength_EditValueChanged);
            this.comboBoxEdit1.EditValue = "点";
            this.comboBoxEdit1.Location = new Point(0x98, 0x38);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.comboBoxEdit1.Properties.Enabled = false;
            this.comboBoxEdit1.Properties.Items.AddRange(new object[] { "点", "厘米", "毫米", "英寸" });
            this.comboBoxEdit1.Size = new Size(0x38, 0x17);
            this.comboBoxEdit1.TabIndex = 0x10;
            this.btnStyle.Location = new Point(0x38, 0x18);
            this.btnStyle.Name = "btnStyle";
            this.btnStyle.Size = new Size(80, 0x18);
            this.btnStyle.TabIndex = 0x11;
            this.chkHatchDirectional.Location = new Point(0x10, 0x58);
            this.chkHatchDirectional.Name = "chkHatchDirectional";
            this.chkHatchDirectional.Properties.Caption = "细切标记显示相对于中央子午线和赤道方向";
            this.chkHatchDirectional.Size = new Size(0x100, 0x13);
            this.chkHatchDirectional.TabIndex = 0x12;
            this.chkHatchDirectional.CheckedChanged += new EventHandler(this.chkHatchDirectional_CheckedChanged);
            this.groupBox3.Controls.Add(this.comboBoxEdit2);
            this.groupBox3.Controls.Add(this.txtAxisTickOffset);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new Point(8, 0x100);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x110, 0x48);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "轴刻度";
            this.comboBoxEdit2.EditValue = "点";
            this.comboBoxEdit2.Location = new Point(0xb8, 0x18);
            this.comboBoxEdit2.Name = "comboBoxEdit2";
            this.comboBoxEdit2.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.comboBoxEdit2.Properties.Enabled = false;
            this.comboBoxEdit2.Properties.Items.AddRange(new object[] { "点", "厘米", "毫米", "英寸" });
            this.comboBoxEdit2.Size = new Size(0x38, 0x17);
            this.comboBoxEdit2.TabIndex = 0x10;
            bits = new int[4];
            this.txtAxisTickOffset.EditValue = new decimal(bits);
            this.txtAxisTickOffset.Location = new Point(80, 0x18);
            this.txtAxisTickOffset.Name = "txtAxisTickOffset";
            this.txtAxisTickOffset.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtAxisTickOffset.Properties.UseCtrlIncrement = false;
            this.txtAxisTickOffset.Size = new Size(80, 0x17);
            this.txtAxisTickOffset.TabIndex = 15;
            this.txtAxisTickOffset.EditValueChanged += new EventHandler(this.txtAxisTickOffset_EditValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x42, 0x11);
            this.label1.TabIndex = 14;
            this.label1.Text = "轴刻度偏移";
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "GridHatchPropertyPage";
            base.Size = new Size(0x138, 0x158);
            base.Load += new EventHandler(this.GridHatchPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.txtHatchIntervalYSecond.Properties.EndInit();
            this.txtHatchIntervalYMinute.Properties.EndInit();
            this.txtHatchIntervalXSecond.Properties.EndInit();
            this.txtHatchIntervalXMinute.Properties.EndInit();
            this.txtHatchIntervalYDegree.Properties.EndInit();
            this.txtHatchIntervalXDegree.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.txtHatchLength.Properties.EndInit();
            this.comboBoxEdit1.Properties.EndInit();
            this.chkHatchDirectional.Properties.EndInit();
            this.groupBox3.ResumeLayout(false);
            this.comboBoxEdit2.Properties.EndInit();
            this.txtAxisTickOffset.Properties.EndInit();
            base.ResumeLayout(false);
        }

        public void SetObjects(object @object)
        {
        }

        private void txtAxisTickOffset_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void txtHatchIntervalXDegree_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void txtHatchLength_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.m_IsPageDirty;
            }
        }

        int IPropertyPage.Height
        {
            get
            {
                return base.Height;
            }
        }

        int IPropertyPage.Width
        {
            get
            {
                return base.Width;
            }
        }

        public string Title
        {
            get
            {
                return this.m_Title;
            }
            set
            {
                this.m_Title = value;
            }
        }
    }
}

