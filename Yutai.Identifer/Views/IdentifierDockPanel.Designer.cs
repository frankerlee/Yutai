using Syncfusion.Windows.Forms.Tools;


namespace Yutai.Plugins.Identifer.Views
{
    partial class IdentifierDockPanel
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
            this._cboIdentifierMode = new Syncfusion.Windows.Forms.Tools.ComboBoxAdv();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            this.toolStripEx1 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.toolZoomToShape = new System.Windows.Forms.ToolStripButton();
            this.toolClear = new System.Windows.Forms.ToolStripButton();
            this.splitContainerAdv1 = new Syncfusion.Windows.Forms.Tools.SplitContainerAdv();
            this.trvFeatures = new System.Windows.Forms.TreeView();
            this.panCoords = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            this.txtCoords = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.lstAttribute = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this._cboIdentifierMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.toolStripEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerAdv1)).BeginInit();
            this.splitContainerAdv1.Panel1.SuspendLayout();
            this.splitContainerAdv1.Panel2.SuspendLayout();
            this.splitContainerAdv1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panCoords)).BeginInit();
            this.panCoords.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCoords)).BeginInit();
            this.SuspendLayout();
            // 
            // _cboIdentifierMode
            // 
            this._cboIdentifierMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._cboIdentifierMode.BeforeTouchSize = new System.Drawing.Size(213, 21);
            this._cboIdentifierMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cboIdentifierMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._cboIdentifierMode.Location = new System.Drawing.Point(45, 6);
            this._cboIdentifierMode.Name = "_cboIdentifierMode";
            this._cboIdentifierMode.Size = new System.Drawing.Size(213, 21);
            this._cboIdentifierMode.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "图层";
            // 
            // panel1
            // 
            this.panel1.BorderColor = System.Drawing.Color.Gray;
            this.panel1.BorderSides = System.Windows.Forms.Border3DSide.Top;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this._cboIdentifierMode);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(268, 32);
            this.panel1.TabIndex = 5;
            // 
            // toolStripEx1
            // 
            this.toolStripEx1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripEx1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx1.Image = null;
            this.toolStripEx1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolZoomToShape,
            this.toolClear});
            this.toolStripEx1.Location = new System.Drawing.Point(0, 0);
            this.toolStripEx1.Name = "toolStripEx1";
            this.toolStripEx1.ShowCaption = false;
            this.toolStripEx1.ShowItemToolTips = true;
            this.toolStripEx1.Size = new System.Drawing.Size(268, 35);
            this.toolStripEx1.TabIndex = 45;
            this.toolStripEx1.Text = "toolStripEx1";
            // 
            // toolZoomToShape
            // 
            this.toolZoomToShape.Checked = true;
            this.toolZoomToShape.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolZoomToShape.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolZoomToShape.Image = global::Yutai.Plugins.Identifer.Properties.Resources.icon_zoom_to_layer;
            this.toolZoomToShape.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolZoomToShape.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolZoomToShape.Name = "toolZoomToShape";
            this.toolZoomToShape.Padding = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.toolZoomToShape.Size = new System.Drawing.Size(38, 32);
            this.toolZoomToShape.Text = "放大到要素";
            // 
            // toolClear
            // 
            this.toolClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolClear.Image = global::Yutai.Plugins.Identifer.Properties.Resources.img_clear24;
            this.toolClear.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolClear.Name = "toolClear";
            this.toolClear.Padding = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.toolClear.Size = new System.Drawing.Size(38, 32);
            this.toolClear.Text = "清除";
            // 
            // splitContainerAdv1
            // 
            this.splitContainerAdv1.BeforeTouchSize = 7;
            this.splitContainerAdv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerAdv1.Location = new System.Drawing.Point(0, 67);
            this.splitContainerAdv1.Name = "splitContainerAdv1";
            this.splitContainerAdv1.Orientation = System.Windows.Forms.Orientation.Vertical;
            // 
            // splitContainerAdv1.Panel1
            // 
            this.splitContainerAdv1.Panel1.Controls.Add(this.trvFeatures);
            this.splitContainerAdv1.Panel1.Controls.Add(this.panCoords);
            // 
            // splitContainerAdv1.Panel2
            // 
            this.splitContainerAdv1.Panel2.Controls.Add(this.lstAttribute);
            this.splitContainerAdv1.Size = new System.Drawing.Size(268, 326);
            this.splitContainerAdv1.SplitterDistance = 294;
            this.splitContainerAdv1.TabIndex = 46;
            this.splitContainerAdv1.Text = "splitContainerAdv1";
            // 
            // trvFeatures
            // 
            this.trvFeatures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvFeatures.Location = new System.Drawing.Point(0, 0);
            this.trvFeatures.Name = "trvFeatures";
            this.trvFeatures.Size = new System.Drawing.Size(268, 273);
            this.trvFeatures.TabIndex = 1;
            this.trvFeatures.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvFeatures_NodeMouseClick);
            // 
            // panCoords
            // 
            this.panCoords.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.panCoords.Controls.Add(this.txtCoords);
            this.panCoords.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panCoords.Location = new System.Drawing.Point(0, 273);
            this.panCoords.Name = "panCoords";
            this.panCoords.Size = new System.Drawing.Size(268, 21);
            this.panCoords.TabIndex = 0;
            // 
            // txtCoords
            // 
            this.txtCoords.BeforeTouchSize = new System.Drawing.Size(268, 21);
            this.txtCoords.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCoords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCoords.Enabled = false;
            this.txtCoords.Location = new System.Drawing.Point(0, 0);
            this.txtCoords.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.txtCoords.Name = "txtCoords";
            this.txtCoords.Size = new System.Drawing.Size(268, 21);
            this.txtCoords.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Default;
            this.txtCoords.TabIndex = 6;
            // 
            // lstAttribute
            // 
            this.lstAttribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstAttribute.Location = new System.Drawing.Point(0, 0);
            this.lstAttribute.Name = "lstAttribute";
            this.lstAttribute.Size = new System.Drawing.Size(268, 25);
            this.lstAttribute.TabIndex = 0;
            this.lstAttribute.UseCompatibleStateImageBehavior = false;
            this.lstAttribute.View = System.Windows.Forms.View.Details;
            this.lstAttribute.Resize += new System.EventHandler(this.lstAttribute_Resize);
            // 
            // IdentifierDockPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerAdv1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStripEx1);
            this.Name = "IdentifierDockPanel";
            this.Size = new System.Drawing.Size(268, 393);
            ((System.ComponentModel.ISupportInitialize)(this._cboIdentifierMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStripEx1.ResumeLayout(false);
            this.toolStripEx1.PerformLayout();
            this.splitContainerAdv1.Panel1.ResumeLayout(false);
            this.splitContainerAdv1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerAdv1)).EndInit();
            this.splitContainerAdv1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panCoords)).EndInit();
            this.panCoords.ResumeLayout(false);
            this.panCoords.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCoords)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.ComboBoxAdv _cboIdentifierMode;
        private System.Windows.Forms.Label label1;
        private GradientPanel panel1;
        private Syncfusion.Windows.Forms.Tools.ToolStripEx toolStripEx1;
        private System.Windows.Forms.ToolStripButton toolClear;
        private System.Windows.Forms.ToolStripButton toolZoomToShape;
        private SplitContainerAdv splitContainerAdv1;
        private GradientPanel panCoords;
        private TextBoxExt txtCoords;
        private System.Windows.Forms.TreeView trvFeatures;
        private System.Windows.Forms.ListView lstAttribute;
    }
}
