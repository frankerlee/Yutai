using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class RepresentationRuleListBox1 : UserControl
    {
        private IContainer icontainer_0 = null;
        private IRepresentationRules irepresentationRules_0 = null;
        private ListBox listBox1;

        public RepresentationRuleListBox1()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            this.listBox1 = new ListBox();
            base.SuspendLayout();
            this.listBox1.Dock = DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(0xff, 0xd0);
            this.listBox1.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.listBox1);
            base.Name = "RepresentationRuleListBox1";
            base.Size = new Size(0xff, 0xd8);
            base.Load += new EventHandler(this.RepresentationRuleListBox1_Load);
            base.ResumeLayout(false);
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

