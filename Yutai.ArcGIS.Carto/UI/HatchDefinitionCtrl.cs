using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Location;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class HatchDefinitionCtrl : UserControl
    {
        private bool bool_0 = false;
        private StyleButton btnHatchSymbol;
        private StyleButton btnTextSymbol;
        private CheckEdit chkTextSymbol;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private IHatchDefinition ihatchDefinition_0 = null;
        private int int_0 = 1;
        private Label label3;
        private Label label4;
        private Panel panel1;
        private RadioGroup radioGroup1;
        private SimpleButton simpleButton1;
        private SimpleButton simpleButton2;
        private TextEdit txtLength;
        private TextEdit txtOffset;

        public event ValueChangedHandler ValueChanged;

        public HatchDefinitionCtrl()
        {
            this.InitializeComponent();
        }

        private void chkTextSymbol_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (this.btnTextSymbol.Style == null)
                {
                    this.btnTextSymbol.Style = new TextSymbolClass();
                }
                this.btnTextSymbol.Enabled = this.chkTextSymbol.Checked;
                this.simpleButton2.Enabled = this.chkTextSymbol.Checked;
                if (this.chkTextSymbol.Checked)
                {
                    this.ihatchDefinition_0.TextSymbol = this.btnTextSymbol.Style as ITextSymbol;
                }
                else
                {
                    this.ihatchDefinition_0.TextSymbol = null;
                }
            }
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void HatchDefinitionCtrl_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        public void Init()
        {
            if (this.ihatchDefinition_0 != null)
            {
                this.bool_0 = false;
                if (this.ihatchDefinition_0 is IHatchLineDefinition)
                {
                    this.radioGroup1.SelectedIndex = 0;
                    this.txtLength.Text = (this.ihatchDefinition_0 as IHatchLineDefinition).Length.ToString();
                    this.panel1.Visible = true;
                }
                else
                {
                    this.radioGroup1.SelectedIndex = 1;
                    this.panel1.Visible = false;
                }
                this.txtOffset.Text = this.ihatchDefinition_0.Offset.ToString();
                this.btnHatchSymbol.Style = this.ihatchDefinition_0.HatchSymbol;
                this.btnHatchSymbol.Invalidate();
                this.chkTextSymbol.Checked = this.ihatchDefinition_0.TextSymbol != null;
                if (!this.chkTextSymbol.Checked)
                {
                    this.btnTextSymbol.Enabled = false;
                    this.simpleButton2.Enabled = false;
                }
                else
                {
                    this.btnTextSymbol.Enabled = true;
                    this.simpleButton2.Enabled = true;
                }
                if (this.ihatchDefinition_0.TextSymbol == null)
                {
                    this.btnTextSymbol.Style = new TextSymbolClass();
                }
                else
                {
                    this.btnTextSymbol.Style = this.ihatchDefinition_0.TextSymbol;
                }
                this.btnTextSymbol.Invalidate();
                this.bool_0 = true;
            }
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.panel1 = new Panel();
            this.txtLength = new TextEdit();
            this.label3 = new Label();
            this.simpleButton1 = new SimpleButton();
            this.txtOffset = new TextEdit();
            this.label4 = new Label();
            this.btnHatchSymbol = new StyleButton();
            this.radioGroup1 = new RadioGroup();
            this.groupBox2 = new GroupBox();
            this.simpleButton2 = new SimpleButton();
            this.btnTextSymbol = new StyleButton();
            this.chkTextSymbol = new CheckEdit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.txtLength.Properties.BeginInit();
            this.txtOffset.Properties.BeginInit();
            this.radioGroup1.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.chkTextSymbol.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.simpleButton1);
            this.groupBox1.Controls.Add(this.txtOffset);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnHatchSymbol);
            this.groupBox1.Controls.Add(this.radioGroup1);
            this.groupBox1.Location = new Point(8, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(280, 0x70);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "刻度";
            this.panel1.Controls.Add(this.txtLength);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new Point(8, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x108, 40);
            this.panel1.TabIndex = 7;
            this.txtLength.EditValue = "0";
            this.txtLength.Location = new Point(0x40, 8);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new Size(80, 0x17);
            this.txtLength.TabIndex = 5;
            this.txtLength.EditValueChanged += new EventHandler(this.txtLength_EditValueChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 0x10);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x2a, 0x11);
            this.label3.TabIndex = 4;
            this.label3.Text = "线长度";
            this.simpleButton1.Location = new Point(160, 80);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(0x58, 0x18);
            this.simpleButton1.TabIndex = 6;
            this.simpleButton1.Text = "刻度线方向...";
            this.simpleButton1.Click += new EventHandler(this.simpleButton1_Click);
            this.txtOffset.EditValue = "0";
            this.txtOffset.Location = new Point(0x48, 80);
            this.txtOffset.Name = "txtOffset";
            this.txtOffset.Size = new Size(80, 0x17);
            this.txtOffset.TabIndex = 5;
            this.txtOffset.EditValueChanged += new EventHandler(this.txtOffset_EditValueChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 0x58);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x2a, 0x11);
            this.label4.TabIndex = 4;
            this.label4.Text = "偏移量";
            this.btnHatchSymbol.Location = new Point(120, 0x10);
            this.btnHatchSymbol.Name = "btnHatchSymbol";
            this.btnHatchSymbol.Size = new Size(0x60, 0x18);
            this.btnHatchSymbol.Style = null;
            this.btnHatchSymbol.TabIndex = 1;
            this.radioGroup1.Location = new Point(8, 0x10);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Columns = 2;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "线"), new RadioGroupItem(null, "点") });
            this.radioGroup1.Size = new Size(0x70, 0x18);
            this.radioGroup1.TabIndex = 0;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            this.groupBox2.Controls.Add(this.simpleButton2);
            this.groupBox2.Controls.Add(this.btnTextSymbol);
            this.groupBox2.Controls.Add(this.chkTextSymbol);
            this.groupBox2.Location = new Point(8, 0x7b);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(280, 0x48);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "标注";
            this.simpleButton2.Location = new Point(0x80, 40);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(80, 0x18);
            this.simpleButton2.TabIndex = 7;
            this.simpleButton2.Text = "标注设置...";
            this.simpleButton2.Click += new EventHandler(this.simpleButton2_Click);
            this.btnTextSymbol.Location = new Point(0x10, 40);
            this.btnTextSymbol.Name = "btnTextSymbol";
            this.btnTextSymbol.Size = new Size(0x60, 0x18);
            this.btnTextSymbol.Style = null;
            this.btnTextSymbol.TabIndex = 2;
            this.chkTextSymbol.Location = new Point(0x10, 0x10);
            this.chkTextSymbol.Name = "chkTextSymbol";
            this.chkTextSymbol.Properties.Caption = "标注刻度线";
            this.chkTextSymbol.Size = new Size(0x58, 0x13);
            this.chkTextSymbol.TabIndex = 0;
            this.chkTextSymbol.CheckedChanged += new EventHandler(this.chkTextSymbol_CheckedChanged);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "HatchDefinitionCtrl";
            base.Size = new Size(0x128, 200);
            base.Load += new EventHandler(this.HatchDefinitionCtrl_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.txtLength.Properties.EndInit();
            this.txtOffset.Properties.EndInit();
            this.radioGroup1.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.chkTextSymbol.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void method_0(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                }
                catch
                {
                }
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.panel1.Visible = this.radioGroup1.SelectedIndex == 0;
            if (this.bool_0)
            {
                IHatchDefinition definition = null;
                if (this.radioGroup1.SelectedIndex == 0)
                {
                    definition = new HatchLineDefinitionClass {
                        HatchSymbol = new SimpleLineSymbolClass()
                    };
                    try
                    {
                        (definition as IHatchLineDefinition).Length = double.Parse(this.txtLength.Text);
                    }
                    catch
                    {
                    }
                    if (this.ihatchDefinition_0 != null)
                    {
                        definition.AdjustTextOrientation = this.ihatchDefinition_0.AdjustTextOrientation;
                        definition.Alignment = this.ihatchDefinition_0.Alignment;
                        definition.DisplayPrecision = this.ihatchDefinition_0.DisplayPrecision;
                        definition.Expression = this.ihatchDefinition_0.Expression;
                        definition.ExpressionParserEngine = this.ihatchDefinition_0.ExpressionParserEngine;
                        definition.ExpressionSimple = this.ihatchDefinition_0.ExpressionSimple;
                        definition.Offset = this.ihatchDefinition_0.Offset;
                        definition.Prefix = this.ihatchDefinition_0.Prefix;
                        definition.ShowSign = this.ihatchDefinition_0.ShowSign;
                        definition.Suffix = this.ihatchDefinition_0.Suffix;
                        definition.TextDisplay = this.ihatchDefinition_0.TextDisplay;
                        definition.TextSymbol = this.ihatchDefinition_0.TextSymbol;
                    }
                }
                else
                {
                    definition = new HatchMarkerDefinitionClass {
                        HatchSymbol = new SimpleMarkerSymbolClass()
                    };
                    if (this.ihatchDefinition_0 != null)
                    {
                        definition.AdjustTextOrientation = this.ihatchDefinition_0.AdjustTextOrientation;
                        if (this.ihatchDefinition_0.Alignment == esriHatchAlignmentType.esriHatchAlignCenter)
                        {
                            definition.Alignment = esriHatchAlignmentType.esriHatchAlignLeft;
                        }
                        else
                        {
                            definition.Alignment = this.ihatchDefinition_0.Alignment;
                        }
                        definition.DisplayPrecision = this.ihatchDefinition_0.DisplayPrecision;
                        definition.Expression = this.ihatchDefinition_0.Expression;
                        definition.ExpressionParserEngine = this.ihatchDefinition_0.ExpressionParserEngine;
                        definition.ExpressionSimple = this.ihatchDefinition_0.ExpressionSimple;
                        definition.Offset = this.ihatchDefinition_0.Offset;
                        definition.Prefix = this.ihatchDefinition_0.Prefix;
                        definition.ShowSign = this.ihatchDefinition_0.ShowSign;
                        definition.Suffix = this.ihatchDefinition_0.Suffix;
                        definition.TextDisplay = this.ihatchDefinition_0.TextDisplay;
                        definition.TextSymbol = this.ihatchDefinition_0.TextSymbol;
                    }
                }
                this.ihatchDefinition_0 = definition;
                if (this.ValueChanged != null)
                {
                    this.ValueChanged(this, new EventArgs());
                }
                this.Init();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            new frmHatchAlignment { HatchDefinition = this.ihatchDefinition_0 }.ShowDialog();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            new frmTextDisplaySet { HatchDefinition = this.ihatchDefinition_0 }.ShowDialog();
        }

        private void txtLength_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                (this.ihatchDefinition_0 as IHatchLineDefinition).Length = double.Parse(this.txtLength.Text);
            }
            catch
            {
            }
        }

        private void txtOffset_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.ihatchDefinition_0.Offset = double.Parse(this.txtOffset.Text);
            }
            catch
            {
            }
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

        public int HatchInterval
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
            }
        }

        public delegate void ValueChangedHandler(object sender, EventArgs e);
    }
}

