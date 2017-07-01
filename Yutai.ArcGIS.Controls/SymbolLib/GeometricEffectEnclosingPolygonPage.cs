using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    internal partial class GeometricEffectEnclosingPolygonPage : GeometricEffectBaseControl
    {
        private bool m_CanDo = false;
        private BasicSymbolLayerBaseControl m_pControl = null;

        public GeometricEffectEnclosingPolygonPage(BasicSymbolLayerBaseControl pControl)
        {
            this.InitializeComponent();
            this.m_pControl = pControl;
            base.m_pGeometricEffect = new GeometricEffectEnclosingPolygonClass();
        }

        public override bool Apply()
        {
            if (this.comboBox1.SelectedIndex == -1)
            {
                return false;
            }
            (base.m_pGeometricEffect as IGraphicAttributes).set_Value((int) this.comboBox1.Tag,
                this.comboBox1.SelectedIndex);
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
                (base.m_pGeometricEffect as IGraphicAttributes).set_Value((int) (sender as ComboBox).Tag,
                    (sender as ComboBox).SelectedIndex);
            }
        }

        private void GeometricEffectEnclosingPolygonPage_Load(object sender, EventArgs e)
        {
            if (base.m_pGeometricEffect == null)
            {
                base.m_pGeometricEffect = new GeometricEffectEnclosingPolygonClass();
            }
            int attrId = (base.m_pGeometricEffect as IGraphicAttributes).get_ID(0);
            string str = (base.m_pGeometricEffect as IGraphicAttributes).get_Name(0);
            this.comboBox1.SelectedIndex = (int) (base.m_pGeometricEffect as IGraphicAttributes).get_Value(attrId);
            this.comboBox1.Tag = attrId;
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