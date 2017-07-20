namespace Yutai.Pipeline.Editor.Forms.Mark
{
    partial class FrmMultiCheQiConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMultiCheQiConfig));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelFields = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ucSelectFeatureClasses1 = new Yutai.Pipeline.Editor.Controls.UcSelectFeatureClasses();
            this.cmbFlagLineLayer = new Yutai.Pipeline.Editor.Controls.UcSelectFeatureClass();
            this.cmbFlagAnnoLayer = new Yutai.Pipeline.Editor.Controls.UcSelectFeatureClass();
            this.ucFontContent = new Yutai.Pipeline.Editor.Controls.UcFont();
            this.ucFontHeader = new Yutai.Pipeline.Editor.Controls.UcFont();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panelFields);
            this.groupBox1.Location = new System.Drawing.Point(12, 230);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(459, 223);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "属性信息";
            // 
            // panelFields
            // 
            this.panelFields.AutoScroll = true;
            this.panelFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFields.Location = new System.Drawing.Point(3, 17);
            this.panelFields.Name = "panelFields";
            this.panelFields.Size = new System.Drawing.Size(453, 203);
            this.panelFields.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(477, 247);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(623, 427);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(542, 427);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(477, 312);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucSelectFeatureClasses1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cmbFlagLineLayer);
            this.groupBox2.Controls.Add(this.cmbFlagAnnoLayer);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(459, 212);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "图层设置";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "辅助线层：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "注记图层：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "扯旗图层：";
            // 
            // ucSelectFeatureClasses1
            // 
            this.ucSelectFeatureClasses1.Location = new System.Drawing.Point(79, 19);
            this.ucSelectFeatureClasses1.Map = null;
            this.ucSelectFeatureClasses1.Name = "ucSelectFeatureClasses1";
            this.ucSelectFeatureClasses1.SelectedFeatureLayerList = ((System.Collections.Generic.List<ESRI.ArcGIS.Carto.IFeatureLayer>)(resources.GetObject("ucSelectFeatureClasses1.SelectedFeatureLayerList")));
            this.ucSelectFeatureClasses1.Size = new System.Drawing.Size(372, 126);
            this.ucSelectFeatureClasses1.TabIndex = 2;
            // 
            // cmbFlagLineLayer
            // 
            this.cmbFlagLineLayer.Label = "";
            this.cmbFlagLineLayer.LabelWidth = 25;
            this.cmbFlagLineLayer.Location = new System.Drawing.Point(79, 183);
            this.cmbFlagLineLayer.Margin = new System.Windows.Forms.Padding(5);
            this.cmbFlagLineLayer.Name = "cmbFlagLineLayer";
            this.cmbFlagLineLayer.Size = new System.Drawing.Size(372, 20);
            this.cmbFlagLineLayer.TabIndex = 0;
            this.cmbFlagLineLayer.VisibleOpenButton = false;
            // 
            // cmbFlagAnnoLayer
            // 
            this.cmbFlagAnnoLayer.Label = "";
            this.cmbFlagAnnoLayer.LabelWidth = 25;
            this.cmbFlagAnnoLayer.Location = new System.Drawing.Point(79, 153);
            this.cmbFlagAnnoLayer.Margin = new System.Windows.Forms.Padding(5);
            this.cmbFlagAnnoLayer.Name = "cmbFlagAnnoLayer";
            this.cmbFlagAnnoLayer.Size = new System.Drawing.Size(372, 20);
            this.cmbFlagAnnoLayer.TabIndex = 0;
            this.cmbFlagAnnoLayer.VisibleOpenButton = false;
            // 
            // ucFontContent
            // 
            this.ucFontContent.GroupHeader = "内容字体设置";
            this.ucFontContent.Location = new System.Drawing.Point(477, 121);
            this.ucFontContent.Name = "ucFontContent";
            this.ucFontContent.Size = new System.Drawing.Size(221, 103);
            this.ucFontContent.TabIndex = 1;
            // 
            // ucFontHeader
            // 
            this.ucFontHeader.GroupHeader = "表头字体设置";
            this.ucFontHeader.Location = new System.Drawing.Point(477, 12);
            this.ucFontHeader.Name = "ucFontHeader";
            this.ucFontHeader.Size = new System.Drawing.Size(221, 103);
            this.ucFontHeader.TabIndex = 1;
            // 
            // FrmMultiCheQiConfig
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(710, 465);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.ucFontContent);
            this.Controls.Add(this.ucFontHeader);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmMultiCheQiConfig";
            this.Text = "多要素扯旗设置";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Controls.UcFont ucFontHeader;
        private Controls.UcFont ucFontContent;
        private System.Windows.Forms.Panel panelFields;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Controls.UcSelectFeatureClass cmbFlagLineLayer;
        private Controls.UcSelectFeatureClass cmbFlagAnnoLayer;
        private Controls.UcSelectFeatureClasses ucSelectFeatureClasses1;
    }
}