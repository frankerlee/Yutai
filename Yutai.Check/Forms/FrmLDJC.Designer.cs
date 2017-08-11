namespace Yutai.Check.Forms
{
    partial class FrmLDJC
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ucSelectFeatureClass1 = new Yutai.Pipeline.Editor.Controls.UcSelectFeatureClass();
            this.ucSelectFeatureClass2 = new Yutai.Pipeline.Editor.Controls.UcSelectFeatureClass();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(330, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "刷新图层";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "点图层：";
            // 
            // ucSelectFeatureClass1
            // 
            this.ucSelectFeatureClass1.Label = "";
            this.ucSelectFeatureClass1.LabelWidth = 25;
            this.ucSelectFeatureClass1.Location = new System.Drawing.Point(80, 13);
            this.ucSelectFeatureClass1.Margin = new System.Windows.Forms.Padding(5);
            this.ucSelectFeatureClass1.Name = "ucSelectFeatureClass1";
            this.ucSelectFeatureClass1.Size = new System.Drawing.Size(244, 20);
            this.ucSelectFeatureClass1.TabIndex = 4;
            this.ucSelectFeatureClass1.VisibleOpenButton = false;
            // 
            // ucSelectFeatureClass2
            // 
            this.ucSelectFeatureClass2.Label = "";
            this.ucSelectFeatureClass2.LabelWidth = 25;
            this.ucSelectFeatureClass2.Location = new System.Drawing.Point(80, 42);
            this.ucSelectFeatureClass2.Margin = new System.Windows.Forms.Padding(5);
            this.ucSelectFeatureClass2.Name = "ucSelectFeatureClass2";
            this.ucSelectFeatureClass2.Size = new System.Drawing.Size(244, 20);
            this.ucSelectFeatureClass2.TabIndex = 4;
            this.ucSelectFeatureClass2.VisibleOpenButton = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "线图层：";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(330, 87);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "关闭";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(249, 87);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "执行检查";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // FrmLDJC
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(417, 115);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.ucSelectFeatureClass2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucSelectFeatureClass1);
            this.Name = "FrmLDJC";
            this.Text = "漏点检查";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label1;
        private Pipeline.Editor.Controls.UcSelectFeatureClass ucSelectFeatureClass1;
        private Pipeline.Editor.Controls.UcSelectFeatureClass ucSelectFeatureClass2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}