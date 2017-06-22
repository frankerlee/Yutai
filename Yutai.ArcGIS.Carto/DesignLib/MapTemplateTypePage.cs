using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public partial class MapTemplateTypePage : UserControl
    {
        private IContainer icontainer_0 = null;
        internal int m_MapTemplateType = 0;

        public MapTemplateTypePage()
        {
            this.InitializeComponent();
        }

 private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButton2.Checked)
            {
                this.m_MapTemplateType = 1;
            }
        }

        private void rdoRectangle_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoRectangle.Checked)
            {
                this.m_MapTemplateType = 0;
            }
        }
    }
}

