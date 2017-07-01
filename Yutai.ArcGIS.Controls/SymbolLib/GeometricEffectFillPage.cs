using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.Connection.Local;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    internal partial class GeometricEffectFillPage : GeometricEffectBaseControl
    {
        private bool m_CanDo = false;
        private BasicSymbolLayerBaseControl m_pControl = null;

        public GeometricEffectFillPage(BasicSymbolLayerBaseControl pControl)
        {
            this.InitializeComponent();
            base.m_pGeometricEffect = new BasicFillSymbolClass();
            this.m_pControl = pControl;
        }

        public override bool Apply()
        {
            IGraphicAttributes fillPattern =
                (base.m_pGeometricEffect as IBasicFillSymbol).FillPattern as IGraphicAttributes;
            if (fillPattern.ClassName == "1")
            {
                fillPattern.set_Value((int) this.symbolItem1.Tag, this.symbolItem1.Symbol);
            }
            else
            {
                double num;
                double num2;
                if (fillPattern.ClassName == "2")
                {
                    num = 0.0;
                    num2 = 0.0;
                    double val = 0.0;
                    double num4 = 0.0;
                    try
                    {
                        num = double.Parse(this.txtWidth.Text);
                    }
                    catch
                    {
                        MessageBox.Show("请出入数字型数据！");
                        return false;
                    }
                    try
                    {
                        num2 = double.Parse(this.txtAngle.Text);
                    }
                    catch
                    {
                        MessageBox.Show("请出入数字型数据！");
                        return false;
                    }
                    try
                    {
                        val = double.Parse(this.txtStep.Text);
                    }
                    catch
                    {
                        MessageBox.Show("请出入数字型数据！");
                        return false;
                    }
                    try
                    {
                        num4 = double.Parse(this.txtOffset.Text);
                    }
                    catch
                    {
                        MessageBox.Show("请出入数字型数据！");
                        return false;
                    }
                    fillPattern.set_Value((int) this.HatchsymbolItem.Tag, this.HatchsymbolItem.Symbol);
                    fillPattern.set_Value((int) this.txtWidth.Tag, num);
                    fillPattern.set_Value((int) this.txtAngle.Tag, num2);
                    fillPattern.set_Value((int) this.txtStep.Tag, val);
                    fillPattern.set_Value((int) this.txtOffset.Tag, num4);
                }
                else if (fillPattern.ClassName == "3")
                {
                    num = 0.0;
                    double num5 = 0.0;
                    num2 = 0.0;
                    try
                    {
                        num = double.Parse(this.txtInterval.Text);
                    }
                    catch
                    {
                        MessageBox.Show("请出入数字型数据！");
                        return false;
                    }
                    try
                    {
                        num5 = double.Parse(this.txtInterval.Text);
                    }
                    catch
                    {
                        MessageBox.Show("请出入数字型数据！");
                        return false;
                    }
                    try
                    {
                        num2 = double.Parse(this.txtAngle.Text);
                    }
                    catch
                    {
                        MessageBox.Show("请出入数字型数据！");
                        return false;
                    }
                    if ((this.comboBox1.SelectedIndex == -1) || (this.comboBox2.SelectedIndex == -1))
                    {
                        return false;
                    }
                    fillPattern.set_Value((int) this.symbolItemColor1.Tag, this.symbolItemColor1.Symbol);
                    fillPattern.set_Value((int) this.symbolItemColor2.Tag, this.symbolItemColor2.Symbol);
                    fillPattern.set_Value((int) this.comboBox1.Tag, this.comboBox1.SelectedIndex);
                    fillPattern.set_Value((int) this.comboBox2.Tag, this.comboBox2.SelectedIndex);
                    fillPattern.set_Value((int) this.txtInterval.Tag, num);
                    fillPattern.set_Value((int) this.txtAngleGrad.Tag, num2);
                    fillPattern.set_Value((int) this.txtPercent.Tag, num5);
                }
            }
            return true;
        }

        private void btnAddGemoetricEffic_Click(object sender, EventArgs e)
        {
            frmGeometricEffectList list = new frmGeometricEffectList
            {
                BasicSymbolLayerBaseControl = this.m_pControl
            };
            if ((list.ShowDialog() == DialogResult.OK) && (this.m_pControl != null))
            {
                this.m_pControl.AddControl(this, list.SelectControl);
            }
        }

        private void btnChangeEffic_Click(object sender, EventArgs e)
        {
            this.contextMenuStrip1.Show(this, this.btnChangeEffic.Right, this.btnChangeEffic.Bottom);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ((base.m_pGeometricEffect as IBasicFillSymbol).FillPattern as IGraphicAttributes).set_Value(
                    (int) (sender as ComboBox).Tag, (sender as ComboBox).SelectedIndex);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ((base.m_pGeometricEffect as IBasicFillSymbol).FillPattern as IGraphicAttributes).set_Value(
                    (int) (sender as ComboBox).Tag, (sender as ComboBox).SelectedIndex);
            }
        }

        private void GeometricEffectFillPage_Load(object sender, EventArgs e)
        {
            int num2;
            string str;
            int num3;
            if (base.m_pGeometricEffect == null)
            {
                base.m_pGeometricEffect = new BasicFillSymbolClass();
            }
            IGraphicAttributes fillPattern =
                (base.m_pGeometricEffect as IBasicFillSymbol).FillPattern as IGraphicAttributes;
            switch (fillPattern.GraphicAttributeCount)
            {
                case 1:
                    num2 = fillPattern.get_ID(0);
                    str = fillPattern.get_Name(num2);
                    this.symbolItem1.Symbol = fillPattern.get_Value(num2);
                    this.symbolItem1.Tag = num2;
                    this.panel1.Visible = true;
                    this.panel4.Visible = false;
                    this.panel3.Visible = false;
                    num3 = this.panel2.Bottom + this.panel1.Height;
                    base.Size = new Size(base.Width, num3);
                    this.solidColorToolStripMenuItem.Checked = true;
                    break;

                case 5:
                    this.panel1.Visible = false;
                    this.panel4.Visible = true;
                    this.panel3.Visible = false;
                    num3 = this.panel2.Bottom + this.panel4.Height;
                    base.Size = new Size(base.Width, num3);
                    num2 = fillPattern.get_ID(0);
                    str = fillPattern.get_Name(num2);
                    this.HatchsymbolItem.Symbol = fillPattern.get_Value(num2);
                    this.HatchsymbolItem.Tag = num2;
                    num2 = fillPattern.get_ID(1);
                    str = fillPattern.get_Name(num2);
                    this.txtWidth.Text = fillPattern.get_Value(num2).ToString();
                    this.txtWidth.Tag = num2;
                    num2 = fillPattern.get_ID(2);
                    str = fillPattern.get_Name(num2);
                    this.txtAngle.Text = fillPattern.get_Value(num2).ToString();
                    this.txtAngle.Tag = num2;
                    num2 = fillPattern.get_ID(3);
                    str = fillPattern.get_Name(num2);
                    this.txtStep.Text = fillPattern.get_Value(num2).ToString();
                    this.txtStep.Tag = num2;
                    num2 = fillPattern.get_ID(4);
                    str = fillPattern.get_Name(num2);
                    this.txtOffset.Text = fillPattern.get_Value(num2).ToString();
                    this.txtOffset.Tag = num2;
                    this.hatchToolStripMenuItem.Checked = true;
                    break;

                case 7:
                    this.panel1.Visible = false;
                    this.panel4.Visible = false;
                    this.panel3.Visible = true;
                    num3 = this.panel2.Bottom + this.panel3.Height;
                    base.Size = new Size(base.Width, num3);
                    num2 = fillPattern.get_ID(0);
                    str = fillPattern.get_Name(num2);
                    this.symbolItemColor1.Symbol = fillPattern.get_Value(num2);
                    this.symbolItemColor1.Tag = num2;
                    num2 = fillPattern.get_ID(1);
                    str = fillPattern.get_Name(num2);
                    this.symbolItemColor2.Symbol = fillPattern.get_Value(num2);
                    this.symbolItemColor2.Tag = num2;
                    num2 = fillPattern.get_ID(2);
                    str = fillPattern.get_Name(num2);
                    this.comboBox1.SelectedIndex = (int) fillPattern.get_Value(num2);
                    this.comboBox1.Tag = num2;
                    num2 = fillPattern.get_ID(3);
                    str = fillPattern.get_Name(num2);
                    this.comboBox2.SelectedIndex = (int) fillPattern.get_Value(num2);
                    this.comboBox2.Tag = num2;
                    num2 = fillPattern.get_ID(4);
                    str = fillPattern.get_Name(num2);
                    this.txtInterval.Text = fillPattern.get_Value(num2).ToString();
                    this.txtInterval.Tag = num2;
                    num2 = fillPattern.get_ID(5);
                    str = fillPattern.get_Name(num2);
                    this.txtPercent.Text = fillPattern.get_Value(num2).ToString();
                    this.txtPercent.Tag = num2;
                    num2 = fillPattern.get_ID(6);
                    str = fillPattern.get_Name(num2);
                    this.txtAngleGrad.Text = fillPattern.get_Value(num2).ToString();
                    this.txtAngleGrad.Tag = num2;
                    this.gradientToolStripMenuItem.Checked = true;
                    break;
            }
            this.m_CanDo = true;
        }

        private void gradientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.m_CanDo = false;
            if (!this.gradientToolStripMenuItem.Checked)
            {
                this.gradientToolStripMenuItem.Checked = true;
                this.solidColorToolStripMenuItem.Checked = false;
                this.hatchToolStripMenuItem.Checked = false;
                IFillPattern pattern = new GradientPatternClass();
                IGraphicAttributes attributes = pattern as IGraphicAttributes;
                this.panel1.Visible = false;
                this.panel4.Visible = false;
                this.panel3.Visible = true;
                int height = this.panel2.Bottom + this.panel3.Height;
                base.Size = new Size(base.Width, height);
                int attrId = attributes.get_ID(0);
                string str = attributes.get_Name(attrId);
                this.symbolItemColor1.Symbol = attributes.get_Value(attrId);
                this.symbolItemColor1.Tag = attrId;
                attrId = attributes.get_ID(1);
                str = attributes.get_Name(attrId);
                this.symbolItemColor2.Symbol = attributes.get_Value(attrId);
                this.symbolItemColor2.Tag = attrId;
                attrId = attributes.get_ID(2);
                str = attributes.get_Name(attrId);
                this.comboBox1.Tag = attrId;
                this.comboBox1.SelectedIndex = (int) attributes.get_Value(attrId);
                attrId = attributes.get_ID(3);
                str = attributes.get_Name(attrId);
                this.comboBox2.Tag = attrId;
                this.comboBox2.SelectedIndex = (int) attributes.get_Value(attrId);
                attrId = attributes.get_ID(4);
                str = attributes.get_Name(attrId);
                this.txtInterval.Tag = attrId;
                this.txtInterval.Text = attributes.get_Value(attrId).ToString();
                attrId = attributes.get_ID(5);
                str = attributes.get_Name(attrId);
                this.txtPercent.Tag = attrId;
                this.txtPercent.Text = attributes.get_Value(attrId).ToString();
                attrId = attributes.get_ID(5);
                str = attributes.get_Name(attrId);
                this.txtAngleGrad.Tag = attrId;
                this.txtAngleGrad.Text = attributes.get_Value(attrId).ToString();
                try
                {
                    (base.m_pGeometricEffect as IBasicFillSymbol).FillPattern = pattern;
                }
                catch
                {
                }
            }
            this.m_CanDo = true;
        }

        private void HatchsymbolItem_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            IRgbColor rgbColor = new RgbColorClass
            {
                RGB = (this.HatchsymbolItem.Symbol as IColor).RGB
            };
            dialog.Color = Converter.FromRGBColor(rgbColor);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                rgbColor = Converter.ToRGBColor(dialog.Color);
                this.HatchsymbolItem.Symbol = rgbColor;
                ((base.m_pGeometricEffect as IBasicFillSymbol).FillPattern as IGraphicAttributes).set_Value(
                    (int) this.HatchsymbolItem.Tag, this.HatchsymbolItem.Symbol);
            }
        }

        private void HatchsymbolItem_Load(object sender, EventArgs e)
        {
        }

        private void hatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.m_CanDo = false;
            if (!this.hatchToolStripMenuItem.Checked)
            {
                this.hatchToolStripMenuItem.Checked = true;
                this.solidColorToolStripMenuItem.Checked = false;
                this.gradientToolStripMenuItem.Checked = false;
                IFillPattern pattern = new LinePatternClass();
                IGraphicAttributes attributes = pattern as IGraphicAttributes;
                this.panel1.Visible = false;
                this.panel4.Visible = true;
                this.panel3.Visible = false;
                int height = this.panel2.Bottom + this.panel4.Height;
                base.Size = new Size(base.Width, height);
                int attrId = attributes.get_ID(0);
                string str = attributes.get_Name(attrId);
                this.HatchsymbolItem.Tag = attrId;
                this.HatchsymbolItem.Symbol = attributes.get_Value(attrId);
                attrId = attributes.get_ID(1);
                str = attributes.get_Name(attrId);
                this.txtWidth.Tag = attrId;
                this.txtWidth.Text = attributes.get_Value(attrId).ToString();
                attrId = attributes.get_ID(2);
                str = attributes.get_Name(attrId);
                this.txtAngle.Tag = attrId;
                this.txtAngle.Text = attributes.get_Value(attrId).ToString();
                attrId = attributes.get_ID(3);
                str = attributes.get_Name(attrId);
                this.txtStep.Tag = attrId;
                this.txtStep.Text = attributes.get_Value(attrId).ToString();
                attrId = attributes.get_ID(4);
                str = attributes.get_Name(attrId);
                this.txtOffset.Tag = attrId;
                this.txtOffset.Text = attributes.get_Value(attrId).ToString();
                try
                {
                    (base.m_pGeometricEffect as IBasicFillSymbol).FillPattern = pattern;
                }
                catch
                {
                }
            }
            this.m_CanDo = true;
        }

        private void solidColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.m_CanDo = false;
            if (!this.solidColorToolStripMenuItem.Checked)
            {
                this.solidColorToolStripMenuItem.Checked = true;
                this.hatchToolStripMenuItem.Checked = false;
                this.gradientToolStripMenuItem.Checked = false;
                IFillPattern pattern = new SolidColorPatternClass();
                IGraphicAttributes attributes = pattern as IGraphicAttributes;
                int attrId = attributes.get_ID(0);
                string str = attributes.get_Name(attrId);
                this.symbolItem1.Tag = attrId;
                this.symbolItem1.Symbol = attributes.get_Value(attrId);
                this.panel1.Visible = true;
                this.panel4.Visible = false;
                this.panel3.Visible = false;
                int height = this.panel2.Bottom + this.panel1.Height;
                base.Size = new Size(base.Width, height);
                try
                {
                    (base.m_pGeometricEffect as IBasicFillSymbol).FillPattern = pattern;
                }
                catch
                {
                }
            }
            this.m_CanDo = true;
        }

        private void symbolItem1_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            IRgbColor rgbColor = new RgbColorClass
            {
                RGB = (this.symbolItem1.Symbol as IColor).RGB
            };
            dialog.Color = Converter.FromRGBColor(rgbColor);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                rgbColor = Converter.ToRGBColor(dialog.Color);
                this.symbolItem1.Symbol = rgbColor;
                ((base.m_pGeometricEffect as IBasicFillSymbol).FillPattern as IGraphicAttributes).set_Value(
                    (int) this.symbolItem1.Tag, this.symbolItem1.Symbol);
            }
        }

        private void symbolItemColor1_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            IRgbColor rgbColor = new RgbColorClass
            {
                RGB = (this.symbolItemColor1.Symbol as IColor).RGB
            };
            dialog.Color = Converter.FromRGBColor(rgbColor);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                rgbColor = Converter.ToRGBColor(dialog.Color);
                this.symbolItemColor1.Symbol = rgbColor;
                ((base.m_pGeometricEffect as IBasicFillSymbol).FillPattern as IGraphicAttributes).set_Value(
                    (int) this.symbolItemColor1.Tag, this.symbolItemColor1.Symbol);
            }
        }

        private void symbolItemColor2_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            IRgbColor rgbColor = new RgbColorClass
            {
                RGB = (this.symbolItemColor2.Symbol as IColor).RGB
            };
            dialog.Color = Converter.FromRGBColor(rgbColor);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                rgbColor = Converter.ToRGBColor(dialog.Color);
                this.symbolItemColor2.Symbol = rgbColor;
                ((base.m_pGeometricEffect as IBasicFillSymbol).FillPattern as IGraphicAttributes).set_Value(
                    (int) this.symbolItemColor2.Tag, this.symbolItemColor2.Symbol);
            }
        }

        private void txtAngleGrad_Leave(object sender, EventArgs e)
        {
            double val = 0.0;
            try
            {
                val = double.Parse((sender as TextBox).Text);
            }
            catch
            {
                return;
            }
            ((base.m_pGeometricEffect as IBasicFillSymbol).FillPattern as IGraphicAttributes).set_Value(
                (int) (sender as TextBox).Tag, val);
        }
    }
}