namespace Yutai.Views
{
    partial class MainView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            Syncfusion.Windows.Forms.Tools.Office2013ColorTable office2013ColorTable1 = new Syncfusion.Windows.Forms.Tools.Office2013ColorTable();
            Syncfusion.Windows.Forms.Tools.ToolTipInfo toolTipInfo1 = new Syncfusion.Windows.Forms.Tools.ToolTipInfo();
            this._dockingManager1 = new Syncfusion.Windows.Forms.Tools.DockingManager(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.parentBarItem3 = new Syncfusion.Windows.Forms.Tools.XPMenus.ParentBarItem();
            this.dockingClientPanel1 = new Syncfusion.Windows.Forms.Tools.DockingClientPanel();
            this.mapContainer = new Syncfusion.Windows.Forms.Tools.TabSplitterContainer();
            this.mapPage = new Syncfusion.Windows.Forms.Tools.TabSplitterPage();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.scenePage = new Syncfusion.Windows.Forms.Tools.TabSplitterPage();
            this.axSceneControl1 = new ESRI.ArcGIS.Controls.AxSceneControl();
            this._mainFrameBarManager1 = new Syncfusion.Windows.Forms.Tools.XPMenus.MainFrameBarManager(this);
            this.statusStripEx1 = new Syncfusion.Windows.Forms.Tools.StatusStripEx();
            this.statusStripLabel5 = new Syncfusion.Windows.Forms.Tools.StatusStripLabel();
            this.statusMapUnits = new Syncfusion.Windows.Forms.Tools.StatusStripLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.superToolTip1 = new Syncfusion.Windows.Forms.Tools.SuperToolTip(this);
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.ribbonControlAdv1 = new Syncfusion.Windows.Forms.Tools.RibbonControlAdv();
            ((System.ComponentModel.ISupportInitialize)(this._dockingManager1)).BeginInit();
            this.dockingClientPanel1.SuspendLayout();
            this.mapContainer.SuspendLayout();
            this.mapPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            this.scenePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axSceneControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._mainFrameBarManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControlAdv1)).BeginInit();
            this.SuspendLayout();
            // 
            // _dockingManager1
            // 
            this._dockingManager1.ActiveCaptionFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this._dockingManager1.AutoHideTabForeColor = System.Drawing.Color.Empty;
            this._dockingManager1.DockedCaptionFont = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this._dockingManager1.DockLayoutStream = ((System.IO.MemoryStream)(resources.GetObject("_dockingManager1.DockLayoutStream")));
            this._dockingManager1.DockTabAlignment = Syncfusion.Windows.Forms.Tools.DockTabAlignmentStyle.Left;
            this._dockingManager1.DockTabForeColor = System.Drawing.Color.Empty;
            this._dockingManager1.DockTabHeight = 26;
            this._dockingManager1.HostControl = this;
            this._dockingManager1.ImageList = this.imageList1;
            this._dockingManager1.InActiveCaptionBackground = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212))))));
            this._dockingManager1.InActiveCaptionFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this._dockingManager1.MetroButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._dockingManager1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this._dockingManager1.MetroSplitterBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(159)))), ((int)(((byte)(183)))));
            this._dockingManager1.ReduceFlickeringInRtl = false;
            this._dockingManager1.VisualStyle = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this._dockingManager1.CaptionButtons.Add(new Syncfusion.Windows.Forms.Tools.CaptionButton(Syncfusion.Windows.Forms.Tools.CaptionButtonType.Close, "CloseButton"));
            this._dockingManager1.CaptionButtons.Add(new Syncfusion.Windows.Forms.Tools.CaptionButton(Syncfusion.Windows.Forms.Tools.CaptionButtonType.Pin, "PinButton"));
            this._dockingManager1.CaptionButtons.Add(new Syncfusion.Windows.Forms.Tools.CaptionButton(Syncfusion.Windows.Forms.Tools.CaptionButtonType.Maximize, "MaximizeButton"));
            this._dockingManager1.CaptionButtons.Add(new Syncfusion.Windows.Forms.Tools.CaptionButton(Syncfusion.Windows.Forms.Tools.CaptionButtonType.Restore, "RestoreButton"));
            this._dockingManager1.CaptionButtons.Add(new Syncfusion.Windows.Forms.Tools.CaptionButton(Syncfusion.Windows.Forms.Tools.CaptionButtonType.Menu, "MenuButton"));
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icon_edit_copy.png");
            this.imageList1.Images.SetKeyName(1, "icon_edit_cut.png");
            // 
            // parentBarItem3
            // 
            this.parentBarItem3.BarName = "parentBarItem3";
            this.parentBarItem3.CategoryIndex = 1;
            this.parentBarItem3.ID = "Trial";
            this.parentBarItem3.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.parentBarItem3.ShowToolTipInPopUp = false;
            this.parentBarItem3.SizeToFit = true;
            this.parentBarItem3.Style = Syncfusion.Windows.Forms.VisualStyle.Metro;
            this.parentBarItem3.Text = "Trial";
            this.parentBarItem3.WrapLength = 20;
            // 
            // dockingClientPanel1
            // 
            this.dockingClientPanel1.BackColor = System.Drawing.Color.White;
            this.dockingClientPanel1.Controls.Add(this.mapContainer);
            this.dockingClientPanel1.Location = new System.Drawing.Point(298, 81);
            this.dockingClientPanel1.Name = "dockingClientPanel1";
            this.dockingClientPanel1.Size = new System.Drawing.Size(434, 312);
            this.dockingClientPanel1.TabIndex = 4;
            // 
            // mapContainer
            // 
            this.mapContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapContainer.Location = new System.Drawing.Point(0, 0);
            this.mapContainer.Name = "mapContainer";
            this.mapContainer.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.mapContainer.PrimaryPages.AddRange(new Syncfusion.Windows.Forms.Tools.TabSplitterPage[] {
            this.mapPage});
            this.mapContainer.SecondaryPages.AddRange(new Syncfusion.Windows.Forms.Tools.TabSplitterPage[] {
            this.scenePage});
            this.mapContainer.Size = new System.Drawing.Size(434, 312);
            this.mapContainer.SplitterBackColor = System.Drawing.Color.White;
            this.mapContainer.SplitterPosition = 217;
            this.mapContainer.TabIndex = 0;
            this.mapContainer.Text = "地图视图";
            // 
            // mapPage
            // 
            this.mapPage.AutoScroll = true;
            this.mapPage.Controls.Add(this.axMapControl1);
            this.mapPage.Hide = false;
            this.mapPage.Location = new System.Drawing.Point(0, 0);
            this.mapPage.Name = "mapPage";
            this.mapPage.Size = new System.Drawing.Size(217, 312);
            this.mapPage.TabIndex = 1;
            this.mapPage.Text = "二维窗口";
            // 
            // axMapControl1
            // 
            this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(0, 0);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(217, 312);
            this.axMapControl1.TabIndex = 0;
            // 
            // scenePage
            // 
            this.scenePage.AutoScroll = true;
            this.scenePage.Controls.Add(this.axSceneControl1);
            this.scenePage.Hide = false;
            this.scenePage.Location = new System.Drawing.Point(237, 0);
            this.scenePage.Name = "scenePage";
            this.scenePage.Size = new System.Drawing.Size(197, 312);
            this.scenePage.TabIndex = 2;
            this.scenePage.Text = "三维窗口";
            // 
            // axSceneControl1
            // 
            this.axSceneControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axSceneControl1.Location = new System.Drawing.Point(0, 0);
            this.axSceneControl1.Name = "axSceneControl1";
            this.axSceneControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSceneControl1.OcxState")));
            this.axSceneControl1.Size = new System.Drawing.Size(197, 312);
            this.axSceneControl1.TabIndex = 0;
            // 
            // _mainFrameBarManager1
            // 
            this._mainFrameBarManager1.AutoLoadToolBarPositions = false;
            this._mainFrameBarManager1.AutoPersistCustomization = false;
            this._mainFrameBarManager1.AutoScale = true;
            this._mainFrameBarManager1.BarPositionInfo = ((System.IO.MemoryStream)(resources.GetObject("_mainFrameBarManager1.BarPositionInfo")));
            this._mainFrameBarManager1.CurrentBaseFormType = "Yutai.UI.Forms.MapWindowView";
            this._mainFrameBarManager1.EnableMenuMerge = true;
            this._mainFrameBarManager1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._mainFrameBarManager1.Form = this;
            this._mainFrameBarManager1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(201)))), ((int)(((byte)(232)))));
            this._mainFrameBarManager1.ResetCustomization = false;
            this._mainFrameBarManager1.UseBackwardCompatiblity = false;
            // 
            // statusStripEx1
            // 
            this.statusStripEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(206)))), ((int)(((byte)(255)))));
            this.statusStripEx1.BeforeTouchSize = new System.Drawing.Size(794, 22);
            this.statusStripEx1.Dock = Syncfusion.Windows.Forms.Tools.DockStyleEx.Bottom;
            this.statusStripEx1.Location = new System.Drawing.Point(1, 467);
            this.statusStripEx1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(206)))), ((int)(((byte)(255)))));
            this.statusStripEx1.Name = "statusStripEx1";
            this.statusStripEx1.Size = new System.Drawing.Size(794, 22);
            this.statusStripEx1.TabIndex = 1;
            this.statusStripEx1.Text = "statusStripEx1";
            // 
            // statusStripLabel5
            // 
            this.statusStripLabel5.Margin = new System.Windows.Forms.Padding(0, 4, 0, 2);
            this.statusStripLabel5.Name = "statusStripLabel5";
            this.SetShortcut(this.statusStripLabel5, System.Windows.Forms.Keys.None);
            this.statusStripLabel5.Size = new System.Drawing.Size(10, 15);
            this.statusStripLabel5.Text = "|";
            // 
            // statusMapUnits
            // 
            this.statusMapUnits.Margin = new System.Windows.Forms.Padding(0, 4, 0, 2);
            this.statusMapUnits.Name = "statusMapUnits";
            this.SetShortcut(this.statusMapUnits, System.Windows.Forms.Keys.None);
            this.statusMapUnits.Size = new System.Drawing.Size(61, 15);
            this.statusMapUnits.Text = "地图单位";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.SetShortcut(this.toolStripStatusLabel3, System.Windows.Forms.Keys.None);
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(52, 15);
            this.toolStripStatusLabel3.Text = "进度";
            // 
            // statusProgress
            // 
            this.statusProgress.Name = "statusProgress";
            this.SetShortcut(this.statusProgress, System.Windows.Forms.Keys.None);
            this.statusProgress.Size = new System.Drawing.Size(100, 15);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.SetShortcut(this.toolStripMenuItem6, System.Windows.Forms.Keys.None);
            this.toolStripMenuItem6.Size = new System.Drawing.Size(181, 22);
            this.toolStripMenuItem6.Text = "属性";
            // 
            // superToolTip1
            // 
            this.superToolTip1.MetroColor = System.Drawing.Color.White;
            this.superToolTip1.Style = Syncfusion.Windows.Forms.Tools.SuperToolTip.SuperToolTipStyle.Office2013Style;
            this.superToolTip1.UseFading = Syncfusion.Windows.Forms.Tools.SuperToolTip.FadingType.System;
            this.superToolTip1.VisualStyle = Syncfusion.Windows.Forms.Tools.SuperToolTip.Appearance.Metro;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(124, 344);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 5;
            // 
            // ribbonControlAdv1
            // 
            this.ribbonControlAdv1.CaptionFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ribbonControlAdv1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.ribbonControlAdv1.Location = new System.Drawing.Point(1, 1);
            this.ribbonControlAdv1.MenuButtonFont = new System.Drawing.Font("Segoe UI", 8.25F);
            this.ribbonControlAdv1.MenuButtonText = "项目";
            this.ribbonControlAdv1.MenuButtonWidth = 56;
            this.ribbonControlAdv1.MenuColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(198)))));
            this.ribbonControlAdv1.Name = "ribbonControlAdv1";
            office2013ColorTable1.BackStageCaptionColor = System.Drawing.Color.White;
            office2013ColorTable1.ButtonBackgroundPressed = System.Drawing.Color.Empty;
            office2013ColorTable1.ButtonBackgroundSelected = System.Drawing.Color.Empty;
            office2013ColorTable1.CaptionBackColor = System.Drawing.Color.White;
            office2013ColorTable1.CaptionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            office2013ColorTable1.CheckedTabColor = System.Drawing.Color.White;
            office2013ColorTable1.CheckedTabForeColor = System.Drawing.Color.Empty;
            office2013ColorTable1.CloseButtonColor = System.Drawing.Color.Empty;
            office2013ColorTable1.ContextMenuBackColor = System.Drawing.Color.Empty;
            office2013ColorTable1.ContextMenuItemSelected = System.Drawing.Color.Empty;
            office2013ColorTable1.HeaderColor = System.Drawing.Color.White;
            office2013ColorTable1.HoverTabForeColor = System.Drawing.Color.Empty;
            office2013ColorTable1.LauncherBackColorSelected = System.Drawing.Color.Empty;
            office2013ColorTable1.LauncherColorNormal = System.Drawing.Color.Empty;
            office2013ColorTable1.LauncherColorSelected = System.Drawing.Color.Empty;
            office2013ColorTable1.MaximizeButtonColor = System.Drawing.Color.Empty;
            office2013ColorTable1.MinimizeButtonColor = System.Drawing.Color.Empty;
            office2013ColorTable1.PanelBackColor = System.Drawing.Color.White;
            office2013ColorTable1.RestoreButtonColor = System.Drawing.Color.Empty;
            office2013ColorTable1.RibbonPanelBorderColor = System.Drawing.Color.Empty;
            office2013ColorTable1.SelectedTabBorderColor = System.Drawing.Color.White;
            office2013ColorTable1.SelectedTabColor = System.Drawing.Color.White;
            office2013ColorTable1.SplitButtonBackgroundPressed = System.Drawing.Color.Empty;
            office2013ColorTable1.SplitButtonBackgroundSelected = System.Drawing.Color.Empty;
            office2013ColorTable1.SystemButtonBackground = System.Drawing.Color.Empty;
            office2013ColorTable1.TabBackColor = System.Drawing.Color.White;
            office2013ColorTable1.TabForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(2)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            office2013ColorTable1.TitleColor = System.Drawing.Color.Empty;
            office2013ColorTable1.ToolStripBackColor = System.Drawing.Color.White;
            office2013ColorTable1.ToolStripBorderColor = System.Drawing.Color.White;
            office2013ColorTable1.ToolStripItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            office2013ColorTable1.ToolStripSpliterColor = System.Drawing.Color.LightGray;
            office2013ColorTable1.UpDownButtonBackColor = System.Drawing.Color.Empty;
            this.ribbonControlAdv1.Office2013ColorTable = office2013ColorTable1;
            this.ribbonControlAdv1.OfficeColorScheme = Syncfusion.Windows.Forms.Tools.ToolStripEx.ColorScheme.Silver;
            // 
            // ribbonControlAdv1.OfficeMenu
            // 
            this.ribbonControlAdv1.OfficeMenu.Name = "OfficeMenu";
            this.ribbonControlAdv1.OfficeMenu.ShowItemToolTips = true;
            this.ribbonControlAdv1.OfficeMenu.Size = new System.Drawing.Size(12, 65);
            this.ribbonControlAdv1.QuickPanelImageLayout = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ribbonControlAdv1.RibbonHeaderImage = Syncfusion.Windows.Forms.Tools.RibbonHeaderImage.None;
            this.ribbonControlAdv1.RibbonStyle = Syncfusion.Windows.Forms.Tools.RibbonStyle.Office2013;
            this.ribbonControlAdv1.SelectedTab = null;
            this.ribbonControlAdv1.ShowRibbonDisplayOptionButton = true;
            this.ribbonControlAdv1.Size = new System.Drawing.Size(798, 141);
            this.ribbonControlAdv1.SystemText.QuickAccessDialogDropDownName = "Start menu";
            this.ribbonControlAdv1.TabIndex = 6;
            this.ribbonControlAdv1.Text = "ribbonControlAdv1";
            this.ribbonControlAdv1.TitleColor = System.Drawing.Color.Black;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(796, 490);
            this.ColorScheme = Syncfusion.Windows.Forms.Tools.RibbonForm.ColorSchemeType.Silver;
            this.Controls.Add(this.ribbonControlAdv1);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.dockingClientPanel1);
            this.Controls.Add(this.statusStripEx1);
            this.Name = "MainView";
            this.Padding = new System.Windows.Forms.Padding(1, 0, 1, 1);
            this.Text = "Yutai 地理信息平台";
            toolTipInfo1.Body.Size = new System.Drawing.Size(20, 20);
            toolTipInfo1.Footer.Size = new System.Drawing.Size(20, 20);
            toolTipInfo1.Header.Size = new System.Drawing.Size(20, 20);
            this.superToolTip1.SetToolTip(this, toolTipInfo1);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this._dockingManager1)).EndInit();
            this.dockingClientPanel1.ResumeLayout(false);
            this.mapContainer.ResumeLayout(false);
            this.mapPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            this.scenePage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axSceneControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._mainFrameBarManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControlAdv1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.DockingManager _dockingManager1;
        private Syncfusion.Windows.Forms.Tools.XPMenus.ParentBarItem parentBarItem3;
        private Syncfusion.Windows.Forms.Tools.DockingClientPanel dockingClientPanel1;
        private Syncfusion.Windows.Forms.Tools.XPMenus.MainFrameBarManager _mainFrameBarManager1;
        private Syncfusion.Windows.Forms.Tools.StatusStripLabel statusStripLabel5;
        private Syncfusion.Windows.Forms.Tools.StatusStripLabel statusMapUnits;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripProgressBar statusProgress;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private Syncfusion.Windows.Forms.Tools.SuperToolTip superToolTip1;
        private Syncfusion.Windows.Forms.Tools.StatusStripEx statusStripEx1;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private Syncfusion.Windows.Forms.Tools.TabSplitterContainer mapContainer;
        private Syncfusion.Windows.Forms.Tools.TabSplitterPage mapPage;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private Syncfusion.Windows.Forms.Tools.TabSplitterPage scenePage;
        private ESRI.ArcGIS.Controls.AxSceneControl axSceneControl1;
        private Syncfusion.Windows.Forms.Tools.RibbonControlAdv ribbonControlAdv1;
        
    }
}
