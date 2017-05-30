namespace Yutai.Catalog.UI
{
    partial class CatalogComboBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CatalogComboBox));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.cmbCatalog = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "computer_16.gif");
            this.imageList1.Images.SetKeyName(1, "folder-closed_16.gif");
            this.imageList1.Images.SetKeyName(2, "folder-open_16.gif");
            this.imageList1.Images.SetKeyName(3, "documents_16.gif");
            this.imageList1.Images.SetKeyName(4, "disc.png");
            this.imageList1.Images.SetKeyName(5, "mappeddrive.png");
            this.imageList1.Images.SetKeyName(6, "harddisc.png");
            this.imageList1.Images.SetKeyName(7, "floppy.png");
            // 
            // cmbCatalog
            // 
            this.cmbCatalog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCatalog.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCatalog.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCatalog.FormattingEnabled = true;
            this.cmbCatalog.ItemHeight = 16;
            this.cmbCatalog.Location = new System.Drawing.Point(0, 0);
            this.cmbCatalog.Name = "cmbCatalog";
            this.cmbCatalog.Size = new System.Drawing.Size(276, 22);
            this.cmbCatalog.TabIndex = 1;
            // 
            // CatalogComboBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbCatalog);
            this.Name = "CatalogComboBox";
            this.Size = new System.Drawing.Size(276, 23);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ComboBox cmbCatalog;
    }
}
