namespace Yutai.Pipeline.Editor.Views
{
    partial class AnnotationSortingDockPanel
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDirection = new System.Windows.Forms.ComboBox();
            this.btnSelectFeature = new System.Windows.Forms.Button();
            this.btnSelectDatumPoint = new System.Windows.Forms.Button();
            this.listBoxAnnotation = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.cmbDirection);
            this.splitContainer1.Panel1.Controls.Add(this.btnSelectFeature);
            this.splitContainer1.Panel1.Controls.Add(this.btnSelectDatumPoint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listBoxAnnotation);
            this.splitContainer1.Size = new System.Drawing.Size(192, 407);
            this.splitContainer1.SplitterDistance = 58;
            this.splitContainer1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "注记方向：";
            // 
            // cmbDirection
            // 
            this.cmbDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDirection.FormattingEnabled = true;
            this.cmbDirection.Items.AddRange(new object[] {
            "左上",
            "右上",
            "右下",
            "左下"});
            this.cmbDirection.Location = new System.Drawing.Point(74, 3);
            this.cmbDirection.Name = "cmbDirection";
            this.cmbDirection.Size = new System.Drawing.Size(110, 20);
            this.cmbDirection.TabIndex = 5;
            // 
            // btnSelectFeature
            // 
            this.btnSelectFeature.Location = new System.Drawing.Point(5, 29);
            this.btnSelectFeature.Name = "btnSelectFeature";
            this.btnSelectFeature.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFeature.TabIndex = 4;
            this.btnSelectFeature.Text = "加载点注记";
            this.btnSelectFeature.UseVisualStyleBackColor = true;
            this.btnSelectFeature.Click += new System.EventHandler(this.btnSelectFeature_Click);
            // 
            // btnSelectDatumPoint
            // 
            this.btnSelectDatumPoint.Location = new System.Drawing.Point(109, 29);
            this.btnSelectDatumPoint.Name = "btnSelectDatumPoint";
            this.btnSelectDatumPoint.Size = new System.Drawing.Size(75, 23);
            this.btnSelectDatumPoint.TabIndex = 3;
            this.btnSelectDatumPoint.Text = "选择基准点";
            this.btnSelectDatumPoint.UseVisualStyleBackColor = true;
            this.btnSelectDatumPoint.Click += new System.EventHandler(this.btnSelectDatumPoint_Click);
            // 
            // listBoxAnnotation
            // 
            this.listBoxAnnotation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxAnnotation.FormattingEnabled = true;
            this.listBoxAnnotation.ItemHeight = 12;
            this.listBoxAnnotation.Location = new System.Drawing.Point(0, 0);
            this.listBoxAnnotation.Name = "listBoxAnnotation";
            this.listBoxAnnotation.Size = new System.Drawing.Size(192, 345);
            this.listBoxAnnotation.TabIndex = 0;
            // 
            // AnnotationSortingDockPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "AnnotationSortingDockPanel";
            this.Size = new System.Drawing.Size(192, 407);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDirection;
        private System.Windows.Forms.Button btnSelectFeature;
        private System.Windows.Forms.Button btnSelectDatumPoint;
        private System.Windows.Forms.ListBox listBoxAnnotation;
    }
}
