using Yutai.Plugins.TableEditor.Controls;

namespace Yutai.Plugins.TableEditor.Views
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
            this.lblValidation = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblField = new System.Windows.Forms.Label();
            this.btnMinus = new System.Windows.Forms.Button();
            this.btnMultiply = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnPlus = new System.Windows.Forms.Button();
            this.btnDivide = new System.Windows.Forms.Button();
            this.lblType = new System.Windows.Forms.Label();
            this.fieldListBox = new Yutai.Plugins.TableEditor.Controls.FieldsListBox(this.components);
            this.txtExpression = new Yutai.Plugins.TableEditor.Controls.ExpressionTextBox(this.components);
            this.functionTreeView = new Yutai.Plugins.TableEditor.Controls.FunctionTreeView(this.components);
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(482, 354);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 24);
            this.btnCancel.TabIndex = 34;
            this.btnCancel.Text = "取消";
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(392, 354);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(85, 24);
            this.btnOk.TabIndex = 33;
            this.btnOk.Text = "计算";
            // 
            // lblValidation
            // 
            this.lblValidation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblValidation.Location = new System.Drawing.Point(12, 354);
            this.lblValidation.Name = "lblValidation";
            this.lblValidation.Size = new System.Drawing.Size(338, 22);
            this.lblValidation.TabIndex = 41;
            this.lblValidation.Text = "表达式为空";
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
            // lblField
            // 
            this.lblField.AutoSize = true;
            this.lblField.Location = new System.Drawing.Point(12, 200);
            this.lblField.Name = "lblField";
            this.lblField.Size = new System.Drawing.Size(71, 12);
            this.lblField.TabIndex = 45;
            this.lblField.Text = "字段名称 = ";
            // 
            // btnMinus
            // 
            this.btnMinus.Location = new System.Drawing.Point(280, 50);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(32, 21);
            this.btnMinus.TabIndex = 50;
            this.btnMinus.Text = "-";
            this.btnMinus.Click += new System.EventHandler(this.btnMinus_Click);
            // 
            // btnMultiply
            // 
            this.btnMultiply.Location = new System.Drawing.Point(280, 103);
            this.btnMultiply.Name = "btnMultiply";
            this.btnMultiply.Size = new System.Drawing.Size(32, 21);
            this.btnMultiply.TabIndex = 56;
            this.btnMultiply.Text = "*";
            this.btnMultiply.Click += new System.EventHandler(this.btnMultiply_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(280, 164);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(70, 21);
            this.btnClear.TabIndex = 64;
            this.btnClear.Text = "清除";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnPlus
            // 
            this.btnPlus.Location = new System.Drawing.Point(280, 23);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(32, 21);
            this.btnPlus.TabIndex = 46;
            this.btnPlus.Text = "+";
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
            // 
            // btnDivide
            // 
            this.btnDivide.Location = new System.Drawing.Point(280, 77);
            this.btnDivide.Name = "btnDivide";
            this.btnDivide.Size = new System.Drawing.Size(32, 21);
            this.btnDivide.TabIndex = 47;
            this.btnDivide.Text = "/";
            this.btnDivide.Click += new System.EventHandler(this.btnDivide_Click);
            // 
            // lblType
            // 
            this.lblType.Location = new System.Drawing.Point(206, 200);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(144, 12);
            this.lblType.TabIndex = 67;
            this.lblType.Text = "类型";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.txtExpression.Location = new System.Drawing.Point(12, 215);
            this.txtExpression.Multiline = true;
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.Size = new System.Drawing.Size(338, 134);
            this.txtExpression.TabIndex = 42;
            this.txtExpression.TextChanged += new System.EventHandler(this.txtExpression_TextChanged);
            // 
            // functionTreeView
            // 
            this.functionTreeView.HideSelection = false;
            this.functionTreeView.Location = new System.Drawing.Point(361, 24);
            this.functionTreeView.Name = "functionTreeView";
            this.functionTreeView.Size = new System.Drawing.Size(206, 325);
            this.functionTreeView.TabIndex = 35;
            this.functionTreeView.Text = "functionsTreeView1";
            this.functionTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.functionTreeView_NodeMouseDoubleClick);
            // 
            // FieldCalculator
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(575, 385);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.fieldListBox);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnMultiply);
            this.Controls.Add(this.btnMinus);
            this.Controls.Add(this.btnDivide);
            this.Controls.Add(this.btnPlus);
            this.Controls.Add(this.lblField);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtExpression);
            this.Controls.Add(this.lblValidation);
            this.Controls.Add(this.functionTreeView);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "FieldCalculator";
            this.Text = "字段计算器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private Controls.FunctionTreeView functionTreeView;
        private System.Windows.Forms.Label lblValidation;
        private ExpressionTextBox txtExpression;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblField;
        private System.Windows.Forms.Button btnMinus;
        private System.Windows.Forms.Button btnMultiply;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnPlus;
        private System.Windows.Forms.Button btnDivide;
        private FieldsListBox fieldListBox;
        private System.Windows.Forms.Label lblType;
    }
}