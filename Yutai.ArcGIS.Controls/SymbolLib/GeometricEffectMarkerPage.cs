using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    internal partial class GeometricEffectMarkerPage : GeometricEffectBaseControl
    {
        private BasicSymbolLayerBaseControl m_pControl = null;

        public GeometricEffectMarkerPage(BasicSymbolLayerBaseControl pControl)
        {
            this.InitializeComponent();
            this.m_pControl = pControl;
            base.m_pGeometricEffect = new BasicMarkerSymbolClass();
        }

        public override bool Apply()
        {
            double val = 0.0;
            double num2 = 0.0;
            try
            {
                val = double.Parse(this.textBox1.Text);
            }
            catch
            {
                MessageBox.Show("请出入数字型数据！");
                return false;
            }
            try
            {
                num2 = double.Parse(this.textBox2.Text);
            }
            catch
            {
                MessageBox.Show("请出入数字型数据！");
                return false;
            }
            (base.m_pGeometricEffect as IGraphicAttributes).set_Value((int) this.symbolItem1.Tag,
                this.symbolItem1.Symbol);
            (base.m_pGeometricEffect as IGraphicAttributes).set_Value((int) this.textBox1.Tag, val);
            (base.m_pGeometricEffect as IGraphicAttributes).set_Value((int) this.textBox2.Tag, num2);
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
            (base.m_pGeometricEffect as IGraphicAttributes).set_Value((int) (sender as CheckBox).Tag,
                (sender as CheckBox).Checked);
        }

        private void GeometricEffectMarkerPage_Load(object sender, EventArgs e)
        {
            if (base.m_pGeometricEffect == null)
            {
                base.m_pGeometricEffect = new BasicMarkerSymbolClass();
            }
            int graphicAttributeCount = (base.m_pGeometricEffect as IGraphicAttributes).GraphicAttributeCount;
            int attrId = (base.m_pGeometricEffect as IGraphicAttributes).get_ID(0);
            string str = (base.m_pGeometricEffect as IGraphicAttributes).get_Name(attrId);
            this.symbolItem1.Symbol = (base.m_pGeometricEffect as IGraphicAttributes).get_Value(attrId);
            this.symbolItem1.Tag = attrId;
            attrId = (base.m_pGeometricEffect as IGraphicAttributes).get_ID(1);
            str = (base.m_pGeometricEffect as IGraphicAttributes).get_Name(attrId);
            this.textBox1.Text = (base.m_pGeometricEffect as IGraphicAttributes).get_Value(attrId).ToString();
            this.textBox1.Tag = attrId;
            attrId = (base.m_pGeometricEffect as IGraphicAttributes).get_ID(2);
            str = (base.m_pGeometricEffect as IGraphicAttributes).get_Name(attrId);
            this.textBox2.Text = (base.m_pGeometricEffect as IGraphicAttributes).get_Value(attrId).ToString();
            this.textBox2.Tag = attrId;
        }

        private void symbolItem1_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            selector.SetSymbol(this.symbolItem1.Symbol);
            selector.SetStyleGallery(frmRepresationRule.m_pSG);
            if (selector.ShowDialog() == DialogResult.OK)
            {
                this.symbolItem1.Symbol = selector.GetSymbol();
                (base.m_pGeometricEffect as IGraphicAttributes).set_Value((int) this.symbolItem1.Tag,
                    selector.GetSymbol());
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
            (base.m_pGeometricEffect as IGraphicAttributes).set_Value((int) (sender as TextBox).Tag, val);
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