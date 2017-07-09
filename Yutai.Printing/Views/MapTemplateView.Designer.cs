namespace Yutai.Plugins.Printing.Views
{
    partial class MapTemplateView
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnConnectDB = new DevExpress.XtraEditors.SimpleButton();
            this.btnDisconnectDB = new DevExpress.XtraEditors.SimpleButton();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnConnectDB);
            this.flowLayoutPanel1.Controls.Add(this.btnDisconnectDB);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(304, 37);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btnConnectDB
            // 
            this.btnConnectDB.Image = global::Yutai.Plugins.Printing.Properties.Resources.icon_database_connect;
            this.btnConnectDB.Location = new System.Drawing.Point(3, 3);
            this.btnConnectDB.Name = "btnConnectDB";
            this.btnConnectDB.Size = new System.Drawing.Size(32, 32);
            this.btnConnectDB.TabIndex = 0;
            this.btnConnectDB.Click += new System.EventHandler(this.btnConnectDB_Click);
            // 
            // btnDisconnectDB
            // 
            this.btnDisconnectDB.Image = global::Yutai.Plugins.Printing.Properties.Resources.icon_database_close;
            this.btnDisconnectDB.Location = new System.Drawing.Point(41, 3);
            this.btnDisconnectDB.Name = "btnDisconnectDB";
            this.btnDisconnectDB.Size = new System.Drawing.Size(32, 32);
            this.btnDisconnectDB.TabIndex = 1;
            this.btnDisconnectDB.Click += new System.EventHandler(this.btnDisconnectDB_Click);
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 37);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(304, 416);
            this.treeView1.TabIndex = 2;
            this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // MapTemplateView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "MapTemplateView";
            this.Size = new System.Drawing.Size(304, 453);
            this.Load += new System.EventHandler(this.MapTemplateView_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraEditors.SimpleButton btnConnectDB;
        private DevExpress.XtraEditors.SimpleButton btnDisconnectDB;
        private System.Windows.Forms.TreeView treeView1;
    }
}
