using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.Editor;

namespace Yutai.ArcGIS.Controls.Controls.EditorUI
{
    public partial class frmSelectSymbolClass : Form
    {

        public frmSelectSymbolClass()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListViewItem item = this.listView1.SelectedItems[0];
            EditTemplateSchem tag = item.Tag as EditTemplateSchem;
            foreach (KeyValuePair<string, object> pair in tag.FieldValues)
            {
                this.EditTemplate.SetFieldValue(pair.Key, pair.Value.ToString());
            }
        }

 private void frmSelectSymbolClass_Load(object sender, EventArgs e)
        {
            this.button1.Enabled = false;
            string[] items = new string[3];
            for (int i = 0; i < this.EditTemplate.EditTemplateSchems.Count; i++)
            {
                EditTemplateSchem schem = this.EditTemplate.EditTemplateSchems[i];
                items[0] = schem.Value;
                items[1] = schem.Label;
                items[2] = schem.Description;
                ListViewItem item = new ListViewItem(items) {
                    Tag = schem
                };
                this.listView1.Items.Add(item);
            }
        }

 private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.button1.Enabled = this.listView1.SelectedIndices.Count > 0;
        }

        public YTEditTemplate EditTemplate { get; set; }
    }
}

