using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    internal partial class GeometricEffectReverseCurverPage : GeometricEffectBaseControl
    {
        private bool m_CanDo = false;
        private BasicSymbolLayerBaseControl m_pControl = null;

        public GeometricEffectReverseCurverPage(BasicSymbolLayerBaseControl pControl)
        {
            this.InitializeComponent();
            this.m_pControl = pControl;
            base.m_pGeometricEffect = new GeometricEffectReverseClass();
        }

        public override bool Apply()
        {
            (base.m_pGeometricEffect as IGraphicAttributes).set_Value((int) this.checkBox1.Tag, this.checkBox1.Checked);
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                (base.m_pGeometricEffect as IGraphicAttributes).set_Value((int) (sender as CheckBox).Tag,
                    (sender as CheckBox).Checked);
            }
        }

        private void GeometricEffectReverseCurverPage_Load(object sender, EventArgs e)
        {
            if (base.m_pGeometricEffect == null)
            {
                base.m_pGeometricEffect = new GeometricEffectReverseClass();
            }
            int graphicAttributeCount = (base.m_pGeometricEffect as IGraphicAttributes).GraphicAttributeCount;
            int attrId = (base.m_pGeometricEffect as IGraphicAttributes).get_ID(0);
            string str = (base.m_pGeometricEffect as IGraphicAttributes).get_Name(attrId);
            this.checkBox1.Checked = (bool) (base.m_pGeometricEffect as IGraphicAttributes).get_Value(attrId);
            this.checkBox1.Tag = attrId;
            this.m_CanDo = true;
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