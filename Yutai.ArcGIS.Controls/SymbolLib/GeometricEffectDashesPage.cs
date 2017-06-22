using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    internal partial class GeometricEffectDashesPage : GeometricEffectBaseControl
    {
        private BasicSymbolLayerBaseControl m_pControl = null;

        public GeometricEffectDashesPage(BasicSymbolLayerBaseControl pControl)
        {
            this.InitializeComponent();
            if (base.m_pGeometricEffect == null)
            {
                base.m_pGeometricEffect = new GeometricEffectDashClass();
            }
            this.m_pControl = pControl;
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
            (base.m_pGeometricEffect as IGraphicAttributes).set_Value((int) this.textBox1.Tag, val);
            (base.m_pGeometricEffect as IGraphicAttributes).set_Value((int) this.textBox2.Tag, num2);
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

 private void GeometricEffectDashesPage_Load(object sender, EventArgs e)
        {
            if (base.m_pGeometricEffect == null)
            {
                base.m_pGeometricEffect = new GeometricEffectDashClass();
            }
            int attrId = (base.m_pGeometricEffect as IGraphicAttributes).get_ID(0);
            string str = (base.m_pGeometricEffect as IGraphicAttributes).get_Name(attrId);
            double[] numArray = (double[]) (base.m_pGeometricEffect as IGraphicAttributes).get_Value(attrId);
            string str2 = numArray[0].ToString();
            for (int i = 1; i < numArray.Length; i++)
            {
                str2 = str2 + "," + numArray[i].ToString();
            }
            this.textBox1.Text = str2;
            this.textBox1.Tag = attrId;
            attrId = (base.m_pGeometricEffect as IGraphicAttributes).get_ID(1);
            str = (base.m_pGeometricEffect as IGraphicAttributes).get_Name(attrId);
            this.textBox2.Text = (base.m_pGeometricEffect as IGraphicAttributes).get_Value(attrId).ToString();
            this.textBox2.Tag = attrId;
        }

 private void textBox1_Leave(object sender, EventArgs e)
        {
            if (sender == this.textBox1)
            {
                string[] strArray = (sender as TextBox).Text.Split(new char[] { ',' });
                double[] val = new double[strArray.Length];
                for (int i = 0; i < val.Length; i++)
                {
                    try
                    {
                        val[i] = double.Parse(strArray[i]);
                    }
                    catch
                    {
                        return;
                    }
                }
                (base.m_pGeometricEffect as IGraphicAttributes).set_Value((int) (sender as TextBox).Tag, val);
            }
            else
            {
                double num2 = 0.0;
                try
                {
                    num2 = double.Parse((sender as TextBox).Text);
                }
                catch
                {
                    return;
                }
                (base.m_pGeometricEffect as IGraphicAttributes).set_Value((int) (sender as TextBox).Tag, num2);
            }
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

