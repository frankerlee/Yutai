namespace Yutai.Pipeline.Editor.Forms.Profession
{
    partial class FrmAutoGenerateCode
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtPointLayer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLineLayer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbKeyField = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbStartKeyField = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbEndKeyField = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbStartCodeField = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbEndCodeField = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbCodeField = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtRule = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.checkBoxUseAttr = new System.Windows.Forms.CheckBox();
            this.cmbPipelineLayers = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnCreateRule = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "点图层：";
            // 
            // txtPointLayer
            // 
            this.txtPointLayer.Location = new System.Drawing.Point(65, 14);
            this.txtPointLayer.Name = "txtPointLayer";
            this.txtPointLayer.ReadOnly = true;
            this.txtPointLayer.Size = new System.Drawing.Size(195, 21);
            this.txtPointLayer.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "线图层：";
            // 
            // txtLineLayer
            // 
            this.txtLineLayer.Location = new System.Drawing.Point(65, 14);
            this.txtLineLayer.Name = "txtLineLayer";
            this.txtLineLayer.ReadOnly = true;
            this.txtLineLayer.Size = new System.Drawing.Size(195, 21);
            this.txtLineLayer.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "点线关联字段：";
            // 
            // cmbKeyField
            // 
            this.cmbKeyField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKeyField.FormattingEnabled = true;
            this.cmbKeyField.Location = new System.Drawing.Point(101, 40);
            this.cmbKeyField.Name = "cmbKeyField";
            this.cmbKeyField.Size = new System.Drawing.Size(159, 20);
            this.cmbKeyField.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "起点关联字段：";
            // 
            // cmbStartKeyField
            // 
            this.cmbStartKeyField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStartKeyField.FormattingEnabled = true;
            this.cmbStartKeyField.Location = new System.Drawing.Point(101, 40);
            this.cmbStartKeyField.Name = "cmbStartKeyField";
            this.cmbStartKeyField.Size = new System.Drawing.Size(159, 20);
            this.cmbStartKeyField.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "终点关联字段：";
            // 
            // cmbEndKeyField
            // 
            this.cmbEndKeyField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEndKeyField.FormattingEnabled = true;
            this.cmbEndKeyField.Location = new System.Drawing.Point(101, 66);
            this.cmbEndKeyField.Name = "cmbEndKeyField";
            this.cmbEndKeyField.Size = new System.Drawing.Size(159, 20);
            this.cmbEndKeyField.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "起点编号字段：";
            // 
            // cmbStartCodeField
            // 
            this.cmbStartCodeField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStartCodeField.FormattingEnabled = true;
            this.cmbStartCodeField.Location = new System.Drawing.Point(101, 92);
            this.cmbStartCodeField.Name = "cmbStartCodeField";
            this.cmbStartCodeField.Size = new System.Drawing.Size(159, 20);
            this.cmbStartCodeField.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 121);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "终点编号字段：";
            // 
            // cmbEndCodeField
            // 
            this.cmbEndCodeField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEndCodeField.FormattingEnabled = true;
            this.cmbEndCodeField.Location = new System.Drawing.Point(101, 118);
            this.cmbEndCodeField.Name = "cmbEndCodeField";
            this.cmbEndCodeField.Size = new System.Drawing.Size(159, 20);
            this.cmbEndCodeField.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "编号字段：";
            // 
            // cmbCodeField
            // 
            this.cmbCodeField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCodeField.FormattingEnabled = true;
            this.cmbCodeField.Location = new System.Drawing.Point(77, 66);
            this.cmbCodeField.Name = "cmbCodeField";
            this.cmbCodeField.Size = new System.Drawing.Size(183, 20);
            this.cmbCodeField.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "编号规则：";
            // 
            // txtRule
            // 
            this.txtRule.Location = new System.Drawing.Point(77, 14);
            this.txtRule.Multiline = true;
            this.txtRule.Name = "txtRule";
            this.txtRule.ReadOnly = true;
            this.txtRule.Size = new System.Drawing.Size(183, 119);
            this.txtRule.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtPointLayer);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbKeyField);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cmbCodeField);
            this.groupBox1.Location = new System.Drawing.Point(12, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(271, 97);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "点图层设置";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtLineLayer);
            this.groupBox2.Controls.Add(this.cmbStartKeyField);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cmbEndKeyField);
            this.groupBox2.Controls.Add(this.cmbStartCodeField);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.cmbEndCodeField);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(12, 141);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(271, 146);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "线图层设置";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnCreateRule);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtRule);
            this.groupBox3.Location = new System.Drawing.Point(12, 293);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(271, 167);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "规则设置";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(208, 466);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(127, 466);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // checkBoxUseAttr
            // 
            this.checkBoxUseAttr.AutoSize = true;
            this.checkBoxUseAttr.Checked = true;
            this.checkBoxUseAttr.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUseAttr.Location = new System.Drawing.Point(20, 470);
            this.checkBoxUseAttr.Name = "checkBoxUseAttr";
            this.checkBoxUseAttr.Size = new System.Drawing.Size(96, 16);
            this.checkBoxUseAttr.TabIndex = 8;
            this.checkBoxUseAttr.Text = "使用属性关联";
            this.checkBoxUseAttr.UseVisualStyleBackColor = true;
            // 
            // cmbPipelineLayers
            // 
            this.cmbPipelineLayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPipelineLayers.FormattingEnabled = true;
            this.cmbPipelineLayers.Location = new System.Drawing.Point(77, 12);
            this.cmbPipelineLayers.Name = "cmbPipelineLayers";
            this.cmbPipelineLayers.Size = new System.Drawing.Size(195, 20);
            this.cmbPipelineLayers.TabIndex = 9;
            this.cmbPipelineLayers.SelectedIndexChanged += new System.EventHandler(this.cmbPipelineLayers_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(18, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 10;
            this.label11.Text = "图层组：";
            // 
            // btnCreateRule
            // 
            this.btnCreateRule.Location = new System.Drawing.Point(185, 139);
            this.btnCreateRule.Name = "btnCreateRule";
            this.btnCreateRule.Size = new System.Drawing.Size(75, 23);
            this.btnCreateRule.TabIndex = 4;
            this.btnCreateRule.Text = "生成表达式";
            this.btnCreateRule.UseVisualStyleBackColor = true;
            this.btnCreateRule.Click += new System.EventHandler(this.btnCreateRule_Click);
            // 
            // FrmAutoGenerateCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 497);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmbPipelineLayers);
            this.Controls.Add(this.checkBoxUseAttr);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmAutoGenerateCode";
            this.Text = "自动生成编号";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPointLayer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLineLayer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbKeyField;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbStartKeyField;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbEndKeyField;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbStartCodeField;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbEndCodeField;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbCodeField;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtRule;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox checkBoxUseAttr;
        private System.Windows.Forms.ComboBox cmbPipelineLayers;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnCreateRule;
    }
}