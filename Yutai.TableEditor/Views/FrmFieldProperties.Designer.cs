using System.Windows.Forms;

namespace Yutai.Plugins.TableEditor.Views
{
    partial class FrmFieldProperties
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAlias = new System.Windows.Forms.TextBox();
            this.chkVisible = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.udPrecision = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.udWidth = new System.Windows.Forms.NumericUpDown();
            this.cboDataType = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtExpression = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.udPrecision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWidth)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(228, 294);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 24);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(138, 294);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(85, 24);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Ok";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 37;
            this.label1.Text = "名称";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(91, 27);
            this.txtName.MaxLength = 10;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(192, 21);
            this.txtName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 39;
            this.label2.Text = "别名";
            // 
            // txtAlias
            // 
            this.txtAlias.Location = new System.Drawing.Point(91, 62);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.Size = new System.Drawing.Size(192, 21);
            this.txtAlias.TabIndex = 2;
            // 
            // chkVisible
            // 
            this.chkVisible.AutoSize = true;
            this.chkVisible.Location = new System.Drawing.Point(91, 97);
            this.chkVisible.Name = "chkVisible";
            this.chkVisible.Size = new System.Drawing.Size(72, 16);
            this.chkVisible.TabIndex = 3;
            this.chkVisible.Text = "是否显示";
            this.chkVisible.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 42;
            this.label3.Text = "数据类型";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 203);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 44;
            this.label4.Text = "精度";
            // 
            // udPrecision
            // 
            this.udPrecision.Location = new System.Drawing.Point(91, 200);
            this.udPrecision.Maximum = new decimal(new int[] {
            18,
            0,
            0,
            0});
            this.udPrecision.Name = "udPrecision";
            this.udPrecision.Size = new System.Drawing.Size(192, 21);
            this.udPrecision.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 168);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 46;
            this.label5.Text = "字段长度";
            // 
            // udWidth
            // 
            this.udWidth.Location = new System.Drawing.Point(91, 165);
            this.udWidth.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.udWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udWidth.Name = "udWidth";
            this.udWidth.Size = new System.Drawing.Size(192, 21);
            this.udWidth.TabIndex = 5;
            this.udWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cboDataType
            // 
            this.cboDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDataType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cboDataType.Location = new System.Drawing.Point(91, 129);
            this.cboDataType.Name = "cboDataType";
            this.cboDataType.Size = new System.Drawing.Size(192, 21);
            this.cboDataType.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtExpression);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboDataType);
            this.groupBox1.Controls.Add(this.udPrecision);
            this.groupBox1.Controls.Add(this.chkVisible);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.txtAlias);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.udWidth);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(9, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 278);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // txtExpression
            // 
            this.txtExpression.Location = new System.Drawing.Point(91, 235);
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.ReadOnly = true;
            this.txtExpression.Size = new System.Drawing.Size(192, 21);
            this.txtExpression.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 238);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 49;
            this.label6.Text = "表达式";
            // 
            // FrmFieldProperties
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(322, 324);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "FrmFieldProperties";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "字段属性";
            this.Load += new System.EventHandler(this.FrmFieldProperties_Load);
            ((System.ComponentModel.ISupportInitialize)(this.udPrecision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWidth)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAlias;
        private System.Windows.Forms.CheckBox chkVisible;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown udPrecision;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown udWidth;
        private System.Windows.Forms.ComboBox cboDataType;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtExpression;
        private System.Windows.Forms.Label label6;
    }
}