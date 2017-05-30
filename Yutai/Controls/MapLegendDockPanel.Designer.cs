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
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
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
            this.contextMenuLayer.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(236)))), ((int)(((byte)(249)))));
            this.contextMenuLayer.Name = "contextMenuLayer";
            this.contextMenuLayer.Size = new System.Drawing.Size(61, 4);
            this.contextMenuLayer.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Default;
            this.contextMenuLayer.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuLayer_Opening);
            // 
            // MapLegendDockPanel
            // 
            this.Controls.Add(this.axTOCControl1);
            this.Name = "MapLegendDockPanel";
            this.Size = new System.Drawing.Size(259, 264);
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private Syncfusion.Windows.Forms.Tools.ContextMenuStripEx contextMenuLayer;
    }
}
