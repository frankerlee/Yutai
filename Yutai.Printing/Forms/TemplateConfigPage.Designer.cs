namespace Yutai.Plugins.Printing.Forms
{
    partial class TemplateConfigPage
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtDB = new DevExpress.XtraEditors.TextEdit();
            this.btnTemplateDB = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lstIndexes = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.txtDB.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(19, 18);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "模板数据库";
            // 
            // txtDB
            // 
            this.txtDB.Location = new System.Drawing.Point(91, 17);
            this.txtDB.Name = "txtDB";
            this.txtDB.Size = new System.Drawing.Size(332, 20);
            this.txtDB.TabIndex = 1;
            // 
            // btnTemplateDB
            // 
            this.btnTemplateDB.Location = new System.Drawing.Point(429, 14);
            this.btnTemplateDB.Name = "btnTemplateDB";
            this.btnTemplateDB.Size = new System.Drawing.Size(75, 23);
            this.btnTemplateDB.TabIndex = 2;
            this.btnTemplateDB.Text = "选择";
            this.btnTemplateDB.Click += new System.EventHandler(this.btnTemplateDB_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(19, 53);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "索引图配置";
            // 
            // lstIndexes
            // 
            this.lstIndexes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstIndexes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lstIndexes.FullRowSelect = true;
            this.lstIndexes.Location = new System.Drawing.Point(19, 77);
            this.lstIndexes.Name = "lstIndexes";
            this.lstIndexes.Size = new System.Drawing.Size(488, 303);
            this.lstIndexes.TabIndex = 4;
            this.lstIndexes.UseCompatibleStateImageBehavior = false;
            this.lstIndexes.View = System.Windows.Forms.View.Details;
            this.lstIndexes.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstIndexes_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "名称";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "索引图层";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "模板名称";
            this.columnHeader3.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "搜索字段";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader4.Width = 100;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(348, 49);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 5;
            this.simpleButton1.Text = "新增";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(429, 49);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "图名字段";
            // 
            // TemplateConfigPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.lstIndexes);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.btnTemplateDB);
            this.Controls.Add(this.txtDB);
            this.Controls.Add(this.labelControl1);
            this.Name = "TemplateConfigPage";
            this.Size = new System.Drawing.Size(520, 401);
            ((System.ComponentModel.ISupportInitialize)(this.txtDB.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtDB;
        private DevExpress.XtraEditors.SimpleButton btnTemplateDB;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.ListView lstIndexes;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
    }
}
