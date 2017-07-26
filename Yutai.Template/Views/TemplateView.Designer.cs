namespace Yutai.Plugins.Template.Views
{
    partial class TemplateView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplateView));
            this.trvDatabase = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.menuDatabase = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuAddConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConnection = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuRemoveConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConnectionRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRegisterTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFCTemplates = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuAddFCTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFCTemplateRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDomains = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuAddDomain = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDomainRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuCopyDomains = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPasteDomain = new System.Windows.Forms.ToolStripMenuItem();
            this.menuObject = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuProperty = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuCopyObject = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDatasets = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuAddDataset = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDatasetRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuPasteObject = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDatabase.SuspendLayout();
            this.menuConnection.SuspendLayout();
            this.menuFCTemplates.SuspendLayout();
            this.menuDomains.SuspendLayout();
            this.menuObject.SuspendLayout();
            this.menuDatasets.SuspendLayout();
            this.SuspendLayout();
            // 
            // trvDatabase
            // 
            this.trvDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvDatabase.ImageIndex = 0;
            this.trvDatabase.ImageList = this.imageList1;
            this.trvDatabase.Location = new System.Drawing.Point(0, 0);
            this.trvDatabase.Name = "trvDatabase";
            this.trvDatabase.SelectedImageIndex = 0;
            this.trvDatabase.Size = new System.Drawing.Size(279, 582);
            this.trvDatabase.TabIndex = 1;
            this.trvDatabase.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvDatabase_NodeMouseDoubleClick);
            this.trvDatabase.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trvDatabase_MouseDown);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Databases");
            this.imageList1.Images.SetKeyName(1, "Database");
            this.imageList1.Images.SetKeyName(2, "Templates");
            this.imageList1.Images.SetKeyName(3, "Domains");
            this.imageList1.Images.SetKeyName(4, "Template");
            this.imageList1.Images.SetKeyName(5, "Field");
            this.imageList1.Images.SetKeyName(6, "Domain");
            this.imageList1.Images.SetKeyName(7, "Datasets");
            this.imageList1.Images.SetKeyName(8, "Dataset");
            // 
            // menuDatabase
            // 
            this.menuDatabase.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddConnection,
            this.mnuRefresh});
            this.menuDatabase.Name = "menuDatabase";
            this.menuDatabase.Size = new System.Drawing.Size(137, 48);
            // 
            // mnuAddConnection
            // 
            this.mnuAddConnection.Name = "mnuAddConnection";
            this.mnuAddConnection.Size = new System.Drawing.Size(136, 22);
            this.mnuAddConnection.Text = "添加数据库";
            this.mnuAddConnection.Click += new System.EventHandler(this.mnuAddConnection_Click);
            // 
            // mnuRefresh
            // 
            this.mnuRefresh.Name = "mnuRefresh";
            this.mnuRefresh.Size = new System.Drawing.Size(136, 22);
            this.mnuRefresh.Text = "刷新";
            this.mnuRefresh.Click += new System.EventHandler(this.mnuRefresh_Click);
            // 
            // menuConnection
            // 
            this.menuConnection.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRemoveConnection,
            this.mnuConnectionRefresh,
            this.toolStripSeparator1,
            this.mnuRegisterTemplate,
            this.toolStripSeparator5,
            this.mnuPasteObject});
            this.menuConnection.Name = "menuConnection";
            this.menuConnection.Size = new System.Drawing.Size(173, 126);
            // 
            // mnuRemoveConnection
            // 
            this.mnuRemoveConnection.Name = "mnuRemoveConnection";
            this.mnuRemoveConnection.Size = new System.Drawing.Size(172, 22);
            this.mnuRemoveConnection.Text = "断开连接";
            this.mnuRemoveConnection.Click += new System.EventHandler(this.mnuRemoveConnection_Click);
            // 
            // mnuConnectionRefresh
            // 
            this.mnuConnectionRefresh.Name = "mnuConnectionRefresh";
            this.mnuConnectionRefresh.Size = new System.Drawing.Size(172, 22);
            this.mnuConnectionRefresh.Text = "刷新";
            this.mnuConnectionRefresh.Click += new System.EventHandler(this.mnuConnectionRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(169, 6);
            // 
            // mnuRegisterTemplate
            // 
            this.mnuRegisterTemplate.Name = "mnuRegisterTemplate";
            this.mnuRegisterTemplate.Size = new System.Drawing.Size(172, 22);
            this.mnuRegisterTemplate.Text = "注册为模板数据库";
            this.mnuRegisterTemplate.Click += new System.EventHandler(this.mnuRegisterTemplate_Click);
            // 
            // menuFCTemplates
            // 
            this.menuFCTemplates.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddFCTemplate,
            this.mnuFCTemplateRefresh});
            this.menuFCTemplates.Name = "menuFCTemplates";
            this.menuFCTemplates.Size = new System.Drawing.Size(161, 48);
            // 
            // mnuAddFCTemplate
            // 
            this.mnuAddFCTemplate.Name = "mnuAddFCTemplate";
            this.mnuAddFCTemplate.Size = new System.Drawing.Size(160, 22);
            this.mnuAddFCTemplate.Text = "新建要素类模板";
            this.mnuAddFCTemplate.Click += new System.EventHandler(this.mnuAddFCTemplate_Click);
            // 
            // mnuFCTemplateRefresh
            // 
            this.mnuFCTemplateRefresh.Name = "mnuFCTemplateRefresh";
            this.mnuFCTemplateRefresh.Size = new System.Drawing.Size(160, 22);
            this.mnuFCTemplateRefresh.Text = "刷新";
            this.mnuFCTemplateRefresh.Click += new System.EventHandler(this.mnuFCTemplateRefresh_Click);
            // 
            // menuDomains
            // 
            this.menuDomains.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddDomain,
            this.mnuDomainRefresh,
            this.toolStripSeparator2,
            this.mnuCopyDomains,
            this.mnuPasteDomain});
            this.menuDomains.Name = "menuDomains";
            this.menuDomains.Size = new System.Drawing.Size(149, 98);
            // 
            // mnuAddDomain
            // 
            this.mnuAddDomain.Name = "mnuAddDomain";
            this.mnuAddDomain.Size = new System.Drawing.Size(148, 22);
            this.mnuAddDomain.Text = "新建数据字典";
            this.mnuAddDomain.Click += new System.EventHandler(this.mnuAddDomain_Click);
            // 
            // mnuDomainRefresh
            // 
            this.mnuDomainRefresh.Name = "mnuDomainRefresh";
            this.mnuDomainRefresh.Size = new System.Drawing.Size(148, 22);
            this.mnuDomainRefresh.Text = "刷新";
            this.mnuDomainRefresh.Click += new System.EventHandler(this.mnuDomainRefresh_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // mnuCopyDomains
            // 
            this.mnuCopyDomains.Name = "mnuCopyDomains";
            this.mnuCopyDomains.Size = new System.Drawing.Size(152, 22);
            this.mnuCopyDomains.Text = "复制";
            this.mnuCopyDomains.Visible = false;
            // 
            // mnuPasteDomain
            // 
            this.mnuPasteDomain.Name = "mnuPasteDomain";
            this.mnuPasteDomain.Size = new System.Drawing.Size(152, 22);
            this.mnuPasteDomain.Text = "粘贴";
            this.mnuPasteDomain.Visible = false;
            // 
            // menuObject
            // 
            this.menuObject.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDelete,
            this.toolStripSeparator3,
            this.mnuProperty,
            this.toolStripSeparator4,
            this.mnuCopyObject});
            this.menuObject.Name = "menuObject";
            this.menuObject.Size = new System.Drawing.Size(101, 82);
            // 
            // mnuDelete
            // 
            this.mnuDelete.Name = "mnuDelete";
            this.mnuDelete.Size = new System.Drawing.Size(100, 22);
            this.mnuDelete.Text = "删除";
            this.mnuDelete.Click += new System.EventHandler(this.mnuDelete_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(97, 6);
            // 
            // mnuProperty
            // 
            this.mnuProperty.Name = "mnuProperty";
            this.mnuProperty.Size = new System.Drawing.Size(100, 22);
            this.mnuProperty.Text = "属性";
            this.mnuProperty.Click += new System.EventHandler(this.mnuProperty_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(97, 6);
            // 
            // mnuCopyObject
            // 
            this.mnuCopyObject.Name = "mnuCopyObject";
            this.mnuCopyObject.Size = new System.Drawing.Size(100, 22);
            this.mnuCopyObject.Text = "复制";
            this.mnuCopyObject.Click += new System.EventHandler(this.mnuCopyObject_Click);
            // 
            // menuDatasets
            // 
            this.menuDatasets.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddDataset,
            this.mnuDatasetRefresh});
            this.menuDatasets.Name = "menuFCTemplates";
            this.menuDatasets.Size = new System.Drawing.Size(137, 48);
            // 
            // mnuAddDataset
            // 
            this.mnuAddDataset.Name = "mnuAddDataset";
            this.mnuAddDataset.Size = new System.Drawing.Size(136, 22);
            this.mnuAddDataset.Text = "新建数据集";
            this.mnuAddDataset.Click += new System.EventHandler(this.mnuAddDataset_Click);
            // 
            // mnuDatasetRefresh
            // 
            this.mnuDatasetRefresh.Name = "mnuDatasetRefresh";
            this.mnuDatasetRefresh.Size = new System.Drawing.Size(136, 22);
            this.mnuDatasetRefresh.Text = "刷新";
            this.mnuDatasetRefresh.Click += new System.EventHandler(this.mnuDatasetRefresh_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(169, 6);
            // 
            // mnuPasteObject
            // 
            this.mnuPasteObject.Name = "mnuPasteObject";
            this.mnuPasteObject.Size = new System.Drawing.Size(172, 22);
            this.mnuPasteObject.Text = "粘贴";
            this.mnuPasteObject.Click += new System.EventHandler(this.mnuPasteObject_Click);
            // 
            // TemplateView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.trvDatabase);
            this.Name = "TemplateView";
            this.Size = new System.Drawing.Size(279, 582);
            this.menuDatabase.ResumeLayout(false);
            this.menuConnection.ResumeLayout(false);
            this.menuFCTemplates.ResumeLayout(false);
            this.menuDomains.ResumeLayout(false);
            this.menuObject.ResumeLayout(false);
            this.menuDatasets.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TreeView trvDatabase;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip menuDatabase;
        private System.Windows.Forms.ToolStripMenuItem mnuAddConnection;
        private System.Windows.Forms.ToolStripMenuItem mnuRefresh;
        private System.Windows.Forms.ContextMenuStrip menuConnection;
        private System.Windows.Forms.ToolStripMenuItem mnuRemoveConnection;
        private System.Windows.Forms.ToolStripMenuItem mnuConnectionRefresh;
        private System.Windows.Forms.ContextMenuStrip menuFCTemplates;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFCTemplate;
        private System.Windows.Forms.ToolStripMenuItem mnuFCTemplateRefresh;
        private System.Windows.Forms.ContextMenuStrip menuDomains;
        private System.Windows.Forms.ToolStripMenuItem mnuAddDomain;
        private System.Windows.Forms.ToolStripMenuItem mnuDomainRefresh;
        private System.Windows.Forms.ContextMenuStrip menuObject;
        private System.Windows.Forms.ToolStripMenuItem mnuDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnuProperty;
        private System.Windows.Forms.ContextMenuStrip menuDatasets;
        private System.Windows.Forms.ToolStripMenuItem mnuAddDataset;
        private System.Windows.Forms.ToolStripMenuItem mnuDatasetRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuRegisterTemplate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuCopyDomains;
        private System.Windows.Forms.ToolStripMenuItem mnuPasteDomain;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mnuCopyObject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem mnuPasteObject;
    }
}
