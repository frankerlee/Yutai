using System;
using System.ComponentModel;

namespace Yutai.Plugins.Scene.Forms
{
	    partial class frmProperties
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
			this.ToolTip1 = new System.Windows.Forms.ToolTip(this.icontainer_0);
			this.Frame1 = new System.Windows.Forms.GroupBox();
			this.cmdTextureBrowse = new System.Windows.Forms.Button();
			this.cmbTextureGroup = new System.Windows.Forms.ComboBox();
			this.txtDigitizeHeight = new System.Windows.Forms.TextBox();
			this.cmbTargetLayer = new System.Windows.Forms.ComboBox();
			this.Label4 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.Frame1.SuspendLayout();
			base.SuspendLayout();
			this.Frame1.BackColor = System.Drawing.SystemColors.Control;
			this.Frame1.Controls.Add(this.label2);
			this.Frame1.Controls.Add(this.txtDigitizeHeight);
			this.Frame1.Controls.Add(this.cmdTextureBrowse);
			this.Frame1.Controls.Add(this.cmbTextureGroup);
			this.Frame1.Controls.Add(this.cmbTargetLayer);
			this.Frame1.Controls.Add(this.Label4);
			this.Frame1.Controls.Add(this.Label3);
			this.Frame1.Controls.Add(this.Label1);
			this.Frame1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Frame1.Location = new System.Drawing.Point(8, 8);
			this.Frame1.Name = "Frame1";
			this.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Frame1.Size = new System.Drawing.Size(273, 161);
			this.Frame1.TabIndex = 2;
			this.Frame1.TabStop = false;
			this.Frame1.Text = "默认设置";
			this.cmdTextureBrowse.BackColor = System.Drawing.SystemColors.Control;
			this.cmdTextureBrowse.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdTextureBrowse.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdTextureBrowse.Location = new System.Drawing.Point(232, 116);
			this.cmdTextureBrowse.Name = "cmdTextureBrowse";
			this.cmdTextureBrowse.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdTextureBrowse.Size = new System.Drawing.Size(33, 17);
			this.cmdTextureBrowse.TabIndex = 8;
			this.cmdTextureBrowse.Text = "...";
			this.cmdTextureBrowse.UseVisualStyleBackColor = false;
			this.cmdTextureBrowse.Click += new EventHandler(this.cmdTextureBrowse_Click);
			this.cmbTextureGroup.BackColor = System.Drawing.SystemColors.Window;
			this.cmbTextureGroup.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmbTextureGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTextureGroup.ForeColor = System.Drawing.SystemColors.WindowText;
			this.cmbTextureGroup.Location = new System.Drawing.Point(8, 116);
			this.cmbTextureGroup.Name = "cmbTextureGroup";
			this.cmbTextureGroup.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmbTextureGroup.Size = new System.Drawing.Size(209, 20);
			this.cmbTextureGroup.TabIndex = 7;
			this.txtDigitizeHeight.AcceptsReturn = true;
			this.txtDigitizeHeight.BackColor = System.Drawing.SystemColors.Window;
			this.txtDigitizeHeight.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtDigitizeHeight.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtDigitizeHeight.Location = new System.Drawing.Point(77, 65);
			this.txtDigitizeHeight.MaxLength = 0;
			this.txtDigitizeHeight.Name = "txtDigitizeHeight";
			this.txtDigitizeHeight.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtDigitizeHeight.Size = new System.Drawing.Size(76, 21);
			this.txtDigitizeHeight.TabIndex = 5;
			this.cmbTargetLayer.BackColor = System.Drawing.SystemColors.Window;
			this.cmbTargetLayer.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmbTargetLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTargetLayer.ForeColor = System.Drawing.SystemColors.WindowText;
			this.cmbTargetLayer.Location = new System.Drawing.Point(8, 32);
			this.cmbTargetLayer.Name = "cmbTargetLayer";
			this.cmbTargetLayer.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmbTargetLayer.Size = new System.Drawing.Size(257, 20);
			this.cmbTargetLayer.TabIndex = 3;
			this.cmbTargetLayer.SelectedIndexChanged += new EventHandler(this.cmbTargetLayer_SelectedIndexChanged);
			this.Label4.AutoSize = true;
			this.Label4.BackColor = System.Drawing.SystemColors.Control;
			this.Label4.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label4.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label4.Location = new System.Drawing.Point(162, 68);
			this.Label4.Name = "Label4";
			this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label4.Size = new System.Drawing.Size(83, 12);
			this.Label4.TabIndex = 10;
			this.Label4.Text = "scene z units";
			this.Label4.Visible = false;
			this.Label3.BackColor = System.Drawing.SystemColors.Control;
			this.Label3.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label3.Location = new System.Drawing.Point(8, 100);
			this.Label3.Name = "Label3";
			this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label3.Size = new System.Drawing.Size(257, 17);
			this.Label3.TabIndex = 9;
			this.Label3.Text = "建筑物四周纹理和屋顶符号组";
			this.Label1.AutoSize = true;
			this.Label1.BackColor = System.Drawing.SystemColors.Control;
			this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label1.Location = new System.Drawing.Point(8, 16);
			this.Label1.Name = "Label1";
			this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label1.Size = new System.Drawing.Size(41, 12);
			this.Label1.TabIndex = 4;
			this.Label1.Text = "3D图层";
			this.cmdOK.BackColor = System.Drawing.SystemColors.Control;
			this.cmdOK.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdOK.Location = new System.Drawing.Point(176, 175);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdOK.Size = new System.Drawing.Size(49, 25);
			this.cmdOK.TabIndex = 0;
			this.cmdOK.Text = "确定";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.cmdCancel.BackColor = System.Drawing.SystemColors.Control;
			this.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdCancel.Location = new System.Drawing.Point(232, 175);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdCancel.Size = new System.Drawing.Size(49, 25);
			this.cmdCancel.TabIndex = 1;
			this.cmdCancel.Text = "取消";
			this.cmdCancel.UseVisualStyleBackColor = false;
			this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.SystemColors.Control;
			this.label2.Cursor = System.Windows.Forms.Cursors.Default;
			this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label2.Location = new System.Drawing.Point(6, 68);
			this.label2.Name = "label2";
			this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.label2.Size = new System.Drawing.Size(65, 12);
			this.label2.TabIndex = 6;
			this.label2.Text = "建筑物高度";
			base.AcceptButton = this.cmdOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			base.CancelButton = this.cmdCancel;
			base.ClientSize = new System.Drawing.Size(288, 210);
			base.Controls.Add(this.Frame1);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.cmdCancel);
			this.Cursor = System.Windows.Forms.Cursors.Default;
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Location = new System.Drawing.Point(2, 22);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmProperties";
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			base.ShowInTaskbar = false;
			this.Text = "3D建筑属性";
			base.Load += new EventHandler(this.frmProperties_Load);
			this.Frame1.ResumeLayout(false);
			this.Frame1.PerformLayout();
			base.ResumeLayout(false);
		}

	
		private IContainer icontainer_0;
        public System.Windows.Forms.ToolTip ToolTip1;

        public System.Windows.Forms.Button cmdTextureBrowse;

        public System.Windows.Forms.ComboBox cmbTextureGroup;

        public System.Windows.Forms.TextBox txtDigitizeHeight;

        public System.Windows.Forms.ComboBox cmbTargetLayer;

        public System.Windows.Forms.Label Label4;

        public System.Windows.Forms.Label Label3;

        public System.Windows.Forms.Label Label1;

        public System.Windows.Forms.GroupBox Frame1;

        public System.Windows.Forms.Button cmdOK;

        public System.Windows.Forms.Button cmdCancel;


        public System.Windows.Forms.Label label2;
       
    }
}