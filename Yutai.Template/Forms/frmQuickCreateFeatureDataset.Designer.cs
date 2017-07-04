using DevExpress.XtraEditors;

namespace Yutai.Plugins.Template.Forms
{
    partial class frmQuickCreateFeatureDataset
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
            this.btnDB = new DevExpress.XtraEditors.SimpleButton();
            this.cmbTemplate = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPreName = new DevExpress.XtraEditors.TextEdit();
            this.txtDB = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.btnCurrentExtent = new DevExpress.XtraEditors.SimpleButton();
            this.btnFullExtent = new DevExpress.XtraEditors.SimpleButton();
            this.txtYMax = new DevExpress.XtraEditors.TextEdit();
            this.label8 = new System.Windows.Forms.Label();
            this.txtXMax = new DevExpress.XtraEditors.TextEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.txtYMin = new DevExpress.XtraEditors.TextEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.txtXMin = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSpatialRef = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.chkFeatureClasses = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.txtNameNext = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.chkNamePre = new System.Windows.Forms.CheckBox();
            this.chkNameNext = new System.Windows.Forms.CheckBox();
            this.txtAliasPre = new DevExpress.XtraEditors.TextEdit();
            this.txtAliasNext = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtPreName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtYMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtXMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYMin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtXMin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpatialRef.Properties)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkFeatureClasses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameNext.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAliasPre.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAliasNext.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDB
            // 
            this.btnDB.Location = new System.Drawing.Point(376, 11);
            this.btnDB.Name = "btnDB";
            this.btnDB.Size = new System.Drawing.Size(75, 23);
            this.btnDB.TabIndex = 14;
            this.btnDB.Text = "选择";
            this.btnDB.Click += new System.EventHandler(this.btnDB_Click);
            // 
            // cmbTemplate
            // 
            this.cmbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTemplate.FormattingEnabled = true;
            this.cmbTemplate.Location = new System.Drawing.Point(78, 43);
            this.cmbTemplate.Name = "cmbTemplate";
            this.cmbTemplate.Size = new System.Drawing.Size(151, 20);
            this.cmbTemplate.TabIndex = 13;
            this.cmbTemplate.SelectedIndexChanged += new System.EventHandler(this.cmbTemplate_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "模    板";
            // 
            // txtPreName
            // 
            this.txtPreName.Location = new System.Drawing.Point(92, 74);
            this.txtPreName.Name = "txtPreName";
            this.txtPreName.Size = new System.Drawing.Size(112, 20);
            this.txtPreName.TabIndex = 11;
            // 
            // txtDB
            // 
            this.txtDB.Enabled = false;
            this.txtDB.Location = new System.Drawing.Point(78, 12);
            this.txtDB.Name = "txtDB";
            this.txtDB.Size = new System.Drawing.Size(286, 20);
            this.txtDB.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "保存位置";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(12, 136);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(439, 250);
            this.xtraTabControl1.TabIndex = 15;
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
            // btnCurrentExtent
            // 
            this.btnCurrentExtent.Location = new System.Drawing.Point(288, 11);
            this.btnCurrentExtent.Name = "btnCurrentExtent";
            this.btnCurrentExtent.Size = new System.Drawing.Size(49, 23);
            this.btnCurrentExtent.TabIndex = 13;
            this.btnCurrentExtent.Text = "当前";
            this.btnCurrentExtent.Click += new System.EventHandler(this.btnCurrentExtent_Click);
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
            // txtXMin
            // 
            this.txtXMin.Location = new System.Drawing.Point(76, 44);
            this.txtXMin.Name = "txtXMin";
            this.txtXMin.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtXMin.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtXMin.Size = new System.Drawing.Size(118, 20);
            this.txtXMin.TabIndex = 5;
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
            // txtSpatialRef
            // 
            this.txtSpatialRef.Enabled = false;
            this.txtSpatialRef.Location = new System.Drawing.Point(76, 14);
            this.txtSpatialRef.Name = "txtSpatialRef";
            this.txtSpatialRef.Size = new System.Drawing.Size(205, 20);
            this.txtSpatialRef.TabIndex = 3;
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
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.chkFeatureClasses);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(433, 221);
            this.xtraTabPage2.Text = "图层组";
            // 
            // chkFeatureClasses
            // 
            this.chkFeatureClasses.Location = new System.Drawing.Point(10, 12);
            this.chkFeatureClasses.Name = "chkFeatureClasses";
            this.chkFeatureClasses.Size = new System.Drawing.Size(405, 194);
            this.chkFeatureClasses.TabIndex = 0;
            // 
            // txtNameNext
            // 
            this.txtNameNext.Location = new System.Drawing.Point(308, 74);
            this.txtNameNext.Name = "txtNameNext";
            this.txtNameNext.Size = new System.Drawing.Size(140, 20);
            this.txtNameNext.TabIndex = 17;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(371, 415);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 20;
            this.simpleButton2.Text = "取消";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(238, 415);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 19;
            this.simpleButton1.Text = "生成";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // chkNamePre
            // 
            this.chkNamePre.AutoSize = true;
            this.chkNamePre.Location = new System.Drawing.Point(14, 77);
            this.chkNamePre.Name = "chkNamePre";
            this.chkNamePre.Size = new System.Drawing.Size(72, 16);
            this.chkNamePre.TabIndex = 21;
            this.chkNamePre.Text = "名称前缀";
            this.chkNamePre.UseVisualStyleBackColor = true;
            this.chkNamePre.CheckedChanged += new System.EventHandler(this.chkNamePre_CheckedChanged);
            // 
            // chkNameNext
            // 
            this.chkNameNext.AutoSize = true;
            this.chkNameNext.Location = new System.Drawing.Point(222, 77);
            this.chkNameNext.Name = "chkNameNext";
            this.chkNameNext.Size = new System.Drawing.Size(72, 16);
            this.chkNameNext.TabIndex = 22;
            this.chkNameNext.Text = "名称后缀";
            this.chkNameNext.UseVisualStyleBackColor = true;
            this.chkNameNext.CheckedChanged += new System.EventHandler(this.chkNameNext_CheckedChanged);
            // 
            // txtAliasPre
            // 
            this.txtAliasPre.Location = new System.Drawing.Point(92, 100);
            this.txtAliasPre.Name = "txtAliasPre";
            this.txtAliasPre.Size = new System.Drawing.Size(112, 20);
            this.txtAliasPre.TabIndex = 23;
            // 
            // txtAliasNext
            // 
            this.txtAliasNext.Location = new System.Drawing.Point(308, 100);
            this.txtAliasNext.Name = "txtAliasNext";
            this.txtAliasNext.Size = new System.Drawing.Size(140, 20);
            this.txtAliasNext.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 25;
            this.label2.Text = "别名前缀";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(236, 105);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 26;
            this.label9.Text = "别名后缀";
            // 
            // frmQuickCreateFeatureDataset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 450);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAliasNext);
            this.Controls.Add(this.txtAliasPre);
            this.Controls.Add(this.chkNameNext);
            this.Controls.Add(this.chkNamePre);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.txtNameNext);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.btnDB);
            this.Controls.Add(this.cmbTemplate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPreName);
            this.Controls.Add(this.txtDB);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQuickCreateFeatureDataset";
            this.Text = "快速创建图层组";
            ((System.ComponentModel.ISupportInitialize)(this.txtPreName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtYMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtXMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYMin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtXMin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpatialRef.Properties)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkFeatureClasses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameNext.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAliasPre.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAliasNext.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnDB;
        private System.Windows.Forms.ComboBox cmbTemplate;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtPreName;
        private DevExpress.XtraEditors.TextEdit txtDB;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
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
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraEditors.CheckedListBoxControl chkFeatureClasses;
        private CheckEdit chkPre;
        private System.Windows.Forms.CheckBox chkNamePre;
        private System.Windows.Forms.CheckBox chkNameNext;
        private TextEdit txtNameNext;
        private SimpleButton simpleButton2;
        private SimpleButton simpleButton1;
        private TextEdit txtAliasPre;
        private TextEdit txtAliasNext;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
    }
}