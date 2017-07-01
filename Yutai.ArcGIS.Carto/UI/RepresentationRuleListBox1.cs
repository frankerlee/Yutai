using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class RepresentationRuleListBox1 : UserControl
    {
        private IContainer icontainer_0 = null;
        private IRepresentationRules irepresentationRules_0 = null;

        public RepresentationRuleListBox1()
        {
            this.InitializeComponent();
        }

        private void RepresentationRuleListBox1_Load(object sender, EventArgs e)
        {
            int num;
            IRepresentationRule rule;
            this.irepresentationRules_0.Reset();
            this.irepresentationRules_0.Next(out num, out rule);
            this.listBox1.Items.Add(rule.LayerCount.ToString());
        }
    }
}