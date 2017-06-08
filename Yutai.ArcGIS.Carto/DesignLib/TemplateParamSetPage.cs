using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class TemplateParamSetPage : UserControl
    {
        private System.Collections.Hashtable hashtable_0 = null;
        private IContainer icontainer_0 = null;
        private int int_0 = -1;
        private Label lbl1;
        private EditListView listView1;
        private LVColumnHeader lvcolumnHeader_0;
        private LVColumnHeader lvcolumnHeader_1;

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
            this.listView1 = new EditListView();
            this.lvcolumnHeader_0 = new LVColumnHeader();
            this.lvcolumnHeader_1 = new LVColumnHeader();
            this.lbl1 = new Label();
            base.SuspendLayout();
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.lvcolumnHeader_0, this.lvcolumnHeader_1 });
            this.listView1.ComboBoxBgColor = Color.LightBlue;
            this.listView1.ComboBoxFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.listView1.EditBgColor = Color.LightBlue;
            this.listView1.EditFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new Point(0x10, 0x25);
            this.listView1.LockRowCount = 0;
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x110, 0x60);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.lvcolumnHeader_0.ColumnStyle = ListViewColumnStyle.ReadOnly;
            this.lvcolumnHeader_0.Text = "参数";
            this.lvcolumnHeader_0.Width = 0x90;
            this.lvcolumnHeader_1.ColumnStyle = ListViewColumnStyle.EditBox;
            this.lvcolumnHeader_1.Text = "值";
            this.lvcolumnHeader_1.Width = 0x7a;
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new Point(14, 9);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new Size(0x35, 12);
            this.lbl1.TabIndex = 3;
            this.lbl1.Text = "参数列表";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.lbl1);
            base.Controls.Add(this.listView1);
            base.Name = "TemplateParamSetPage";
            base.Size = new Size(0x139, 0x9f);
            base.Load += new EventHandler(this.TemplateParamSetPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
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

