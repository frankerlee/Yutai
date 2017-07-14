namespace Yutai.Pipeline.Editor.Controls
{
    partial class UcSpatialReference
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
            this.label1 = new System.Windows.Forms.Label();
            this.rgType = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.rgType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "使用与一下选项相同的坐标系";
            // 
            // rgType
            // 
            this.rgType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rgType.EditValue = 0;
            this.rgType.Location = new System.Drawing.Point(0, 12);
            this.rgType.Name = "rgType";
            this.rgType.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.rgType.Properties.Appearance.Options.UseBackColor = true;
            this.rgType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rgType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "此图层的源数据"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "数据框"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "将数据导出到的要素数据集(仅当导出到地理数据库中的要素数据集时适用)")});
            this.rgType.Size = new System.Drawing.Size(504, 70);
            this.rgType.TabIndex = 1;
            // 
            // UcSpatialReference
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rgType);
            this.Controls.Add(this.label1);
            this.Name = "UcSpatialReference";
            this.Size = new System.Drawing.Size(504, 82);
            ((System.ComponentModel.ISupportInitialize)(this.rgType.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.RadioGroup rgType;
    }
}
