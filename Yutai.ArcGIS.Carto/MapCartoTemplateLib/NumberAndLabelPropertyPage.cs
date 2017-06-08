using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class NumberAndLabelPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private SimpleButton btnDivisionMarkSymbol;
        private SimpleButton btnLabelSymbolSelector;
        private SimpleButton btnSubdivisionMarkSymbol;
        private ComboBoxEdit cboLabelFrequency;
        private ComboBoxEdit cboLabelPosition;
        private ComboBoxEdit cboMarkFrequency;
        private ComboBoxEdit cboMarkPosition;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private IMapSurroundFrame imapSurroundFrame_0 = null;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;
        private Label label1;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label8;
        private Label lblMarkHeight;
        private Label lblPosition;
        private Label lblsubMarkHeight;
        private MapTemplateElement mapTemplateElement_0 = null;
        private string string_0 = "数字和标注";
        private SpinEdit txtDivisionMarkHeight;
        private SpinEdit txtLabelGap;
        private SpinEdit txtSubdivisionMarkHeight;

        public event OnValueChangeEventHandler OnValueChange;

        public NumberAndLabelPropertyPage()
        {
            this.InitializeComponent();
            MapCartoTemplateLib.ScaleBarEventsClass.ValueChange += new MapCartoTemplateLib.ScaleBarEventsClass.ValueChangeHandler(this.method_1);
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                this.imapSurroundFrame_0.MapSurround = (MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar as IClone).Clone() as IMapSurround;
            }
        }

        private void btnDivisionMarkSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                IScaleMarks pScaleBar = MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar as IScaleMarks;
                ILineSymbol divisionMarkSymbol = pScaleBar.DivisionMarkSymbol;
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(ApplicationBase.StyleGallery);
                    selector.SetSymbol(divisionMarkSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        divisionMarkSymbol = selector.GetSymbol() as ILineSymbol;
                        pScaleBar.DivisionMarkSymbol = divisionMarkSymbol;
                        this.method_2();
                    }
                }
            }
            catch
            {
            }
        }

        private void btnLabelSymbolSelector_Click(object sender, EventArgs e)
        {
            try
            {
                ITextSymbol labelSymbol = MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar.LabelSymbol;
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(ApplicationBase.StyleGallery);
                    selector.SetSymbol(labelSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        labelSymbol = selector.GetSymbol() as ITextSymbol;
                        MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar.LabelSymbol = labelSymbol;
                        this.method_2();
                    }
                }
            }
            catch
            {
            }
        }

        private void btnSubdivisionMarkSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                IScaleMarks pScaleBar = MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar as IScaleMarks;
                ILineSymbol subdivisionMarkSymbol = pScaleBar.SubdivisionMarkSymbol;
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(ApplicationBase.StyleGallery);
                    selector.SetSymbol(subdivisionMarkSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        subdivisionMarkSymbol = selector.GetSymbol() as ILineSymbol;
                        pScaleBar.SubdivisionMarkSymbol = subdivisionMarkSymbol;
                        this.method_2();
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

        private void cboLabelFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar.LabelFrequency = (esriScaleBarFrequency) this.cboLabelFrequency.SelectedIndex;
                this.method_2();
            }
        }

        private void cboLabelPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar.LabelPosition = (esriVertPosEnum) this.cboLabelPosition.SelectedIndex;
                this.method_2();
            }
        }

        private void cboMarkFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IScaleMarks pScaleBar = MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar as IScaleMarks;
                pScaleBar.MarkFrequency = (esriScaleBarFrequency) this.cboMarkFrequency.SelectedIndex;
                this.method_2();
            }
        }

        private void cboMarkPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IScaleMarks pScaleBar = MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar as IScaleMarks;
                pScaleBar.MarkPosition = (esriVertPosEnum) this.cboMarkPosition.SelectedIndex;
                this.method_2();
            }
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
            this.label8 = new Label();
            this.btnLabelSymbolSelector = new SimpleButton();
            this.txtLabelGap = new SpinEdit();
            this.label6 = new Label();
            this.cboMarkPosition = new ComboBoxEdit();
            this.label4 = new Label();
            this.cboMarkFrequency = new ComboBoxEdit();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.btnSubdivisionMarkSymbol = new SimpleButton();
            this.txtSubdivisionMarkHeight = new SpinEdit();
            this.lblsubMarkHeight = new Label();
            this.btnDivisionMarkSymbol = new SimpleButton();
            this.txtDivisionMarkHeight = new SpinEdit();
            this.lblMarkHeight = new Label();
            this.cboLabelPosition = new ComboBoxEdit();
            this.lblPosition = new Label();
            this.cboLabelFrequency = new ComboBoxEdit();
            this.label5 = new Label();
            this.groupBox1.SuspendLayout();
            this.txtLabelGap.Properties.BeginInit();
            this.cboMarkPosition.Properties.BeginInit();
            this.cboMarkFrequency.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.txtSubdivisionMarkHeight.Properties.BeginInit();
            this.txtDivisionMarkHeight.Properties.BeginInit();
            this.cboLabelPosition.Properties.BeginInit();
            this.cboLabelFrequency.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.btnLabelSymbolSelector);
            this.groupBox1.Controls.Add(this.txtLabelGap);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cboMarkPosition);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cboMarkFrequency);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xe8, 0x88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数字";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x70, 0x48);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x11, 0x11);
            this.label8.TabIndex = 0x13;
            this.label8.Text = "点";
            this.btnLabelSymbolSelector.Location = new Point(0xb0, 8);
            this.btnLabelSymbolSelector.Name = "btnLabelSymbolSelector";
            this.btnLabelSymbolSelector.Size = new Size(0x30, 0x18);
            this.btnLabelSymbolSelector.TabIndex = 0x12;
            this.btnLabelSymbolSelector.Text = "符号";
            this.btnLabelSymbolSelector.Visible = false;
            this.btnLabelSymbolSelector.Click += new EventHandler(this.btnLabelSymbolSelector_Click);
            this.txtLabelGap.EditValue = 0;
            this.txtLabelGap.Location = new Point(0x30, 0x48);
            this.txtLabelGap.Name = "txtLabelGap";
            this.txtLabelGap.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtLabelGap.Properties.UseCtrlIncrement = false;
            this.txtLabelGap.Size = new Size(0x38, 0x15);
            this.txtLabelGap.TabIndex = 0x11;
            this.txtLabelGap.EditValueChanged += new EventHandler(this.txtLabelGap_EditValueChanged);
            this.label6.AutoSize = true;
            this.label6.Location = new Point(8, 0x48);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x1d, 0x11);
            this.label6.TabIndex = 0x10;
            this.label6.Text = "间隔";
            this.cboMarkPosition.EditValue = "";
            this.cboMarkPosition.Location = new Point(0x30, 0x2d);
            this.cboMarkPosition.Name = "cboMarkPosition";
            this.cboMarkPosition.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboMarkPosition.Properties.Items.AddRange(new object[] { "在比例尺上面", "在标记前面", "在标记后面", "在比例尺前面", "在比例尺后面", "在比例尺下面" });
            this.cboMarkPosition.Size = new Size(0xa8, 0x15);
            this.cboMarkPosition.TabIndex = 12;
            this.cboMarkPosition.SelectedIndexChanged += new EventHandler(this.cboMarkPosition_SelectedIndexChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 0x30);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1d, 0x11);
            this.label4.TabIndex = 11;
            this.label4.Text = "位置";
            this.cboMarkFrequency.EditValue = "";
            this.cboMarkFrequency.Location = new Point(0x30, 0x11);
            this.cboMarkFrequency.Name = "cboMarkFrequency";
            this.cboMarkFrequency.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboMarkFrequency.Properties.Items.AddRange(new object[] { "无标注", "单个标注", "标注在尾段", "中分标注", "中分标注且第一段中间点标注", "中分标注且第一段标注下一级", "中分标注且标注下一级", "" });
            this.cboMarkFrequency.Size = new Size(0xa8, 0x15);
            this.cboMarkFrequency.TabIndex = 1;
            this.cboMarkFrequency.SelectedIndexChanged += new EventHandler(this.cboMarkFrequency_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "频率";
            this.groupBox2.Controls.Add(this.btnSubdivisionMarkSymbol);
            this.groupBox2.Controls.Add(this.txtSubdivisionMarkHeight);
            this.groupBox2.Controls.Add(this.lblsubMarkHeight);
            this.groupBox2.Controls.Add(this.btnDivisionMarkSymbol);
            this.groupBox2.Controls.Add(this.txtDivisionMarkHeight);
            this.groupBox2.Controls.Add(this.lblMarkHeight);
            this.groupBox2.Controls.Add(this.cboLabelPosition);
            this.groupBox2.Controls.Add(this.lblPosition);
            this.groupBox2.Controls.Add(this.cboLabelFrequency);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new Point(8, 0x98);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xe8, 0xb8);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "标记";
            this.btnSubdivisionMarkSymbol.Location = new Point(0x98, 0x90);
            this.btnSubdivisionMarkSymbol.Name = "btnSubdivisionMarkSymbol";
            this.btnSubdivisionMarkSymbol.Size = new Size(0x30, 0x18);
            this.btnSubdivisionMarkSymbol.TabIndex = 0x15;
            this.btnSubdivisionMarkSymbol.Text = "符号";
            this.btnSubdivisionMarkSymbol.Click += new EventHandler(this.btnSubdivisionMarkSymbol_Click);
            this.txtSubdivisionMarkHeight.EditValue = 0;
            this.txtSubdivisionMarkHeight.Location = new Point(80, 0x90);
            this.txtSubdivisionMarkHeight.Name = "txtSubdivisionMarkHeight";
            this.txtSubdivisionMarkHeight.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtSubdivisionMarkHeight.Properties.UseCtrlIncrement = false;
            this.txtSubdivisionMarkHeight.Size = new Size(0x40, 0x15);
            this.txtSubdivisionMarkHeight.TabIndex = 20;
            this.txtSubdivisionMarkHeight.EditValueChanged += new EventHandler(this.txtSubdivisionMarkHeight_EditValueChanged);
            this.lblsubMarkHeight.AutoSize = true;
            this.lblsubMarkHeight.Location = new Point(8, 0x92);
            this.lblsubMarkHeight.Name = "lblsubMarkHeight";
            this.lblsubMarkHeight.Size = new Size(0x42, 0x11);
            this.lblsubMarkHeight.TabIndex = 0x13;
            this.lblsubMarkHeight.Text = "子刻度高度";
            this.btnDivisionMarkSymbol.Location = new Point(0x98, 0x71);
            this.btnDivisionMarkSymbol.Name = "btnDivisionMarkSymbol";
            this.btnDivisionMarkSymbol.Size = new Size(0x30, 0x18);
            this.btnDivisionMarkSymbol.TabIndex = 0x12;
            this.btnDivisionMarkSymbol.Text = "符号";
            this.btnDivisionMarkSymbol.Click += new EventHandler(this.btnDivisionMarkSymbol_Click);
            this.txtDivisionMarkHeight.EditValue = 0;
            this.txtDivisionMarkHeight.Location = new Point(80, 0x72);
            this.txtDivisionMarkHeight.Name = "txtDivisionMarkHeight";
            this.txtDivisionMarkHeight.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtDivisionMarkHeight.Properties.UseCtrlIncrement = false;
            this.txtDivisionMarkHeight.Size = new Size(0x40, 0x15);
            this.txtDivisionMarkHeight.TabIndex = 0x11;
            this.txtDivisionMarkHeight.EditValueChanged += new EventHandler(this.txtDivisionMarkHeight_EditValueChanged);
            this.lblMarkHeight.AutoSize = true;
            this.lblMarkHeight.Location = new Point(8, 120);
            this.lblMarkHeight.Name = "lblMarkHeight";
            this.lblMarkHeight.Size = new Size(0x36, 0x11);
            this.lblMarkHeight.TabIndex = 0x10;
            this.lblMarkHeight.Text = "刻度高度";
            this.cboLabelPosition.EditValue = "";
            this.cboLabelPosition.Location = new Point(8, 0x57);
            this.cboLabelPosition.Name = "cboLabelPosition";
            this.cboLabelPosition.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboLabelPosition.Properties.Items.AddRange(new object[] { "在比例尺上面", "在标记前面", "在标记后面", "在比例尺前面", "在比例尺后面", "在比例尺下面" });
            this.cboLabelPosition.Size = new Size(0xd0, 0x15);
            this.cboLabelPosition.TabIndex = 12;
            this.cboLabelPosition.SelectedIndexChanged += new EventHandler(this.cboLabelPosition_SelectedIndexChanged);
            this.lblPosition.AutoSize = true;
            this.lblPosition.Location = new Point(8, 0x43);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new Size(0x1d, 0x11);
            this.lblPosition.TabIndex = 11;
            this.lblPosition.Text = "位置";
            this.cboLabelFrequency.EditValue = "";
            this.cboLabelFrequency.Location = new Point(8, 0x27);
            this.cboLabelFrequency.Name = "cboLabelFrequency";
            this.cboLabelFrequency.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboLabelFrequency.Properties.Items.AddRange(new object[] { "无标注", "单个标注", "标注在尾段", "中分标注", "中分标注且第一段中间点标注", "中分标注且第一段标注下一级", "中分标注且标注下一级", "" });
            this.cboLabelFrequency.Size = new Size(0xd0, 0x15);
            this.cboLabelFrequency.TabIndex = 1;
            this.cboLabelFrequency.SelectedIndexChanged += new EventHandler(this.cboLabelFrequency_SelectedIndexChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(8, 0x15);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 0x11);
            this.label5.TabIndex = 0;
            this.label5.Text = "频率";
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "NumberAndLabelPropertyPage";
            base.Size = new Size(0xf8, 0x160);
            base.Load += new EventHandler(this.NumberAndLabelPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.txtLabelGap.Properties.EndInit();
            this.cboMarkPosition.Properties.EndInit();
            this.cboMarkFrequency.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.txtSubdivisionMarkHeight.Properties.EndInit();
            this.txtDivisionMarkHeight.Properties.EndInit();
            this.cboLabelPosition.Properties.EndInit();
            this.cboLabelFrequency.Properties.EndInit();
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        private void method_0()
        {
            IScaleBar pScaleBar = MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar;
            IScaleMarks marks = pScaleBar as IScaleMarks;
            this.cboMarkFrequency.SelectedIndex = (int) marks.MarkFrequency;
            this.cboMarkPosition.SelectedIndex = (int) marks.MarkPosition;
            this.txtLabelGap.Text = pScaleBar.LabelGap.ToString("#.##");
            this.cboLabelFrequency.SelectedIndex = (int) pScaleBar.LabelFrequency;
            this.cboLabelPosition.SelectedIndex = (int) pScaleBar.LabelPosition;
            this.txtDivisionMarkHeight.Text = marks.DivisionMarkHeight.ToString("#.##");
            this.txtSubdivisionMarkHeight.Text = marks.SubdivisionMarkHeight.ToString("#.##");
            if (pScaleBar is IScaleLine)
            {
                this.lblMarkHeight.Enabled = true;
                this.lblPosition.Enabled = true;
                this.lblsubMarkHeight.Enabled = true;
                this.cboLabelPosition.Enabled = true;
                this.txtDivisionMarkHeight.Enabled = true;
                this.txtSubdivisionMarkHeight.Enabled = true;
            }
            else
            {
                this.lblMarkHeight.Enabled = false;
                this.lblPosition.Enabled = false;
                this.lblsubMarkHeight.Enabled = false;
                this.cboLabelPosition.Enabled = false;
                this.txtDivisionMarkHeight.Enabled = false;
                this.txtSubdivisionMarkHeight.Enabled = false;
            }
        }

        private void method_1(object object_0)
        {
            this.bool_0 = false;
            this.method_0();
            this.bool_0 = true;
        }

        private void method_2()
        {
            this.bool_1 = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        private void NumberAndLabelPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_0 = true;
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_0();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.mapTemplateElement_0 = object_0 as MapTemplateElement;
            this.imapSurroundFrame_0 = this.mapTemplateElement_0.Element as IMapSurroundFrame;
            if (MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar == null)
            {
                MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar = (this.imapSurroundFrame_0.MapSurround as IClone).Clone() as IScaleBar;
            }
        }

        private void txtDivisionMarkHeight_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IScaleMarks pScaleBar = MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar as IScaleMarks;
                try
                {
                    pScaleBar.DivisionMarkHeight = double.Parse(this.txtDivisionMarkHeight.Text);
                    this.method_2();
                }
                catch
                {
                }
            }
        }

        private void txtLabelGap_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IScaleBar pScaleBar = MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar;
                try
                {
                    pScaleBar.LabelGap = double.Parse(this.txtLabelGap.Text);
                    this.method_2();
                }
                catch
                {
                }
            }
        }

        private void txtSubdivisionMarkHeight_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IScaleMarks pScaleBar = MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar as IScaleMarks;
                try
                {
                    pScaleBar.SubdivisionMarkHeight = double.Parse(this.txtSubdivisionMarkHeight.Text);
                    this.method_2();
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

