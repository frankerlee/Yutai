namespace Yutai.Views
{
    partial class NewMainView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewMainView));
            this.ribbonManager = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.tabContent = new DevExpress.XtraTab.XtraTabControl();
            this.pageMap = new DevExpress.XtraTab.XtraTabPage();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.dockManager = new DevExpress.XtraBars.Docking.DockManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabContent)).BeginInit();
            this.tabContent.SuspendLayout();
            this.pageMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonManager
            // 
            this.ribbonManager.ExpandCollapseItem.Id = 0;
            this.ribbonManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonManager.ExpandCollapseItem});
            this.ribbonManager.Location = new System.Drawing.Point(0, 0);
            this.ribbonManager.MaxItemId = 1;
            this.ribbonManager.Name = "ribbonManager";
            this.ribbonManager.Size = new System.Drawing.Size(1128, 50);
            this.ribbonManager.StatusBar = this.ribbonStatusBar;
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 653);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbonManager;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1128, 31);
            // 
            // tabContent
            // 
            this.tabContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabContent.Location = new System.Drawing.Point(0, 50);
            this.tabContent.Name = "tabContent";
            this.tabContent.SelectedTabPage = this.pageMap;
            this.tabContent.Size = new System.Drawing.Size(1128, 603);
            this.tabContent.TabIndex = 2;
            this.tabContent.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.pageMap});
            // 
            // pageMap
            // 
            this.pageMap.Controls.Add(this.axLicenseControl1);
            this.pageMap.Controls.Add(this.axMapControl1);
            this.pageMap.Name = "pageMap";
            this.pageMap.Size = new System.Drawing.Size(1122, 574);
            this.pageMap.Text = "地图窗口";
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(364, 422);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 0;
            // 
            // axMapControl1
            // 
            this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(0, 0);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(1122, 574);
            this.axMapControl1.TabIndex = 1;
            // 
            // dockManager
            // 
            this.dockManager.Form = this;
            this.dockManager.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl"});
            // 
            // NewMainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1128, 684);
            this.Controls.Add(this.tabContent);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbonManager);
            this.Name = "NewMainView";
            this.Ribbon = this.ribbonManager;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "NewMainView";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabContent)).EndInit();
            this.tabContent.ResumeLayout(false);
            this.pageMap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonManager;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraTab.XtraTabControl tabContent;
        private DevExpress.XtraTab.XtraTabPage pageMap;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private DevExpress.XtraBars.Docking.DockManager dockManager;
    }
}