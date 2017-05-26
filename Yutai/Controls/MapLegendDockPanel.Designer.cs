namespace Yutai.Controls
{
    partial class MapLegendDockPanel
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapLegendDockPanel));
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.contextMenuLayer = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            this.mnuZoomToLayer = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            this.contextMenuLayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.ContextMenuStrip = this.contextMenuLayer;
            this.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axTOCControl1.Location = new System.Drawing.Point(0, 0);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(259, 264);
            this.axTOCControl1.TabIndex = 0;
            // 
            // contextMenuLayer
            // 
            this.contextMenuLayer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuZoomToLayer});
            this.contextMenuLayer.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(236)))), ((int)(((byte)(249)))));
            this.contextMenuLayer.Name = "contextMenuLayer";
            this.contextMenuLayer.Size = new System.Drawing.Size(153, 48);
            this.contextMenuLayer.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Default;
            // 
            // mnuZoomToLayer
            // 
            this.mnuZoomToLayer.Name = "mnuZoomToLayer";
            this.mnuZoomToLayer.Size = new System.Drawing.Size(152, 22);
            this.mnuZoomToLayer.Text = "放大到图层";
            // 
            // MapLegendDockPanel
            // 
            this.Controls.Add(this.axTOCControl1);
            this.Name = "MapLegendDockPanel";
            this.Size = new System.Drawing.Size(259, 264);
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            this.contextMenuLayer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private Syncfusion.Windows.Forms.Tools.ContextMenuStripEx contextMenuLayer;
        private System.Windows.Forms.ToolStripMenuItem mnuZoomToLayer;
    }
}
