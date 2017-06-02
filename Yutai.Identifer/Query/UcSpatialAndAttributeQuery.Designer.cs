using System;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Plugins.Identifer.Query
{
    partial class UcSpatialAndAttributeQuery
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
            components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.btnApply = new Button();
            this.chkUsetSelectedLayer = new CheckBox();
            this.label2 = new Label();
            this.cboOperationType = new ComboBox();
            this.label1 = new Label();
            this.memEditWhereCaluse = new TextBox();
            this.comboBoxLayer = new ComboBox();
            this.label4 = new Label();
            this.groupBox1 = new GroupBox();
            this.cboUnit = new ComboBox();
            this.txtRadius = new TextBox();
            this.chkUseBuffer = new CheckBox();
            this.chkUseSelectFeature = new CheckBox();
            this.cboSourceLayer = new ComboBox();
            this.label3 = new Label();
            this.cboSpatialRelation = new ComboBox();
            this.panel1 = new Panel();
            this.lblStatu = new Label();
            this.btnCreateQuery = new Button();
          
            this.groupBox1.SuspendLayout();
           
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.btnApply.Location = new Point(267, 331);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(64, 24);
            this.btnApply.TabIndex = 27;
            this.btnApply.Text = "应用";
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            this.chkUsetSelectedLayer.Location = new Point(52, 45);
            this.chkUsetSelectedLayer.Name = "chkUsetSelectedLayer";
            this.chkUsetSelectedLayer.Text = "只显示可选图层";
            this.chkUsetSelectedLayer.Size = new Size(112, 19);
            this.chkUsetSelectedLayer.TabIndex = 19;
            this.chkUsetSelectedLayer.Visible = false;
            this.chkUsetSelectedLayer.CheckedChanged += new EventHandler(this.chkUsetSelectedLayer_CheckedChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(17, 27);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "图层";
            this.cboOperationType.Text = "选择要素";
            this.cboOperationType.Location = new Point(17, 70);
            this.cboOperationType.Name = "cboOperationType";
          
            cboOperationType.Items.AddRange(new object[] { "选择要素", "选择要素,并添加到选择集中", "的选择集中删除", "的选择集中选择" });
           
            this.cboOperationType.Size = new Size(248, 21);
            this.cboOperationType.TabIndex = 16;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(17, 3);
            this.label1.Name = "label1";
            this.label1.Size = new Size(209, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "根据要素的空间关系从图层中选择要素";
            this.memEditWhereCaluse.Text = "";
            this.memEditWhereCaluse.Location = new Point(7, 263);
            this.memEditWhereCaluse.Name = "memEditWhereCaluse";
            this.memEditWhereCaluse.Size = new Size(343, 62);
            this.memEditWhereCaluse.TabIndex = 29;
            this.comboBoxLayer.Text = "";
            this.comboBoxLayer.Location = new Point(52, 18);
            this.comboBoxLayer.Name = "comboBoxLayer";
            
            this.comboBoxLayer.Size = new Size(288, 21);
            this.comboBoxLayer.TabIndex = 49;
            this.comboBoxLayer.SelectedIndexChanged += new EventHandler(this.comboBoxLayer_SelectedIndexChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(13, 241);
            this.label4.Name = "label4";
            this.label4.Size = new Size(125, 12);
            this.label4.TabIndex = 50;
            this.label4.Text = "并满足如下条件的要素";
            this.groupBox1.Controls.Add(this.cboUnit);
            this.groupBox1.Controls.Add(this.txtRadius);
            this.groupBox1.Controls.Add(this.chkUseBuffer);
            this.groupBox1.Controls.Add(this.chkUseSelectFeature);
            this.groupBox1.Controls.Add(this.cboSourceLayer);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboSpatialRelation);
            this.groupBox1.Location = new Point(7, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(343, 145);
            this.groupBox1.TabIndex = 51;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "空间过滤";
            this.cboUnit.Text = "";
            this.cboUnit.Location = new Point(106, 118);
            this.cboUnit.Name = "cboUnit";
          
            this.cboUnit.Size = new Size(96, 21);
            this.cboUnit.TabIndex = 33;
            this.cboUnit.Visible = false;
            this.txtRadius.Text = "0";
            this.txtRadius.Location = new Point(10, 118);
            this.txtRadius.Name = "txtRadius";
            this.txtRadius.Size = new Size(88, 21);
            this.txtRadius.TabIndex = 32;
            this.chkUseBuffer.Location = new Point(10, 94);
            this.chkUseBuffer.Name = "chkUseBuffer";
            this.chkUseBuffer.Text = "对要素进行缓冲区操作";
            this.chkUseBuffer.Size = new Size(168, 19);
            this.chkUseBuffer.TabIndex = 31;
            this.chkUseBuffer.CheckedChanged += new EventHandler(this.chkUseBuffer_CheckedChanged);
            this.chkUseSelectFeature.Location = new Point(162, 36);
            this.chkUseSelectFeature.Name = "chkUseSelectFeature";
            this.chkUseSelectFeature.Text = "使用选中的要素";
            this.chkUseSelectFeature.Size = new Size(104, 19);
            this.chkUseSelectFeature.TabIndex = 30;
            this.cboSourceLayer.Text = "";
            this.cboSourceLayer.Location = new Point(10, 36);
            this.cboSourceLayer.Name = "cboSourceLayer";
          
            this.cboSourceLayer.Size = new Size(144, 21);
            this.cboSourceLayer.TabIndex = 29;
            this.cboSourceLayer.SelectedIndexChanged += new EventHandler(this.cboSourceLayer_SelectedIndexChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(10, 20);
            this.label3.Name = "label3";
            this.label3.Size = new Size(89, 12);
            this.label3.TabIndex = 28;
            this.label3.Text = "与图层中的要素";
            this.cboSpatialRelation.Text = "相交";
            this.cboSpatialRelation.Location = new Point(10, 65);
            this.cboSpatialRelation.Name = "cboSpatialRelation";
            cboSpatialRelation.Items.AddRange(new object[] { "相交", "包围矩形相交", "相接", "重叠", "被包含", "包含" });
           
            this.cboSpatialRelation.Size = new Size(248, 21);
            this.cboSpatialRelation.TabIndex = 27;
            this.panel1.Controls.Add(this.lblStatu);
            this.panel1.Location = new Point(162, 191);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(152, 64);
            this.panel1.TabIndex = 52;
            this.panel1.Visible = false;
            this.lblStatu.AutoSize = true;
            this.lblStatu.Location = new Point(8, 8);
            this.lblStatu.Name = "lblStatu";
            this.lblStatu.Size = new Size(107, 12);
            this.lblStatu.TabIndex = 14;
            this.lblStatu.Text = "正在查找请稍候...";
            this.btnCreateQuery.Location = new Point(7, 331);
            this.btnCreateQuery.Name = "btnCreateQuery";
            this.btnCreateQuery.Size = new Size(93, 24);
            this.btnCreateQuery.TabIndex = 53;
            this.btnCreateQuery.Text = "构建查询条件";
            this.btnCreateQuery.Click += new EventHandler(this.btnCreateQuery_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.btnCreateQuery);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.comboBoxLayer);
            base.Controls.Add(this.memEditWhereCaluse);
            base.Controls.Add(this.btnApply);
            base.Controls.Add(this.chkUsetSelectedLayer);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.cboOperationType);
            base.Controls.Add(this.label1);
            base.Name = "SpatialAndAttributeQueryCtrl";
            base.Size = new Size(366, 384);
            base.Load += new EventHandler(this.UcSpatialAndAttributeQuery_Load);
         
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
         
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion

        private Button btnApply;

        private CheckBox chkUsetSelectedLayer;

        private Label label2;

        private ComboBox cboOperationType;

        private Label label1;

        private TextBox memEditWhereCaluse;

        private ComboBox comboBoxLayer;

        private Label label4;

        private GroupBox groupBox1;

        private ComboBox cboUnit;

        private TextBox txtRadius;

        private CheckBox chkUseBuffer;

        private CheckBox chkUseSelectFeature;

        private ComboBox cboSourceLayer;

        private Label label3;

        private ComboBox cboSpatialRelation;

        private Panel panel1;

        private Label lblStatu;

        private Button btnCreateQuery;

    }
}
