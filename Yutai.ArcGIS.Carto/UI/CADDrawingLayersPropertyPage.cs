using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;
using ItemCheckEventHandler = DevExpress.XtraEditors.Controls.ItemCheckEventHandler;

namespace Yutai.ArcGIS.Carto.UI
{
    public class CADDrawingLayersPropertyPage : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private SimpleButton btnAllUnVisible;
        private SimpleButton btnAllVisible;
        private SimpleButton btnRestore;
        private CheckedListBoxControl checkedListBoxControl1;
        private Container container_0 = null;
        private ICadDrawingLayers icadDrawingLayers_0 = null;
        private Label label1;

        public CADDrawingLayersPropertyPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                for (int i = 0; i < this.checkedListBoxControl1.Items.Count; i++)
                {
                    CheckedListBoxItem item = this.checkedListBoxControl1.Items[i];
                    this.icadDrawingLayers_0.set_DrawingLayerVisible(i, item.CheckState == CheckState.Checked);
                }
            }
            return true;
        }

        private void btnAllUnVisible_Click(object sender, EventArgs e)
        {
            this.bool_0 = true;
            for (int i = 0; i < this.checkedListBoxControl1.Items.Count; i++)
            {
                CheckedListBoxItem item = this.checkedListBoxControl1.Items[i];
                item.CheckState = CheckState.Unchecked;
            }
        }

        private void btnAllVisible_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBoxControl1.Items.Count; i++)
            {
                CheckedListBoxItem item = this.checkedListBoxControl1.Items[i];
                item.CheckState = CheckState.Checked;
            }
            this.bool_0 = true;
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            this.bool_0 = true;
            for (int i = 0; i < this.checkedListBoxControl1.Items.Count; i++)
            {
                if (this.icadDrawingLayers_0.get_OriginalDrawingLayerVisible(i))
                {
                    CheckedListBoxItem item = this.checkedListBoxControl1.Items[i];
                    item.CheckState = CheckState.Checked;
                }
            }
        }

        private void CADDrawingLayersPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void checkedListBoxControl1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.bool_0 = true;
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.checkedListBoxControl1 = new CheckedListBoxControl();
            this.btnAllVisible = new SimpleButton();
            this.btnAllUnVisible = new SimpleButton();
            this.btnRestore = new SimpleButton();
            ((ISupportInitialize) this.checkedListBoxControl1).BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(140, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "设置图层的可见或不可见";
            this.checkedListBoxControl1.ItemHeight = 0x11;
            this.checkedListBoxControl1.Location = new Point(8, 0x30);
            this.checkedListBoxControl1.Name = "checkedListBoxControl1";
            this.checkedListBoxControl1.Size = new Size(0xf8, 0x88);
            this.checkedListBoxControl1.TabIndex = 1;
            this.checkedListBoxControl1.ItemCheck += new ItemCheckEventHandler(this.checkedListBoxControl1_ItemCheck);
            this.btnAllVisible.Location = new Point(0x10, 200);
            this.btnAllVisible.Name = "btnAllVisible";
            this.btnAllVisible.Size = new Size(0x40, 0x18);
            this.btnAllVisible.TabIndex = 2;
            this.btnAllVisible.Text = "全部可见";
            this.btnAllVisible.Click += new EventHandler(this.btnAllVisible_Click);
            this.btnAllUnVisible.Location = new Point(0x58, 200);
            this.btnAllUnVisible.Name = "btnAllUnVisible";
            this.btnAllUnVisible.Size = new Size(0x48, 0x18);
            this.btnAllUnVisible.TabIndex = 3;
            this.btnAllUnVisible.Text = "全部不可见";
            this.btnAllUnVisible.Click += new EventHandler(this.btnAllUnVisible_Click);
            this.btnRestore.Location = new Point(0xa8, 200);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new Size(0x58, 0x18);
            this.btnRestore.TabIndex = 4;
            this.btnRestore.Text = "恢复初始设置";
            this.btnRestore.Click += new EventHandler(this.btnRestore_Click);
            base.Controls.Add(this.btnRestore);
            base.Controls.Add(this.btnAllUnVisible);
            base.Controls.Add(this.btnAllVisible);
            base.Controls.Add(this.checkedListBoxControl1);
            base.Controls.Add(this.label1);
            base.Name = "CADDrawingLayersPropertyPage";
            base.Size = new Size(320, 0x128);
            base.Load += new EventHandler(this.CADDrawingLayersPropertyPage_Load);
            ((ISupportInitialize) this.checkedListBoxControl1).EndInit();
            base.ResumeLayout(false);
        }

        private void method_0()
        {
            this.checkedListBoxControl1.Items.Clear();
            for (int i = 0; i < this.icadDrawingLayers_0.DrawingLayerCount; i++)
            {
                this.checkedListBoxControl1.Items.Add(this.icadDrawingLayers_0.get_DrawingLayerName(i), this.icadDrawingLayers_0.get_DrawingLayerVisible(i));
            }
        }

        public IBasicMap FocusMap
        {
            set
            {
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_0;
            }
        }

        public object SelectItem
        {
            set
            {
                this.icadDrawingLayers_0 = value as ICadDrawingLayers;
            }
        }
    }
}

