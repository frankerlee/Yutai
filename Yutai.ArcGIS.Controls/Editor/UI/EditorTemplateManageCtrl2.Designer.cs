namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class EditorTemplateManageCtrl2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorTemplateManageCtrl2));
            this.PropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.listView1 = new Yutai.ArcGIS.Controls.Editor.UI.EditTemplateListView();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.清除分组ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.LayerGroupToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.GeometryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.分组依据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.LayerFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.AnnoFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FillFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LineFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PointFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.过滤依据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ShowAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PropertyToolStripMenuItem
            // 
            this.PropertyToolStripMenuItem.Name = "PropertyToolStripMenuItem";
            this.PropertyToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.PropertyToolStripMenuItem.Text = "属性";
            this.PropertyToolStripMenuItem.Click += new System.EventHandler(this.PropertyToolStripMenuItem_Click);
            // 
            // CopyToolStripMenuItem
            // 
            this.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
            this.CopyToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.CopyToolStripMenuItem.Text = "复制";
            this.CopyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.DeleteToolStripMenuItem.Text = "删除";
            this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteToolStripMenuItem,
            this.CopyToolStripMenuItem,
            this.toolStripSeparator1,
            this.PropertyToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 76);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 25);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(394, 538);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.SmallIcon;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "组织模板";
            // 
            // 清除分组ToolStripMenuItem
            // 
            this.清除分组ToolStripMenuItem.Name = "清除分组ToolStripMenuItem";
            this.清除分组ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.清除分组ToolStripMenuItem.Text = "清除分组";
            this.清除分组ToolStripMenuItem.Click += new System.EventHandler(this.清除分组ToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(121, 6);
            // 
            // LayerGroupToolStripMenuItem1
            // 
            this.LayerGroupToolStripMenuItem1.Name = "LayerGroupToolStripMenuItem1";
            this.LayerGroupToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.LayerGroupToolStripMenuItem1.Text = "图层";
            // 
            // GeometryToolStripMenuItem
            // 
            this.GeometryToolStripMenuItem.Name = "GeometryToolStripMenuItem";
            this.GeometryToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.GeometryToolStripMenuItem.Text = "类型";
            // 
            // 分组依据ToolStripMenuItem
            // 
            this.分组依据ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GeometryToolStripMenuItem,
            this.LayerGroupToolStripMenuItem1,
            this.toolStripSeparator5,
            this.清除分组ToolStripMenuItem});
            this.分组依据ToolStripMenuItem.Name = "分组依据ToolStripMenuItem";
            this.分组依据ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.分组依据ToolStripMenuItem.Text = "分组依据";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(145, 6);
            // 
            // LayerFilterToolStripMenuItem
            // 
            this.LayerFilterToolStripMenuItem.Name = "LayerFilterToolStripMenuItem";
            this.LayerFilterToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.LayerFilterToolStripMenuItem.Text = "图层";
            this.LayerFilterToolStripMenuItem.Click += new System.EventHandler(this.LayerFilterToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(97, 6);
            // 
            // AnnoFilterToolStripMenuItem
            // 
            this.AnnoFilterToolStripMenuItem.Name = "AnnoFilterToolStripMenuItem";
            this.AnnoFilterToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.AnnoFilterToolStripMenuItem.Text = "注记";
            this.AnnoFilterToolStripMenuItem.Click += new System.EventHandler(this.AnnoFilterToolStripMenuItem_Click);
            // 
            // FillFilterToolStripMenuItem
            // 
            this.FillFilterToolStripMenuItem.Name = "FillFilterToolStripMenuItem";
            this.FillFilterToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.FillFilterToolStripMenuItem.Text = "面";
            this.FillFilterToolStripMenuItem.Click += new System.EventHandler(this.FillFilterToolStripMenuItem_Click);
            // 
            // LineFilterToolStripMenuItem
            // 
            this.LineFilterToolStripMenuItem.Name = "LineFilterToolStripMenuItem";
            this.LineFilterToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.LineFilterToolStripMenuItem.Text = "线";
            this.LineFilterToolStripMenuItem.Click += new System.EventHandler(this.LineFilterToolStripMenuItem_Click);
            // 
            // PointFilterToolStripMenuItem
            // 
            this.PointFilterToolStripMenuItem.Name = "PointFilterToolStripMenuItem";
            this.PointFilterToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.PointFilterToolStripMenuItem.Text = "点";
            this.PointFilterToolStripMenuItem.Click += new System.EventHandler(this.PointFilterToolStripMenuItem_Click);
            // 
            // 过滤依据ToolStripMenuItem
            // 
            this.过滤依据ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PointFilterToolStripMenuItem,
            this.LineFilterToolStripMenuItem,
            this.FillFilterToolStripMenuItem,
            this.AnnoFilterToolStripMenuItem,
            this.toolStripSeparator2,
            this.LayerFilterToolStripMenuItem});
            this.过滤依据ToolStripMenuItem.Name = "过滤依据ToolStripMenuItem";
            this.过滤依据ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.过滤依据ToolStripMenuItem.Text = "过滤依据";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(145, 6);
            // 
            // ShowAllToolStripMenuItem
            // 
            this.ShowAllToolStripMenuItem.Name = "ShowAllToolStripMenuItem";
            this.ShowAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ShowAllToolStripMenuItem.Text = "显示所有模板";
            this.ShowAllToolStripMenuItem.Click += new System.EventHandler(this.显示所有模板ToolStripMenuItem_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowAllToolStripMenuItem,
            this.toolStripSeparator3,
            this.过滤依据ToolStripMenuItem,
            this.toolStripSeparator4,
            this.分组依据ToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "通过分组和过滤排列模板";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(394, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // EditorTemplateManageCtrl2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "EditorTemplateManageCtrl2";
            this.Size = new System.Drawing.Size(394, 563);
            this.Load += new System.EventHandler(this.EditorTemplateManageCtrl_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem PropertyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private EditTemplateListView listView1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem 清除分组ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem LayerGroupToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem GeometryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 分组依据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem LayerFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem AnnoFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FillFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LineFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PointFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 过滤依据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem ShowAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStrip toolStrip1;
    }
}
