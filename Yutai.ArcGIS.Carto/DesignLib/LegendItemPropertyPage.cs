using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class LegendItemPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private SimpleButton btnLegendItemsSelector;
        private SimpleButton btnMoveDown;
        private SimpleButton btnMoveUp;
        private SimpleButton btnSelect;
        private SimpleButton btnSelectAll;
        private SimpleButton btnUnSelect;
        private SimpleButton btnUnSelectAll;
        private CheckEdit checkEdit4;
        private CheckEdit chkAutoAdd;
        private CheckEdit chkAutoReorder;
        private CheckEdit chkAutoVisibility;
        private CheckEdit chkNewColumn;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private ILegend ilegend_0 = null;
        private IMapSurroundFrame imapSurroundFrame_0 = null;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;
        private Label label1;
        private Label label2;
        private Label label4;
        private ListBoxControl listLegendLayers;
        private ListBoxControl listMapLayers;
        private string string_0 = "项";
        private SpinEdit txtColumns;

        public event OnValueChangeEventHandler OnValueChange;

        public LegendItemPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                this.ilegend_0.ClearItems();
                for (int i = 0; i < this.listLegendLayers.ItemCount; i++)
                {
                    ILegendItem item = ((this.listLegendLayers.Items[i] as LegendItemObject).LegendItem as IClone).Clone() as ILegendItem;
                    this.ilegend_0.AddItem(item);
                }
                this.ilegend_0.AutoReorder = this.chkAutoReorder.Checked;
                this.ilegend_0.AutoAdd = this.chkAutoAdd.Checked;
                this.ilegend_0.AutoVisibility = this.chkAutoVisibility.Checked;
            }
        }

        private void btnLegendItemsSelector_Click(object sender, EventArgs e)
        {
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            this.method_2();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            this.method_2();
        }

        private void btnUnSelect_Click(object sender, EventArgs e)
        {
            this.method_2();
        }

        private void btnUnSelectAll_Click(object sender, EventArgs e)
        {
            this.method_2();
        }

        public void Cancel()
        {
        }

        private void chkAutoAdd_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.method_2();
            }
        }

        private void chkAutoAdd_Click(object sender, EventArgs e)
        {
        }

        private void chkAutoReorder_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.method_2();
            }
        }

        private void chkAutoReorder_Click(object sender, EventArgs e)
        {
        }

        private void chkAutoVisibility_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.method_2();
            }
        }

        private void chkAutoVisibility_Click(object sender, EventArgs e)
        {
        }

        private void chkNewColumn_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                (this.listLegendLayers.SelectedItem as LegendItemObject).LegendItem.NewColumn = this.chkNewColumn.Checked;
                this.method_2();
            }
        }

        private void chkNewColumn_Click(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LegendItemPropertyPage));
            this.groupBox1 = new GroupBox();
            this.btnLegendItemsSelector = new SimpleButton();
            this.chkNewColumn = new CheckEdit();
            this.txtColumns = new SpinEdit();
            this.label4 = new Label();
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
            this.groupBox2 = new GroupBox();
            this.checkEdit4 = new CheckEdit();
            this.chkAutoReorder = new CheckEdit();
            this.chkAutoAdd = new CheckEdit();
            this.chkAutoVisibility = new CheckEdit();
            this.groupBox1.SuspendLayout();
            this.chkNewColumn.Properties.BeginInit();
            this.txtColumns.Properties.BeginInit();
            ((ISupportInitialize) this.listLegendLayers).BeginInit();
            ((ISupportInitialize) this.listMapLayers).BeginInit();
            this.groupBox2.SuspendLayout();
            this.checkEdit4.Properties.BeginInit();
            this.chkAutoReorder.Properties.BeginInit();
            this.chkAutoAdd.Properties.BeginInit();
            this.chkAutoVisibility.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnLegendItemsSelector);
            this.groupBox1.Controls.Add(this.chkNewColumn);
            this.groupBox1.Controls.Add(this.txtColumns);
            this.groupBox1.Controls.Add(this.label4);
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
            this.groupBox1.Size = new Size(0x178, 200);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "指定图例项";
            this.btnLegendItemsSelector.Enabled = false;
            this.btnLegendItemsSelector.Location = new Point(0x120, 0x68);
            this.btnLegendItemsSelector.Name = "btnLegendItemsSelector";
            this.btnLegendItemsSelector.Size = new Size(0x38, 0x18);
            this.btnLegendItemsSelector.TabIndex = 0x11;
            this.btnLegendItemsSelector.Text = "样式...";
            this.btnLegendItemsSelector.Click += new EventHandler(this.btnLegendItemsSelector_Click);
            this.chkNewColumn.Location = new Point(0x120, 0x88);
            this.chkNewColumn.Name = "chkNewColumn";
            this.chkNewColumn.Properties.Caption = "放在新列";
            this.chkNewColumn.Properties.Enabled = false;
            this.chkNewColumn.Size = new Size(80, 0x13);
            this.chkNewColumn.TabIndex = 0x10;
            this.chkNewColumn.Click += new EventHandler(this.chkNewColumn_Click);
            this.chkNewColumn.CheckedChanged += new EventHandler(this.chkNewColumn_CheckedChanged);
            int[] bits = new int[4];
            this.txtColumns.EditValue = new decimal(bits);
            this.txtColumns.Location = new Point(0x138, 160);
            this.txtColumns.Name = "txtColumns";
            this.txtColumns.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtColumns.Properties.Enabled = false;
            this.txtColumns.Properties.UseCtrlIncrement = false;
            this.txtColumns.Size = new Size(0x30, 0x17);
            this.txtColumns.TabIndex = 15;
            this.txtColumns.EditValueChanged += new EventHandler(this.txtColumns_EditValueChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x120, 0xa8);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x11, 0x11);
            this.label4.TabIndex = 14;
            this.label4.Text = "列";
            this.listLegendLayers.ItemHeight = 15;
            this.listLegendLayers.Location = new Point(0xa8, 40);
            this.listLegendLayers.Name = "listLegendLayers";
            this.listLegendLayers.Size = new Size(0x70, 0x90);
            this.listLegendLayers.TabIndex = 13;
            this.listLegendLayers.SelectedIndexChanged += new EventHandler(this.listLegendLayers_SelectedIndexChanged);
            this.listMapLayers.ItemHeight = 15;
            this.listMapLayers.Location = new Point(8, 40);
            this.listMapLayers.Name = "listMapLayers";
            this.listMapLayers.Size = new Size(0x68, 0x90);
            this.listMapLayers.TabIndex = 12;
            this.listMapLayers.SelectedIndexChanged += new EventHandler(this.listMapLayers_SelectedIndexChanged);
            this.btnMoveDown.Enabled = false;
            this.btnMoveDown.Image = (Image) resources.GetObject("btnMoveDown.Image");
            this.btnMoveDown.Location = new Point(0x120, 0x48);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new Size(0x18, 0x18);
            this.btnMoveDown.TabIndex = 9;
            this.btnMoveDown.Visible = false;
            this.btnMoveDown.Click += new EventHandler(this.btnMoveDown_Click);
            this.btnMoveUp.Enabled = false;
            this.btnMoveUp.Image = (Image) resources.GetObject("btnMoveUp.Image");
            this.btnMoveUp.Location = new Point(0x120, 40);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new Size(0x18, 0x18);
            this.btnMoveUp.TabIndex = 8;
            this.btnMoveUp.Visible = false;
            this.btnMoveUp.Click += new EventHandler(this.btnMoveUp_Click);
            this.btnUnSelectAll.Location = new Point(120, 160);
            this.btnUnSelectAll.Name = "btnUnSelectAll";
            this.btnUnSelectAll.Size = new Size(0x20, 0x18);
            this.btnUnSelectAll.TabIndex = 7;
            this.btnUnSelectAll.Text = "<<";
            this.btnUnSelectAll.Click += new EventHandler(this.btnUnSelectAll_Click);
            this.btnUnSelect.Enabled = false;
            this.btnUnSelect.Location = new Point(120, 0x80);
            this.btnUnSelect.Name = "btnUnSelect";
            this.btnUnSelect.Size = new Size(0x20, 0x18);
            this.btnUnSelect.TabIndex = 6;
            this.btnUnSelect.Text = "<";
            this.btnUnSelect.Click += new EventHandler(this.btnUnSelect_Click);
            this.btnSelectAll.Location = new Point(120, 0x48);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new Size(0x20, 0x18);
            this.btnSelectAll.TabIndex = 5;
            this.btnSelectAll.Text = ">>";
            this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
            this.btnSelect.Enabled = false;
            this.btnSelect.Location = new Point(120, 40);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new Size(0x20, 0x18);
            this.btnSelect.TabIndex = 4;
            this.btnSelect.Text = ">";
            this.btnSelect.Click += new EventHandler(this.btnSelect_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0xb0, 0x12);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x2a, 0x11);
            this.label2.TabIndex = 2;
            this.label2.Text = "图例项";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x12);
            this.label1.Name = "label1";
            this.label1.Size = new Size(60, 0x11);
            this.label1.TabIndex = 1;
            this.label1.Text = "地图图层:";
            this.groupBox2.Controls.Add(this.checkEdit4);
            this.groupBox2.Controls.Add(this.chkAutoReorder);
            this.groupBox2.Controls.Add(this.chkAutoAdd);
            this.groupBox2.Controls.Add(this.chkAutoVisibility);
            this.groupBox2.Location = new Point(8, 0xd8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x178, 0x70);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "地图链接";
            this.checkEdit4.Location = new Point(8, 0x57);
            this.checkEdit4.Name = "checkEdit4";
            this.checkEdit4.Properties.Caption = "当设置了参考比例后，缩放符号";
            this.checkEdit4.Size = new Size(0xe8, 0x13);
            this.checkEdit4.TabIndex = 3;
            this.chkAutoReorder.Location = new Point(8, 0x42);
            this.chkAutoReorder.Name = "chkAutoReorder";
            this.chkAutoReorder.Properties.Caption = "当图层重新排序后，图层也要重新排序";
            this.chkAutoReorder.Size = new Size(0xe8, 0x13);
            this.chkAutoReorder.TabIndex = 2;
            this.chkAutoReorder.Click += new EventHandler(this.chkAutoReorder_Click);
            this.chkAutoReorder.CheckedChanged += new EventHandler(this.chkAutoReorder_CheckedChanged);
            this.chkAutoAdd.Location = new Point(8, 0x2d);
            this.chkAutoAdd.Name = "chkAutoAdd";
            this.chkAutoAdd.Properties.Caption = "当在地图中增加一个新图层时在图例表中新增一项";
            this.chkAutoAdd.Size = new Size(0x128, 0x13);
            this.chkAutoAdd.TabIndex = 1;
            this.chkAutoAdd.Click += new EventHandler(this.chkAutoAdd_Click);
            this.chkAutoAdd.CheckedChanged += new EventHandler(this.chkAutoAdd_CheckedChanged);
            this.chkAutoVisibility.Location = new Point(8, 0x18);
            this.chkAutoVisibility.Name = "chkAutoVisibility";
            this.chkAutoVisibility.Properties.Caption = "只显示地图上具有可见数据的图例项";
            this.chkAutoVisibility.Size = new Size(0xe8, 0x13);
            this.chkAutoVisibility.TabIndex = 0;
            this.chkAutoVisibility.Click += new EventHandler(this.chkAutoVisibility_Click);
            this.chkAutoVisibility.CheckedChanged += new EventHandler(this.chkAutoVisibility_CheckedChanged);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "LegendItemPropertyPage";
            base.Size = new Size(0x198, 0x158);
            base.Load += new EventHandler(this.LegendItemPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.chkNewColumn.Properties.EndInit();
            this.txtColumns.Properties.EndInit();
            ((ISupportInitialize) this.listLegendLayers).EndInit();
            ((ISupportInitialize) this.listMapLayers).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.checkEdit4.Properties.EndInit();
            this.chkAutoReorder.Properties.EndInit();
            this.chkAutoAdd.Properties.EndInit();
            this.chkAutoVisibility.Properties.EndInit();
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        private void LegendItemPropertyPage_Load(object sender, EventArgs e)
        {
            IMap map = this.ilegend_0.Map;
            for (int i = 0; i < map.LayerCount; i++)
            {
                ILayer layer = map.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    this.method_1(layer as ICompositeLayer);
                }
                else
                {
                    this.listMapLayers.Items.Add(new LayerObject(layer));
                }
            }
            this.method_0();
            this.bool_0 = true;
        }

        private void listLegendLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listLegendLayers.SelectedIndices.Count > 0)
            {
                this.btnLegendItemsSelector.Enabled = true;
                this.chkNewColumn.Enabled = true;
                this.txtColumns.Enabled = true;
                this.btnUnSelect.Enabled = true;
                if (this.listLegendLayers.SelectedIndex == 0)
                {
                    this.btnMoveUp.Enabled = false;
                }
                else
                {
                    this.btnMoveUp.Enabled = true;
                }
                if (this.listLegendLayers.SelectedIndex == (this.listLegendLayers.ItemCount - 1))
                {
                    this.btnMoveDown.Enabled = false;
                }
                else
                {
                    this.btnMoveDown.Enabled = true;
                }
                ILegendItem legendItem = (this.listLegendLayers.SelectedItem as LegendItemObject).LegendItem;
                this.bool_0 = false;
                this.txtColumns.Text = legendItem.Columns.ToString();
                this.chkNewColumn.Checked = legendItem.NewColumn;
                this.bool_0 = true;
            }
            else
            {
                this.btnLegendItemsSelector.Enabled = false;
                this.chkNewColumn.Enabled = false;
                this.txtColumns.Enabled = false;
                this.btnUnSelect.Enabled = false;
                this.btnMoveDown.Enabled = false;
                this.btnMoveUp.Enabled = false;
            }
        }

        private void listMapLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listMapLayers.SelectedIndex > 0)
            {
                this.btnSelect.Enabled = true;
            }
        }

        private void method_0()
        {
            ILegendItem item = null;
            for (int i = 0; i < this.ilegend_0.ItemCount; i++)
            {
                item = (this.ilegend_0.get_Item(i) as IClone).Clone() as ILegendItem;
                this.listLegendLayers.Items.Add(new LegendItemObject(item));
            }
            this.chkAutoReorder.Checked = this.ilegend_0.AutoReorder;
            this.chkAutoAdd.Checked = this.ilegend_0.AutoAdd;
            this.chkAutoVisibility.Checked = this.ilegend_0.AutoVisibility;
        }

        private void method_1(ICompositeLayer icompositeLayer_0)
        {
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    this.method_1(layer as ICompositeLayer);
                }
                else
                {
                    this.listMapLayers.Items.Add(new ObjectWrap(layer));
                }
            }
        }

        private void method_2()
        {
            this.bool_1 = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_0();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.imapSurroundFrame_0 = object_0 as IMapSurroundFrame;
            this.ilegend_0 = this.imapSurroundFrame_0.MapSurround as ILegend;
        }

        private void txtColumns_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    (this.listLegendLayers.SelectedItem as LegendItemObject).LegendItem.Columns = short.Parse(this.txtColumns.Text);
                    this.method_2();
                }
                catch
                {
                }
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_1;
            }
        }

        int IPropertyPage.Height
        {
            get
            {
                return base.Height;
            }
        }

        int IPropertyPage.Width
        {
            get
            {
                return base.Width;
            }
        }

        public string Title
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}

