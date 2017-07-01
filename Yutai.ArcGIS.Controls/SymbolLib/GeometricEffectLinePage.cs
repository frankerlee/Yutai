using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.Connection.Local;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    internal partial class GeometricEffectLinePage : GeometricEffectBaseControl
    {
        private bool m_CanDo = false;
        private BasicSymbolLayerBaseControl m_pControl = null;

        public GeometricEffectLinePage(BasicSymbolLayerBaseControl pControl)
        {
            this.InitializeComponent();
            this.m_pControl = pControl;
            base.m_pGeometricEffect = new BasicLineSymbolClass();
        }

        public override bool Apply()
        {
            double val = 0.0;
            try
            {
                val = double.Parse(this.textBox1.Text);
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
            IGraphicAttributes stroke = (base.m_pGeometricEffect as IBasicLineSymbol).Stroke as IGraphicAttributes;
            stroke.set_Value((int) this.textBox1.Tag, val);
            stroke.set_Value((int) this.comboBox1.Tag, this.comboBox1.SelectedIndex);
            stroke.set_Value((int) this.comboBox2.Tag, this.comboBox2.SelectedIndex);
            stroke.set_Value((int) this.symbolItem1.Tag, this.symbolItem1.Symbol);
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ((base.m_pGeometricEffect as IBasicLineSymbol).Stroke as IGraphicAttributes).set_Value(
                    (int) (sender as ComboBox).Tag, (sender as ComboBox).SelectedIndex);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ((base.m_pGeometricEffect as IBasicLineSymbol).Stroke as IGraphicAttributes).set_Value(
                    (int) (sender as ComboBox).Tag, (sender as ComboBox).SelectedIndex);
            }
        }

        private void GeometricEffectLinePage_Load(object sender, EventArgs e)
        {
            if (base.m_pGeometricEffect == null)
            {
                base.m_pGeometricEffect = new BasicLineSymbolClass();
            }
            int graphicAttributeCount =
                ((base.m_pGeometricEffect as IBasicLineSymbol).Stroke as IGraphicAttributes).GraphicAttributeCount;
            IGraphicAttributes stroke = (base.m_pGeometricEffect as IBasicLineSymbol).Stroke as IGraphicAttributes;
            int attrId = stroke.get_ID(0);
            string str = stroke.get_Name(attrId);
            this.textBox1.Text = stroke.get_Value(attrId).ToString();
            this.textBox1.Tag = attrId;
            attrId = stroke.get_ID(1);
            str = stroke.get_Name(attrId);
            this.comboBox1.SelectedIndex = (int) stroke.get_Value(attrId);
            this.comboBox1.Tag = attrId;
            attrId = stroke.get_ID(2);
            str = stroke.get_Name(attrId);
            this.comboBox2.SelectedIndex = (int) stroke.get_Value(attrId);
            this.comboBox2.Tag = attrId;
            attrId = stroke.get_ID(3);
            str = stroke.get_Name(attrId);
            object obj2 = stroke.get_Value(attrId);
            this.symbolItem1.Symbol = obj2;
            this.symbolItem1.Tag = attrId;
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
                ((base.m_pGeometricEffect as IBasicLineSymbol).Stroke as IGraphicAttributes).set_Value(
                    (int) this.symbolItem1.Tag, this.symbolItem1.Symbol);
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
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
            ((base.m_pGeometricEffect as IBasicLineSymbol).Stroke as IGraphicAttributes).set_Value(
                (int) (sender as TextBox).Tag, val);
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.m_pControl != null)
            {
                this.m_pControl.RemoveControl(this);
            }
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGeometricEffectList list = new frmGeometricEffectList
            {
                BasicSymbolLayerBaseControl = this.m_pControl
            };
            if ((list.ShowDialog() == DialogResult.OK) && (this.m_pControl != null))
            {
                this.m_pControl.ReplaceControl(this, list.SelectControl);
            }
        }
    }
}