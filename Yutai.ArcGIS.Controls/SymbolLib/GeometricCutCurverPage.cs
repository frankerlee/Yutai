using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    internal partial class GeometricCutCurverPage : GeometricEffectBaseControl
    {
        private BasicSymbolLayerBaseControl m_pControl = null;

        public GeometricCutCurverPage(BasicSymbolLayerBaseControl pControl)
        {
            this.InitializeComponent();
            this.m_pControl = pControl;
            base.m_pGeometricEffect = new GeometricEffectCutClass();
        }

        public override bool Apply()
        {
            double val = 0.0;
            double num2 = 0.0;
            try
            {
                val = double.Parse(this.txtStart.Text);
            }
            catch
            {
                MessageBox.Show("请出入数字型数据！");
                return false;
            }
            try
            {
                num2 = double.Parse(this.txtEnd.Text);
            }
            catch
            {
                MessageBox.Show("请出入数字型数据！");
                return false;
            }
            (base.m_pGeometricEffect as IGraphicAttributes).set_Value((int) this.txtStart.Tag, val);
            (base.m_pGeometricEffect as IGraphicAttributes).set_Value((int) this.txtEnd.Tag, num2);
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

 private Control FindControl(int id)
        {
            for (int i = 0; i < base.Controls.Count; i++)
            {
                if ((!(base.Controls[i] is Label) && (base.Controls[i].Tag != null)) && ((base.Controls[i].Tag is int) && (id == ((int) base.Controls[i].Tag))))
                {
                    return base.Controls[i];
                }
            }
            return null;
        }

        private void GeometricCutCurverPage_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < (base.m_pGeometricEffect as IGraphicAttributes).GraphicAttributeCount; i++)
            {
                int attrId = (base.m_pGeometricEffect as IGraphicAttributes).get_ID(i);
                string str = (base.m_pGeometricEffect as IGraphicAttributes).get_Name(attrId);
                if (attrId == 0)
                {
                    this.txtStart.Text = (base.m_pGeometricEffect as IGraphicAttributes).get_Value(attrId).ToString();
                    this.txtStart.Tag = attrId;
                }
                else
                {
                    this.txtEnd.Text = (base.m_pGeometricEffect as IGraphicAttributes).get_Value(attrId).ToString();
                    this.txtEnd.Tag = attrId;
                }
            }
        }

 private void txtStart_Leave(object sender, EventArgs e)
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

