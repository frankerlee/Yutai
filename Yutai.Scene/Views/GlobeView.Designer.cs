namespace Yutai.Plugins.Scene.Views
{
    partial class GlobeView
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GlobeView));
            this.axGlobeControl1 = new ESRI.ArcGIS.Controls.AxGlobeControl();
            ((System.ComponentModel.ISupportInitialize)(this.axGlobeControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // axGlobeControl1
            // 
            this.axGlobeControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axGlobeControl1.Location = new System.Drawing.Point(0, 0);
            this.axGlobeControl1.Name = "axGlobeControl1";
            this.axGlobeControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axGlobeControl1.OcxState")));
            this.axGlobeControl1.Size = new System.Drawing.Size(392, 464);
            this.axGlobeControl1.TabIndex = 0;
            // 
            // GlobeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.axGlobeControl1);
            this.Name = "GlobeView";
            this.Size = new System.Drawing.Size(392, 464);
            ((System.ComponentModel.ISupportInitialize)(this.axGlobeControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxGlobeControl axGlobeControl1;
    }
}
