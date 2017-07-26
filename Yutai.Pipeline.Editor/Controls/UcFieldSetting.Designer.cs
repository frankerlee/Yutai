namespace Yutai.Pipeline.Editor.Controls
{
    partial class UcFieldSetting
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtAttribute = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbField = new Yutai.Pipeline.Editor.Controls.UcSelectField();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtExpression = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.numLength = new System.Windows.Forms.NumericUpDown();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLength)).BeginInit();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtAttribute);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(80, 26);
            this.panel1.TabIndex = 0;
            // 
            // txtAttribute
            // 
            this.txtAttribute.BackColor = System.Drawing.SystemColors.Control;
            this.txtAttribute.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAttribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAttribute.Location = new System.Drawing.Point(3, 3);
            this.txtAttribute.Multiline = true;
            this.txtAttribute.Name = "txtAttribute";
            this.txtAttribute.Size = new System.Drawing.Size(74, 20);
            this.txtAttribute.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmbField);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(80, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(120, 26);
            this.panel2.TabIndex = 1;
            // 
            // cmbField
            // 
            this.cmbField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbField.Enabled = false;
            this.cmbField.Label = "";
            this.cmbField.LabelWidth = 25;
            this.cmbField.Location = new System.Drawing.Point(3, 3);
            this.cmbField.Name = "cmbField";
            this.cmbField.Size = new System.Drawing.Size(114, 20);
            this.cmbField.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtExpression);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(200, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3);
            this.panel3.Size = new System.Drawing.Size(203, 26);
            this.panel3.TabIndex = 2;
            // 
            // txtExpression
            // 
            this.txtExpression.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtExpression.Enabled = false;
            this.txtExpression.Location = new System.Drawing.Point(3, 3);
            this.txtExpression.Multiline = true;
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.Size = new System.Drawing.Size(197, 20);
            this.txtExpression.TabIndex = 0;
            this.txtExpression.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtExpression_MouseDoubleClick);
            this.txtExpression.MouseEnter += new System.EventHandler(this.txtExpression_MouseEnter);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.numLength);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(403, 0);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(3);
            this.panel4.Size = new System.Drawing.Size(80, 26);
            this.panel4.TabIndex = 3;
            // 
            // numLength
            // 
            this.numLength.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numLength.Enabled = false;
            this.numLength.Location = new System.Drawing.Point(3, 3);
            this.numLength.Name = "numLength";
            this.numLength.Size = new System.Drawing.Size(74, 21);
            this.numLength.TabIndex = 1;
            this.numLength.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnDelete);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(483, 0);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(3);
            this.panel5.Size = new System.Drawing.Size(60, 26);
            this.panel5.TabIndex = 4;
            // 
            // btnDelete
            // 
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelete.Location = new System.Drawing.Point(3, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(54, 20);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // UcFieldSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "UcFieldSetting";
            this.Size = new System.Drawing.Size(543, 26);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numLength)).EndInit();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtAttribute;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtExpression;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private UcSelectField cmbField;
        private System.Windows.Forms.NumericUpDown numLength;
        private System.Windows.Forms.Button btnDelete;
    }
}
