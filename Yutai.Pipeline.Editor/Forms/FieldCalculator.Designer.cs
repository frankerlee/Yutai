using Yutai.Pipeline.Editor.Controls;

namespace Yutai.Pipeline.Editor.Forms
{
    partial class FieldCalculator
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
            this.components = new System.ComponentModel.Container();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.fieldListBox = new Yutai.Pipeline.Editor.Controls.FieldsListBox(this.components);
            this.txtExpression = new Yutai.Pipeline.Editor.Controls.ExpressionTextBox(this.components);
            this.numLength = new System.Windows.Forms.NumericUpDown();
            this.btnAddSerial = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numLength)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(189, 355);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 24);
            this.btnCancel.TabIndex = 34;
            this.btnCancel.Text = "取消";
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(99, 355);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(85, 24);
            this.btnOk.TabIndex = 33;
            this.btnOk.Text = "计算";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 44;
            this.label1.Text = "字段：";
            // 
            // fieldListBox
            // 
            this.fieldListBox.BackColor = System.Drawing.SystemColors.Window;
            this.fieldListBox.ItemHeight = 12;
            this.fieldListBox.Location = new System.Drawing.Point(12, 24);
            this.fieldListBox.Name = "fieldListBox";
            this.fieldListBox.Size = new System.Drawing.Size(262, 160);
            this.fieldListBox.TabIndex = 65;
            this.fieldListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.fieldListBox_MouseDoubleClick);
            // 
            // txtExpression
            // 
            this.txtExpression.HideSelection = false;
            this.txtExpression.Location = new System.Drawing.Point(12, 220);
            this.txtExpression.Multiline = true;
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.Size = new System.Drawing.Size(262, 129);
            this.txtExpression.TabIndex = 66;
            // 
            // numLength
            // 
            this.numLength.Location = new System.Drawing.Point(12, 193);
            this.numLength.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numLength.Name = "numLength";
            this.numLength.Size = new System.Drawing.Size(59, 21);
            this.numLength.TabIndex = 67;
            this.numLength.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // btnAddSerial
            // 
            this.btnAddSerial.Location = new System.Drawing.Point(77, 192);
            this.btnAddSerial.Name = "btnAddSerial";
            this.btnAddSerial.Size = new System.Drawing.Size(75, 23);
            this.btnAddSerial.TabIndex = 68;
            this.btnAddSerial.Text = "添加流水号";
            this.btnAddSerial.UseVisualStyleBackColor = true;
            this.btnAddSerial.Click += new System.EventHandler(this.btnAddSerial_Click);
            // 
            // FieldCalculator
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(283, 385);
            this.Controls.Add(this.btnAddSerial);
            this.Controls.Add(this.numLength);
            this.Controls.Add(this.txtExpression);
            this.Controls.Add(this.fieldListBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FieldCalculator";
            this.Text = "字段计算器";
            ((System.ComponentModel.ISupportInitialize)(this.numLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
        private FieldsListBox fieldListBox;
        private ExpressionTextBox txtExpression;
        private System.Windows.Forms.NumericUpDown numLength;
        private System.Windows.Forms.Button btnAddSerial;
    }
}