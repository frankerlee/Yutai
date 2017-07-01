using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    internal partial class MarkerPlacementRandomAlongLinePage : MarkerPlacementBaseControl
    {
        private bool m_CanDo = false;

        public MarkerPlacementRandomAlongLinePage(BasicSymbolLayerBaseControl pControl)
        {
            this.InitializeComponent();
            base.m_pControl = pControl;
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
            base.m_pGeometricEffect.set_Value((int) this.textBox1.Tag, val);
            base.m_pGeometricEffect.set_Value((int) this.checkBox1.Tag, this.checkBox1.Checked);
            base.m_pGeometricEffect.set_Value((int) this.comboBox1.Tag, this.comboBox1.SelectedIndex);
            base.m_pGeometricEffect.set_Value((int) this.comboBox2.Tag, this.comboBox3.SelectedIndex);
            base.m_pGeometricEffect.set_Value((int) this.comboBox3.Tag, this.comboBox3.SelectedIndex);
            return true;
        }

        private void btnChangeEffic_Click(object sender, EventArgs e)
        {
            frmMarkerPlacementList list = new frmMarkerPlacementList
            {
                BasicSymbolLayerBaseControl = base.m_pControl
            };
            if ((list.ShowDialog() == DialogResult.OK) && (base.m_pControl != null))
            {
                base.m_pControl.ReplaceControl(this, list.SelectControl);
                base.m_pBasicMarkerSymbol.MarkerPlacement =
                    (list.SelectControl as MarkerPlacementBaseControl).GeometricEffect as IMarkerPlacement;
                (list.SelectControl as MarkerPlacementBaseControl).BasicMarkerSymbol = base.m_pBasicMarkerSymbol;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                base.m_pGeometricEffect.set_Value((int) (sender as CheckBox).Tag, (sender as CheckBox).Checked);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                base.m_pGeometricEffect.set_Value((int) (sender as ComboBox).Tag, (sender as ComboBox).SelectedIndex);
            }
        }

        private void MarkerPlacementRandomAlongLinePage_Load(object sender, EventArgs e)
        {
            if (base.m_pGeometricEffect == null)
            {
                base.m_pGeometricEffect = new MarkerPlacementRandomAlongLineClass();
            }
            int graphicAttributeCount = base.m_pGeometricEffect.GraphicAttributeCount;
            int attrId = base.m_pGeometricEffect.get_ID(0);
            string str = base.m_pGeometricEffect.get_Name(attrId);
            this.textBox1.Text = base.m_pGeometricEffect.get_Value(attrId).ToString();
            this.textBox1.Tag = attrId;
            attrId = base.m_pGeometricEffect.get_ID(1);
            str = base.m_pGeometricEffect.get_Name(attrId);
            this.comboBox1.SelectedIndex = (int) base.m_pGeometricEffect.get_Value(attrId);
            this.comboBox1.Tag = attrId;
            attrId = base.m_pGeometricEffect.get_ID(2);
            str = base.m_pGeometricEffect.get_Name(attrId);
            this.comboBox2.SelectedIndex = (int) base.m_pGeometricEffect.get_Value(attrId);
            this.comboBox2.Tag = attrId;
            attrId = base.m_pGeometricEffect.get_ID(3);
            str = base.m_pGeometricEffect.get_Name(attrId);
            this.comboBox3.SelectedIndex = (int) base.m_pGeometricEffect.get_Value(attrId);
            this.comboBox3.Tag = attrId;
            attrId = base.m_pGeometricEffect.get_ID(4);
            str = base.m_pGeometricEffect.get_Name(attrId);
            this.checkBox1.Checked = (bool) base.m_pGeometricEffect.get_Value(attrId);
            this.checkBox1.Tag = attrId;
            this.m_CanDo = true;
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
            base.m_pGeometricEffect.set_Value((int) (sender as TextBox).Tag, val);
        }
    }
}