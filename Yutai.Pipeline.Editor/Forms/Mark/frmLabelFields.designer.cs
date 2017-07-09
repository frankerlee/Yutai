namespace Yutai.Pipeline.Editor.Forms.Mark
{
    partial class FrmLabelFields
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lsbAllFields = new System.Windows.Forms.ListBox();
            this.lsbSelectedFields = new System.Windows.Forms.ListBox();
            this.grbSimgleLabel = new System.Windows.Forms.GroupBox();
            this.grbMultilinelabel = new System.Windows.Forms.GroupBox();
            this.labDelimiter = new System.Windows.Forms.Label();
            this.cobDown = new System.Windows.Forms.ComboBox();
            this.cobRight = new System.Windows.Forms.ComboBox();
            this.cobLeft = new System.Windows.Forms.ComboBox();
            this.cobUp = new System.Windows.Forms.ComboBox();
            this.rdbSinglelabel = new System.Windows.Forms.RadioButton();
            this.rdbMultilinelabel = new System.Windows.Forms.RadioButton();
            this.grbSimgleLabel.SuspendLayout();
            this.grbMultilinelabel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(154, 65);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(44, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = ">";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(154, 122);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(44, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "<";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(350, 122);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(44, 23);
            this.btnDown.TabIndex = 5;
            this.btnDown.Text = "下移";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(350, 65);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(44, 23);
            this.btnUp.TabIndex = 6;
            this.btnUp.Text = "上移";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(259, 437);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(66, 27);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(354, 437);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(66, 27);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lsbAllFields
            // 
            this.lsbAllFields.FormattingEnabled = true;
            this.lsbAllFields.ItemHeight = 12;
            this.lsbAllFields.Location = new System.Drawing.Point(20, 23);
            this.lsbAllFields.Name = "lsbAllFields";
            this.lsbAllFields.Size = new System.Drawing.Size(120, 148);
            this.lsbAllFields.TabIndex = 9;
            this.lsbAllFields.DoubleClick += new System.EventHandler(this.btnAdd_Click);
            // 
            // lsbSelectedFields
            // 
            this.lsbSelectedFields.FormattingEnabled = true;
            this.lsbSelectedFields.ItemHeight = 12;
            this.lsbSelectedFields.Location = new System.Drawing.Point(214, 20);
            this.lsbSelectedFields.Name = "lsbSelectedFields";
            this.lsbSelectedFields.Size = new System.Drawing.Size(120, 148);
            this.lsbSelectedFields.TabIndex = 9;
            this.lsbSelectedFields.DoubleClick += new System.EventHandler(this.btnDelete_Click);
            // 
            // grbSimgleLabel
            // 
            this.grbSimgleLabel.Controls.Add(this.lsbAllFields);
            this.grbSimgleLabel.Controls.Add(this.lsbSelectedFields);
            this.grbSimgleLabel.Controls.Add(this.btnDown);
            this.grbSimgleLabel.Controls.Add(this.btnDelete);
            this.grbSimgleLabel.Controls.Add(this.btnUp);
            this.grbSimgleLabel.Controls.Add(this.btnAdd);
            this.grbSimgleLabel.Location = new System.Drawing.Point(15, 38);
            this.grbSimgleLabel.Name = "grbSimgleLabel";
            this.grbSimgleLabel.Size = new System.Drawing.Size(405, 194);
            this.grbSimgleLabel.TabIndex = 11;
            this.grbSimgleLabel.TabStop = false;
            this.grbSimgleLabel.Text = "单行标注";
            // 
            // grbMultilinelabel
            // 
            this.grbMultilinelabel.Controls.Add(this.labDelimiter);
            this.grbMultilinelabel.Controls.Add(this.cobDown);
            this.grbMultilinelabel.Controls.Add(this.cobRight);
            this.grbMultilinelabel.Controls.Add(this.cobLeft);
            this.grbMultilinelabel.Controls.Add(this.cobUp);
            this.grbMultilinelabel.Location = new System.Drawing.Point(15, 254);
            this.grbMultilinelabel.Name = "grbMultilinelabel";
            this.grbMultilinelabel.Size = new System.Drawing.Size(405, 164);
            this.grbMultilinelabel.TabIndex = 13;
            this.grbMultilinelabel.TabStop = false;
            this.grbMultilinelabel.Text = "多行标注";
            // 
            // labDelimiter
            // 
            this.labDelimiter.AutoSize = true;
            this.labDelimiter.Location = new System.Drawing.Point(144, 83);
            this.labDelimiter.Name = "labDelimiter";
            this.labDelimiter.Size = new System.Drawing.Size(95, 12);
            this.labDelimiter.TabIndex = 4;
            this.labDelimiter.Text = "---------------";
            // 
            // cobDown
            // 
            this.cobDown.FormattingEnabled = true;
            this.cobDown.Location = new System.Drawing.Point(144, 107);
            this.cobDown.Name = "cobDown";
            this.cobDown.Size = new System.Drawing.Size(97, 20);
            this.cobDown.TabIndex = 3;
            // 
            // cobRight
            // 
            this.cobRight.FormattingEnabled = true;
            this.cobRight.Location = new System.Drawing.Point(244, 78);
            this.cobRight.Name = "cobRight";
            this.cobRight.Size = new System.Drawing.Size(99, 20);
            this.cobRight.TabIndex = 2;
            // 
            // cobLeft
            // 
            this.cobLeft.FormattingEnabled = true;
            this.cobLeft.Location = new System.Drawing.Point(42, 78);
            this.cobLeft.Name = "cobLeft";
            this.cobLeft.Size = new System.Drawing.Size(98, 20);
            this.cobLeft.TabIndex = 1;
            // 
            // cobUp
            // 
            this.cobUp.FormattingEnabled = true;
            this.cobUp.Location = new System.Drawing.Point(144, 52);
            this.cobUp.Name = "cobUp";
            this.cobUp.Size = new System.Drawing.Size(97, 20);
            this.cobUp.TabIndex = 0;
            // 
            // rdbSinglelabel
            // 
            this.rdbSinglelabel.AutoSize = true;
            this.rdbSinglelabel.Location = new System.Drawing.Point(52, 13);
            this.rdbSinglelabel.Name = "rdbSinglelabel";
            this.rdbSinglelabel.Size = new System.Drawing.Size(71, 16);
            this.rdbSinglelabel.TabIndex = 14;
            this.rdbSinglelabel.TabStop = true;
            this.rdbSinglelabel.Text = "单行标注";
            this.rdbSinglelabel.UseVisualStyleBackColor = true;
            this.rdbSinglelabel.CheckedChanged += new System.EventHandler(this.rdbSinglelabel_CheckedChanged);
            // 
            // rdbMultilinelabel
            // 
            this.rdbMultilinelabel.AutoSize = true;
            this.rdbMultilinelabel.Location = new System.Drawing.Point(201, 13);
            this.rdbMultilinelabel.Name = "rdbMultilinelabel";
            this.rdbMultilinelabel.Size = new System.Drawing.Size(71, 16);
            this.rdbMultilinelabel.TabIndex = 15;
            this.rdbMultilinelabel.TabStop = true;
            this.rdbMultilinelabel.Text = "多行标注";
            this.rdbMultilinelabel.UseVisualStyleBackColor = true;
            this.rdbMultilinelabel.CheckedChanged += new System.EventHandler(this.rdbMultilinelabel_CheckedChanged);
            // 
            // frmLabelFields
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 472);
            this.Controls.Add(this.rdbMultilinelabel);
            this.Controls.Add(this.rdbSinglelabel);
            this.Controls.Add(this.grbMultilinelabel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.grbSimgleLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLabelFields";
            this.Text = "选择标注字段";
            this.grbSimgleLabel.ResumeLayout(false);
            this.grbMultilinelabel.ResumeLayout(false);
            this.grbMultilinelabel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListBox lsbAllFields;
        private System.Windows.Forms.ListBox lsbSelectedFields;
        private System.Windows.Forms.GroupBox grbSimgleLabel;
        private System.Windows.Forms.GroupBox grbMultilinelabel;
        private System.Windows.Forms.Label labDelimiter;
        private System.Windows.Forms.ComboBox cobDown;
        private System.Windows.Forms.ComboBox cobRight;
        private System.Windows.Forms.ComboBox cobLeft;
        private System.Windows.Forms.ComboBox cobUp;
        private System.Windows.Forms.RadioButton rdbSinglelabel;
        private System.Windows.Forms.RadioButton rdbMultilinelabel;

    }
}