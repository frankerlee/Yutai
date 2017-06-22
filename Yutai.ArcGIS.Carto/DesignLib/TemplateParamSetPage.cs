using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.ControlExtend;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public partial class TemplateParamSetPage : UserControl
    {
        private System.Collections.Hashtable hashtable_0 = null;
        private IContainer icontainer_0 = null;
        private int int_0 = -1;

        public TemplateParamSetPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            this.hashtable_0.Clear();
            for (int i = 0; i < this.listView1.Items.Count; i++)
            {
                ListViewItem item = this.listView1.Items[i];
                this.hashtable_0.Add(item.Text, item.SubItems[1].Text);
            }
        }

 private void TemplateParamSetPage_Load(object sender, EventArgs e)
        {
            if (this.int_0 == -1)
            {
            }
        }

        public System.Collections.Hashtable Hashtable
        {
            set
            {
                this.hashtable_0 = value;
            }
        }

        public int TemplateOID
        {
            set
            {
                this.int_0 = value;
            }
        }
    }
}

