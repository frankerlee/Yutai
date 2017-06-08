using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class OtherGridPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private StyleButton btnStyle;
        private ComboBox cboAnnoUnit;
        private ComboBox cboFontName;
        private ComboBox cboFontSize;
        private CheckBox chkDrawCornerShortLine;
        private CheckBox chkDrawJWD;
        private CheckBox chkDrawRoundLineShortLine;
        private CheckBox chkDrawRoundText;
        private CheckBox chkRoundText;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private IContainer icontainer_0 = null;
        private ILineSymbol ilineSymbol_0;
        private IMarkerSymbol imarkerSymbol_0;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private MapCartoTemplateLib.MapTemplate mapTemplate_0 = null;
        private RadioButton rdoLine;
        private RadioButton rdoNone;
        private RadioButton rdoTick;
        private TextBox txtAnnUnitScale;

        public event OnValueChangeEventHandler OnValueChange;

        public OtherGridPropertyPage()
        {
            this.InitializeComponent();
            SimpleMarkerSymbolClass class2 = new SimpleMarkerSymbolClass {
                Style = esriSimpleMarkerStyle.esriSMSCross,
                Size = 28.0
            };
            this.imarkerSymbol_0 = class2;
            CartographicLineSymbolClass class3 = new CartographicLineSymbolClass {
                Cap = esriLineCapStyle.esriLCSSquare
            };
            RgbColorClass class4 = new RgbColorClass {
                Red = 0,
                Blue = 0,
                Green = 0
            };
            class3.Color = class4;
            class3.Join = esriLineJoinStyle.esriLJSMitre;
            class3.Width = 1.0;
            this.ilineSymbol_0 = class3;
            this.cboFontName.Items.Clear();
            InstalledFontCollection fonts = new InstalledFontCollection();
            for (int i = 0; i < fonts.Families.Length; i++)
            {
                this.cboFontName.Items.Add(fonts.Families[i].Name);
            }
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                if (this.rdoNone.Checked)
                {
                    this.mapTemplate_0.GridSymbol = null;
                }
                else
                {
                    this.mapTemplate_0.GridSymbol = this.btnStyle.Style as ISymbol;
                }
                this.mapTemplate_0.BigFontSize = double.Parse(this.cboFontSize.Text);
                this.mapTemplate_0.SmallFontSize = this.mapTemplate_0.BigFontSize + 3.0;
                this.mapTemplate_0.FontName = this.cboFontName.Text;
                this.mapTemplate_0.AnnoUnit = (this.cboAnnoUnit.SelectedIndex == 0) ? esriUnits.esriMeters : esriUnits.esriKilometers;
                this.mapTemplate_0.AnnoUnitZoomScale = Convert.ToDouble(this.txtAnnUnitScale.Text);
                this.MapTemplate.DrawCornerShortLine = this.chkDrawCornerShortLine.Checked;
                this.MapTemplate.DrawRoundText = this.chkRoundText.Checked;
                this.MapTemplate.DrawCornerText = this.chkDrawRoundText.Checked;
                this.MapTemplate.DrawRoundLineShortLine = this.chkDrawRoundLineShortLine.Checked;
                this.MapTemplate.DrawJWD = this.chkDrawJWD.Checked;
            }
        }

        private void btnStyle_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            if (selector != null)
            {
                selector.SetStyleGallery(ApplicationBase.StyleGallery);
                selector.SetSymbol(this.btnStyle.Style);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.bool_1 = true;
                    this.btnStyle.Style = selector.GetSymbol();
                    if (this.rdoTick.Checked)
                    {
                        this.imarkerSymbol_0 = this.btnStyle.Style as IMarkerSymbol;
                    }
                    else
                    {
                        this.ilineSymbol_0 = this.btnStyle.Style as ILineSymbol;
                    }
                    if (this.OnValueChange != null)
                    {
                        this.OnValueChange();
                    }
                }
            }
        }

        public void Cancel()
        {
        }

        private void cboFontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void cboFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkDrawRoundText_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.chkDrawCornerShortLine.Checked)
                {
                    if (this.MapTemplate.NewMapFrameTypeVal == MapFrameType.MFTRect)
                    {
                        if (this.MapTemplate.MapFramingType == MapFramingType.StandardFraming)
                        {
                            this.chkDrawRoundText.Enabled = true;
                        }
                        else
                        {
                            this.chkDrawRoundText.Enabled = false;
                            this.chkDrawRoundText.Checked = false;
                        }
                    }
                    else
                    {
                        this.chkDrawJWD.Enabled = true;
                    }
                }
                else if (this.MapTemplate.NewMapFrameTypeVal == MapFrameType.MFTRect)
                {
                    this.chkDrawRoundText.Checked = false;
                    this.chkDrawRoundText.Enabled = false;
                }
                else
                {
                    this.chkDrawJWD.Checked = false;
                    this.chkDrawJWD.Enabled = false;
                }
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkRoundText_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.rdoNone = new RadioButton();
            this.btnStyle = new StyleButton();
            this.rdoTick = new RadioButton();
            this.rdoLine = new RadioButton();
            this.groupBox2 = new GroupBox();
            this.label4 = new Label();
            this.label1 = new Label();
            this.cboFontSize = new ComboBox();
            this.cboFontName = new ComboBox();
            this.groupBox3 = new GroupBox();
            this.txtAnnUnitScale = new TextBox();
            this.label2 = new Label();
            this.label3 = new Label();
            this.cboAnnoUnit = new ComboBox();
            this.chkDrawCornerShortLine = new CheckBox();
            this.chkDrawRoundLineShortLine = new CheckBox();
            this.chkDrawJWD = new CheckBox();
            this.chkDrawRoundText = new CheckBox();
            this.chkRoundText = new CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.rdoNone);
            this.groupBox1.Controls.Add(this.btnStyle);
            this.groupBox1.Controls.Add(this.rdoTick);
            this.groupBox1.Controls.Add(this.rdoLine);
            this.groupBox1.Location = new Point(8, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x108, 0x54);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "格网显示属性";
            this.rdoNone.AutoSize = true;
            this.rdoNone.Location = new Point(0xc7, 0x15);
            this.rdoNone.Name = "rdoNone";
            this.rdoNone.Size = new Size(0x23, 0x10);
            this.rdoNone.TabIndex = 3;
            this.rdoNone.Text = "无";
            this.rdoNone.UseVisualStyleBackColor = true;
            this.rdoNone.CheckedChanged += new EventHandler(this.rdoNone_CheckedChanged);
            this.btnStyle.Location = new Point(0x40, 0x2b);
            this.btnStyle.Name = "btnStyle";
            this.btnStyle.Size = new Size(80, 0x20);
            this.btnStyle.Style = null;
            this.btnStyle.TabIndex = 2;
            this.btnStyle.Click += new EventHandler(this.btnStyle_Click);
            this.rdoTick.AutoSize = true;
            this.rdoTick.Checked = true;
            this.rdoTick.Location = new Point(0x81, 0x15);
            this.rdoTick.Name = "rdoTick";
            this.rdoTick.Size = new Size(0x2f, 0x10);
            this.rdoTick.TabIndex = 1;
            this.rdoTick.TabStop = true;
            this.rdoTick.Text = "十字";
            this.rdoTick.UseVisualStyleBackColor = true;
            this.rdoTick.CheckedChanged += new EventHandler(this.rdoTick_CheckedChanged);
            this.rdoLine.AutoSize = true;
            this.rdoLine.Location = new Point(0x13, 0x15);
            this.rdoLine.Name = "rdoLine";
            this.rdoLine.Size = new Size(0x3b, 0x10);
            this.rdoLine.TabIndex = 0;
            this.rdoLine.Text = "网格线";
            this.rdoLine.UseVisualStyleBackColor = true;
            this.rdoLine.CheckedChanged += new EventHandler(this.rdoLine_CheckedChanged);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cboFontSize);
            this.groupBox2.Controls.Add(this.cboFontName);
            this.groupBox2.Location = new Point(8, 0x5d);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x108, 0x4a);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "标注样式";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x11, 0x29);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x23, 12);
            this.label4.TabIndex = 0x22;
            this.label4.Text = "大小:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x11, 0x11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 12);
            this.label1.TabIndex = 0x21;
            this.label1.Text = "字体:";
            this.cboFontSize.Items.AddRange(new object[] { 
                "5", "6", "7", "8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", 
                "34", "36", "48", "72"
             });
            this.cboFontSize.Location = new Point(0x39, 0x29);
            this.cboFontSize.Name = "cboFontSize";
            this.cboFontSize.Size = new Size(0x40, 20);
            this.cboFontSize.TabIndex = 0x20;
            this.cboFontSize.Text = "5";
            this.cboFontSize.SelectedIndexChanged += new EventHandler(this.cboFontSize_SelectedIndexChanged);
            this.cboFontName.Location = new Point(0x39, 0x11);
            this.cboFontName.Name = "cboFontName";
            this.cboFontName.Size = new Size(0xb8, 20);
            this.cboFontName.TabIndex = 0x1f;
            this.cboFontName.SelectedIndexChanged += new EventHandler(this.cboFontName_SelectedIndexChanged);
            this.groupBox3.Controls.Add(this.txtAnnUnitScale);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.cboAnnoUnit);
            this.groupBox3.Location = new Point(8, 0xb1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x108, 0x4a);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "标注样式";
            this.groupBox3.Visible = false;
            this.txtAnnUnitScale.Location = new Point(0x4c, 0x2c);
            this.txtAnnUnitScale.Name = "txtAnnUnitScale";
            this.txtAnnUnitScale.Size = new Size(0xa5, 0x15);
            this.txtAnnUnitScale.TabIndex = 0x23;
            this.txtAnnUnitScale.Text = "1";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x11, 0x29);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 0x22;
            this.label2.Text = "单位倍数";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x11, 0x11);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 12);
            this.label3.TabIndex = 0x21;
            this.label3.Text = "标注单位";
            this.cboAnnoUnit.Items.AddRange(new object[] { "米", "公里" });
            this.cboAnnoUnit.Location = new Point(0x4c, 0x11);
            this.cboAnnoUnit.Name = "cboAnnoUnit";
            this.cboAnnoUnit.Size = new Size(0xa5, 20);
            this.cboAnnoUnit.TabIndex = 0x1f;
            this.cboAnnoUnit.Text = "米";
            this.chkDrawCornerShortLine.AutoSize = true;
            this.chkDrawCornerShortLine.Location = new Point(0x121, 14);
            this.chkDrawCornerShortLine.Name = "chkDrawCornerShortLine";
            this.chkDrawCornerShortLine.Size = new Size(0x60, 0x10);
            this.chkDrawCornerShortLine.TabIndex = 8;
            this.chkDrawCornerShortLine.Text = "绘制四角短线";
            this.chkDrawCornerShortLine.UseVisualStyleBackColor = true;
            this.chkDrawCornerShortLine.CheckedChanged += new EventHandler(this.chkDrawRoundText_CheckedChanged);
            this.chkDrawRoundLineShortLine.AutoSize = true;
            this.chkDrawRoundLineShortLine.Location = new Point(0x121, 0x3e);
            this.chkDrawRoundLineShortLine.Name = "chkDrawRoundLineShortLine";
            this.chkDrawRoundLineShortLine.Size = new Size(0x60, 0x10);
            this.chkDrawRoundLineShortLine.TabIndex = 9;
            this.chkDrawRoundLineShortLine.Text = "绘制四周短线";
            this.chkDrawRoundLineShortLine.UseVisualStyleBackColor = true;
            this.chkDrawRoundLineShortLine.CheckedChanged += new EventHandler(this.chkDrawRoundText_CheckedChanged);
            this.chkDrawJWD.AutoSize = true;
            this.chkDrawJWD.Location = new Point(0x121, 0x6a);
            this.chkDrawJWD.Name = "chkDrawJWD";
            this.chkDrawJWD.Size = new Size(0x60, 0x10);
            this.chkDrawJWD.TabIndex = 10;
            this.chkDrawJWD.Text = "绘制经纬度值";
            this.chkDrawJWD.UseVisualStyleBackColor = true;
            this.chkDrawJWD.CheckedChanged += new EventHandler(this.chkDrawRoundText_CheckedChanged);
            this.chkDrawRoundText.AutoSize = true;
            this.chkDrawRoundText.Location = new Point(0x121, 0x25);
            this.chkDrawRoundText.Name = "chkDrawRoundText";
            this.chkDrawRoundText.Size = new Size(0x60, 0x10);
            this.chkDrawRoundText.TabIndex = 11;
            this.chkDrawRoundText.Text = "绘制四角注记";
            this.chkDrawRoundText.UseVisualStyleBackColor = true;
            this.chkDrawRoundText.CheckedChanged += new EventHandler(this.chkDrawRoundText_CheckedChanged);
            this.chkRoundText.AutoSize = true;
            this.chkRoundText.Location = new Point(0x121, 0x54);
            this.chkRoundText.Name = "chkRoundText";
            this.chkRoundText.Size = new Size(0x60, 0x10);
            this.chkRoundText.TabIndex = 12;
            this.chkRoundText.Text = "绘制四周注记";
            this.chkRoundText.UseVisualStyleBackColor = true;
            this.chkRoundText.CheckedChanged += new EventHandler(this.chkRoundText_CheckedChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.chkRoundText);
            base.Controls.Add(this.chkDrawRoundText);
            base.Controls.Add(this.chkDrawJWD);
            base.Controls.Add(this.chkDrawRoundLineShortLine);
            base.Controls.Add(this.chkDrawCornerShortLine);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "OtherGridPropertyPage";
            base.Size = new Size(0x1ed, 0xfb);
            base.Load += new EventHandler(this.OtherGridPropertyPage_Load);
            base.VisibleChanged += new EventHandler(this.OtherGridPropertyPage_VisibleChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        private void method_0()
        {
            this.btnStyle.Style = this.mapTemplate_0.GridSymbol;
            if (this.mapTemplate_0.GridSymbol is IMarkerSymbol)
            {
                this.rdoTick.Checked = true;
            }
            else if (this.mapTemplate_0.GridSymbol is ILineSymbol)
            {
                this.rdoLine.Checked = true;
            }
            else
            {
                this.rdoNone.Checked = true;
            }
            if (this.mapTemplate_0.NewMapFrameTypeVal == MapFrameType.MFTRect)
            {
                this.chkDrawJWD.Checked = false;
                this.chkDrawJWD.Enabled = false;
            }
            else
            {
                this.chkDrawJWD.Enabled = true;
            }
            string fontName = this.mapTemplate_0.FontName;
            for (int i = 0; i < this.cboFontName.Items.Count; i++)
            {
                if (fontName == this.cboFontName.Items[i].ToString())
                {
                    this.cboFontName.SelectedIndex = i;
                    break;
                }
            }
            this.cboFontSize.Text = this.mapTemplate_0.BigFontSize.ToString();
            this.cboAnnoUnit.SelectedIndex = (this.mapTemplate_0.AnnoUnit == esriUnits.esriMeters) ? 0 : 1;
            this.txtAnnUnitScale.Text = this.mapTemplate_0.AnnoUnitZoomScale.ToString();
            this.chkDrawCornerShortLine.Checked = this.MapTemplate.DrawCornerShortLine;
            this.chkDrawRoundLineShortLine.Checked = this.MapTemplate.DrawRoundLineShortLine;
            this.chkRoundText.Checked = this.MapTemplate.DrawRoundText;
            if (this.MapTemplate.NewMapFrameTypeVal == MapFrameType.MFTRect)
            {
                if (this.MapTemplate.MapFramingType == MapFramingType.StandardFraming)
                {
                    this.chkDrawRoundText.Enabled = true;
                    this.chkDrawRoundText.Checked = this.MapTemplate.DrawCornerText;
                }
                else
                {
                    this.chkDrawRoundText.Enabled = false;
                    this.chkDrawRoundText.Checked = false;
                }
                this.chkDrawJWD.Enabled = false;
                this.chkDrawJWD.Checked = false;
            }
            else
            {
                this.chkDrawJWD.Enabled = true;
                this.chkDrawJWD.Checked = this.MapTemplate.DrawJWD;
                this.chkDrawRoundText.Enabled = false;
                this.chkDrawRoundText.Checked = false;
            }
        }

        private void OtherGridPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_0 = true;
        }

        private void OtherGridPropertyPage_VisibleChanged(object sender, EventArgs e)
        {
            if (base.Visible)
            {
                this.bool_0 = false;
                this.method_0();
                this.bool_0 = true;
            }
        }

        private void rdoLine_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0 && this.rdoLine.Checked)
            {
                this.bool_1 = true;
                this.btnStyle.Enabled = true;
                this.btnStyle.Style = this.ilineSymbol_0;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void rdoNone_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0 && this.rdoNone.Checked)
            {
                this.bool_1 = true;
                this.btnStyle.Enabled = false;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void rdoTick_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0 && this.rdoTick.Checked)
            {
                this.bool_1 = true;
                this.btnStyle.Enabled = true;
                this.btnStyle.Style = this.imarkerSymbol_0;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_0();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.MapTemplate = object_0 as MapCartoTemplateLib.MapTemplate;
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

        public MapCartoTemplateLib.MapTemplate MapTemplate
        {
            get
            {
                return this.mapTemplate_0;
            }
            set
            {
                this.mapTemplate_0 = value;
            }
        }

        public string Title
        {
            get
            {
                return "其他";
            }
            set
            {
            }
        }
    }
}

