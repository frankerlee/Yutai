using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    internal partial class GeometricSmoothCurverPage : GeometricEffectBaseControl
    {
        private BasicSymbolLayerBaseControl m_pControl = null;

        public GeometricSmoothCurverPage(BasicSymbolLayerBaseControl pControl)
        {
            this.InitializeComponent();
            this.m_pControl = pControl;
            base.m_pGeometricEffect = new GeometricEffectSmoothClass();
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
            (base.m_pGeometricEffect as IGraphicAttributes).set_Value((int) this.textBox1.Tag, val);
            return true;
        }

        private void btnAddGemoetricEffic_Click(object sender, EventArgs e)
        {
            frmGeometricEffectList list = new frmGeometricEffectList {
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

 private void GeometricSmoothCurverPage_Load(object sender, EventArgs e)
        {
            if (base.m_pGeometricEffect == null)
            {
                base.m_pGeometricEffect = new GeometricEffectSmoothClass();
            }
            int graphicAttributeCount = (base.m_pGeometricEffect as IGraphicAttributes).GraphicAttributeCount;
            int attrId = (base.m_pGeometricEffect as IGraphicAttributes).get_ID(0);
            string str = (base.m_pGeometricEffect as IGraphicAttributes).get_Name(attrId);
            this.textBox1.Text = (base.m_pGeometricEffect as IGraphicAttributes).get_Value(attrId).ToString();
            this.textBox1.Tag = attrId;
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
            frmGeometricEffectList list = new frmGeometricEffectList {
                BasicSymbolLayerBaseControl = this.m_pControl
            };
            if ((list.ShowDialog() == DialogResult.OK) && (this.m_pControl != null))
            {
                this.m_pControl.ReplaceControl(this, list.SelectControl);
            }
        }
    }
}

