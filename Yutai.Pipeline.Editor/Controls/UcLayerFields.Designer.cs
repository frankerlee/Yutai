namespace Yutai.Pipeline.Editor.Controls
{
    partial class UcLayerFields
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
            this.btnSelectFields = new System.Windows.Forms.Button();
            this.chkLayer = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnSelectFields
            // 
            this.btnSelectFields.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelectFields.Location = new System.Drawing.Point(654, 0);
            this.btnSelectFields.Name = "btnSelectFields";
            this.btnSelectFields.Size = new System.Drawing.Size(75, 22);
            this.btnSelectFields.TabIndex = 2;
            this.btnSelectFields.Text = "选择字段";
            this.btnSelectFields.UseVisualStyleBackColor = true;
            this.btnSelectFields.Click += new System.EventHandler(this.btnSelectFields_Click);
            this.btnSelectFields.MouseLeave += new System.EventHandler(this.btnSelectFields_MouseLeave);
            this.btnSelectFields.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnSelectFields_MouseMove);
            // 
            // chkLayer
            // 
            this.chkLayer.AutoSize = true;
            this.chkLayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkLayer.Location = new System.Drawing.Point(0, 0);
            this.chkLayer.Name = "chkLayer";
            this.chkLayer.Size = new System.Drawing.Size(654, 22);
            this.chkLayer.TabIndex = 4;
            this.chkLayer.UseVisualStyleBackColor = true;
            this.chkLayer.MouseLeave += new System.EventHandler(this.chkLayer_MouseLeave);
            this.chkLayer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chkLayer_MouseMove);
            // 
            // UcLayerFields
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.chkLayer);
            this.Controls.Add(this.btnSelectFields);
            this.Name = "UcLayerFields";
            this.Size = new System.Drawing.Size(729, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSelectFields;
        private System.Windows.Forms.CheckBox chkLayer;
    }
}
