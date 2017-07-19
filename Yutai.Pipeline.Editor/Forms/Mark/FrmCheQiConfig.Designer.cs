namespace Yutai.Pipeline.Editor.Forms.Mark
{
    partial class FrmCheQiConfig
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TxtExpression = new System.Windows.Forms.TextBox();
            this.btnExpression = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.ucFont = new Yutai.Pipeline.Editor.Controls.UcFont();
            this.cmbFlagLineLayer = new Yutai.Pipeline.Editor.Controls.UcSelectFeatureClass();
            this.cmbFlagAnnoLayer = new Yutai.Pipeline.Editor.Controls.UcSelectFeatureClass();
            this.cmbFlagLayer = new Yutai.Pipeline.Editor.Controls.UcSelectFeatureClass();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbFlagLineLayer);
            this.groupBox1.Controls.Add(this.cmbFlagAnnoLayer);
            this.groupBox1.Controls.Add(this.cmbFlagLayer);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(369, 111);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图层设置";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "辅助线层：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "注记图层：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "扯旗图层：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TxtExpression);
            this.groupBox2.Controls.Add(this.btnExpression);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 129);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(369, 92);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "表达式设置";
            // 
            // TxtExpression
            // 
            this.TxtExpression.Location = new System.Drawing.Point(79, 20);
            this.TxtExpression.Multiline = true;
            this.TxtExpression.Name = "TxtExpression";
            this.TxtExpression.ReadOnly = true;
            this.TxtExpression.Size = new System.Drawing.Size(282, 37);
            this.TxtExpression.TabIndex = 3;
            // 
            // btnExpression
            // 
            this.btnExpression.Location = new System.Drawing.Point(274, 63);
            this.btnExpression.Name = "btnExpression";
            this.btnExpression.Size = new System.Drawing.Size(87, 23);
            this.btnExpression.TabIndex = 2;
            this.btnExpression.Text = "表达式";
            this.btnExpression.UseVisualStyleBackColor = true;
            this.btnExpression.Click += new System.EventHandler(this.btnExpression_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "管线点字段：";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(306, 336);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(225, 336);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ucFont
            // 
            this.ucFont.Location = new System.Drawing.Point(12, 227);
            this.ucFont.Name = "ucFont";
            this.ucFont.Size = new System.Drawing.Size(369, 103);
            this.ucFont.TabIndex = 10;
            // 
            // cmbFlagLineLayer
            // 
            this.cmbFlagLineLayer.Label = "";
            this.cmbFlagLineLayer.LabelWidth = 25;
            this.cmbFlagLineLayer.Location = new System.Drawing.Point(79, 82);
            this.cmbFlagLineLayer.Margin = new System.Windows.Forms.Padding(5);
            this.cmbFlagLineLayer.Name = "cmbFlagLineLayer";
            this.cmbFlagLineLayer.Size = new System.Drawing.Size(282, 20);
            this.cmbFlagLineLayer.TabIndex = 0;
            this.cmbFlagLineLayer.VisibleOpenButton = false;
            // 
            // cmbFlagAnnoLayer
            // 
            this.cmbFlagAnnoLayer.Label = "";
            this.cmbFlagAnnoLayer.LabelWidth = 25;
            this.cmbFlagAnnoLayer.Location = new System.Drawing.Point(79, 52);
            this.cmbFlagAnnoLayer.Margin = new System.Windows.Forms.Padding(5);
            this.cmbFlagAnnoLayer.Name = "cmbFlagAnnoLayer";
            this.cmbFlagAnnoLayer.Size = new System.Drawing.Size(282, 20);
            this.cmbFlagAnnoLayer.TabIndex = 0;
            this.cmbFlagAnnoLayer.VisibleOpenButton = false;
            // 
            // cmbFlagLayer
            // 
            this.cmbFlagLayer.Label = "";
            this.cmbFlagLayer.LabelWidth = 25;
            this.cmbFlagLayer.Location = new System.Drawing.Point(79, 22);
            this.cmbFlagLayer.Margin = new System.Windows.Forms.Padding(5);
            this.cmbFlagLayer.Name = "cmbFlagLayer";
            this.cmbFlagLayer.Size = new System.Drawing.Size(282, 20);
            this.cmbFlagLayer.TabIndex = 0;
            this.cmbFlagLayer.VisibleOpenButton = false;
            this.cmbFlagLayer.SelectComplateEvent += new Yutai.Pipeline.Editor.Controls.SelectComplateHandler(this.cmbFlagLayer_SelectComplateEvent);
            // 
            // FrmCheQiConfig
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(393, 366);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.ucFont);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCheQiConfig";
            this.Text = "管线扯旗设置";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private Controls.UcSelectFeatureClass cmbFlagLayer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Controls.UcSelectFeatureClass cmbFlagLineLayer;
        private Controls.UcSelectFeatureClass cmbFlagAnnoLayer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox TxtExpression;
        private System.Windows.Forms.Button btnExpression;
        private System.Windows.Forms.Label label4;
        private Controls.UcFont ucFont;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}