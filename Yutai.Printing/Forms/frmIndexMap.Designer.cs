namespace Yutai.Plugins.Printing.Forms
{
    partial class frmIndexMap
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
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtLayer = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbTemplate = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbNameField = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chkQueryFields = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.btnLayer = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLayer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTemplate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNameField.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkQueryFields)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(83, 16);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(185, 20);
            this.txtName.TabIndex = 1;
            // 
            // txtLayer
            // 
            this.txtLayer.Location = new System.Drawing.Point(83, 44);
            this.txtLayer.Name = "txtLayer";
            this.txtLayer.Size = new System.Drawing.Size(185, 20);
            this.txtLayer.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "索引图层：";
            // 
            // cmbTemplate
            // 
            this.cmbTemplate.Location = new System.Drawing.Point(83, 71);
            this.cmbTemplate.Name = "cmbTemplate";
            this.cmbTemplate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTemplate.Size = new System.Drawing.Size(185, 20);
            this.cmbTemplate.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "制图模板：";
            // 
            // cmbNameField
            // 
            this.cmbNameField.Location = new System.Drawing.Point(83, 97);
            this.cmbNameField.Name = "cmbNameField";
            this.cmbNameField.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbNameField.Size = new System.Drawing.Size(185, 20);
            this.cmbNameField.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "名称字段：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "查询字段：";
            // 
            // chkQueryFields
            // 
            this.chkQueryFields.Location = new System.Drawing.Point(83, 124);
            this.chkQueryFields.Name = "chkQueryFields";
            this.chkQueryFields.Size = new System.Drawing.Size(184, 163);
            this.chkQueryFields.TabIndex = 9;
            // 
            // btnLayer
            // 
            this.btnLayer.Location = new System.Drawing.Point(274, 41);
            this.btnLayer.Name = "btnLayer";
            this.btnLayer.Size = new System.Drawing.Size(75, 26);
            this.btnLayer.TabIndex = 10;
            this.btnLayer.Text = "选择图层";
            this.btnLayer.Click += new System.EventHandler(this.btnLayer_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(273, 194);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 29);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(274, 259);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 26);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "取消";
            // 
            // frmIndexMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 297);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnLayer);
            this.Controls.Add(this.chkQueryFields);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbNameField);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbTemplate);
            this.Controls.Add(this.txtLayer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmIndexMap";
            this.Text = "索引图配置";
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLayer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTemplate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNameField.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkQueryFields)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtLayer;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.ComboBoxEdit cmbTemplate;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.ComboBoxEdit cmbNameField;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.CheckedListBoxControl chkQueryFields;
        private DevExpress.XtraEditors.SimpleButton btnLayer;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnClose;
    }
}