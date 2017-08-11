namespace Yutai.Check.Controls
{
    partial class GridControlView
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
            this.mainGridControl = new DevExpress.XtraGrid.GridControl();
            this.mainGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.mainGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // mainGridControl
            // 
            this.mainGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainGridControl.Location = new System.Drawing.Point(0, 0);
            this.mainGridControl.MainView = this.mainGridView;
            this.mainGridControl.Name = "mainGridControl";
            this.mainGridControl.Size = new System.Drawing.Size(627, 317);
            this.mainGridControl.TabIndex = 0;
            this.mainGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.mainGridView});
            // 
            // mainGridView
            // 
            this.mainGridView.GridControl = this.mainGridControl;
            this.mainGridView.Name = "mainGridView";
            this.mainGridView.OptionsBehavior.Editable = false;
            this.mainGridView.OptionsView.ColumnAutoWidth = false;
            this.mainGridView.OptionsView.ShowGroupPanel = false;
            this.mainGridView.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.mainGridView_RowCellClick);
            // 
            // GridControlView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainGridControl);
            this.Name = "GridControlView";
            this.Size = new System.Drawing.Size(627, 317);
            ((System.ComponentModel.ISupportInitialize)(this.mainGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl mainGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView mainGridView;
    }
}
