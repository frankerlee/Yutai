using System.Windows.Forms;

namespace Yutai.Check.Views
{
    partial class CheckResultDockPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckResultDockPanel));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonExport = new System.Windows.Forms.ToolStripButton();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.toolSelected = new System.Windows.Forms.ToolStripButton();
            this.toolPanTo = new System.Windows.Forms.ToolStripButton();
            this.toolZoomTo = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSelected,
            this.toolPanTo,
            this.toolZoomTo,
            this.toolStripButtonExport});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(708, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonExport
            // 
            this.toolStripButtonExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonExport.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonExport.Image")));
            this.toolStripButtonExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExport.Name = "toolStripButtonExport";
            this.toolStripButtonExport.Size = new System.Drawing.Size(60, 22);
            this.toolStripButtonExport.Text = "导出结果";
            this.toolStripButtonExport.Click += new System.EventHandler(this.toolStripButtonExport_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 25);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(708, 385);
            this.mainPanel.TabIndex = 1;
            // 
            // toolSelected
            // 
            this.toolSelected.Checked = true;
            this.toolSelected.CheckOnClick = true;
            this.toolSelected.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolSelected.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolSelected.Image = ((System.Drawing.Image)(resources.GetObject("toolSelected.Image")));
            this.toolSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSelected.Name = "toolSelected";
            this.toolSelected.Size = new System.Drawing.Size(36, 22);
            this.toolSelected.Text = "选择";
            this.toolSelected.Click += new System.EventHandler(this.toolSelected_Click);
            // 
            // toolPanTo
            // 
            this.toolPanTo.Checked = true;
            this.toolPanTo.CheckOnClick = true;
            this.toolPanTo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolPanTo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolPanTo.Image = ((System.Drawing.Image)(resources.GetObject("toolPanTo.Image")));
            this.toolPanTo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolPanTo.Name = "toolPanTo";
            this.toolPanTo.Size = new System.Drawing.Size(36, 22);
            this.toolPanTo.Text = "平移";
            this.toolPanTo.Click += new System.EventHandler(this.toolPanTo_Click);
            // 
            // toolZoomTo
            // 
            this.toolZoomTo.CheckOnClick = true;
            this.toolZoomTo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolZoomTo.Image = ((System.Drawing.Image)(resources.GetObject("toolZoomTo.Image")));
            this.toolZoomTo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolZoomTo.Name = "toolZoomTo";
            this.toolZoomTo.Size = new System.Drawing.Size(36, 22);
            this.toolZoomTo.Text = "缩放";
            this.toolZoomTo.Click += new System.EventHandler(this.toolZoomTo_Click);
            // 
            // CheckResultDockPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.toolStrip1);
            this.Name = "CheckResultDockPanel";
            this.Size = new System.Drawing.Size(708, 410);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.ToolStripButton toolStripButtonExport;
        private ToolStripButton toolSelected;
        private ToolStripButton toolPanTo;
        private ToolStripButton toolZoomTo;
    }
}
