namespace Yutai.Pipeline.Editor.Views
{
    partial class IdentifyRoadNameDockPanel
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
            this.panel12 = new System.Windows.Forms.Panel();
            this.btnExecute = new System.Windows.Forms.Button();
            this.panel10 = new System.Windows.Forms.Panel();
            this.numSearchRadius = new System.Windows.Forms.NumericUpDown();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.ucField_PipeRoadName = new Yutai.Pipeline.Editor.Controls.UcSelectField();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.ucFeatureClass_PipeLayer = new Yutai.Pipeline.Editor.Controls.UcSelectFeatureClass();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ucField_PolyRoadName = new Yutai.Pipeline.Editor.Controls.UcSelectField();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ucFeatureClass_RoadLayer = new Yutai.Pipeline.Editor.Controls.UcSelectFeatureClass();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucExtentSetting = new Yutai.Pipeline.Editor.Controls.UcExtentSetting();
            this.panel12.SuspendLayout();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSearchRadius)).BeginInit();
            this.panel11.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.btnExecute);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel12.Location = new System.Drawing.Point(252, 130);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(551, 22);
            this.panel12.TabIndex = 16;
            // 
            // btnExecute
            // 
            this.btnExecute.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExecute.Location = new System.Drawing.Point(476, 0);
            this.btnExecute.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 22);
            this.btnExecute.TabIndex = 7;
            this.btnExecute.Text = "执行";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.numSearchRadius);
            this.panel10.Controls.Add(this.panel11);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(252, 105);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(551, 25);
            this.panel10.TabIndex = 15;
            // 
            // numSearchRadius
            // 
            this.numSearchRadius.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numSearchRadius.Location = new System.Drawing.Point(99, 0);
            this.numSearchRadius.Name = "numSearchRadius";
            this.numSearchRadius.Size = new System.Drawing.Size(452, 21);
            this.numSearchRadius.TabIndex = 1;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.label5);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(99, 25);
            this.panel11.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "搜索半径";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.ucField_PipeRoadName);
            this.panel8.Controls.Add(this.panel9);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(252, 80);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(551, 25);
            this.panel8.TabIndex = 14;
            // 
            // ucField_PipeRoadName
            // 
            this.ucField_PipeRoadName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucField_PipeRoadName.Label = "";
            this.ucField_PipeRoadName.LabelWidth = 25;
            this.ucField_PipeRoadName.Location = new System.Drawing.Point(99, 0);
            this.ucField_PipeRoadName.Name = "ucField_PipeRoadName";
            this.ucField_PipeRoadName.Size = new System.Drawing.Size(452, 25);
            this.ucField_PipeRoadName.TabIndex = 1;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.label4);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(99, 25);
            this.panel9.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "道路名称字段";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.ucFeatureClass_PipeLayer);
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(252, 55);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(551, 25);
            this.panel6.TabIndex = 13;
            // 
            // ucFeatureClass_PipeLayer
            // 
            this.ucFeatureClass_PipeLayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucFeatureClass_PipeLayer.Label = "";
            this.ucFeatureClass_PipeLayer.LabelWidth = 25;
            this.ucFeatureClass_PipeLayer.Location = new System.Drawing.Point(99, 0);
            this.ucFeatureClass_PipeLayer.Margin = new System.Windows.Forms.Padding(5);
            this.ucFeatureClass_PipeLayer.Name = "ucFeatureClass_PipeLayer";
            this.ucFeatureClass_PipeLayer.Size = new System.Drawing.Size(452, 25);
            this.ucFeatureClass_PipeLayer.TabIndex = 1;
            this.ucFeatureClass_PipeLayer.VisibleOpenButton = false;
            this.ucFeatureClass_PipeLayer.SelectComplateEvent += new Yutai.Pipeline.Editor.Controls.SelectComplateHandler(this.ucFeatureClass_PipeLayer_SelectComplateEvent);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.label3);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(99, 25);
            this.panel7.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "管线图层";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ucField_PolyRoadName);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(252, 30);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(551, 25);
            this.panel2.TabIndex = 12;
            // 
            // ucField_PolyRoadName
            // 
            this.ucField_PolyRoadName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucField_PolyRoadName.Label = "";
            this.ucField_PolyRoadName.LabelWidth = 25;
            this.ucField_PolyRoadName.Location = new System.Drawing.Point(99, 0);
            this.ucField_PolyRoadName.Name = "ucField_PolyRoadName";
            this.ucField_PolyRoadName.Size = new System.Drawing.Size(452, 25);
            this.ucField_PolyRoadName.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(99, 25);
            this.panel5.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "道路名称字段";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ucFeatureClass_RoadLayer);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(252, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(551, 25);
            this.panel3.TabIndex = 11;
            // 
            // ucFeatureClass_RoadLayer
            // 
            this.ucFeatureClass_RoadLayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucFeatureClass_RoadLayer.Label = "";
            this.ucFeatureClass_RoadLayer.LabelWidth = 25;
            this.ucFeatureClass_RoadLayer.Location = new System.Drawing.Point(99, 0);
            this.ucFeatureClass_RoadLayer.Margin = new System.Windows.Forms.Padding(5);
            this.ucFeatureClass_RoadLayer.Name = "ucFeatureClass_RoadLayer";
            this.ucFeatureClass_RoadLayer.Size = new System.Drawing.Size(452, 25);
            this.ucFeatureClass_RoadLayer.TabIndex = 1;
            this.ucFeatureClass_RoadLayer.VisibleOpenButton = false;
            this.ucFeatureClass_RoadLayer.SelectComplateEvent += new Yutai.Pipeline.Editor.Controls.SelectComplateHandler(this.ucFeatureClass_RoadLayer_SelectComplateEvent);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(99, 25);
            this.panel4.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "道路面";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(252, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(551, 5);
            this.panel1.TabIndex = 10;
            // 
            // ucExtentSetting
            // 
            this.ucExtentSetting.Dock = System.Windows.Forms.DockStyle.Left;
            this.ucExtentSetting.Enabled = false;
            this.ucExtentSetting.Location = new System.Drawing.Point(0, 0);
            this.ucExtentSetting.Name = "ucExtentSetting";
            this.ucExtentSetting.Size = new System.Drawing.Size(252, 284);
            this.ucExtentSetting.TabIndex = 9;
            // 
            // IdentifyRoadNameDockPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel12);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ucExtentSetting);
            this.Name = "IdentifyRoadNameDockPanel";
            this.Size = new System.Drawing.Size(803, 284);
            this.panel12.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numSearchRadius)).EndInit();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.NumericUpDown numSearchRadius;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel8;
        private Controls.UcSelectField ucField_PipeRoadName;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel6;
        private Controls.UcSelectFeatureClass ucFeatureClass_PipeLayer;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private Controls.UcSelectField ucField_PolyRoadName;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private Controls.UcSelectFeatureClass ucFeatureClass_RoadLayer;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private Controls.UcExtentSetting ucExtentSetting;
    }
}
