using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.UI
{
    public class ScaleAndUnitsPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private SimpleButton btnSymbolSelector;
        private ComboBoxEdit cboLabelPosition;
        private ComboBoxEdit cboResizeHint;
        private ComboBoxEdit cboUnits;
        private CheckEdit chkDivisionsBeforeZero;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private IMapSurroundFrame imapSurroundFrame_0 = null;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private string string_0 = "比例和单位";
        private TextEdit textEdit1;
        private SpinEdit txtDivisions;
        private SpinEdit txtGap;
        private TextEdit txtLabel;
        private SpinEdit txtsubDivisions;

        public event OnValueChangeEventHandler OnValueChange;

        public ScaleAndUnitsPropertyPage()
        {
            this.InitializeComponent();
            ScaleBarEventsClass.ValueChange += new ScaleBarEventsClass.ValueChangeHandler(this.method_2);
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                this.imapSurroundFrame_0.MapSurround = (ScaleBarFormatPropertyPage.m_pScaleBar as IClone).Clone() as IMapSurround;
            }
        }

        private void btnSymbolSelector_Click(object sender, EventArgs e)
        {
            try
            {
                ITextSymbol unitLabelSymbol = ScaleBarFormatPropertyPage.m_pScaleBar.UnitLabelSymbol;
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    selector.SetSymbol(unitLabelSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        unitLabelSymbol = selector.GetSymbol() as ITextSymbol;
                        ScaleBarFormatPropertyPage.m_pScaleBar.UnitLabelSymbol = unitLabelSymbol;
                        this.method_1();
                    }
                }
            }
            catch
            {
            }
        }

        public void Cancel()
        {
        }

        private void cboLabelPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IScaleBar pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar;
                try
                {
                    pScaleBar.UnitLabelPosition = (esriScaleBarPos) this.cboLabelPosition.SelectedIndex;
                    this.method_1();
                }
                catch
                {
                }
            }
        }

        private void cboResizeHint_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IScaleBar pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar;
                try
                {
                    pScaleBar.ResizeHint = (esriScaleBarResizeHint) this.cboResizeHint.SelectedIndex;
                    this.method_1();
                }
                catch
                {
                }
            }
        }

        private void cboUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IScaleBar pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar;
                try
                {
                    pScaleBar.Units = (esriUnits) this.cboUnits.SelectedIndex;
                    this.method_1();
                }
                catch
                {
                }
            }
        }

        private void chkDivisionsBeforeZero_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IScaleBar pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar;
                try
                {
                    if (this.chkDivisionsBeforeZero.Checked)
                    {
                        pScaleBar.DivisionsBeforeZero = 1;
                    }
                    else
                    {
                        pScaleBar.DivisionsBeforeZero = 0;
                    }
                    this.method_1();
                }
                catch
                {
                }
            }
        }

        private void chkDivisionsBeforeZero_Click(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.textEdit1 = new TextEdit();
            this.label8 = new Label();
            this.label7 = new Label();
            this.cboResizeHint = new ComboBoxEdit();
            this.chkDivisionsBeforeZero = new CheckEdit();
            this.txtsubDivisions = new SpinEdit();
            this.txtDivisions = new SpinEdit();
            this.label2 = new Label();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.btnSymbolSelector = new SimpleButton();
            this.txtGap = new SpinEdit();
            this.label6 = new Label();
            this.txtLabel = new TextEdit();
            this.label5 = new Label();
            this.cboLabelPosition = new ComboBoxEdit();
            this.label4 = new Label();
            this.label3 = new Label();
            this.cboUnits = new ComboBoxEdit();
            this.groupBox1.SuspendLayout();
            this.textEdit1.Properties.BeginInit();
            this.cboResizeHint.Properties.BeginInit();
            this.chkDivisionsBeforeZero.Properties.BeginInit();
            this.txtsubDivisions.Properties.BeginInit();
            this.txtDivisions.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.txtGap.Properties.BeginInit();
            this.txtLabel.Properties.BeginInit();
            this.cboLabelPosition.Properties.BeginInit();
            this.cboUnits.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.textEdit1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cboResizeHint);
            this.groupBox1.Controls.Add(this.chkDivisionsBeforeZero);
            this.groupBox1.Controls.Add(this.txtsubDivisions);
            this.groupBox1.Controls.Add(this.txtDivisions);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xc0, 0xa8);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "比例";
            this.textEdit1.EditValue = "自动";
            this.textEdit1.Location = new Point(0x40, 0x10);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Enabled = false;
            this.textEdit1.Size = new Size(0x70, 0x15);
            this.textEdit1.TabIndex = 13;
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x10, 0x70);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x4f, 0x11);
            this.label8.TabIndex = 7;
            this.label8.Text = "当大小改变时";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(8, 0x13);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x2a, 0x11);
            this.label7.TabIndex = 6;
            this.label7.Text = "分隔值";
            this.cboResizeHint.EditValue = "";
            this.cboResizeHint.Location = new Point(0x10, 0x88);
            this.cboResizeHint.Name = "cboResizeHint";
            this.cboResizeHint.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboResizeHint.Properties.Items.AddRange(new object[] { "调整宽度", "调整刻度值", "调整刻度数" });
            this.cboResizeHint.Size = new Size(160, 0x15);
            this.cboResizeHint.TabIndex = 5;
            this.cboResizeHint.SelectedIndexChanged += new EventHandler(this.cboResizeHint_SelectedIndexChanged);
            this.chkDivisionsBeforeZero.EditValue = false;
            this.chkDivisionsBeforeZero.Location = new Point(8, 0x58);
            this.chkDivisionsBeforeZero.Name = "chkDivisionsBeforeZero";
            this.chkDivisionsBeforeZero.Properties.Caption = "零前显示一个子刻度";
            this.chkDivisionsBeforeZero.Size = new Size(0x90, 0x13);
            this.chkDivisionsBeforeZero.TabIndex = 4;
            this.chkDivisionsBeforeZero.Click += new EventHandler(this.chkDivisionsBeforeZero_Click);
            this.chkDivisionsBeforeZero.CheckedChanged += new EventHandler(this.chkDivisionsBeforeZero_CheckedChanged);
            this.txtsubDivisions.EditValue = 0;
            this.txtsubDivisions.Location = new Point(0x40, 0x40);
            this.txtsubDivisions.Name = "txtsubDivisions";
            this.txtsubDivisions.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtsubDivisions.Properties.UseCtrlIncrement = false;
            this.txtsubDivisions.Size = new Size(0x70, 0x15);
            this.txtsubDivisions.TabIndex = 3;
            this.txtsubDivisions.EditValueChanged += new EventHandler(this.txtsubDivisions_EditValueChanged);
            this.txtDivisions.EditValue = 0;
            this.txtDivisions.Location = new Point(0x40, 40);
            this.txtDivisions.Name = "txtDivisions";
            this.txtDivisions.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtDivisions.Properties.UseCtrlIncrement = false;
            this.txtDivisions.Size = new Size(0x70, 0x15);
            this.txtDivisions.TabIndex = 2;
            this.txtDivisions.EditValueChanged += new EventHandler(this.txtDivisions_EditValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x43);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x36, 0x11);
            this.label2.TabIndex = 1;
            this.label2.Text = "子刻段数";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x2d);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x2a, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "刻段数";
            this.groupBox2.Controls.Add(this.btnSymbolSelector);
            this.groupBox2.Controls.Add(this.txtGap);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtLabel);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cboLabelPosition);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cboUnits);
            this.groupBox2.Location = new Point(8, 0xb8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xc0, 0x80);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "单位";
            this.btnSymbolSelector.Location = new Point(0x88, 0x47);
            this.btnSymbolSelector.Name = "btnSymbolSelector";
            this.btnSymbolSelector.Size = new Size(0x30, 0x18);
            this.btnSymbolSelector.TabIndex = 15;
            this.btnSymbolSelector.Text = "符号";
            this.btnSymbolSelector.Click += new EventHandler(this.btnSymbolSelector_Click);
            this.txtGap.EditValue = 0;
            this.txtGap.Location = new Point(0x38, 0x60);
            this.txtGap.Name = "txtGap";
            this.txtGap.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtGap.Properties.UseCtrlIncrement = false;
            this.txtGap.Size = new Size(0x40, 0x15);
            this.txtGap.TabIndex = 14;
            this.txtGap.EditValueChanged += new EventHandler(this.txtGap_EditValueChanged);
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x10, 0x63);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x1d, 0x11);
            this.label6.TabIndex = 13;
            this.label6.Text = "间隔";
            this.txtLabel.EditValue = "";
            this.txtLabel.Location = new Point(0x38, 70);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new Size(0x48, 0x15);
            this.txtLabel.TabIndex = 12;
            this.txtLabel.EditValueChanged += new EventHandler(this.txtLabel_EditValueChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x10, 0x48);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 0x11);
            this.label5.TabIndex = 11;
            this.label5.Text = "标注";
            this.cboLabelPosition.EditValue = "";
            this.cboLabelPosition.Location = new Point(0x38, 0x2a);
            this.cboLabelPosition.Name = "cboLabelPosition";
            this.cboLabelPosition.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboLabelPosition.Properties.Items.AddRange(new object[] { "在比例尺上面", "在标记前面", "在标记后面", "在比例尺前面", "在比例尺后面", "在比例尺下面" });
            this.cboLabelPosition.Size = new Size(0x80, 0x15);
            this.cboLabelPosition.TabIndex = 10;
            this.cboLabelPosition.SelectedIndexChanged += new EventHandler(this.cboLabelPosition_SelectedIndexChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x10, 0x2c);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1d, 0x11);
            this.label4.TabIndex = 9;
            this.label4.Text = "位置";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x10, 0x13);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 0x11);
            this.label3.TabIndex = 8;
            this.label3.Text = "单位";
            this.cboUnits.EditValue = "";
            this.cboUnits.Location = new Point(0x38, 0x10);
            this.cboUnits.Name = "cboUnits";
            this.cboUnits.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboUnits.Properties.Items.AddRange(new object[] { "未知单位", "英寸", "点", "英尺", "码", "英里", "海里", "毫米", "厘米", "米", "公里", "十进制度", "分米" });
            this.cboUnits.Size = new Size(0x80, 0x15);
            this.cboUnits.TabIndex = 7;
            this.cboUnits.SelectedIndexChanged += new EventHandler(this.cboUnits_SelectedIndexChanged);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "ScaleAndUnitsPropertyPage";
            base.Size = new Size(0xd8, 320);
            base.Load += new EventHandler(this.ScaleAndUnitsPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.textEdit1.Properties.EndInit();
            this.cboResizeHint.Properties.EndInit();
            this.chkDivisionsBeforeZero.Properties.EndInit();
            this.txtsubDivisions.Properties.EndInit();
            this.txtDivisions.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.txtGap.Properties.EndInit();
            this.txtLabel.Properties.EndInit();
            this.cboLabelPosition.Properties.EndInit();
            this.cboUnits.Properties.EndInit();
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        private void method_0()
        {
            IScaleBar pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar;
            this.txtDivisions.Text = pScaleBar.Divisions.ToString();
            this.txtsubDivisions.Text = pScaleBar.Subdivisions.ToString();
            this.chkDivisionsBeforeZero.Checked = pScaleBar.DivisionsBeforeZero == 1;
            this.cboResizeHint.SelectedIndex = (int) pScaleBar.ResizeHint;
            this.cboUnits.SelectedIndex = (int) pScaleBar.Units;
            this.cboLabelPosition.SelectedIndex = (int) pScaleBar.UnitLabelPosition;
            this.txtLabel.Text = pScaleBar.UnitLabel;
            this.txtGap.Text = pScaleBar.UnitLabelGap.ToString("#.##");
        }

        private void method_1()
        {
            this.bool_1 = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        private void method_2(object object_0)
        {
            this.bool_0 = false;
            this.method_0();
            this.bool_0 = true;
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_0();
            this.bool_0 = true;
        }

        private void ScaleAndUnitsPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.imapSurroundFrame_0 = object_0 as IMapSurroundFrame;
            if (ScaleBarFormatPropertyPage.m_pScaleBar == null)
            {
                ScaleBarFormatPropertyPage.m_pScaleBar = (this.imapSurroundFrame_0.MapSurround as IClone).Clone() as IScaleBar;
            }
        }

        private void txtDivisions_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IScaleBar pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar;
                try
                {
                    pScaleBar.Divisions = short.Parse(this.txtDivisions.Text);
                    this.method_1();
                }
                catch
                {
                }
            }
        }

        private void txtGap_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IScaleBar pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar;
                try
                {
                    pScaleBar.UnitLabelGap = double.Parse(this.txtGap.Text);
                    this.method_1();
                }
                catch
                {
                }
            }
        }

        private void txtLabel_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IScaleBar pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar;
                try
                {
                    pScaleBar.UnitLabel = this.txtLabel.Text;
                    this.method_1();
                }
                catch
                {
                }
            }
        }

        private void txtsubDivisions_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IScaleBar pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar;
                try
                {
                    pScaleBar.Subdivisions = short.Parse(this.txtsubDivisions.Text);
                    this.method_1();
                }
                catch
                {
                }
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_1;
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
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}

