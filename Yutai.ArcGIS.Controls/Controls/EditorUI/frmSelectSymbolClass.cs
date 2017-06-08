using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Controls.Controls.EditorUI
{
    public class frmSelectSymbolClass : Form
    {
        private Button button1;
        private Button button2;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private IContainer components = null;
        private ListView listView1;

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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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

        private void InitializeComponent()
        {
            this.listView1 = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.columnHeader3 = new ColumnHeader();
            this.button1 = new Button();
            this.button2 = new Button();
            base.SuspendLayout();
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader3 });
            this.listView1.Location = new Point(12, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x13d, 0x8d);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader1.Text = "值";
            this.columnHeader1.Width = 0x71;
            this.columnHeader2.Text = "分类";
            this.columnHeader2.Width = 0x66;
            this.columnHeader3.Text = "描述";
            this.columnHeader3.Width = 0x56;
            this.button1.DialogResult = DialogResult.OK;
            this.button1.Location = new Point(0xa2, 0xa9);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 1;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.DialogResult = DialogResult.Cancel;
            this.button2.Location = new Point(0xfe, 0xa9);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 2;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x155, 0xcf);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.listView1);
            base.Name = "frmSelectSymbolClass";
            this.Text = "选择符号类";
            base.Load += new EventHandler(this.frmSelectSymbolClass_Load);
            base.ResumeLayout(false);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.button1.Enabled = this.listView1.SelectedIndices.Count > 0;
        }

        public JLKEditTemplate EditTemplate { get; set; }
    }
}

