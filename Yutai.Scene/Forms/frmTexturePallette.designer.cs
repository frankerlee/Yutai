using System;
using System.ComponentModel;

namespace Yutai.Plugins.Scene.Forms
{
	    partial class frmTexturePallette
    {
		protected override void Dispose(bool bool_2)
		{
			if (bool_2 && this.icontainer_0 != null)
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(bool_2);
		}

	
	private void InitializeComponent()
		{
			this.icontainer_0 = new Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(this.icontainer_0);
			this.Frame2 = new System.Windows.Forms.GroupBox();
			this.frRoofColor = new System.Windows.Forms.Panel();
			this.Image1 = new System.Windows.Forms.PictureBox();
			this.cmdOK = new System.Windows.Forms.Button();
			this.Frame1 = new System.Windows.Forms.GroupBox();
			this.cmdRemove = new System.Windows.Forms.Button();
			this.cmdTextureBrowse = new System.Windows.Forms.Button();
			this.tv1 = new System.Windows.Forms.TreeView();
			this.DlgCommonOpen = new System.Windows.Forms.OpenFileDialog();
			this.DlgCommonColor = new System.Windows.Forms.ColorDialog();
			this.mnuGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuRemoveGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuAddImage = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuRename = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuRoofColor = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuImage = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuRemoveImage = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.icontainer_0);
			this.Frame2.SuspendLayout();
			((ISupportInitialize)this.Image1).BeginInit();
			this.Frame1.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.Frame2.BackColor = System.Drawing.SystemColors.Control;
			this.Frame2.Controls.Add(this.frRoofColor);
			this.Frame2.Controls.Add(this.Image1);
			this.Frame2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Frame2.Location = new System.Drawing.Point(167, 8);
			this.Frame2.Name = "Frame2";
			this.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Frame2.Size = new System.Drawing.Size(157, 198);
			this.Frame2.TabIndex = 5;
			this.Frame2.TabStop = false;
			this.Frame2.Text = "预览";
			this.frRoofColor.BackColor = System.Drawing.SystemColors.Control;
			this.frRoofColor.Cursor = System.Windows.Forms.Cursors.Default;
			this.frRoofColor.ForeColor = System.Drawing.SystemColors.ControlText;
			this.frRoofColor.Location = new System.Drawing.Point(8, 16);
			this.frRoofColor.Name = "frRoofColor";
			this.frRoofColor.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.frRoofColor.Size = new System.Drawing.Size(130, 169);
			this.frRoofColor.TabIndex = 6;
			this.Image1.Cursor = System.Windows.Forms.Cursors.Default;
			this.Image1.Location = new System.Drawing.Point(8, 16);
			this.Image1.Name = "Image1";
			this.Image1.Size = new System.Drawing.Size(130, 169);
			this.Image1.TabIndex = 7;
			this.Image1.TabStop = false;
			this.cmdOK.BackColor = System.Drawing.SystemColors.Control;
			this.cmdOK.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdOK.Location = new System.Drawing.Point(280, 216);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdOK.Size = new System.Drawing.Size(46, 22);
			this.cmdOK.TabIndex = 4;
			this.cmdOK.Text = "确定";
			this.cmdOK.UseVisualStyleBackColor = false;
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.Frame1.BackColor = System.Drawing.SystemColors.Control;
			this.Frame1.Controls.Add(this.cmdRemove);
			this.Frame1.Controls.Add(this.cmdTextureBrowse);
			this.Frame1.Controls.Add(this.tv1);
			this.Frame1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Frame1.Location = new System.Drawing.Point(0, 8);
			this.Frame1.Name = "Frame1";
			this.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Frame1.Size = new System.Drawing.Size(161, 200);
			this.Frame1.TabIndex = 3;
			this.Frame1.TabStop = false;
			this.Frame1.Text = "纹理组";
			this.cmdRemove.BackColor = System.Drawing.SystemColors.Control;
			this.cmdRemove.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdRemove.Enabled = false;
			this.cmdRemove.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdRemove.Location = new System.Drawing.Point(8, 168);
			this.cmdRemove.Name = "cmdRemove";
			this.cmdRemove.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdRemove.Size = new System.Drawing.Size(56, 24);
			this.cmdRemove.TabIndex = 1;
			this.cmdRemove.Text = "删除";
			this.cmdRemove.UseVisualStyleBackColor = false;
			this.cmdRemove.Click += new EventHandler(this.cmdRemove_Click);
			this.cmdTextureBrowse.BackColor = System.Drawing.SystemColors.Control;
			this.cmdTextureBrowse.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdTextureBrowse.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdTextureBrowse.Location = new System.Drawing.Point(96, 168);
			this.cmdTextureBrowse.Name = "cmdTextureBrowse";
			this.cmdTextureBrowse.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdTextureBrowse.Size = new System.Drawing.Size(56, 24);
			this.cmdTextureBrowse.TabIndex = 2;
			this.cmdTextureBrowse.Text = "创建";
			this.cmdTextureBrowse.UseVisualStyleBackColor = false;
			this.cmdTextureBrowse.Click += new EventHandler(this.cmdTextureBrowse_Click);
			this.tv1.Location = new System.Drawing.Point(8, 16);
			this.tv1.Name = "tv1";
			this.tv1.Size = new System.Drawing.Size(144, 150);
			this.tv1.TabIndex = 0;
			this.tv1.DoubleClick += new EventHandler(this.tv1_DoubleClick);
			this.tv1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv1_AfterSelect);
			this.tv1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tv1_MouseDown);
			this.tv1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tv1_NodeMouseClick);
			this.tv1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tv1_KeyDown);
			this.mnuGroup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.mnuRemoveGroup,
				this.mnuAddImage,
				this.mnuRename,
				this.mnuRoofColor
			});
			this.mnuGroup.Name = "mnuGroup";
			this.mnuGroup.Size = new System.Drawing.Size(110, 22);
			this.mnuGroup.Text = "纹理组";
			this.mnuGroup.Visible = false;
			this.mnuRemoveGroup.Name = "mnuRemoveGroup";
			this.mnuRemoveGroup.Size = new System.Drawing.Size(137, 22);
			this.mnuRemoveGroup.Text = "删除(&R)";
			this.mnuRemoveGroup.Click += new EventHandler(this.mnuRemoveGroup_Click);
			this.mnuAddImage.Name = "mnuAddImage";
			this.mnuAddImage.Size = new System.Drawing.Size(137, 22);
			this.mnuAddImage.Text = "添加影像(&i)";
			this.mnuAddImage.Click += new EventHandler(this.mnuAddImage_Click);
			this.mnuRename.Name = "mnuRename";
			this.mnuRename.Size = new System.Drawing.Size(137, 22);
			this.mnuRename.Text = "重命名";
			this.mnuRename.Visible = false;
			this.mnuRename.Click += new EventHandler(this.mnuRename_Click);
			this.mnuRoofColor.Name = "mnuRoofColor";
			this.mnuRoofColor.Size = new System.Drawing.Size(137, 22);
			this.mnuRoofColor.Text = "屋顶颜色(&C)";
			this.mnuRoofColor.Click += new EventHandler(this.mnuRoofColor_Click);
			this.mnuImage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.mnuRemoveImage
			});
			this.mnuImage.Name = "mnuImage";
			this.mnuImage.Size = new System.Drawing.Size(110, 22);
			this.mnuImage.Text = "影像";
			this.mnuImage.Visible = false;
			this.mnuRemoveImage.Name = "mnuRemoveImage";
			this.mnuRemoveImage.Size = new System.Drawing.Size(98, 22);
			this.mnuRemoveImage.Text = "删除";
			this.mnuRemoveImage.Click += new EventHandler(this.mnuRemoveImage_Click);
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.mnuGroup,
				this.mnuImage
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(111, 48);
			base.AcceptButton = this.cmdOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			base.ClientSize = new System.Drawing.Size(378, 250);
			base.Controls.Add(this.Frame2);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.Frame1);
			this.Cursor = System.Windows.Forms.Cursors.Default;
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Location = new System.Drawing.Point(8, 27);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmTexturePallette";
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			base.ShowInTaskbar = false;
			this.Text = "3D建筑纹理板";
			base.Load += new EventHandler(this.frmTexturePallette_Load);
			this.Frame2.ResumeLayout(false);
			((ISupportInitialize)this.Image1).EndInit();
			this.Frame1.ResumeLayout(false);
			this.contextMenuStrip1.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	
		
		private IContainer icontainer_0;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        public System.Drawing.Image m_pTexture;

        public System.Windows.Forms.ToolTip ToolTip1;

        public System.Windows.Forms.Panel frRoofColor;

        public System.Windows.Forms.PictureBox Image1;

        public System.Windows.Forms.GroupBox Frame2;

        public System.Windows.Forms.Button cmdOK;

        public System.Windows.Forms.Button cmdRemove;

        public System.Windows.Forms.Button cmdTextureBrowse;

        public System.Windows.Forms.TreeView tv1;

        public System.Windows.Forms.GroupBox Frame1;

        public System.Windows.Forms.OpenFileDialog DlgCommonOpen;

        public System.Windows.Forms.ColorDialog DlgCommonColor;

        public System.Windows.Forms.ToolStripMenuItem mnuRemoveGroup;

        public System.Windows.Forms.ToolStripMenuItem mnuAddImage;

        public System.Windows.Forms.ToolStripMenuItem mnuRename;

        public System.Windows.Forms.ToolStripMenuItem mnuRoofColor;

        public System.Windows.Forms.ToolStripMenuItem mnuGroup;

        public System.Windows.Forms.ToolStripMenuItem mnuRemoveImage;

        public System.Windows.Forms.ToolStripMenuItem mnuImage;
    }
}