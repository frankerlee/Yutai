using System;
using System.Collections;
using System.ComponentModel;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Plugins.Scene.Forms
{
	    partial class frmFlyByProps
    {
		protected override void Dispose(bool bool_3)
		{
			try
			{
				if (bool_3 && this.icontainer_0 != null)
				{
					this.icontainer_0.Dispose();
				}
				base.Dispose(bool_3);
			}
			catch
			{
			}
		}

	
	private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFlyByProps));
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdDeleteImages = new System.Windows.Forms.Button();
            this.cmdPlayImages = new System.Windows.Forms.Button();
            this.fraLoop = new System.Windows.Forms.GroupBox();
            this.optNo = new System.Windows.Forms.RadioButton();
            this.optReturn = new System.Windows.Forms.RadioButton();
            this.optContinuos = new System.Windows.Forms.RadioButton();
            this.fraDirection = new System.Windows.Forms.GroupBox();
            this.optForward = new System.Windows.Forms.RadioButton();
            this.optBackward = new System.Windows.Forms.RadioButton();
            this.btnSelectBackGround = new System.Windows.Forms.Button();
            this.cboOutputImgsType = new System.Windows.Forms.ComboBox();
            this.cmdBrowse = new System.Windows.Forms.Button();
            this.txtOutputImgsPath = new System.Windows.Forms.TextBox();
            this.txtOutputImgsPrefix = new System.Windows.Forms.TextBox();
            this.cboOutputImgsEvery = new System.Windows.Forms.ComboBox();
            this.lblFreeSpaceAfter = new System.Windows.Forms.Label();
            this.lblFileSize = new System.Windows.Forms.Label();
            this.lblFreeSpace = new System.Windows.Forms.Label();
            this.imgPreview = new System.Windows.Forms.PictureBox();
            this.lblOutputImgsFormat = new System.Windows.Forms.Label();
            this.lblOutputImgsPrefix = new System.Windows.Forms.Label();
            this.lblOutputImgsEvery = new System.Windows.Forms.Label();
            this.lblOutputImgsPath = new System.Windows.Forms.Label();
            this.udDelay = new System.Windows.Forms.PictureBox();
            this.chkBackground = new System.Windows.Forms.CheckBox();
            this.imgBackground = new System.Windows.Forms.PictureBox();
            this.lblFrameDelay = new System.Windows.Forms.Label();
            this.chkStatic = new System.Windows.Forms.CheckBox();
            this.fraStatic = new System.Windows.Forms.GroupBox();
            this.optStaticTarget = new System.Windows.Forms.RadioButton();
            this.optStaticObserver = new System.Windows.Forms.RadioButton();
            this.cboPointStep = new System.Windows.Forms.ComboBox();
            this.cboRollFactor = new System.Windows.Forms.ComboBox();
            this.lblNoInclination = new System.Windows.Forms.Label();
            this.lblPointStep = new System.Windows.Forms.Label();
            this.lblRollFactor = new System.Windows.Forms.Label();
            this.lblRollAngleR = new System.Windows.Forms.Label();
            this.lblInclinationR = new System.Windows.Forms.Label();
            this.lblAzimuthR = new System.Windows.Forms.Label();
            this.lblRollAngle = new System.Windows.Forms.Label();
            this.lblInclination = new System.Windows.Forms.Label();
            this.lblAzimuth = new System.Windows.Forms.Label();
            this.tmrAnimation = new System.Windows.Forms.Timer(this.components);
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.fraPath = new System.Windows.Forms.TabPage();
            this.sldTarOffset = new System.Windows.Forms.TrackBar();
            this.cboOffsetType = new System.Windows.Forms.ComboBox();
            this.chkOffset = new System.Windows.Forms.CheckBox();
            this.chkSmooth = new System.Windows.Forms.CheckBox();
            this.chkShowPath = new System.Windows.Forms.CheckBox();
            this.lblTarOffset = new System.Windows.Forms.Label();
            this.lblMaxOffset = new System.Windows.Forms.Label();
            this.cboMaxOffset = new System.Windows.Forms.ComboBox();
            this.lblPntCount = new System.Windows.Forms.Label();
            this.lstPoints = new System.Windows.Forms.ListBox();
            this.lblStepSize = new System.Windows.Forms.Label();
            this.cboStepSize = new System.Windows.Forms.ComboBox();
            this.fraAnimation = new System.Windows.Forms.TabPage();
            this.flstTextures = new System.Windows.Forms.ListBox();
            this.sldFrameDelay = new System.Windows.Forms.TrackBar();
            this.fraCamera = new System.Windows.Forms.TabPage();
            this.sldInclination = new System.Windows.Forms.TrackBar();
            this.fraOutput = new System.Windows.Forms.TabPage();
            this.flstOutputImgs = new System.Windows.Forms.ListBox();
            this.cmdAnim = new System.Windows.Forms.Button();
            this.cmdDone = new System.Windows.Forms.Button();
            this.fraLoop.SuspendLayout();
            this.fraDirection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgBackground)).BeginInit();
            this.fraStatic.SuspendLayout();
            this.TabControl1.SuspendLayout();
            this.fraPath.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sldTarOffset)).BeginInit();
            this.fraAnimation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sldFrameDelay)).BeginInit();
            this.fraCamera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sldInclination)).BeginInit();
            this.fraOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdDeleteImages
            // 
            this.cmdDeleteImages.BackColor = System.Drawing.SystemColors.Control;
            this.cmdDeleteImages.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdDeleteImages.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdDeleteImages.Location = new System.Drawing.Point(263, 175);
            this.cmdDeleteImages.Name = "cmdDeleteImages";
            this.cmdDeleteImages.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdDeleteImages.Size = new System.Drawing.Size(28, 25);
            this.cmdDeleteImages.TabIndex = 57;
            this.cmdDeleteImages.Text = "D";
            this.ToolTip1.SetToolTip(this.cmdDeleteImages, "Delete images from disk.");
            this.cmdDeleteImages.UseVisualStyleBackColor = false;
            this.cmdDeleteImages.Visible = false;
            this.cmdDeleteImages.Click += new System.EventHandler(this.cmdDeleteImages_Click);
            // 
            // cmdPlayImages
            // 
            this.cmdPlayImages.BackColor = System.Drawing.SystemColors.Control;
            this.cmdPlayImages.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdPlayImages.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdPlayImages.Location = new System.Drawing.Point(264, 144);
            this.cmdPlayImages.Name = "cmdPlayImages";
            this.cmdPlayImages.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdPlayImages.Size = new System.Drawing.Size(27, 25);
            this.cmdPlayImages.TabIndex = 56;
            this.cmdPlayImages.Text = "P";
            this.ToolTip1.SetToolTip(this.cmdPlayImages, "Play animation from files.");
            this.cmdPlayImages.UseVisualStyleBackColor = false;
            this.cmdPlayImages.Visible = false;
            this.cmdPlayImages.Click += new System.EventHandler(this.cmdPlayImages_Click);
            // 
            // fraLoop
            // 
            this.fraLoop.BackColor = System.Drawing.SystemColors.Control;
            this.fraLoop.Controls.Add(this.optNo);
            this.fraLoop.Controls.Add(this.optReturn);
            this.fraLoop.Controls.Add(this.optContinuos);
            this.fraLoop.ForeColor = System.Drawing.SystemColors.ControlText;
            this.fraLoop.Location = new System.Drawing.Point(16, 61);
            this.fraLoop.Name = "fraLoop";
            this.fraLoop.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fraLoop.Size = new System.Drawing.Size(227, 44);
            this.fraLoop.TabIndex = 20;
            this.fraLoop.TabStop = false;
            this.fraLoop.Text = "循环  ";
            this.ToolTip1.SetToolTip(this.fraLoop, "Set Looping Option");
            // 
            // optNo
            // 
            this.optNo.BackColor = System.Drawing.SystemColors.Control;
            this.optNo.Cursor = System.Windows.Forms.Cursors.Default;
            this.optNo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.optNo.Location = new System.Drawing.Point(10, 17);
            this.optNo.Name = "optNo";
            this.optNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.optNo.Size = new System.Drawing.Size(58, 19);
            this.optNo.TabIndex = 23;
            this.optNo.TabStop = true;
            this.optNo.Text = "无";
            this.optNo.UseVisualStyleBackColor = false;
            this.optNo.CheckedChanged += new System.EventHandler(this.optNo_CheckedChanged);
            // 
            // optReturn
            // 
            this.optReturn.BackColor = System.Drawing.SystemColors.Control;
            this.optReturn.Cursor = System.Windows.Forms.Cursors.Default;
            this.optReturn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.optReturn.Location = new System.Drawing.Point(67, 17);
            this.optReturn.Name = "optReturn";
            this.optReturn.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.optReturn.Size = new System.Drawing.Size(59, 19);
            this.optReturn.TabIndex = 22;
            this.optReturn.TabStop = true;
            this.optReturn.Text = "一次";
            this.optReturn.UseVisualStyleBackColor = false;
            this.optReturn.CheckedChanged += new System.EventHandler(this.optReturn_CheckedChanged);
            // 
            // optContinuos
            // 
            this.optContinuos.BackColor = System.Drawing.SystemColors.Control;
            this.optContinuos.Cursor = System.Windows.Forms.Cursors.Default;
            this.optContinuos.ForeColor = System.Drawing.SystemColors.ControlText;
            this.optContinuos.Location = new System.Drawing.Point(130, 17);
            this.optContinuos.Name = "optContinuos";
            this.optContinuos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.optContinuos.Size = new System.Drawing.Size(87, 19);
            this.optContinuos.TabIndex = 21;
            this.optContinuos.TabStop = true;
            this.optContinuos.Text = "连续飞行";
            this.optContinuos.UseVisualStyleBackColor = false;
            this.optContinuos.CheckedChanged += new System.EventHandler(this.optContinuos_CheckedChanged);
            // 
            // fraDirection
            // 
            this.fraDirection.BackColor = System.Drawing.SystemColors.Control;
            this.fraDirection.Controls.Add(this.optForward);
            this.fraDirection.Controls.Add(this.optBackward);
            this.fraDirection.ForeColor = System.Drawing.SystemColors.ControlText;
            this.fraDirection.Location = new System.Drawing.Point(16, 13);
            this.fraDirection.Name = "fraDirection";
            this.fraDirection.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fraDirection.Size = new System.Drawing.Size(227, 44);
            this.fraDirection.TabIndex = 16;
            this.fraDirection.TabStop = false;
            this.fraDirection.Text = "方向  ";
            this.ToolTip1.SetToolTip(this.fraDirection, "Change FlyBy Direction");
            // 
            // optForward
            // 
            this.optForward.BackColor = System.Drawing.SystemColors.Control;
            this.optForward.Cursor = System.Windows.Forms.Cursors.Default;
            this.optForward.ForeColor = System.Drawing.SystemColors.ControlText;
            this.optForward.Location = new System.Drawing.Point(34, 17);
            this.optForward.Name = "optForward";
            this.optForward.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.optForward.Size = new System.Drawing.Size(73, 23);
            this.optForward.TabIndex = 18;
            this.optForward.TabStop = true;
            this.optForward.Text = "向前";
            this.optForward.UseVisualStyleBackColor = false;
            this.optForward.CheckedChanged += new System.EventHandler(this.optForward_CheckedChanged);
            // 
            // optBackward
            // 
            this.optBackward.BackColor = System.Drawing.SystemColors.Control;
            this.optBackward.Cursor = System.Windows.Forms.Cursors.Default;
            this.optBackward.ForeColor = System.Drawing.SystemColors.ControlText;
            this.optBackward.Location = new System.Drawing.Point(115, 17);
            this.optBackward.Name = "optBackward";
            this.optBackward.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.optBackward.Size = new System.Drawing.Size(88, 19);
            this.optBackward.TabIndex = 17;
            this.optBackward.TabStop = true;
            this.optBackward.Text = "向后";
            this.optBackward.UseVisualStyleBackColor = false;
            this.optBackward.CheckedChanged += new System.EventHandler(this.optBackward_CheckedChanged);
            // 
            // btnSelectBackGround
            // 
            this.btnSelectBackGround.Enabled = false;
            this.btnSelectBackGround.Location = new System.Drawing.Point(228, 205);
            this.btnSelectBackGround.Name = "btnSelectBackGround";
            this.btnSelectBackGround.Size = new System.Drawing.Size(52, 23);
            this.btnSelectBackGround.TabIndex = 61;
            this.btnSelectBackGround.Text = "添加";
            this.ToolTip1.SetToolTip(this.btnSelectBackGround, "添加背景图片");
            this.btnSelectBackGround.UseVisualStyleBackColor = true;
            this.btnSelectBackGround.Click += new System.EventHandler(this.btnSelectBackGround_Click);
            // 
            // cboOutputImgsType
            // 
            this.cboOutputImgsType.BackColor = System.Drawing.SystemColors.Window;
            this.cboOutputImgsType.Cursor = System.Windows.Forms.Cursors.Default;
            this.cboOutputImgsType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOutputImgsType.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboOutputImgsType.Location = new System.Drawing.Point(200, 56);
            this.cboOutputImgsType.Name = "cboOutputImgsType";
            this.cboOutputImgsType.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cboOutputImgsType.Size = new System.Drawing.Size(59, 20);
            this.cboOutputImgsType.TabIndex = 50;
            this.cboOutputImgsType.SelectedIndexChanged += new System.EventHandler(this.cboOutputImgsType_SelectedIndexChanged);
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.BackColor = System.Drawing.SystemColors.Control;
            this.cmdBrowse.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdBrowse.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdBrowse.Location = new System.Drawing.Point(264, 112);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdBrowse.Size = new System.Drawing.Size(27, 25);
            this.cmdBrowse.TabIndex = 48;
            this.cmdBrowse.Text = "...";
            this.cmdBrowse.UseVisualStyleBackColor = false;
            this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
            // 
            // txtOutputImgsPath
            // 
            this.txtOutputImgsPath.AcceptsReturn = true;
            this.txtOutputImgsPath.BackColor = System.Drawing.SystemColors.Window;
            this.txtOutputImgsPath.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtOutputImgsPath.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtOutputImgsPath.Location = new System.Drawing.Point(32, 120);
            this.txtOutputImgsPath.MaxLength = 0;
            this.txtOutputImgsPath.Name = "txtOutputImgsPath";
            this.txtOutputImgsPath.ReadOnly = true;
            this.txtOutputImgsPath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtOutputImgsPath.Size = new System.Drawing.Size(227, 21);
            this.txtOutputImgsPath.TabIndex = 47;
            this.txtOutputImgsPath.TextChanged += new System.EventHandler(this.txtOutputImgsPath_TextChanged);
            // 
            // txtOutputImgsPrefix
            // 
            this.txtOutputImgsPrefix.AcceptsReturn = true;
            this.txtOutputImgsPrefix.BackColor = System.Drawing.SystemColors.Window;
            this.txtOutputImgsPrefix.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtOutputImgsPrefix.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtOutputImgsPrefix.Location = new System.Drawing.Point(80, 56);
            this.txtOutputImgsPrefix.MaxLength = 10;
            this.txtOutputImgsPrefix.Name = "txtOutputImgsPrefix";
            this.txtOutputImgsPrefix.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtOutputImgsPrefix.Size = new System.Drawing.Size(68, 21);
            this.txtOutputImgsPrefix.TabIndex = 41;
            this.txtOutputImgsPrefix.TextChanged += new System.EventHandler(this.txtOutputImgsPrefix_TextChanged);
            // 
            // cboOutputImgsEvery
            // 
            this.cboOutputImgsEvery.BackColor = System.Drawing.SystemColors.Window;
            this.cboOutputImgsEvery.Cursor = System.Windows.Forms.Cursors.Default;
            this.cboOutputImgsEvery.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOutputImgsEvery.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboOutputImgsEvery.Location = new System.Drawing.Point(152, 16);
            this.cboOutputImgsEvery.Name = "cboOutputImgsEvery";
            this.cboOutputImgsEvery.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cboOutputImgsEvery.Size = new System.Drawing.Size(92, 20);
            this.cboOutputImgsEvery.TabIndex = 39;
            // 
            // lblFreeSpaceAfter
            // 
            this.lblFreeSpaceAfter.BackColor = System.Drawing.SystemColors.Control;
            this.lblFreeSpaceAfter.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblFreeSpaceAfter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFreeSpaceAfter.Location = new System.Drawing.Point(136, 328);
            this.lblFreeSpaceAfter.Name = "lblFreeSpaceAfter";
            this.lblFreeSpaceAfter.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblFreeSpaceAfter.Size = new System.Drawing.Size(131, 44);
            this.lblFreeSpaceAfter.TabIndex = 55;
            this.lblFreeSpaceAfter.Text = "当前循环后可用空间: 0 Kb.";
            this.lblFreeSpaceAfter.Visible = false;
            // 
            // lblFileSize
            // 
            this.lblFileSize.BackColor = System.Drawing.SystemColors.Control;
            this.lblFileSize.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblFileSize.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFileSize.Location = new System.Drawing.Point(136, 272);
            this.lblFileSize.Name = "lblFileSize";
            this.lblFileSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblFileSize.Size = new System.Drawing.Size(126, 14);
            this.lblFileSize.TabIndex = 54;
            this.lblFileSize.Text = "文件大小: 0 Kb.";
            this.lblFileSize.Visible = false;
            // 
            // lblFreeSpace
            // 
            this.lblFreeSpace.BackColor = System.Drawing.SystemColors.Control;
            this.lblFreeSpace.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblFreeSpace.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFreeSpace.Location = new System.Drawing.Point(136, 296);
            this.lblFreeSpace.Name = "lblFreeSpace";
            this.lblFreeSpace.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblFreeSpace.Size = new System.Drawing.Size(121, 27);
            this.lblFreeSpace.TabIndex = 53;
            this.lblFreeSpace.Text = "当前可用空间: 0 Kb.";
            this.lblFreeSpace.Visible = false;
            // 
            // imgPreview
            // 
            this.imgPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imgPreview.Cursor = System.Windows.Forms.Cursors.Default;
            this.imgPreview.Location = new System.Drawing.Point(13, 263);
            this.imgPreview.Name = "imgPreview";
            this.imgPreview.Size = new System.Drawing.Size(117, 100);
            this.imgPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgPreview.TabIndex = 58;
            this.imgPreview.TabStop = false;
            // 
            // lblOutputImgsFormat
            // 
            this.lblOutputImgsFormat.BackColor = System.Drawing.SystemColors.Control;
            this.lblOutputImgsFormat.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblOutputImgsFormat.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblOutputImgsFormat.Location = new System.Drawing.Point(152, 56);
            this.lblOutputImgsFormat.Name = "lblOutputImgsFormat";
            this.lblOutputImgsFormat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblOutputImgsFormat.Size = new System.Drawing.Size(45, 14);
            this.lblOutputImgsFormat.TabIndex = 44;
            this.lblOutputImgsFormat.Text = "格式：";
            // 
            // lblOutputImgsPrefix
            // 
            this.lblOutputImgsPrefix.BackColor = System.Drawing.SystemColors.Control;
            this.lblOutputImgsPrefix.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblOutputImgsPrefix.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblOutputImgsPrefix.Location = new System.Drawing.Point(40, 56);
            this.lblOutputImgsPrefix.Name = "lblOutputImgsPrefix";
            this.lblOutputImgsPrefix.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblOutputImgsPrefix.Size = new System.Drawing.Size(45, 14);
            this.lblOutputImgsPrefix.TabIndex = 42;
            this.lblOutputImgsPrefix.Text = "前缀";
            // 
            // lblOutputImgsEvery
            // 
            this.lblOutputImgsEvery.BackColor = System.Drawing.SystemColors.Control;
            this.lblOutputImgsEvery.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblOutputImgsEvery.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblOutputImgsEvery.Location = new System.Drawing.Point(32, 24);
            this.lblOutputImgsEvery.Name = "lblOutputImgsEvery";
            this.lblOutputImgsEvery.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblOutputImgsEvery.Size = new System.Drawing.Size(91, 14);
            this.lblOutputImgsEvery.TabIndex = 40;
            this.lblOutputImgsEvery.Text = "保存快照间隔";
            // 
            // lblOutputImgsPath
            // 
            this.lblOutputImgsPath.BackColor = System.Drawing.SystemColors.Control;
            this.lblOutputImgsPath.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblOutputImgsPath.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblOutputImgsPath.Location = new System.Drawing.Point(32, 96);
            this.lblOutputImgsPath.Name = "lblOutputImgsPath";
            this.lblOutputImgsPath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblOutputImgsPath.Size = new System.Drawing.Size(78, 14);
            this.lblOutputImgsPath.TabIndex = 38;
            this.lblOutputImgsPath.Text = "输出文件路径：:";
            // 
            // udDelay
            // 
            this.udDelay.BackColor = System.Drawing.SystemColors.Control;
            this.udDelay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.udDelay.Cursor = System.Windows.Forms.Cursors.Default;
            this.udDelay.ForeColor = System.Drawing.SystemColors.ControlText;
            this.udDelay.Location = new System.Drawing.Point(222, 141);
            this.udDelay.Name = "udDelay";
            this.udDelay.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.udDelay.Size = new System.Drawing.Size(40, 20);
            this.udDelay.TabIndex = 58;
            this.udDelay.TabStop = false;
            this.udDelay.Visible = false;
            // 
            // chkBackground
            // 
            this.chkBackground.BackColor = System.Drawing.SystemColors.Control;
            this.chkBackground.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkBackground.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkBackground.Location = new System.Drawing.Point(26, 181);
            this.chkBackground.Name = "chkBackground";
            this.chkBackground.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkBackground.Size = new System.Drawing.Size(84, 24);
            this.chkBackground.TabIndex = 51;
            this.chkBackground.Text = "显示背景";
            this.chkBackground.UseVisualStyleBackColor = false;
            this.chkBackground.CheckedChanged += new System.EventHandler(this.chkBackground_CheckedChanged);
            // 
            // imgBackground
            // 
            this.imgBackground.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imgBackground.Cursor = System.Windows.Forms.Cursors.Default;
            this.imgBackground.Location = new System.Drawing.Point(26, 263);
            this.imgBackground.Name = "imgBackground";
            this.imgBackground.Size = new System.Drawing.Size(193, 100);
            this.imgBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgBackground.TabIndex = 59;
            this.imgBackground.TabStop = false;
            // 
            // lblFrameDelay
            // 
            this.lblFrameDelay.BackColor = System.Drawing.SystemColors.Control;
            this.lblFrameDelay.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblFrameDelay.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFrameDelay.Location = new System.Drawing.Point(14, 117);
            this.lblFrameDelay.Name = "lblFrameDelay";
            this.lblFrameDelay.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblFrameDelay.Size = new System.Drawing.Size(155, 14);
            this.lblFrameDelay.TabIndex = 24;
            this.lblFrameDelay.Text = "帧延迟: 1.0秒";
            // 
            // chkStatic
            // 
            this.chkStatic.BackColor = System.Drawing.SystemColors.Control;
            this.chkStatic.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkStatic.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkStatic.Location = new System.Drawing.Point(18, 253);
            this.chkStatic.Name = "chkStatic";
            this.chkStatic.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkStatic.Size = new System.Drawing.Size(165, 19);
            this.chkStatic.TabIndex = 60;
            this.chkStatic.Text = " 固定观测点/目标";
            this.chkStatic.UseVisualStyleBackColor = false;
            this.chkStatic.CheckStateChanged += new System.EventHandler(this.chkStatic_CheckStateChanged);
            // 
            // fraStatic
            // 
            this.fraStatic.BackColor = System.Drawing.SystemColors.Control;
            this.fraStatic.Controls.Add(this.optStaticTarget);
            this.fraStatic.Controls.Add(this.optStaticObserver);
            this.fraStatic.ForeColor = System.Drawing.SystemColors.ControlText;
            this.fraStatic.Location = new System.Drawing.Point(16, 278);
            this.fraStatic.Name = "fraStatic";
            this.fraStatic.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fraStatic.Size = new System.Drawing.Size(188, 74);
            this.fraStatic.TabIndex = 59;
            this.fraStatic.TabStop = false;
            // 
            // optStaticTarget
            // 
            this.optStaticTarget.BackColor = System.Drawing.SystemColors.Control;
            this.optStaticTarget.Cursor = System.Windows.Forms.Cursors.Default;
            this.optStaticTarget.ForeColor = System.Drawing.SystemColors.ControlText;
            this.optStaticTarget.Location = new System.Drawing.Point(16, 42);
            this.optStaticTarget.Name = "optStaticTarget";
            this.optStaticTarget.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.optStaticTarget.Size = new System.Drawing.Size(83, 14);
            this.optStaticTarget.TabIndex = 62;
            this.optStaticTarget.TabStop = true;
            this.optStaticTarget.Text = "目标";
            this.optStaticTarget.UseVisualStyleBackColor = false;
            // 
            // optStaticObserver
            // 
            this.optStaticObserver.BackColor = System.Drawing.SystemColors.Control;
            this.optStaticObserver.Cursor = System.Windows.Forms.Cursors.Default;
            this.optStaticObserver.ForeColor = System.Drawing.SystemColors.ControlText;
            this.optStaticObserver.Location = new System.Drawing.Point(16, 20);
            this.optStaticObserver.Name = "optStaticObserver";
            this.optStaticObserver.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.optStaticObserver.Size = new System.Drawing.Size(83, 14);
            this.optStaticObserver.TabIndex = 61;
            this.optStaticObserver.TabStop = true;
            this.optStaticObserver.Text = "观测者";
            this.optStaticObserver.UseVisualStyleBackColor = false;
            // 
            // cboPointStep
            // 
            this.cboPointStep.BackColor = System.Drawing.SystemColors.Window;
            this.cboPointStep.Cursor = System.Windows.Forms.Cursors.Default;
            this.cboPointStep.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboPointStep.Location = new System.Drawing.Point(90, 219);
            this.cboPointStep.Name = "cboPointStep";
            this.cboPointStep.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cboPointStep.Size = new System.Drawing.Size(45, 20);
            this.cboPointStep.TabIndex = 43;
            this.cboPointStep.Text = "2";
            this.cboPointStep.SelectedIndexChanged += new System.EventHandler(this.cboPointStep_SelectedIndexChanged);
            // 
            // cboRollFactor
            // 
            this.cboRollFactor.BackColor = System.Drawing.SystemColors.Window;
            this.cboRollFactor.Cursor = System.Windows.Forms.Cursors.Default;
            this.cboRollFactor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboRollFactor.Location = new System.Drawing.Point(89, 69);
            this.cboRollFactor.Name = "cboRollFactor";
            this.cboRollFactor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cboRollFactor.Size = new System.Drawing.Size(88, 20);
            this.cboRollFactor.TabIndex = 36;
            this.cboRollFactor.Text = "<None>";
            // 
            // lblNoInclination
            // 
            this.lblNoInclination.BackColor = System.Drawing.SystemColors.Control;
            this.lblNoInclination.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblNoInclination.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblNoInclination.Location = new System.Drawing.Point(87, 184);
            this.lblNoInclination.Name = "lblNoInclination";
            this.lblNoInclination.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblNoInclination.Size = new System.Drawing.Size(157, 18);
            this.lblNoInclination.TabIndex = 65;
            this.lblNoInclination.Text = "(0 = 沿路径倾斜)";
            this.lblNoInclination.Visible = false;
            // 
            // lblPointStep
            // 
            this.lblPointStep.BackColor = System.Drawing.SystemColors.Control;
            this.lblPointStep.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblPointStep.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPointStep.Location = new System.Drawing.Point(16, 222);
            this.lblPointStep.Name = "lblPointStep";
            this.lblPointStep.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblPointStep.Size = new System.Drawing.Size(54, 19);
            this.lblPointStep.TabIndex = 45;
            this.lblPointStep.Text = "步长:";
            // 
            // lblRollFactor
            // 
            this.lblRollFactor.AutoSize = true;
            this.lblRollFactor.BackColor = System.Drawing.SystemColors.Control;
            this.lblRollFactor.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblRollFactor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRollFactor.Location = new System.Drawing.Point(16, 72);
            this.lblRollFactor.Name = "lblRollFactor";
            this.lblRollFactor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblRollFactor.Size = new System.Drawing.Size(59, 12);
            this.lblRollFactor.TabIndex = 37;
            this.lblRollFactor.Text = "翻滚因子:";
            // 
            // lblRollAngleR
            // 
            this.lblRollAngleR.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblRollAngleR.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblRollAngleR.ForeColor = System.Drawing.Color.Blue;
            this.lblRollAngleR.Location = new System.Drawing.Point(88, 39);
            this.lblRollAngleR.Name = "lblRollAngleR";
            this.lblRollAngleR.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblRollAngleR.Size = new System.Drawing.Size(117, 18);
            this.lblRollAngleR.TabIndex = 30;
            this.lblRollAngleR.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblInclinationR
            // 
            this.lblInclinationR.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblInclinationR.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblInclinationR.ForeColor = System.Drawing.Color.Blue;
            this.lblInclinationR.Location = new System.Drawing.Point(88, 104);
            this.lblInclinationR.Name = "lblInclinationR";
            this.lblInclinationR.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblInclinationR.Size = new System.Drawing.Size(117, 18);
            this.lblInclinationR.TabIndex = 29;
            this.lblInclinationR.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblAzimuthR
            // 
            this.lblAzimuthR.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblAzimuthR.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblAzimuthR.ForeColor = System.Drawing.Color.Blue;
            this.lblAzimuthR.Location = new System.Drawing.Point(88, 8);
            this.lblAzimuthR.Name = "lblAzimuthR";
            this.lblAzimuthR.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblAzimuthR.Size = new System.Drawing.Size(117, 18);
            this.lblAzimuthR.TabIndex = 28;
            this.lblAzimuthR.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblRollAngle
            // 
            this.lblRollAngle.BackColor = System.Drawing.SystemColors.Control;
            this.lblRollAngle.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblRollAngle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRollAngle.Location = new System.Drawing.Point(16, 39);
            this.lblRollAngle.Name = "lblRollAngle";
            this.lblRollAngle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblRollAngle.Size = new System.Drawing.Size(66, 18);
            this.lblRollAngle.TabIndex = 27;
            this.lblRollAngle.Text = "翻滚角度:";
            // 
            // lblInclination
            // 
            this.lblInclination.AutoSize = true;
            this.lblInclination.BackColor = System.Drawing.SystemColors.Control;
            this.lblInclination.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblInclination.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblInclination.Location = new System.Drawing.Point(16, 104);
            this.lblInclination.Name = "lblInclination";
            this.lblInclination.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblInclination.Size = new System.Drawing.Size(35, 12);
            this.lblInclination.TabIndex = 26;
            this.lblInclination.Text = "倾角:";
            // 
            // lblAzimuth
            // 
            this.lblAzimuth.BackColor = System.Drawing.SystemColors.Control;
            this.lblAzimuth.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblAzimuth.ForeColor = System.Drawing.Color.Black;
            this.lblAzimuth.Location = new System.Drawing.Point(16, 8);
            this.lblAzimuth.Name = "lblAzimuth";
            this.lblAzimuth.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblAzimuth.Size = new System.Drawing.Size(59, 14);
            this.lblAzimuth.TabIndex = 25;
            this.lblAzimuth.Text = "方位角";
            // 
            // tmrAnimation
            // 
            this.tmrAnimation.Interval = 1000;
            this.tmrAnimation.Tick += new System.EventHandler(this.tmrAnimation_Tick);
            // 
            // TabControl1
            // 
            this.TabControl1.Controls.Add(this.fraPath);
            this.TabControl1.Controls.Add(this.fraAnimation);
            this.TabControl1.Controls.Add(this.fraCamera);
            this.TabControl1.Controls.Add(this.fraOutput);
            this.TabControl1.Location = new System.Drawing.Point(8, 8);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(312, 400);
            this.TabControl1.TabIndex = 47;
            // 
            // fraPath
            // 
            this.fraPath.Controls.Add(this.sldTarOffset);
            this.fraPath.Controls.Add(this.cboOffsetType);
            this.fraPath.Controls.Add(this.chkOffset);
            this.fraPath.Controls.Add(this.chkSmooth);
            this.fraPath.Controls.Add(this.chkShowPath);
            this.fraPath.Controls.Add(this.lblTarOffset);
            this.fraPath.Controls.Add(this.lblMaxOffset);
            this.fraPath.Controls.Add(this.cboMaxOffset);
            this.fraPath.Controls.Add(this.lblPntCount);
            this.fraPath.Controls.Add(this.lstPoints);
            this.fraPath.Controls.Add(this.lblStepSize);
            this.fraPath.Controls.Add(this.cboStepSize);
            this.fraPath.Location = new System.Drawing.Point(4, 22);
            this.fraPath.Name = "fraPath";
            this.fraPath.Size = new System.Drawing.Size(304, 374);
            this.fraPath.TabIndex = 0;
            this.fraPath.Text = "路径";
            // 
            // sldTarOffset
            // 
            this.sldTarOffset.Location = new System.Drawing.Point(16, 104);
            this.sldTarOffset.Name = "sldTarOffset";
            this.sldTarOffset.Size = new System.Drawing.Size(136, 45);
            this.sldTarOffset.TabIndex = 65;
            this.sldTarOffset.MouseUp += new System.Windows.Forms.MouseEventHandler(this.sldTarOffset_MouseUp);
            // 
            // cboOffsetType
            // 
            this.cboOffsetType.BackColor = System.Drawing.SystemColors.Window;
            this.cboOffsetType.Cursor = System.Windows.Forms.Cursors.Default;
            this.cboOffsetType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOffsetType.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboOffsetType.ItemHeight = 12;
            this.cboOffsetType.Location = new System.Drawing.Point(72, 56);
            this.cboOffsetType.Name = "cboOffsetType";
            this.cboOffsetType.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cboOffsetType.Size = new System.Drawing.Size(169, 20);
            this.cboOffsetType.TabIndex = 64;
            this.cboOffsetType.SelectedIndexChanged += new System.EventHandler(this.cboOffsetType_SelectedIndexChanged);
            // 
            // chkOffset
            // 
            this.chkOffset.BackColor = System.Drawing.SystemColors.Control;
            this.chkOffset.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkOffset.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkOffset.Location = new System.Drawing.Point(8, 56);
            this.chkOffset.Name = "chkOffset";
            this.chkOffset.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkOffset.Size = new System.Drawing.Size(63, 16);
            this.chkOffset.TabIndex = 63;
            this.chkOffset.Text = "偏移";
            this.chkOffset.UseVisualStyleBackColor = false;
            this.chkOffset.CheckStateChanged += new System.EventHandler(this.chkOffset_CheckStateChanged);
            // 
            // chkSmooth
            // 
            this.chkSmooth.BackColor = System.Drawing.SystemColors.Control;
            this.chkSmooth.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkSmooth.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkSmooth.Location = new System.Drawing.Point(8, 32);
            this.chkSmooth.Name = "chkSmooth";
            this.chkSmooth.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkSmooth.Size = new System.Drawing.Size(93, 24);
            this.chkSmooth.TabIndex = 4;
            this.chkSmooth.Text = "平滑处理";
            this.chkSmooth.UseVisualStyleBackColor = false;
            this.chkSmooth.CheckStateChanged += new System.EventHandler(this.chkSmooth_CheckStateChanged);
            // 
            // chkShowPath
            // 
            this.chkShowPath.BackColor = System.Drawing.SystemColors.Control;
            this.chkShowPath.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkShowPath.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkShowPath.Location = new System.Drawing.Point(8, 8);
            this.chkShowPath.Name = "chkShowPath";
            this.chkShowPath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkShowPath.Size = new System.Drawing.Size(93, 24);
            this.chkShowPath.TabIndex = 3;
            this.chkShowPath.Text = "显示路径";
            this.chkShowPath.UseVisualStyleBackColor = false;
            this.chkShowPath.CheckedChanged += new System.EventHandler(this.chkShowPath_CheckedChanged);
            this.chkShowPath.CheckStateChanged += new System.EventHandler(this.chkShowPath_CheckStateChanged);
            // 
            // lblTarOffset
            // 
            this.lblTarOffset.BackColor = System.Drawing.SystemColors.Control;
            this.lblTarOffset.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblTarOffset.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTarOffset.Location = new System.Drawing.Point(32, 88);
            this.lblTarOffset.Name = "lblTarOffset";
            this.lblTarOffset.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblTarOffset.Size = new System.Drawing.Size(96, 14);
            this.lblTarOffset.TabIndex = 12;
            this.lblTarOffset.Text = "高度/偏移: 0.00";
            // 
            // lblMaxOffset
            // 
            this.lblMaxOffset.AutoSize = true;
            this.lblMaxOffset.BackColor = System.Drawing.SystemColors.Control;
            this.lblMaxOffset.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblMaxOffset.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMaxOffset.Location = new System.Drawing.Point(184, 88);
            this.lblMaxOffset.Name = "lblMaxOffset";
            this.lblMaxOffset.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblMaxOffset.Size = new System.Drawing.Size(35, 12);
            this.lblMaxOffset.TabIndex = 11;
            this.lblMaxOffset.Text = "最大:";
            // 
            // cboMaxOffset
            // 
            this.cboMaxOffset.BackColor = System.Drawing.SystemColors.Window;
            this.cboMaxOffset.Cursor = System.Windows.Forms.Cursors.Default;
            this.cboMaxOffset.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboMaxOffset.ItemHeight = 12;
            this.cboMaxOffset.Location = new System.Drawing.Point(184, 112);
            this.cboMaxOffset.Name = "cboMaxOffset";
            this.cboMaxOffset.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cboMaxOffset.Size = new System.Drawing.Size(96, 20);
            this.cboMaxOffset.TabIndex = 9;
            this.cboMaxOffset.SelectedIndexChanged += new System.EventHandler(this.cboMaxOffset_SelectedIndexChanged);
            this.cboMaxOffset.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cboMaxOffset_KeyUp);
            this.cboMaxOffset.Leave += new System.EventHandler(this.cboMaxOffset_Leave);
            // 
            // lblPntCount
            // 
            this.lblPntCount.BackColor = System.Drawing.SystemColors.Control;
            this.lblPntCount.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblPntCount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPntCount.Location = new System.Drawing.Point(16, 152);
            this.lblPntCount.Name = "lblPntCount";
            this.lblPntCount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblPntCount.Size = new System.Drawing.Size(126, 14);
            this.lblPntCount.TabIndex = 8;
            this.lblPntCount.Text = "点数:";
            // 
            // lstPoints
            // 
            this.lstPoints.BackColor = System.Drawing.SystemColors.Window;
            this.lstPoints.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstPoints.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lstPoints.ItemHeight = 12;
            this.lstPoints.Location = new System.Drawing.Point(16, 168);
            this.lstPoints.Name = "lstPoints";
            this.lstPoints.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lstPoints.Size = new System.Drawing.Size(270, 124);
            this.lstPoints.TabIndex = 6;
            this.lstPoints.SelectedIndexChanged += new System.EventHandler(this.lstPoints_SelectedIndexChanged);
            this.lstPoints.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstPoints_KeyDown);
            // 
            // lblStepSize
            // 
            this.lblStepSize.AutoSize = true;
            this.lblStepSize.BackColor = System.Drawing.SystemColors.Control;
            this.lblStepSize.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblStepSize.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblStepSize.Location = new System.Drawing.Point(152, 320);
            this.lblStepSize.Name = "lblStepSize";
            this.lblStepSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblStepSize.Size = new System.Drawing.Size(35, 12);
            this.lblStepSize.TabIndex = 7;
            this.lblStepSize.Text = "步长:";
            // 
            // cboStepSize
            // 
            this.cboStepSize.BackColor = System.Drawing.SystemColors.Window;
            this.cboStepSize.Cursor = System.Windows.Forms.Cursors.Default;
            this.cboStepSize.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboStepSize.ItemHeight = 12;
            this.cboStepSize.Location = new System.Drawing.Point(208, 312);
            this.cboStepSize.Name = "cboStepSize";
            this.cboStepSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cboStepSize.Size = new System.Drawing.Size(78, 20);
            this.cboStepSize.TabIndex = 5;
            this.cboStepSize.SelectedIndexChanged += new System.EventHandler(this.cboStepSize_SelectedIndexChanged);
            this.cboStepSize.TextChanged += new System.EventHandler(this.cboStepSize_TextChanged);
            // 
            // fraAnimation
            // 
            this.fraAnimation.Controls.Add(this.btnSelectBackGround);
            this.fraAnimation.Controls.Add(this.flstTextures);
            this.fraAnimation.Controls.Add(this.sldFrameDelay);
            this.fraAnimation.Controls.Add(this.fraLoop);
            this.fraAnimation.Controls.Add(this.fraDirection);
            this.fraAnimation.Controls.Add(this.lblFrameDelay);
            this.fraAnimation.Controls.Add(this.udDelay);
            this.fraAnimation.Controls.Add(this.imgBackground);
            this.fraAnimation.Controls.Add(this.chkBackground);
            this.fraAnimation.Location = new System.Drawing.Point(4, 22);
            this.fraAnimation.Name = "fraAnimation";
            this.fraAnimation.Size = new System.Drawing.Size(304, 374);
            this.fraAnimation.TabIndex = 2;
            this.fraAnimation.Text = "动画";
            // 
            // flstTextures
            // 
            this.flstTextures.Enabled = false;
            this.flstTextures.ItemHeight = 12;
            this.flstTextures.Location = new System.Drawing.Point(26, 205);
            this.flstTextures.Name = "flstTextures";
            this.flstTextures.Size = new System.Drawing.Size(193, 52);
            this.flstTextures.TabIndex = 60;
            this.flstTextures.SelectedIndexChanged += new System.EventHandler(this.flstTextures_SelectedIndexChanged);
            // 
            // sldFrameDelay
            // 
            this.sldFrameDelay.Location = new System.Drawing.Point(14, 141);
            this.sldFrameDelay.Name = "sldFrameDelay";
            this.sldFrameDelay.Size = new System.Drawing.Size(192, 45);
            this.sldFrameDelay.TabIndex = 59;
            this.sldFrameDelay.Scroll += new System.EventHandler(this.sldFrameDelay_Scroll);
            // 
            // fraCamera
            // 
            this.fraCamera.Controls.Add(this.sldInclination);
            this.fraCamera.Controls.Add(this.lblRollFactor);
            this.fraCamera.Controls.Add(this.lblRollAngleR);
            this.fraCamera.Controls.Add(this.lblInclinationR);
            this.fraCamera.Controls.Add(this.lblAzimuthR);
            this.fraCamera.Controls.Add(this.lblRollAngle);
            this.fraCamera.Controls.Add(this.lblInclination);
            this.fraCamera.Controls.Add(this.lblAzimuth);
            this.fraCamera.Controls.Add(this.cboRollFactor);
            this.fraCamera.Controls.Add(this.lblNoInclination);
            this.fraCamera.Controls.Add(this.lblPointStep);
            this.fraCamera.Controls.Add(this.chkStatic);
            this.fraCamera.Controls.Add(this.fraStatic);
            this.fraCamera.Controls.Add(this.cboPointStep);
            this.fraCamera.Location = new System.Drawing.Point(4, 22);
            this.fraCamera.Name = "fraCamera";
            this.fraCamera.Size = new System.Drawing.Size(304, 374);
            this.fraCamera.TabIndex = 1;
            this.fraCamera.Text = "相机";
            // 
            // sldInclination
            // 
            this.sldInclination.Location = new System.Drawing.Point(69, 136);
            this.sldInclination.Name = "sldInclination";
            this.sldInclination.Size = new System.Drawing.Size(160, 45);
            this.sldInclination.TabIndex = 38;
            this.sldInclination.Scroll += new System.EventHandler(this.sldInclination_Scroll);
            // 
            // fraOutput
            // 
            this.fraOutput.Controls.Add(this.flstOutputImgs);
            this.fraOutput.Controls.Add(this.lblOutputImgsEvery);
            this.fraOutput.Controls.Add(this.cboOutputImgsEvery);
            this.fraOutput.Controls.Add(this.cboOutputImgsType);
            this.fraOutput.Controls.Add(this.txtOutputImgsPrefix);
            this.fraOutput.Controls.Add(this.lblOutputImgsPrefix);
            this.fraOutput.Controls.Add(this.lblOutputImgsFormat);
            this.fraOutput.Controls.Add(this.lblOutputImgsPath);
            this.fraOutput.Controls.Add(this.cmdBrowse);
            this.fraOutput.Controls.Add(this.txtOutputImgsPath);
            this.fraOutput.Controls.Add(this.cmdDeleteImages);
            this.fraOutput.Controls.Add(this.cmdPlayImages);
            this.fraOutput.Controls.Add(this.imgPreview);
            this.fraOutput.Controls.Add(this.lblFreeSpaceAfter);
            this.fraOutput.Controls.Add(this.lblFileSize);
            this.fraOutput.Controls.Add(this.lblFreeSpace);
            this.fraOutput.Location = new System.Drawing.Point(4, 22);
            this.fraOutput.Name = "fraOutput";
            this.fraOutput.Size = new System.Drawing.Size(304, 374);
            this.fraOutput.TabIndex = 3;
            this.fraOutput.Text = "输出";
            // 
            // flstOutputImgs
            // 
            this.flstOutputImgs.ItemHeight = 12;
            this.flstOutputImgs.Location = new System.Drawing.Point(32, 147);
            this.flstOutputImgs.Name = "flstOutputImgs";
            this.flstOutputImgs.Size = new System.Drawing.Size(184, 88);
            this.flstOutputImgs.TabIndex = 58;
            this.flstOutputImgs.SelectedIndexChanged += new System.EventHandler(this.flstOutputImgs_SelectedIndexChanged);
            // 
            // cmdAnim
            // 
            this.cmdAnim.BackColor = System.Drawing.SystemColors.Control;
            this.cmdAnim.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdAnim.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdAnim.Location = new System.Drawing.Point(48, 416);
            this.cmdAnim.Name = "cmdAnim";
            this.cmdAnim.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdAnim.Size = new System.Drawing.Size(145, 36);
            this.cmdAnim.TabIndex = 49;
            this.cmdAnim.Text = "开始飞行";
            this.cmdAnim.UseVisualStyleBackColor = false;
            this.cmdAnim.Click += new System.EventHandler(this.cmdAnim_Click);
            // 
            // cmdDone
            // 
            this.cmdDone.BackColor = System.Drawing.SystemColors.Control;
            this.cmdDone.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdDone.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdDone.Location = new System.Drawing.Point(233, 418);
            this.cmdDone.Name = "cmdDone";
            this.cmdDone.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdDone.Size = new System.Drawing.Size(83, 32);
            this.cmdDone.TabIndex = 48;
            this.cmdDone.Text = "退出";
            this.cmdDone.UseVisualStyleBackColor = false;
            this.cmdDone.Click += new System.EventHandler(this.cmdDone_Click);
            // 
            // frmFlyByProps
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(330, 460);
            this.Controls.Add(this.cmdAnim);
            this.Controls.Add(this.cmdDone);
            this.Controls.Add(this.TabControl1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(3, 22);
            this.MaximizeBox = false;
            this.Name = "frmFlyByProps";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "飞行属性设置";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmFlyByProps_Closing);
            this.Closed += new System.EventHandler(this.frmFlyByProps_Closed);
            this.Load += new System.EventHandler(this.frmFlyByProps_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmFlyByProps_KeyDown);
            this.fraLoop.ResumeLayout(false);
            this.fraDirection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgBackground)).EndInit();
            this.fraStatic.ResumeLayout(false);
            this.TabControl1.ResumeLayout(false);
            this.fraPath.ResumeLayout(false);
            this.fraPath.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sldTarOffset)).EndInit();
            this.fraAnimation.ResumeLayout(false);
            this.fraAnimation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sldFrameDelay)).EndInit();
            this.fraCamera.ResumeLayout(false);
            this.fraCamera.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sldInclination)).EndInit();
            this.fraOutput.ResumeLayout(false);
            this.fraOutput.PerformLayout();
            this.ResumeLayout(false);

		}

	
		private IContainer icontainer_0;
		private System.Windows.Forms.Button btnSelectBackGround;
        public System.Windows.Forms.ToolTip ToolTip1;

        public System.Windows.Forms.Button cmdDeleteImages;

        public System.Windows.Forms.Button cmdPlayImages;

        public System.Windows.Forms.ComboBox cboOutputImgsType;

        public System.Windows.Forms.Button cmdBrowse;

        public System.Windows.Forms.TextBox txtOutputImgsPath;

        public System.Windows.Forms.TextBox txtOutputImgsPrefix;

        public System.Windows.Forms.ComboBox cboOutputImgsEvery;

        public System.Windows.Forms.Label lblFreeSpaceAfter;

        public System.Windows.Forms.Label lblFileSize;

        public System.Windows.Forms.Label lblFreeSpace;

        public System.Windows.Forms.PictureBox imgPreview;

        public System.Windows.Forms.Label lblOutputImgsFormat;

        public System.Windows.Forms.Label lblOutputImgsPrefix;

        public System.Windows.Forms.Label lblOutputImgsEvery;

        public System.Windows.Forms.Label lblOutputImgsPath;

        public System.Windows.Forms.PictureBox udDelay;

        public System.Windows.Forms.CheckBox chkBackground;

        public System.Windows.Forms.RadioButton optNo;

        public System.Windows.Forms.RadioButton optReturn;

        public System.Windows.Forms.RadioButton optContinuos;

        public System.Windows.Forms.GroupBox fraLoop;

        public System.Windows.Forms.RadioButton optForward;

        public System.Windows.Forms.RadioButton optBackward;

        public System.Windows.Forms.GroupBox fraDirection;

        public System.Windows.Forms.PictureBox imgBackground;

        public System.Windows.Forms.Label lblFrameDelay;

        public System.Windows.Forms.CheckBox chkStatic;

        public System.Windows.Forms.RadioButton optStaticTarget;

        public System.Windows.Forms.RadioButton optStaticObserver;

        public System.Windows.Forms.GroupBox fraStatic;

        public System.Windows.Forms.ComboBox cboPointStep;

        public System.Windows.Forms.ComboBox cboRollFactor;

        public System.Windows.Forms.Label lblNoInclination;

        public System.Windows.Forms.Label lblPointStep;

        public System.Windows.Forms.Label lblRollFactor;

        public System.Windows.Forms.Label lblRollAngleR;

        public System.Windows.Forms.Label lblInclinationR;

        public System.Windows.Forms.Label lblAzimuthR;

        public System.Windows.Forms.Label lblRollAngle;

        public System.Windows.Forms.Label lblInclination;

        public System.Windows.Forms.Label lblAzimuth;

        public System.Windows.Forms.Timer tmrAnimation;

        internal System.Windows.Forms.TabControl TabControl1;

        internal System.Windows.Forms.ListBox flstOutputImgs;

        internal System.Windows.Forms.TrackBar sldFrameDelay;

        internal System.Windows.Forms.ListBox flstTextures;

        internal System.Windows.Forms.TrackBar sldInclination;

        public System.Windows.Forms.Button cmdAnim;

        public System.Windows.Forms.Button cmdDone;

        internal System.Windows.Forms.TrackBar sldTarOffset;

        public System.Windows.Forms.ComboBox cboOffsetType;

        public System.Windows.Forms.CheckBox chkOffset;

        public System.Windows.Forms.CheckBox chkSmooth;

        public System.Windows.Forms.CheckBox chkShowPath;

        public System.Windows.Forms.Label lblTarOffset;

        public System.Windows.Forms.Label lblMaxOffset;

        public System.Windows.Forms.ComboBox cboMaxOffset;

        public System.Windows.Forms.Label lblPntCount;

        public System.Windows.Forms.ListBox lstPoints;

        public System.Windows.Forms.Label lblStepSize;

        public System.Windows.Forms.ComboBox cboStepSize;

        internal System.Windows.Forms.TabPage fraPath;

        internal System.Windows.Forms.TabPage fraAnimation;

        internal System.Windows.Forms.TabPage fraCamera;


        internal System.Windows.Forms.TabPage fraOutput;
        private IContainer components;
    }
}