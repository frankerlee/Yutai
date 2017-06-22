using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class frmLabelRotateAngles
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
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
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new Point(152, 176);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(56, 24);
            this.simpleButton2.TabIndex = 19;
            this.simpleButton2.Text = "取消";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(80, 176);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 18;
            this.btnOK.Text = "确定";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(54, 17);
            this.label1.TabIndex = 23;
            this.label1.Text = "新建角度";
            this.txtAngle.EditValue = "";
            this.txtAngle.Location = new Point(72, 8);
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Size = new Size(64, 23);
            this.txtAngle.TabIndex = 22;
            this.groupBox1.Controls.Add(this.btnMoveDown);
            this.groupBox1.Controls.Add(this.btnMoveUp);
            this.groupBox1.Controls.Add(this.btnDeleteLayer);
            this.groupBox1.Controls.Add(this.listPointPlacementAngles);
            this.groupBox1.Location = new Point(16, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(200, 136);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "角度";
            this.btnMoveDown.Image = (System.Drawing.Image)resources.GetObject("btnMoveDown.Image");
            this.btnMoveDown.Location = new Point(136, 48);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new Size(24, 24);
            this.btnMoveDown.TabIndex = 24;
            this.btnMoveDown.Click += new EventHandler(this.btnMoveDown_Click);
            this.btnMoveUp.Image = (System.Drawing.Image)resources.GetObject("btnMoveUp.Image");
            this.btnMoveUp.Location = new Point(136, 16);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new Size(24, 24);
            this.btnMoveUp.TabIndex = 23;
            this.btnMoveUp.Click += new EventHandler(this.btnMoveUp_Click);
            this.btnDeleteLayer.Image = (System.Drawing.Image)resources.GetObject("btnDeleteLayer.Image");
            this.btnDeleteLayer.Location = new Point(136, 80);
            this.btnDeleteLayer.Name = "btnDeleteLayer";
            this.btnDeleteLayer.Size = new Size(24, 24);
            this.btnDeleteLayer.TabIndex = 22;
            this.btnDeleteLayer.Click += new EventHandler(this.btnDeleteLayer_Click);
            this.listPointPlacementAngles.ItemHeight = 12;
            this.listPointPlacementAngles.Location = new Point(8, 16);
            this.listPointPlacementAngles.Name = "listPointPlacementAngles";
            this.listPointPlacementAngles.Size = new Size(120, 88);
            this.listPointPlacementAngles.TabIndex = 0;
            this.btnAddAngle.Location = new Point(144, 8);
            this.btnAddAngle.Name = "btnAddAngle";
            this.btnAddAngle.Size = new Size(72, 24);
            this.btnAddAngle.TabIndex = 20;
            this.btnAddAngle.Text = "添加角度";
            this.btnAddAngle.Click += new EventHandler(this.btnAddAngle_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(232, 213);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.txtAngle);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.btnAddAngle);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.btnOK);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmLabelRotateAngles";
            this.Text = "点放置角度";
            base.Load += new EventHandler(this.frmLabelRotateAngles_Load);
            this.txtAngle.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnAddAngle;
        private SimpleButton btnDeleteLayer;
        private SimpleButton btnMoveDown;
        private SimpleButton btnMoveUp;
        private SimpleButton btnOK;
        private GroupBox groupBox1;
        private Label label1;
        private ListBox listPointPlacementAngles;
        private SimpleButton simpleButton2;
        private TextEdit txtAngle;
    }
}