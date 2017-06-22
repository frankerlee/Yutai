using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    internal partial class MarkerPlacementOnPointPage : MarkerPlacementBaseControl
    {

        public MarkerPlacementOnPointPage(BasicSymbolLayerBaseControl pControl)
        {
            this.InitializeComponent();
            base.m_pControl = pControl;
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
            base.m_pGeometricEffect.set_Value((int) this.textBox1.Tag, val);
            base.m_pGeometricEffect.set_Value((int) this.textBox2.Tag, num2);
            return true;
        }

        private void btnChangeEffic_Click(object sender, EventArgs e)
        {
            frmMarkerPlacementList list = new frmMarkerPlacementList {
                BasicSymbolLayerBaseControl = base.m_pControl
            };
            if ((list.ShowDialog() == DialogResult.OK) && (base.m_pControl != null))
            {
                base.m_pControl.ReplaceControl(this, list.SelectControl);
                base.m_pBasicMarkerSymbol.MarkerPlacement = (list.SelectControl as MarkerPlacementBaseControl).GeometricEffect as IMarkerPlacement;
                (list.SelectControl as MarkerPlacementBaseControl).BasicMarkerSymbol = base.m_pBasicMarkerSymbol;
            }
        }

 private void MarkerPlacementOnPointPage_Load(object sender, EventArgs e)
        {
            if (base.m_pGeometricEffect == null)
            {
                base.m_pGeometricEffect = new MarkerPlacementOnPointClass();
            }
            int graphicAttributeCount = base.m_pGeometricEffect.GraphicAttributeCount;
            int attrId = base.m_pGeometricEffect.get_ID(0);
            string str = base.m_pGeometricEffect.get_Name(attrId);
            this.textBox1.Text = base.m_pGeometricEffect.get_Value(attrId).ToString();
            this.textBox1.Tag = attrId;
            attrId = base.m_pGeometricEffect.get_ID(1);
            str = base.m_pGeometricEffect.get_Name(attrId);
            this.textBox2.Text = base.m_pGeometricEffect.get_Value(attrId).ToString();
            this.textBox2.Tag = attrId;
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

