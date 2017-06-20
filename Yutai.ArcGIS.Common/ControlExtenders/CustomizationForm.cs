using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Common.ControlExtenders
{
    public class CustomizationForm : Form
    {
        private bool bool_0 = false;
        private Button button1;
        private CheckedListBox chlsbCommands;
        private CheckedListBox cklsbtoolBar;
        private ColumnHeader columnHeader_0;
        private Dictionary<string, IList<ToolStripItem>> dictionary_0 = new Dictionary<string, IList<ToolStripItem>>();
        private IContainer icontainer_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private ListView listView1;
        private ListBox lsbCatalog;
        private StandardBarManager standardBarManager_0 = null;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;

        public CustomizationForm()
        {
            this.InitializeComponent();
        }

        private void chlsbCommands_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (this.bool_0)
            {
                Class5 class2 = this.chlsbCommands.Items[e.Index] as Class5;
                if (class2 != null)
                {
                    class2.ToolStripItem.Visible = !class2.ToolStripItem.Visible;
                }
            }
        }

        private void chlsbCommands_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cklsbtoolBar_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (this.bool_0)
            {
                Class4 class2 = this.cklsbtoolBar.Items[e.Index] as Class4;
                if (class2 != null)
                {
                    class2.ToolStrip.Visible = !class2.ToolStrip.Visible;
                }
            }
        }

        private void CustomizationForm_Load(object sender, EventArgs e)
        {
            if (this.standardBarManager_0 != null)
            {
                for (int i = 0; i < this.standardBarManager_0.BarCount; i++)
                {
                    ToolStrip bar = this.standardBarManager_0.GetBar(i);
                    if (bar != this.standardBarManager_0.MainMenu)
                    {
                        this.method_3(bar);
                        this.cklsbtoolBar.Items.Add(new Class4(bar), bar.Visible);
                    }
                }
                this.bool_0 = true;
            }
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomizationForm));
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.label3 = new Label();
            this.cklsbtoolBar = new CheckedListBox();
            this.tabPage2 = new TabPage();
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.label2 = new Label();
            this.label1 = new Label();
            this.lsbCatalog = new ListBox();
            this.chlsbCommands = new CheckedListBox();
            this.button1 = new Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            base.SuspendLayout();
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = DockStyle.Top;
            this.tabControl1.Location = new Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x182, 0x105);
            this.tabControl1.TabIndex = 0;
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.cklsbtoolBar);
            this.tabPage1.Location = new Point(4, 0x15);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(0x17a, 0xec);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "工具栏";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(6, 13);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x29, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "工具栏";
            this.cklsbtoolBar.FormattingEnabled = true;
            this.cklsbtoolBar.Location = new Point(8, 0x1c);
            this.cklsbtoolBar.Name = "cklsbtoolBar";
            this.cklsbtoolBar.Size = new Size(0x16a, 0xc4);
            this.cklsbtoolBar.TabIndex = 0;
            this.cklsbtoolBar.ItemCheck += new ItemCheckEventHandler(this.cklsbtoolBar_ItemCheck);
            this.tabPage2.Controls.Add(this.listView1);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.lsbCatalog);
            this.tabPage2.Controls.Add(this.chlsbCommands);
            this.tabPage2.Location = new Point(4, 0x15);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(3);
            this.tabPage2.Size = new Size(0x17a, 0xec);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "命令";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0 });
            this.listView1.HeaderStyle = ColumnHeaderStyle.None;
            this.listView1.HideSelection = false;
            this.listView1.Location = new Point(160, 0x16);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(210, 0xd6);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.ItemChecked += new ItemCheckedEventHandler(this.listView1_ItemChecked);
            this.listView1.MouseMove += new MouseEventHandler(this.listView1_MouseMove);
            this.columnHeader_0.Width = 0xb8;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x9e, 7);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "命令";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "类别";
            this.lsbCatalog.FormattingEnabled = true;
            this.lsbCatalog.ItemHeight = 12;
            this.lsbCatalog.Location = new Point(8, 0x16);
            this.lsbCatalog.Name = "lsbCatalog";
            this.lsbCatalog.Size = new Size(0x92, 0xd0);
            this.lsbCatalog.TabIndex = 2;
            this.lsbCatalog.SelectedIndexChanged += new EventHandler(this.lsbCatalog_SelectedIndexChanged);
            this.chlsbCommands.FormattingEnabled = true;
            this.chlsbCommands.Location = new Point(0x102, 0x16);
            this.chlsbCommands.Name = "chlsbCommands";
            this.chlsbCommands.Size = new Size(0x70, 0xd4);
            this.chlsbCommands.TabIndex = 1;
            this.chlsbCommands.SelectedIndexChanged += new EventHandler(this.chlsbCommands_SelectedIndexChanged);
            this.chlsbCommands.ItemCheck += new ItemCheckEventHandler(this.chlsbCommands_ItemCheck);
            this.button1.DialogResult = DialogResult.OK;
            this.button1.Location = new Point(0x11b, 0x10b);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x3d, 0x19);
            this.button1.TabIndex = 1;
            this.button1.Text = "关闭";
            this.button1.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x182, 0x126);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.tabControl1);
            base.Name = "CustomizationForm";
            this.Text = "自定义";
            base.Load += new EventHandler(this.CustomizationForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            base.ResumeLayout(false);
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (this.bool_0)
            {
                ToolStripItem tag = e.Item.Tag as ToolStripItem;
                if (tag != null)
                {
                    tag.Visible = !tag.Visible;
                }
            }
        }

        private void listView1_MouseMove(object sender, MouseEventArgs e)
        {
            if (((e.Button & MouseButtons.Left) == MouseButtons.Left) && (this.listView1.SelectedItems.Count > 0))
            {
                DataObject data = new DataObject();
                data.SetData(this.listView1.SelectedItems[0]);
                this.listView1.DoDragDrop(data, DragDropEffects.Link | DragDropEffects.Move | DragDropEffects.Copy | DragDropEffects.Scroll);
            }
        }

        private void lsbCatalog_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.listView1.Items.Clear();
                if (this.lsbCatalog.SelectedIndex != -1)
                {
                    IList<ToolStripItem> list;
                    string str = this.lsbCatalog.SelectedItem.ToString();
                    try
                    {
                        list = this.dictionary_0[str];
                    }
                    catch
                    {
                        list = null;
                    }
                    this.bool_0 = false;
                    if (list != null)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            ToolStripItem item = list[i];
                            ListViewItem item2 = new ListViewItem(item.Text) {
                                Tag = item,
                                Checked = item.Visible
                            };
                            this.listView1.Items.Add(item2);
                        }
                    }
                    this.bool_0 = true;
                }
            }
        }

        private void method_0(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void method_1(object sender, DragEventArgs e)
        {
        }

        private void method_2(ToolStripMenuItem toolStripMenuItem_0)
        {
            string category = "杂项";
            for (int i = 0; i < toolStripMenuItem_0.DropDownItems.Count; i++)
            {
                IList<ToolStripItem> list;
                ToolStripItem item = toolStripMenuItem_0.DropDownItems[i];
                if (item.Tag is ICommand)
                {
                    category = (item.Tag as ICommand).Category;
                    if ((category == null) || (category.Trim().Length == 0))
                    {
                        category = "杂项";
                    }
                }
                else if (item is ToolStripMenuItem)
                {
                    if ((item as ToolStripMenuItem).DropDownItems.Count > 0)
                    {
                        this.method_2(item as ToolStripMenuItem);
                    }
                    else
                    {
                        category = "杂项";
                    }
                }
                else
                {
                    category = "杂项";
                }
                try
                {
                    list = this.dictionary_0[category];
                }
                catch
                {
                    list = null;
                }
                if (list == null)
                {
                    this.lsbCatalog.Items.Add(category);
                    list = new List<ToolStripItem>();
                    this.dictionary_0.Add(category, list);
                }
                list.Add(item);
            }
        }

        private void method_3(ToolStrip toolStrip_0)
        {
            string category = "杂项";
            for (int i = 0; i < toolStrip_0.Items.Count; i++)
            {
                IList<ToolStripItem> list;
                ToolStripItem item = toolStrip_0.Items[i];
                if (item.Tag is ICommand)
                {
                    category = (item.Tag as ICommand).Category;
                    if ((category == null) || (category.Trim().Length == 0))
                    {
                        category = "杂项";
                    }
                }
                else if (item is ToolStripMenuItem)
                {
                    if ((item as ToolStripMenuItem).DropDownItems.Count > 0)
                    {
                        this.method_2(item as ToolStripMenuItem);
                    }
                    else
                    {
                        category = "杂项";
                    }
                }
                else
                {
                    category = "杂项";
                }
                try
                {
                    list = this.dictionary_0[category];
                }
                catch
                {
                    list = null;
                }
                if (list == null)
                {
                    this.lsbCatalog.Items.Add(category);
                    list = new List<ToolStripItem>();
                    this.dictionary_0.Add(category, list);
                }
                list.Add(item);
            }
        }

        public StandardBarManager StandardBarManager
        {
            set
            {
                this.standardBarManager_0 = value;
            }
        }

        private class Class4
        {
            private System.Windows.Forms.ToolStrip toolStrip_0 = null;

            public Class4(System.Windows.Forms.ToolStrip toolStrip_1)
            {
                this.toolStrip_0 = toolStrip_1;
            }

            public override string ToString()
            {
                return this.toolStrip_0.Text;
            }

            public System.Windows.Forms.ToolStrip ToolStrip
            {
                get
                {
                    return this.toolStrip_0;
                }
            }
        }

        private class Class5
        {
            private System.Windows.Forms.ToolStripItem toolStripItem_0 = null;

            public Class5(System.Windows.Forms.ToolStripItem toolStripItem_1)
            {
                this.toolStripItem_0 = toolStripItem_1;
            }

            public override string ToString()
            {
                return this.toolStripItem_0.Text;
            }

            public System.Windows.Forms.ToolStripItem ToolStripItem
            {
                get
                {
                    return this.toolStripItem_0;
                }
            }
        }
    }
}

