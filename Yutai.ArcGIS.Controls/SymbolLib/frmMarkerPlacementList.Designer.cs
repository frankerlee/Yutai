using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    partial class frmMarkerPlacementList
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

       
        private void InitializeComponent()
        {
            this.components = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMarkerPlacementList));
            TreeNode node = new TreeNode("点", 1, 1);
            TreeNode node2 = new TreeNode("线", 1, 1);
            TreeNode node3 = new TreeNode("面", 1, 1);
            this.imageList1 = new ImageList(this.components);
            this.treeView1 = new TreeView();
            this.btnOK = new Button();
            this.button1 = new Button();
            base.SuspendLayout();
            this.imageList1.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Bitmap1.bmp");
            this.imageList1.Images.SetKeyName(1, "Bitmap1.bmp");
            this.imageList1.Images.SetKeyName(2, "Bitmap6.bmp");
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 1);
            this.treeView1.Name = "treeView1";
            node.ImageIndex = 1;
            node.Name = "节点0";
            node.SelectedImageIndex = 1;
            node.Text = "点";
            node2.ImageIndex = 1;
            node2.Name = "线";
            node2.SelectedImageIndex = 1;
            node2.Text = "线";
            node3.ImageIndex = 1;
            node3.Name = "节点2";
            node3.SelectedImageIndex = 1;
            node3.Text = "面";
            this.treeView1.Nodes.AddRange(new TreeNode[] { node, node2, node3 });
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new Size(219, 234);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
            this.btnOK.Location = new System.Drawing.Point(50, 243);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(58, 30);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "添加";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(151, 243);
            this.button1.Name = "button1";
            this.button1.Size = new Size(58, 30);
            this.button1.TabIndex = 2;
            this.button1.Text = "取消";
            this.button1.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(221, 273);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.treeView1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmMarkerPlacementList";
            this.Text = "点放置位置";
            base.Load += new EventHandler(this.frmGeometricEffectList_Load);
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
        private Button btnOK;
        private Button button1;
        private ImageList imageList1;
        private TreeView treeView1;
    }
}