namespace Yutai.UI.Controls
{
    partial class GISDataComboBox
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
            this.components = new System.ComponentModel.Container();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.cmbCatalog = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // cmbCatalog
            // 
            this.cmbCatalog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCatalog.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCatalog.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCatalog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCatalog.FormattingEnabled = true;
            this.cmbCatalog.Location = new System.Drawing.Point(0, 0);
            this.cmbCatalog.Name = "cmbCatalog";
            this.cmbCatalog.Size = new System.Drawing.Size(262, 22);
            this.cmbCatalog.TabIndex = 0;
            this.cmbCatalog.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbCatalog_DrawItem);
            this.cmbCatalog.SelectedIndexChanged += new System.EventHandler(this.cmbCatalog_SelectedIndexChanged);
            // 
            // GISDataComboBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbCatalog);
            this.Name = "GISDataComboBox";
            this.Size = new System.Drawing.Size(262, 25);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ComboBox cmbCatalog;
    }
}
