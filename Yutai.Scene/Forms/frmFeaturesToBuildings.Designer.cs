using System;
using System.ComponentModel;

namespace Yutai.Plugins.Scene.Forms
{
	    partial class frmFeaturesToBuildings
    {
		protected override void Dispose(bool bool_0)
		{
			if (bool_0 && this.icontainer_0 != null)
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(bool_0);
		}

	
	private void InitializeComponent()
		{
			this.icontainer_0 = new Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFeaturesToBuildings));
			this.ToolTip1 = new System.Windows.Forms.ToolTip(this.icontainer_0);
			this.lstTextureGroups = new System.Windows.Forms.CheckedListBox();
			this.cmdTexturesBrowse = new System.Windows.Forms.Button();
			this.cmbBuildingLayer = new System.Windows.Forms.ComboBox();
			this.cmdLayerBrowse = new System.Windows.Forms.Button();
			this.chkSelFeatures = new System.Windows.Forms.CheckBox();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.DlgCommonOpen = new System.Windows.Forms.OpenFileDialog();
			this.Label4 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.ImageList1 = new System.Windows.Forms.ImageList(this.icontainer_0);
			this.mnuGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuRemoveGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuAddImage = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuRename = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuRoofColor = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuImage = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuRemoveImage = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuBuildingPopup = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuBuildingsDeleteSelected = new System.Windows.Forms.ToolStripMenuItem();
			this.sls4 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuBuildingSelection = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuCreateBuildingFromFootprint = new System.Windows.Forms.ToolStripMenuItem();
			this.sls3 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuBuildingsSelectAll = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuBuildingDeselectAll = new System.Windows.Forms.ToolStripMenuItem();
			this.slash = new System.Windows.Forms.ToolStripSeparator();
			this.mnuApplyTextureGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuRoofColorPopup = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuRoofColorPop = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.icontainer_0);
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cmbHeight = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.chkConst = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtDigitizeHeight = new System.Windows.Forms.TextBox();
			this.contextMenuStrip1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.lstTextureGroups.BackColor = System.Drawing.SystemColors.Window;
			this.lstTextureGroups.Cursor = System.Windows.Forms.Cursors.Default;
			this.lstTextureGroups.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lstTextureGroups.Location = new System.Drawing.Point(68, 300);
			this.lstTextureGroups.Name = "lstTextureGroups";
			this.lstTextureGroups.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lstTextureGroups.Size = new System.Drawing.Size(228, 36);
			this.lstTextureGroups.TabIndex = 11;
			this.lstTextureGroups.SelectedIndexChanged += new EventHandler(this.lstTextureGroups_SelectedIndexChanged);
			this.cmdTexturesBrowse.BackColor = System.Drawing.SystemColors.Control;
			this.cmdTexturesBrowse.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdTexturesBrowse.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdTexturesBrowse.Location = new System.Drawing.Point(306, 296);
			this.cmdTexturesBrowse.Name = "cmdTexturesBrowse";
			this.cmdTexturesBrowse.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdTexturesBrowse.Size = new System.Drawing.Size(26, 16);
			this.cmdTexturesBrowse.TabIndex = 10;
			this.cmdTexturesBrowse.Text = "...";
			this.cmdTexturesBrowse.UseVisualStyleBackColor = false;
			this.cmdTexturesBrowse.Click += new EventHandler(this.cmdTexturesBrowse_Click);
			this.cmbBuildingLayer.BackColor = System.Drawing.SystemColors.Window;
			this.cmbBuildingLayer.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmbBuildingLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbBuildingLayer.ForeColor = System.Drawing.SystemColors.WindowText;
			this.cmbBuildingLayer.Location = new System.Drawing.Point(83, 9);
			this.cmbBuildingLayer.Name = "cmbBuildingLayer";
			this.cmbBuildingLayer.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmbBuildingLayer.Size = new System.Drawing.Size(206, 20);
			this.cmbBuildingLayer.TabIndex = 7;
			this.cmbBuildingLayer.SelectedIndexChanged += new EventHandler(this.cmbBuildingLayer_SelectedIndexChanged);
			this.cmdLayerBrowse.BackColor = System.Drawing.SystemColors.Control;
			this.cmdLayerBrowse.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdLayerBrowse.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdLayerBrowse.Location = new System.Drawing.Point(299, 9);
			this.cmdLayerBrowse.Name = "cmdLayerBrowse";
			this.cmdLayerBrowse.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdLayerBrowse.Size = new System.Drawing.Size(24, 17);
			this.cmdLayerBrowse.TabIndex = 6;
			this.cmdLayerBrowse.Text = "...";
			this.cmdLayerBrowse.UseVisualStyleBackColor = false;
			this.cmdLayerBrowse.Click += new EventHandler(this.cmdLayerBrowse_Click);
			this.chkSelFeatures.BackColor = System.Drawing.SystemColors.Control;
			this.chkSelFeatures.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkSelFeatures.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkSelFeatures.Location = new System.Drawing.Point(83, 33);
			this.chkSelFeatures.Name = "chkSelFeatures";
			this.chkSelFeatures.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkSelFeatures.Size = new System.Drawing.Size(160, 17);
			this.chkSelFeatures.TabIndex = 5;
			this.chkSelFeatures.Text = "使用选中的要素";
			this.chkSelFeatures.UseVisualStyleBackColor = false;
			this.cmdOK.BackColor = System.Drawing.SystemColors.Control;
			this.cmdOK.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdOK.Location = new System.Drawing.Point(203, 167);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdOK.Size = new System.Drawing.Size(49, 25);
			this.cmdOK.TabIndex = 0;
			this.cmdOK.Text = "确定";
			this.cmdOK.UseVisualStyleBackColor = false;
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.cmdCancel.BackColor = System.Drawing.SystemColors.Control;
			this.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdCancel.Location = new System.Drawing.Point(269, 167);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdCancel.Size = new System.Drawing.Size(49, 25);
			this.cmdCancel.TabIndex = 2;
			this.cmdCancel.Text = "取消";
			this.cmdCancel.UseVisualStyleBackColor = false;
			this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
			this.Label4.AutoSize = true;
			this.Label4.BackColor = System.Drawing.SystemColors.Control;
			this.Label4.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label4.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label4.Location = new System.Drawing.Point(-4, 300);
			this.Label4.Name = "Label4";
			this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label4.Size = new System.Drawing.Size(65, 12);
			this.Label4.TabIndex = 12;
			this.Label4.Text = "活动纹理组";
			this.Label2.AutoSize = true;
			this.Label2.BackColor = System.Drawing.SystemColors.Control;
			this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label2.Location = new System.Drawing.Point(8, 9);
			this.Label2.Name = "Label2";
			this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label2.Size = new System.Drawing.Size(77, 12);
			this.Label2.TabIndex = 3;
			this.Label2.Text = "输入多边形：";
			this.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.ImageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.mnuGroup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.mnuRemoveGroup,
				this.mnuAddImage,
				this.mnuRename,
				this.mnuRoofColor
			});
			this.mnuGroup.Name = "mnuGroup";
			this.mnuGroup.Size = new System.Drawing.Size(160, 22);
			this.mnuGroup.Text = "组";
			this.mnuGroup.Visible = false;
			this.mnuRemoveGroup.Name = "mnuRemoveGroup";
			this.mnuRemoveGroup.Size = new System.Drawing.Size(137, 22);
			this.mnuRemoveGroup.Text = "删除(&R)";
			this.mnuAddImage.Name = "mnuAddImage";
			this.mnuAddImage.Size = new System.Drawing.Size(137, 22);
			this.mnuAddImage.Text = "添加影像(&i)";
			this.mnuRename.Name = "mnuRename";
			this.mnuRename.Size = new System.Drawing.Size(137, 22);
			this.mnuRename.Text = "重命名";
			this.mnuRoofColor.Name = "mnuRoofColor";
			this.mnuRoofColor.Size = new System.Drawing.Size(137, 22);
			this.mnuRoofColor.Text = "屋顶颜色(&C)";
			this.mnuRoofColor.Click += new EventHandler(this.mnuRoofColor_Click);
			this.mnuImage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.mnuRemoveImage
			});
			this.mnuImage.Name = "mnuImage";
			this.mnuImage.Size = new System.Drawing.Size(160, 22);
			this.mnuImage.Text = "影像";
			this.mnuImage.Visible = false;
			this.mnuRemoveImage.Name = "mnuRemoveImage";
			this.mnuRemoveImage.Size = new System.Drawing.Size(125, 22);
			this.mnuRemoveImage.Text = "重命名(&R)";
			this.mnuBuildingPopup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.mnuBuildingsDeleteSelected,
				this.sls4,
				this.mnuBuildingSelection,
				this.slash,
				this.mnuApplyTextureGroup
			});
			this.mnuBuildingPopup.Name = "mnuBuildingPopup";
			this.mnuBuildingPopup.Size = new System.Drawing.Size(160, 22);
			this.mnuBuildingPopup.Text = "mnuBuildingPopup";
			this.mnuBuildingPopup.Visible = false;
			this.mnuBuildingsDeleteSelected.Name = "mnuBuildingsDeleteSelected";
			this.mnuBuildingsDeleteSelected.Size = new System.Drawing.Size(112, 22);
			this.mnuBuildingsDeleteSelected.Text = "删除";
			this.mnuBuildingsDeleteSelected.Click += new EventHandler(this.mnuBuildingsDeleteSelected_Click);
			this.sls4.Name = "sls4";
			this.sls4.Size = new System.Drawing.Size(109, 6);
			this.mnuBuildingSelection.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.mnuCreateBuildingFromFootprint,
				this.sls3,
				this.mnuBuildingsSelectAll,
				this.mnuBuildingDeselectAll
			});
			this.mnuBuildingSelection.Name = "mnuBuildingSelection";
			this.mnuBuildingSelection.Size = new System.Drawing.Size(112, 22);
			this.mnuBuildingSelection.Text = "选择";
			this.mnuCreateBuildingFromFootprint.Enabled = false;
			this.mnuCreateBuildingFromFootprint.Name = "mnuCreateBuildingFromFootprint";
			this.mnuCreateBuildingFromFootprint.Size = new System.Drawing.Size(254, 22);
			this.mnuCreateBuildingFromFootprint.Text = "选中要素创建建筑";
			this.mnuCreateBuildingFromFootprint.Click += new EventHandler(this.mnuCreateBuildingFromFootprint_Click);
			this.sls3.Name = "sls3";
			this.sls3.Size = new System.Drawing.Size(251, 6);
			this.mnuBuildingsSelectAll.Name = "mnuBuildingsSelectAll";
			this.mnuBuildingsSelectAll.Size = new System.Drawing.Size(254, 22);
			this.mnuBuildingsSelectAll.Text = "从活动建筑层中选取所有建筑";
			this.mnuBuildingsSelectAll.Click += new EventHandler(this.mnuBuildingsSelectAll_Click);
			this.mnuBuildingDeselectAll.Name = "mnuBuildingDeselectAll";
			this.mnuBuildingDeselectAll.Size = new System.Drawing.Size(254, 22);
			this.mnuBuildingDeselectAll.Text = "从活动建筑层中取消选定所有建筑";
			this.mnuBuildingDeselectAll.Click += new EventHandler(this.mnuBuildingDeselectAll_Click);
			this.slash.Name = "slash";
			this.slash.Size = new System.Drawing.Size(109, 6);
			this.mnuApplyTextureGroup.Enabled = false;
			this.mnuApplyTextureGroup.Name = "mnuApplyTextureGroup";
			this.mnuApplyTextureGroup.Size = new System.Drawing.Size(112, 22);
			this.mnuApplyTextureGroup.Text = "属性(&P)";
			this.mnuApplyTextureGroup.Click += new EventHandler(this.mnuApplyTextureGroup_Click);
			this.mnuRoofColorPopup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.mnuRoofColorPop
			});
			this.mnuRoofColorPopup.Name = "mnuRoofColorPopup";
			this.mnuRoofColorPopup.Size = new System.Drawing.Size(160, 22);
			this.mnuRoofColorPopup.Text = "屋顶颜色";
			this.mnuRoofColorPopup.Visible = false;
			this.mnuRoofColorPop.Name = "mnuRoofColorPop";
			this.mnuRoofColorPop.Size = new System.Drawing.Size(122, 22);
			this.mnuRoofColorPop.Text = "屋顶颜色";
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.mnuGroup,
				this.mnuImage,
				this.mnuBuildingPopup,
				this.mnuRoofColorPopup
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(161, 92);
			this.groupBox1.Controls.Add(this.cmbHeight);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.chkConst);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtDigitizeHeight);
			this.groupBox1.Location = new System.Drawing.Point(12, 56);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(311, 105);
			this.groupBox1.TabIndex = 13;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "建筑物高度";
			this.cmbHeight.BackColor = System.Drawing.SystemColors.Window;
			this.cmbHeight.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmbHeight.Enabled = false;
			this.cmbHeight.ForeColor = System.Drawing.SystemColors.WindowText;
			this.cmbHeight.Location = new System.Drawing.Point(56, 68);
			this.cmbHeight.Name = "cmbHeight";
			this.cmbHeight.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmbHeight.Size = new System.Drawing.Size(221, 20);
			this.cmbHeight.TabIndex = 11;
			this.label5.AutoSize = true;
			this.label5.BackColor = System.Drawing.SystemColors.Control;
			this.label5.Cursor = System.Windows.Forms.Cursors.Default;
			this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label5.Location = new System.Drawing.Point(6, 68);
			this.label5.Name = "label5";
			this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.label5.Size = new System.Drawing.Size(29, 12);
			this.label5.TabIndex = 8;
			this.label5.Text = "字段";
			this.chkConst.AutoSize = true;
			this.chkConst.Checked = true;
			this.chkConst.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkConst.Location = new System.Drawing.Point(6, 21);
			this.chkConst.Name = "chkConst";
			this.chkConst.Size = new System.Drawing.Size(72, 16);
			this.chkConst.TabIndex = 7;
			this.chkConst.Text = "固定高度";
			this.chkConst.UseVisualStyleBackColor = true;
			this.chkConst.CheckedChanged += new EventHandler(this.chkConst_CheckedChanged);
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.SystemColors.Control;
			this.label1.Cursor = System.Windows.Forms.Cursors.Default;
			this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label1.Location = new System.Drawing.Point(6, 46);
			this.label1.Name = "label1";
			this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 6;
			this.label1.Text = "高度值";
			this.txtDigitizeHeight.AcceptsReturn = true;
			this.txtDigitizeHeight.BackColor = System.Drawing.SystemColors.Window;
			this.txtDigitizeHeight.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtDigitizeHeight.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtDigitizeHeight.Location = new System.Drawing.Point(56, 43);
			this.txtDigitizeHeight.MaxLength = 0;
			this.txtDigitizeHeight.Name = "txtDigitizeHeight";
			this.txtDigitizeHeight.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtDigitizeHeight.Size = new System.Drawing.Size(221, 21);
			this.txtDigitizeHeight.TabIndex = 5;
			base.AcceptButton = this.cmdOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			base.CancelButton = this.cmdCancel;
			base.ClientSize = new System.Drawing.Size(339, 199);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.cmbBuildingLayer);
			base.Controls.Add(this.cmdLayerBrowse);
			base.Controls.Add(this.chkSelFeatures);
			base.Controls.Add(this.lstTextureGroups);
			base.Controls.Add(this.cmdTexturesBrowse);
			base.Controls.Add(this.Label2);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.Label4);
			base.Controls.Add(this.cmdCancel);
			this.Cursor = System.Windows.Forms.Cursors.Default;
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		
			base.Location = new System.Drawing.Point(8, 27);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmFeaturesToBuildings";
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Text = "要素创建3D建筑";
			base.Load += new EventHandler(this.frmFeaturesToBuildings_Load);
			this.contextMenuStrip1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	
		private IContainer icontainer_0;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox cmbHeight;
		private System.Windows.Forms.CheckBox chkConst;
        public System.Windows.Forms.ToolTip ToolTip1;
        public System.Windows.Forms.CheckedListBox lstTextureGroups;
        public System.Windows.Forms.Button cmdTexturesBrowse;
        public System.Windows.Forms.ComboBox cmbBuildingLayer;
        public System.Windows.Forms.Button cmdLayerBrowse;
        public System.Windows.Forms.CheckBox chkSelFeatures;
        public System.Windows.Forms.Button cmdOK;
        public System.Windows.Forms.Button cmdCancel;
        public System.Windows.Forms.OpenFileDialog DlgCommonOpen;
        public System.Windows.Forms.Label Label4;
        public System.Windows.Forms.Label Label2;
        public System.Windows.Forms.ImageList ImageList1;
        public System.Windows.Forms.ToolStripMenuItem mnuRemoveGroup;
        public System.Windows.Forms.ToolStripMenuItem mnuAddImage;
        public System.Windows.Forms.ToolStripMenuItem mnuRename;
        public System.Windows.Forms.ToolStripMenuItem mnuRoofColor;
        public System.Windows.Forms.ToolStripMenuItem mnuGroup;
        public System.Windows.Forms.ToolStripMenuItem mnuRemoveImage;
        public System.Windows.Forms.ToolStripMenuItem mnuImage;
        public System.Windows.Forms.ToolStripMenuItem mnuBuildingsDeleteSelected;
        public System.Windows.Forms.ToolStripSeparator sls4;
        public System.Windows.Forms.ToolStripMenuItem mnuCreateBuildingFromFootprint;
        public System.Windows.Forms.ToolStripSeparator sls3;
        public System.Windows.Forms.ToolStripMenuItem mnuBuildingsSelectAll;
        public System.Windows.Forms.ToolStripMenuItem mnuBuildingDeselectAll;
        public System.Windows.Forms.ToolStripMenuItem mnuBuildingSelection;
        public System.Windows.Forms.ToolStripSeparator slash;
        public System.Windows.Forms.ToolStripMenuItem mnuApplyTextureGroup;
        public System.Windows.Forms.ToolStripMenuItem mnuBuildingPopup;
        public System.Windows.Forms.ToolStripMenuItem mnuRoofColorPop;
        public System.Windows.Forms.ToolStripMenuItem mnuRoofColorPopup;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtDigitizeHeight;
    }
}