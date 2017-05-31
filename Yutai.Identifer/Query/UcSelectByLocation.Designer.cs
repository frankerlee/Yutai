using System;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Plugins.Identifer.Query
{
    partial class UcSelectByLocation
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
            this.cboOperationType = new System.Windows.Forms.ComboBox();
            this.checkedListBoxLayer = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkUsetSelectedLayer = new System.Windows.Forms.CheckBox();
            this.cboSpatialRelation = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboSourceLayer = new System.Windows.Forms.ComboBox();
            this.chkUseSelectFeature = new System.Windows.Forms.CheckBox();
            this.chkUseBuffer = new System.Windows.Forms.CheckBox();
            this.txtRadius = new System.Windows.Forms.TextBox();
            this.cboUnit = new System.Windows.Forms.ComboBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblStatu = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "根据要素的空间关系从图层中选择要素";
            // 
            // cboOperationType
            // 
            this.cboOperationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOperationType.Items.AddRange(new object[] {
            "选择要素",
            "选择要素,并添加到选择集中",
            "的选择集中删除",
            "的选择集中选择"});
            this.cboOperationType.Location = new System.Drawing.Point(8, 168);
            this.cboOperationType.Name = "cboOperationType";
            this.cboOperationType.Size = new System.Drawing.Size(248, 20);
            this.cboOperationType.TabIndex = 1;
            // 
            // checkedListBoxLayer
            // 
            this.checkedListBoxLayer.Location = new System.Drawing.Point(8, 56);
            this.checkedListBoxLayer.Name = "checkedListBoxLayer";
            this.checkedListBoxLayer.Size = new System.Drawing.Size(248, 100);
            this.checkedListBoxLayer.TabIndex = 2;
            this.checkedListBoxLayer.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxLayer_ItemCheck);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "从下列图层";
            // 
            // chkUsetSelectedLayer
            // 
            this.chkUsetSelectedLayer.Location = new System.Drawing.Point(96, 32);
            this.chkUsetSelectedLayer.Name = "chkUsetSelectedLayer";
            this.chkUsetSelectedLayer.Size = new System.Drawing.Size(112, 19);
            this.chkUsetSelectedLayer.TabIndex = 4;
            this.chkUsetSelectedLayer.Text = "只显示可选图层";
            this.chkUsetSelectedLayer.Visible = false;
            this.chkUsetSelectedLayer.CheckedChanged += new System.EventHandler(this.chkUsetSelectedLayer_CheckedChanged);
            // 
            // cboSpatialRelation
            // 
            this.cboSpatialRelation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSpatialRelation.Items.AddRange(new object[] {
            "相交",
            "包围矩形相交",
            "相接",
            "重叠",
            "被包含",
            "包含"});
            this.cboSpatialRelation.Location = new System.Drawing.Point(8, 248);
            this.cboSpatialRelation.Name = "cboSpatialRelation";
            this.cboSpatialRelation.Size = new System.Drawing.Size(248, 20);
            this.cboSpatialRelation.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "与图层中的要素";
            // 
            // cboSourceLayer
            // 
            this.cboSourceLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSourceLayer.Location = new System.Drawing.Point(8, 216);
            this.cboSourceLayer.Name = "cboSourceLayer";
            this.cboSourceLayer.Size = new System.Drawing.Size(144, 20);
            this.cboSourceLayer.TabIndex = 7;
            this.cboSourceLayer.SelectedIndexChanged += new System.EventHandler(this.cboSourceLayer_SelectedIndexChanged);
            // 
            // chkUseSelectFeature
            // 
            this.chkUseSelectFeature.Location = new System.Drawing.Point(160, 216);
            this.chkUseSelectFeature.Name = "chkUseSelectFeature";
            this.chkUseSelectFeature.Size = new System.Drawing.Size(104, 19);
            this.chkUseSelectFeature.TabIndex = 8;
            this.chkUseSelectFeature.Text = "使用选中的要素";
            // 
            // chkUseBuffer
            // 
            this.chkUseBuffer.Location = new System.Drawing.Point(8, 280);
            this.chkUseBuffer.Name = "chkUseBuffer";
            this.chkUseBuffer.Size = new System.Drawing.Size(168, 19);
            this.chkUseBuffer.TabIndex = 9;
            this.chkUseBuffer.Text = "对要素进行缓冲区操作";
            this.chkUseBuffer.CheckedChanged += new System.EventHandler(this.chkUseBuffer_CheckedChanged);
            // 
            // txtRadius
            // 
            this.txtRadius.Location = new System.Drawing.Point(8, 304);
            this.txtRadius.Name = "txtRadius";
            this.txtRadius.Size = new System.Drawing.Size(88, 21);
            this.txtRadius.TabIndex = 10;
            this.txtRadius.Text = "0";
            // 
            // cboUnit
            // 
            this.cboUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUnit.Location = new System.Drawing.Point(104, 304);
            this.cboUnit.Name = "cboUnit";
            this.cboUnit.Size = new System.Drawing.Size(96, 20);
            this.cboUnit.TabIndex = 11;
            this.cboUnit.Visible = false;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(112, 336);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(64, 24);
            this.btnApply.TabIndex = 12;
            this.btnApply.Text = "应用";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblStatu);
            this.panel1.Location = new System.Drawing.Point(40, 160);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(152, 64);
            this.panel1.TabIndex = 14;
            this.panel1.Visible = false;
            // 
            // lblStatu
            // 
            this.lblStatu.AutoSize = true;
            this.lblStatu.Location = new System.Drawing.Point(8, 8);
            this.lblStatu.Name = "lblStatu";
            this.lblStatu.Size = new System.Drawing.Size(107, 12);
            this.lblStatu.TabIndex = 14;
            this.lblStatu.Text = "正在查找请稍候...";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(192, 336);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(64, 24);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "关闭";
            this.btnClose.Visible = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // UcSelectByLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.cboUnit);
            this.Controls.Add(this.txtRadius);
            this.Controls.Add(this.chkUseBuffer);
            this.Controls.Add(this.chkUseSelectFeature);
            this.Controls.Add(this.cboSourceLayer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboSpatialRelation);
            this.Controls.Add(this.chkUsetSelectedLayer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkedListBoxLayer);
            this.Controls.Add(this.cboOperationType);
            this.Controls.Add(this.label1);
            this.Name = "UcSelectByLocation";
            this.Size = new System.Drawing.Size(280, 384);
            this.Load += new System.EventHandler(this.UcSelectByLocation_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;

        private ComboBox cboOperationType;

        private Label label2;

        private ComboBox cboSpatialRelation;

        private Label label3;

        private CheckedListBox checkedListBoxLayer;

        private CheckBox chkUsetSelectedLayer;

        private CheckBox chkUseSelectFeature;

        private CheckBox chkUseBuffer;

        private TextBox txtRadius;

        private ComboBox cboUnit;

        private ComboBox cboSourceLayer;

        private Button btnApply;

        private Panel panel1;

        private Label lblStatu;
        private Button btnClose;
    }
}
