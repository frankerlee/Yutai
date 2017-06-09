using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class LegendLayerUserControl : UserControl
    {
        private bool bool_0 = false;
        private SimpleButton btnMoveDown;
        private SimpleButton btnMoveUp;
        private SimpleButton btnSelect;
        private SimpleButton btnSelectAll;
        private SimpleButton btnUnSelect;
        private SimpleButton btnUnSelectAll;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private ILegend ilegend_0 = null;
        private IMap imap_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private ListBoxControl listLegendLayers;
        private ListBoxControl listMapLayers;
        private SpinEdit spinEdit1;

        public LegendLayerUserControl()
        {
            this.InitializeComponent();
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.listLegendLayers.SelectedIndex;
            object selectedItem = this.listLegendLayers.SelectedItem;
            this.listLegendLayers.Items.RemoveAt(selectedIndex);
            this.ilegend_0.RemoveItem(selectedIndex);
            selectedIndex++;
            if (selectedIndex == this.ilegend_0.ItemCount)
            {
                this.ilegend_0.AddItem((selectedItem as LegendItemWrap).LegendItem);
                this.listLegendLayers.Items.Add(selectedItem);
            }
            else
            {
                this.ilegend_0.InsertItem(selectedIndex, (selectedItem as LegendItemWrap).LegendItem);
                this.listLegendLayers.Items.Insert(selectedIndex, selectedItem);
            }
            this.listLegendLayers.SelectedItem = selectedItem;
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.listLegendLayers.SelectedIndex;
            object selectedItem = this.listLegendLayers.SelectedItem;
            this.listMapLayers.Items.RemoveAt(selectedIndex);
            this.ilegend_0.RemoveItem(selectedIndex);
            selectedIndex--;
            this.ilegend_0.InsertItem(selectedIndex, (selectedItem as LegendItemWrap).LegendItem);
            this.listLegendLayers.Items.Insert(selectedIndex, selectedItem);
            this.listLegendLayers.SelectedItem = selectedItem;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.listMapLayers.SelectedIndices.Count; i++)
            {
                ILayer layer = (this.listMapLayers.Items[this.listMapLayers.SelectedIndices[i]] as ObjectWrap).Object as ILayer;
                ILegendItem item = this.method_2(layer);
                this.listLegendLayers.Items.Add(new LegendItemWrap(item));
                this.ilegend_0.AddItem(item);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.listMapLayers.Items.Count; i++)
            {
                ILayer layer = (this.listMapLayers.Items[i] as ObjectWrap).Object as ILayer;
                ILegendItem item = this.method_2(layer);
                this.listLegendLayers.Items.Add(new LegendItemWrap(item));
                this.ilegend_0.AddItem(item);
            }
        }

        private void btnUnSelect_Click(object sender, EventArgs e)
        {
            for (int i = this.listLegendLayers.SelectedIndices.Count - 1; i >= 0; i--)
            {
                this.ilegend_0.RemoveItem(this.listLegendLayers.SelectedIndices[i]);
                this.listLegendLayers.Items.RemoveAt(this.listLegendLayers.SelectedIndices[i]);
            }
        }

        private void btnUnSelectAll_Click(object sender, EventArgs e)
        {
            this.ilegend_0.ClearItems();
            this.listLegendLayers.Items.Clear();
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        public IArray GetSelectLegendItem()
        {
            IArray array = new ArrayClass();
            for (int i = 0; i < this.listLegendLayers.Items.Count; i++)
            {
                array.Add(this.listLegendLayers.Items[i]);
            }
            return array;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LegendLayerUserControl));
            this.groupBox1 = new GroupBox();
            this.listLegendLayers = new ListBoxControl();
            this.listMapLayers = new ListBoxControl();
            this.btnMoveDown = new SimpleButton();
            this.btnMoveUp = new SimpleButton();
            this.btnUnSelectAll = new SimpleButton();
            this.btnUnSelect = new SimpleButton();
            this.btnSelectAll = new SimpleButton();
            this.btnSelect = new SimpleButton();
            this.label2 = new Label();
            this.label1 = new Label();
            this.label3 = new Label();
            this.spinEdit1 = new SpinEdit();
            this.groupBox1.SuspendLayout();
            ((ISupportInitialize) this.listLegendLayers).BeginInit();
            ((ISupportInitialize) this.listMapLayers).BeginInit();
            this.spinEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.listLegendLayers);
            this.groupBox1.Controls.Add(this.listMapLayers);
            this.groupBox1.Controls.Add(this.btnMoveDown);
            this.groupBox1.Controls.Add(this.btnMoveUp);
            this.groupBox1.Controls.Add(this.btnUnSelectAll);
            this.groupBox1.Controls.Add(this.btnUnSelect);
            this.groupBox1.Controls.Add(this.btnSelectAll);
            this.groupBox1.Controls.Add(this.btnSelect);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x158, 0xd0);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择要创建图例的图层";
            this.listLegendLayers.ItemHeight = 15;
            this.listLegendLayers.Location = new Point(0xb0, 0x30);
            this.listLegendLayers.Name = "listLegendLayers";
            this.listLegendLayers.Size = new Size(0x70, 0x90);
            this.listLegendLayers.TabIndex = 13;
            this.listLegendLayers.SelectedIndexChanged += new EventHandler(this.listLegendLayers_SelectedIndexChanged);
            this.listMapLayers.ItemHeight = 15;
            this.listMapLayers.Location = new Point(8, 0x30);
            this.listMapLayers.Name = "listMapLayers";
            this.listMapLayers.Size = new Size(0x68, 0x88);
            this.listMapLayers.TabIndex = 12;
            this.listMapLayers.SelectedIndexChanged += new EventHandler(this.listMapLayers_SelectedIndexChanged);
            this.btnMoveDown.Image = (System.Drawing.Image)resources.GetObject("btnMoveDown.Image");
            this.btnMoveDown.Location = new Point(0x128, 0x58);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new Size(0x20, 0x18);
            this.btnMoveDown.TabIndex = 9;
            this.btnMoveDown.Click += new EventHandler(this.btnMoveDown_Click);
            this.btnMoveUp.Image = (System.Drawing.Image)resources.GetObject("btnMoveUp.Image");
            this.btnMoveUp.Location = new Point(0x128, 0x30);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new Size(0x20, 0x18);
            this.btnMoveUp.TabIndex = 8;
            this.btnMoveUp.Click += new EventHandler(this.btnMoveUp_Click);
            this.btnUnSelectAll.Location = new Point(0x80, 0xa8);
            this.btnUnSelectAll.Name = "btnUnSelectAll";
            this.btnUnSelectAll.Size = new Size(0x20, 0x18);
            this.btnUnSelectAll.TabIndex = 7;
            this.btnUnSelectAll.Text = "<<";
            this.btnUnSelectAll.Click += new EventHandler(this.btnUnSelectAll_Click);
            this.btnUnSelect.Enabled = false;
            this.btnUnSelect.Location = new Point(0x80, 0x88);
            this.btnUnSelect.Name = "btnUnSelect";
            this.btnUnSelect.Size = new Size(0x20, 0x18);
            this.btnUnSelect.TabIndex = 6;
            this.btnUnSelect.Text = "<";
            this.btnUnSelect.Click += new EventHandler(this.btnUnSelect_Click);
            this.btnSelectAll.Location = new Point(0x80, 80);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new Size(0x20, 0x18);
            this.btnSelectAll.TabIndex = 5;
            this.btnSelectAll.Text = ">>";
            this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
            this.btnSelect.Enabled = false;
            this.btnSelect.Location = new Point(0x80, 0x30);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new Size(0x20, 0x18);
            this.btnSelect.TabIndex = 4;
            this.btnSelect.Text = ">";
            this.btnSelect.Click += new EventHandler(this.btnSelect_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0xb0, 0x18);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x2a, 0x11);
            this.label2.TabIndex = 2;
            this.label2.Text = "图例项";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(60, 0x11);
            this.label1.TabIndex = 1;
            this.label1.Text = "地图图层:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x18, 0xe3);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x36, 0x11);
            this.label3.TabIndex = 1;
            this.label3.Text = "图例列数";
            this.label3.Visible = false;
            int[] bits = new int[4];
            bits[0] = 1;
            this.spinEdit1.EditValue = new decimal(bits);
            this.spinEdit1.Location = new Point(0x58, 0xdf);
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.spinEdit1.Properties.UseCtrlIncrement = false;
            this.spinEdit1.Size = new Size(0x68, 0x17);
            this.spinEdit1.TabIndex = 2;
            this.spinEdit1.Visible = false;
            this.spinEdit1.EditValueChanged += new EventHandler(this.spinEdit1_EditValueChanged);
            base.Controls.Add(this.spinEdit1);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.groupBox1);
            base.Name = "LegendLayerUserControl";
            base.Size = new Size(400, 0x108);
            base.Load += new EventHandler(this.LegendLayerUserControl_Load);
            this.groupBox1.ResumeLayout(false);
            ((ISupportInitialize) this.listLegendLayers).EndInit();
            ((ISupportInitialize) this.listMapLayers).EndInit();
            this.spinEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void LegendLayerUserControl_Load(object sender, EventArgs e)
        {
            this.method_1();
            this.bool_0 = true;
        }

        private void listLegendLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listMapLayers.SelectedIndex >= 0)
            {
                this.btnUnSelect.Enabled = true;
            }
            else
            {
                this.btnUnSelect.Enabled = false;
            }
            if (this.listLegendLayers.Items.Count <= 1)
            {
                this.btnMoveDown.Enabled = false;
                this.btnMoveUp.Enabled = false;
            }
            else if (this.listLegendLayers.SelectedIndices.Count == 1)
            {
                if (this.listLegendLayers.SelectedIndex > 0)
                {
                    this.btnMoveDown.Enabled = true;
                }
                else if (this.listLegendLayers.SelectedIndex < (this.listLegendLayers.Items.Count - 1))
                {
                    this.btnMoveUp.Enabled = true;
                }
            }
            else
            {
                this.btnMoveDown.Enabled = false;
                this.btnMoveUp.Enabled = false;
            }
        }

        private void listMapLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listMapLayers.SelectedIndex >= 0)
            {
                this.btnSelect.Enabled = true;
            }
            else
            {
                this.btnSelect.Enabled = false;
            }
        }

        private void method_0(ICompositeLayer icompositeLayer_0)
        {
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    this.method_0(layer as ICompositeLayer);
                }
                else
                {
                    this.listMapLayers.Items.Add(new ObjectWrap(layer));
                }
            }
        }

        private void method_1()
        {
            int num;
            for (num = 0; num < this.imap_0.LayerCount; num++)
            {
                ILayer layer = this.imap_0.get_Layer(num);
                if (layer is IGroupLayer)
                {
                    this.method_0(layer as ICompositeLayer);
                }
                else
                {
                    this.listMapLayers.Items.Add(new ObjectWrap(layer));
                }
            }
            for (num = 0; num < this.ilegend_0.ItemCount; num++)
            {
                this.listLegendLayers.Items.Add(new LegendItemWrap(this.ilegend_0.get_Item(num)));
            }
        }

        private ILegendItem method_2(ILayer ilayer_0)
        {
            return new HorizontalLegendItemClass { Layer = ilayer_0 };
        }

        private void spinEdit1_EditValueChanged(object sender, EventArgs e)
        {
        }

        public IMap FocusMap
        {
            set
            {
                this.imap_0 = value;
            }
        }

        public ILegend Legend
        {
            set
            {
                this.ilegend_0 = value;
            }
        }
    }
}

