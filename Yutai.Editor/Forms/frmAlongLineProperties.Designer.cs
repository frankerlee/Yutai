namespace Yutai.Plugins.Editor.Forms
{
    partial class frmAlongLineProperties
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
            this.chkEnds = new System.Windows.Forms.CheckBox();
            this.rbDist = new System.Windows.Forms.RadioButton();
            this.rbNOP = new System.Windows.Forms.RadioButton();
            this.tbLineLength = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.txtNOP = new DevExpress.XtraEditors.SpinEdit();
            this.txtDist = new DevExpress.XtraEditors.SpinEdit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNOP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDist.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDist);
            this.groupBox1.Controls.Add(this.txtNOP);
            this.groupBox1.Controls.Add(this.chkEnds);
            this.groupBox1.Controls.Add(this.rbDist);
            this.groupBox1.Controls.Add(this.rbNOP);
            this.groupBox1.Location = new System.Drawing.Point(20, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 115);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "构建参数";
            // 
            // chkEnds
            // 
            this.chkEnds.AutoSize = true;
            this.chkEnds.Location = new System.Drawing.Point(7, 83);
            this.chkEnds.Name = "chkEnds";
            this.chkEnds.Size = new System.Drawing.Size(86, 18);
            this.chkEnds.TabIndex = 2;
            this.chkEnds.Text = "终点增加点";
            this.chkEnds.UseVisualStyleBackColor = true;
            // 
            // rbDist
            // 
            this.rbDist.AutoSize = true;
            this.rbDist.Location = new System.Drawing.Point(7, 45);
            this.rbDist.Name = "rbDist";
            this.rbDist.Size = new System.Drawing.Size(85, 18);
            this.rbDist.TabIndex = 1;
            this.rbDist.TabStop = true;
            this.rbDist.Text = "按距离创建";
            this.rbDist.UseVisualStyleBackColor = true;
            // 
            // rbNOP
            // 
            this.rbNOP.AutoSize = true;
            this.rbNOP.Checked = true;
            this.rbNOP.Location = new System.Drawing.Point(7, 21);
            this.rbNOP.Name = "rbNOP";
            this.rbNOP.Size = new System.Drawing.Size(85, 18);
            this.rbNOP.TabIndex = 0;
            this.rbNOP.TabStop = true;
            this.rbNOP.Text = "按点数创建";
            this.rbNOP.UseVisualStyleBackColor = true;
            // 
            // tbLineLength
            // 
            this.tbLineLength.Location = new System.Drawing.Point(100, 14);
            this.tbLineLength.Name = "tbLineLength";
            this.tbLineLength.ReadOnly = true;
            this.tbLineLength.Size = new System.Drawing.Size(178, 22);
            this.tbLineLength.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 14);
            this.label1.TabIndex = 5;
            this.label1.Text = "线长度:";
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(97, 165);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(87, 24);
            this.cmdOK.TabIndex = 9;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(191, 165);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(87, 24);
            this.cmdCancel.TabIndex = 8;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // txtNOP
            // 
            this.txtNOP.EditValue = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.txtNOP.Location = new System.Drawing.Point(98, 21);
            this.txtNOP.Name = "txtNOP";
            this.txtNOP.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtNOP.Properties.IsFloatValue = false;
            this.txtNOP.Properties.Mask.EditMask = "N00";
            this.txtNOP.Size = new System.Drawing.Size(100, 20);
            this.txtNOP.TabIndex = 3;
            // 
            // txtDist
            // 
            this.txtDist.EditValue = new decimal(new int[] {
            50,
            0,
            0,
            65536});
            this.txtDist.Location = new System.Drawing.Point(98, 47);
            this.txtDist.Name = "txtDist";
            this.txtDist.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDist.Size = new System.Drawing.Size(100, 20);
            this.txtDist.TabIndex = 4;
            // 
            // frmAlongLineProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 201);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbLineLength);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAlongLineProperties";
            this.Text = "创建参数";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNOP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDist.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkEnds;
        private System.Windows.Forms.RadioButton rbDist;
        private System.Windows.Forms.RadioButton rbNOP;
        private System.Windows.Forms.TextBox tbLineLength;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private DevExpress.XtraEditors.SpinEdit txtDist;
        private DevExpress.XtraEditors.SpinEdit txtNOP;
    }
}