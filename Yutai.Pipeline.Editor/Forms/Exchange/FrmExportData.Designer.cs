namespace Yutai.Pipeline.Editor.Forms.Exchange
{
    partial class FrmExportData
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelLayers = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ucSpatialReference1 = new Yutai.Pipeline.Editor.Controls.UcSpatialReference();
            this.ucExtentSetting1 = new Yutai.Pipeline.Editor.Controls.UcExtentSetting();
            this.ucExportPath1 = new Yutai.Pipeline.Editor.Controls.UcExportPath();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panelLayers);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(257, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(533, 244);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择输出要素及其字段";
            // 
            // panelLayers
            // 
            this.panelLayers.AutoScroll = true;
            this.panelLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLayers.Location = new System.Drawing.Point(3, 17);
            this.panelLayers.Name = "panelLayers";
            this.panelLayers.Size = new System.Drawing.Size(527, 224);
            this.panelLayers.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnOK);
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.ucExportPath1);
            this.groupBox2.Controls.Add(this.ucSpatialReference1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(257, 249);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(533, 184);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设置输出参数";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(252, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(5, 433);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(257, 244);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(533, 5);
            this.panel2.TabIndex = 4;
            // 
            // ucSpatialReference1
            // 
            this.ucSpatialReference1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucSpatialReference1.Location = new System.Drawing.Point(3, 17);
            this.ucSpatialReference1.Name = "ucSpatialReference1";
            this.ucSpatialReference1.Size = new System.Drawing.Size(527, 81);
            this.ucSpatialReference1.TabIndex = 0;
            // 
            // ucExtentSetting1
            // 
            this.ucExtentSetting1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ucExtentSetting1.Enabled = false;
            this.ucExtentSetting1.Location = new System.Drawing.Point(0, 0);
            this.ucExtentSetting1.Name = "ucExtentSetting1";
            this.ucExtentSetting1.Size = new System.Drawing.Size(252, 433);
            this.ucExtentSetting1.TabIndex = 0;
            // 
            // ucExportPath1
            // 
            this.ucExportPath1.Location = new System.Drawing.Point(77, 104);
            this.ucExportPath1.Name = "ucExportPath1";
            this.ucExportPath1.Size = new System.Drawing.Size(450, 20);
            this.ucExportPath1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "输出位置：";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(452, 153);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(371, 153);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FrmExportData
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(790, 433);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ucExtentSetting1);
            this.Name = "FrmExportData";
            this.Text = "数据输出";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.UcExtentSetting ucExtentSetting1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelLayers;
        private Controls.UcSpatialReference ucSpatialReference1;
        private System.Windows.Forms.Label label1;
        private Controls.UcExportPath ucExportPath1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}