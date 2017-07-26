namespace Yutai.Plugins.Template.Forms
{
    partial class frmEditTemplate
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
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.cmbDatasets = new System.Windows.Forms.ComboBox();
            this.cmbGeometryTypes = new System.Windows.Forms.ComboBox();
            this.cmbFeatureType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAliasName = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBaseName = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.FieldName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.AliasName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.AllowNull = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.IsKey = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.FieldType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.FieldLength = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Precision = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DomainName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.btnImportFC = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteField = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAliasName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBaseName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteField)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(581, 318);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(56, 24);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(582, 318);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(56, 24);
            this.btnApply.TabIndex = 5;
            this.btnApply.Text = "应用";
            this.btnApply.Visible = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(454, 318);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(56, 24);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(6, 7);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(631, 301);
            this.xtraTabControl1.TabIndex = 7;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.cmbDatasets);
            this.xtraTabPage1.Controls.Add(this.cmbGeometryTypes);
            this.xtraTabPage1.Controls.Add(this.cmbFeatureType);
            this.xtraTabPage1.Controls.Add(this.label6);
            this.xtraTabPage1.Controls.Add(this.label5);
            this.xtraTabPage1.Controls.Add(this.label4);
            this.xtraTabPage1.Controls.Add(this.label3);
            this.xtraTabPage1.Controls.Add(this.txtAliasName);
            this.xtraTabPage1.Controls.Add(this.label2);
            this.xtraTabPage1.Controls.Add(this.txtBaseName);
            this.xtraTabPage1.Controls.Add(this.label1);
            this.xtraTabPage1.Controls.Add(this.txtName);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(625, 272);
            this.xtraTabPage1.Text = "基本信息";
            // 
            // cmbDatasets
            // 
            this.cmbDatasets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDatasets.FormattingEnabled = true;
            this.cmbDatasets.Location = new System.Drawing.Point(71, 96);
            this.cmbDatasets.Name = "cmbDatasets";
            this.cmbDatasets.Size = new System.Drawing.Size(194, 22);
            this.cmbDatasets.TabIndex = 25;
            this.cmbDatasets.SelectedIndexChanged += new System.EventHandler(this.cmbDatasets_SelectedIndexChanged);
            // 
            // cmbGeometryTypes
            // 
            this.cmbGeometryTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGeometryTypes.FormattingEnabled = true;
            this.cmbGeometryTypes.Location = new System.Drawing.Point(347, 66);
            this.cmbGeometryTypes.Name = "cmbGeometryTypes";
            this.cmbGeometryTypes.Size = new System.Drawing.Size(194, 22);
            this.cmbGeometryTypes.TabIndex = 24;
            this.cmbGeometryTypes.SelectedIndexChanged += new System.EventHandler(this.cmbGeometryTypes_SelectedIndexChanged);
            // 
            // cmbFeatureType
            // 
            this.cmbFeatureType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFeatureType.FormattingEnabled = true;
            this.cmbFeatureType.Location = new System.Drawing.Point(71, 68);
            this.cmbFeatureType.Name = "cmbFeatureType";
            this.cmbFeatureType.Size = new System.Drawing.Size(194, 22);
            this.cmbFeatureType.TabIndex = 23;
            this.cmbFeatureType.SelectedIndexChanged += new System.EventHandler(this.cmbFeatureType_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 14);
            this.label6.TabIndex = 22;
            this.label6.Text = "要素集";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(288, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 14);
            this.label5.TabIndex = 21;
            this.label5.Text = "图形类型";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 14);
            this.label4.TabIndex = 20;
            this.label4.Text = "要素类型";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(288, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 14);
            this.label3.TabIndex = 19;
            this.label3.Text = "基本别称";
            // 
            // txtAliasName
            // 
            this.txtAliasName.Location = new System.Drawing.Point(347, 40);
            this.txtAliasName.Name = "txtAliasName";
            this.txtAliasName.Size = new System.Drawing.Size(197, 20);
            this.txtAliasName.TabIndex = 18;
            this.txtAliasName.EditValueChanged += new System.EventHandler(this.txtAliasName_EditValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 14);
            this.label2.TabIndex = 17;
            this.label2.Text = "基本名称";
            // 
            // txtBaseName
            // 
            this.txtBaseName.Location = new System.Drawing.Point(72, 40);
            this.txtBaseName.Name = "txtBaseName";
            this.txtBaseName.Size = new System.Drawing.Size(197, 20);
            this.txtBaseName.TabIndex = 16;
            this.txtBaseName.EditValueChanged += new System.EventHandler(this.txtBaseName_EditValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 15;
            this.label1.Text = "模板名称";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(71, 14);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(471, 20);
            this.txtName.TabIndex = 14;
            this.txtName.EditValueChanged += new System.EventHandler(this.txtName_EditValueChanged);
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.gridControl1);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(625, 272);
            this.xtraTabPage2.Text = "字段设置";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(10, 10);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2,
            this.repositoryItemLookUpEdit1,
            this.repositoryItemLookUpEdit2,
            this.btnDeleteField});
            this.gridControl1.Size = new System.Drawing.Size(600, 250);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.FieldName,
            this.AliasName,
            this.AllowNull,
            this.IsKey,
            this.FieldType,
            this.FieldLength,
            this.Precision,
            this.DomainName});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            // 
            // FieldName
            // 
            this.FieldName.Caption = "名称";
            this.FieldName.FieldName = "FieldName";
            this.FieldName.Name = "FieldName";
            this.FieldName.Visible = true;
            this.FieldName.VisibleIndex = 0;
            // 
            // AliasName
            // 
            this.AliasName.Caption = "别名";
            this.AliasName.FieldName = "AliasName";
            this.AliasName.Name = "AliasName";
            this.AliasName.Visible = true;
            this.AliasName.VisibleIndex = 1;
            // 
            // AllowNull
            // 
            this.AllowNull.Caption = "允许为空";
            this.AllowNull.ColumnEdit = this.repositoryItemCheckEdit1;
            this.AllowNull.FieldName = "AllowNull";
            this.AllowNull.Name = "AllowNull";
            this.AllowNull.Visible = true;
            this.AllowNull.VisibleIndex = 2;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // IsKey
            // 
            this.IsKey.Caption = "是否键值";
            this.IsKey.ColumnEdit = this.repositoryItemCheckEdit2;
            this.IsKey.FieldName = "IsKey";
            this.IsKey.Name = "IsKey";
            this.IsKey.Visible = true;
            this.IsKey.VisibleIndex = 3;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // FieldType
            // 
            this.FieldType.Caption = "类型";
            this.FieldType.ColumnEdit = this.repositoryItemLookUpEdit1;
            this.FieldType.FieldName = "FieldType";
            this.FieldType.Name = "FieldType";
            this.FieldType.Visible = true;
            this.FieldType.VisibleIndex = 4;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            // 
            // FieldLength
            // 
            this.FieldLength.Caption = "长度";
            this.FieldLength.FieldName = "FieldLength";
            this.FieldLength.Name = "FieldLength";
            this.FieldLength.Visible = true;
            this.FieldLength.VisibleIndex = 5;
            // 
            // Precision
            // 
            this.Precision.Caption = "小数位";
            this.Precision.FieldName = "Precision";
            this.Precision.Name = "Precision";
            this.Precision.Visible = true;
            this.Precision.VisibleIndex = 6;
            // 
            // DomainName
            // 
            this.DomainName.Caption = "数据字典";
            this.DomainName.ColumnEdit = this.repositoryItemLookUpEdit2;
            this.DomainName.FieldName = "DomainName";
            this.DomainName.Name = "DomainName";
            this.DomainName.Visible = true;
            this.DomainName.VisibleIndex = 7;
            // 
            // repositoryItemLookUpEdit2
            // 
            this.repositoryItemLookUpEdit2.AutoHeight = false;
            this.repositoryItemLookUpEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit2.Name = "repositoryItemLookUpEdit2";
            // 
            // btnImportFC
            // 
            this.btnImportFC.Location = new System.Drawing.Point(7, 318);
            this.btnImportFC.Name = "btnImportFC";
            this.btnImportFC.Size = new System.Drawing.Size(68, 24);
            this.btnImportFC.TabIndex = 8;
            this.btnImportFC.Text = "字段导入";
            this.btnImportFC.Click += new System.EventHandler(this.btnImportFC_Click);
            // 
            // btnDeleteField
            // 
            this.btnDeleteField.AutoHeight = false;
            this.btnDeleteField.Caption = "删除";
            this.btnDeleteField.Name = "btnDeleteField";
            this.btnDeleteField.NullText = "删除";
            this.btnDeleteField.Click += new System.EventHandler(this.btnDeleteField_Click);
            // 
            // frmEditTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 354);
            this.Controls.Add(this.btnImportFC);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEditTemplate";
            this.Text = "要素类模板";
            this.Load += new System.EventHandler(this.frmEditTemplate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAliasName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBaseName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteField)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtAliasName;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtBaseName;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn FieldName;
        private DevExpress.XtraGrid.Columns.GridColumn AliasName;
        private DevExpress.XtraGrid.Columns.GridColumn AllowNull;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn IsKey;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn FieldType;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn FieldLength;
        private DevExpress.XtraGrid.Columns.GridColumn Precision;
        private DevExpress.XtraGrid.Columns.GridColumn DomainName;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit2;
        private System.Windows.Forms.ComboBox cmbDatasets;
        private System.Windows.Forms.ComboBox cmbGeometryTypes;
        private System.Windows.Forms.ComboBox cmbFeatureType;
        private DevExpress.XtraEditors.SimpleButton btnImportFC;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit btnDeleteField;
    }
}