namespace Yutai.Pipeline.Editor.Forms.Exchange
{
    partial class FrmImportExcelData
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
            this.txtExcelPath = new System.Windows.Forms.TextBox();
            this.btnSelectExcel = new System.Windows.Forms.Button();
            this.cmbPointSheet = new System.Windows.Forms.ComboBox();
            this.cmbLineSheet = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.checkBoxConvertCoordinateSystem = new System.Windows.Forms.CheckBox();
            this.cmbEndNoField = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbStartNoField = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbZField = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbYField = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbXField = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbNoField = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnImport = new System.Windows.Forms.Button();
            this.checkBoxIsGroup = new System.Windows.Forms.CheckBox();
            this.cmbPointGroupField = new System.Windows.Forms.ComboBox();
            this.cmbLineGroupField = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtGDBPath = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSelectGDB = new System.Windows.Forms.Button();
            this.chkHasCoordInfo = new System.Windows.Forms.CheckBox();
            this.panelSurvey = new System.Windows.Forms.Panel();
            this.cmbSurveyZField = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbSurveyYField = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbSurveyXField = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cmbSurveyNoField = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.cmbSurveySheet = new System.Windows.Forms.ComboBox();
            this.panelSurvey.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Excel位置：";
            // 
            // txtExcelPath
            // 
            this.txtExcelPath.Location = new System.Drawing.Point(89, 6);
            this.txtExcelPath.Name = "txtExcelPath";
            this.txtExcelPath.Size = new System.Drawing.Size(577, 21);
            this.txtExcelPath.TabIndex = 1;
            // 
            // btnSelectExcel
            // 
            this.btnSelectExcel.Location = new System.Drawing.Point(672, 5);
            this.btnSelectExcel.Name = "btnSelectExcel";
            this.btnSelectExcel.Size = new System.Drawing.Size(75, 23);
            this.btnSelectExcel.TabIndex = 2;
            this.btnSelectExcel.Text = "选择";
            this.btnSelectExcel.UseVisualStyleBackColor = true;
            this.btnSelectExcel.Click += new System.EventHandler(this.btnSelectExcel_Click);
            // 
            // cmbPointSheet
            // 
            this.cmbPointSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPointSheet.FormattingEnabled = true;
            this.cmbPointSheet.Location = new System.Drawing.Point(89, 33);
            this.cmbPointSheet.Name = "cmbPointSheet";
            this.cmbPointSheet.Size = new System.Drawing.Size(121, 20);
            this.cmbPointSheet.TabIndex = 3;
            this.cmbPointSheet.SelectedIndexChanged += new System.EventHandler(this.cmbPointSheet_SelectedIndexChanged);
            // 
            // cmbLineSheet
            // 
            this.cmbLineSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLineSheet.FormattingEnabled = true;
            this.cmbLineSheet.Location = new System.Drawing.Point(626, 33);
            this.cmbLineSheet.Name = "cmbLineSheet";
            this.cmbLineSheet.Size = new System.Drawing.Size(121, 20);
            this.cmbLineSheet.TabIndex = 3;
            this.cmbLineSheet.SelectedIndexChanged += new System.EventHandler(this.cmbLineSheet_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "点表：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(579, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "线表：";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Excel文件|*.xlsx";
            // 
            // checkBoxConvertCoordinateSystem
            // 
            this.checkBoxConvertCoordinateSystem.AutoSize = true;
            this.checkBoxConvertCoordinateSystem.Location = new System.Drawing.Point(89, 137);
            this.checkBoxConvertCoordinateSystem.Name = "checkBoxConvertCoordinateSystem";
            this.checkBoxConvertCoordinateSystem.Size = new System.Drawing.Size(138, 16);
            this.checkBoxConvertCoordinateSystem.TabIndex = 29;
            this.checkBoxConvertCoordinateSystem.Text = "变换测量/数学坐标系";
            this.checkBoxConvertCoordinateSystem.UseVisualStyleBackColor = true;
            // 
            // cmbEndNoField
            // 
            this.cmbEndNoField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEndNoField.FormattingEnabled = true;
            this.cmbEndNoField.Location = new System.Drawing.Point(626, 85);
            this.cmbEndNoField.Name = "cmbEndNoField";
            this.cmbEndNoField.Size = new System.Drawing.Size(121, 20);
            this.cmbEndNoField.TabIndex = 28;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(555, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 20;
            this.label6.Text = "终点点号：";
            // 
            // cmbStartNoField
            // 
            this.cmbStartNoField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStartNoField.FormattingEnabled = true;
            this.cmbStartNoField.Location = new System.Drawing.Point(626, 59);
            this.cmbStartNoField.Name = "cmbStartNoField";
            this.cmbStartNoField.Size = new System.Drawing.Size(121, 20);
            this.cmbStartNoField.TabIndex = 27;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(555, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 18;
            this.label5.Text = "起点点号：";
            // 
            // cmbZField
            // 
            this.cmbZField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbZField.FormattingEnabled = true;
            this.cmbZField.Location = new System.Drawing.Point(89, 159);
            this.cmbZField.Name = "cmbZField";
            this.cmbZField.Size = new System.Drawing.Size(121, 20);
            this.cmbZField.TabIndex = 23;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(18, 162);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 14;
            this.label12.Text = "地面高程：";
            // 
            // cmbYField
            // 
            this.cmbYField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYField.FormattingEnabled = true;
            this.cmbYField.Location = new System.Drawing.Point(89, 111);
            this.cmbYField.Name = "cmbYField";
            this.cmbYField.Size = new System.Drawing.Size(121, 20);
            this.cmbYField.TabIndex = 24;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(36, 114);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 12);
            this.label11.TabIndex = 17;
            this.label11.Text = "Y坐标：";
            // 
            // cmbXField
            // 
            this.cmbXField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbXField.FormattingEnabled = true;
            this.cmbXField.Location = new System.Drawing.Point(89, 85);
            this.cmbXField.Name = "cmbXField";
            this.cmbXField.Size = new System.Drawing.Size(121, 20);
            this.cmbXField.TabIndex = 25;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(36, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 12);
            this.label10.TabIndex = 16;
            this.label10.Text = "X坐标：";
            // 
            // cmbNoField
            // 
            this.cmbNoField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNoField.FormattingEnabled = true;
            this.cmbNoField.Location = new System.Drawing.Point(89, 59);
            this.cmbNoField.Name = "cmbNoField";
            this.cmbNoField.Size = new System.Drawing.Size(121, 20);
            this.cmbNoField.TabIndex = 26;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "管点编号：";
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(672, 184);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 31;
            this.btnImport.Text = "导入";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // checkBoxIsGroup
            // 
            this.checkBoxIsGroup.AutoSize = true;
            this.checkBoxIsGroup.Checked = true;
            this.checkBoxIsGroup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIsGroup.Location = new System.Drawing.Point(626, 113);
            this.checkBoxIsGroup.Name = "checkBoxIsGroup";
            this.checkBoxIsGroup.Size = new System.Drawing.Size(72, 16);
            this.checkBoxIsGroup.TabIndex = 32;
            this.checkBoxIsGroup.Text = "是否分类";
            this.checkBoxIsGroup.UseVisualStyleBackColor = true;
            this.checkBoxIsGroup.CheckedChanged += new System.EventHandler(this.checkBoxIsGroup_CheckedChanged);
            // 
            // cmbPointGroupField
            // 
            this.cmbPointGroupField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPointGroupField.FormattingEnabled = true;
            this.cmbPointGroupField.Location = new System.Drawing.Point(626, 133);
            this.cmbPointGroupField.Name = "cmbPointGroupField";
            this.cmbPointGroupField.Size = new System.Drawing.Size(121, 20);
            this.cmbPointGroupField.TabIndex = 33;
            // 
            // cmbLineGroupField
            // 
            this.cmbLineGroupField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLineGroupField.FormattingEnabled = true;
            this.cmbLineGroupField.Location = new System.Drawing.Point(626, 159);
            this.cmbLineGroupField.Name = "cmbLineGroupField";
            this.cmbLineGroupField.Size = new System.Drawing.Size(121, 20);
            this.cmbLineGroupField.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(531, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "点表分类字段：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(531, 162);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 12);
            this.label8.TabIndex = 20;
            this.label8.Text = "线表分类字段：";
            // 
            // txtGDBPath
            // 
            this.txtGDBPath.Location = new System.Drawing.Point(89, 185);
            this.txtGDBPath.Name = "txtGDBPath";
            this.txtGDBPath.Size = new System.Drawing.Size(496, 21);
            this.txtGDBPath.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 189);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "导入位置：";
            // 
            // btnSelectGDB
            // 
            this.btnSelectGDB.Location = new System.Drawing.Point(591, 184);
            this.btnSelectGDB.Name = "btnSelectGDB";
            this.btnSelectGDB.Size = new System.Drawing.Size(75, 23);
            this.btnSelectGDB.TabIndex = 34;
            this.btnSelectGDB.Text = "选择";
            this.btnSelectGDB.UseVisualStyleBackColor = true;
            this.btnSelectGDB.Click += new System.EventHandler(this.btnSelectGDB_Click);
            // 
            // chkHasCoordInfo
            // 
            this.chkHasCoordInfo.AutoSize = true;
            this.chkHasCoordInfo.Checked = true;
            this.chkHasCoordInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHasCoordInfo.Location = new System.Drawing.Point(216, 35);
            this.chkHasCoordInfo.Name = "chkHasCoordInfo";
            this.chkHasCoordInfo.Size = new System.Drawing.Size(72, 16);
            this.chkHasCoordInfo.TabIndex = 35;
            this.chkHasCoordInfo.Text = "包含坐标";
            this.chkHasCoordInfo.UseVisualStyleBackColor = true;
            this.chkHasCoordInfo.CheckedChanged += new System.EventHandler(this.chkHasCoordInfo_CheckedChanged);
            // 
            // panelSurvey
            // 
            this.panelSurvey.Controls.Add(this.cmbSurveyZField);
            this.panelSurvey.Controls.Add(this.label13);
            this.panelSurvey.Controls.Add(this.cmbSurveyYField);
            this.panelSurvey.Controls.Add(this.label14);
            this.panelSurvey.Controls.Add(this.cmbSurveyXField);
            this.panelSurvey.Controls.Add(this.label15);
            this.panelSurvey.Controls.Add(this.cmbSurveyNoField);
            this.panelSurvey.Controls.Add(this.label16);
            this.panelSurvey.Controls.Add(this.label17);
            this.panelSurvey.Controls.Add(this.cmbSurveySheet);
            this.panelSurvey.Enabled = false;
            this.panelSurvey.Location = new System.Drawing.Point(284, 30);
            this.panelSurvey.Name = "panelSurvey";
            this.panelSurvey.Size = new System.Drawing.Size(217, 146);
            this.panelSurvey.TabIndex = 36;
            // 
            // cmbSurveyZField
            // 
            this.cmbSurveyZField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSurveyZField.FormattingEnabled = true;
            this.cmbSurveyZField.Location = new System.Drawing.Point(76, 107);
            this.cmbSurveyZField.Name = "cmbSurveyZField";
            this.cmbSurveyZField.Size = new System.Drawing.Size(121, 20);
            this.cmbSurveyZField.TabIndex = 33;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(23, 110);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 12);
            this.label13.TabIndex = 29;
            this.label13.Text = "Z坐标：";
            // 
            // cmbSurveyYField
            // 
            this.cmbSurveyYField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSurveyYField.FormattingEnabled = true;
            this.cmbSurveyYField.Location = new System.Drawing.Point(76, 81);
            this.cmbSurveyYField.Name = "cmbSurveyYField";
            this.cmbSurveyYField.Size = new System.Drawing.Size(121, 20);
            this.cmbSurveyYField.TabIndex = 34;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(23, 84);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(47, 12);
            this.label14.TabIndex = 32;
            this.label14.Text = "Y坐标：";
            // 
            // cmbSurveyXField
            // 
            this.cmbSurveyXField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSurveyXField.FormattingEnabled = true;
            this.cmbSurveyXField.Location = new System.Drawing.Point(76, 55);
            this.cmbSurveyXField.Name = "cmbSurveyXField";
            this.cmbSurveyXField.Size = new System.Drawing.Size(121, 20);
            this.cmbSurveyXField.TabIndex = 35;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(23, 58);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(47, 12);
            this.label15.TabIndex = 31;
            this.label15.Text = "X坐标：";
            // 
            // cmbSurveyNoField
            // 
            this.cmbSurveyNoField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSurveyNoField.FormattingEnabled = true;
            this.cmbSurveyNoField.Location = new System.Drawing.Point(76, 29);
            this.cmbSurveyNoField.Name = "cmbSurveyNoField";
            this.cmbSurveyNoField.Size = new System.Drawing.Size(121, 20);
            this.cmbSurveyNoField.TabIndex = 36;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(5, 32);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 12);
            this.label16.TabIndex = 30;
            this.label16.Text = "管点编号：";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(17, 6);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 12);
            this.label17.TabIndex = 28;
            this.label17.Text = "测量表：";
            // 
            // cmbSurveySheet
            // 
            this.cmbSurveySheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSurveySheet.FormattingEnabled = true;
            this.cmbSurveySheet.Location = new System.Drawing.Point(76, 3);
            this.cmbSurveySheet.Name = "cmbSurveySheet";
            this.cmbSurveySheet.Size = new System.Drawing.Size(121, 20);
            this.cmbSurveySheet.TabIndex = 27;
            this.cmbSurveySheet.SelectedIndexChanged += new System.EventHandler(this.cmbSurveySheet_SelectedIndexChanged);
            // 
            // frmImportExcelData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 215);
            this.Controls.Add(this.panelSurvey);
            this.Controls.Add(this.chkHasCoordInfo);
            this.Controls.Add(this.btnSelectGDB);
            this.Controls.Add(this.cmbLineGroupField);
            this.Controls.Add(this.cmbPointGroupField);
            this.Controls.Add(this.checkBoxIsGroup);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.checkBoxConvertCoordinateSystem);
            this.Controls.Add(this.cmbEndNoField);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbStartNoField);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbZField);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cmbYField);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmbXField);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cmbNoField);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbLineSheet);
            this.Controls.Add(this.cmbPointSheet);
            this.Controls.Add(this.btnSelectExcel);
            this.Controls.Add(this.txtGDBPath);
            this.Controls.Add(this.txtExcelPath);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label1);
            this.Name = "frmImportExcelData";
            this.Text = "导入Excel数据";
            this.panelSurvey.ResumeLayout(false);
            this.panelSurvey.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtExcelPath;
        private System.Windows.Forms.Button btnSelectExcel;
        private System.Windows.Forms.ComboBox cmbPointSheet;
        private System.Windows.Forms.ComboBox cmbLineSheet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox checkBoxConvertCoordinateSystem;
        private System.Windows.Forms.ComboBox cmbEndNoField;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbStartNoField;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbZField;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbYField;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbXField;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbNoField;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.CheckBox checkBoxIsGroup;
        private System.Windows.Forms.ComboBox cmbPointGroupField;
        private System.Windows.Forms.ComboBox cmbLineGroupField;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtGDBPath;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSelectGDB;
        private System.Windows.Forms.CheckBox chkHasCoordInfo;
        private System.Windows.Forms.Panel panelSurvey;
        private System.Windows.Forms.ComboBox cmbSurveyZField;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbSurveyYField;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmbSurveyXField;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmbSurveyNoField;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cmbSurveySheet;
    }
}