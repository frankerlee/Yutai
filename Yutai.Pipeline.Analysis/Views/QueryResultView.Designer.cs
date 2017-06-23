namespace Yutai.Pipeline.Analysis.Views
{
    partial class QueryResultView
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cmbStatWay = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.cmbCalField = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.cmbStatField = new System.Windows.Forms.ToolStripComboBox();
            this.btnStatics = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnExportExcel = new System.Windows.Forms.ToolStripButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.mainGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.detailGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detailGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cmbStatWay,
            this.toolStripLabel2,
            this.cmbCalField,
            this.toolStripLabel3,
            this.cmbStatField,
            this.btnStatics,
            this.btnPrint,
            this.btnExportExcel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(629, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(56, 22);
            this.toolStripLabel1.Text = "统计方式";
            // 
            // cmbStatWay
            // 
            this.cmbStatWay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatWay.Items.AddRange(new object[] {
            "计数",
            "求和",
            "平均值"});
            this.cmbStatWay.Name = "cmbStatWay";
            this.cmbStatWay.Size = new System.Drawing.Size(121, 25);
            this.cmbStatWay.SelectedIndexChanged += new System.EventHandler(this.cmbStatWay_SelectedIndexChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabel2.Text = "计算项";
            // 
            // cmbCalField
            // 
            this.cmbCalField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCalField.Name = "cmbCalField";
            this.cmbCalField.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabel3.Text = "分类项";
            // 
            // cmbStatField
            // 
            this.cmbStatField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatField.Name = "cmbStatField";
            this.cmbStatField.Size = new System.Drawing.Size(121, 25);
            this.cmbStatField.SelectedIndexChanged += new System.EventHandler(this.cmbStatField_SelectedIndexChanged);
            // 
            // btnStatics
            // 
            this.btnStatics.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStatics.Image = global::Yutai.Pipeline.Analysis.Properties.Resources.icon_sum;
            this.btnStatics.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStatics.Name = "btnStatics";
            this.btnStatics.Size = new System.Drawing.Size(23, 22);
            this.btnStatics.Text = "toolStripButton1";
            this.btnStatics.ToolTipText = "进行统计操作";
            this.btnStatics.Click += new System.EventHandler(this.btnStatics_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrint.Image = global::Yutai.Pipeline.Analysis.Properties.Resources.icon_print;
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(23, 22);
            this.btnPrint.Text = "toolStripButton2";
            this.btnPrint.ToolTipText = "打印";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportExcel.Image = global::Yutai.Pipeline.Analysis.Properties.Resources.icon_excel;
            this.btnExportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(23, 22);
            this.btnExportExcel.Text = "toolStripButton3";
            this.btnExportExcel.ToolTipText = "输出Excel";
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.LevelTemplate = this.detailGridView;
            gridLevelNode1.RelationName = "Level1";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControl1.Location = new System.Drawing.Point(0, 25);
            this.gridControl1.MainView = this.mainGridView;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(629, 432);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.mainGridView,
            this.detailGridView});
            // 
            // mainGridView
            // 
            this.mainGridView.GridControl = this.gridControl1;
            this.mainGridView.Name = "mainGridView";
            this.mainGridView.MasterRowGetLevelDefaultView += new DevExpress.XtraGrid.Views.Grid.MasterRowGetLevelDefaultViewEventHandler(this.mainGridView_MasterRowGetLevelDefaultView);
            // 
            // detailGridView
            // 
            this.detailGridView.GridControl = this.gridControl1;
            this.detailGridView.Name = "detailGridView";
            // 
            // QueryResultView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "QueryResultView";
            this.Size = new System.Drawing.Size(629, 457);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detailGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cmbStatWay;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox cmbCalField;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripComboBox cmbStatField;
        private System.Windows.Forms.ToolStripButton btnStatics;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.ToolStripButton btnExportExcel;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView mainGridView;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private DevExpress.XtraGrid.Views.Grid.GridView detailGridView;
    }
}
