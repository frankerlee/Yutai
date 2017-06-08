namespace Yutai.Plugins.Locator.Views
{
    partial class LocatorDockPanel
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cmbLocators = new System.Windows.Forms.ComboBox();
            this.txtKey = new DevExpress.XtraEditors.TextEdit();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btnZoom = new DevExpress.XtraEditors.CheckButton();
            this.grdResult = new DevExpress.XtraGrid.GridControl();
            this.cardView1 = new DevExpress.XtraGrid.Views.Card.CardView();
            this.图层 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.序号 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.名称 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.地址 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.说明 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.电话 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.邮箱 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.要素 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.照片 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageEdit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Controls.Add(this.labelControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelControl2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbLocators, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtKey, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnClear, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSearch, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnZoom, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(313, 65);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl1.Location = new System.Drawing.Point(7, 3);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(7, 3, 3, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(50, 29);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "定位器：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(7, 38);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(7, 3, 3, 3);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "关键字：";
            // 
            // cmbLocators
            // 
            this.cmbLocators.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbLocators.FormattingEnabled = true;
            this.cmbLocators.Location = new System.Drawing.Point(63, 7);
            this.cmbLocators.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.cmbLocators.Name = "cmbLocators";
            this.cmbLocators.Size = new System.Drawing.Size(167, 20);
            this.cmbLocators.TabIndex = 2;
            // 
            // txtKey
            // 
            this.txtKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtKey.Location = new System.Drawing.Point(63, 38);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(167, 20);
            this.txtKey.TabIndex = 3;
            // 
            // btnClear
            // 
            this.btnClear.Image = global::Yutai.Plugins.Locator.Properties.Resources.img_clear24;
            this.btnClear.Location = new System.Drawing.Point(276, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(34, 29);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "simpleButton2";
            // 
            // btnSearch
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.btnSearch, 2);
            this.btnSearch.Location = new System.Drawing.Point(236, 38);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(74, 23);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "搜 索";
            // 
            // btnZoom
            // 
            this.btnZoom.Image = global::Yutai.Plugins.Locator.Properties.Resources.icon_zoom_to_layer;
            this.btnZoom.Location = new System.Drawing.Point(236, 3);
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new System.Drawing.Size(34, 29);
            this.btnZoom.TabIndex = 7;
            // 
            // grdResult
            // 
            this.grdResult.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.grdResult.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.grdResult.Location = new System.Drawing.Point(0, 65);
            this.grdResult.MainView = this.cardView1;
            this.grdResult.Name = "grdResult";
            this.grdResult.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageEdit1});
            this.grdResult.Size = new System.Drawing.Size(313, 400);
            this.grdResult.TabIndex = 1;
            this.grdResult.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.cardView1});
            // 
            // cardView1
            // 
            this.cardView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.图层,
            this.序号,
            this.名称,
            this.地址,
            this.说明,
            this.电话,
            this.邮箱,
            this.要素,
            this.照片});
            this.cardView1.FocusedCardTopFieldIndex = 0;
            this.cardView1.GridControl = this.grdResult;
            this.cardView1.Name = "cardView1";
            this.cardView1.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            this.cardView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.cardView1_FocusedRowChanged);
            // 
            // 图层
            // 
            this.图层.Caption = "图层";
            this.图层.FieldName = "图层";
            this.图层.Name = "图层";
            this.图层.OptionsColumn.AllowEdit = false;
            this.图层.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True;
            this.图层.Visible = true;
            this.图层.VisibleIndex = 0;
            // 
            // 序号
            // 
            this.序号.Caption = "序号";
            this.序号.FieldName = "序号";
            this.序号.Name = "序号";
            // 
            // 名称
            // 
            this.名称.Caption = "名称";
            this.名称.FieldName = "名称";
            this.名称.Name = "名称";
            this.名称.Visible = true;
            this.名称.VisibleIndex = 1;
            // 
            // 地址
            // 
            this.地址.Caption = "地址";
            this.地址.FieldName = "地址";
            this.地址.Name = "地址";
            this.地址.Visible = true;
            this.地址.VisibleIndex = 2;
            // 
            // 说明
            // 
            this.说明.Caption = "说明";
            this.说明.FieldName = "说明";
            this.说明.Name = "说明";
            this.说明.Visible = true;
            this.说明.VisibleIndex = 3;
            // 
            // 电话
            // 
            this.电话.Caption = "电话";
            this.电话.FieldName = "电话";
            this.电话.Name = "电话";
            this.电话.Visible = true;
            this.电话.VisibleIndex = 4;
            // 
            // 邮箱
            // 
            this.邮箱.Caption = "邮箱";
            this.邮箱.FieldName = "邮箱";
            this.邮箱.Name = "邮箱";
            this.邮箱.Visible = true;
            this.邮箱.VisibleIndex = 5;
            // 
            // 要素
            // 
            this.要素.Caption = "要素";
            this.要素.FieldName = "要素";
            this.要素.Name = "要素";
            // 
            // 照片
            // 
            this.照片.Caption = "照片";
            this.照片.ColumnEdit = this.repositoryItemImageEdit1;
            this.照片.FieldName = "照片";
            this.照片.Name = "照片";
            this.照片.Visible = true;
            this.照片.VisibleIndex = 6;
            // 
            // repositoryItemImageEdit1
            // 
            this.repositoryItemImageEdit1.AutoHeight = false;
            this.repositoryItemImageEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageEdit1.Name = "repositoryItemImageEdit1";
            // 
            // LocatorDockPanel
            // 
            this.Controls.Add(this.grdResult);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "LocatorDockPanel";
            this.Size = new System.Drawing.Size(313, 465);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.ComboBox cmbLocators;
        private DevExpress.XtraEditors.TextEdit txtKey;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraGrid.GridControl grdResult;
        private DevExpress.XtraGrid.Views.Card.CardView cardView1;
        private DevExpress.XtraGrid.Columns.GridColumn 图层;
        private DevExpress.XtraGrid.Columns.GridColumn 序号;
        private DevExpress.XtraGrid.Columns.GridColumn 名称;
        private DevExpress.XtraGrid.Columns.GridColumn 地址;
        private DevExpress.XtraGrid.Columns.GridColumn 说明;
        private DevExpress.XtraGrid.Columns.GridColumn 电话;
        private DevExpress.XtraGrid.Columns.GridColumn 邮箱;
        private DevExpress.XtraGrid.Columns.GridColumn 要素;
        private DevExpress.XtraGrid.Columns.GridColumn 照片;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageEdit repositoryItemImageEdit1;
        private DevExpress.XtraEditors.CheckButton btnZoom;
    }
}
