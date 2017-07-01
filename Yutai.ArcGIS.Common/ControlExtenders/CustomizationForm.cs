using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Common.ControlExtenders
{
    public partial class CustomizationForm : Form
    {
        private bool bool_0 = false;
        private Dictionary<string, IList<ToolStripItem>> dictionary_0 = new Dictionary<string, IList<ToolStripItem>>();
        private IContainer icontainer_0 = null;
        private StandardBarManager standardBarManager_0 = null;

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
                this.listView1.DoDragDrop(data,
                    DragDropEffects.Link | DragDropEffects.Move | DragDropEffects.Copy | DragDropEffects.Scroll);
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
                            ListViewItem item2 = new ListViewItem(item.Text)
                            {
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
            set { this.standardBarManager_0 = value; }
        }

        private partial class Class4
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
                get { return this.toolStrip_0; }
            }
        }

        private partial class Class5
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
                get { return this.toolStripItem_0; }
            }
        }
    }
}