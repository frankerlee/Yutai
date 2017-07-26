namespace Yutai.Pipeline.Editor.Forms.Exchange
{
    partial class FrmClip
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ucExtentSetting1 = new Yutai.Pipeline.Editor.Controls.UcExtentSetting();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ucExportPath1 = new Yutai.Pipeline.Editor.Controls.UcExportPath();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelLayers = new System.Windows.Forms.Panel();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucExtentSetting1
            // 
            this.ucExtentSetting1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ucExtentSetting1.Enabled = false;
            this.ucExtentSetting1.Location = new System.Drawing.Point(0, 0);
            this.ucExtentSetting1.Name = "ucExtentSetting1";
            this.ucExtentSetting1.Size = new System.Drawing.Size(252, 357);
            this.ucExtentSetting1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnOK);
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.ucExportPath1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(252, 255);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(542, 102);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设置输出参数";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(375, 69);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(456, 69);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "输出位置：";
            // 
            // ucExportPath1
            // 
            this.ucExportPath1.BackColor = System.Drawing.SystemColors.Control;
            this.ucExportPath1.Location = new System.Drawing.Point(81, 20);
            this.ucExportPath1.Name = "ucExportPath1";
            this.ucExportPath1.Size = new System.Drawing.Size(450, 20);
            this.ucExportPath1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panelLayers);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(252, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(542, 255);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择裁剪图层";
            // 
            // panelLayers
            // 
            this.panelLayers.AutoScroll = true;
            this.panelLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLayers.Location = new System.Drawing.Point(3, 17);
            this.panelLayers.Name = "panelLayers";
            this.panelLayers.Size = new System.Drawing.Size(536, 235);
            this.panelLayers.TabIndex = 0;
            // 
            // FrmClip
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(794, 357);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.ucExtentSetting1);
            this.Name = "FrmClip";
            this.Text = "裁剪";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmClip_FormClosed);
            this.Load += new System.EventHandler(this.FrmClip_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.UcExtentSetting ucExtentSetting1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private Controls.UcExportPath ucExportPath1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panelLayers;
    }
}