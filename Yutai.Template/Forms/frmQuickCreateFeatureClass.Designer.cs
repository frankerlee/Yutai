namespace Yutai.Plugins.Template.Forms
{
    partial class frmQuickCreateFeatureClass
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
            this.txtDB = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTemplate = new System.Windows.Forms.ComboBox();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.btnDB = new DevExpress.XtraEditors.SimpleButton();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSpatialRef = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.txtXMin = new DevExpress.XtraEditors.TextEdit();
            this.txtYMin = new DevExpress.XtraEditors.TextEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.txtXMax = new DevExpress.XtraEditors.TextEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.txtYMax = new DevExpress.XtraEditors.TextEdit();
            this.label8 = new System.Windows.Forms.Label();
            this.btnFullExtent = new DevExpress.XtraEditors.SimpleButton();
            this.btnCurrentExtent = new DevExpress.XtraEditors.SimpleButton();
            this.chkIndex = new DevExpress.XtraEditors.CheckEdit();
            this.grpIndex = new DevExpress.XtraEditors.GroupControl();
            this.label9 = new System.Windows.Forms.Label();
            this.txtWidth = new DevExpress.XtraEditors.TextEdit();
            this.txtHeight = new DevExpress.XtraEditors.TextEdit();
            this.label10 = new System.Windows.Forms.Label();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtDB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpatialRef.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtXMin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYMin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtXMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIndex.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpIndex)).BeginInit();
            this.grpIndex.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtWidth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHeight.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "保存位置";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "名    称";
            // 
            // txtDB
            // 
            this.txtDB.Enabled = false;
            this.txtDB.Location = new System.Drawing.Point(85, 17);
            this.txtDB.Name = "txtDB";
            this.txtDB.Size = new System.Drawing.Size(286, 20);
            this.txtDB.TabIndex = 2;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(85, 48);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(153, 20);
            this.txtName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(248, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "模    板";
            // 
            // cmbTemplate
            // 
            this.cmbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTemplate.FormattingEnabled = true;
            this.cmbTemplate.Location = new System.Drawing.Point(307, 48);
            this.cmbTemplate.Name = "cmbTemplate";
            this.cmbTemplate.Size = new System.Drawing.Size(151, 20);
            this.cmbTemplate.TabIndex = 5;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(19, 74);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(439, 250);
            this.xtraTabControl1.TabIndex = 6;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.btnCurrentExtent);
            this.xtraTabPage1.Controls.Add(this.btnFullExtent);
            this.xtraTabPage1.Controls.Add(this.txtYMax);
            this.xtraTabPage1.Controls.Add(this.label8);
            this.xtraTabPage1.Controls.Add(this.txtXMax);
            this.xtraTabPage1.Controls.Add(this.label7);
            this.xtraTabPage1.Controls.Add(this.txtYMin);
            this.xtraTabPage1.Controls.Add(this.label6);
            this.xtraTabPage1.Controls.Add(this.txtXMin);
            this.xtraTabPage1.Controls.Add(this.label5);
            this.xtraTabPage1.Controls.Add(this.txtSpatialRef);
            this.xtraTabPage1.Controls.Add(this.label4);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(433, 221);
            this.xtraTabPage1.Text = "坐标系";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.grpIndex);
            this.xtraTabPage2.Controls.Add(this.chkIndex);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(433, 221);
            this.xtraTabPage2.Text = "索引设置";
            // 
            // btnDB
            // 
            this.btnDB.Location = new System.Drawing.Point(383, 16);
            this.btnDB.Name = "btnDB";
            this.btnDB.Size = new System.Drawing.Size(75, 23);
            this.btnDB.TabIndex = 7;
            this.btnDB.Text = "选择";
            this.btnDB.Click += new System.EventHandler(this.btnDB_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 14);
            this.label4.TabIndex = 1;
            this.label4.Text = "坐标系";
            // 
            // txtSpatialRef
            // 
            this.txtSpatialRef.Enabled = false;
            this.txtSpatialRef.Location = new System.Drawing.Point(76, 14);
            this.txtSpatialRef.Name = "txtSpatialRef";
            this.txtSpatialRef.Size = new System.Drawing.Size(205, 20);
            this.txtSpatialRef.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 14);
            this.label5.TabIndex = 4;
            this.label5.Text = "最小X";
            // 
            // txtXMin
            // 
            this.txtXMin.Location = new System.Drawing.Point(76, 44);
            this.txtXMin.Name = "txtXMin";
            this.txtXMin.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtXMin.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtXMin.Size = new System.Drawing.Size(118, 20);
            this.txtXMin.TabIndex = 5;
            // 
            // txtYMin
            // 
            this.txtYMin.Location = new System.Drawing.Point(274, 44);
            this.txtYMin.Name = "txtYMin";
            this.txtYMin.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtYMin.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtYMin.Size = new System.Drawing.Size(118, 20);
            this.txtYMin.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(213, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 14);
            this.label6.TabIndex = 6;
            this.label6.Text = "最小Y";
            // 
            // txtXMax
            // 
            this.txtXMax.Location = new System.Drawing.Point(76, 70);
            this.txtXMax.Name = "txtXMax";
            this.txtXMax.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtXMax.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtXMax.Size = new System.Drawing.Size(118, 20);
            this.txtXMax.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 14);
            this.label7.TabIndex = 8;
            this.label7.Text = "最大X";
            // 
            // txtYMax
            // 
            this.txtYMax.Location = new System.Drawing.Point(274, 70);
            this.txtYMax.Name = "txtYMax";
            this.txtYMax.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtYMax.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtYMax.Size = new System.Drawing.Size(118, 20);
            this.txtYMax.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(213, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 14);
            this.label8.TabIndex = 10;
            this.label8.Text = "最大Y";
            // 
            // btnFullExtent
            // 
            this.btnFullExtent.Location = new System.Drawing.Point(343, 11);
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.Size = new System.Drawing.Size(49, 23);
            this.btnFullExtent.TabIndex = 12;
            this.btnFullExtent.Text = "全图";
            this.btnFullExtent.Click += new System.EventHandler(this.btnFullExtent_Click);
            // 
            // btnCurrentExtent
            // 
            this.btnCurrentExtent.Location = new System.Drawing.Point(288, 11);
            this.btnCurrentExtent.Name = "btnCurrentExtent";
            this.btnCurrentExtent.Size = new System.Drawing.Size(49, 23);
            this.btnCurrentExtent.TabIndex = 13;
            this.btnCurrentExtent.Text = "当前";
            this.btnCurrentExtent.Click += new System.EventHandler(this.btnCurrentExtent_Click);
            // 
            // chkIndex
            // 
            this.chkIndex.Location = new System.Drawing.Point(16, 16);
            this.chkIndex.Name = "chkIndex";
            this.chkIndex.Properties.Caption = "生成索引图";
            this.chkIndex.Size = new System.Drawing.Size(75, 19);
            this.chkIndex.TabIndex = 0;
            this.chkIndex.CheckedChanged += new System.EventHandler(this.chkIndex_CheckedChanged);
            // 
            // grpIndex
            // 
            this.grpIndex.Controls.Add(this.txtHeight);
            this.grpIndex.Controls.Add(this.label10);
            this.grpIndex.Controls.Add(this.txtWidth);
            this.grpIndex.Controls.Add(this.label9);
            this.grpIndex.Location = new System.Drawing.Point(19, 44);
            this.grpIndex.Name = "grpIndex";
            this.grpIndex.Size = new System.Drawing.Size(396, 91);
            this.grpIndex.TabIndex = 1;
            this.grpIndex.Text = "索引设置";
            this.grpIndex.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 36);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 14);
            this.label9.TabIndex = 2;
            this.label9.Text = "宽度";
            // 
            // txtWidth
            // 
            this.txtWidth.EditValue = "250";
            this.txtWidth.Location = new System.Drawing.Point(46, 33);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtWidth.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtWidth.Size = new System.Drawing.Size(118, 20);
            this.txtWidth.TabIndex = 6;
            // 
            // txtHeight
            // 
            this.txtHeight.EditValue = "250";
            this.txtHeight.Location = new System.Drawing.Point(214, 33);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtHeight.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtHeight.Size = new System.Drawing.Size(118, 20);
            this.txtHeight.TabIndex = 8;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(173, 36);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 14);
            this.label10.TabIndex = 7;
            this.label10.Text = "高度";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(250, 334);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 8;
            this.simpleButton1.Text = "生成";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(383, 334);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 9;
            this.simpleButton2.Text = "取消";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // frmQuickCreateFeatureClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 369);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.btnDB);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.cmbTemplate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtDB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmQuickCreateFeatureClass";
            this.Text = "快速创建要素类";
            ((System.ComponentModel.ISupportInitialize)(this.txtDB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage1.PerformLayout();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSpatialRef.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtXMin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYMin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtXMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIndex.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpIndex)).EndInit();
            this.grpIndex.ResumeLayout(false);
            this.grpIndex.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtWidth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHeight.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtDB;
        private DevExpress.XtraEditors.TextEdit txtName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTemplate;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraEditors.SimpleButton btnDB;
        private DevExpress.XtraEditors.SimpleButton btnCurrentExtent;
        private DevExpress.XtraEditors.SimpleButton btnFullExtent;
        private DevExpress.XtraEditors.TextEdit txtYMax;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraEditors.TextEdit txtXMax;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.TextEdit txtYMin;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.TextEdit txtXMin;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.TextEdit txtSpatialRef;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.GroupControl grpIndex;
        private DevExpress.XtraEditors.TextEdit txtHeight;
        private System.Windows.Forms.Label label10;
        private DevExpress.XtraEditors.TextEdit txtWidth;
        private System.Windows.Forms.Label label9;
        private DevExpress.XtraEditors.CheckEdit chkIndex;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
    }
}