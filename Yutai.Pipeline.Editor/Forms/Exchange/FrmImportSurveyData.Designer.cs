namespace Yutai.Pipeline.Editor.Forms.Exchange
{
    partial class FrmImportSurveyData
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
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCoordType = new System.Windows.Forms.ComboBox();
            this.txtBaseName = new System.Windows.Forms.TextBox();
            this.txtSurveyer = new System.Windows.Forms.TextBox();
            this.dateSurvey = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.ucSelectGDBFile = new Yutai.Pipeline.Editor.Controls.UcSelectFile();
            this.ucSelectSurveyFile = new Yutai.Pipeline.Editor.Controls.UcSelectFile();
            this.ucSelectExcelFile = new Yutai.Pipeline.Editor.Controls.UcSelectFile();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Excel文件";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "测量文件";
            // 
            // cmbCoordType
            // 
            this.cmbCoordType.FormattingEnabled = true;
            this.cmbCoordType.Items.AddRange(new object[] {
            "点号,X,Y",
            "点号,Y,X",
            "点号,编码,X,Y",
            "点号,编码,Y,X",
            "点号,X,Y,Z",
            "点号,Y,X,Z",
            "点号,编码,X,Y,Z",
            "点号,编码,Y,X,Z"});
            this.cmbCoordType.Location = new System.Drawing.Point(77, 68);
            this.cmbCoordType.Name = "cmbCoordType";
            this.cmbCoordType.Size = new System.Drawing.Size(527, 20);
            this.cmbCoordType.TabIndex = 2;
            // 
            // txtBaseName
            // 
            this.txtBaseName.Location = new System.Drawing.Point(77, 122);
            this.txtBaseName.Name = "txtBaseName";
            this.txtBaseName.Size = new System.Drawing.Size(527, 21);
            this.txtBaseName.TabIndex = 3;
            // 
            // txtSurveyer
            // 
            this.txtSurveyer.Location = new System.Drawing.Point(77, 149);
            this.txtSurveyer.Name = "txtSurveyer";
            this.txtSurveyer.Size = new System.Drawing.Size(527, 21);
            this.txtSurveyer.TabIndex = 3;
            // 
            // dateSurvey
            // 
            this.dateSurvey.Location = new System.Drawing.Point(77, 176);
            this.dateSurvey.Name = "dateSurvey";
            this.dateSurvey.Size = new System.Drawing.Size(200, 21);
            this.dateSurvey.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "坐标类型";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "  数据库";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "基本名称";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "  测量员";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 180);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "测量时间";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(529, 211);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 41);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(448, 211);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 41);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ucSelectGDBFile
            // 
            this.ucSelectGDBFile.Filter = "地理数据库|*.mdb;*.gdb";
            this.ucSelectGDBFile.Location = new System.Drawing.Point(77, 94);
            this.ucSelectGDBFile.Name = "ucSelectGDBFile";
            this.ucSelectGDBFile.Size = new System.Drawing.Size(527, 22);
            this.ucSelectGDBFile.TabIndex = 0;
            // 
            // ucSelectSurveyFile
            // 
            this.ucSelectSurveyFile.Filter = "所有文件|*.*";
            this.ucSelectSurveyFile.Location = new System.Drawing.Point(77, 40);
            this.ucSelectSurveyFile.Name = "ucSelectSurveyFile";
            this.ucSelectSurveyFile.Size = new System.Drawing.Size(527, 22);
            this.ucSelectSurveyFile.TabIndex = 0;
            // 
            // ucSelectExcelFile
            // 
            this.ucSelectExcelFile.Filter = "Excel文件|*.xlsx;*.xls";
            this.ucSelectExcelFile.Location = new System.Drawing.Point(77, 12);
            this.ucSelectExcelFile.Name = "ucSelectExcelFile";
            this.ucSelectExcelFile.Size = new System.Drawing.Size(527, 22);
            this.ucSelectExcelFile.TabIndex = 0;
            // 
            // FrmImportSurveyData
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(616, 261);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dateSurvey);
            this.Controls.Add(this.txtSurveyer);
            this.Controls.Add(this.txtBaseName);
            this.Controls.Add(this.cmbCoordType);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucSelectGDBFile);
            this.Controls.Add(this.ucSelectSurveyFile);
            this.Controls.Add(this.ucSelectExcelFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmImportSurveyData";
            this.Text = "挂接测量文件";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.UcSelectFile ucSelectExcelFile;
        private System.Windows.Forms.Label label1;
        private Controls.UcSelectFile ucSelectSurveyFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCoordType;
        private Controls.UcSelectFile ucSelectGDBFile;
        private System.Windows.Forms.TextBox txtBaseName;
        private System.Windows.Forms.TextBox txtSurveyer;
        private System.Windows.Forms.DateTimePicker dateSurvey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}