namespace Yutai.Plugins.Template.Forms
{
    partial class frmCreateFCByTemplate
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
            this.wizardControl1 = new DevExpress.XtraWizard.WizardControl();
            this.welcomeWizardPage1 = new DevExpress.XtraWizard.WelcomeWizardPage();
            this.wizardPage1 = new DevExpress.XtraWizard.WizardPage();
            this.lstTemplate = new System.Windows.Forms.ListBox();
            this.completionWizardPage1 = new DevExpress.XtraWizard.CompletionWizardPage();
            this.wizardPage2 = new DevExpress.XtraWizard.WizardPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExtent = new System.Windows.Forms.Button();
            this.btnSpatialRef = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtXYCoordinate = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtFCName = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDB = new DevExpress.XtraEditors.SimpleButton();
            this.txtDB = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMinX = new DevExpress.XtraEditors.TextEdit();
            this.txtMaxY = new DevExpress.XtraEditors.TextEdit();
            this.txtMaxX = new DevExpress.XtraEditors.TextEdit();
            this.txtMinY = new DevExpress.XtraEditors.TextEdit();
            this.btnImportFromFC = new System.Windows.Forms.Button();
            this.wizardPage3 = new DevExpress.XtraWizard.WizardPage();
            this.checkedListBoxControl1 = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.btnAddField = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.wizardControl1.SuspendLayout();
            this.wizardPage1.SuspendLayout();
            this.wizardPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtXYCoordinate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFCName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinY.Properties)).BeginInit();
            this.wizardPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.CancelText = "取消";
            this.wizardControl1.Controls.Add(this.welcomeWizardPage1);
            this.wizardControl1.Controls.Add(this.wizardPage1);
            this.wizardControl1.Controls.Add(this.completionWizardPage1);
            this.wizardControl1.Controls.Add(this.wizardPage2);
            this.wizardControl1.Controls.Add(this.wizardPage3);
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.FinishText = "完成";
            this.wizardControl1.HelpText = "帮助";
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.NextText = "下一步";
            this.wizardControl1.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] {
            this.welcomeWizardPage1,
            this.wizardPage1,
            this.wizardPage2,
            this.wizardPage3,
            this.completionWizardPage1});
            this.wizardControl1.PreviousText = "上一步";
            this.wizardControl1.Size = new System.Drawing.Size(530, 438);
            // 
            // welcomeWizardPage1
            // 
            this.welcomeWizardPage1.IntroductionText = "该功能将指导依据系统现有的模板创建要素类";
            this.welcomeWizardPage1.Name = "welcomeWizardPage1";
            this.welcomeWizardPage1.ProceedText = "点击下一步继续";
            this.welcomeWizardPage1.Size = new System.Drawing.Size(313, 305);
            this.welcomeWizardPage1.Text = "欢迎使用本功能";
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.lstTemplate);
            this.wizardPage1.DescriptionText = "选择模板将规定了要素类的属性字段";
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Size = new System.Drawing.Size(498, 293);
            this.wizardPage1.Text = "选择模板";
            // 
            // lstTemplate
            // 
            this.lstTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstTemplate.FormattingEnabled = true;
            this.lstTemplate.ItemHeight = 12;
            this.lstTemplate.Location = new System.Drawing.Point(0, 0);
            this.lstTemplate.Name = "lstTemplate";
            this.lstTemplate.Size = new System.Drawing.Size(498, 293);
            this.lstTemplate.TabIndex = 0;
            // 
            // completionWizardPage1
            // 
            this.completionWizardPage1.Name = "completionWizardPage1";
            this.completionWizardPage1.Size = new System.Drawing.Size(313, 305);
            // 
            // wizardPage2
            // 
            this.wizardPage2.Controls.Add(this.groupBox1);
            this.wizardPage2.Controls.Add(this.txtFCName);
            this.wizardPage2.Controls.Add(this.label2);
            this.wizardPage2.Controls.Add(this.btnDB);
            this.wizardPage2.Controls.Add(this.txtDB);
            this.wizardPage2.Controls.Add(this.label1);
            this.wizardPage2.DescriptionText = "请填写创建需要的参数";
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.Size = new System.Drawing.Size(498, 293);
            this.wizardPage2.Text = "保存位置和坐标";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnImportFromFC);
            this.groupBox1.Controls.Add(this.txtMinY);
            this.groupBox1.Controls.Add(this.txtMaxX);
            this.groupBox1.Controls.Add(this.txtMaxY);
            this.groupBox1.Controls.Add(this.txtMinX);
            this.groupBox1.Controls.Add(this.btnExtent);
            this.groupBox1.Controls.Add(this.btnSpatialRef);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtXYCoordinate);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Location = new System.Drawing.Point(21, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(456, 204);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "坐标系";
            // 
            // btnExtent
            // 
            this.btnExtent.Location = new System.Drawing.Point(365, 167);
            this.btnExtent.Name = "btnExtent";
            this.btnExtent.Size = new System.Drawing.Size(75, 23);
            this.btnExtent.TabIndex = 31;
            this.btnExtent.Text = "视图范围";
            this.btnExtent.UseVisualStyleBackColor = true;
            // 
            // btnSpatialRef
            // 
            this.btnSpatialRef.Location = new System.Drawing.Point(366, 46);
            this.btnSpatialRef.Name = "btnSpatialRef";
            this.btnSpatialRef.Size = new System.Drawing.Size(74, 22);
            this.btnSpatialRef.TabIndex = 4;
            this.btnSpatialRef.Text = "选择";
            this.btnSpatialRef.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(206, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 30;
            this.label4.Text = "最大横坐标";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(123, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 28;
            this.label5.Text = "最小横坐标";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(168, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 26;
            this.label6.Text = "最小纵坐标";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(168, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 24;
            this.label7.Text = "最大纵坐标";
            // 
            // txtXYCoordinate
            // 
            this.txtXYCoordinate.EditValue = "";
            this.txtXYCoordinate.Location = new System.Drawing.Point(17, 46);
            this.txtXYCoordinate.Name = "txtXYCoordinate";
            this.txtXYCoordinate.Properties.ReadOnly = true;
            this.txtXYCoordinate.Size = new System.Drawing.Size(340, 20);
            this.txtXYCoordinate.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(17, 20);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(216, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "请选择将用于该索引图要素类的坐标系。";
            // 
            // txtFCName
            // 
            this.txtFCName.Location = new System.Drawing.Point(78, 41);
            this.txtFCName.Name = "txtFCName";
            this.txtFCName.Size = new System.Drawing.Size(291, 20);
            this.txtFCName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "名称";
            // 
            // btnDB
            // 
            this.btnDB.Location = new System.Drawing.Point(386, 14);
            this.btnDB.Name = "btnDB";
            this.btnDB.Size = new System.Drawing.Size(75, 23);
            this.btnDB.TabIndex = 2;
            this.btnDB.Text = "选择";
            // 
            // txtDB
            // 
            this.txtDB.Location = new System.Drawing.Point(78, 12);
            this.txtDB.Name = "txtDB";
            this.txtDB.Size = new System.Drawing.Size(291, 20);
            this.txtDB.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "保存位置";
            // 
            // txtMinX
            // 
            this.txtMinX.Location = new System.Drawing.Point(6, 123);
            this.txtMinX.Name = "txtMinX";
            this.txtMinX.Properties.DisplayFormat.FormatString = "d4";
            this.txtMinX.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtMinX.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtMinX.Size = new System.Drawing.Size(111, 20);
            this.txtMinX.TabIndex = 33;
            // 
            // txtMaxY
            // 
            this.txtMaxY.Location = new System.Drawing.Point(147, 82);
            this.txtMaxY.Name = "txtMaxY";
            this.txtMaxY.Properties.DisplayFormat.FormatString = "d4";
            this.txtMaxY.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtMaxY.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtMaxY.Size = new System.Drawing.Size(111, 20);
            this.txtMaxY.TabIndex = 34;
            // 
            // txtMaxX
            // 
            this.txtMaxX.Location = new System.Drawing.Point(277, 121);
            this.txtMaxX.Name = "txtMaxX";
            this.txtMaxX.Properties.DisplayFormat.FormatString = "d4";
            this.txtMaxX.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtMaxX.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtMaxX.Size = new System.Drawing.Size(111, 20);
            this.txtMaxX.TabIndex = 35;
            // 
            // txtMinY
            // 
            this.txtMinY.Location = new System.Drawing.Point(147, 164);
            this.txtMinY.Name = "txtMinY";
            this.txtMinY.Properties.DisplayFormat.FormatString = "d4";
            this.txtMinY.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtMinY.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtMinY.Size = new System.Drawing.Size(111, 20);
            this.txtMinY.TabIndex = 36;
            // 
            // btnImportFromFC
            // 
            this.btnImportFromFC.Location = new System.Drawing.Point(332, 12);
            this.btnImportFromFC.Name = "btnImportFromFC";
            this.btnImportFromFC.Size = new System.Drawing.Size(107, 22);
            this.btnImportFromFC.TabIndex = 37;
            this.btnImportFromFC.Text = "从要素类导入";
            this.btnImportFromFC.UseVisualStyleBackColor = true;
            // 
            // wizardPage3
            // 
            this.wizardPage3.Controls.Add(this.btnAddField);
            this.wizardPage3.Controls.Add(this.checkedListBoxControl1);
            this.wizardPage3.DescriptionText = "你可以选择不需要的字段";
            this.wizardPage3.Name = "wizardPage3";
            this.wizardPage3.Size = new System.Drawing.Size(498, 293);
            this.wizardPage3.Text = "字段设置";
            // 
            // checkedListBoxControl1
            // 
            this.checkedListBoxControl1.Location = new System.Drawing.Point(13, 15);
            this.checkedListBoxControl1.Name = "checkedListBoxControl1";
            this.checkedListBoxControl1.Size = new System.Drawing.Size(386, 261);
            this.checkedListBoxControl1.TabIndex = 0;
            // 
            // btnAddField
            // 
            this.btnAddField.Location = new System.Drawing.Point(405, 15);
            this.btnAddField.Name = "btnAddField";
            this.btnAddField.Size = new System.Drawing.Size(75, 23);
            this.btnAddField.TabIndex = 1;
            this.btnAddField.Text = "增加";
            // 
            // frmCreateFCByTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 438);
            this.Controls.Add(this.wizardControl1);
            this.Name = "frmCreateFCByTemplate";
            this.Text = "依据模板创建要素类";
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
            this.wizardControl1.ResumeLayout(false);
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage2.ResumeLayout(false);
            this.wizardPage2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtXYCoordinate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFCName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinY.Properties)).EndInit();
            this.wizardPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraWizard.WizardControl wizardControl1;
        private DevExpress.XtraWizard.WelcomeWizardPage welcomeWizardPage1;
        private DevExpress.XtraWizard.WizardPage wizardPage1;
        private System.Windows.Forms.ListBox lstTemplate;
        private DevExpress.XtraWizard.CompletionWizardPage completionWizardPage1;
        private DevExpress.XtraWizard.WizardPage wizardPage2;
        private DevExpress.XtraEditors.TextEdit txtFCName;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton btnDB;
        private DevExpress.XtraEditors.TextEdit txtDB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnExtent;
        private System.Windows.Forms.Button btnSpatialRef;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.ButtonEdit txtXYCoordinate;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.Button btnImportFromFC;
        private DevExpress.XtraEditors.TextEdit txtMinY;
        private DevExpress.XtraEditors.TextEdit txtMaxX;
        private DevExpress.XtraEditors.TextEdit txtMaxY;
        private DevExpress.XtraEditors.TextEdit txtMinX;
        private DevExpress.XtraWizard.WizardPage wizardPage3;
        private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControl1;
        private DevExpress.XtraEditors.SimpleButton btnAddField;
    }
}