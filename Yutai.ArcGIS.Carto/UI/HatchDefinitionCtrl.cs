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
    internal partial class HatchDefinitionCtrl : UserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private IHatchDefinition ihatchDefinition_0 = null;
        private int int_0 = 1;

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
                    definition = new HatchLineDefinitionClass
                    {
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
                    definition = new HatchMarkerDefinitionClass
                    {
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
            new frmHatchAlignment {HatchDefinition = this.ihatchDefinition_0}.ShowDialog();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            new frmTextDisplaySet {HatchDefinition = this.ihatchDefinition_0}.ShowDialog();
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
            get { return this.ihatchDefinition_0; }
            set { this.ihatchDefinition_0 = value; }
        }

        public int HatchInterval
        {
            get { return this.int_0; }
            set { this.int_0 = value; }
        }

        public delegate void ValueChangedHandler(object sender, EventArgs e);
    }
}