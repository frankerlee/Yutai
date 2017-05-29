namespace Yutai.Controls
{
    partial class OverviewDockPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OverviewDockPanel));
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.contextMenuOverview = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            this.mnuFullExtent = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCurrent = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            this.contextMenuOverview.SuspendLayout();
            this.SuspendLayout();
            // 
            // axMapControl1
            // 
            this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(0, 0);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(291, 277);
            this.axMapControl1.TabIndex = 1;
            this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            this.axMapControl1.OnMouseUp += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseUpEventHandler(this.axMapControl1_OnMouseUp);
            this.axMapControl1.Resize += new System.EventHandler(this.axMapControl1_Resize);
            // 
            // contextMenuOverview
            // 
            this.contextMenuOverview.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFullExtent,
            this.mnuCurrent});
            this.contextMenuOverview.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(236)))), ((int)(((byte)(249)))));
            this.contextMenuOverview.Name = "contextMenuOverview";
            this.contextMenuOverview.Size = new System.Drawing.Size(153, 70);
            this.contextMenuOverview.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Default;
            // 
            // mnuFullExtent
            // 
            this.mnuFullExtent.Name = "mnuFullExtent";
            this.mnuFullExtent.Size = new System.Drawing.Size(100, 22);
            this.mnuFullExtent.Text = "全图";
            // 
            // mnuCurrent
            // 
            this.mnuCurrent.Name = "mnuCurrent";
            this.mnuCurrent.Size = new System.Drawing.Size(100, 22);
            this.mnuCurrent.Text = "当前";
            // 
            // OverviewDockPanel
            // 
            this.Controls.Add(this.axMapControl1);
            this.Name = "OverviewDockPanel";
            this.Size = new System.Drawing.Size(291, 277);
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            this.contextMenuOverview.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private Syncfusion.Windows.Forms.Tools.ContextMenuStripEx contextMenuOverview;
        private System.Windows.Forms.ToolStripMenuItem mnuFullExtent;
        private System.Windows.Forms.ToolStripMenuItem mnuCurrent;
    }
}
