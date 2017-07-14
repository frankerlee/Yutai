using System;
using System.ComponentModel;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Yutai.Plugins.Scene.Forms
{
	    partial class frmViewSettings
    {
		protected override void Dispose(bool bool_1)
		{
			if (bool_1 && this.icontainer_0 != null)
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(bool_1);
		}

	
	private void InitializeComponent()
		{
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cboViewers = new DevExpress.XtraEditors.ComboBoxEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDistance = new DevExpress.XtraEditors.LabelControl();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.txtTargetZ = new DevExpress.XtraEditors.TextEdit();
            this.txtTargetY = new DevExpress.XtraEditors.TextEdit();
            this.txtTargetX = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.txtObserverZ = new DevExpress.XtraEditors.TextEdit();
            this.txtObserverY = new DevExpress.XtraEditors.TextEdit();
            this.txtObserverX = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.spinRollAngle = new DevExpress.XtraEditors.SpinEdit();
            this.spinViewFieldAngle = new DevExpress.XtraEditors.SpinEdit();
            this.rdoProjectionType = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cboViewers.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTargetZ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTargetY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTargetX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObserverZ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObserverY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObserverX.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinRollAngle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinViewFieldAngle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoProjectionType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "应用到";
            // 
            // cboViewers
            // 
            this.cboViewers.Location = new System.Drawing.Point(80, 6);
            this.cboViewers.Name = "cboViewers";
            this.cboViewers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboViewers.Size = new System.Drawing.Size(164, 20);
            this.cboViewers.TabIndex = 1;
            this.cboViewers.SelectedIndexChanged += new System.EventHandler(this.cboViewers_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblDistance);
            this.groupBox1.Controls.Add(this.btnApply);
            this.groupBox1.Controls.Add(this.labelControl10);
            this.groupBox1.Controls.Add(this.txtTargetZ);
            this.groupBox1.Controls.Add(this.txtTargetY);
            this.groupBox1.Controls.Add(this.txtTargetX);
            this.groupBox1.Controls.Add(this.labelControl6);
            this.groupBox1.Controls.Add(this.labelControl7);
            this.groupBox1.Controls.Add(this.labelControl8);
            this.groupBox1.Controls.Add(this.labelControl9);
            this.groupBox1.Controls.Add(this.txtObserverZ);
            this.groupBox1.Controls.Add(this.txtObserverY);
            this.groupBox1.Controls.Add(this.txtObserverX);
            this.groupBox1.Controls.Add(this.labelControl5);
            this.groupBox1.Controls.Add(this.labelControl4);
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Location = new System.Drawing.Point(13, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(305, 162);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "位置";
            // 
            // lblDistance
            // 
            this.lblDistance.Location = new System.Drawing.Point(120, 133);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(0, 14);
            this.lblDistance.TabIndex = 17;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(190, 133);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 16;
            this.btnApply.Text = "应用";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(17, 130);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(88, 14);
            this.labelControl10.TabIndex = 15;
            this.labelControl10.Text = "到目标点的距离:";
            // 
            // txtTargetZ
            // 
            this.txtTargetZ.Location = new System.Drawing.Point(190, 97);
            this.txtTargetZ.Name = "txtTargetZ";
            this.txtTargetZ.Size = new System.Drawing.Size(100, 20);
            this.txtTargetZ.TabIndex = 14;
            // 
            // txtTargetY
            // 
            this.txtTargetY.Location = new System.Drawing.Point(190, 67);
            this.txtTargetY.Name = "txtTargetY";
            this.txtTargetY.Size = new System.Drawing.Size(100, 20);
            this.txtTargetY.TabIndex = 13;
            // 
            // txtTargetX
            // 
            this.txtTargetX.Location = new System.Drawing.Point(190, 40);
            this.txtTargetX.Name = "txtTargetX";
            this.txtTargetX.Size = new System.Drawing.Size(100, 20);
            this.txtTargetX.TabIndex = 12;
            this.txtTargetX.EditValueChanged += new System.EventHandler(this.txtTargetX_EditValueChanged);
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(161, 101);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(7, 14);
            this.labelControl6.TabIndex = 11;
            this.labelControl6.Text = "Z";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(161, 69);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(8, 14);
            this.labelControl7.TabIndex = 10;
            this.labelControl7.Text = "Y";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(161, 41);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(7, 14);
            this.labelControl8.TabIndex = 9;
            this.labelControl8.Text = "X";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(161, 20);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(36, 14);
            this.labelControl9.TabIndex = 8;
            this.labelControl9.Text = "目标点";
            // 
            // txtObserverZ
            // 
            this.txtObserverZ.Location = new System.Drawing.Point(46, 97);
            this.txtObserverZ.Name = "txtObserverZ";
            this.txtObserverZ.Size = new System.Drawing.Size(100, 20);
            this.txtObserverZ.TabIndex = 7;
            // 
            // txtObserverY
            // 
            this.txtObserverY.Location = new System.Drawing.Point(46, 67);
            this.txtObserverY.Name = "txtObserverY";
            this.txtObserverY.Size = new System.Drawing.Size(100, 20);
            this.txtObserverY.TabIndex = 6;
            // 
            // txtObserverX
            // 
            this.txtObserverX.Location = new System.Drawing.Point(46, 40);
            this.txtObserverX.Name = "txtObserverX";
            this.txtObserverX.Size = new System.Drawing.Size(100, 20);
            this.txtObserverX.TabIndex = 5;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(17, 101);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(7, 14);
            this.labelControl5.TabIndex = 4;
            this.labelControl5.Text = "Z";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(17, 69);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(8, 14);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "Y";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(17, 41);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(7, 14);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "X";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(17, 20);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "观测点";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelControl13);
            this.groupBox2.Controls.Add(this.labelControl12);
            this.groupBox2.Controls.Add(this.spinRollAngle);
            this.groupBox2.Controls.Add(this.spinViewFieldAngle);
            this.groupBox2.Controls.Add(this.rdoProjectionType);
            this.groupBox2.Controls.Add(this.labelControl11);
            this.groupBox2.Location = new System.Drawing.Point(13, 212);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(316, 165);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "View属性";
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(152, 30);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(36, 14);
            this.labelControl13.TabIndex = 7;
            this.labelControl13.Text = "翻滚角";
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(29, 126);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(24, 14);
            this.labelControl12.TabIndex = 6;
            this.labelControl12.Text = "视角";
            // 
            // spinRollAngle
            // 
            this.spinRollAngle.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinRollAngle.Location = new System.Drawing.Point(194, 23);
            this.spinRollAngle.Name = "spinRollAngle";
            this.spinRollAngle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinRollAngle.Size = new System.Drawing.Size(100, 20);
            this.spinRollAngle.TabIndex = 5;
            // 
            // spinViewFieldAngle
            // 
            this.spinViewFieldAngle.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinViewFieldAngle.Location = new System.Drawing.Point(97, 126);
            this.spinViewFieldAngle.Name = "spinViewFieldAngle";
            this.spinViewFieldAngle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinViewFieldAngle.Size = new System.Drawing.Size(100, 20);
            this.spinViewFieldAngle.TabIndex = 4;
            // 
            // rdoProjectionType
            // 
            this.rdoProjectionType.Location = new System.Drawing.Point(17, 40);
            this.rdoProjectionType.Name = "rdoProjectionType";
            this.rdoProjectionType.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.rdoProjectionType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoProjectionType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rdoProjectionType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "透视投影"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "正射投影")});
            this.rdoProjectionType.Size = new System.Drawing.Size(100, 80);
            this.rdoProjectionType.TabIndex = 3;
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(17, 20);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(24, 14);
            this.labelControl11.TabIndex = 2;
            this.labelControl11.Text = "投影";
            // 
            // simpleButton2
            // 
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(232, 383);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 17;
            this.simpleButton2.Text = "取消";
            // 
            // frmViewSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 447);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cboViewers);
            this.Controls.Add(this.labelControl1);
            this.Name = "frmViewSettings";
            this.Text = "视窗设置";
            this.Load += new System.EventHandler(this.frmViewSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cboViewers.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTargetZ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTargetY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTargetX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObserverZ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObserverY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObserverX.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinRollAngle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinViewFieldAngle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoProjectionType.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

	
		private LabelControl labelControl1;
		private ComboBoxEdit cboViewers;
		private System.Windows.Forms.GroupBox groupBox1;
		private TextEdit txtObserverZ;
		private TextEdit txtObserverY;
		private TextEdit txtObserverX;
		private LabelControl labelControl5;
		private LabelControl labelControl4;
		private LabelControl labelControl3;
		private LabelControl labelControl2;
		private System.Windows.Forms.GroupBox groupBox2;
		private SimpleButton btnApply;
		private LabelControl labelControl10;
		private TextEdit txtTargetZ;
		private TextEdit txtTargetY;
		private TextEdit txtTargetX;
		private LabelControl labelControl6;
		private LabelControl labelControl7;
		private LabelControl labelControl8;
		private LabelControl labelControl9;
		private LabelControl labelControl11;
		private RadioGroup rdoProjectionType;
		private LabelControl labelControl13;
		private LabelControl labelControl12;
		private SpinEdit spinRollAngle;
		private SpinEdit spinViewFieldAngle;
		private SimpleButton simpleButton2;
		private LabelControl lblDistance;
    }
}