using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Carto.UI
{
    public class frmLayerCheck : Form
    {
        private Button btnClear;
        private Button btnClose;
        private Button btnSelectAll;
        private Button btnSwitchSelect;
        private CheckedListBox checkedListBox1;
        private IContainer icontainer_0 = null;
        [CompilerGenerated]
        private IMap imap_0;
        private Label label1;

        public frmLayerCheck()
        {
            this.InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                if (this.checkedListBox1.GetItemChecked(i))
                {
                    this.checkedListBox1.SetItemChecked(i, false);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            ISet set = new SetClass();
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                if (this.checkedListBox1.GetItemChecked(i))
                {
                    object obj2 = this.checkedListBox1.Items[i];
                    set.Add((obj2 as LayerObject).Layer);
                }
            }
            if (set.Count > 0)
            {
                (this.Map as IMapClipOptions).ClipFilter = set;
            }
            else
            {
                (this.Map as IMapClipOptions).ClipFilter = null;
            }
            base.DialogResult = DialogResult.OK;
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                if (!this.checkedListBox1.GetItemChecked(i))
                {
                    this.checkedListBox1.SetItemChecked(i, true);
                }
            }
        }

        private void btnSwitchSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                bool itemChecked = this.checkedListBox1.GetItemChecked(i);
                this.checkedListBox1.SetItemChecked(i, !itemChecked);
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

        private void frmLayerCheck_Load(object sender, EventArgs e)
        {
            ISet clipFilter = (this.Map as IMapClipOptions).ClipFilter;
            for (int i = 0; i < this.Map.LayerCount; i++)
            {
                ILayer unk = this.Map.get_Layer(i);
                bool isChecked = false;
                if (clipFilter != null)
                {
                    isChecked = clipFilter.Find(unk);
                }
                this.checkedListBox1.Items.Add(new LayerObject(unk), isChecked);
            }
        }

        private void InitializeComponent()
        {
            this.checkedListBox1 = new CheckedListBox();
            this.label1 = new Label();
            this.btnSwitchSelect = new Button();
            this.btnClose = new Button();
            this.btnClear = new Button();
            this.btnSelectAll = new Button();
            base.SuspendLayout();
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new Point(6, 0x2d);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new Size(0xc0, 180);
            this.checkedListBox1.TabIndex = 0;
            this.label1.Location = new Point(4, 3);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x10c, 0x27);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择从裁剪中排除的图层。这些图层在裁剪区外的要素也将显示。";
            this.btnSwitchSelect.Location = new Point(0xe0, 0x4b);
            this.btnSwitchSelect.Name = "btnSwitchSelect";
            this.btnSwitchSelect.Size = new Size(0x30, 0x18);
            this.btnSwitchSelect.TabIndex = 8;
            this.btnSwitchSelect.Text = "反选";
            this.btnSwitchSelect.Click += new EventHandler(this.btnSwitchSelect_Click);
            this.btnClose.Location = new Point(0xe0, 0xc6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x30, 0x18);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.btnClear.Location = new Point(0xe0, 0x69);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(0x30, 0x18);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "清空";
            this.btnClear.Click += new EventHandler(this.btnClear_Click);
            this.btnSelectAll.Location = new Point(0xe0, 0x2d);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new Size(0x30, 0x18);
            this.btnSelectAll.TabIndex = 5;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x11c, 0x106);
            base.Controls.Add(this.btnSwitchSelect);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.btnClear);
            base.Controls.Add(this.btnSelectAll);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.checkedListBox1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmLayerCheck";
            this.Text = "图层";
            base.Load += new EventHandler(this.frmLayerCheck_Load);
            base.ResumeLayout(false);
        }

        public IMap Map
        {
            [CompilerGenerated]
            get
            {
                return this.imap_0;
            }
            [CompilerGenerated]
            set
            {
                this.imap_0 = value;
            }
        }
    }
}

