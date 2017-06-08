using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class frmLabelRotateAngles : Form
    {
        private SimpleButton btnAddAngle;
        private SimpleButton btnDeleteLayer;
        private SimpleButton btnMoveDown;
        private SimpleButton btnMoveUp;
        private SimpleButton btnOK;
        private Container container_0 = null;
        private double[] double_0 = null;
        private GroupBox groupBox1;
        private Label label1;
        private ListBox listPointPlacementAngles;
        private SimpleButton simpleButton2;
        private TextEdit txtAngle;

        public frmLabelRotateAngles()
        {
            this.InitializeComponent();
        }

        private void btnAddAngle_Click(object sender, EventArgs e)
        {
            try
            {
                double item = double.Parse(this.txtAngle.Text);
                this.listPointPlacementAngles.Items.Add(item);
            }
            catch
            {
            }
        }

        private void btnDeleteLayer_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.listPointPlacementAngles.SelectedIndex;
            if (selectedIndex != -1)
            {
                this.listPointPlacementAngles.Items.RemoveAt(selectedIndex);
            }
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.listPointPlacementAngles.SelectedIndex;
            if ((selectedIndex > -1) && (selectedIndex < (this.listPointPlacementAngles.Items.Count - 1)))
            {
                object item = this.listPointPlacementAngles.Items[selectedIndex];
                this.listPointPlacementAngles.Items.RemoveAt(selectedIndex);
                if (this.listPointPlacementAngles.Items.Count == selectedIndex)
                {
                    this.listPointPlacementAngles.Items.Add(item);
                }
                else
                {
                    this.listPointPlacementAngles.Items.Insert(selectedIndex + 1, item);
                }
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.listPointPlacementAngles.SelectedIndex;
            if (selectedIndex > 0)
            {
                object item = this.listPointPlacementAngles.Items[selectedIndex];
                this.listPointPlacementAngles.Items.RemoveAt(selectedIndex);
                this.listPointPlacementAngles.Items.Insert(selectedIndex - 1, item);
            }
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmLabelRotateAngles_Load(object sender, EventArgs e)
        {
            if (this.double_0 != null)
            {
                this.listPointPlacementAngles.Items.Clear();
                for (int i = 0; i < this.double_0.Length; i++)
                {
                    this.listPointPlacementAngles.Items.Add(this.double_0[i]);
                }
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLabelRotateAngles));
            this.simpleButton2 = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.label1 = new Label();
            this.txtAngle = new TextEdit();
            this.groupBox1 = new GroupBox();
            this.btnMoveDown = new SimpleButton();
            this.btnMoveUp = new SimpleButton();
            this.btnDeleteLayer = new SimpleButton();
            this.listPointPlacementAngles = new ListBox();
            this.btnAddAngle = new SimpleButton();
            this.txtAngle.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.simpleButton2.DialogResult = DialogResult.Cancel;
            this.simpleButton2.Location = new Point(0x98, 0xb0);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(0x38, 0x18);
            this.simpleButton2.TabIndex = 0x13;
            this.simpleButton2.Text = "取消";
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(80, 0xb0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 0x12;
            this.btnOK.Text = "确定";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x36, 0x11);
            this.label1.TabIndex = 0x17;
            this.label1.Text = "新建角度";
            this.txtAngle.EditValue = "";
            this.txtAngle.Location = new Point(0x48, 8);
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Size = new Size(0x40, 0x17);
            this.txtAngle.TabIndex = 0x16;
            this.groupBox1.Controls.Add(this.btnMoveDown);
            this.groupBox1.Controls.Add(this.btnMoveUp);
            this.groupBox1.Controls.Add(this.btnDeleteLayer);
            this.groupBox1.Controls.Add(this.listPointPlacementAngles);
            this.groupBox1.Location = new Point(0x10, 0x20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(200, 0x88);
            this.groupBox1.TabIndex = 0x15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "角度";
            this.btnMoveDown.Image = (System.Drawing.Image)resources.GetObject("btnMoveDown.Image");
            this.btnMoveDown.Location = new Point(0x88, 0x30);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new Size(0x18, 0x18);
            this.btnMoveDown.TabIndex = 0x18;
            this.btnMoveDown.Click += new EventHandler(this.btnMoveDown_Click);
            this.btnMoveUp.Image = (System.Drawing.Image)resources.GetObject("btnMoveUp.Image");
            this.btnMoveUp.Location = new Point(0x88, 0x10);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new Size(0x18, 0x18);
            this.btnMoveUp.TabIndex = 0x17;
            this.btnMoveUp.Click += new EventHandler(this.btnMoveUp_Click);
            this.btnDeleteLayer.Image = (System.Drawing.Image)resources.GetObject("btnDeleteLayer.Image");
            this.btnDeleteLayer.Location = new Point(0x88, 80);
            this.btnDeleteLayer.Name = "btnDeleteLayer";
            this.btnDeleteLayer.Size = new Size(0x18, 0x18);
            this.btnDeleteLayer.TabIndex = 0x16;
            this.btnDeleteLayer.Click += new EventHandler(this.btnDeleteLayer_Click);
            this.listPointPlacementAngles.ItemHeight = 12;
            this.listPointPlacementAngles.Location = new Point(8, 0x10);
            this.listPointPlacementAngles.Name = "listPointPlacementAngles";
            this.listPointPlacementAngles.Size = new Size(120, 0x58);
            this.listPointPlacementAngles.TabIndex = 0;
            this.btnAddAngle.Location = new Point(0x90, 8);
            this.btnAddAngle.Name = "btnAddAngle";
            this.btnAddAngle.Size = new Size(0x48, 0x18);
            this.btnAddAngle.TabIndex = 20;
            this.btnAddAngle.Text = "添加角度";
            this.btnAddAngle.Click += new EventHandler(this.btnAddAngle_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0xe8, 0xd5);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.txtAngle);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.btnAddAngle);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.btnOK);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmLabelRotateAngles";
            this.Text = "点放置角度";
            base.Load += new EventHandler(this.frmLabelRotateAngles_Load);
            this.txtAngle.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public object Angles
        {
            get
            {
                this.double_0 = new double[this.listPointPlacementAngles.Items.Count];
                for (int i = 0; i < this.listPointPlacementAngles.Items.Count; i++)
                {
                    this.double_0[i] = (double) this.listPointPlacementAngles.Items[i];
                }
                return this.double_0;
            }
            set
            {
                this.double_0 = value as double[];
            }
        }
    }
}

