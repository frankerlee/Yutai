namespace Yutai.Plugins.Printing.Views
{
    partial class AutoLayoutView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoLayoutView));
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
            this.cmbScale = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cmbMapTemplate = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.btnClearPage = new DevExpress.XtraEditors.SimpleButton();
            this.btnQueryPage = new DevExpress.XtraEditors.SimpleButton();
            this.grpFence = new DevExpress.XtraEditors.GroupControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPoint = new DevExpress.XtraEditors.SimpleButton();
            this.btnCircle = new DevExpress.XtraEditors.SimpleButton();
            this.btnRectangle = new DevExpress.XtraEditors.SimpleButton();
            this.btnPolygon = new DevExpress.XtraEditors.SimpleButton();
            this.btnPolyline = new DevExpress.XtraEditors.SimpleButton();
            this.btnLineBuffer = new DevExpress.XtraEditors.SimpleButton();
            this.btnExtent = new DevExpress.XtraEditors.SimpleButton();
            this.btnClearClip = new DevExpress.XtraEditors.SimpleButton();
            this.grpKey = new DevExpress.XtraEditors.GroupControl();
            this.txtSearchKey = new DevExpress.XtraEditors.TextEdit();
            this.grpIndexMap = new DevExpress.XtraEditors.GroupControl();
            this.cmbIndexLayer = new DevExpress.XtraEditors.ComboBoxEdit();
            this.rdoSelectMode = new DevExpress.XtraEditors.RadioGroup();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lstPages = new System.Windows.Forms.CheckedListBox();
            this.groupControl6 = new DevExpress.XtraEditors.GroupControl();
            this.btnBatchPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeletePage = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnZoom = new DevExpress.XtraEditors.SimpleButton();
            this.btnLast = new DevExpress.XtraEditors.SimpleButton();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.btnPre = new DevExpress.XtraEditors.SimpleButton();
            this.btnFirst = new DevExpress.XtraEditors.SimpleButton();
            this.propertyPage = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).BeginInit();
            this.groupControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbScale.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMapTemplate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpFence)).BeginInit();
            this.grpFence.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpKey)).BeginInit();
            this.grpKey.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpIndexMap)).BeginInit();
            this.grpIndexMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbIndexLayer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoSelectMode.Properties)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).BeginInit();
            this.groupControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertyPage)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(307, 649);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.groupControl5);
            this.xtraTabPage1.Controls.Add(this.groupControl4);
            this.xtraTabPage1.Controls.Add(this.grpFence);
            this.xtraTabPage1.Controls.Add(this.grpKey);
            this.xtraTabPage1.Controls.Add(this.grpIndexMap);
            this.xtraTabPage1.Controls.Add(this.rdoSelectMode);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(301, 620);
            this.xtraTabPage1.Text = "制图范围";
            // 
            // groupControl5
            // 
            this.groupControl5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl5.Controls.Add(this.cmbScale);
            this.groupControl5.Controls.Add(this.labelControl2);
            this.groupControl5.Controls.Add(this.cmbMapTemplate);
            this.groupControl5.Controls.Add(this.labelControl1);
            this.groupControl5.Location = new System.Drawing.Point(7, 7);
            this.groupControl5.Name = "groupControl5";
            this.groupControl5.Size = new System.Drawing.Size(288, 89);
            this.groupControl5.TabIndex = 5;
            this.groupControl5.Text = "模板设置";
            // 
            // cmbScale
            // 
            this.cmbScale.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbScale.Location = new System.Drawing.Point(74, 59);
            this.cmbScale.Margin = new System.Windows.Forms.Padding(8, 3, 3, 3);
            this.cmbScale.Name = "cmbScale";
            this.cmbScale.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbScale.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.cmbScale.Size = new System.Drawing.Size(209, 20);
            this.cmbScale.TabIndex = 4;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(10, 62);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(63, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "比例尺    1:";
            // 
            // cmbMapTemplate
            // 
            this.cmbMapTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMapTemplate.Location = new System.Drawing.Point(50, 28);
            this.cmbMapTemplate.Name = "cmbMapTemplate";
            this.cmbMapTemplate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMapTemplate.Size = new System.Drawing.Size(233, 20);
            this.cmbMapTemplate.TabIndex = 1;
            this.cmbMapTemplate.SelectedIndexChanged += new System.EventHandler(this.cmbMapTemplate_SelectedIndexChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(11, 31);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "模板";
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.btnClearPage);
            this.groupControl4.Controls.Add(this.btnQueryPage);
            this.groupControl4.Location = new System.Drawing.Point(7, 395);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(288, 64);
            this.groupControl4.TabIndex = 4;
            this.groupControl4.Text = "执行";
            // 
            // btnClearPage
            // 
            this.btnClearPage.Location = new System.Drawing.Point(100, 30);
            this.btnClearPage.Name = "btnClearPage";
            this.btnClearPage.Size = new System.Drawing.Size(82, 29);
            this.btnClearPage.TabIndex = 1;
            this.btnClearPage.Text = "清除";
            this.btnClearPage.Click += new System.EventHandler(this.btnClearPage_Click);
            // 
            // btnQueryPage
            // 
            this.btnQueryPage.Location = new System.Drawing.Point(12, 30);
            this.btnQueryPage.Name = "btnQueryPage";
            this.btnQueryPage.Size = new System.Drawing.Size(82, 29);
            this.btnQueryPage.TabIndex = 0;
            this.btnQueryPage.Text = "执行查询";
            this.btnQueryPage.Click += new System.EventHandler(this.btnSearchKey_Click);
            // 
            // grpFence
            // 
            this.grpFence.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFence.Controls.Add(this.flowLayoutPanel1);
            this.grpFence.Location = new System.Drawing.Point(7, 289);
            this.grpFence.Name = "grpFence";
            this.grpFence.Size = new System.Drawing.Size(288, 100);
            this.grpFence.TabIndex = 3;
            this.grpFence.Text = "范围工具";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.btnPoint);
            this.flowLayoutPanel1.Controls.Add(this.btnCircle);
            this.flowLayoutPanel1.Controls.Add(this.btnRectangle);
            this.flowLayoutPanel1.Controls.Add(this.btnPolygon);
            this.flowLayoutPanel1.Controls.Add(this.btnPolyline);
            this.flowLayoutPanel1.Controls.Add(this.btnLineBuffer);
            this.flowLayoutPanel1.Controls.Add(this.btnExtent);
            this.flowLayoutPanel1.Controls.Add(this.btnClearClip);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(10, 24);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(273, 65);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // btnPoint
            // 
            this.btnPoint.Image = global::Yutai.Plugins.Printing.Properties.Resources.icon_fence_point;
            this.btnPoint.Location = new System.Drawing.Point(3, 3);
            this.btnPoint.Name = "btnPoint";
            this.btnPoint.Size = new System.Drawing.Size(32, 28);
            this.btnPoint.TabIndex = 0;
            this.btnPoint.ToolTip = "点加半径";
            this.btnPoint.Click += new System.EventHandler(this.btnPoint_Click);
            // 
            // btnCircle
            // 
            this.btnCircle.Image = global::Yutai.Plugins.Printing.Properties.Resources.icon_fence_circle;
            this.btnCircle.Location = new System.Drawing.Point(41, 3);
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(32, 28);
            this.btnCircle.TabIndex = 1;
            this.btnCircle.ToolTip = "圆形绘图区域";
            this.btnCircle.Click += new System.EventHandler(this.btnCircle_Click);
            // 
            // btnRectangle
            // 
            this.btnRectangle.Image = global::Yutai.Plugins.Printing.Properties.Resources.icon_fence_rectangle;
            this.btnRectangle.Location = new System.Drawing.Point(79, 3);
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.Size = new System.Drawing.Size(32, 28);
            this.btnRectangle.TabIndex = 2;
            this.btnRectangle.ToolTip = "矩形绘图区域";
            this.btnRectangle.Click += new System.EventHandler(this.btnRectangle_Click);
            // 
            // btnPolygon
            // 
            this.btnPolygon.Image = global::Yutai.Plugins.Printing.Properties.Resources.icon_fence_polygon;
            this.btnPolygon.Location = new System.Drawing.Point(117, 3);
            this.btnPolygon.Name = "btnPolygon";
            this.btnPolygon.Size = new System.Drawing.Size(32, 28);
            this.btnPolygon.TabIndex = 4;
            this.btnPolygon.ToolTip = "多边形绘图区域";
            this.btnPolygon.Click += new System.EventHandler(this.btnPolygon_Click);
            // 
            // btnPolyline
            // 
            this.btnPolyline.Image = global::Yutai.Plugins.Printing.Properties.Resources.icon_fence_line;
            this.btnPolyline.Location = new System.Drawing.Point(155, 3);
            this.btnPolyline.Name = "btnPolyline";
            this.btnPolyline.Size = new System.Drawing.Size(32, 28);
            this.btnPolyline.TabIndex = 4;
            this.btnPolyline.ToolTip = "线形绘图区域";
            this.btnPolyline.Click += new System.EventHandler(this.btnPolyline_Click);
            // 
            // btnLineBuffer
            // 
            this.btnLineBuffer.Image = global::Yutai.Plugins.Printing.Properties.Resources.icon_line_buffer;
            this.btnLineBuffer.Location = new System.Drawing.Point(193, 3);
            this.btnLineBuffer.Name = "btnLineBuffer";
            this.btnLineBuffer.Size = new System.Drawing.Size(32, 28);
            this.btnLineBuffer.TabIndex = 5;
            this.btnLineBuffer.ToolTip = "线加缓冲半径绘图区域";
            this.btnLineBuffer.Click += new System.EventHandler(this.btnLineBuffer_Click);
            // 
            // btnExtent
            // 
            this.btnExtent.Image = global::Yutai.Plugins.Printing.Properties.Resources.icon_fence_extent;
            this.btnExtent.Location = new System.Drawing.Point(231, 3);
            this.btnExtent.Name = "btnExtent";
            this.btnExtent.Size = new System.Drawing.Size(32, 28);
            this.btnExtent.TabIndex = 6;
            this.btnExtent.ToolTip = "当前视图作为绘图区域";
            this.btnExtent.Click += new System.EventHandler(this.btnExtent_Click);
            // 
            // btnClearClip
            // 
            this.btnClearClip.Image = global::Yutai.Plugins.Printing.Properties.Resources.icon_fence_basket;
            this.btnClearClip.Location = new System.Drawing.Point(3, 37);
            this.btnClearClip.Name = "btnClearClip";
            this.btnClearClip.Size = new System.Drawing.Size(32, 28);
            this.btnClearClip.TabIndex = 7;
            this.btnClearClip.ToolTip = "删除";
            this.btnClearClip.Click += new System.EventHandler(this.btnClearClip_Click);
            // 
            // grpKey
            // 
            this.grpKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpKey.Controls.Add(this.txtSearchKey);
            this.grpKey.Location = new System.Drawing.Point(7, 224);
            this.grpKey.Name = "grpKey";
            this.grpKey.Size = new System.Drawing.Size(289, 59);
            this.grpKey.TabIndex = 2;
            this.grpKey.Text = "关键字";
            // 
            // txtSearchKey
            // 
            this.txtSearchKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchKey.Location = new System.Drawing.Point(5, 27);
            this.txtSearchKey.Name = "txtSearchKey";
            this.txtSearchKey.Size = new System.Drawing.Size(278, 20);
            this.txtSearchKey.TabIndex = 0;
            // 
            // grpIndexMap
            // 
            this.grpIndexMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpIndexMap.Controls.Add(this.cmbIndexLayer);
            this.grpIndexMap.Location = new System.Drawing.Point(7, 165);
            this.grpIndexMap.Name = "grpIndexMap";
            this.grpIndexMap.Size = new System.Drawing.Size(288, 53);
            this.grpIndexMap.TabIndex = 1;
            this.grpIndexMap.Text = "分幅图";
            // 
            // cmbIndexLayer
            // 
            this.cmbIndexLayer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbIndexLayer.Location = new System.Drawing.Point(5, 24);
            this.cmbIndexLayer.Name = "cmbIndexLayer";
            this.cmbIndexLayer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbIndexLayer.Size = new System.Drawing.Size(272, 20);
            this.cmbIndexLayer.TabIndex = 0;
            // 
            // rdoSelectMode
            // 
            this.rdoSelectMode.Location = new System.Drawing.Point(6, 101);
            this.rdoSelectMode.Name = "rdoSelectMode";
            this.rdoSelectMode.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "分幅图+关键字+范围"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "任意范围")});
            this.rdoSelectMode.Size = new System.Drawing.Size(289, 60);
            this.rdoSelectMode.TabIndex = 0;
            this.rdoSelectMode.SelectedIndexChanged += new System.EventHandler(this.rdoSelectMode_SelectedIndexChanged);
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.splitContainer1);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(301, 620);
            this.xtraTabPage2.Text = "页面";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lstPages);
            this.splitContainer1.Panel1.Controls.Add(this.groupControl6);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.propertyPage);
            this.splitContainer1.Size = new System.Drawing.Size(301, 620);
            this.splitContainer1.SplitterDistance = 303;
            this.splitContainer1.TabIndex = 0;
            // 
            // lstPages
            // 
            this.lstPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstPages.FormattingEnabled = true;
            this.lstPages.Location = new System.Drawing.Point(0, 63);
            this.lstPages.Name = "lstPages";
            this.lstPages.Size = new System.Drawing.Size(301, 240);
            this.lstPages.TabIndex = 1;
            this.lstPages.SelectedIndexChanged += new System.EventHandler(this.lstPages_SelectedIndexChanged);
            // 
            // groupControl6
            // 
            this.groupControl6.Controls.Add(this.btnBatchPrint);
            this.groupControl6.Controls.Add(this.btnDeletePage);
            this.groupControl6.Controls.Add(this.btnPrint);
            this.groupControl6.Controls.Add(this.btnZoom);
            this.groupControl6.Controls.Add(this.btnLast);
            this.groupControl6.Controls.Add(this.btnNext);
            this.groupControl6.Controls.Add(this.btnPre);
            this.groupControl6.Controls.Add(this.btnFirst);
            this.groupControl6.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl6.Location = new System.Drawing.Point(0, 0);
            this.groupControl6.Name = "groupControl6";
            this.groupControl6.Size = new System.Drawing.Size(301, 63);
            this.groupControl6.TabIndex = 0;
            this.groupControl6.Text = "页面工具";
            // 
            // btnBatchPrint
            // 
            this.btnBatchPrint.Image = global::Yutai.Plugins.Printing.Properties.Resources.icon_page_print2;
            this.btnBatchPrint.Location = new System.Drawing.Point(232, 25);
            this.btnBatchPrint.Name = "btnBatchPrint";
            this.btnBatchPrint.Size = new System.Drawing.Size(32, 28);
            this.btnBatchPrint.TabIndex = 15;
            this.btnBatchPrint.Click += new System.EventHandler(this.btnBatchPrint_Click);
            // 
            // btnDeletePage
            // 
            this.btnDeletePage.Image = global::Yutai.Plugins.Printing.Properties.Resources.icon_page_delete;
            this.btnDeletePage.Location = new System.Drawing.Point(200, 25);
            this.btnDeletePage.Name = "btnDeletePage";
            this.btnDeletePage.Size = new System.Drawing.Size(32, 28);
            this.btnDeletePage.TabIndex = 14;
            this.btnDeletePage.Click += new System.EventHandler(this.btnDeletePage_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Image = global::Yutai.Plugins.Printing.Properties.Resources.icon_page_print;
            this.btnPrint.Location = new System.Drawing.Point(168, 25);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(32, 28);
            this.btnPrint.TabIndex = 13;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnZoom
            // 
            this.btnZoom.Image = global::Yutai.Plugins.Printing.Properties.Resources.icon_page_zoom;
            this.btnZoom.Location = new System.Drawing.Point(136, 25);
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new System.Drawing.Size(32, 28);
            this.btnZoom.TabIndex = 11;
            this.btnZoom.Click += new System.EventHandler(this.btnZoom_Click);
            // 
            // btnLast
            // 
            this.btnLast.Image = global::Yutai.Plugins.Printing.Properties.Resources.icon_page_last;
            this.btnLast.Location = new System.Drawing.Point(104, 25);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(32, 28);
            this.btnLast.TabIndex = 12;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.Image = global::Yutai.Plugins.Printing.Properties.Resources.icon_page_next;
            this.btnNext.Location = new System.Drawing.Point(72, 25);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(32, 28);
            this.btnNext.TabIndex = 10;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPre
            // 
            this.btnPre.Image = ((System.Drawing.Image)(resources.GetObject("btnPre.Image")));
            this.btnPre.Location = new System.Drawing.Point(40, 25);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(32, 28);
            this.btnPre.TabIndex = 9;
            this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Image = global::Yutai.Plugins.Printing.Properties.Resources.icon_page_first;
            this.btnFirst.Location = new System.Drawing.Point(8, 25);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(32, 28);
            this.btnFirst.TabIndex = 8;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // propertyPage
            // 
            this.propertyPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyPage.Location = new System.Drawing.Point(0, 0);
            this.propertyPage.Name = "propertyPage";
            this.propertyPage.Size = new System.Drawing.Size(301, 313);
            this.propertyPage.TabIndex = 0;
            // 
            // AutoLayoutView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "AutoLayoutView";
            this.Size = new System.Drawing.Size(307, 649);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).EndInit();
            this.groupControl5.ResumeLayout(false);
            this.groupControl5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbScale.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMapTemplate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpFence)).EndInit();
            this.grpFence.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpKey)).EndInit();
            this.grpKey.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpIndexMap)).EndInit();
            this.grpIndexMap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbIndexLayer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoSelectMode.Properties)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).EndInit();
            this.groupControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertyPage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraEditors.RadioGroup rdoSelectMode;
        private DevExpress.XtraEditors.GroupControl grpFence;
        private DevExpress.XtraEditors.SimpleButton btnClearClip;
        private DevExpress.XtraEditors.SimpleButton btnExtent;
        private DevExpress.XtraEditors.SimpleButton btnLineBuffer;
        private DevExpress.XtraEditors.SimpleButton btnPolyline;
        private DevExpress.XtraEditors.SimpleButton btnPolygon;
        private DevExpress.XtraEditors.SimpleButton btnRectangle;
        private DevExpress.XtraEditors.SimpleButton btnCircle;
        private DevExpress.XtraEditors.SimpleButton btnPoint;
        private DevExpress.XtraEditors.GroupControl grpKey;
        private DevExpress.XtraEditors.TextEdit txtSearchKey;
        private DevExpress.XtraEditors.GroupControl grpIndexMap;
        private DevExpress.XtraEditors.ComboBoxEdit cmbIndexLayer;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.SimpleButton btnClearPage;
        private DevExpress.XtraEditors.SimpleButton btnQueryPage;
        private DevExpress.XtraEditors.GroupControl groupControl5;
        private DevExpress.XtraEditors.ComboBoxEdit cmbScale;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit cmbMapTemplate;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.GroupControl groupControl6;
        private DevExpress.XtraEditors.SimpleButton btnBatchPrint;
        private DevExpress.XtraEditors.SimpleButton btnDeletePage;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnZoom;
        private DevExpress.XtraEditors.SimpleButton btnLast;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.XtraEditors.SimpleButton btnPre;
        private DevExpress.XtraEditors.SimpleButton btnFirst;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyPage;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckedListBox lstPages;
    }
}
