namespace Yutai.Plugins.Locator.Views
{
    partial class LocatorDockPanel
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBoxAdv1 = new Syncfusion.Windows.Forms.Tools.ComboBoxAdv();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripEx1 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.toolSearchKey = new System.Windows.Forms.ToolStripTextBox();
            this.toolSearch = new System.Windows.Forms.ToolStripButton();
            this.toolZoomToShape = new System.Windows.Forms.ToolStripButton();
            this.toolClear = new System.Windows.Forms.ToolStripButton();
            this.grdResult = new Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxAdv1)).BeginInit();
            this.toolStripEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdResult)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBoxAdv1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(313, 28);
            this.panel1.TabIndex = 0;
            // 
            // comboBoxAdv1
            // 
            this.comboBoxAdv1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxAdv1.BeforeTouchSize = new System.Drawing.Size(229, 20);
            this.comboBoxAdv1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAdv1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxAdv1.Location = new System.Drawing.Point(74, 5);
            this.comboBoxAdv1.Name = "comboBoxAdv1";
            this.comboBoxAdv1.Size = new System.Drawing.Size(229, 20);
            this.comboBoxAdv1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "定位器：";
            // 
            // toolStripEx1
            // 
            this.toolStripEx1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripEx1.Image = null;
            this.toolStripEx1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSearchKey,
            this.toolSearch,
            this.toolZoomToShape,
            this.toolClear});
            this.toolStripEx1.Location = new System.Drawing.Point(0, 28);
            this.toolStripEx1.Name = "toolStripEx1";
            this.toolStripEx1.ShowCaption = false;
            this.toolStripEx1.Size = new System.Drawing.Size(313, 25);
            this.toolStripEx1.Stretch = true;
            this.toolStripEx1.TabIndex = 2;
            // 
            // toolSearchKey
            // 
            this.toolSearchKey.Name = "toolSearchKey";
            this.toolSearchKey.Size = new System.Drawing.Size(150, 25);
            // 
            // toolSearch
            // 
            this.toolSearch.Image = global::Yutai.Plugins.Locator.Properties.Resources.YTLocator;
            this.toolSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSearch.Name = "toolSearch";
            this.toolSearch.Size = new System.Drawing.Size(52, 22);
            this.toolSearch.Text = "查找";
            // 
            // toolZoomToShape
            // 
            this.toolZoomToShape.Checked = true;
            this.toolZoomToShape.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolZoomToShape.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolZoomToShape.Image = global::Yutai.Plugins.Locator.Properties.Resources.icon_zoom_to_layer;
            this.toolZoomToShape.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolZoomToShape.Name = "toolZoomToShape";
            this.toolZoomToShape.Size = new System.Drawing.Size(23, 22);
            this.toolZoomToShape.Text = "放大到位置";
            // 
            // toolClear
            // 
            this.toolClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolClear.Image = global::Yutai.Plugins.Locator.Properties.Resources.img_clear24;
            this.toolClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolClear.Name = "toolClear";
            this.toolClear.Size = new System.Drawing.Size(23, 22);
            this.toolClear.Text = "清除";
            // 
            // grdResult
            // 
            this.grdResult.BackColor = System.Drawing.SystemColors.Window;
            this.grdResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdResult.FreezeCaption = false;
            this.grdResult.GridOfficeScrollBars = Syncfusion.Windows.Forms.OfficeScrollBars.Metro;
            this.grdResult.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Metro;
            this.grdResult.Location = new System.Drawing.Point(0, 53);
            this.grdResult.Name = "grdResult";
            this.grdResult.Size = new System.Drawing.Size(313, 412);
            this.grdResult.TabIndex = 3;
            this.grdResult.TableDescriptor.AllowNew = false;
            this.grdResult.TableDescriptor.TableOptions.CaptionRowHeight = 29;
            this.grdResult.TableDescriptor.TableOptions.ColumnHeaderRowHeight = 25;
            this.grdResult.TableDescriptor.TableOptions.RecordRowHeight = 25;
            this.grdResult.Text = "gridGroupingControl1";
            this.grdResult.TopLevelGroupOptions.ShowColumnHeaders = false;
            this.grdResult.VersionInfo = "14.1450.0.41";
            // 
            // LocatorDockPanel
            // 
            this.Controls.Add(this.grdResult);
            this.Controls.Add(this.toolStripEx1);
            this.Controls.Add(this.panel1);
            this.Name = "LocatorDockPanel";
            this.Size = new System.Drawing.Size(313, 465);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxAdv1)).EndInit();
            this.toolStripEx1.ResumeLayout(false);
            this.toolStripEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Syncfusion.Windows.Forms.Tools.ComboBoxAdv comboBoxAdv1;
        private System.Windows.Forms.Label label1;
        private Syncfusion.Windows.Forms.Tools.ToolStripEx toolStripEx1;
        private System.Windows.Forms.ToolStripButton toolZoomToShape;
        private System.Windows.Forms.ToolStripButton toolClear;
        private Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl grdResult;
        private System.Windows.Forms.ToolStripTextBox toolSearchKey;
        private System.Windows.Forms.ToolStripButton toolSearch;
    }
}
