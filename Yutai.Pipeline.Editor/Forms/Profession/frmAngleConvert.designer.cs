namespace Yutai.Pipeline.Editor.Forms.Profession
{
    partial class FrmAngleConvert
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbField = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonDegreeToRadian = new System.Windows.Forms.RadioButton();
            this.radioButtonRadianToDegree = new System.Windows.Forms.RadioButton();
            this.btnConvert = new System.Windows.Forms.Button();
            this.txtDegree = new System.Windows.Forms.TextBox();
            this.txtRadian = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ucSelectFeatureClass1 = new Yutai.Pipeline.Editor.Controls.UcSelectFeatureClass();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "要素类：";
            // 
            // cmbField
            // 
            this.cmbField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbField.FormattingEnabled = true;
            this.cmbField.Location = new System.Drawing.Point(72, 37);
            this.cmbField.Name = "cmbField";
            this.cmbField.Size = new System.Drawing.Size(183, 20);
            this.cmbField.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "字段：";
            // 
            // radioButtonDegreeToRadian
            // 
            this.radioButtonDegreeToRadian.AutoSize = true;
            this.radioButtonDegreeToRadian.Checked = true;
            this.radioButtonDegreeToRadian.Location = new System.Drawing.Point(72, 63);
            this.radioButtonDegreeToRadian.Name = "radioButtonDegreeToRadian";
            this.radioButtonDegreeToRadian.Size = new System.Drawing.Size(83, 16);
            this.radioButtonDegreeToRadian.TabIndex = 5;
            this.radioButtonDegreeToRadian.TabStop = true;
            this.radioButtonDegreeToRadian.Text = "角度转弧度";
            this.radioButtonDegreeToRadian.UseVisualStyleBackColor = true;
            // 
            // radioButtonRadianToDegree
            // 
            this.radioButtonRadianToDegree.AutoSize = true;
            this.radioButtonRadianToDegree.Location = new System.Drawing.Point(172, 63);
            this.radioButtonRadianToDegree.Name = "radioButtonRadianToDegree";
            this.radioButtonRadianToDegree.Size = new System.Drawing.Size(83, 16);
            this.radioButtonRadianToDegree.TabIndex = 5;
            this.radioButtonRadianToDegree.Text = "弧度转角度";
            this.radioButtonRadianToDegree.UseVisualStyleBackColor = true;
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(396, 56);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(75, 23);
            this.btnConvert.TabIndex = 2;
            this.btnConvert.Text = "转换";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // txtDegree
            // 
            this.txtDegree.Location = new System.Drawing.Point(57, 20);
            this.txtDegree.Name = "txtDegree";
            this.txtDegree.Size = new System.Drawing.Size(100, 21);
            this.txtDegree.TabIndex = 6;
            this.txtDegree.TextChanged += new System.EventHandler(this.txtDegree_TextChanged);
            // 
            // txtRadian
            // 
            this.txtRadian.Location = new System.Drawing.Point(233, 20);
            this.txtRadian.Name = "txtRadian";
            this.txtRadian.Size = new System.Drawing.Size(100, 21);
            this.txtRadian.TabIndex = 6;
            this.txtRadian.TextChanged += new System.EventHandler(this.txtRadian_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "角度：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(186, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "弧度：";
            // 
            // ucSelectFeatureClass1
            // 
            this.ucSelectFeatureClass1.Label = "";
            this.ucSelectFeatureClass1.LabelWidth = 25;
            this.ucSelectFeatureClass1.Location = new System.Drawing.Point(72, 9);
            this.ucSelectFeatureClass1.Margin = new System.Windows.Forms.Padding(5);
            this.ucSelectFeatureClass1.Name = "ucSelectFeatureClass1";
            this.ucSelectFeatureClass1.Size = new System.Drawing.Size(397, 20);
            this.ucSelectFeatureClass1.TabIndex = 8;
            this.ucSelectFeatureClass1.VisibleOpenButton = false;
            this.ucSelectFeatureClass1.SelectComplateEvent += new Yutai.Pipeline.Editor.Controls.SelectComplateHandler(this.ucSelectFeatureClass1_SelectComplateEvent);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtDegree);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtRadian);
            this.groupBox1.Location = new System.Drawing.Point(15, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(456, 55);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "快捷工具";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Yutai.Pipeline.Editor.Properties.Resources.img_arrow_change;
            this.pictureBox1.Location = new System.Drawing.Point(163, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // FrmAngleConvert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 148);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ucSelectFeatureClass1);
            this.Controls.Add(this.radioButtonRadianToDegree);
            this.Controls.Add(this.radioButtonDegreeToRadian);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbField);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "FrmAngleConvert";
            this.Text = "角度/弧度转换";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbField;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButtonDegreeToRadian;
        private System.Windows.Forms.RadioButton radioButtonRadianToDegree;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.TextBox txtDegree;
        private System.Windows.Forms.TextBox txtRadian;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Controls.UcSelectFeatureClass ucSelectFeatureClass1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}