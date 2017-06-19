namespace Yutai.Plugins.Editor.Controls
{
    partial class SnapConfigPage
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
            this.configPanelControl1 = new Yutai.UI.Controls.ConfigPanelControl();
            this.chkSnap = new System.Windows.Forms.CheckBox();
            this.chkSnapPoint = new System.Windows.Forms.CheckBox();
            this.chkSnapMidPoint = new System.Windows.Forms.CheckBox();
            this.chkSnapEndPoint = new System.Windows.Forms.CheckBox();
            this.chkSnapVertexPoint = new System.Windows.Forms.CheckBox();
            this.chkSnapSketch = new System.Windows.Forms.CheckBox();
            this.chkSnapBoundary = new System.Windows.Forms.CheckBox();
            this.chkSnapTargent = new System.Windows.Forms.CheckBox();
            this.chkSnapIntersectPoint = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // configPanelControl1
            // 
           
            this.configPanelControl1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.configPanelControl1.Controls.Add(this.chkSnapSketch);
            this.configPanelControl1.Controls.Add(this.chkSnapBoundary);
            this.configPanelControl1.Controls.Add(this.chkSnapTargent);
            this.configPanelControl1.Controls.Add(this.chkSnapIntersectPoint);
            this.configPanelControl1.Controls.Add(this.chkSnapVertexPoint);
            this.configPanelControl1.Controls.Add(this.chkSnapEndPoint);
            this.configPanelControl1.Controls.Add(this.chkSnapMidPoint);
            this.configPanelControl1.Controls.Add(this.chkSnapPoint);
            this.configPanelControl1.Controls.Add(this.chkSnap);
            this.configPanelControl1.HeaderText = "捕捉设置";
            this.configPanelControl1.Location = new System.Drawing.Point(2, 4);
            this.configPanelControl1.Name = "configPanelControl1";
            this.configPanelControl1.Size = new System.Drawing.Size(373, 213);
            this.configPanelControl1.TabIndex = 0;
            // 
            // chkSnap
            // 
            this.chkSnap.Location = new System.Drawing.Point(16, 38);
            this.chkSnap.Name = "chkSnap";
            this.chkSnap.Size = new System.Drawing.Size(188, 19);
            this.chkSnap.TabIndex = 5;
            this.chkSnap.Text = "启动捕捉";
            // 
            // chkSnapPoint
            // 
            this.chkSnapPoint.Location = new System.Drawing.Point(16, 79);
            this.chkSnapPoint.Name = "chkSnapPoint";
            this.chkSnapPoint.Size = new System.Drawing.Size(94, 19);
            this.chkSnapPoint.TabIndex = 6;
            this.chkSnapPoint.Text = "点捕捉";
            // 
            // chkSnapMidPoint
            // 
            this.chkSnapMidPoint.Location = new System.Drawing.Point(110, 79);
            this.chkSnapMidPoint.Name = "chkSnapMidPoint";
            this.chkSnapMidPoint.Size = new System.Drawing.Size(94, 19);
            this.chkSnapMidPoint.TabIndex = 7;
            this.chkSnapMidPoint.Text = "中点捕捉";
            // 
            // chkSnapEndPoint
            // 
            this.chkSnapEndPoint.Location = new System.Drawing.Point(204, 79);
            this.chkSnapEndPoint.Name = "chkSnapEndPoint";
            this.chkSnapEndPoint.Size = new System.Drawing.Size(94, 19);
            this.chkSnapEndPoint.TabIndex = 8;
            this.chkSnapEndPoint.Text = "端点捕捉";
            // 
            // chkSnapVertexPoint
            // 
            this.chkSnapVertexPoint.Location = new System.Drawing.Point(298, 79);
            this.chkSnapVertexPoint.Name = "chkSnapVertexPoint";
            this.chkSnapVertexPoint.Size = new System.Drawing.Size(94, 19);
            this.chkSnapVertexPoint.TabIndex = 9;
            this.chkSnapVertexPoint.Text = "节点捕捉";
            // 
            // chkSnapSketch
            // 
            this.chkSnapSketch.Location = new System.Drawing.Point(298, 114);
            this.chkSnapSketch.Name = "chkSnapSketch";
            this.chkSnapSketch.Size = new System.Drawing.Size(94, 19);
            this.chkSnapSketch.TabIndex = 13;
            this.chkSnapSketch.Text = "草图捕捉";
            // 
            // chkSnapBoundary
            // 
            this.chkSnapBoundary.Location = new System.Drawing.Point(204, 114);
            this.chkSnapBoundary.Name = "chkSnapBoundary";
            this.chkSnapBoundary.Size = new System.Drawing.Size(94, 19);
            this.chkSnapBoundary.TabIndex = 12;
            this.chkSnapBoundary.Text = "边界捕捉";
            // 
            // chkSnapTargent
            // 
            this.chkSnapTargent.Location = new System.Drawing.Point(110, 114);
            this.chkSnapTargent.Name = "chkSnapTargent";
            this.chkSnapTargent.Size = new System.Drawing.Size(94, 19);
            this.chkSnapTargent.TabIndex = 11;
            this.chkSnapTargent.Text = "切线捕捉";
            // 
            // chkSnapIntersectPoint
            // 
            this.chkSnapIntersectPoint.Location = new System.Drawing.Point(16, 114);
            this.chkSnapIntersectPoint.Name = "chkSnapIntersectPoint";
            this.chkSnapIntersectPoint.Size = new System.Drawing.Size(94, 19);
            this.chkSnapIntersectPoint.TabIndex = 10;
            this.chkSnapIntersectPoint.Text = "交点捕捉";
            // 
            // SnapConfigPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.configPanelControl1);
            this.Name = "SnapConfigPage";
            this.Size = new System.Drawing.Size(385, 253);
            this.ResumeLayout(false);

        }

        #endregion

        private UI.Controls.ConfigPanelControl configPanelControl1;
        private System.Windows.Forms.CheckBox chkSnapSketch;
        private System.Windows.Forms.CheckBox chkSnapBoundary;
        private System.Windows.Forms.CheckBox chkSnapTargent;
        private System.Windows.Forms.CheckBox chkSnapIntersectPoint;
        private System.Windows.Forms.CheckBox chkSnapVertexPoint;
        private System.Windows.Forms.CheckBox chkSnapEndPoint;
        private System.Windows.Forms.CheckBox chkSnapMidPoint;
        private System.Windows.Forms.CheckBox chkSnapPoint;
        private System.Windows.Forms.CheckBox chkSnap;
    }
}
