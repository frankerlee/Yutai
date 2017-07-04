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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this._cboIdentifierMode = new System.Windows.Forms.ComboBox();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.trvFeatures = new System.Windows.Forms.TreeView();
            this.txtCoords = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.lstAttribute = new System.Windows.Forms.ListView();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnZoom = new DevExpress.XtraEditors.CheckButton();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCoords)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Controls.Add(this.labelControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._cboIdentifierMode, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnClear, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnZoom, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(268, 36);
            this.tableLayoutPanel1.TabIndex = 47;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 8);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(8, 8, 3, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "图层：";
            // 
            // _cboIdentifierMode
            // 
            this._cboIdentifierMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cboIdentifierMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cboIdentifierMode.FormattingEnabled = true;
            this._cboIdentifierMode.Location = new System.Drawing.Point(53, 8);
            this._cboIdentifierMode.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this._cboIdentifierMode.Name = "_cboIdentifierMode";
            this._cboIdentifierMode.Size = new System.Drawing.Size(132, 20);
            this._cboIdentifierMode.TabIndex = 1;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 36);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.trvFeatures);
            this.splitContainerControl1.Panel1.Controls.Add(this.txtCoords);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.lstAttribute);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(268, 357);
            this.splitContainerControl1.TabIndex = 48;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // trvFeatures
            // 
            this.trvFeatures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvFeatures.Location = new System.Drawing.Point(0, 0);
            this.trvFeatures.Name = "trvFeatures";
            this.trvFeatures.Size = new System.Drawing.Size(268, 78);
            this.trvFeatures.TabIndex = 1;
            this.trvFeatures.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvFeatures_NodeMouseClick);
            // 
            // txtCoords
            // 
            this.txtCoords.BeforeTouchSize = new System.Drawing.Size(268, 22);
            this.txtCoords.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCoords.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtCoords.Location = new System.Drawing.Point(0, 78);
            this.txtCoords.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.txtCoords.Name = "txtCoords";
            this.txtCoords.ReadOnly = true;
            this.txtCoords.Size = new System.Drawing.Size(268, 22);
            this.txtCoords.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Default;
            this.txtCoords.TabIndex = 0;
            // 
            // lstAttribute
            // 
            this.lstAttribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstAttribute.Location = new System.Drawing.Point(0, 0);
            this.lstAttribute.Name = "lstAttribute";
            this.lstAttribute.Size = new System.Drawing.Size(268, 252);
            this.lstAttribute.TabIndex = 0;
            this.lstAttribute.UseCompatibleStateImageBehavior = false;
            this.lstAttribute.View = System.Windows.Forms.View.Details;
            // 
            // btnClear
            // 
            this.btnClear.Image = global::Yutai.Plugins.Identifer.Properties.Resources.img_clear24;
            this.btnClear.Location = new System.Drawing.Point(231, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(34, 30);
            this.btnClear.TabIndex = 3;
            // 
            // btnZoom
            // 
            this.btnZoom.Image = global::Yutai.Plugins.Identifer.Properties.Resources.ZoomSelection;
            this.btnZoom.Location = new System.Drawing.Point(191, 3);
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new System.Drawing.Size(34, 30);
            this.btnZoom.TabIndex = 4;
            // 
            // IdentifierDockPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "IdentifierDockPanel";
            this.Size = new System.Drawing.Size(268, 393);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCoords)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.ComboBox _cboIdentifierMode;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private TextBoxExt txtCoords;
        private System.Windows.Forms.TreeView trvFeatures;
        private System.Windows.Forms.ListView lstAttribute;
        private DevExpress.XtraEditors.CheckButton btnZoom;
    }
}
