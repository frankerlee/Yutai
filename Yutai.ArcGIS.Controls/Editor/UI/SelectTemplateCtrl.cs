using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public class SelectTemplateCtrl : UserControl
    {
        private Button btnClearAll;
        private Button btnSelectAll;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private IContainer components = null;
        private List<List<YTEditTemplateWrap>> EditTemplates = new List<List<YTEditTemplateWrap>>();
        private ImageList imageList1;
        private Label label1;
        private Label label2;
        private List<IFeatureLayer> Layers = new List<IFeatureLayer>();
        private ListView listView1;
        internal int Step = 0;
        internal int StepCount = 0;

        public SelectTemplateCtrl()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        internal void InitControl()
        {
            this.listView1.Items.Clear();
            string[] items = new string[2];
            foreach (YTEditTemplateWrap wrap in this.EditTemplates[this.Step])
            {
                items[0] = wrap.EditTemplate.Name;
                items[1] = wrap.EditTemplate.Name;
                ListViewItem item = new ListViewItem(items) {
                    Tag = wrap,
                    Checked = wrap.IsUse
                };
                if (!this.imageList1.Images.ContainsKey(wrap.EditTemplate.ImageKey) && (wrap.EditTemplate.Bitmap != null))
                {
                    this.imageList1.Images.Add((string) wrap.EditTemplate.ImageKey, (Image)wrap.EditTemplate.Bitmap);
                }
                item.ImageKey = wrap.EditTemplate.ImageKey;
                this.listView1.Items.Add(item);
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.listView1 = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.imageList1 = new ImageList(this.components);
            this.label1 = new Label();
            this.label2 = new Label();
            this.btnClearAll = new Button();
            this.btnSelectAll = new Button();
            base.SuspendLayout();
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader1, this.columnHeader2 });
            this.listView1.Location = new Point(0x11, 0x27);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x124, 160);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.ItemChecked += new ItemCheckedEventHandler(this.listView1_ItemChecked);
            this.columnHeader1.Text = "模板名称";
            this.columnHeader1.Width = 0x75;
            this.columnHeader2.Text = "类别";
            this.columnHeader2.Width = 0x94;
            this.imageList1.ColorDepth = ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new Size(0x10, 0x10);
            this.imageList1.TransparentColor = Color.Transparent;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(15, 13);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "图层：";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x3e, 13);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "        ";
            this.btnClearAll.Location = new Point(0x13b, 0x44);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new Size(0x4b, 0x17);
            this.btnClearAll.TabIndex = 4;
            this.btnClearAll.Text = "全部清除";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Location = new Point(0x13b, 0x27);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new Size(0x4b, 0x17);
            this.btnSelectAll.TabIndex = 3;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.btnClearAll);
            base.Controls.Add(this.btnSelectAll);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.listView1);
            base.Name = "SelectTemplateCtrl";
            base.Size = new Size(0x18e, 0xee);
            base.Load += new EventHandler(this.SelectTemplateCtrl_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public bool LastStep()
        {
            this.Step--;
            this.InitControl();
            return (this.Step > 0);
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            (e.Item.Tag as YTEditTemplateWrap).IsUse = e.Item.Checked;
        }

        public bool NextStep(bool IsAdd)
        {
            if (IsAdd)
            {
                this.Step++;
            }
            this.InitControl();
            return (this.Step < (this.StepCount - 1));
        }

        private void SelectTemplateCtrl_Load(object sender, EventArgs e)
        {
            foreach (KeyValuePair<IFeatureLayer, List<YTEditTemplateWrap>> pair in this.Templates)
            {
                if (pair.Key is IFDOGraphicsLayer)
                {
                    this.Layers.Add(pair.Key);
                    this.EditTemplates.Add(pair.Value);
                }
                else if (pair.Value.Count > 1)
                {
                    this.Layers.Add(pair.Key);
                    this.EditTemplates.Add(pair.Value);
                }
            }
            this.StepCount = this.Layers.Count;
        }

        internal Dictionary<IFeatureLayer, List<YTEditTemplateWrap>> Templates { get; set; }
    }
}

